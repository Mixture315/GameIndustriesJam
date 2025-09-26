using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro; // これを追加する

public class CoinUi : MonoBehaviour
{
    public PlayerController player;  // プレイヤーをインスペクターで指定
    public TextMeshProUGUI coinText; // コイン枚数

    [Header("アウトラインの太さ")]
    public float outlineWidth = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        // Outline の有効化
        coinText.fontSharedMaterial.EnableKeyword("OUTLINE_ON");

        // 色と太さの設定
        coinText.outlineColor = Color.black;
        coinText.outlineWidth = outlineWidth; // 0～1 の範囲
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーのコイン数を UI に反映
        if (player != null && coinText != null)
        {
            coinText.text = "Coin: " + player.coinCount.ToString();
        }
    }
}
