using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
private bool doublejump;
    Rigidbody2D rb;
    
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight =  10f ;

    float direction = 0;
    bool isGrounded = false;
    bool isFacingRight = true;

    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(direction);
        if((isFacingRight && direction == -1) || (!isFacingRight && direction == 1))
            Flip();
    }

    void OnMove(InputValue value)
    {
        float v = value.Get<float>();
        Debug.Log(v);
        direction = v;
    }

    void Move(float dir)
    {
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        anim.SetBool("isRunning", dir != 0);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            rb.linearVelocity = new Vector2(dir * (3*speed), rb.linearVelocityY);
            Debug.Log("sprinting");
        }
    }


    void OnJump()
    {
        if(isGrounded && !Input.GetKey(KeyCode.Space))
        {
            doublejump = false;
        }
        if(isGrounded || doublejump)
        {
            Debug.Log("jump active");
            Jump();
            doublejump = !doublejump;
        }
    
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = false;
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (Vector2.Angle(collision.GetContact(i).normal, Vector2.up) < 45f)
            {
                isGrounded = true;
            }
        }

    }

    void OnCollisionExit2D(Collision2D collision) 
    {
        isGrounded = false;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        UnityEngine.Vector3 newLocalScale = transform.localScale;
        newLocalScale.x *= -1f;
        transform.localScale = newLocalScale;
        
    }




    
    
}
