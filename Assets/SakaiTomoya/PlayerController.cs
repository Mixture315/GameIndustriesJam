using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public float wallJumpPower;
    public float maxHorizontalVelocity;
    public float dashSpeed;
    public float dashTime;
    public bool isGround = false;
    public bool isRightWallTouch;
    public bool isLeftWallTouch;
    bool isDash = false;
    public Vector2 rightWallJumpDirection;
    public Vector2 leftWallJumpDirection;
    Vector2 dashDirection;
    Rigidbody2D myRigid;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();       
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Dash();
    }

    void Move()
    {
        if (isDash == false)
        {
            float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            if ((x > 0 && isRightWallTouch) || x < 0 && isLeftWallTouch)
                return;

            Vector3 vel = myRigid.velocity;
            vel.x += x;
            if (Mathf.Abs(vel.x) > maxHorizontalVelocity)
            {
                vel.x = maxHorizontalVelocity * Mathf.Sign(vel.x);
            }
            myRigid.velocity = vel;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround == true)
            {
                myRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            }
            else if(isRightWallTouch == true)
            {
                myRigid.velocity = Vector3.zero;
                myRigid.AddForce(rightWallJumpDirection * wallJumpPower, ForceMode2D.Force);
            }
            else if(isLeftWallTouch == true)
            {
                myRigid.velocity = Vector3.zero;
                myRigid.AddForce(leftWallJumpDirection * wallJumpPower, ForceMode2D.Force);
            }
        }
    }

    void Dash()
    {
        if (myRigid.velocity.x == 0) return;
        if(Input.GetKeyDown(KeyCode.LeftShift) && isDash == false)
        {
            isDash = true;
            dashDirection = new Vector2(Mathf.Sign(myRigid.velocity.x), 0);
            Invoke("DashFinish", dashTime);
        }

        if(isDash)
        {
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime);
        }
    }

    void DashFinish()
    {
        isDash = false;
    }
}
