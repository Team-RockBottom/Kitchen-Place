using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureData", menuName = "Scriptable Objects/FurnitureData")]
public class FurnitureData : ScriptableObject
{
    public int Index;
    public Sprite Image;
    public string Name;
    public string Description;
    public Vector3 Size;
    public int Price;
}
