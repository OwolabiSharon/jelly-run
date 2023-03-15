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
    public float extraJumps = 1f;
    public float airSlamPoints = 200f;
    public float triggerPoints = 100f;
    public float jerkForward = 1f;
    public float balloonForce_x = 1f;
    public float balloonForce_y = 1f;
    // public Transform playerHurt;
    // public Transform collectCoin;
    // public Transform balloon;
    // public Transform extraTriggerPoints;
    // public audioManager audioManager;
    // public TextMeshProUGUI plusPoints;
    
    
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    BoxCollider2D myBodyTrigger;
    CapsuleCollider2D myBodyCollider;
    Animator myAnimator;
    AnimatorStateInfo stateInfo;
    SpriteRenderer spriteRenderer;
    Color originalColor;
    bool doubleJumped;
    bool bufferedSlide;
    float gravityScaleAtStart;
    public bool isAlive = true;
    public bool isInvinsible = false;
    public float hpBar = 100;
    public float totalPoints = 0;
    // private InputAction action;
    // public InputActionAsset inputActionAsset;
    // public TextMeshProUGUI hpText;
    // public TextMeshProUGUI pointText;
    public bool isPlaying = false;
    // public GameObject mainMenu;
    // public GameObject gamePlay;
    

    // private void Awake()
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }
    // Start is called before the first frame update
    void Start()
    {
       // MenuReference = FindObjectOfType<Menu>();
       //audioManager = GetComponent<audioManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myBodyTrigger = GameObject.Find ("playerTriggerCollider").GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        //action = inputActionAsset.FindAction("Slide");
        if(!isPlaying)
        {
             myAnimator.SetBool("isPlaying", false);
             myAnimator.SetBool("isIdle", true);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);

        //up and down jumping animation
        if (!isAlive) { return; }
        float maxHeight = 0;
        
        if (myRigidbody.velocity.y > 0)
        {
           myAnimator.SetBool("jumping", true);
           maxHeight = myRigidbody.velocity.y;
        } else if (myRigidbody.velocity.y < maxHeight)
        {
            myAnimator.SetBool("jumping", false);
            myAnimator.SetBool("falling", true);
            myRigidbody.velocity -= new Vector2 (0f, fallSpeed);
            maxHeight = myRigidbody.velocity.y;
        } else if (maxHeight == 0 && myRigidbody.velocity.y == maxHeight)
        {
            myAnimator.SetBool("jumping", false);
            myAnimator.SetTrigger("landing");
            maxHeight = myRigidbody.velocity.y;
        }
        
        //constant running
        if(runSpeed < maxSpeed && isPlaying)
        {
            runSpeed += 0.0001f;
        }
        
        //if is grounded
        if (myBodyTrigger.IsTouchingLayers(LayerMask.GetMask("ground")) && isPlaying)
        {
            whileGrounded();
            myAnimator.SetBool("isPlaying", true);
            myAnimator.SetBool("isIdle", false);
            Vector2 playerVelocity = new Vector2 (runSpeed, myRigidbody.velocity.y);
            Debug.Log(runSpeed);
            myRigidbody.velocity = playerVelocity;
        }else if(myBodyTrigger.IsTouchingLayers(LayerMask.GetMask("ground")) && !isPlaying)
        {
            whileGrounded();
        }
        else
        {
            myAnimator.SetBool("isGrounded", false);
        }
        //death
    }

    void whileGrounded()
    {
        myAnimator.SetBool("jumping", false);
        myAnimator.SetBool("falling", false);
        myAnimator.SetBool("isGrounded", true);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive || !isPlaying) { return; }
        if(value.isPressed && myBodyCollider.IsTouchingLayers(LayerMask.GetMask("ground")))
        {
            myAnimator.SetBool("jumping", true);
            myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpHeight);  
            extraJumps += 1;
        }
        else if(value.isPressed && extraJumps > 0)
        {
            myAnimator.SetBool("falling", false);
           // myAnimator.SetTrigger("doubleJump");
            myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpHeight);   
            extraJumps -= 1;
        }
    }
}
