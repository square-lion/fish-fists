using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    GameManager gameManager;
    AudioManager audioManager;

    public Vector2 maxSpeed;
    public float slowestSpeed;
    public Vector2 curSpeed;

    public float targetRotation;
    public float curRotation;

    public float accelerationRate;
    public float deaccelerationRate;
    public float rotationSpeed;

    public bool punchHit;

    public Transform joystick;


    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update(){
        //Set Movement
        if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) || joystick.localPosition.y > 40){
            if(curSpeed.y < maxSpeed.y-.1)
                curSpeed.y += accelerationRate * Time.deltaTime;
        }
        else{
            if(curSpeed.y > slowestSpeed + .1)
                curSpeed.y -= deaccelerationRate * Time.deltaTime;
        }

        if((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) || joystick.localPosition.y < -40){
            if(curSpeed.y > -maxSpeed.y+.1)
                curSpeed.y -= accelerationRate * Time.deltaTime;
        }
        else{
            if(curSpeed.y < -slowestSpeed -.1)
                curSpeed.y += deaccelerationRate * Time.deltaTime;
        }

        if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) || joystick.localPosition.x > 40){
            if(curSpeed.x < maxSpeed.x-.1)
                curSpeed.x += accelerationRate * Time.deltaTime;
        }
        else{
            if(curSpeed.x > slowestSpeed + .1)
                curSpeed.x -= deaccelerationRate * Time.deltaTime;
        }

        if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) || joystick.localPosition.x < -40){
            if(curSpeed.x > -maxSpeed.x+.1)
                curSpeed.x -= accelerationRate * Time.deltaTime;
        }  
        else{
            if(curSpeed.x < -slowestSpeed - .1)
                curSpeed.x += deaccelerationRate * Time.deltaTime;
        }

        //Set Rotation
        Vector2 moveDirection = rb.velocity;
            if (moveDirection != Vector2.zero && Input.anyKey) {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * rotationSpeed);
            }

        spriteRenderer.flipY = curSpeed.x < 0;

        rb.velocity = curSpeed;

        if(Input.GetKeyDown(KeyCode.Space)){
            Punch();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Fish") && animator.GetBool("Attacking") && other.GetComponent<FishController>().curKnockbackTime <= 0){
            gameManager.ComboPunch(1);
            punchHit = true;
            other.GetComponent<FishController>().HitByPlayer();
        }
    }

    public void PunchEnd(){
        animator.SetBool("Attacking", false);

        if(!punchHit)
            gameManager.ComboPunch(0);
        else
            punchHit = false;
    }

    public void Punch(){
        if(!animator.GetBool("Attacking")){
            animator.SetBool("Attacking", true);
            audioManager.Play("Punch");
        }
    }
}


