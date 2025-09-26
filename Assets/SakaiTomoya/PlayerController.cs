using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーの加速スピード")]
    public float speed;
    [Header("プレイヤーのジャンプ力")]
    public float jumpPower;
    [Header("プレイヤーの壁ジャンプ力")]
    public float wallJumpPower;
    [Header("プレイヤーの水平方向の最大速度")]
    public float maxHorizontalVelocity;
    [Header("プレイヤーのダッシュ中のスピード")]
    public float dashSpeed;
    [Header("プレイヤーのダッシュの時間")]
    public float dashTime;
    [Header("プレイヤーのダッシュのクールタイム")]
    public float dashCoolTime;

    [HideInInspector]
    public int coinCount = 0;
    [HideInInspector]
    public bool isGround = false;
    [HideInInspector]
    public bool isRightWallTouch;
    [HideInInspector]
    public bool isLeftWallTouch;

    bool isDash = false;
    bool dashOk = true;

    [Header("プレイヤーの右壁ジャンプの方向")]
    public Vector2 rightWallJumpDirection;
    [Header("プレイヤーの左壁ジャンプの方向")]
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
        if(Input.GetKeyDown(KeyCode.LeftShift) && isDash == false && dashOk == true)
        {
            dashOk = false;
            isDash = true;
            dashDirection = new Vector2(Mathf.Sign(myRigid.velocity.x), 0);
            StartCoroutine(DashReset());
        }

        if(isDash)
        {
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime);
        }
    }

    IEnumerator DashReset()
    {
        yield return new WaitForSeconds(dashTime);
        isDash = false;
        yield return new WaitForSeconds(dashCoolTime);
        dashOk = true;
    }
}
