using Prime31.TransitionKit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public TMP_Text text;

    public int count = 1;
    public int Save_num; // 進行度を保存する変数
    public GameObject[] stageSelect = default; // ステージ選択ボタンの配列
    public GameObject[] look_select = default; // ステージ選択ボタンの配列

    [SerializeField]
    public Texture2D maskTexture;

    public int next = 0;
    private bool _isUiVisible = true;

    void Start()
    {
        //// PlayerPrefsから現在のステージ進行度を取得
        //Save_num = PlayerPrefs.GetInt("SCORE", 0);


        // ステージの解放状態をチェック
        for (int i = 0; i < look_select.Length; i++)
        {
            if (i < Save_num)
            {
                look_select[i].SetActive(false); // 解放済みのボタンを表示
            }
            else
            {
                look_select[i].SetActive(true); // 未解放のボタンを非表示
            }

            stageSelect[i].SetActive(false);
        }
    }

    void Update()
    {
        text.text = count.ToString();

        if (Input.GetKeyDown(KeyCode.A))
        {
            count--;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            count++;
        }

        if (count - 1 <= 0)
        {
            count = 1;
        }
        else if(count - 1 >= 10)
        {
            count = 10;
        }

        // ステージの解放状態をチェック
        for (int i = 0; i < stageSelect.Length; i++)
        {
            stageSelect[i].SetActive(false);
        }
        stageSelect[count - 1].SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))
            if(!look_select[count - 1].activeSelf)
            Scene();

    }

    private void Scene()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
        {
            // bigger buttons for higher res mobile devices
            if (Screen.width >= 1500 || Screen.height >= 1500)
                GUI.skin.button.fixedHeight = 60;
        }

        // hide the UI during transitions
        if (!_isUiVisible)
            return;

        var mask = new ImageMaskTransition()
        {
            maskTexture = maskTexture,
            backgroundColor = Color.yellow,
            nextScene = count + 1
        };
        TransitionKit.instance.transitionWithDelegate(mask);

    }

}
