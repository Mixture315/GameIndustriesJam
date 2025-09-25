using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public bool isGround = false;
    public bool isRightWallTouch;
    public bool isLeftWallTouch;
    public Vector2 rightWallJumpDirection;
    public Vector2 leftWallJumpDirection;
    public float wallJumpPower;
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
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if((x > 0 && isRightWallTouch) || x < 0 && isLeftWallTouch)
        {
            return;
        }
        transform.Translate(x, 0, 0);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround == true)
            {
                myRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            else if(isRightWallTouch == true)
            {
                myRigid.AddForce(rightWallJumpDirection * jumpPower, ForceMode2D.Impulse);
            }
            else if(isLeftWallTouch == true)
            {
                myRigid.AddForce(leftWallJumpDirection * jumpPower, ForceMode2D.Impulse);
            }
        }
    }
}
