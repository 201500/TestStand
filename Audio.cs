using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource themeSong;
    [SerializeField] private AudioSource clickAudio;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource loseSound;

    [SerializeField] private GameObject playingSoundImage;
    [SerializeField] private GameObject playingInterSoundImage;
    [SerializeField] private GameObject pausedSoundImage;
    [SerializeField] private GameObject pausedInterSoundImage;

    private bool interfaceSoundStatus = true;
    
    private void Start()
    {
        themeSong.Play();
        playingSoundImage.SetActive(true);
        pausedSoundImage.SetActive(false);
        playingInterSoundImage.SetActive(true);
        pausedInterSoundImage.SetActive(false);
    }

    public void ChangeSoundPlay()
    {
        if (themeSong.isPlaying)
        {
            themeSong.Pause();
            playingSoundImage.SetActive(false);
            pausedSoundImage.SetActive(true);
        }
        else
        {
            themeSong.Play();
            playingSoundImage.SetActive(true);
            pausedSoundImage.SetActive(false);
        }
    }

    public void InterfaceButton()
    {
        PlaySound(clickAudio);
    }

    public void WinSound()
    {
        PlaySound(winSound);
    }

    public void LoseSound()
    {
        PlaySound(loseSound);
    }

    private void PlaySound(AudioSource sound)
    {
        if (interfaceSoundStatus)
        {
            sound.Play();
        }
    }

    public void ChangeInterfaceSound()
    {
        interfaceSoundStatus = !interfaceSoundStatus;
        if (interfaceSoundStatus)
        {
            playingInterSoundImage.SetActive(true);
            pausedInterSoundImage.SetActive(false);
        }
        else
        {
            playingInterSoundImage.SetActive(false);
            pausedInterSoundImage.SetActive(true);
        }
    }
}
