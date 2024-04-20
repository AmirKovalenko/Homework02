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
    AsyncOperation asyncOperation;
    private int sceneCounter = 1;
    public void WinScene()
    {
        if (sceneCounter == 1) //wins in stage1
        {
            sceneCounter = 4;  //advance to stage2
            asyncOperation = SceneManager.LoadSceneAsync(sceneCounter);
            asyncOperation.allowSceneActivation = false;
        }
        else if (sceneCounter == 4) //wins in stage2
        {
            sceneCounter = 5;
            asyncOperation = SceneManager.LoadSceneAsync(sceneCounter);
            asyncOperation.allowSceneActivation = false;
            CreditsScene();
        }

        if (asyncOperation != null)
            asyncOperation.allowSceneActivation = false;
    }
    public void LoseScene()
    {
        if (sceneCounter == 1)  //loses in stage1
        {
            sceneCounter = 2; 
            asyncOperation = SceneManager.LoadSceneAsync(sceneCounter);
            sceneCounter = 1;
            asyncOperation.allowSceneActivation = false;
        }
        else if (sceneCounter == 4)  //loses in stage2
        {
            sceneCounter = 2;
            asyncOperation = SceneManager.LoadSceneAsync(sceneCounter);
            sceneCounter = 4;
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
            loadingText.SetActive(true);
            if (Input.anyKeyDown)
            {
                
                asyncOperation.allowSceneActivation = true;
            }
        } 
    }
    public void Retry()
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneCounter);
        asyncOperation.allowSceneActivation = false;
        Time.timeScale = 1;
    }
    public void CreditsScene() 
    {
        sceneCounter = 5;
        SceneManager.LoadSceneAsync(sceneCounter);
    }
    public void MainMenu()
    {
        sceneCounter = 0;
        SceneManager.LoadSceneAsync(sceneCounter);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
