using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProdLoader : MonoBehaviour
{
    private void Awake()
    {
        var mainController = ManagersHolder.Instance.mainController;
        mainController.SceneGotInvoked?.Invoke(mainController.sceneSetups[0]);
    }
}
