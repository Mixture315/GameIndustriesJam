using Prime31.TransitionKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClearSelect : Singleton<ClearSelect>
{
    [Header("→の画像")]
    public Image image;
    [Header("→の位置")]
    public Transform text1_transform;
    public Transform text2_transform;
    public Transform text3_transform;
    [Header("→の位置カウント")]
    public int count;

    [SerializeField]
    public Texture2D maskTexture;

    public int next = 0;
    private bool _isUiVisible = true;
    public int now_scene_count = 0;
    void Start()
    {
        now_scene_count = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        gameObject.SetActive(false);
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.W))
        {
            count--;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            count++;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (count)
            {
                case 0:
                    next = ++now_scene_count;
                    break;
                case 1:
                    next = now_scene_count;
                    break;
                case 2:
                    next = 0;

                    break;
            }

            Scene();

        }

        Vector2 pos  = image.transform.position;

        switch (count)
        {
            case 0:
                pos.y = text1_transform.position.y;
            break;
            case 1:
                pos.y = text2_transform.position.y;

                break;
            case 2:
                pos.y = text3_transform.position.y;

                break;
        }

        image.transform.position = pos;



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
            nextScene = next
        };
        TransitionKit.instance.transitionWithDelegate(mask);

    }
}
