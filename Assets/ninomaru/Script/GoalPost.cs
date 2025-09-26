using Prime31.TransitionKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPost : MonoBehaviour
{
    [SerializeField]
    public Canvas canvas;

    void Start()
    {
        canvas = Instantiate(canvas);
    }

    void Update()
    {


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvas.gameObject.SetActive(true);
    }

}
