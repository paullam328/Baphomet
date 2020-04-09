using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraShake : MonoBehaviour
{

    private Animator myAnim;

    PostProcessVolume ppv;
    ColorGrading cg;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetBrightnessSlider (float brightness)
    {
        cg.brightness.value = brightness;
        //cg.gamma.value = ;
    }
    public void SmallShake()
    {
        myAnim.SetTrigger("SmallShake");
    }
    public void MediumShake()
    {
        myAnim.SetTrigger("MediumShake");
    }
    public void LargeShake()
    {
        myAnim.SetTrigger("LargeShake");
    }
}
