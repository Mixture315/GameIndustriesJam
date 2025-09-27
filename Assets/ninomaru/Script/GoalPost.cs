using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalPost : MonoBehaviour
{
    [SerializeField]
    public Canvas canvas;

    public Timer timer;

    void Start()
    {
        canvas = Instantiate(canvas);

        //GameObject timerObj = GameObject.FindGameObjectsWithTag("Timer");
        //if(timerObj != null)
        //{
        //    timer = timerObj.GetComponent<Timer>();
        //}
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

        timer.timerStop = true;
    }

}
