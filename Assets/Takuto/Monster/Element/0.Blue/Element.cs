using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element_Blue : MonoBehaviour
{
    [Header("プレイヤーのワープ位置")]
    // ワープ位置
    public Vector2 playerTeleportPos = new Vector2(0, 0);

    [Header("移動速度")]
    public float moveSpeed = 1;
    [Header("最大速度")]
    public float maxMoveSpeed = 6;
    [Header("移動時間")]
    public float setMoveTime = 1.2f;
    [Header("移動待ち時間")]
    public float setMoveWaitTime = 0.5f;

    // 移動時間
    private float moveTime = 0.0f;
    // 移動待ち時間
    private float moveWaitTime = 0.0f;

    private Rigidbody2D rb;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        moveTime     = setMoveTime;
        moveWaitTime = setMoveWaitTime;

        // ゲーム開始時にプレイヤーを探す
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<PlayerController>();
        }

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)  // プレイヤーが見つかっているときだけ移動処理
        {
            Move();
            Direction();
        }
    }

    // 移動
    void Move()
    {
        // 待ち時間が0より大きいなら
        if (moveWaitTime > 0)
        {
            // 移動時間を設定
            moveTime = setMoveTime;

            moveWaitTime -= Time.deltaTime;
            return;
        }
        // 移動時間
        else
        {
            // 移動時間を設定
            moveTime -= Time.deltaTime;

            if(moveTime <= 0f)
            {
                moveWaitTime = setMoveWaitTime;
                rb.velocity = Vector3.zero;

                return;
            }
        }

        // プレイヤーまでの方向を求める
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // 加速度を与える（ForceMode2D.Force = 加速度的）
        rb.AddForce(direction * 1);

        // 速度制限をかける
        if (maxMoveSpeed > moveSpeed)
        {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }

    // 向き
    void Direction()
    {
        Vector3 pos = transform.position;
        Vector3 playerPos = player.transform.position;

        // プレイヤーが右に居る
        if (pos.x < playerPos.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // プレイヤーが左にいる
        else if (pos.x > playerPos.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // 当たり判定
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (player != null)
        {
            // プレイヤーの加速度を0にする
            player.myRigid.velocity = Vector3.zero;

            // プレイヤーの位置を移動（Vector2 から Vector3 へ変換）
            player.transform.position = new Vector3(playerTeleportPos.x, playerTeleportPos.y, other.transform.position.z);
        }
    }
}
