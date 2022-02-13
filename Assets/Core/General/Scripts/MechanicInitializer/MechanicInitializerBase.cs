using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_101_Mechanics.ClassCollection
{
    public class MechanicInitializerBase : MonoBehaviour, MechanicInitializer
    {
        private SceneSetup _currentSetup;
        
        public void InjectValuesAndInitialize(SceneSetup mainSceneSetup ,SceneSetup sceneSetup)
        {
            _currentSetup = sceneSetup;

            var basicUI = Instantiate(ManagersHolder.Instance.prefabsManager.ui_MechanicUpperPanel_prefab, transform);
            basicUI.InjectValuesAndInitialize(mainSceneSetup, sceneSetup);
        }
    }
}

