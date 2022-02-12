using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainManager : PersistentSingleton<MainManager>
{
    public MainController mainController;
    public PrefabsManager prefabsManager;
    
    protected override void Awake() {
        base.Awake();
        InitializeManagers();
    }

    void InitializeManagers()
    {
        mainController = GetComponentInChildren<MainController>();
        prefabsManager = GetComponentInChildren<PrefabsManager>();
    }
}
