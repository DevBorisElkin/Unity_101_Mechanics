using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity_101_Mechanics.ClassCollection;

public class SnapScrollingItem : MonoBehaviour
{
    public int canHoldItems = 8;
    [SerializeField] public Transform holderForItems;
    
    public void SetUp(List<SceneSetup> setups)
    {
        for (int i = 0; i < setups.Count; i++)
        {
            var item = Instantiate(MainManager.Instance.prefabsManager.snapScrollingSubItem_prefab, holderForItems);
            item.SetUp(setups[i]);
        }
    }
}
