using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public RenderTexture viewTexture;
    public GameObject antView;
    public GameObject personView;

    public GameObject ant;
    public GameObject planet;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C)) {
            if (antView.tag == "MainCamera") {
                // Switch to person view
                antView.tag = "Untagged";
                personView.tag = "MainCamera";

                personView.SetActive(true);
                antView.GetComponent<Camera>().targetTexture = viewTexture;

                ant.transform.parent = planet.transform;
			} else {
                // Switch to ant view
                antView.tag = "MainCamera";
                personView.tag = "Untagged";

                personView.SetActive(false);
                antView.GetComponent<Camera>().targetTexture = null;

                ant.transform.parent = null;
            }
		}
	}
}
