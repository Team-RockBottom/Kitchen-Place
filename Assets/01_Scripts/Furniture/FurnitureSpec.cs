using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureSpec", menuName = "Scriptable Objects/FurnitureSpec")]
public class FurnitureSpec : ScriptableObject
{
    [field: SerializeField] public int Index{ get; private set; } //���� ��ȣ
    [field: SerializeField] public string Name { get; private set; } //���� �̸�
    [TextArea(3, 5)]
    [field: SerializeField] public string Description { get; private set; } //���� ����
    [field: SerializeField] public Sprite Sprite { get; private set; } //���� ��������Ʈ
    [field: SerializeField] public GameObject Prefeb { get; private set; } //���� ������
    [field: SerializeField] public Vector3 Size { get; private set; } //���� �԰�
    [field: SerializeField] public int Price { get; private set; } //���� ����
    [field: SerializeField] public string URL { get; private set; } //���� ���� ����Ʈ
}
