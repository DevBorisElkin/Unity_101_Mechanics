using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity_101_Mechanics.ClassCollection;

public class SnapScrollingSubItem : MonoBehaviour
{
    [SerializeField] private TMP_Text description;
    [SerializeField] private Image previewSprite;
    [SerializeField] private Button button;

    private SceneSetup _setup;
    
    public void SetUp(SceneSetup sceneSetup)
    {
        _setup = sceneSetup;
        
        description.text = _setup.MechanicData.description;
        previewSprite.sprite = _setup.MechanicData.previewSprite;
        
        button.onClick.AddListener(OnClick_SceneSelected);
    }

    private void OnClick_SceneSelected() => ManagersHolder.Instance.mainController.OpenScene(_setup);

    private void OnDestroy() => button.onClick.RemoveAllListeners();
}
