using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private CameraShake theCam;
    public float speed = 10;
    public float jumpingSpeed = 4;
    public float jumpStrength = 10;
    private bool levelBoundHit = false;

    public GameObject shootingPoint;
    public GameObject arrow;
    public float angularForce = 90;
    public float arrowSlowdownFactor = 5;

    public int shootCount = 0;
    public bool isShooting = false;


    //For evil version:
    public float walkX = 0;
    public bool isFacingLeft = false;
    public bool isJumping = false;
    public bool canFightGoat = false;

    public float maxHealth = 10;
    public float healthRemaining = 10;
    public float ratio;

    private GameObject levelManager;
    private GameObject spawnPoint;

    public GameObject goat;
    public GameObject baby;
    public GameObject evilVersion;
    public GameObject gargoyleSpawnManager;
    public GameObject advancedGargoyleManager;

    public GameObject cameraMain;
    public Animator plainAnim;
    public bool isDead = false;

    public float zRot;
    public Color ogcolor;
    public Vector2 beginningTrans;

    // Use this for initialization
    void Start()
    {
        levelManager = GameObject.FindWithTag("LevelManager");
        spawnPoint = GameObject.FindWithTag("SpawnPoint");
        gargoyleSpawnManager = GameObject.FindWithTag("GargoyleSpawnManager");
        //theCam = GameObject.Find("CameraBox").GetComponent<CameraShake>();
        plainAnim = GetComponent<Animator>();

        ogcolor = GetComponent<SpriteRenderer>().color;
        beginningTrans = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<SpriteRenderer>().color = ogcolor;
        transform.localScale = beginningTrans;
        if (!isDead)
        {
            ratio = healthRemaining / maxHealth;
            walkX = 0;
            if (!levelBoundHit)
            {
                if (isJumping)
                {
                    //plainAnim.SetBool("IsJumping", true);
                    walkX = -1 * Mathf.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * jumpingSpeed;
                }
                else
                {
                    // plainAnim.SetBool("IsJumping", false);
                    walkX = -1 * Mathf.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * speed;
                }
            }
            transform.Translate(walkX, 0, 0);
                plainAnim.speed = Mathf.Abs(Input.GetAxis("Horizontal"));

            if (Input.GetKeyDown("joystick 1 button 0") && !isJumping)
            {  //makes player jump
                Jump();
                isJumping = true;
            }
            if (Mathf.Round(Input.GetAxisRaw("FireX")) != 0 || Mathf.Round(Input.GetAxisRaw("FireY")) != 0)
            {
                isShooting = false;
                if (shootCount < 1)
                    RotateArmAndShoot(this.gameObject, Input.GetAxisRaw("FireX"), Input.GetAxisRaw("FireY"), isShooting);
            }

            if ((Mathf.Round(Input.GetAxisRaw("FireX")) != 0 || Mathf.Round(Input.GetAxisRaw("FireY")) != 0) && Mathf.Round(Input.GetAxisRaw("Fire1")) < 0)
            {
                GetComponent<SpriteRenderer>().color = ogcolor;
                isShooting = true;
                if (shootCount < 1)
                    RotateArmAndShoot(this.gameObject, Input.GetAxisRaw("FireX"), Input.GetAxisRaw("FireY"), isShooting);
                shootCount++;
            }
            else if (Mathf.Round(Input.GetAxisRaw("Fire1")) < 0)
            {
                GetComponent<SpriteRenderer>().color = ogcolor;
                isShooting = true;
                if (shootCount < 1)
                    RotateArmAndShoot(this.gameObject, 0, 0, isShooting);
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


        //For Evil Version:
        /*if (!isShooting && !isJumping && walkX == 0)
        {
            isIdle = true;
        }
        else
        {
            isIdle = false;
        }*/
    }


    public void RotateArmAndShoot(GameObject shooter, float axisX, float axisY, bool shoot)
    {
        zRot = 0;
        GameObject arrowObj = null;
        float shootingOffset;
        int magnitude;



        if (isFacingLeft)
        {
            shootingOffset = -0.7f;
            magnitude = -1;
            arrow.GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<SpriteRenderer>().flipX = true;

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
            arrow.GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<SpriteRenderer>().flipX = true;

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


        if (shoot) {
            Shoot(arrowObj, magnitude);
        }
    }

    void Shoot(GameObject arrowObj, int magnitude)
    {
        arrowObj = Instantiate(arrow, shootingPoint.transform.position,
      Quaternion.Euler(0, 0, magnitude * zRot));

        arrowObj.GetComponent<Rigidbody2D>().velocity = new Vector2(magnitude * angularForce * ((90 - Mathf.Abs(zRot)) / 90) / arrowSlowdownFactor
            , zRot / arrowSlowdownFactor);
        AkSoundEngine.PostEvent("Player_Fire", this.gameObject);
    }

    void Jump()
    {
       // if (CanJump())
      //  {
            // Jump on ridigbody
            GetComponent<Rigidbody2D>().AddForce(jumpStrength * transform.up, ForceMode2D.Impulse);
            AkSoundEngine.PostEvent("Player_Jump", this.gameObject);
       // }
    }

    //RESTART COMPLETELY IMPLEMENTED HERE:
    void Restart()
    {
        //TODO:Implement this
        healthRemaining = maxHealth;
        gargoyleSpawnManager.GetComponent<GargoyleSpawnManager>().Restart();
        switch(levelManager.GetComponent<LevelManager>().currentLevel)
        {
            case 1:
                goat.GetComponent<Goat>().Restart();
                transform.position = spawnPoint.transform.position;
                break;
            case 2:
                baby.GetComponent<Baby>().Restart();
                transform.position = spawnPoint.transform.position;
                break;
            case 3:
                evilVersion.GetComponent<EvilVersion>().Restart();
                transform.position = spawnPoint.transform.position;
                break;
            case 4:
                if (advancedGargoyleManager)
                {
                    advancedGargoyleManager.GetComponent<AdvancedGargoyleManager>().Restart();
                    transform.position = spawnPoint.transform.position;
                }
                break;
            case 5:
                break;
        }
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
    IEnumerator RestartProcedure()
    {
        /*while (timer < shootingDelay)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        Shoot(this.gameObject, 0, 0);
        timer = 0;*/
        Color OGcolor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;
        isDead = true;

        yield return new WaitForSeconds(2);

        GetComponent<SpriteRenderer>().color = OGcolor;
        Restart();
        isDead = false;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BossDetector")
        {
            canFightGoat = true;
        }
        if (col.gameObject.tag == "EnemyProjectile")
        {
            healthRemaining--;
            // cameraMain.GetComponent<CameraShake>().SmallShake();
            //Debug.Log("Collided with enemy");
            //theCam.SmallShake();
        }
        if (col.gameObject.tag == "Ray")
        {
            healthRemaining -= 4;
            // cameraMain.GetComponent<CameraShake>().SmallShake();
            // cameraMain.GetComponent<CameraShake>().SmallShake();
            //theCam.SmallShake();
        }
        if (healthRemaining <= 0)
        {
            //Restart();
            //plainAnim.SetBool("IsDead", true);
            
            StartCoroutine("RestartProcedure");

           
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
