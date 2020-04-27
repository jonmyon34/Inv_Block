using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody rb;
    PlayerHealthManager playerHealthManager;

    public const float MOVE_SPEED = 15.0f;

    void Awake()
    {
        playerHealthManager = this.gameObject.GetComponent<PlayerHealthManager>();
    }

    void FixedUpdate()
    {

        if (playerHealthManager.exist)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { LeftMove(); }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) { RightMove(); }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) { ForwardMove(); }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) { BackMove(); }
        }

    }

    private void LeftMove() { this.transform.position += new Vector3(-MOVE_SPEED, 0.0f, 0.0f) * Time.deltaTime; }
    private void RightMove() { this.transform.position += new Vector3(MOVE_SPEED, 0.0f, 0.0f) * Time.deltaTime; }
    private void ForwardMove() { this.transform.position += new Vector3(0.0f, 0.0f, MOVE_SPEED) * Time.deltaTime; }
    private void BackMove() { this.transform.position += new Vector3(0.0f, 0.0f, -MOVE_SPEED) * Time.deltaTime; }

}