using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuysSpawner : MonoBehaviour
{
    public GameObject patient;
    public GameObject pig;
    public GameObject Robber;
    public GameObject Santa;
    public GameObject SoccerPlayerRd;

    private float randomVal; 
    private float zVal;

    private GameObject newGuy;

    public GameObject[] towns;

    // Start is called before the first frame update
    void Start()
    {
        towns = GameObject.FindGameObjectsWithTag("Town");
        statics.canSpawnPeople = true;
    }

    void Update(){
        if(statics.canSpawnPeople && !statics.stopSpawn){
            statics.canSpawnPeople = false;
            spawnAll();
            StartCoroutine(LetSpawn());
        }
    }

    void spawnAll(){
        randomVal = Random.Range(0, 10);
        if (randomVal > 7 && statics.inPlay){
            spawnPeople(pig);
        }

        randomVal = Random.Range(0, 10);
        if (randomVal > 2){
            spawnPeople(SoccerPlayerRd);
        }

        randomVal = Random.Range(0, 10);
        if (randomVal > 1){
            spawnPeople(patient);
        }

        randomVal = Random.Range(0, 10);
        if (randomVal > 8){
            spawnPeople(Santa);
        }

        randomVal = Random.Range(0, 10);
        if (randomVal > 7){
            spawnPeople(Robber);
        }
    }

    void spawnPeople(GameObject who){
        float randomSpawn = Random.Range(0, 10);
        if (randomSpawn > 6) {
            zVal = -0.78f;
        }
        else {
            zVal = -0.2f;
        }
        newGuy = Instantiate(who, new Vector3(10f, 0.33f, zVal), Quaternion.Euler(0, -90, 0));
        connectWithTown(newGuy);
    }

    void connectWithTown(GameObject peop){
        if (towns.Length == 1){
            peop.transform.parent = towns[0].transform;
        }
        else {
            peop.transform.parent = towns[1].transform;
        }      
    }

    IEnumerator LetSpawn(){
        yield return new WaitForSeconds(5f);
        statics.canSpawnPeople = true;
    }
}
