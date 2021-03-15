using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject flashText;

    private void Start() {
        InvokeRepeating("flashTheText",0f,0.5f);
    }

    private void flashTheText() {
        flashText.SetActive(!flashText.activeInHierarchy);
    }

    public void loadGame() {
        SceneManager.LoadScene("Game");
    }

    public void quit() {
        Application.Quit();
    }
}
