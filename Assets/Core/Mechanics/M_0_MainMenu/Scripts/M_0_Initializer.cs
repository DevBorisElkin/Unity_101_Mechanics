using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity_101_Mechanics.ClassCollection;
using UnityEngine;

public class M_0_Initializer : MonoBehaviour
{
    [SerializeField] private SnapScrolling snapScrolling;

    public void Awake()
    {
        List<SceneSetup> initialSetups = new List<SceneSetup>();
        initialSetups.InsertRange(0, ManagersHolder.Instance.mainController.sceneSetups.Where(a => a.Index > 0).ToList());
        snapScrolling.SetUp(initialSetups);
    }
}
