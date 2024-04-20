using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private AudioSource buttonPress;

    public void SoundSettings()
    {
        soundMenu.SetActive(true);
        mainMenu.SetActive(false);
        buttonPress.Play();
    }
    public void SoundSettingsExit()
    {
        soundMenu.SetActive(false);
        mainMenu.SetActive(true);
        buttonPress.Play();
    }

}
