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
    [Header("プレイヤーのジャンプしたときの声")]
    public AudioClip jumpVoice;
    [Header("プレイヤーのダッシュしたときの声")]
    public AudioClip dashVoice;
    [Header("プレイヤーのグラビティスケール")]
    public float gravityScale;

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

    [HideInInspector]
    public Rigidbody2D myRigid;

    Animator myAnim;
    SpriteRenderer mySpriteRenderer;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント取得
        myRigid = GetComponent<Rigidbody2D>();       
        myAnim = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();

        myRigid.gravityScale = gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Move();//移動処理
        Jump();//ジャンプ処理
        Dash();//ダッシュ処理
        Anim();//アニメーション処理

        Debug.Log(myRigid.velocity.x);

    }

    private void FixedUpdate()
    {
        
    }


    void Move()
    {
        if (isDash == false)
        {
            float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            if ((x > 0.01f && isRightWallTouch) || x < -0.01 && isLeftWallTouch)
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
                myAudioSource.PlayOneShot(jumpVoice);
            }
            else if(isRightWallTouch == true)
            {
                myRigid.velocity = Vector3.zero;
                myRigid.AddForce(rightWallJumpDirection * wallJumpPower, ForceMode2D.Force);
                myAudioSource.PlayOneShot(jumpVoice);
            }
            else if(isLeftWallTouch == true)
            {
                myRigid.velocity = Vector3.zero;
                myRigid.AddForce(leftWallJumpDirection * wallJumpPower, ForceMode2D.Force);
                myAudioSource.PlayOneShot(jumpVoice);
            }
        }
    }

    void Dash()
    {
        if (Mathf.Approximately(myRigid.velocity.x,0.0f)) return;
        if(Input.GetKeyDown(KeyCode.LeftShift) && isDash == false && dashOk == true)
        {
            dashOk = false;
            isDash = true;
            myRigid.gravityScale = 0;
            dashDirection = new Vector2(Mathf.Sign(myRigid.velocity.x), 0);
            myAudioSource.PlayOneShot(dashVoice);
            StartCoroutine(DashReset());
        }

        if(isDash)
        {
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime);
        }
    }
    void Anim()
    {
        if(myRigid.velocity.x > 0.01f)
        {
            mySpriteRenderer.flipX = false;
        }
        else if(myRigid.velocity.x < -0.01f)
        {
            mySpriteRenderer.flipX = true;
        }

        if (myRigid.velocity.y > 0.01f)
        {
            myAnim.SetTrigger("JumpUp");
        }
        else if (myRigid.velocity.y < -0.01f)
        {
            myAnim.SetTrigger("JumpDown");
        }
        //else if (!Mathf.Approximately(myRigid.velocity.x,0))
        else if (Mathf.FloorToInt(myRigid.velocity.x) != 0)
        {
            myAnim.SetTrigger("Run");
        }
        else
        {
            myAnim.SetTrigger("Wait");
        }
    }

    IEnumerator DashReset()
    {
        yield return new WaitForSeconds(dashTime);
        isDash = false;
        myRigid.gravityScale = gravityScale;
        yield return new WaitForSeconds(dashCoolTime);
        dashOk = true;
    }
}
