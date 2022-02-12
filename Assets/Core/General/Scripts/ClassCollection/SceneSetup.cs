using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_101_Mechanics.ClassCollection
{
    [Serializable]
    public class SceneSetup
    {
        [SerializeField] private int _index;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _previewSprite;
        [SerializeField] private GameObject _scenePrefab;

        public int Index { get => _index; private set => _index = value; }
        public string Description { get => _description; private set => _description = value; }
        public Sprite PreviewSprite { get => _previewSprite; private set => _previewSprite = value; }
        public GameObject ScenePrefab { get => _scenePrefab; private set => _scenePrefab = value; }
    }
}

