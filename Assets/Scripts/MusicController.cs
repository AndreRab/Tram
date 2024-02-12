using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource sound;
    private bool currentstate = true;

    // Update is called once per frame
    void Update()
    {
        if (statics.canPlaySound && !currentstate){
            sound.Play();
            currentstate = true;
        }
        else if (!statics.canPlaySound && currentstate){
            sound.Stop();
            currentstate = false;
        }
    }
}
