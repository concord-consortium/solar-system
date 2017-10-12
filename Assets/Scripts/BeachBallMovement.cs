using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBallMovement : MonoBehaviour {

  public GameObject beachBall;
  public GameObject personView;

  private SteamVR_TrackedObject trackedObj;
  private SteamVR_Controller.Device Controller {
    get { return SteamVR_Controller.Input((int)trackedObj.index); }
  }

  void Awake() {
    trackedObj = GetComponent<SteamVR_TrackedObject>();
  }
	
	// Update is called once per frame
	void Update() {
    if (personView.tag == "MainCamera") {
      beachBall.transform.rotation = trackedObj.transform.rotation;
      beachBall.transform.Rotate(90, 0, 0);
    }
	}
}
