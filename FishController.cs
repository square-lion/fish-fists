using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{

    public int health;
    public int points;

    public int knockbackForce;
    public float knockbackTime;
    public float curKnockbackTime;

    public float runAwayTime;

    public Vector2 maxSpeed;
    public Vector2 targetSpeed;
    public Vector2 curSpeed;

    public float accelerationRate;
    public float deaccelerationRate;

    public float timeTillDeath;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    GameManager gameManager;


    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManager>();

        if(transform.position.x < 0)
            targetSpeed = new Vector2(maxSpeed.x, Random.Range(-maxSpeed.y,maxSpeed.y));
        else
            targetSpeed = new Vector2(-maxSpeed.x, Random.Range(-maxSpeed.y,maxSpeed.y));

        Invoke("Kill", timeTillDeath);
    }

    void Update(){

        if(curKnockbackTime > 0){
            curKnockbackTime -= Time.deltaTime;
        }
        else{
            //Set Motion
            if(curSpeed.x < targetSpeed.x-.1)
                curSpeed.x += accelerationRate * Time.deltaTime;
            else if(curSpeed.x > targetSpeed.x+.1)
                curSpeed.x -= accelerationRate * Time.deltaTime;

            if(curSpeed.y < targetSpeed.y-.1)
                curSpeed.y += accelerationRate * Time.deltaTime;
            else if(curSpeed.y > targetSpeed.y+.1)
                curSpeed.y -= accelerationRate * Time.deltaTime;      


            //Set Rotation
            Vector2 moveDirection = rb.velocity;
                if (moveDirection != Vector2.zero) {
                    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }

            spriteRenderer.flipY = rb.velocity.x <= 0;

            if(Mathf.Abs(rb.velocity.x) < maxSpeed.x && Mathf.Abs(rb.velocity.y) < maxSpeed.y)
                rb.velocity = curSpeed;
        }

    }

    public void HitByPlayer(){
            health--;

            gameManager.AddPoints(points);

            if(health <= 0){
                Destroy(gameObject);
            }

            Vector3 direction = transform.position - FindObjectOfType<PlayerController>().transform.position;
            direction.Normalize();
            curKnockbackTime = knockbackTime;

            rb.AddForce(direction * knockbackForce);

        targetSpeed = new Vector2(-targetSpeed.x, -targetSpeed.y);

        animator.SetBool("Hit", true);

        Invoke("StopRunning", runAwayTime);
    }

    void Kill(){
        Destroy(gameObject);
    }

    public void HitOver(){
        animator.SetBool("Hit", false);
    }

    void StopRunning(){
        Debug.Log("Go Back");
        rb.velocity = Vector3.zero;
        curSpeed = Vector2.zero;
        targetSpeed = new Vector2(-targetSpeed.x, -targetSpeed.y);
    }
}
