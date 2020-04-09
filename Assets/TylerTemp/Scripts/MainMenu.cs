using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //public RenderTexture playRenderTexture;
    //public RenderTexture controlsRenderTexture;
    //public RenderTexture creditsRenderTexture;
    //public RenderTexture exitRenderTexture;

    public GameObject playRenderTexture;
    public GameObject controlsRenderTexture;
    public GameObject creditsRenderTexture;
    public GameObject exitRenderTexture;

    //public Animator menuAnimator;

    private bool trigger = true;

    private int currentChannel; // 1 = Play, 2 = Controls, 3 = Credits, 4 = Story

    //Renderer rend;
    [SerializeField] Material currentMat;

    // Use this for initialization
    void Start () {
        //menuAnimator = GetComponent<Animator>();
        currentChannel = 1; // Start at Play

        //rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Vertical Axis: " + Input.GetAxis("Vertical"));
        //if (Input.GetAxis("Vertical") > 1)
        //      {

        //      }
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (trigger && (Mathf.Abs(horizontal) > 0.4f || Mathf.Abs(vertical) > 0.4f))
        {
            if (horizontal > 0.4 || vertical > 0.4)
            {
                //menuAnimator.SetFloat("Joystick Axis", Input.GetAxis("Vertical"));
                if (currentChannel < 4)
                {
                    currentChannel++;
                }
                else
                {
                    currentChannel = 1;
                }

                ChangeChannel(currentChannel);
                StartCoroutine(DelayButtonPress());
                //Debug.Log("Next channel selected: (" + Input.GetAxis("Horizontal") + ", " + Input.GetAxis("Vertical") + ")");
                //Debug.Log("Current Channel: " + currentChannel);
            }
            else if (horizontal < -0.4 || vertical < -0.4)
            {
                if (currentChannel > 1)
                {
                    currentChannel--;
                }
                else
                {
                    currentChannel = 4;
                }

                ChangeChannel(currentChannel);
                StartCoroutine(DelayButtonPress());
                Debug.Log("Previous channel selected: (" + Input.GetAxis("Horizontal") + ", " + Input.GetAxis("Vertical") + ")");
                Debug.Log("Current Channel: " + currentChannel);
            }
        }

        if (Input.GetButtonDown("Select"))
        {
            switch (currentChannel)
            {
                case 1: // PLay
                    // Start the game, move to game camera
                    Debug.Log("Play Selected");
                    SceneManager.LoadScene("BaphometsBecoming");
                    break;
                case 2: // Controls
                    // Zoom in or play controls
                    Debug.Log("Controls Selected");
                    break;
                case 3: // Credits
                    // Zoom in or play credits
                    Debug.Log("Credits Selected");
                    break;
                case 4: // Exit
                    // Game exit code
                    Debug.Log("Exit Selected");
                    Application.Quit();
                    break;
                default:
                    Debug.Log("No valid channel selected.");
                    break;
            }
        }
    }

    public IEnumerator DelayButtonPress()
    {
        trigger = false;
        yield return new WaitForSeconds(1f);
        trigger = true;
    }

    public void ChangeChannel(int channel)
    {
        switch (channel)
        {
            case 1: // PLay
                //currentMat.mainTexture = playRenderTexture;
                playRenderTexture.SetActive(true);
                controlsRenderTexture.SetActive(false);
                creditsRenderTexture.SetActive(false);
                exitRenderTexture.SetActive(false);
                Debug.Log("Play Render Texture Added");
                break;
            case 2: // Controls
                //currentMat.mainTexture = controlsRenderTexture;
                Debug.Log("Controls Render Texture Added");
                playRenderTexture.SetActive(false);
                controlsRenderTexture.SetActive(true);
                creditsRenderTexture.SetActive(false);
                exitRenderTexture.SetActive(false);
                break;
            case 3: // Credits
                //currentMat.mainTexture = creditsRenderTexture;
                Debug.Log("Credits Render Texture Added");
                playRenderTexture.SetActive(false);
                controlsRenderTexture.SetActive(false);
                creditsRenderTexture.SetActive(true);
                exitRenderTexture.SetActive(false);
                break;
            case 4: // Exit
                //currentMat.mainTexture = exitRenderTexture;
                Debug.Log("Exit Render Texture Added");
                playRenderTexture.SetActive(false);
                controlsRenderTexture.SetActive(false);
                creditsRenderTexture.SetActive(false);
                exitRenderTexture.SetActive(true);
                break;
            default:
                Debug.Log("No valid channel selected.");
                break;
        }
    }
}
