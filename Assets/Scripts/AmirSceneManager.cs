using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AmirSceneManager : MonoBehaviour
{
    [SerializeField] private Image loadingBar;
    [SerializeField] private GameObject loadingText;
    [SerializeField] private GameObject buttons;
    AsyncOperation asyncOperation;
    [SerializeField] public int sceneCounter = 0;

    //bugs started to appear, ran out of time to fix it. scenes dont load in the right order
    public void WinScene()
    {
        if (sceneCounter == 2) //wins in stage1
        {
            sceneCounter = 5;  //advance to stage2
            asyncOperation = SceneManager.LoadSceneAsync(sceneCounter);
            asyncOperation.allowSceneActivation = false;
        }
        else if (sceneCounter == 5) //wins in stage2
        {
            sceneCounter = 6;
            //asyncOperation = SceneManager.LoadSceneAsync(sceneCounter);
            //asyncOperation.allowSceneActivation = false;
            CreditsScene();
        }

        if (asyncOperation != null)
            asyncOperation.allowSceneActivation = false;
    }
    public void LoseScene()
    {
        if (sceneCounter == 2)  //loses in stage1
        {
            sceneCounter = 1; 
            asyncOperation = SceneManager.LoadSceneAsync(sceneCounter);
            sceneCounter = 2;
            asyncOperation.allowSceneActivation = false;
        }
        else if (sceneCounter == 5)  //loses in stage2
        {
            sceneCounter = 1;
            asyncOperation = SceneManager.LoadSceneAsync(sceneCounter);
            sceneCounter = 7;
            asyncOperation.allowSceneActivation = false;
        }

        if (asyncOperation != null)
            asyncOperation.allowSceneActivation = false;
    }

    private void Update()
    {
        if (asyncOperation != null)
        {
            loadingBar.fillAmount = (asyncOperation.progress / 0.9f);
            buttons.SetActive(false);
            loadingText.SetActive(true);
            if (Input.anyKeyDown)   
                asyncOperation.allowSceneActivation = true;
        } 
    }
    public void Retry()
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneCounter);
        asyncOperation.allowSceneActivation = false;
        Time.timeScale = 1;
        loadingText.SetActive(false);
    }
    public void CreditsScene() 
    {
        sceneCounter = 7;
        SceneManager.LoadSceneAsync(sceneCounter);
    }
    public void MainMenu()
    {
        sceneCounter = 0;
        SceneManager.LoadSceneAsync(sceneCounter);
        sceneCounter = 2;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
