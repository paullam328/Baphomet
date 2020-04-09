using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    //projectile motion:
    public float timeFloat;
    public float velocityX;
    public float velocityY;
    public bool isProjectile = false;

    //baby
    public GameObject baby;
    public bool isLeftEye1 = false;
    public bool isLeftEye2 = false;
    public bool isRightEye1 = false;
    public bool isRightEye2 = false;

    public bool isEnemyProjectile = false;
    public GameObject splatterGround;
    public GameObject splatterPlayer;
    public bool isVoidBall = false;

    public GameObject arrowExplosion;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("LevelBound")
            || col.gameObject.tag.Equals("Ground"))
        {
            if (isVoidBall && col.gameObject.tag.Equals("Ground"))
                Instantiate(splatterGround, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z - 1), transform.rotation);
            Destroy(gameObject);
        }
        if (col.gameObject.tag.Equals("Player") && isEnemyProjectile)
        {
            if (isVoidBall)
                Instantiate(splatterPlayer, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), transform.rotation);
            Destroy(gameObject);
        }
        if (col.gameObject.tag.Equals("Enemy") && !isEnemyProjectile)
        {
            Instantiate(arrowExplosion, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z - 1), transform.rotation);
            Destroy(gameObject);
        }
    }
}
