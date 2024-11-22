using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureSpec", menuName = "Scriptable Objects/FurnitureSpec")]
public class FurnitureSpec : ScriptableObject
{
    [field: SerializeField] public int Index{ get; private set; } //가구 번호
    [field: SerializeField] public string Name { get; private set; } //가구 이름
    [TextArea(3, 5)]
    [field: SerializeField] public string Description { get; private set; } //가구 설명
    [field: SerializeField] public Sprite Sprite { get; private set; } //가구 스프라이트
    [field: SerializeField] public GameObject Prefeb { get; private set; } //가구 프리펩
    [field: SerializeField] public Vector3 Size { get; private set; } //가구 규격
    [field: SerializeField] public int Price { get; private set; } //가구 가격
    [field: SerializeField] public string URL { get; private set; } //가구 구매 사이트
}
