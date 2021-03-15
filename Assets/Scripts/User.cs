using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User {
    public string name;
    public string email;
    public string password;

    public User(string name, string email, string password) {
        this.name = name;
        this.email = email;
        this.password = password;
    }
}
