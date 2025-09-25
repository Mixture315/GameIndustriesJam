using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRightLeftController : MonoBehaviour
{
    string myName;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        name = gameObject.name;
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
        {
            if(myName == "Right")
            {
                playerController.isRightWallTouch = true;
            }
            else if(myName == "Left")
            {
                playerController.isLeftWallTouch = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
        {
            if (myName == "Right")
            {
                playerController.isRightWallTouch = false;
            }
            else if (myName == "Left")
            {
                playerController.isLeftWallTouch = false;
            }
        }
    }
}
