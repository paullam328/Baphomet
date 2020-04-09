using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunArm : MonoBehaviour {

    public GameObject playerCharacter;
    public float originalX;
    public float originalY;

	// Use this for initialization
	void Start () {
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        originalX = playerCharacter.GetComponent<CharacterController>().transform.position.x;
        originalY = playerCharacter.GetComponent<CharacterController>().transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        // transform.position = new Vector3(originalX - playerCharacter.GetComponent<CharacterController>().zRot /90 , originalY + playerCharacter.GetComponent<CharacterController>().zRot / 90,0);
        if (!playerCharacter.GetComponent<CharacterController>().isFacingLeft)
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
            //transform.rotation = Quaternion.Euler(0, 0, playerCharacter.GetComponent<CharacterController>().zRot);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.125f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, playerCharacter.GetComponent<CharacterController>().zRot), 0.25f);
            
        }
        else
        {
            //transform.rotation = Quaternion.Euler(0, 180, playerCharacter.GetComponent<CharacterController>().zRot);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, playerCharacter.GetComponent<CharacterController>().zRot), 0.25f);
        }
        //transform.rotation = Quaternion.Euler(0,0,playerCharacter.GetComponent<CharacterController>().zRot);
	}
}
