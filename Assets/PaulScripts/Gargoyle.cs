using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float frequency = 20f;
    [SerializeField]
    float magnitude = 0.5f;
    [SerializeField]
    GameObject leftBoundary;
    [SerializeField]
    GameObject rightBoundary;

    bool facingLeft = true;

    Vector3 position;
    Vector3 localScale;

    public int maxHealth = 3;
    public int healthRemaining = 3;
    public GameObject explosion;
    // Use this for initialization
    void Start()
    {
        position = transform.position;
        localScale = transform.localScale;
        if (GameObject.FindGameObjectsWithTag("LevelBound")[0].transform.position.x < GameObject.FindGameObjectsWithTag("LevelBound")[1].transform.position.x)
        {
            leftBoundary = GameObject.FindGameObjectsWithTag("LevelBound")[0];
            rightBoundary = GameObject.FindGameObjectsWithTag("LevelBound")[1];
        }
        else
        {
            leftBoundary = GameObject.FindGameObjectsWithTag("LevelBound")[1];
            rightBoundary = GameObject.FindGameObjectsWithTag("LevelBound")[0];
        }

    }

    // Update is called once per frame
    void Update()
    {
        CheckDirection();
        Move();
    }

    public void CheckDirection()
    {
        if (position.x < leftBoundary.transform.position.x)
        {
            facingLeft = false;
        }
        else if (position.x > rightBoundary.transform.position.x)
        {
            facingLeft = true;
        }

        if ((facingLeft && localScale.x > 0) || (!facingLeft && localScale.x < 0))
        {
            localScale.x *= -1; // Flip sprite
        }

        transform.localScale = localScale;
    }

    public void Move()
    {
        //Debug.Log(facingLeft);
        if (facingLeft)
        {
            position -= transform.right * Time.deltaTime * moveSpeed;
            transform.position = position + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        }
        else
        {
            position += transform.right * Time.deltaTime * moveSpeed;
            transform.position = position + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Arrow")
        {
            healthRemaining--;
        }
        if (healthRemaining == 0)
        {
            //Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, -9f), transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
