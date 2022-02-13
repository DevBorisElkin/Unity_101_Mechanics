using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity_101_Mechanics.ClassCollection;

public class MainController : MonoBehaviour
{
    private MechanicInitializerBase oldSpawnedContext;
    public Action<SceneSetup> SceneGotInvoked;
    public List<SceneSetup> sceneSetups;

    private void Awake()
    {
        Debug.Log("MainController.Awake();");
        ManageSubscriptions(true);
    }

    private void OnValidate()
    {
        if (sceneSetups == null || sceneSetups.Count < 0) return;

        for (int i = 0; i < sceneSetups.Count; i++)
        {
            sceneSetups[i].Index = i;
            if (string.IsNullOrEmpty(sceneSetups[i].Description))
                sceneSetups[i].Description = $"Mechanic {i}";
        }
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

        oldSpawnedContext = Instantiate(setup.ScenePrefab, null);
        if(setup.Index != 0)
            oldSpawnedContext.InjectValuesAndInitialize(sceneSetups[0], setup);
    }

}
