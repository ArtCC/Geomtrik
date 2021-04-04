using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float velocity;

    private Rigidbody2D rbody;
    private float degreesPerSec = 360f;

    // Start is called before the first frame update
    void Start() {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            rbody.velocity = Vector2.up * velocity;
            float rotAmount = degreesPerSec * Time.deltaTime;
            float curRot = transform.localRotation.eulerAngles.z;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot+rotAmount));
        }
    }
}