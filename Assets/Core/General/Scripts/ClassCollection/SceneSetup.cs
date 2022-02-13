using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Unity_101_Mechanics.ClassCollection
{
    [Serializable]
    public class SceneSetup
    {
        [SerializeField] private int _index;
        [SerializeField] [ReadOnly] private string _description;
        [SerializeField][Expandable] private MechanicData _mechanicData;
        [SerializeField] private MechanicInitializerBase _scenePrefab;

        public int Index { get => _index; set => _index = value; }
        public string Description { get => _description; set => _description = value; }
        public MechanicInitializerBase ScenePrefab { get => _scenePrefab; private set => _scenePrefab = value; }
        public MechanicData MechanicData { get => _mechanicData; private set => _mechanicData = value; }
    }
}

