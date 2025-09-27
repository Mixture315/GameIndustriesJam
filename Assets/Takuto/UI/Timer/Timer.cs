using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro; 

public class Timer : MonoBehaviour
{
    // タイマーの文字
    public TextMeshProUGUI timerText;

    [Header("アウトラインの太さ")]
    public float outlineWidth = 0.2f;

    [Header("分")]
    public int timeM = 0;
    [Header("秒")]
    public int timeS = 0;
    [Header("フレーム")]
    public float timeF = 0;

    [Header("タイマーストップ")]
    public bool timerStop   = false;

    // Start is called before the first frame update
    void Start()
    {
        timeM     = 0;
        timeS     = 0;
        timeF     = 0;
        timerStop = false;

        // Outline の有効化
        timerText.fontSharedMaterial.EnableKeyword("OUTLINE_ON");

        // 色と太さの設定
        timerText.outlineColor = Color.black;
        timerText.outlineWidth = outlineWidth; // 0～1 の範囲
    }

    // Update is called once per frame
    void Update()
    {
        int displayF = Mathf.Min((int)timeF, 99); // 100 を超えないよう制限
        timerText.text = $"{timeM:D2}:{timeS:D2}:{displayF:D2}";

        // 分
        TimeM();
        // 秒
        TimeS();
        // フレーム
        TimeF();
    }

    // 分
    void TimeM()
    {
        // 秒が60以上
        if (timeS >= 60)
        {
            // 分が99より小さい
            if (timeM < 99)
            { 
                 // 秒を0にする
                 timeS = 0;

                 // 分を増やす
                 timeM++;
            }
            else if(timeM >= 99)
            {
                // タイマーを止める
                timerStop = true;

                // 分を99にする
                timeM = 99;

                // 秒を59にする
                timeS = 59;

                // フレームを99にする
                timeF = 99;
            }
        }

    }

    // 秒
    void TimeS()
    {
        // フレームが100以上なら
        if (timeF >= 100)
        {
            // 秒を増やす
            timeS++;

            // フレームを0にする
            timeF = 0f;
        }
    }

    // フレーム
    void TimeF()
    {
        // タイマーを止めないなら
        if (!timerStop)
        {
            // フレームを計測
            timeF += Time.deltaTime * 60;
        }
    }
}
