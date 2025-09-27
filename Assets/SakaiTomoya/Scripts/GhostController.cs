using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostController : MonoBehaviour
{
    int ghostPosStep = 0;
    float timer = 0.0f;

    GameObject player;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + ghostPosStep + "X"))
            Destroy(gameObject);

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > playerController.positionRecordTime)
        {
            Vector2 pos = new Vector2(0, 0);
            if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + ghostPosStep + "X"))
            {
                pos.x = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + ghostPosStep + "X");
            }
            else
            {
                Destroy(gameObject);
            }

            if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + ghostPosStep + "Y"))
            {
                pos.y = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + ghostPosStep + "Y");
            }
            else
            {
                Destroy(gameObject);
            }
            transform.position = pos;
            ghostPosStep++;
            timer = 0;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
