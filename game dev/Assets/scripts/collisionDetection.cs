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
    public float runSpeed = 10f;
    public float maxSpeed = 10f;
    public float jumpHeight = 100f;
    public float slide = 1f;
    public float airSlam = 3f;
    public float fallSpeed = 1f;
    public float extraJumps = 1f;
    public float airSlamPoints = 200f;
    public float triggerPoints = 100f;
    public float jerkForward = 1f;
    public float balloonForce_x = 1f;
    public float balloonForce_y = 1f;
    public Transform playerHurt;
    public Transform collectCoin;
    public Transform balloon;
    public Transform extraTriggerPoints;
    public audioManager audioManager;
    public TextMeshProUGUI plusPoints;
    
    
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    AnimatorStateInfo stateInfo;
    SpriteRenderer spriteRenderer;
    Color originalColor;
    bool isAlive = true;
    bool isInvinsible = false;
    public float hpBar = 100;
    public float totalPoints = 0;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI pointText;
    public bool isPlaying = false;
    private void awake()
    {
        characterMovement = GetComponent<characterMovement>();
        totalPoints = characterMovement.totalPoints;
        hpBar = characterMovement.hpBar;
        extraJumps = characterMovement.extraJumps;
        plusPoints = characterMovement.plusPoints;
        pointText = characterMovement.pointText;
        audioManager = characterMovement.audioManager;
        balloon = characterMovement.balloon;
        runSpeed = characterMovement.runSpeed;
        jumpHeight = characterMovement.jumpHeight;
        airSlamPoints = characterMovement.airSlamPoints;
        triggerPoints = characterMovement.triggerPoints;
        extraTriggerPoints = characterMovement.extraTriggerPoints;
        isAlive = characterMovement.isAlive;
    }

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (totalPoints > PlayerPrefs.GetFloat("highscore"))
        {
            PlayerPrefs.SetFloat("highscore", totalPoints );
        }

        if (hpBar <= 0)
        {
           Die();
         
        }

        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (extraJumps > 0)
        {
            extraJumps -= 1;
        }
    }
    
    private void gainPoints(float points, Vector3 objectPos)
    {
        plusPoints.gameObject.SetActive(true);
        plusPoints.transform.position = objectPos;
        totalPoints += points;
        pointText.text = $"{totalPoints}";
        
        Invoke("offtext", 0.3f);
    }
    public void gainCoinPoints(float points)
    {
        plusPoints.gameObject.SetActive(true);
        plusPoints.transform.position = transform.position;
        totalPoints += points;
        pointText.text = $"{totalPoints}";
        
        Invoke("offtext", 0.3f);
    }

    void offtext()
    {
        plusPoints.gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other) {
        //balloon
        if (other.gameObject.tag == "balloon")
        {
            Transform particle = other.transform.Find("particles");
            Vector3 particleSpawn = particle.position;
            audioManager.SendMessage("OnBalloonBounceFunc");
            Instantiate(balloon,particleSpawn,Quaternion.identity);
            if (extraJumps == 0)
            {
                extraJumps += 1;
            } 
            myRigidbody.velocity = new Vector2 (runSpeed, jumpHeight); 
            if (stateInfo.IsName("airSlam")) 
            {
                gainPoints(airSlamPoints,other.transform.position);
            }
            Destroy(other.gameObject);
        }

        //damage causing interactables
        if (other.gameObject.tag == "causeDamage" || other.gameObject.tag == "tree" )
            {
                Transform particle = other.transform.Find("particles");
                Vector3 particleSpawn = particle.position;
                
                //other.GetComponentInChildren<ParticleSystem>().Play();
                //partic.play()
                if (stateInfo.IsName("airSlam") && other.gameObject.tag == "causeDamage") 
                {
                    gainPoints(airSlamPoints, other.transform.position);
                    myAnimator.SetBool("jumping", true);
                    myRigidbody.velocity = new Vector2 (runSpeed, jumpHeight); 
                    if (extraJumps == 0)
                    {
                        extraJumps += 1;
                    } 
                    Instantiate(balloon,particleSpawn,Quaternion.identity);
                    Destroy(other.gameObject);
                    audioManager.SendMessage("onAirSlamFunc");
                }else
                {
                    if (isInvinsible) { return; }
                    myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jerkForward); 
                    myAnimator.SetTrigger("tookDamage");
                    hpBar -= 20f;    
                    hpText.text = $"{hpBar}/";
                    Instantiate(playerHurt,transform.position,Quaternion.identity);
                    //StartCoroutine(Invinsibility());
                    audioManager.SendMessage("onDamageFunc");
                }
            }
         if (other.gameObject.tag == "extraPoints" && stateInfo.IsName("slide"))
         {
            gainPoints(triggerPoints, other.transform.position);
            audioManager.SendMessage("onAirSlamFunc");
            Instantiate(extraTriggerPoints,transform.position,Quaternion.identity);
         }
    }

    IEnumerator LoadNextLevel()
    {   
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Die()
    {
        audioManager.SendMessage("onDeathFunc");
        isAlive = false;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        myAnimator.SetTrigger("death");
        StartCoroutine(LoadNextLevel());
    }

    // private IEnumerator Invinsibility()
    // {
    //     Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a * 0.5f);
    //     spriteRenderer.color = newColor;
    //     isInvinsible = true;
    //     yield return new WaitForSeconds(0.5f);
    //     spriteRenderer.color = originalColor;
    //     isInvinsible = false;
    //     //myRigidbody.velocity = new Vector2 (0f, (-1 * airSlam)); 
         
    // }
}
