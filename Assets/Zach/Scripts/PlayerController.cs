using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int baseSpeed;
    public float gravity;
    public float groundLevel;
    public float jumpHeight;
    public float health { get; internal set; }

    bool moveLeft;
    bool moveRight;

    public bool isPlayerOne;

    float downVelocity;
    bool isGrounded;

    CameraScript cam;
    PlayerManager playerManager;

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
        cam = ServiceLocator.instance.cam;
        playerManager = ServiceLocator.instance.playerManager;
    }

    void Update()
    {
        CheckMovement(isPlayerOne);
    }

    void FixedUpdate()
    {
        Movement(isPlayerOne);

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
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void CheckMovement(bool _isPlayerOne)
    {
        //keyboard controls
        if (_isPlayerOne)
        {
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
            if (Input.GetKey("s") && !moveLeft && isGrounded)
            {
                moveRight = true;
            }
            else if (Input.GetKey("s") && moveLeft && isGrounded)
            {
                moveLeft = false;
            }
            if (Input.GetKeyUp("s") && moveRight && isGrounded)
            {
                moveRight = false;
            }
            if (Input.GetKeyUp("s") && Input.GetKey("a") && isGrounded)
            {
                moveLeft = true;
            }

            //actions for changing state on Space bar
            if (isGrounded && Input.GetKeyDown("d"))
            {
                isGrounded = false;
                downVelocity = jumpHeight;
            }

            //stop movement when hitting the ground
            if (isGrounded && !Input.GetKey("a") && !Input.GetKey("s"))
            {
                moveLeft = false;
                moveRight = false;
            }
        }
        else
        {
            //actions for changing state on left key
            if (Input.GetKey("j") && !moveRight && isGrounded)
            {
                moveLeft = true;
            }
            else if (Input.GetKey("j") && moveRight && isGrounded)
            {
                moveRight = false;
            }
            if (Input.GetKeyUp("j") && moveLeft && isGrounded)
            {
                moveLeft = false;
            }
            if (Input.GetKeyUp("j") && Input.GetKey("j") && isGrounded)
            {
                moveRight = true;
            }

            //actions for changing state on right key
            if (Input.GetKey("k") && !moveLeft && isGrounded)
            {
                moveRight = true;
            }
            else if (Input.GetKey("k") && moveLeft && isGrounded)
            {
                moveLeft = false;
            }
            if (Input.GetKeyUp("k") && moveRight && isGrounded)
            {
                moveRight = false;
            }
            if (Input.GetKeyUp("k") && Input.GetKey("j") && isGrounded)
            {
                moveLeft = true;
            }

            //actions for changing state on Space bar
            if (isGrounded && Input.GetKeyDown("l"))
            {
                isGrounded = false;
                downVelocity = jumpHeight;
            }

            //stop movement when hitting the ground
            if (isGrounded && !Input.GetKey("j") && !Input.GetKey("k"))
            {
                moveLeft = false;
                moveRight = false;
            }
        }
    }

    void Movement(bool _isPlayerOne)
    {
        if (moveLeft)
        {
            transform.position += Vector3.right * -baseSpeed * 0.01f;
            if (transform.position.x < cam.transform.position.x - (cam.boxExtents.x / 2 - transform.localScale.x))
            {

                transform.position += Vector3.right * baseSpeed * 0.01f;
            }
        }
        if (moveRight)
        {
            transform.position += Vector3.right * baseSpeed * 0.01f;
            if (transform.position.x > cam.transform.position.x + (cam.boxExtents.x / 2 - transform.localScale.x))
            {

                transform.position += Vector3.right * -baseSpeed * 0.01f;
            }
        }
    }

    void Punch()
    {

    }
}
