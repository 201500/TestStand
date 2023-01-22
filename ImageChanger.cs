using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    [SerializeField] private GameObject imageOne;
    [SerializeField] private GameObject imageTwo;
    [SerializeField] private float updateLength;

    private float currentTime;

    private void Start()
    {
        currentTime = updateLength;
        imageOne.SetActive(true);
        imageTwo.SetActive(false);
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if(currentTime <= updateLength/2)
        {
            imageOne.SetActive(false);
            imageTwo.SetActive(true);
        }

        if(currentTime <= 0)
        {
            currentTime = updateLength;
            imageOne.SetActive(true);
            imageTwo.SetActive(false);
        }
    }
}
