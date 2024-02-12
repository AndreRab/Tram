using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchChecker : MonoBehaviour
{
    private GameObject selfObject;
    public AudioSource audioEat;

    void Start(){
        selfObject =  transform.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if(raycastHit.transform.gameObject == selfObject){
                    addAndSave();
                }
            }
        } 
        if(transform.position.x <= -4 || transform.position.x > 10){
            Destroy(selfObject);
        }
    }

    void addAndSave(){
        UnityEngine.Debug.Log("Something Hit");
        if(statics.canPlaySound){
            audioEat.Play();
        }
        statics.coins ++;
        PlayerPrefs.SetInt("Coins", statics.coins);
        StartCoroutine(StartDestroy());
    }

    IEnumerator StartDestroy(){
        yield return new WaitForSeconds(0.25f);
        Destroy(selfObject);        
    }
}
