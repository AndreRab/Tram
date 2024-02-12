using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traf : MonoBehaviour
{
    public GameObject TrafLight;
    public GameObject greenLight;
    public GameObject yellowLight;
    public GameObject redLight;
    public AudioSource audioTraf;
    private float time;
    private bool isRed = false;

    // Start is called before the first frame update
    void Start()
    {
        redLight.SetActive(true);
        isRed = true;
        time = Random.Range(4f, 8f);
        StartCoroutine(LetGreen());
    }

    // Update is called once per frame
    void Update()
    {
        if(TrafLight.transform.position.x < -2 || TrafLight.transform.position.x > 15){
            Destroy(TrafLight);
        }
    }

    private void OnTriggerEnter(Collider hit) {
        if (hit.gameObject.tag == "Tram" && isRed) {
            UnityEngine.Debug.Log("Ride on the red");
            statics.canTouch = false;
            statics.canInAd = true;
        }
        else if (hit.gameObject.tag == "Tram"){
            statics.score = statics.score + 1;
            statics.canSpawn = true;
            if(statics.canPlaySound){
                audioTraf.Play();
            } 
        }
    }

    IEnumerator LetGreen(){
        yield return new WaitForSeconds(time);
        redLight.SetActive(false);
        isRed = false;
        yellowLight.SetActive(true);
        yield return new WaitForSeconds(1f);
        yellowLight.SetActive(false);
        greenLight.SetActive(true);
        StartCoroutine(LetRed());
    }

    IEnumerator LetRed(){
        yield return new WaitForSeconds(time);
        greenLight.SetActive(false);
        yellowLight.SetActive(true);
        yield return new WaitForSeconds(1f);
        yellowLight.SetActive(false);
        redLight.SetActive(true);
        isRed = true;
        StartCoroutine(LetGreen());
    }
}
