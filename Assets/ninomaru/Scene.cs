using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : Singleton<Scene>
{

    [SerializeField] private string _loadScene; //シーン名を記述

    private void Start()
    {
        SceneManager.LoadScene("title");

    }

    public void SceneChange()
    {
        SceneManager.LoadScene(_loadScene);
    }

}
