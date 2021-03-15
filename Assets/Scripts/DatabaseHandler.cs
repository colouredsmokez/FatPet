using FullSerializer;
using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DatabaseHandler {

    private static readonly string databaseURL = "https://fatpet-e99b2.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void PostMealCallback();
    public delegate void GetMealCallback(Food meal);
    public delegate void GetMealsCallback(Dictionary<string, Food> meals);

    public static void PostMeal(string cat, string mealName,string serving, Food meal, PostMealCallback callback, string idToken) {
        RestClient.Put<Food>($"{databaseURL}Meals/{cat}/{mealName+", "+serving}.json?auth={idToken}", meal).Then(response => { callback(); });
    }

    public static void GetMeal(string cat, string mealName, GetMealCallback callback) {
        RestClient.Get<Food>($"{databaseURL}Meals/{cat}/{mealName}.json").Then(meal => { callback(meal); });
    }

    public static void GetMeals(string cat, GetMealsCallback callback) {
        RestClient.Get($"{databaseURL}Meals/{cat}.json").Then(response => {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, Food>), ref deserialized);
            var meals = deserialized as Dictionary<string, Food>;
            callback(meals);
        });
    }

    public delegate void PostUserCallback();
    public delegate void GetUserCallback(User user);
    public delegate void GetUsersCallback(Dictionary<string, User> users);
    
    public static void PostUser(User user, string userId, PostUserCallback callback, string idToken) {
        RestClient.Put<User>($"{databaseURL}Users/{userId}.json?auth={idToken}", user).Then(response => { callback(); });
    }

    public static void GetUser(string userId, GetUserCallback callback, string idToken) {
        RestClient.Get<User>($"{databaseURL}Users/{userId}.json?auth={idToken}").Then(user => { callback(user); });
    }

    public static void GetUsers(GetUsersCallback callback) {
        RestClient.Get($"{databaseURL}Users.json?auth={AuthHandler.idToken}").Then(response => {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, User>), ref deserialized);
            var users = deserialized as Dictionary<string, User>;
            callback(users);
        });
    }
}
