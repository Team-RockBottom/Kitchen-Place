using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureSpec", menuName = "Scriptable Objects/FurnitureSpec")]
public class FurnitureSpec : ScriptableObject
{
    [field:SerializeField] public int index{ get; private set; } //가구 번호
    [field:SerializeField] new public string name { get; private set; } //가구 이름
    [field:SerializeField] public string description { get; private set; } //가구 설명
    [field:SerializeField] public Sprite furnitureSprite { get; private set; } //가구 스프라이트
    [field:SerializeField] public GameObject furniturePrefeb { get; private set; } //가구 프리펩
}
