using Prime31.TransitionKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPost : MonoBehaviour
{
    [SerializeField]
    public Texture2D maskTexture;

    public int next;
    void Start()
    {
        
    }

    void Update()
    {
        var mask = new ImageMaskTransition()
        {
            maskTexture = maskTexture,
            backgroundColor = Color.yellow,
            nextScene = next
        };
        TransitionKit.instance.transitionWithDelegate(mask);

    }
}
