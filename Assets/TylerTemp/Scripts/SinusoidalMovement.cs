using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMovement : MonoBehaviour {

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float frequency = 20f;
    [SerializeField] float magnitude = 0.5f;
    [SerializeField] GameObject leftBoundary;
    [SerializeField] GameObject rightBoundary;

    bool facingLeft = true;

    Vector3 position;
    Vector3 localScale;

    public int maxHealth = 3;
    public int healthRemaining = 3;

    // Use this for initialization
    void Start () {
        position = transform.position;
        localScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
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
            Destroy(this.gameObject);
        }
    }
}
