using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EvilVersion : MonoBehaviour {

    public GameObject playerCharacter;
    public GameObject arrow;
    public bool isJumping = false;
    public float shootingDelay = 0.5f;
    public float timer = 0;

    public float shootingAxisX = 0;
    public float shootingAxisY = 0;

    public int maxHealth = 10;
    public int healthRemaining = 10;

    private GameObject levelManager;
    public GameObject startingPoint;
    public bool bossEnabled = false;

    public GameObject explosion;

    // Use this for initialization
    void Start () {
        levelManager = GameObject.FindWithTag("LevelManager");
	}
	
	// Update is called once per frame
	void Update () {
        if (bossEnabled)
        {
            float evilWalkX = playerCharacter.GetComponent<CharacterController>().walkX;

            if (playerCharacter.GetComponent<CharacterController>().isFacingLeft)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward);
                evilWalkX *= -1;
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward);
            }
            transform.Translate(evilWalkX, 0, 0);

            if (Input.GetKeyDown("joystick 1 button 0") && !isJumping)
            {
                if (!isJumping)
                {
                    GetComponent<Rigidbody2D>().AddForce(playerCharacter.GetComponent<CharacterController>().jumpStrength * transform.up, ForceMode2D.Impulse);
                    isJumping = true;
                }
            }

            if ((Mathf.Round(Input.GetAxisRaw("FireX")) != 0 || Mathf.Round(Input.GetAxisRaw("FireY")) != 0) && Mathf.Round(Input.GetAxisRaw("Fire1")) < 0)
            {
                //Debug.Log("HI2");

                playerCharacter.GetComponent<CharacterController>().isShooting = true;
                if (playerCharacter.GetComponent<CharacterController>().shootCount < 2)
                {
                    shootingAxisX = -Input.GetAxisRaw("FireX");
                    shootingAxisY = Input.GetAxisRaw("FireY");
                    StartCoroutine("ExecuteShoot");
                }
            }
            else if (Mathf.Round(Input.GetAxisRaw("Fire1")) < 0)
            {
                // Debug.Log("HI3");

                if (playerCharacter.GetComponent<CharacterController>().shootCount < 2)
                {
                    shootingAxisX = 0;
                    shootingAxisY = 0;
                    StartCoroutine("ExecuteShoot");
                }
            }
        }
    }
    
    IEnumerator ExecuteShoot()
    {
        /*while (timer < shootingDelay)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        Shoot(this.gameObject, 0, 0);
        timer = 0;*/
        yield return new WaitForSeconds(2);
        Shoot(this.gameObject, 0, 0);
    }

    public void Shoot(GameObject shooter, float axisX, float axisY)
    {
        float zRot = 0;
        GameObject arrowObj;
        float shootingOffset;
        int magnitude;


            shootingOffset = -0.7f;
            magnitude = -1;

        if (axisX == 0 && axisY == 0)
        {
            //Shooting horizontally
            zRot = 0;
        }
        else if (axisX <= 0 && axisY >= 0)
        {
            //Shooting Left-Upwards
            zRot = 90 * axisY;
        }
        else if (axisX <= 0 && axisY <= 0)
        {
            //Shooting Left-Downwards
            zRot = -(90 * -axisY);
        }


        arrowObj = Instantiate(arrow, new Vector3(shooter.transform.position.x + shootingOffset, shooter.transform.position.y, shooter.transform.position.z),
              Quaternion.Euler(0, 0, magnitude * zRot));

        arrowObj.GetComponent<Arrow>().isEnemyProjectile = true;
        arrowObj.GetComponent<Rigidbody2D>().velocity = new Vector2(magnitude * playerCharacter.GetComponent<CharacterController>().angularForce * ((90 - Mathf.Abs(zRot)) / 90) / (playerCharacter.GetComponent<CharacterController>().arrowSlowdownFactor)
            , zRot / (playerCharacter.GetComponent<CharacterController>().arrowSlowdownFactor));
            
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") // GameObject is a type, gameObject is the property
        {
            isJumping = false;
        }
    }


    //If the character dies:
    public void Restart()
    {
        transform.position = startingPoint.transform.position;
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

            levelManager.GetComponent<LevelManager>().evilVersionDead = true;
            levelManager.GetComponent<LevelManager>().currentLevel = 4;
            Destroy(this.gameObject);
        }
    }

}
