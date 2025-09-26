using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f; // 移動スピード

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 入力取得（WASD / 矢印キー）
        float h = Input.GetAxis("Horizontal"); // A,D ←→
        float v = Input.GetAxis("Vertical");   // W,S ↑↓

        // 移動ベクトルを作る
        Vector3 move = new Vector3(h, v, 0);

        // 移動
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }
}
