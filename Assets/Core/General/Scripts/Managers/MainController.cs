using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity_101_Mechanics.ClassCollection;

public class MainController : MonoBehaviour
{
    private Transform oldSpawnedContext;
    public Action<SceneSetup> SceneGotInvoked;
    public List<SceneSetup> sceneSetups;

    private void Awake()
    {
        Debug.Log("MainController.Awake();");
        ManageSubscriptions(true);
    }

    void ManageSubscriptions(bool state)
    {
        if (state)
        {
            SceneGotInvoked += LoadScene;
        }
        else
        {
            SceneGotInvoked -= LoadScene;
        }
    }

    void LoadScene(SceneSetup setup)
    {
        if(oldSpawnedContext != null) Destroy(oldSpawnedContext.gameObject);

        oldSpawnedContext = Instantiate(setup.ScenePrefab, null).transform;
    }

}
