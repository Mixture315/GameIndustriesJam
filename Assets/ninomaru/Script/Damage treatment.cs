using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagetreatment : MonoBehaviour
{
    //public CameraShake camera;
    [Header("プレイヤーのワープ位置")]
    // ワープ位置
    public Vector2 playerTeleportPos = new Vector2(0, 0);


    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("指定キャラと接触！ 相手は: " + other.gameObject.name);


            //if (camera != null)
            //{
                //StartCoroutine(camera.Shake(0.3f, 0.5f));

                // プレイヤーの加速度を0にする
                player.myRigid.velocity = Vector3.zero;

                // プレイヤーの位置を移動（Vector2 から Vector3 へ変換）
                player.transform.position = new Vector3(playerTeleportPos.x, playerTeleportPos.y, other.transform.position.z);
                SpriteRenderer sprite = player.GetComponent<SpriteRenderer>();
                sprite.enabled = false;
                player.interval_time = 0.5f;
            //}
            //else
            //{
            //    Debug.Log("値がない: ");
            //}

        }
    }

}
