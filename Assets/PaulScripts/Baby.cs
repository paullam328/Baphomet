using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour {

    public GameObject leftEye;
    public GameObject rightEye;
    public GameObject voidBall; //tears

    private GameObject voidBallObj;
    public float ballSpeedX = 0.1f;
    public float ballSpeedY = 10;

    public float testCount = 0;

    public List<GameObject> arrayOfVoidBallObj = new List<GameObject>();

    public int shootingTime = 2;
    public int coolDownTime = 3;
    public float timer = 0;
    public bool coolDown = false;

    public float maxBetweenShotsTime = 0.5000f;
    public float betweenShotsTimer = 0.000f;

    public int maxHealth = 10;
    public int healthRemaining = 10;

    public GameObject startingPoint;
    private GameObject levelManager;

    public bool bossEnabled = false;

    public GameObject explosion;


    // Use this for initialization
    void Start () {
        levelManager = GameObject.FindWithTag("LevelManager");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (bossEnabled)
        {
            if (timer < shootingTime && !coolDown)
            {
                if (betweenShotsTimer == 0)
                {
                    //Left eye 2
                    voidBallObj = Instantiate(voidBall, leftEye.transform.position, leftEye.transform.rotation);
                    voidBallObj.GetComponent<Arrow>().isLeftEye1 = true;
                    voidBallObj.GetComponent<Arrow>().isProjectile = true;
                    arrayOfVoidBallObj.Add(voidBallObj);

                    //Right eye 1
                    voidBallObj = Instantiate(voidBall, rightEye.transform.position, rightEye.transform.rotation);
                    voidBallObj.GetComponent<Arrow>().isRightEye1 = true;
                    voidBallObj.GetComponent<Arrow>().isProjectile = true; //for destroying the projectile-motion object when it collides w/ the ground/player
                    arrayOfVoidBallObj.Add(voidBallObj);

                    //Left eye 1
                    voidBallObj = Instantiate(voidBall, leftEye.transform.position, leftEye.transform.rotation);
                    voidBallObj.GetComponent<Arrow>().isLeftEye2 = true;
                    voidBallObj.GetComponent<Arrow>().isProjectile = true;
                    arrayOfVoidBallObj.Add(voidBallObj);

                    //Right eye 2
                    voidBallObj = Instantiate(voidBall, rightEye.transform.position, rightEye.transform.rotation);
                    voidBallObj.GetComponent<Arrow>().isRightEye2 = true;
                    voidBallObj.GetComponent<Arrow>().isProjectile = true; //for destroying the projectile-motion object when it collides w/ the ground/player
                    arrayOfVoidBallObj.Add(voidBallObj);

                    betweenShotsTimer += Time.fixedDeltaTime;
                }
                else
                {
                    betweenShotsTimer += Time.fixedDeltaTime;
                    if (betweenShotsTimer > maxBetweenShotsTime)
                    {
                        betweenShotsTimer = 0;
                    }
                }


                timer += Time.fixedDeltaTime;
            }
            else
            {
                coolDown = true;
                timer += Time.fixedDeltaTime;
                if (timer >= coolDownTime + shootingTime)
                {
                    coolDown = false;
                    timer = 0;
                }
            }

            for (int i = 0; i < arrayOfVoidBallObj.Count; i++)
            {
                if (arrayOfVoidBallObj[i])
                {
                    int magnitude = 0;
                    float angle = 0;
                    if (arrayOfVoidBallObj[i].GetComponent<Arrow>().isLeftEye1)
                    {
                        magnitude = -1;
                        angle = 0.4f; //Mathf.Cos(angle) doesn't work as intended
                    }
                    if (arrayOfVoidBallObj[i].GetComponent<Arrow>().isRightEye1)
                    {
                        magnitude = 1;
                        angle = 0.4f;
                    }
                    if (arrayOfVoidBallObj[i].GetComponent<Arrow>().isLeftEye2)
                    {
                        magnitude = -1;
                        angle = 0.5f;
                    }
                    if (arrayOfVoidBallObj[i].GetComponent<Arrow>().isRightEye2)
                    {
                        magnitude = 1;
                        angle = 0.5f;
                    }
                    arrayOfVoidBallObj[i].GetComponent<Arrow>().velocityX = magnitude * ballSpeedX * angle;
                    arrayOfVoidBallObj[i].GetComponent<Arrow>().velocityY = ballSpeedY * angle - Physics2D.gravity.magnitude * arrayOfVoidBallObj[i].GetComponent<Arrow>().timeFloat;

                    arrayOfVoidBallObj[i].GetComponent<Rigidbody2D>().velocity = new Vector2(arrayOfVoidBallObj[i].GetComponent<Arrow>().velocityX, arrayOfVoidBallObj[i].GetComponent<Arrow>().velocityY);
                    arrayOfVoidBallObj[i].GetComponent<Arrow>().timeFloat += Time.fixedDeltaTime;
                }
                else
                {
                    arrayOfVoidBallObj.Remove(arrayOfVoidBallObj[i]);
                }
            }
        }
    }

    //If the character dies:
    public void Restart()
    {
        transform.position = startingPoint.transform.position;
        arrayOfVoidBallObj.Clear();
        healthRemaining = maxHealth;
    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Arrow")
        {
            healthRemaining--;
        }
        if (healthRemaining == 0)
        {
            GameObject expObj = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z - 1), transform.rotation);
            Destroy(expObj, 5.14f);

            levelManager.GetComponent<LevelManager>().babyDead = true;
            Destroy(this.gameObject);
            levelManager.GetComponent<LevelManager>().currentLevel = 3;
        }
    }
}
