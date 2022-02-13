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
        [Header("Upper panel")]
        [SerializeField] private RectTransform upperPanel;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image mechanicImage;
        
        [Header("Extensive Description")]
        [SerializeField] private GameObject extDescriptionsModalWindow;
        [SerializeField] private TMP_Text extensiveDescription;
        
        [Header("Buttons")]
        [SerializeField] private Button backToMenuBtn;
        [SerializeField] private Button extensiveDescriptionBtn;
        [SerializeField] private Button closeExtensiveDescrBtn;
        
        [SerializeField] private Button hideShowTopPanelBtn;
        [SerializeField] private TMP_Text showHideBtnTxt;
        
        private SceneSetup _mainSceneSetup;
        private SceneSetup _sceneSetup;

        private bool topState;
        
        public void InjectValuesAndInitialize(SceneSetup mainSceneSetup ,SceneSetup sceneSetup)
        {
            _mainSceneSetup = mainSceneSetup;
            _sceneSetup = sceneSetup;
            
            upperPanel.gameObject.SetActive(true);
            extDescriptionsModalWindow.SetActive(false);

            description.text = _sceneSetup.MechanicData.description;
            mechanicImage.sprite = _sceneSetup.MechanicData.previewSprite;
            extensiveDescription.text = _sceneSetup.MechanicData.extensiveDescription;
            
            backToMenuBtn.onClick.AddListener(MoveToTheMainMenu);
            extensiveDescriptionBtn.onClick.AddListener(() => { SetDescriptionPanelActiveState(true);});
            closeExtensiveDescrBtn.onClick.AddListener(() => { SetDescriptionPanelActiveState(false);});
            hideShowTopPanelBtn.onClick.AddListener(SwitchTopPanelVisibleState);
        }


        void MoveToTheMainMenu() => ManagersHolder.Instance.mainController.OpenScene(_mainSceneSetup);

        void SetDescriptionPanelActiveState(bool state)
        {
            extDescriptionsModalWindow.SetActive(state);
        }

        void SwitchTopPanelVisibleState()
        {
            upperPanel.Animate_TopPanelSlideUpDown(!topState, 0.25f);
            ManageShowHideBtn();
        }

        void ManageShowHideBtn()
        {
            topState = !topState;
            if (topState)
                showHideBtnTxt.text = "Show";
            else
                showHideBtnTxt.text = "Hide";
        }

        //private void OnDestroy()
        //{
        //    backToMenuBtn.onClick.RemoveAllListeners();
        //    extensiveDescriptionBtn.onClick.RemoveAllListeners();
        //    closeExtensiveDescrBtn.onClick.RemoveAllListeners();
        //    hideShowTopPanelBtn.onClick.RemoveAllListeners();
        //}
    }
}

