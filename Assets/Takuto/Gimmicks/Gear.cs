using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [Header("回転速度")]
    public float rotationSpeed = 500; // 回転速度

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
        // 回転
        Rotation();
    }

    // 回転
    void Rotation()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    PlayerController player = other.GetComponent<PlayerController>();
    //    if (player != null)
    //    {
    //        Debug.Log("指定キャラと接触！ 相手は: " + other.gameObject.name);

    //        // プレイヤーの加速度を0にする
    //        player.myRigid.velocity = Vector3.zero;

    //        // プレイヤーの位置を移動（Vector2 から Vector3 へ変換）
    //        player.transform.position = new Vector3(playerTeleportPos.x, playerTeleportPos.y, other.transform.position.z);
    //    }
    //}
}
