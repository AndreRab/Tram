using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{
    public float speed = 0.001f;
    public int kf;

    void Start(){
        float randomVal = Random.Range(0, 21);
        float randPos = Random.Range(0f, 2.5f);
        if (randomVal > 15){
            kf = 1;
            transform.position = new Vector3(-4f , transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else {
            kf = -1;
            transform.position = new Vector3(4f + randPos, transform.position.y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x  + speed*kf, transform.position.y, transform.position.z);
        if(transform.position.x <= -4 || transform.position.x >= 6){
            Destroy(transform.gameObject);
        }
    }
}
