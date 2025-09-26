using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [Header("プレイヤーのワープ位置")]
    // ワープ位置
    public Vector2 playerTeleportPos = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("指定キャラと接触！ 相手は: " + other.gameObject.name);

            // プレイヤーの加速度を0にする
           
            // プレイヤーの位置を移動（Vector2 から Vector3 へ変換）
            player.transform.position = new Vector3(playerTeleportPos.x, playerTeleportPos.y, other.transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        // ギズモの色を設定
        Gizmos.color = Color.red;

        // このオブジェクトに CircleCollider2D がある場合
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        if (col != null)
        {
            // 中心座標と半径を使って円を描画
            Gizmos.DrawWireSphere(transform.position + (Vector3)col.offset, col.radius);
        }
    }
}
