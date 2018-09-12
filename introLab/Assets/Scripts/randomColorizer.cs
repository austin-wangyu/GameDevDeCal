using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomColorizer : MonoBehaviour {

    private Color randomColor;
    SpriteRenderer spRend;
    

	// Use this for initialization
	void Start () {
        randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        spRend = GetComponent<SpriteRenderer>();
        spRend.color = randomColor;
        InvokeRepeating("updateColor", 0.0f, 0.5f);
	}

    void updateColor() {
        randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        spRend.color = randomColor;
    }
	
    // Update is called once per frame
	void Update () {
        
    }
}
