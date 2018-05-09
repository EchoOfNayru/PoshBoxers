using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int baseSpeed;
    public float gravity;
    public float groundLevel;
    public float jumpHeight;

    bool moveLeft;
    bool moveRight;

    public bool isPlayerOne;

    float downVelocity;
    bool isGrounded;

    void Awake()
    {
        if (isPlayerOne)
        {
            ServiceLocator.instance.playerManager.PlayerOne = this;
        }
        else
        {
            ServiceLocator.instance.playerManager.PlayerTwo = this;
        }
    }

    void Start()
    {
        isGrounded = true;
    }

    void Update()
    {
        //keyboard controls
        //actions for changing state on left key
        if (Input.GetKey("a") && !moveRight && isGrounded)
        {
            moveLeft = true;
        }
        else if (Input.GetKey("a") && moveRight && isGrounded)
        {
            moveRight = false;
        }
        if (Input.GetKeyUp("a") && moveLeft && isGrounded)
        {
            moveLeft = false;
        }
        if (Input.GetKeyUp("a") && Input.GetKey("d") && isGrounded)
        {
            moveRight = true;
        }

        //actions for changing state on right key
        if (Input.GetKey("d") && !moveLeft && isGrounded)
        {
            moveRight = true;
        }
        else if (Input.GetKey("d") && moveLeft && isGrounded)
        {
            moveLeft = false;
        }
        if (Input.GetKeyUp("d") && moveRight && isGrounded)
        {
            moveRight = false;
        }
        if (Input.GetKeyUp("d") && Input.GetKey("a") && isGrounded)
        {
            moveLeft = true;
        }

        if (isGrounded && !Input.anyKey)
        {
            moveLeft = false;
            moveRight = false;
        }

        //actions for changing state on Space bar
        if (isGrounded && Input.GetKeyDown("space"))
        {
            isGrounded = false;
            downVelocity = jumpHeight;
        }
    }

    void FixedUpdate()
    {
        if (moveLeft)
        {
            transform.position += Vector3.right * -baseSpeed * 0.01f;
        }
        if (moveRight)
        {
            transform.position += Vector3.right * baseSpeed * 0.01f;
        }

        if (isGrounded)
        {
            downVelocity = 0;
            transform.position = new Vector3(transform.position.x, groundLevel, transform.position.z);
        }
        else
        {
            downVelocity -= gravity;
            transform.position = new Vector3(transform.position.x, transform.position.y + downVelocity, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
