using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour {

    public static AudioClip click,jump;
    //public static AudioClip change, change2, click, up, chucky, door, jumpy, potato;
    static AudioSource audioSrc;
    //public AudioSource BGM;

    // Start is called before the first frame update
    void Start() {
        click = Resources.Load<AudioClip>("click");
        jump = Resources.Load<AudioClip>("jump");
        audioSrc = GetComponent<AudioSource>();
        
        //change = Resources.Load<AudioClip>("change1");
        //up = Resources.Load<AudioClip>("lvlup"); 
        //chucky = Resources.Load<AudioClip>("Chunky_Monkey");
        //door = Resources.Load<AudioClip>("InfiniteDoors");
        //jumpy = Resources.Load<AudioClip>("JumpyGame");
        //potato = Resources.Load<AudioClip>("Potato");  
        //audioSrc = GetComponent<AudioSource>();
    }
/*
    public void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
*/
    public void changeBGM() {
        Destroy(gameObject);
        //DontDestroyOnLoad(Game_Manager.currentTrack);
    }

    public static void PlaySound(string clip) {
        switch (clip) {
            case ("click"): 
                audioSrc.PlayOneShot(click);
                break;
            case ("jump"): 
                audioSrc.PlayOneShot(jump);
                break;
            }
    }

}
