using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVAnimationController : MonoBehaviour {

    public Animator tvAnimator;
    //Animator animator = GameObject.FindObjectsOfType<Animator>();

    void PlaySelected()
    {
        tvAnimator.speed = 0;
        Debug.Log("Play Selected!");
    }

    void ControlsSelected()
    {
        tvAnimator.speed = 0;
        Debug.Log("Controls Selected!");
    }
	
    void CreditsSelected()
    {
        tvAnimator.speed = 0;
        Debug.Log("Credits Selected!");
    }

    void ExitSelected()
    {
        tvAnimator.speed = 0;
        Debug.Log("Exit Selected!");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
