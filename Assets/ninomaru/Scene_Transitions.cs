using Prime31.TransitionKit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Transitions : Singleton<Scene_Transitions>
{
    [SerializeField]
    public Texture2D maskTexture;

    public int next = 0;
    private bool _isUiVisible = true;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void OnClick()
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

    void OnEnable()
    {
        TransitionKit.onScreenObscured += onScreenObscured;
        TransitionKit.onTransitionComplete += onTransitionComplete;
    }


    void OnDisable()
    {
        // as good citizens we ALWAYS remove event handlers that we added
        TransitionKit.onScreenObscured -= onScreenObscured;
        TransitionKit.onTransitionComplete -= onTransitionComplete;
    }

    void onScreenObscured()
    {
        _isUiVisible = false;
    }


    void onTransitionComplete()
    {
        _isUiVisible = true;
    }

}
