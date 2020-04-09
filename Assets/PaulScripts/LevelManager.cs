using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [Header("Setup level bounds")]
    public GameObject LevelBoundary;
    public float level1BoundX;
    public float level2BoundX;
    public float level3BoundX;
    public float level4BoundX;

    [Header("Setup camera bound positions")]
    public GameObject CameraBoundary;
    public float level1CameraBoundX;
    public float level2CameraBoundX;
    public float level3CameraBoundX;
    public float level4CameraBoundX;

    [Header("Setup camera bound scales")]
    public float level1CameraScaleX;
    public float level2CameraScaleX;
    public float level3CameraScaleX;
    public float level4CameraScaleX;

    [Header("SpawnPoints")]
    public GameObject spawnPoint;
    public float level1SpawnPointX;
    public float level2SpawnPointX;
    public float level3SpawnPointX;
    public float level4SpawnPointX;
    public float level5SpawnPointX;

    [Header("Current Level")]
    public int currentLevel = 1;

    [Header("Bosses")]
    public GameObject goat;
    public GameObject baby;
    public GameObject evilVersion;

    [Header("Barriers")]
    public GameObject level2Barrier;
    public GameObject level3Barrier;
    public GameObject level4Barrier;

    public bool goatDead = false;
    public bool babyDead = false;
    public bool evilVersionDead = false;


    // Use this for initialization
    void Start () {
        level2Barrier = GameObject.FindGameObjectWithTag("Level2Barrier");
        level3Barrier = GameObject.FindGameObjectWithTag("Level3Barrier");
        level4Barrier = GameObject.FindGameObjectWithTag("Level4Barrier");

    }
	
	// Update is called once per frame
	void Update () {
        switch (currentLevel)
        {
            case 1:
                LevelBoundary.transform.position = new Vector3(level1BoundX, LevelBoundary.transform.position.y, LevelBoundary.transform.position.z);
                CameraBoundary.transform.position = new Vector3(level1CameraBoundX, CameraBoundary.transform.position.y, CameraBoundary.transform.position.z);
                CameraBoundary.transform.localScale = new Vector3(level1CameraScaleX, CameraBoundary.transform.localScale.y, CameraBoundary.transform.localScale.z);
                spawnPoint.transform.position = new Vector3(level1SpawnPointX, spawnPoint.transform.position.y, spawnPoint.transform.position.z);

                break;
            case 2:
                LevelBoundary.transform.position = new Vector3(level2BoundX, LevelBoundary.transform.position.y, LevelBoundary.transform.position.z);
                CameraBoundary.transform.position = new Vector3(level2CameraBoundX, CameraBoundary.transform.position.y, CameraBoundary.transform.position.z);
                CameraBoundary.transform.localScale = new Vector3(level2CameraScaleX, CameraBoundary.transform.localScale.y, CameraBoundary.transform.localScale.z);
                spawnPoint.transform.position = new Vector3(level2SpawnPointX, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
                break;
            case 3:
                LevelBoundary.transform.position = new Vector3(level3BoundX, LevelBoundary.transform.position.y, LevelBoundary.transform.position.z);
                CameraBoundary.transform.position = new Vector3(level3CameraBoundX, CameraBoundary.transform.position.y, CameraBoundary.transform.position.z);
                CameraBoundary.transform.localScale = new Vector3(level3CameraScaleX, CameraBoundary.transform.localScale.y, CameraBoundary.transform.localScale.z);
                spawnPoint.transform.position = new Vector3(level3SpawnPointX, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
                break;
            case 4:
                LevelBoundary.transform.position = new Vector3(level4BoundX, LevelBoundary.transform.position.y, LevelBoundary.transform.position.z);
                CameraBoundary.transform.position = new Vector3(level4CameraBoundX, CameraBoundary.transform.position.y, CameraBoundary.transform.position.z);
                CameraBoundary.transform.localScale = new Vector3(level4CameraScaleX, CameraBoundary.transform.localScale.y, CameraBoundary.transform.localScale.z);
                spawnPoint.transform.position = new Vector3(level4SpawnPointX, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
                break;
            case 5:
                spawnPoint.transform.position = new Vector3(level5SpawnPointX, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
                break;
        }

        if (currentLevel == 2)
        {
            baby.GetComponent<Baby>().bossEnabled = true;
        }
        if (currentLevel == 3)
        {
            evilVersion.GetComponent<EvilVersion>().bossEnabled = true;
        }

        ActivateLevelBarrier();
    }

    public void ActivateLevelBarrier()
    {
        if (goatDead)
        {
            level2Barrier.SetActive(false);
        }

        if (babyDead)
        {
            level3Barrier.SetActive(false);
        }

        if (evilVersionDead)
        {
            level4Barrier.SetActive(false);
        }
    }
}