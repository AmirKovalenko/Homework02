using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject mainMenu;

    public void SoundSettings()
    {
        soundMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void SoundSettingsExit()
    {
        soundMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

}
