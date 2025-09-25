using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionGear : MonoBehaviour
{
    // 判定対象をインスペクターで指定できるようにする
    public GameObject target;
    // ワープ位置
    public Vector2 teleportPosition = new Vector2(0, 0);

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
        if (other.gameObject == target)
        {
            Debug.Log("指定キャラと接触！ 相手は: " + other.gameObject.name);

            // 位置を移動（Vector2 から Vector3 へ変換）
            other.transform.position = new Vector3(teleportPosition.x, teleportPosition.y, other.transform.position.z);
        }
    }
}
