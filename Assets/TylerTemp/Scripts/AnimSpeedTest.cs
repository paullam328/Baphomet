using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeedTest : MonoBehaviour {

    //public Animation animation;
    public Animator animator;
    private Animator goatAnimator;
    //private GameObject player;

    // Use this for initialization
    void Start () {
        //anim.GetComponent<Animation>();
        //player.GetComponent<GameObject>();
        goatAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //player.animation["Player Run (No Health)"].speed = 0;
        //animation["Player Movement"].speed = 0f;
        animator.speed = Mathf.Abs(Input.GetAxis("Horizontal"));
        goatAnimator.SetBool("isDead", true);
    }
}
