using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private AudioSource sound;
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private GameObject boostText;
    [SerializeField] private Button boostButton;

    private bool paused;
    private bool boosted;

    private void Update()
    {
        if(Time.timeScale == 1)
        {
            boostText.SetActive(false);
        }
        if(Time.timeScale == 5)
        {
            boostText.SetActive(true);
        }
    }

    public void PauseGame()
    {
        if (paused)
        {
            Time.timeScale = 1;
            sound.Play();
            pausedMenu.SetActive(false);
            boostButton.interactable = true;
        }
        else
        {
            Time.timeScale = 0;
            sound.Pause();
            pausedMenu.SetActive(true);
            boostButton.interactable = false;
            BoostGameFix();
        }

        paused = !paused;
    }

    public void BoostGame()
    {
        if (boosted)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 5;
        }

        boosted = !boosted;
    }

    public void BoostGameFix()
    {
        boosted = false;
    }
}
