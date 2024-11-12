using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureSpec", menuName = "Scriptable Objects/FurnitureSpec")]
public class FurnitureSpec : ScriptableObject
{
    public int index; //가구 번호
    public string name; //가구 이름
    public string description; //가구 설명
    public Sprite furnitureSprite; //가구 스프라이트
    public GameObject furniturePrefeb; //가구 프리펩
}
