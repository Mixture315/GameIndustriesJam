using Prime31.TransitionKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPost : MonoBehaviour
{
    [SerializeField]
    public Texture2D maskTexture;
    [SerializeField]
    public int next = 0;

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
        var mask = new ImageMaskTransition()
        {
            maskTexture = maskTexture,
            backgroundColor = Color.yellow,
            nextScene = next
        };
        TransitionKit.instance.transitionWithDelegate(mask);

    }

}
