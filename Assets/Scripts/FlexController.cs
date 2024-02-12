using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexController : MonoBehaviour
{
    public Animator anim;
    public Animator animTram;

    // Update is called once per frame
    void Update()
    {
        if(statics.speed == 0 ){
            anim.SetBool("CanFlex", false);
        }
        else{
            anim.SetBool("CanFlex", true);
        }
    }
}
