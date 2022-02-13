using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_101_Mechanics.ClassCollection
{
    public class MechanicInitializerBase : MonoBehaviour, MechanicInitializer
    {
        [SerializeField] protected UI_MechanicUpperPanel ui_mechanicUpperPanel;
        private SceneSetup _currentSetup;
        
        public void InjectValuesAndInitialize(SceneSetup mainSceneSetup ,SceneSetup sceneSetup)
        {
            _currentSetup = sceneSetup;
            ui_mechanicUpperPanel.InjectValuesAndInitialize(mainSceneSetup, sceneSetup);
        }
    }
}

