using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {

  public GameObject laserPrefab;
  public GameObject teleportReticlePrefab;
  public Transform cameraRigTransform;
  public Transform headTransform;
  public LayerMask teleportMask;
  public GameObject ball;

  private SteamVR_TrackedObject trackedObj;
  private GameObject laser;
  private GameObject reticle;
  private Transform laserTransform;
  private Vector3 hitPoint;
  private Transform teleportReticleTransform;
  private bool shouldTeleport;

  private SteamVR_Controller.Device Controller {
    get { return SteamVR_Controller.Input((int)trackedObj.index); }
  }

  void Awake() {
    trackedObj = GetComponent<SteamVR_TrackedObject>();
  }

  void Start() {
    laser = Instantiate(laserPrefab);
    laserTransform = laser.transform;
    reticle = Instantiate(teleportReticlePrefab);
    teleportReticleTransform = reticle.transform;
  }

  void Update() {
    if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
      RaycastHit hit;

      if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask)) {
        hitPoint = hit.point;
        ShowLaser(hit);

        reticle.SetActive(true);
        teleportReticleTransform.position = hitPoint;
        teleportReticleTransform.forward = (teleportReticleTransform.position - ball.transform.position).normalized;
        shouldTeleport = true;
      }
    } else {
      laser.SetActive(false);
      reticle.SetActive(false);
    }

    if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport) {
      Teleport();
    }
  }

  private void ShowLaser(RaycastHit hit) {
    laser.SetActive(true);
    laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
    laserTransform.LookAt(hitPoint);
    laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
        hit.distance);
  }

  private void Teleport() {
    shouldTeleport = false;
    reticle.SetActive(false);
    Vector3 difference = cameraRigTransform.position - headTransform.position;
    difference.y = 0;
    cameraRigTransform.position = (hitPoint + difference).normalized * 25f;

    cameraRigTransform.up = (cameraRigTransform.position - ball.transform.position).normalized;
  }
}
