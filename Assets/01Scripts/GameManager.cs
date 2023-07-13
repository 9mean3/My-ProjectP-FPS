using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    public void SetMouseCurser(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void loadScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
}
