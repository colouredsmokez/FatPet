using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;

public static class AuthHandler {

    private const string apiKey = "AIzaSyBE2gNx21Sa9ETv6V34M5NuA4lWHHO8N0E";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void EmailVerificationSuccess();
    public delegate void EmailVerificationFail();

    public static string idToken;
    public static string userId;
    
    public static void SignUp(string email, string password, User user) {
        var payLoad = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
        RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}", payLoad).Then(response => {
            Debug.Log("Created User");
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, string>), ref deserialized);
            var authResponse = deserialized as Dictionary<string, string>;
            DatabaseHandler.PostUser(user, authResponse["localId"], () => { }, authResponse["idToken"]);
            SendEmailVerification(authResponse["idToken"]);
        });
    }

    private static void SendEmailVerification(string newIdToken) {
        var payLoad = $"{{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"{newIdToken}\"}}";
        RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={apiKey}", payLoad);
    }

    public static void SignIn(string email, string password) {
        var payLoad = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
        RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}", payLoad).Then(response => {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, string>), ref deserialized);
            var authResponse = deserialized as Dictionary<string, string>;
            CheckEmailVerification(authResponse["idToken"], 
                () => {
                    Debug.Log("Email verified, getting user info");
                    PlayerPrefs.SetString("login","success");
                    DatabaseHandler.GetUser(userId, user => { Debug.Log($"{user.name}, {user.email}, {user.password}"); }, idToken);
                    DatabaseHandler.GetUser(userId, user => { PlayerPrefs.SetString("user",user.name); }, idToken);
                },
                () => {
                    Debug.Log("Email not verified, try again");
                    PlayerPrefs.SetString("login","fail");
                });
        });
    }

    private static void CheckEmailVerification(string newIdToken, EmailVerificationSuccess callback,EmailVerificationFail fallback) {
        var payLoad = $"{{\"idToken\":\"{newIdToken}\"}}";
        RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:lookup?key={apiKey}", payLoad).Then(response => {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(UserData), ref deserialized);
            var authResponse = deserialized as UserData;
            if (authResponse.users[0].emailVerified) {
                userId = authResponse.users[0].localId;
                idToken = newIdToken;
                callback();
            } else {
                fallback();
            }
        });
    }
}
