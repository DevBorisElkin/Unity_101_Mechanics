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
        
        description.text = _setup.Description;
        previewSprite.sprite = _setup.PreviewSprite;
        
        button.onClick.AddListener(OnClick_SceneSelected);
    }

    private void OnClick_SceneSelected() => MainManager.Instance.mainController.OpenScene(_setup);

    private void OnDestroy() => button.onClick.RemoveAllListeners();
}
