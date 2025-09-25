using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public float wallJumpPower;
    public float maxHorizontalVelocity;
    public float dashPower;
    public bool isGround = false;
    public bool isRightWallTouch;
    public bool isLeftWallTouch;
    public Vector2 rightWallJumpDirection;
    public Vector2 leftWallJumpDirection;
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
        //Dash();
        Debug.Log(myRigid.velocity);
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if((x > 0 && isRightWallTouch) || x < 0 && isLeftWallTouch)
        {
            Debug.Log("aaa");
            return;
        }
        Vector3 vel = myRigid.velocity;
        vel.x += x;
        if(Mathf.Abs(vel.x) > maxHorizontalVelocity)
        {
            vel.x = maxHorizontalVelocity * Mathf.Sign(vel.x);
        }
        myRigid.velocity = vel;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround == true)
            {
                Debug.Log("bbb");
                myRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            }
            else if(isRightWallTouch == true)
            {
                Debug.Log("ccc");
                myRigid.velocity = Vector3.zero;
                myRigid.AddForce(rightWallJumpDirection * wallJumpPower, ForceMode2D.Force);
            }
            else if(isLeftWallTouch == true)
            {
                Debug.Log("ddd");
                myRigid.velocity = Vector3.zero;
                myRigid.AddForce(leftWallJumpDirection * wallJumpPower, ForceMode2D.Force);
            }
        }
    }

    void Dash()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 vel = myRigid.velocity;
            vel.x = Mathf.Sign(vel.x) * dashPower;
            myRigid.velocity = vel;
        }
    }
}
