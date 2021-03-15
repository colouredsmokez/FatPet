using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour {

    private int _clickCount;

    [SerializeField]
    private int _happiness;

    public int happiness {
        get { return _happiness; }
        set { _happiness = value; }
    }

    private void Start() {

        if (!PlayerPrefs.HasKey("then")) {
            PlayerPrefs.SetString("then",DateTime.Now.ToString());
        }

        if (!PlayerPrefs.HasKey("happiness")) {
            PlayerPrefs.SetInt("happiness",100);
        }
        _happiness = PlayerPrefs.GetInt("happiness");

        TimeSpan ts = DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("then"));
        _happiness -= (int) (ts.TotalHours*10);
        if (_happiness < 0) {
            _happiness = 0;
        }
        InvokeRepeating("updateTime",0f,30f);
    }

    private void updateTime() {
        PlayerPrefs.SetString("then",DateTime.Now.ToString());
    }
    
    private void Update() {
        GetComponent<Animator>().SetBool("Jump",gameObject.transform.position.y > -1.5f);
        if(Input.GetMouseButtonUp(0)) {
            Vector2 v = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v),Vector2.zero);
            if (hit) {
                if(hit.transform.gameObject.tag == "Pet") {
                    _clickCount++;
                    if (_clickCount >= 3) {
                        _clickCount = 0;
                        updateHappiness(1);
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0,10000));
                        SoundManagerScript.PlaySound("jump");
                    }
                }
            }
        }
    }

    public void updateHappiness(int i) {
        _happiness += i;
        if (_happiness > 100) {
            _happiness = 100;
        } else if (_happiness < 0) {
            _happiness = 0;
        }
    }
    
    public void saveHappiness() {
        PlayerPrefs.SetInt("happiness",_happiness);
    }

    public void restartPet() {
        PlayerPrefs.DeleteAll();
    }

    public void savePet() {
        saveHappiness();
        updateTime();
    }
}
