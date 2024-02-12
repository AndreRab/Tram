using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSpawner : MonoBehaviour
{
    public GameObject HotDogs;
    public GameObject Ketch;
    public GameObject Mayon;
    public GameObject Burgers;
    private GameObject newEat;

    private GameObject[] towns;

    private float randomVal;

    private bool canSpawnFood = true;
    
    // Start is called before the first frame update
    void Start()
    {
        towns = GameObject.FindGameObjectsWithTag("Town");
    }

    void Update(){
        if(canSpawnFood && !statics.stopSpawn){
            spawnFood();
            canSpawnFood = false;
            StartCoroutine(LetWaitForFood());
        }
    }

    void connectWithTown(GameObject peop){
        if (towns.Length == 1){
            peop.transform.parent = towns[0].transform;
        }
        else {
            peop.transform.parent = towns[1].transform;
        }      
    }

    void spawnFood(){
        for(int i = 0; i < 4; i ++){
            randomVal = Random.Range(0, 10);
            if (randomVal>7){
                newEat = Instantiate(choseFood(i), chosePlace(), Quaternion.identity);
                connectWithTown(newEat);
            }
        }
    }

    Vector3 chosePlace(){
        randomVal = Random.Range(4f, 8f);
        return new Vector3(randomVal, 0.2f, 0.5f);
    }

    GameObject choseFood(int i){
        randomVal = Random.Range(1,4);
        if (i == 0){
            return HotDogs;
        }
        else if(i == 1){
            return Mayon;
        }
        else if(i == 2){
            return Ketch;
        }
        else{
            return Burgers;
        }
    }

    IEnumerator LetWaitForFood(){
        yield return new WaitForSeconds(10f);
        canSpawnFood = true;
    }
}