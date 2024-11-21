using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureSlot : MonoBehaviour
{
    public Sprite FurnitureIcon
    {
        get => _furnitureIcon.sprite;
        set => _furnitureIcon.sprite = value;
    }
    public string FurnitureNameText
    {
        get => _furnitureNameText.text;
        set => _furnitureNameText.text = value;
    }
    public int FurnitureIndex
    {
        get => _furnitureIndex;
        set => _furnitureIndex = value;
    }
    public GameObject FurniturePrefeb
    {
        get => _furniturePrefeb.gameObject;
        set => _furniturePrefeb = value;
    }
    public string FurnitureSizeText
    {
        get => _furnitureSizeText.text;
        set => _furnitureSizeText.text = value;
    }
    public Vector3 FurnitureSize
    {
        get => _furnitureSize;
        set => _furnitureSize = value;
    }
    [SerializeField] Image _furnitureIcon;
    [SerializeField] TMP_Text _furnitureNameText;
    [SerializeField] TMP_Text _furnitureSizeText;
    private int _furnitureIndex;
    private GameObject _furniturePrefeb;
    private Vector3 _furnitureSize;

    public GameObject GetFurniturePrefeb()
    {
        return _furniturePrefeb;
    }
}
