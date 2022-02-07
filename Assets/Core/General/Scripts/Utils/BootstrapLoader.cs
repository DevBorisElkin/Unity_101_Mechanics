using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapLoader : MonoBehaviour
{
    public int loadDelayMs = 1000;
    public int pointsChangeDelayMs = 1000;

    
    [Space(5f)] public TMP_Text dotsText;
    private async void Start()
    {
        Debug.Log("Code is here -2");
        InfiniteLoading();
        Debug.Log("Code is here -1");
        LoadSceneWithDelay();
    }

    private async void InfiniteLoading()
    {
        int iteration = 0;
        while (true)
        {
            await UniTask.Delay(pointsChangeDelayMs);

            if (iteration < 3)
                dotsText.text += ".";
            else
            {
                dotsText.text = "";
                iteration = 0;
                continue;
            }
            
            iteration++;
        }
    }

    private async void LoadSceneWithDelay()
    {
        await UniTask.Delay(loadDelayMs);
        Debug.Log("Should load scene with index 1");
        SceneManager.LoadSceneAsync(1);
        Debug.Log("Code is here");
    }
}
