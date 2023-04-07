using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class characterMovement : MonoBehaviour
{
    public static characterMovement instance;
    public float runSpeed = 10f;
    public float maxSpeed = 10f;
    public float jumpHeight = 10f;
    public float slide = 1f;
    public float airSlam = 3f;
    public float fallSpeed = 1f;
    public float airSlamPoints = 200f;
    public float triggerPoints = 100f;
    public float jerkForward = 1f;
    public float balloonForce_x = 1f;
    public float balloonForce_y = 1f;
    
    
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    BoxCollider2D myBodyTrigger;
    CapsuleCollider2D myBodyCollider;
    Animator myAnimator;
    AnimatorStateInfo stateInfo;
    SpriteRenderer spriteRenderer;
    Color originalColor;
    
    bool doubleJumped;
    bool bufferedJump = false;
    bool isFalling = false;
    float gravityScaleAtStart;
    public bool isAlive = true;
    public bool isInvinsible = false;
    public float hpBar = 100;
    public float totalPoints = 0;
    public bool isPlaying = false;
    public float speedJumpThreshhold = 13f;
   
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myBodyTrigger = GameObject.Find ("playerTriggerCollider").GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);

        //up and down jumping animation
        if (!isAlive) { return; }

        //constant running
        if(runSpeed < maxSpeed && isPlaying)
        {
            runSpeed += 0.0001f;
        }
        if (myBodyTrigger.IsTouchingLayers(LayerMask.GetMask("ground")))
        {
            Vector2 playerVelocity = new Vector2 (runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
            whileGrounded();
        }else
        {
            myAnimator.SetBool("isGrounded", false);
        }
    }

    void whileGrounded()
    {
        myAnimator.SetBool("isGrounded", true);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive || !isPlaying) { return; }
        if(value.isPressed && (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("ground"))) && stateInfo.IsName("run"))
        {
            JumpFunction();
        }
    }

    void JumpFunction()
    {
        myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpHeight);  
        if(runSpeed < speedJumpThreshhold)
        {
            myAnimator.SetBool("jumping", true);
        }
        else if (runSpeed > maxSpeed)
        {
            myAnimator.SetBool("Fflipping", true);
        }
        else 
        {
            myAnimator.SetBool("Dflipping", true);
        }
    }
    
}


