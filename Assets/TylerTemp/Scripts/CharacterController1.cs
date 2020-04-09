using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController1 : MonoBehaviour {

    public float speed = 10;
    public float jumpingSpeed = 4;
    public float jumpStrength = 10;
    private bool isJumping = false;
    private bool levelBoundHit = false;

    private bool isFacingLeft = false;
    
    public GameObject arrow;
    public float angularForce = 90;
    public float arrowSlowdownFactor = 5;

    public int shootCount = 0;
    public bool isShooting = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float walkX = 0;
        if (!levelBoundHit)
        {
            if (isJumping)
            {
                walkX = -1 * Mathf.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * jumpingSpeed;
            }
            else
            {
                walkX = -1 * Mathf.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * speed;
            }
        }
        transform.Translate(walkX, 0, 0);

        if (Input.GetKeyDown("joystick 1 button 0") && !isJumping)
            {  //makes player jump
            Jump();
            isJumping = true;
        }
        
        if ((Mathf.Round(Input.GetAxisRaw("FireX")) != 0 || Mathf.Round(Input.GetAxisRaw("FireY")) != 0) && Mathf.Round(Input.GetAxisRaw("Fire1")) < 0)
        {
            isShooting = true;
            if (shootCount < 1)
                Shoot(Input.GetAxisRaw("FireX"), Input.GetAxisRaw("FireY"));
            shootCount++;
        }
        else if (Mathf.Round(Input.GetAxisRaw("Fire1")) < 0)
        {
            isShooting = true;
            if (shootCount < 1)
                Shoot(0, 0);
            shootCount++;
        }

        //Rotates the player depending on joystick
        if (Input.GetAxis("Horizontal") < 0)
        {
            isFacingLeft = true;
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            isFacingLeft = false;
            transform.rotation = Quaternion.LookRotation(Vector3.back);
        }

        //Initializing level boundary hit bool:
        levelBoundHit = false;
        if (!isShooting)
        {
            shootCount = 0;
        }
        isShooting = false;
    }


    void Shoot(float axisX, float axisY)
    {
        float zRot = 0;
        GameObject arrowObj;
        float shootingOffset;
        int magnitude;



        if (isFacingLeft)
        {
            shootingOffset = -0.7f;
            magnitude = -1;

            if (axisX == 0 && axisY == 0)
            {
                //Shooting horizontally
                zRot = 0;
            }
            else if (axisX <= 0 && axisY >= 0)
            {
                //Shooting Left-Upwards
                zRot = 90 * axisY;
            }
            else if (axisX <= 0 && axisY <= 0)
            {
                //Shooting Left-Downwards
                zRot = -(90 * -axisY);
            }
        }
        else
        {
            shootingOffset = 0.7f;
            magnitude = 1;


            if (axisX == 0 && axisY == 0)
            {
                //Shooting horizontally
                zRot = 0;
            }
            else if (axisX >= 0 && axisY >= 0)
            {
                //Shooting Right-Upwards
                zRot = 90 * axisY;
            }
            else if (axisX >= 0 && axisY <= 0)
            {
                //Shooting Right-Downwards
                zRot = -(90 * -axisY);
            }
        }
        

        arrowObj = Instantiate(arrow, new Vector3(transform.position.x + shootingOffset, transform.position.y, transform.position.z),
              Quaternion.Euler(0, 0, magnitude * zRot));

        arrowObj.GetComponent<Rigidbody2D>().velocity = new Vector2(magnitude * angularForce * ((90 - Mathf.Abs(zRot)) / 90) / arrowSlowdownFactor
            , zRot / arrowSlowdownFactor);
    }

    void Jump()
    {
       // if (CanJump())
      //  {
            // Jump on ridigbody
            GetComponent<Rigidbody2D>().AddForce(jumpStrength * transform.up, ForceMode2D.Impulse);
       // }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") // GameObject is a type, gameObject is the property
        {
            isJumping = false;
        }
        if (col.gameObject.tag == "LevelBounds")
        {
            levelBoundHit = true; 
        }
    }

    /*bool CanJump()
    {
        // Create Ray
        Ray ray = new Ray(transform.position, transform.up * -1);

        // Create Hit Info
        RaycastHit2D hit = Physics2D.Raycast(transform.localScale.y + 0.2f);

        if (Physics2D.Raycast(ray, out hit, transform.localScale.y + 0.2f))
        {
            
            return true;
        }

        // Nothing so return false
        return false;
    }*/
}
