﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraControl : MonoBehaviour {

  public RenderTexture viewTexture;
  public GameObject antView;
  public GameObject personView;

  public GameObject ant;
  public GameObject planet;

  void Start() {
    if (UnityEngine.VR.VRDevice.isPresent) {
      antView.SetActive(true);
      personView.SetActive(false);
    } else {
      antView.SetActive(false);
      personView.SetActive(false);
    }
  }

  // Update is called once per frame
  void Update() {
    if (UnityEngine.VR.VRDevice.isPresent) {
      
      if (Input.GetKeyDown(KeyCode.C)) {
        if (antView.tag == "MainCamera") {
          // Switch to person view
          antView.tag = "Untagged";
          personView.tag = "MainCamera";

          personView.SetActive(true);
          antView.GetComponent<Camera>().targetTexture = viewTexture;
        } else {
          // Switch to ant view
          antView.tag = "MainCamera";
          personView.tag = "Untagged";

          personView.SetActive(false);
          antView.GetComponent<Camera>().targetTexture = null;
        }
      }
    }
  }
}
