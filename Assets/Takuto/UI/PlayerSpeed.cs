using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro; // これを追加する

public class PlayerSpeed : MonoBehaviour
{
    private PlayerController player;
    public TextMeshProUGUI speedText;

    // Start is called before the first frame update
    void Start()
    {
        // ゲーム開始時にプレイヤーを探す
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<PlayerController>();
        }

        // Outline の有効化
        speedText.fontSharedMaterial.EnableKeyword("OUTLINE_ON");

        // 色と太さの設定
        speedText.outlineColor = Color.black;
        speedText.outlineWidth = 0.3f; // 0～1 の範囲
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
           float speed =  player.maxHorizontalVelocity;

            speedText.text = "Speed: " + speed.ToString();
        }
    }
}
