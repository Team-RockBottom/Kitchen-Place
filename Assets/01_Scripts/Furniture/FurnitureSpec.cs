using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureSpec", menuName = "Scriptable Objects/FurnitureSpec")]
public class FurnitureSpec : ScriptableObject
{
    public string name;
    public string description;
    public Sprite furnitureSprite;
    public GameObject furniturePrefeb;
}
