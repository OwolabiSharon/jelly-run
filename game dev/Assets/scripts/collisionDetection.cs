using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class collisionDetection : MonoBehaviour
{
    public characterMovement characterMovement;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    AnimatorStateInfo stateInfo;


    public float hitForce_x = 1f;
    public float hitForce_y = 1f;
    bool isAlive = true;
    bool isInvinsible = false;
    public bool isPlaying = false;
    float previousSpeed;


    private void awake()
    {
        characterMovement = GetComponent<characterMovement>();
        isAlive = characterMovement.isAlive;
    }

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        cancelJumps();
    }

    void cancelJumps()
    {
        myAnimator.SetBool("jumping", false);
        myAnimator.SetBool("Dflipping", false);
        myAnimator.SetBool("Fflipping", false);
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "obstacle" && !isInvinsible)
        {
            //myRigidbody.velocity = new Vector2 (hitForce_x, hitForce_y); 
            previousSpeed = characterMovement.runSpeed;
            characterMovement.runSpeed /= 1.4f;
            StartCoroutine(Invinsibility());
            StartCoroutine(speedController());
        }
        else if (other.gameObject.tag == "boost" && !isInvinsible)
        {
            //myRigidbody.velocity = new Vector2 (hitForce_x, hitForce_y); 
            previousSpeed = characterMovement.runSpeed;
            characterMovement.runSpeed += 10f;
            StartCoroutine(Invinsibility());
            StartCoroutine(speedController());
        }
        else if (other.gameObject.tag == "instantDeath" && !isInvinsible)
        {
            myRigidbody.velocity = new Vector2 (hitForce_x, hitForce_y); 
            Die();
        }

    }

    void Die()
    {
        isAlive = false;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator LoadNextLevel()
    {   
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator speedController()
    {   
        yield return new WaitForSecondsRealtime(5);
        characterMovement.runSpeed = previousSpeed + 0.006f;
    }

    private IEnumerator Invinsibility()
    {
        isInvinsible = true;
        yield return new WaitForSeconds(0.5f);
        isInvinsible = false;
    }
}
