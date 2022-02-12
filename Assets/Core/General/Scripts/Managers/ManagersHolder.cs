using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ManagersHolder : PersistentSingleton<ManagersHolder>
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
