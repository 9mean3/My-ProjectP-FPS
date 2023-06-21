using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]Transform camera;

    [SerializeField] Transform startPos;
    [SerializeField] Transform stageSelectPos;

    [SerializeField] float duration;
    enum mainmenu
    {
        start,
        selectStage,
    }
    mainmenu mainmenuState;
    void Start()
    {
        mainmenuState = mainmenu.start;
    }

    void Update()
    {
        switch (mainmenuState)
        {
            case mainmenu.start:
                start();
                break;
            case mainmenu.selectStage:
                    selectStage();
                break;
        }
    }

    private void selectStage()
    {
        camera.position = Vector3.Lerp(camera.position, stageSelectPos.position, duration * Time.deltaTime);
    }

    private void start()
    {
        camera.position = Vector3.Lerp(camera.position, startPos.position, duration * Time.deltaTime);
    }

    public void ExitButton()
    {
        if(mainmenuState == mainmenu.selectStage)
        {
            mainmenuState = mainmenu.start;
        }
        else
        {
            Application.Quit();
        }
    }

    public void StartButton()
    {
        mainmenuState = mainmenu.selectStage;
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
}
