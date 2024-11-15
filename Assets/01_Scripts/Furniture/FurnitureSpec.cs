using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureSpec", menuName = "Scriptable Objects/FurnitureSpec")]
public class FurnitureSpec : ScriptableObject
{
    [field:SerializeField] public int index{ get; private set; } //���� ��ȣ
    [field:SerializeField] new public string name { get; private set; } //���� �̸�
    [field:SerializeField] public string description { get; private set; } //���� ����
    [field:SerializeField] public Sprite furnitureSprite { get; private set; } //���� ��������Ʈ
    [field:SerializeField] public GameObject furniturePrefeb { get; private set; } //���� ������
}
