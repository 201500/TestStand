using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ImageTimer : MonoBehaviour
{
    [SerializeField] private bool fillAmount;
    [SerializeField] private bool transparency;

    [SerializeField] private float maxTime;
    [SerializeField] private float increaseTime;

    [SerializeField] private UnityEvent timerEnded;

    private Image img;
    private float currentTime;
    private float saveMaxTime;

    private void Start()
    {
        img = GetComponent<Image>();
        currentTime = 0;
        saveMaxTime = maxTime;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= maxTime)
        {
            timerEnded?.Invoke();
            currentTime = 0;
            maxTime += increaseTime;
        }

        if (fillAmount)
        {
            img.fillAmount = currentTime / maxTime;
        }

        if(transparency)
        {
            img.color = new Color(1, 1, 1, currentTime / maxTime);
        }
    }

    public void ReloadTimer()
    {
        currentTime = 0;
        maxTime = saveMaxTime;
    }
}
