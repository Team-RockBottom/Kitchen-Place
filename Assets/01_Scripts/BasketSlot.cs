using UnityEngine;
using UnityEngine.UI;

public class BasketSlot : MonoBehaviour
{
    #region
    public int _index;
    public int FurnitureIndex
    {
        get => _index;
        set => _index = value;
    }

    public Sprite FurnitureImage
    {
        get => _image.sprite;
        set => _image.sprite = value;
    }

    public int FurnitureCount
    {
        get => _count;
        set
        {
            _count = value;
            _countText.text = value.ToString();
            UpdatePrice();
        }
    }

    public int FurniturePrice
    {
        get => _price;
        set
        {
            _price = value;
            UpdatePrice();
        }
    }
    public string FurnitureName
    {
        get => _name;
        set => _nameText.text = value;
    }

    BasketController _basketController;

    [SerializeField] Image _image;
    [SerializeField] Text _nameText;
    [SerializeField] Text _countText;
    [SerializeField] Text _priceText;
    [SerializeField] Button _plusButton;
    [SerializeField] Button _minusButton;
    [SerializeField] Button _deleteButton;

    public int _count;
    public int _price;
    private string _name;
    #endregion
    private void Awake()
    {
        _basketController = FindObjectOfType<BasketController>();

        if (_image == null)
        {
            _image = transform.GetChild(0).GetComponent<Image>();
        }
        if (_nameText == null)
        {
            _nameText = transform.GetChild(1).GetComponent<Text>();
        }
        if (_countText == null)
        {
            _countText = transform.GetChild(2).GetComponent<Text>();
        }
        if (_priceText == null)
        {
            _priceText = transform.GetChild(3).GetComponent<Text>();
        }
        if ( _plusButton == null)
        {
            _plusButton = transform.GetChild(4).GetComponent<Button>();
        }
        if(_minusButton == null)
        {
            _minusButton = transform.GetChild(5).GetComponent<Button>();
        }
        if (_deleteButton == null)
        {
            _deleteButton = transform.GetChild(6).GetComponent<Button>();
        }
        //리스너 중복 추가 방지 추가된 모든 리스터 제거
        _plusButton.onClick.RemoveAllListeners();
        _minusButton.onClick.RemoveAllListeners();
        _deleteButton.onClick.RemoveAllListeners();

        //리스너 추가
        _plusButton.onClick.AddListener(OnPlus);
        _minusButton.onClick.AddListener(OnMinus);
        _deleteButton.onClick.AddListener(OnDelete);
    }
    void OnPlus()
    {
        FurnitureCount++;
        _basketController.UpdateBasketCount(FurnitureIndex, FurnitureCount);
        _basketController.TotalPrice();
    }
    void OnMinus()
    {
        if (FurnitureCount > 1)
        {
            FurnitureCount--;
            _basketController.UpdateBasketCount(FurnitureIndex, FurnitureCount);
            _basketController.TotalPrice();
        }
    }
    /// <summary>
    /// 장바구니 삭제 버튼 함수 
    /// </summary>
    void OnDelete()
    {
        _basketController.DeleteBasket(FurnitureIndex);
        _basketController.TotalPrice();
    }

    public void UpdatePrice()
    {
        _priceText.text = $"{(_price * _count):n0} 원";
    }
}