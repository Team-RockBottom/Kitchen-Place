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
    [SerializeField] public GameObject _basketCanvas;  //장바구니 캔버스
    [SerializeField] Button _basketButton;      //장바구니 버튼
    [SerializeField] Transform _basketContent;  //장바구니 스크롤뷰 부모 오브젝트
    [SerializeField] GameObject _basketUIPrefab;//장바구니 UI 프리팹 
    [SerializeField] Text _totalPrice; //총 가격 텍스트
    [SerializeField] GameObject _totalBasketImage; //총 장바구니 게임오브젝트 
    [SerializeField] Text _totalBasketCount; //총 장바구니 담긴 갯수 텍스트
    [SerializeField] Button _buyButton; //구매하기 버튼
    [SerializeField] GameObject _payCanvas; //결제하기 캔버스
    [SerializeField] Text _totalPriceButton; //총 가격 결제하기 텍스트
    [SerializeField] public Button _payButton; //구매하기 버튼

    List<UI_BasketSlot> _basketSlots; //추가된 장바구니 슬롯 리스트
    public List<UI_FurnitureSlotData> _uibasketSlotsDataList; //장바구니 리스트 데이터

    [Header("PayPapUpUI")]
    [SerializeField] GameObject _payPopUpImage;     //결제 팝업 UI
    [SerializeField] Text _payPopUpText;            //결제 확인 버튼 텍스트
    [SerializeField] Button _checkButton;           //결제 확인 버튼
    [SerializeField] InputField[] _inputfieldText;  //인풋 필드 텍스트

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
    /// 장바구니 UI 버튼
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
    /// 구매버튼 클릭 함수
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
    /// 결제를 하고 확인을 하면 리스트 초기화 및 인풋필드 초기화
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
    /// 장바구니 리스트에 담기
    /// </summary>
    /// <param name="furnitureSpec"></param>
    public void AddFurnitureBasket(FurnitureSpec furnitureSpec)
    {
        bool found = false;
        //리스트에 같은 항목이 있으면 멈춤
        for (int i = 0; i < _uibasketSlotsDataList.Count; i++)
        {
            if (_uibasketSlotsDataList[i].furnitureIndex == furnitureSpec.Index)
            {
                found = true;
                _uibasketSlotsDataList[i].furnitureCount++;
                break;
            }
        }
        //리스트에 없다면 인덱스 추가 수량은 1
        if (!found)
        {
            //_basketSlotsDataList 에 추가
            UI_FurnitureSlotData slot = new UI_FurnitureSlotData();
            slot.furnitureIndex = furnitureSpec.Index;
            slot.furnitureCount = 1;
            _uibasketSlotsDataList.Add(slot);

            //_basketSlots 프리팹 생성 및 리스트 추가
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
    /// 장바구니 UI 업데이트 함수
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
    /// 리스트에 담긴 장바구니 갯수 갱신
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
    /// 장바구니 삭제 함수
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
    /// 총 가격 텍스트 함수
    /// </summary>
    public void TotalPrice()
    {
        int totalPrice = 0;
        foreach (var slotData in _uibasketSlotsDataList)
        {
            FurnitureSpec furnitureSpec = _uifurnitureSpecRepository.Get(slotData.furnitureIndex);
            totalPrice += furnitureSpec.Price * slotData.furnitureCount;
        }
        _totalPrice.text = $"총 가격 : {totalPrice:n0} 원 ";
        _totalPriceButton.text = $"{totalPrice:n0}원 결제하기";
        _payPopUpText.text = $"{totalPrice:n0}원\n결제가 정상적으로 처리되었습니다.";
    }
    /// <summary>
    /// 총 장바구니 갯수 텍스트 함수
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
    /// 장바구니가 비었으면 버튼 비활성화
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
    /// 인풋필드가 비었는지 확인하고 비었음녀 비활성화
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