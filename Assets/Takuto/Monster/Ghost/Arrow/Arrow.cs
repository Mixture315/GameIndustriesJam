using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("プレイヤーのワープ位置")]
    // ワープ位置
    public Vector2 playerTeleportPos = new Vector2(0, 0);

    // 速度
    public float speed = 5f;

    [Header("矢が右向きか？")]
    public bool facingRight = true;

    // 生存時間
    public float lifeTimer = 1.5f;

    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        // 発射時に向きを回転で設定
        transform.rotation = facingRight ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

        GameObject timerObj = GameObject.FindGameObjectWithTag("Timer");
        if (timerObj != null)
        {
            timer = timerObj.GetComponent<Timer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // X方向に進む
        float moveDir = facingRight ? 1f : -1f;
        transform.position += new Vector3(moveDir * speed * Time.deltaTime, 0, 0);

        Life();
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

    void Life()
    {
        lifeTimer -= Time.deltaTime;

        if(lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
