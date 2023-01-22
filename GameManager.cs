using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Image civilTimerImg;
    [SerializeField] private Image soldierTimerImg;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject deskCanvas;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject cityCanvas;

    [SerializeField] private Text counterCivil;
    [SerializeField] private Text counterSoldier;
    [SerializeField] private Text counterCarrot;
    [SerializeField] private Text counterHarvest;
    [SerializeField] private Text counterRaid;
    [SerializeField] private Text counterEating;
    [SerializeField] private Text gameOverScreenText;
    [SerializeField] private Text counterRaidGameOver;
    [SerializeField] private Text counterCarrotGameOver;
    [SerializeField] private Text counterCivilGameOver;
    [SerializeField] private Text counterSoldierGameOver;

    [SerializeField] private Button civilButton;
    [SerializeField] private Button soldierButton;

    [SerializeField] private int civilCount;
    [SerializeField] private int soldierCount;
    [SerializeField] private int carrotCount;

    [SerializeField] private int carrotPerCivil;
    [SerializeField] private int carrotToSoldiers;

    [SerializeField] private int civilCost;
    [SerializeField] private int soldierCost;
    [SerializeField] private int increaseRaid;

    [SerializeField] private float civilCreateTime;
    [SerializeField] private float soldierCreateTime;

    [SerializeField] private UnityEvent playWinSoundEvent;
    [SerializeField] private UnityEvent playLoseSoundEvent;
    [SerializeField] private UnityEvent reloadProgressBarsEvent;

    private bool playWinSound = true;
    private bool playLoseSound = true;

    private float civilTimer = -2;
    private float soldierTimer = -2;
    private float civilPrint = 0;
    private float soldierPrint = 0;

    private int nextRaid = 0;
    private int raidCounter = 0;
    private int saveCivilCount;
    private int saveSoldierCount;
    private int saveCarrotCount;


    void Start()
    {
        SaveGameProperties();
        UpdateText();
        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        if (civilTimer > 0)
        {
            civilTimer -= Time.deltaTime;
            civilPrint += Time.deltaTime;
            civilTimerImg.fillAmount = civilPrint / civilCreateTime;
        }
        else if (civilTimer > -1)
        {
            CreateCharacter(civilTimerImg, civilButton, ref civilCount, ref civilTimer, ref civilPrint);
        }

        if (soldierTimer > 0)
        {
            soldierTimer -= Time.deltaTime;
            soldierPrint += Time.deltaTime;
            soldierTimerImg.fillAmount = soldierPrint / soldierCreateTime;
        }
        else if (soldierTimer > -1)
        {
            CreateCharacter(soldierTimerImg, soldierButton, ref soldierCount, ref soldierTimer, ref soldierPrint);
        }

        if (carrotCount < civilCost)
        {
            civilButton.interactable = false;
        }
        else if (carrotCount >= civilCost && civilTimer == -2)
        {
            civilButton.interactable = true;
        }

        if (carrotCount < soldierCost)
        {
            soldierButton.interactable = false;
        }
        else if (carrotCount >= soldierCost && soldierTimer == -2)
        {
            soldierButton.interactable = true;
        }

        UpdateText();

        if (civilCount >= 100 && playWinSound)
        {
            GameOver("You reach 100 civils", "win");
        }

        if (carrotCount >= 500 && playWinSound)
        {
            GameOver("You reach 500 carrots", "win");
        } 

        if (soldierCount < 0 && playLoseSound)
        {
            GameOver("Bees killed all your rabbits", "soldier");
        }

        if (carrotCount < 0 && playLoseSound)
        {
            GameOver("Your soldiers ate all the carrots", "carrot");
        }
    }

    public void timeToRaid()
    {
        raidCounter++;
        if (raidCounter > 2)
        {
            soldierCount -= nextRaid;
            nextRaid += increaseRaid;
        }
        Debug.Log(raidCounter);
    }

    public void timeToHarvest()
    {
        carrotCount += carrotPerCivil * civilCount;
    }

    public void timeToEat()
    {
        carrotCount -= carrotToSoldiers * soldierCount;
    }

    public void CreateCivil()
    {
        carrotCount -= civilCost;
        civilTimer = civilCreateTime;
        civilButton.interactable = false;
    }

    public void CreateSoldier()
    {
        carrotCount -= soldierCost;
        soldierTimer = soldierCreateTime;
        soldierButton.interactable = false;
    }

    public void SettingsButton()
    {
        if (settingsPanel.activeSelf)
        {
            deskCanvas.SetActive(false);
            settingsPanel.SetActive(false);
        }
        else
        {
            deskCanvas.SetActive(true);
            settingsPanel.SetActive(true);
        }
    }

    public void InfoButton()
    {
        if (infoPanel.activeSelf)
        {
            deskCanvas.SetActive(false);
            infoPanel.SetActive(false);
        }
        else
        {
            deskCanvas.SetActive(true);
            infoPanel.SetActive(true);
        }
    }

    public void ExitButton()
    {
        CreateCharacter(civilTimerImg, civilButton, ref civilCount, ref civilTimer, ref civilPrint);
        CreateCharacter(soldierTimerImg, soldierButton, ref soldierCount, ref soldierTimer, ref soldierPrint);
        LoadGameProperties();
        cityCanvas.SetActive(false);
    }

    public void RestartGame()
    {
        LoadGameProperties();
        gameOverScreen.SetActive(false);
    }

    private void GameOver(string gameOverMessage, string condition)
    {
        if(condition == "soldier")
        {
            soldierCount = 0;
            playWinSound = false;
        }
        if(condition == "carrot")
        {
            carrotCount = 0;
            playWinSound = false;
        }
        if(condition == "win")
        {
            playLoseSound = false;
        }
        Time.timeScale = 0;
        UpdateTextGameOver(gameOverMessage);
        gameOverScreen.SetActive(true);
        PlaySound();
        CreateCharacter(civilTimerImg, civilButton, ref civilCount, ref civilTimer, ref civilPrint);
        CreateCharacter(soldierTimerImg, soldierButton, ref soldierCount, ref soldierTimer, ref soldierPrint);
    }

    private void SaveGameProperties()
    {
        saveCarrotCount = carrotCount;
        saveCivilCount = civilCount;
        saveSoldierCount = soldierCount;
    }

    private void LoadGameProperties()
    {
        carrotCount = saveCarrotCount;
        civilCount = saveCivilCount;
        soldierCount = saveSoldierCount;
        raidCounter = 0;
        nextRaid = 0;
        reloadProgressBarsEvent.Invoke();
        Time.timeScale = 1;
        playWinSound = true;
        playLoseSound = true;
    }

    private void CreateCharacter(Image characterTimerImg, Button characterButton, ref int characterCount, ref float characterTimer, ref float characterPrint)
    {
        characterTimerImg.fillAmount = 0;
        characterButton.interactable = true;
        characterCount++;
        characterTimer = -2;
        characterPrint = 0;
    }

    private void PlaySound()
    {
        if (playWinSound)
        {
            playWinSoundEvent.Invoke();
            Debug.Log("You Win");
            playWinSound = false;
        }

        if (playLoseSound)
        {
            playLoseSoundEvent.Invoke();
            Debug.Log("You Lose");
            playLoseSound = false;
        }
    }

    private void UpdateTextGameOver(string gameOverMessage)
    {
        gameOverScreenText.text = gameOverMessage;
        if (raidCounter <= 3)
        {
            counterRaidGameOver.text = "0";
        }
        else
        {
            counterRaidGameOver.text = (raidCounter - 3).ToString();
        }
        counterCarrotGameOver.text = carrotCount.ToString();
        counterCivilGameOver.text = civilCount.ToString();
        counterSoldierGameOver.text = soldierCount.ToString();
    }

    private void UpdateText()
    {
        counterCivil.text = civilCount.ToString();
        counterSoldier.text = soldierCount.ToString();
        counterCarrot.text = carrotCount.ToString();
        counterHarvest.text = (carrotPerCivil * civilCount).ToString();
        counterRaid.text = nextRaid.ToString();
        counterEating.text = (carrotToSoldiers * soldierCount).ToString();
    }
}
