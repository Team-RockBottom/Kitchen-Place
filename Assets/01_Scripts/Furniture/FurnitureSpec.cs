using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureSpec", menuName = "Scriptable Objects/FurnitureSpec")]
public class FurnitureSpec : ScriptableObject
{
    public int index; //���� ��ȣ
    public string name; //���� �̸�
    public string description; //���� ����
    public Sprite furnitureSprite; //���� ��������Ʈ
    public GameObject furniturePrefeb; //���� ������
}
