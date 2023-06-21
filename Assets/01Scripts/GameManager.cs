using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float mouseSensSetting = 0.5f;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }

    public void increaseMsSens()
    {

    }

    public void loadScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
}
