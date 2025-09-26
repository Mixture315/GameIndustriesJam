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
        myName = gameObject.name;
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
            if(myName == "Right" && collision.gameObject != playerController.gameObject)
            {
                playerController.isRightWallTouch = true;
            }
            else if(myName == "Left" && collision.gameObject != playerController.gameObject)
            {
                playerController.isLeftWallTouch = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
        {
            if (myName == "Right" && collision.gameObject != playerController.gameObject)
            {
                playerController.isRightWallTouch = false;
            }
            else if (myName == "Left" && collision.gameObject != playerController.gameObject)
            {
                playerController.isLeftWallTouch = false;
            }
        }
    }
}
