using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafSpawner : MonoBehaviour
{
    public GameObject Traf;
    //private float dist = 0f;
    private GameObject newTraf;
    public GameObject[] towns;
    //private float pastDist = 0f;

    // Update is called once per frame
    void Update()
    {
        if(statics.canSpawn){

            towns = GameObject.FindGameObjectsWithTag("Town");

            float randomDist = Random.Range(4f, 10f);
            // if (Mathf.Abs(randomDist - pastDist) < 4f){
            //     randomDist = pastDist + 4f;
            // }
            newTraf = Instantiate(Traf, new Vector3(randomDist, 0.2f, -0.8f),  Quaternion.Euler(0, -90, 0));
            //pastDist = randomDist;
            UnityEngine.Debug.Log("Spawn traf");
            if (towns.Length == 1){
                newTraf.transform.parent = towns[0].transform;
            }
            else {
                newTraf.transform.parent = towns[1].transform;
            }

            statics.canSpawn = false;
        }
    }
}
