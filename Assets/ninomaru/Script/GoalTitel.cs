using Prime31.TransitionKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTitel : MonoBehaviour
{
    [SerializeField]
    public Texture2D maskTexture;

    public int next = 0;
    private bool _isUiVisible = true;

    void Start()
    {

    }

    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
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

        var enumValues = System.Enum.GetValues(typeof(PixelateTransition.PixelateFinalScaleEffect));
        var randomScaleEffect = (PixelateTransition.PixelateFinalScaleEffect)enumValues.GetValue(Random.Range(0, enumValues.Length));

        var pixelater = new PixelateTransition()
        {
            nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
            finalScaleEffect = randomScaleEffect,
            duration = 1.0f
        };
        TransitionKit.instance.transitionWithDelegate(pixelater);
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
