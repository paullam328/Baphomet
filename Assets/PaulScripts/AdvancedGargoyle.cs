using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedGargoyle : MonoBehaviour
{

    public int maxHealth = 3;
    public int healthRemaining = 3;

    public float RotateSpeed = 5f;
    public float Radius = 0.1f;

    public GameObject center;
    public float angle;

    public GameObject explosion;

    private void Start()
    {
    }

    private void Update()
    {

        angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * Radius;
        transform.position = center.transform.position + offset + new Vector3(-10, 11, -3);

        //Debug.Log(center.transform.position);
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
