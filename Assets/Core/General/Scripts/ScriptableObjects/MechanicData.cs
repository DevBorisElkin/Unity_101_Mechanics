using UnityEngine;

[CreateAssetMenu(fileName = "MechanicData", menuName = "ScriptableObjects/MechanicData")]
public class MechanicData : ScriptableObject
{
    public Sprite previewSprite;
    public string description;
    [TextArea(3, 6)]
    public string extensiveDescription;
}
