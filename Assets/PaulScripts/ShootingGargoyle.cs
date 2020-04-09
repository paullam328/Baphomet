using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGargoyle : MonoBehaviour {

    public GameObject playerCharacter;
    public GameObject voidBall;
    public GameObject mouth;

    private GameObject voidBallObj;
    public float ballSpeed = 5;

    public float shootingDelay = 1;
    public float shootingTimer = 0;

    // Use this for initialization
    void Start () {
        playerCharacter = GameObject.FindGameObjectWithTag("Player");

    }
    
    // Update is called once per frame
    void Update() {

        if (shootingTimer >= shootingDelay)
        {
            float distX = playerCharacter.transform.position.x - mouth.transform.position.x;
            float distY = playerCharacter.transform.position.y - mouth.transform.position.y;
            float hypotenuse = Mathf.Sqrt(Mathf.Pow(distX,2)+ Mathf.Pow(distY, 2));
            

            voidBallObj = Instantiate(voidBall, mouth.transform.position, mouth.transform.rotation);
            voidBallObj.GetComponent<Rigidbody2D>().velocity = new Vector2(ballSpeed * distX/hypotenuse, ballSpeed * distY/hypotenuse);
            shootingTimer = 0;
        }

        shootingTimer += Time.deltaTime;
    }
}
