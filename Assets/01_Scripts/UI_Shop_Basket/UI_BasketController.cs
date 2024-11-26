using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UI_FurnitureSlotDataModel;

public class UI_BasketController : MonoBehaviour
{
    #region
    [Header("BasketUI")]
    [SerializeField] GameObject _mainMenuCanvas;
    [SerializeField] UI_FurnitureSpecRepository _uifurnitureSpecRepository;
    [SerializeField] public GameObject _basketCanvas;  //��ٱ��� ĵ����
    [SerializeField] Button _basketButton;      //��ٱ��� ��ư
    [SerializeField] Transform _basketContent;  //��ٱ��� ��ũ�Ѻ� �θ� ������Ʈ
    [SerializeField] GameObject _basketUIPrefab;//��ٱ��� UI ������ 
    [SerializeField] Text _totalPrice; //�� ���� �ؽ�Ʈ
    [SerializeField] GameObject _totalBasketImage; //�� ��ٱ��� ���ӿ�����Ʈ 
    [SerializeField] Text _totalBasketCount; //�� ��ٱ��� ��� ���� �ؽ�Ʈ
    [SerializeField] Button _buyButton; //�����ϱ� ��ư
    [SerializeField] GameObject _payCanvas; //�����ϱ� ĵ����
    [SerializeField] Text _totalPriceButton; //�� ���� �����ϱ� �ؽ�Ʈ
    [SerializeField] public Button _payButton; //�����ϱ� ��ư

    List<UI_BasketSlot> _basketSlots; //�߰��� ��ٱ��� ���� ����Ʈ
    public List<UI_FurnitureSlotData> _uibasketSlotsDataList; //��ٱ��� ����Ʈ ������

    [Header("PayPapUpUI")]
    [SerializeField] GameObject _payPopUpImage;     //���� �˾� UI
    [SerializeField] Text _payPopUpText;            //���� Ȯ�� ��ư �ؽ�Ʈ
    [SerializeField] Button _checkButton;           //���� Ȯ�� ��ư
    [SerializeField] InputField[] _inputfieldText;  //��ǲ �ʵ� �ؽ�Ʈ

    [SerializeField] BackButtonManager _backButtonManager;
    #endregion
    private void Start()
    {
        _basketCanvas.SetActive(false);
        _payCanvas.SetActive(false);
        _basketButton.onClick.AddListener(OnBasket);
        _buyButton.onClick.AddListener(OnPay);
        _basketSlots = new List<UI_BasketSlot>();
        _uibasketSlotsDataList = new List<UI_FurnitureSlotData>();
        _totalBasketImage.SetActive(false);
        _payPopUpImage.SetActive(false);
        _payButton.onClick.AddListener(OnPayPopUp);
        _checkButton.onClick.AddListener(OnCheck);
        for (int i = 0; i < _inputfieldText.Length; i++)
        {
            _inputfieldText[i].onValueChanged.AddListener(CheckInputField);
        }

        _inputfieldText[1].characterLimit = 13;
    }
    /// <summary>
    /// ��ٱ��� UI ��ư
    /// </summary>
    void OnBasket()
    {
        _basketCanvas.SetActive(true);
        _mainMenuCanvas.SetActive(!_mainMenuCanvas.activeSelf);
        UpdateBasketUI();
        CheckBasket();
    }
    void OnPay()
    {
        _basketCanvas.SetActive(false);
        _payCanvas.SetActive(true);
        _payPopUpImage.SetActive(false);
        _payButton.interactable = false;
    }
    /// <summary>
    /// ���Ź�ư Ŭ�� �Լ�
    /// </summary>
    void OnPayPopUp()
    {
        _payPopUpImage.SetActive(true);
        _payButton.interactable = false;
        TotalPrice();
        _backButtonManager._payCanvasExitButton.interactable = false;

        for (int i = 0; i < _inputfieldText.Length; i++)
        {
            _inputfieldText[i].interactable = false;
        }
    }
    /// <summary>
    /// ������ �ϰ� Ȯ���� �ϸ� ����Ʈ �ʱ�ȭ �� ��ǲ�ʵ� �ʱ�ȭ
    /// </summary>
    void OnCheck()
    {
        foreach (var list in _basketSlots)
        {
            Destroy(list.gameObject);
        }

        _uibasketSlotsDataList.Clear();
        _basketSlots.Clear();
        TotalBasketCount();

        for (int i = 0; i < _inputfieldText.Length; i++)
        {
            _inputfieldText[i].text = string.Empty;
        }

        _payButton.interactable = true;
        _backButtonManager._payCanvasExitButton.interactable = true;
        
        for (int i = 0; i < _inputfieldText.Length; i++)
        {
            _inputfieldText[i].interactable = true;
        }
    }
    /// <summary>
    /// ��ٱ��� ����Ʈ�� ���
    /// </summary>
    /// <param name="furnitureSpec"></param>
    public void AddFurnitureBasket(FurnitureSpec furnitureSpec)
    {
        bool found = false;
        //����Ʈ�� ���� �׸��� ������ ����
        for (int i = 0; i < _uibasketSlotsDataList.Count; i++)
        {
            if (_uibasketSlotsDataList[i].furnitureIndex == furnitureSpec.Index)
            {
                found = true;
                _uibasketSlotsDataList[i].furnitureCount++;
                break;
            }
        }
        //����Ʈ�� ���ٸ� �ε��� �߰� ������ 1
        if (!found)
        {
            //_basketSlotsDataList �� �߰�
            UI_FurnitureSlotData slot = new UI_FurnitureSlotData();
            slot.furnitureIndex = furnitureSpec.Index;
            slot.furnitureCount = 1;
            _uibasketSlotsDataList.Add(slot);

            //_basketSlots ������ ���� �� ����Ʈ �߰�
            GameObject instantiantePrefabs = Instantiate(_basketUIPrefab, _basketContent);
            UI_BasketSlot newSlot = instantiantePrefabs.AddComponent<UI_BasketSlot>();
            newSlot._index = furnitureSpec.Index;
            newSlot._count = 1;
            _basketSlots.Add(newSlot);
        }
        TotalBasketCount();
        CheckBasket();
    }
    /// <summary>
    /// ��ٱ��� UI ������Ʈ �Լ�
    /// </summary>
    /// <param name="basketData"></param>
    public void UpdateBasketUI()
    {
        for (int i = 0; i < _uibasketSlotsDataList.Count; i++)
        {
            UI_FurnitureSlotData slotData = _uibasketSlotsDataList[i];
            FurnitureSpec furnitureSpec = _uifurnitureSpecRepository.Get(slotData.furnitureIndex);

            _basketSlots[i].FurnitureImage = furnitureSpec.Sprite;
            _basketSlots[i].FurnitureName = furnitureSpec.Name;
            _basketSlots[i].FurnitureCount = slotData.furnitureCount;
            _basketSlots[i].FurniturePrice = furnitureSpec.Price;
            _basketSlots[i].FurnitureURL = furnitureSpec.URL;
        }
        TotalPrice();
    }
    /// <summary>
    /// ����Ʈ�� ��� ��ٱ��� ���� ����
    /// </summary>
    /// <param name="furnitureIndex"></param>
    /// <param name="furnitureCount"></param>
    public void UpdateBasketCount(int furnitureIndex, int furnitureCount)
    {
        for (int i = 0; i < _uibasketSlotsDataList.Count; i++)
        {
            if (_uibasketSlotsDataList[i].furnitureIndex == furnitureIndex)
            {
                _uibasketSlotsDataList[i].furnitureCount = furnitureCount;
                _basketSlots[i].FurnitureCount = furnitureCount;
                break;
            }   
        }
    }
    /// <summary>
    /// ��ٱ��� ���� �Լ�
    /// </summary>
    /// <param name="furnitureIndex"></param>
    public void DeleteBasket(int furnitureIndex)
    {
        for (int i = 0; i < _uibasketSlotsDataList.Count; i++)
        {
            if (_uibasketSlotsDataList[i].furnitureIndex == furnitureIndex)
            {
                _uibasketSlotsDataList.RemoveAt(i);

                Destroy(_basketSlots[i].gameObject);
                _basketSlots.RemoveAt(i);
                break;
            }
        }
        CheckBasket();
    }
    /// <summary>
    /// �� ���� �ؽ�Ʈ �Լ�
    /// </summary>
    public void TotalPrice()
    {
        int totalPrice = 0;
        foreach (var slotData in _uibasketSlotsDataList)
        {
            FurnitureSpec furnitureSpec = _uifurnitureSpecRepository.Get(slotData.furnitureIndex);
            totalPrice += furnitureSpec.Price * slotData.furnitureCount;
        }
        _totalPrice.text = $"�� ���� : {totalPrice:n0} �� ";
        _totalPriceButton.text = $"{totalPrice:n0}�� �����ϱ�";
        _payPopUpText.text = $"{totalPrice:n0}��\n������ ���������� ó���Ǿ����ϴ�.";
    }
    /// <summary>
    /// �� ��ٱ��� ���� �ؽ�Ʈ �Լ�
    /// </summary>
    public void TotalBasketCount()
    {
        int totalBasketCount = 0;
        foreach (var slotData in _uibasketSlotsDataList)
        {
            totalBasketCount += slotData.furnitureCount; 
        }
        if (totalBasketCount < 1)
        {
            _totalBasketImage.SetActive(false);
        }
        else
        {
            _totalBasketImage.SetActive(true);
            _totalBasketCount.text = totalBasketCount.ToString();
        }
    }
    /// <summary>
    /// ��ٱ��ϰ� ������� ��ư ��Ȱ��ȭ
    /// </summary>
    void CheckBasket()
    {
        if (_uibasketSlotsDataList.Count == 0)
        {
            _buyButton.interactable = false;
        }
        else
        {
            _buyButton.interactable = true;
        }
    }
    /// <summary>
    /// ��ǲ�ʵ尡 ������� Ȯ���ϰ� ������� ��Ȱ��ȭ
    /// </summary>
    /// <param name="value"></param>
    void CheckInputField(string value)
    {
        string text = _inputfieldText[1].text;
        char hypen = '-';
        int hypenCount = text.Count(c => c == hypen);

        if (text.Length == 4)
        {
            if (!text.Contains(hypen))
            {
                text = text.Insert(3, "-");
                _inputfieldText[1].SetTextWithoutNotify(text);
                _inputfieldText[1].caretPosition = 5;
            }
        }

        if (text.Length == 9)
        {
            if(hypenCount == 1)
            {
                text = text.Insert(8, "-");
                _inputfieldText[1].SetTextWithoutNotify(text);
                _inputfieldText[1].caretPosition = 10;
            }
        }

        _inputfieldText[1].SetTextWithoutNotify(text);

        foreach (var inputField in _inputfieldText)
        {
            if (string.IsNullOrEmpty(inputField.text))
            {
                _payButton.interactable = false;
                return;
            }
        }
        
        _payButton.interactable = true;
    }
}