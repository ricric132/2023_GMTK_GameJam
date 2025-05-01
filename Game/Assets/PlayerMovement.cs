using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    public float gravAcc;
    float currentGrav;
    bool prevGrounded;


    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = Vector2.zero;
        timeSinceJump += Time.deltaTime;

        isGrounded = false;
        Collider2D[] groundCheck = Physics2D.OverlapCircleAll(groundCheckLocation.position, groundCheckRange);
        foreach(Collider2D collider in groundCheck){
            if(collider.tag == "Ground"){
                isGrounded = true;
                if(prevGrounded == false){
                    dashPower = 0;
                }
            }
        }

        if(isGrounded){
            prevGrounded = true;
            canMove = true;
            timeSinceOffledge = 0;
            currentGrav = -5;
        }
        else{
            prevGrounded = false;
            timeSinceOffledge += Time.deltaTime;
            currentGrav -= gravAcc * Time.deltaTime;
        }

        if(timeSinceOffledge < coyoteTime && !isJumping){
            canJump = true;
        }
        else{
            canJump = false;
        }

        //if((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && canLaunch){
            //dashChargeAmount = 0;
        //}

        if(Input.GetKey(KeyCode.Q) && dashChargeAmount <= 1.5f){
            dashPower = 0;
            currentGrav = 0;
            timeSinceLaunch = 0;
            dashDir = Direction.Left;
            canMove = false;
            dashChargeAmount += Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.E) && dashChargeAmount <= 1.5f){
            dashPower = 0;
            currentGrav = 0;
            timeSinceLaunch = 0;
            dashDir = Direction.Right;
            canMove = false;
            dashChargeAmount += Time.deltaTime;
        }

        if(Input.GetKeyUp(KeyCode.Q) && dashDir == Direction.Left){
            dashPower = Mathf.Clamp(dashChargeAmount, 0, 1) * maxLaunchPower;
            dashChargeAmount = 0;
        }

        if(Input.GetKeyUp(KeyCode.E) && dashDir == Direction.Right){
            dashPower = Mathf.Clamp(dashChargeAmount, 0, 1) * maxLaunchPower;
            dashChargeAmount = 0;
        }

        timeSinceLaunch += Time.deltaTime;
        if(dashPower > 0){
            float power = Mathf.Max(-Mathf.Pow(5f * timeSinceLaunch, 3) + dashPower, 0);
            //float power = dashPower;
            if(dashDir == Direction.Left){
                velocity = (dashDirL.position - transform.position) * power;
            }
            else{
                velocity = (dashDirR.position - transform.position) * power;    
            }
            Debug.Log(velocity);
        }

        if(canMove){
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            if(isGrounded){
                velocity.x += horizontalInput * speed;
                AirX = 0;
            }
            else{
                AirX += horizontalInput * speed * 2 * Time.deltaTime;
            }
            

            if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && canJump){
                currentGrav = 0;
                timeSinceJump = 0;
                savedJumpX = horizontalInput * jumpXStrength;
                //rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }

            if(timeSinceJump < totalJumpTime){
                float currentJumpForce = Mathf.Pow((2*timeSinceJump - totalJumpTime/5)/totalJumpTime, 2f) * -jumpForce + jumpForce;
                velocity.y += currentJumpForce;
            }
            

            if(!isGrounded){
                velocity.x = AirX + savedJumpX;
            }
        
        }
        
        velocity.y += currentGrav;

        rb.velocity = velocity;
        
    }

    

    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(groundCheckLocation.position, groundCheckRange);
    }
}
