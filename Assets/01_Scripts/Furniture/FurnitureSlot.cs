using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureSlot : MonoBehaviour
{
    // 슬롯데이터를 관리하는 스크립트
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

    [SerializeField] Image _furnitureIcon; // 스프라이트
    [SerializeField] TMP_Text _furnitureNameText; // 이름
    private int _furnitureIndex; // 가구 번호
    private GameObject _furniturePrefeb; // 가구 프리펩
}
