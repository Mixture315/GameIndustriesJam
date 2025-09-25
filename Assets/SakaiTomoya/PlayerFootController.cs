using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootController : MonoBehaviour
{
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger == false)
        {
            playerController.isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
        {
            playerController.isGround = false;
        }
    }
}
