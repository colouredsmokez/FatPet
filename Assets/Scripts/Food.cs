using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Food {
    public string name;
    public string serving;
    public int calorie;

    public Food(string name, string serving, int calorie) {
        this.name = name;
        this.serving = serving;
        this.calorie = calorie;
    }
}
