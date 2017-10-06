using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {

    GravityAttractor planet;
    Rigidbody myBody;

    void Awake() {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();

        myBody = GetComponent<Rigidbody>();
        myBody.useGravity = false;
        myBody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate() {
        planet.Attract(transform);
    }
}
