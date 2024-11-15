using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureSlot : MonoBehaviour
{
    public Sprite furnitureIcon
    {
        get => _furnitureIcon.sprite;
        set => _furnitureIcon.sprite = value;
    }
    public string furnitureNameText
    {
        get => _furnitureNameText.text;
        set => _furnitureNameText.text = value;
    }
    public int furnitureIndex
    {
        get => _furnitureIndex;
        set => _furnitureIndex = value;
    }

    public GameObject furniturePrefeb
    {
        get => _furniturePrefeb.gameObject;
        set => _furniturePrefeb = value;
    }

    [SerializeField] Image _furnitureIcon;
    [SerializeField] TMP_Text _furnitureNameText;
    private int _furnitureIndex;
    private GameObject _furniturePrefeb;

    public GameObject GetFurniturePrefeb()
    {
        return _furniturePrefeb;
    }
}
