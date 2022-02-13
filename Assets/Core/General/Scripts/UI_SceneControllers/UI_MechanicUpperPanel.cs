using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unity_101_Mechanics.ClassCollection
{
    public class UI_MechanicUpperPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text extensiveDescription;
        [SerializeField] private Button backToMenuBtn;
        private SceneSetup _mainSceneSetup;
        private SceneSetup _sceneSetup;
        
        public void InjectValuesAndInitialize(SceneSetup mainSceneSetup ,SceneSetup sceneSetup)
        {
            _mainSceneSetup = mainSceneSetup;
            _sceneSetup = sceneSetup;
            
            backToMenuBtn.onClick.AddListener(MoveToTheMainMenu);
        }


        void MoveToTheMainMenu() => ManagersHolder.Instance.mainController.OpenScene(_mainSceneSetup);
        
        private void OnDestroy() => backToMenuBtn.onClick.RemoveAllListeners();
    }
}

