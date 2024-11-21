using UnityEngine;
using UnityEngine.UI;

public class UI_BasketSlot : MonoBehaviour
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
    
    public string FurnitureURL
    {
        get => _url;
        set => _url = value;
    }

    UI_BasketController _uibasketController;
    UI_StoreController _uistoreController;

    [SerializeField] Image _image;
    [SerializeField] Text _nameText;
    [SerializeField] Text _countText;
    [SerializeField] Text _priceText;
    [SerializeField] Button _plusButton;
    [SerializeField] Button _minusButton;
    [SerializeField] Button _deleteButton;
    [SerializeField] Button _urlButton;

    public int _count;
    public int _price;
    private string _name;
    private string _url;
    #endregion
    private void Awake()
    {
        _uibasketController = FindObjectOfType<UI_BasketController>();
        _uistoreController = FindObjectOfType<UI_StoreController>();
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
        if (_urlButton == null)
        {
            _urlButton = transform.GetChild(7).GetComponent<Button>();
        }
        //리스너 중복 추가 방지 추가된 모든 리스터 제거
        _plusButton.onClick.RemoveAllListeners();
        _minusButton.onClick.RemoveAllListeners();
        _deleteButton.onClick.RemoveAllListeners();
        _urlButton.onClick.RemoveAllListeners();
        //리스너 추가
        _plusButton.onClick.AddListener(OnPlus);
        _minusButton.onClick.AddListener(OnMinus);
        _deleteButton.onClick.AddListener(OnDelete);
        _urlButton.onClick.AddListener(OnPay);
    }
    void OnPlus()
    {
        FurnitureCount++;
        _uibasketController.UpdateBasketCount(FurnitureIndex, FurnitureCount);
        _uibasketController.TotalPrice();
    }

    void OnMinus()
    {
        if (FurnitureCount > 1)
        {
            FurnitureCount--;
            _uibasketController.UpdateBasketCount(FurnitureIndex, FurnitureCount);
            _uibasketController.TotalPrice();
        }
    }
    /// <summary>
    /// 장바구니 삭제 버튼 함수 
    /// </summary>
    void OnDelete()
    {
        _uibasketController.DeleteBasket(FurnitureIndex);
        _uibasketController.TotalPrice();
    }
    /// <summary>
    /// URL 이동 버튼 함수
    /// </summary>
    void OnPay()
    {
        if (FurnitureURL == null)
        {
            Debug.Log("URL Not Found");
        }
        else
        {
            Application.OpenURL(FurnitureURL);
        }
    }
    /// <summary>
    /// 총 가격 업데이트 함수
    /// </summary>
    public void UpdatePrice()
    {
        _priceText.text = $"{(_price * _count):n0} 원";
    }
}