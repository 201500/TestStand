using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button infoButton;

    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject cityCanvas;
    [SerializeField] private GameObject desk;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject info;


    private void Start()
    {
        mainMenuCanvas.SetActive(true);
        cityCanvas.SetActive(false);
        desk.SetActive(false);
        settings.SetActive(false);
        info.SetActive(false);
    }

    private void Update()
    {
        if (desk.activeSelf)
        {
            BtnInterFalse();
        }
        else
        {
            BtnInterTrue();
        }
    }

    public void PlayButtonClick()
    {
        cityCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void SettingsButtonClick()
    {
        desk.SetActive(true);
        settings.SetActive(true);
    }

    public void InfoButtonClick()
    {
        desk.SetActive(true);
        info.SetActive(true);
    }

    private void BtnInterFalse()
    {
        playButton.interactable = false;
        settingsButton.interactable = false;
        infoButton.interactable = false;
    }

    private void BtnInterTrue()
    {
        playButton.interactable = true;
        settingsButton.interactable = true;
        infoButton.interactable = true;
    }
}
