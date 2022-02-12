using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_0_Initializer : MonoBehaviour
{
    [SerializeField] private SnapScrolling snapScrolling;

    public void Awake()
    {
        snapScrolling.SetUp(MainManager.Instance.mainController.sceneSetups);
    }
}
