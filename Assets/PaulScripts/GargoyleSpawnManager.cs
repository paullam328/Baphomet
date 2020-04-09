using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleSpawnManager : MonoBehaviour {

    public GameObject gargoyle;
    public List<GameObject> listOfGargoyles;
    public GameObject[] arrayOfSpawnPoints;

    private GameObject gargoyleObj;
    public float spawnTime = 3;
    public float timer = 0;

	// Use this for initialization
	void Start () {
        arrayOfSpawnPoints = GameObject.FindGameObjectsWithTag("GargoyleSpawner");
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            gargoyleObj = Instantiate(gargoyle, arrayOfSpawnPoints[Random.Range(0,1)].transform.position, transform.rotation);
            listOfGargoyles.Add(gargoyleObj);
            timer = 0;
        }
	}

    public void Restart()
    {
        foreach (GameObject g in listOfGargoyles)
        {
            if (g)
                Destroy(g.gameObject);
        }
        listOfGargoyles.Clear();
    }
}
