using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawner : MonoBehaviour {
    public GameObject prefab;
    Vector3 location;
    public static int counter = 0;

    // Use this for initialization
    void Start () {
        InvokeRepeating("spawnCollectables", 2.0f, 1.0f);
    }
    void spawnCollectables()
    {
        string message = counter.ToString() + " instances";
        Debug.Log(message);
        if (counter < 20){
            counter++;
            location = new Vector3(Random.Range(-30, 30), Random.Range(-15, 15));
            Instantiate(prefab, location, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
