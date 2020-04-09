using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour {

    public GameObject fire;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Instantiate(fire, new Vector3(Random.Range(-14,14), Random.Range(-15, 15), -3), transform.rotation);
    }
}
