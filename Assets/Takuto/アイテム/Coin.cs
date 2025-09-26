using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // 衝突判定の対象
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 当たり判定表示
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            Debug.Log("指定キャラと接触！ 相手は: " + other.gameObject.name);

            
        }
    }

    // 当たり判定表示
    private void OnDrawGizmos()
    {
        CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();
        if (col == null) return;

        Gizmos.color = Color.green;

        // カプセルの中心位置
        Vector3 pos = transform.position + (Vector3)col.offset;

        // Unity が 2D カプセルを描画するAPIを持たないので、
        // ここでは単純にワイヤー円と長方形で「カプセルっぽく」描画する例
        float radius = col.size.x * 0.5f;
        float height = col.size.y;

        if (col.direction == CapsuleDirection2D.Vertical)
        {
            // 上下の丸
            Gizmos.DrawWireSphere(pos + Vector3.up * (height * 0.5f - radius), radius);
            Gizmos.DrawWireSphere(pos - Vector3.up * (height * 0.5f - radius), radius);

            // 真ん中の長方形
            Gizmos.DrawWireCube(pos, new Vector3(col.size.x, height - col.size.x, 0));
        }
        else // 横向き
        {
            Gizmos.DrawWireSphere(pos + Vector3.right * (height * 0.5f - radius), radius);
            Gizmos.DrawWireSphere(pos - Vector3.right * (height * 0.5f - radius), radius);

            Gizmos.DrawWireCube(pos, new Vector3(height - col.size.y, col.size.y, 0));
        }
    }
}
