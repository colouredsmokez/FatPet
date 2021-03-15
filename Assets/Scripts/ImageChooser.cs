using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageChooser : MonoBehaviour
{
    public static int imageNumber; //image1 = 1; image2 = 2; image3 = 3; image4 = 4;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public int randNum;

    public void startChoosing() {
        StartCoroutine("thisImage");
    }

    IEnumerator thisImage(){
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(false);

        randNum = Random.Range(1, 5);
        if (randNum == 1) {
            image1.SetActive(true);
            yield return new WaitForSeconds(2);
            image1.SetActive(false);
        }
        else if (randNum == 2) {
            image2.SetActive(true);
            yield return new WaitForSeconds(2);
            image2.SetActive(false);
        }
        else if (randNum == 3) {
            image3.SetActive(true);
            yield return new WaitForSeconds(2);
            image3.SetActive(false);
        }
        else if (randNum == 4) {
            image4.SetActive(true);
            yield return new WaitForSeconds(2);
            image4.SetActive(false);
        } 
    }

}
