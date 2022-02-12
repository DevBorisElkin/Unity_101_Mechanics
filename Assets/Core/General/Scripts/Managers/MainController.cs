using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void OpenScene(int id)
    {
        var setup = sceneSetups.FirstOrDefault(a => a.Index == id);
        if(setup != null)
            LoadScene(setup);
    }

    public void OpenScene(SceneSetup setup)
    {
        if(setup != null)
            LoadScene(setup);
    }

    void LoadScene(SceneSetup setup)
    {
        if(oldSpawnedContext != null) Destroy(oldSpawnedContext.gameObject);

        oldSpawnedContext = Instantiate(setup.ScenePrefab, null).transform;
    }

}
