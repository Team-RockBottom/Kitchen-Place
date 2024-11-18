using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UI_FurnitureSlotDataModel;

public class UI_BasketController : MonoBehaviour
{
    #region
    [Header("BasketUI")]
    [SerializeField] GameObject _mainMenuCanvas;
    [SerializeField] UI_FurnitureSpecRepository _uifurnitureSpecRepository;
    [SerializeField] GameObject _basketCanvas;  //장바구니 캔버스
    [SerializeField] Button _basketButton;      //장바구니 버튼
    [SerializeField] Transform _basketContent;  //장바구니 스크롤뷰 부모 오브젝트
    [SerializeField] GameObject _basketUIPrefab;//장바구니 UI 프리팹 

    List<UI_BasketSlot> _basketSlots; //추가된 장바구니 슬롯 리스트
    public List<UI_FurnitureSlotData> _uibasketSlotsDataList; //장바구니 데이터

    [SerializeField] Text _totalPrice; //총 가격 텍스트

    #endregion
    private void Start()
    {
        _basketCanvas.SetActive(false);
        _basketButton.onClick.AddListener(OnBasket);
        _basketSlots = new List<UI_BasketSlot>();
        _uibasketSlotsDataList = new List<UI_FurnitureSlotData>();
    }
    /// <summary>
    /// 장바구니 UI 버튼
    /// </summary>
    void OnBasket()
    {
        _basketCanvas.SetActive(true);
        _mainMenuCanvas.SetActive(!_mainMenuCanvas.activeSelf);
        UpdateBasketUI();
    }
    /// <summary>
    /// 장바구니 리스트에 담기
    /// </summary>
    /// <param name="furnitureSpec"></param>
    public void AddFurnitureBasket(UI_FurnitureSpec furnitureSpec)
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
    }
    /// <summary>
    /// 장바구니 UI 업데이트 함수
    /// </summary>
    /// <param name="basketData"></param>
    void UpdateBasketUI()
    {
        for (int i = 0; i < _uibasketSlotsDataList.Count; i++)
        {
            UI_FurnitureSlotData slotData = _uibasketSlotsDataList[i];
            UI_FurnitureSpec furnitureSpec = _uifurnitureSpecRepository.Get(slotData.furnitureIndex);

            _basketSlots[i].FurnitureImage = furnitureSpec.Image;
            _basketSlots[i].FurnitureName = furnitureSpec.Name;
            _basketSlots[i].FurnitureCount = slotData.furnitureCount;
            _basketSlots[i].FurniturePrice = furnitureSpec.Price;
        }
        TotalPrice();
    }
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
    }
    /// <summary>
    /// 총 가격 텍스트 함수
    /// </summary>
    public void TotalPrice()
    {
        int totalPrice = 0;
        foreach (var slotData in _uibasketSlotsDataList)
        {
            UI_FurnitureSpec furnitureSpec = _uifurnitureSpecRepository.Get(slotData.furnitureIndex);
            totalPrice += furnitureSpec.Price * slotData.furnitureCount;
        }
        _totalPrice.text = $"Total Price : {totalPrice:n0} 원 ";
    }
}