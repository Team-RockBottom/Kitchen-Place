using UnityEngine;

[CreateAssetMenu(fileName = "UI_FurnitureSpec", menuName = "Scriptable Objects/UI_FurnitureSpec")]
public class UI_FurnitureSpec : ScriptableObject
{
    [field: SerializeField] public int Index { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Vector3 Size { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
}