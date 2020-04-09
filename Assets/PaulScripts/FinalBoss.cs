using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour {

    public int maxHealth = 20;
    public int healthRemaining = 20;
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void Win()
    {
        SceneManager.LoadScene("WinScreen", LoadSceneMode.Additive);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Arrow")
        {
            Destroy(col.gameObject);
            healthRemaining--;
        }
        if (healthRemaining == 0)
        {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z - 1), transform.rotation);
            Win();
            Destroy(this.gameObject);
        }
    }
}
