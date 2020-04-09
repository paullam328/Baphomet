using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth = 1;
    public int healthRemaining = 1;
    public GameObject enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (healthRemaining == 0)
        {
            Destroy(enemy.gameObject);
        }
	}
}
