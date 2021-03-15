using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordField : MonoBehaviour {
    public string password = "My Password";

    void OnGUI() {
        password = GUI.PasswordField (new Rect (10, 10 , 200, 20), password, "*"[0], 25);
    }
}
