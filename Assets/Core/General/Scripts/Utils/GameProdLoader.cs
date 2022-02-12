using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProdLoader : MonoBehaviour
{
    private void Awake()
    {
        var mainController = MainManager.Instance.mainController;
        mainController.SceneGotInvoked?.Invoke(mainController.sceneSetups[0]);
    }
}
