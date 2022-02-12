using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainManager : PersistentSingleton<MainManager>
{
    //[HideInInspector]
    public MainController mainController;
    
    protected override void Awake() {
        base.Awake();
        InitializeManagers();
    }

    void InitializeManagers()
    {
        mainController = GetComponentInChildren<MainController>();
    }
}
