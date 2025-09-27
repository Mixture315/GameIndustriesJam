using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("プレイヤーのワープ位置")]
    // ワープ位置
    public Vector2 playerTeleportPos = new Vector2(0, 0);

    [Header("発射までの待ち時間")]
    public float setShotWaitTime = 1.0f;

    private float shotWaitTime = 0.0f;

    [Header("矢のプレハブ")]
    public GameObject arrowPrefab;

    [Header("矢の速度")]
    public float setSpeed = 5f;

    [Header("矢の生存時間")]
    public float setLifeTimer = 1.5f;

    private Rigidbody2D rb;
    private PlayerController player;

    private Timer timer;

    private CapsuleCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();

        // 待ち時間を設定
        shotWaitTime = setShotWaitTime;

        // ゲーム開始時にプレイヤーを探す
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<PlayerController>();
        }

        GameObject timerObj = GameObject.FindGameObjectWithTag("Timer");
        if (timerObj != null)
        {
            timer = timerObj.GetComponent<Timer>();
        }

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Direction();
            ShotAction();
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
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        // プレイヤーが左にいる
        else if (pos.x > playerPos.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // 発射の行動
    void ShotAction()
    {
        // 発射タイマー更新
        shotWaitTime -= Time.deltaTime;
        if (shotWaitTime <= 0f)
        {
            Shot();
            shotWaitTime = setShotWaitTime; // タイマーリセット
        }
    }

    // 発射
    void Shot()
    {
        if (arrowPrefab != null)
        {
            Vector3 shotPosition = transform.position;
            shotPosition.y -= col.size.y * 0.27f; // 上端から発射

            // X方向（左右）オフセット
            float xOffset = 0.5f; // 左右にずらす量
            if (transform.rotation.eulerAngles.y == 180) // 右向き
            {
                shotPosition.x += xOffset;
            }
            else // 左向き
            {
                shotPosition.x -= xOffset;
            }

            // 矢を生成
            GameObject arrow = Instantiate(arrowPrefab, shotPosition, Quaternion.identity);
            // 矢を取得
            Arrow arrowScript = arrow.GetComponent<Arrow>();
            if (arrowScript != null)
            {
                // 矢の速度を設定
                arrowScript.speed = setSpeed;
                // 生存時間を設定
                arrowScript.lifeTimer = setLifeTimer;
                // 向きを設定
                arrowScript.facingRight = transform.rotation.eulerAngles.y == 180;
            }
        }
    }

    // 当たり判定
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            timer.timeM = 0;
            timer.timeS = 0;
            timer.timeF = 0;

            // プレイヤーの加速度を0にする
            pc.myRigid.velocity = Vector3.zero;

            // プレイヤーの位置を移動（Vector2 から Vector3 へ変換）
            pc.transform.position = new Vector3(playerTeleportPos.x, playerTeleportPos.y, other.transform.position.z);
        }
    }
}
