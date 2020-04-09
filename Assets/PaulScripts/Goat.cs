using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goat : MonoBehaviour {

    public GameObject startingPoint;
    public GameObject[] verticalPatrolPoints;
    public GameObject currentVerticalPatrolPoint;
    public int verticalDestPoint = 0;
    public int direction = 0;

    public GameObject ray;
    private GameObject rayObject;
    public GameObject mouth;

    public float shootReadyTime = 3;
    public float shootingTime = 2;
    public float timer = 0;

    public int raySpawnCounter = 0;

    public GameObject bossDetector;
    public bool bossDetected = false;
    public GameObject playerCharacter;

    public int maxHealth = 10;
    public int healthRemaining = 10;

    private GameObject levelManager;

    public GameObject explosion;

    // Use this for initialization
    void Start () {
        //going down at first
        transform.position = startingPoint.transform.position;
        currentVerticalPatrolPoint = verticalPatrolPoints[0];
        direction = 1;
        levelManager = GameObject.FindWithTag("LevelManager");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (playerCharacter)
        {
            if (playerCharacter.GetComponent<CharacterController>().canFightGoat)
            {
                timer += Time.fixedDeltaTime;

                if (timer >= shootReadyTime + shootingTime)
                {
                    Destroy(rayObject.gameObject);
                    timer = 0;
                    raySpawnCounter = 0;
                }
                if (timer >= shootReadyTime && timer < shootReadyTime + shootingTime)
                {
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    if (raySpawnCounter < 1)
                    {
                        SprayRay();
                    }
                    raySpawnCounter++;
                }

                if (timer < shootReadyTime)
                {
                    if (Vector2.Distance(transform.position, currentVerticalPatrolPoint.transform.position) < 0.6f)
                    {
                        if (verticalDestPoint == 0)
                        {
                            verticalDestPoint = 1;
                            currentVerticalPatrolPoint = verticalPatrolPoints[verticalDestPoint];
                            direction = -1;
                        }
                        else
                        {
                            verticalDestPoint = 0;
                            currentVerticalPatrolPoint = verticalPatrolPoints[verticalDestPoint];
                            direction = 1;
                        }
                    }
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, direction * 2);
                }
            }
        }
    }

    public void SprayRay()
    {
        rayObject = Instantiate(ray, new Vector3(ray.transform.position.x, mouth.transform.position.y, ray.transform.position.z),transform.rotation);
    }

    //If the character dies:
    public void Restart()
    {
        transform.position = startingPoint.transform.position;
        currentVerticalPatrolPoint = verticalPatrolPoints[0];
        direction = 1;
        playerCharacter.GetComponent<CharacterController>().canFightGoat = false;
        if (rayObject)
        {
            Destroy(rayObject.gameObject);
        }
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

            levelManager.GetComponent<LevelManager>().goatDead = true;
            Destroy(this.gameObject);
            if (rayObject)
            {
                Destroy(rayObject.gameObject);
            }
            levelManager.GetComponent<LevelManager>().currentLevel = 2;
        }
    }
}
