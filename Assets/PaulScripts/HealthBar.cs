using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public GameObject playerCharacter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float ratio = playerCharacter.GetComponent<CharacterController>().ratio;
        //Debug.Log(ratio);
        this.GetComponent<RectTransform>().localScale = new Vector3(1 - ratio,1,1);
        this.GetComponent<RectTransform>().localPosition = new Vector3(30 * ratio - 225,-50,0);

    }
}
