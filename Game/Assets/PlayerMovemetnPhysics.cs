using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemetnPhysics : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public float totalJumpTime;
    float timeSinceJump = 10;
    public Transform groundCheckLocation;
    public float groundCheckRange;
    Rigidbody2D rb;
    bool isGrounded;
    public float jumpXStrength;
    float savedJumpX;
    float AirX;
    public float coyoteTime;
    float timeSinceOffledge;
    bool isJumping;
    bool canJump;
    bool chargingDash;
    float dashChargeAmount;
    bool canMove = true;
    public Transform dashDirL;
    public Transform dashDirR;
    Direction dashDir;

    public enum Direction{
        None,
        Left,
        Right
    }
    float timeSinceLaunch;
    public float maxLaunchPower;
    float dashPower;
    bool canLaunch;
    public SpriteRenderer sprite;
    bool canDash = true;

    public bool onCam = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = Vector2.zero;

        isGrounded = false;
        Collider2D[] groundCheck = Physics2D.OverlapCircleAll(groundCheckLocation.position, groundCheckRange);
        foreach(Collider2D collider in groundCheck){
            if(collider.tag == "Ground"){
                isGrounded = true;
            }
        }

        if(isGrounded){
            canMove = true;
            timeSinceOffledge = 0;
        }
        else{
            timeSinceOffledge += Time.deltaTime;
        }

        if(timeSinceOffledge < coyoteTime && !isJumping){
            canJump = true;
        }
        else{
            canJump = false;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if(isGrounded && rb.velocity.y <= 0){
            canDash = true;
            rb.velocity = new Vector2(horizontalInput*speed, rb.velocity.y);
            AirX = 0;
        }
        else{
            
            rb.AddForce(new Vector2(horizontalInput * speed * 3, 0), ForceMode2D.Force);
        }

        /*

        if(Input.GetKey(KeyCode.Q) && canDash){
            rb.velocity = Vector2.zero;
            sprite.flipX = false;
            dashDir = Direction.Left;
            canMove = false;
            dashChargeAmount += Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.E) && canDash){
            rb.velocity = Vector2.zero;
            sprite.flipX = true;
            dashDir = Direction.Right;
            canMove = false;
            dashChargeAmount += Time.deltaTime;
        }

        if(Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E) || dashChargeAmount >= 1.5f){
            canDash = false;
            if(dashDir == Direction.Left){
                Vector2 dir = (dashDirL.position - transform.position).normalized;
                rb.AddForce(dir*Mathf.Clamp(dashChargeAmount, 0, 1)*maxLaunchPower, ForceMode2D.Impulse);
                dashPower = 0;
            }
            else if(dashDir == Direction.Right){
                Vector2 dir = (dashDirR.position - transform.position).normalized;    
                rb.AddForce(dir*Mathf.Clamp(dashChargeAmount, 0, 1)*maxLaunchPower, ForceMode2D.Impulse);
                dashPower = 0;
            }
            dashChargeAmount = 0;
        }
        */
        

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && canJump){
            savedJumpX = horizontalInput * jumpXStrength;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            rb.AddForce(new Vector2(jumpXStrength * horizontalInput, 0), ForceMode2D.Impulse);
        }
        
    }

   // IEnumerator 

    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(groundCheckLocation.position, groundCheckRange);
    }
}
