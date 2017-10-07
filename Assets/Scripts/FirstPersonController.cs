using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

    public float mouseSensitivityX = 3.5f;
    public float mouseSensitivityY = 3.5f;
    public float walkSpeed = 4;

    Transform cameraT;
    float verticalLookRotation;

    Vector3 moveAmount;
    Vector3 smoothVelocity;
    Rigidbody myBody;

	void Start () {
        cameraT = Camera.main.transform;
        myBody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        if (Camera.main.name == "AntCamera") {
			transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);

			verticalLookRotation += Input.GetAxis(("Mouse Y")) * mouseSensitivityY;
			verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 90);
			cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

			Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
			Vector3 targetMoveAmount = moveDir * walkSpeed;
			moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothVelocity, .15f);
        }
	}

    private void FixedUpdate() {
        if (Camera.main.name == "AntCamera") {
            myBody.MovePosition(myBody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        }
    }
}
