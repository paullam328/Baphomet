using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GammaCorrection : MonoBehaviour {

    PostProcessVolume volume;
    ColorGrading colorGradingLayer;

    // Use this for initialization
    void Start () {
        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorGradingLayer);
    }
	
	// Update is called once per frame
	void Update () {
        //colorGradingLayer.saturation.value = saturation;
        //colorGradingLayer.gamma.value = 1000f;
        // Add code for adjusting gamma
    }
}
