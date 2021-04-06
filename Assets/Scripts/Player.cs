using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float velocity;

    private Rigidbody2D rigid;
    private float degreesPerSec = 360f;
    private float horizontal;
    private Vector2 lastTouchPosition;
    private bool onPosition = false;
    private bool onPlayerWin = false;

    // Start is called before the first frame update
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            rigid.velocity = Vector2.up * velocity;
            float rotAmount = degreesPerSec * Time.deltaTime;
            float curRot = transform.localRotation.eulerAngles.z;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot+rotAmount));
        }
        lateralMovement();
        playerWin();
    }

    // Private functions
    private void lateralMovement() {
        // With tap screen in mobile
        if (Input.touchCount > 0) {
            lastTouchPosition = Input.touches[0].position;
            float width = Screen.width;                
            if (lastTouchPosition.x < (width / 2 + 50)) {
                horizontal = -1.0f;
            } else {
                horizontal = 1.0f;
            }
            rigid.velocity = new Vector2(horizontal * 2, rigid.velocity.y);
        }
    }

    private void playerWin() {
        if (onPosition == true && onPlayerWin == true && rigid.velocity.x == 0 && rigid.velocity.y == 0) {
            onPlayerWin = true;
            Debug.Log("You win!!!");
        }
    }

    private void reloadGame() {
        onPosition = false;
        onPlayerWin = false;
    }

    private void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.tag == "Obstacle") {
            onPosition = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collider) {
        if (collider.gameObject.tag == "Obstacle") {
            onPosition = false;
        }
    }
}