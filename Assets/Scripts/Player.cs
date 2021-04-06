using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float velocity;
    public GameObject winCanvas;

    private Rigidbody2D rigid;
    private Vector2 lastTouchPosition;
    private float degreesPerSec = 360f;
    private float horizontal;
    private bool onPosition = false;
    private bool onPlayerWin = false;
    private float initialTime = 0;

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

        if (onPlayerWin == true && initialTime > 3) {
            reloadGame();
        } else {
            initialTime += Time.deltaTime;
        }
    }

    public void reloadGame() {
        winCanvas.SetActive(false);
        onPosition = false;
        onPlayerWin = false;
        rigid.velocity = Vector2.up * velocity;
        rigid.velocity = new Vector2(1.0f * 5, rigid.velocity.y);
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
        if (onPosition == true && onPlayerWin == false && rigid.velocity.x == 0 && rigid.velocity.y == 0) {
            winCanvas.SetActive(true);
            onPlayerWin = true;
            initialTime = 0;
        }
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