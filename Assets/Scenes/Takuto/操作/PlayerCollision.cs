using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
