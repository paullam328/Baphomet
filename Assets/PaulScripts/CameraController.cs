using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject playerCharacter;
    public GameObject cameraBoundaries;
    //Used for setting cameraBoundaries:
    public float level1XBound;
    public float level2XBound;
    public float level3XBound;
    public float level4XBound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(
            Mathf.Clamp(playerCharacter.transform.position.x
                , cameraBoundaries.GetComponent<BoxCollider>().bounds.min.x
                , cameraBoundaries.GetComponent<BoxCollider>().bounds.max.x), 
            transform.position.y, 
            transform.position.z);
	}
}
