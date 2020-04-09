using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedGargoyleManager : MonoBehaviour
{

    public GameObject gargoyle;
    public GameObject[] arrayOfGargoyles;
    public int numOfGargoyles = 10;

    private GameObject gargoyleObj;
    public float spawnTime = 3;
    public float timer = 0;

    // Use this for initialization
    void Start()
    {
        arrayOfGargoyles = new GameObject[numOfGargoyles];
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            for (int i = 0; i < numOfGargoyles; i++)
            {
                if (arrayOfGargoyles[i] == null)
                {
                    gargoyleObj = Instantiate(gargoyle, transform.position, transform.rotation);
                    gargoyleObj.GetComponent<AdvancedGargoyle>().angle = 360* Mathf.Deg2Rad * (i + 1) / numOfGargoyles;
                    arrayOfGargoyles[i] = gargoyleObj;
                }
            }
            timer = 0;
        }
    }

    public void Restart()
    {
        foreach (GameObject g in arrayOfGargoyles)
        {
            if (g)
                Destroy(g.gameObject);
        }
        for (int i = 0; i < arrayOfGargoyles.Length; i++)
        {
            arrayOfGargoyles[i] = null;
        }
    }
}
