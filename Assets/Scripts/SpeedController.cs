using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    private bool canSlow = true;
    public Animator anim;
    public GameObject tram;
    // Update is called once per frame
    void Update()
    {
        if (statics.inPlay){
            if(Input.touchCount == 1 && statics.canTouch){
                if (statics.speed < 0.1f){
                    statics.speed = statics.speed + 0.00025f;
                }
                canSlow = true;
            } 
            else if (statics.speed > 0f) {
                StartCoroutine(LetSlow());
            }
        }
    }

    IEnumerator LetSlow(){
        yield return new WaitForSeconds(0.1f);
        if (canSlow && statics.speed - 0.001f >= 0){
            statics.speed = statics.speed - 0.001f;
        }
        else {
            canSlow = false;
            statics.speed = 0f;

            if (statics.canTouch == false){
                statics.endGame = true;
            }
        }
    }

    public void LetStopAnimation(){
        //tram.transform.position = new Vector3(0f, 0f, 0f);
        anim.SetBool("PlayAnim", false);
        anim.enabled = false;
        statics.inPlay = true;
        statics.canSpawnPeople = true;
    }
}