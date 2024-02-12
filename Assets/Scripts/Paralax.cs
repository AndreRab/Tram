using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float length;
    private float startPosition;
    private float dist = 0f;

    private GameObject[] peoples;
    private GameObject[] food;
    private GameObject[] towns;

    public GameObject BG;
    public bool wasSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        length = 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x <= -length +2 && wasSpawn != true) {
            var newBG = Instantiate(BG, new Vector3(21.6f, transform.position.y, transform.position.z), Quaternion.identity);
            wasSpawn = true;
        }

        else if(transform.position.x <= -2 * length) {
            towns = GameObject.FindGameObjectsWithTag("Town");
            peoples = GameObject.FindGameObjectsWithTag("People");
            food = GameObject.FindGameObjectsWithTag("Eat");

            foreach (GameObject peop in peoples)
            {
                peop.transform.parent = towns[1].transform;
            }
            statics.canSpawnPeople = true;

            foreach (GameObject dish in food)
            {
                dish.transform.parent = towns[1].transform;
            }
            
            Destroy(gameObject);
        }
        
        transform.position = new Vector3(startPosition + dist, transform.position.y, transform.position.z);
        dist = dist - statics.speed;
    }
}
