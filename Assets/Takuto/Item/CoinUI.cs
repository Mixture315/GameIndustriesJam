using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro; // これを追加する

public class CoinUi : MonoBehaviour
{
    public PlayerController player;  // プレイヤーをインスペクターで指定
    public TextMeshProUGUI coinText; // コイン枚数

    // Start is called before the first frame update
    void Start()
    {
        
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
