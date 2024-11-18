using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static FurnitureSlotDataModel;

public class BasketController : MonoBehaviour
{
    #region
    [Header("BasketUI")]
    [SerializeField] FurnitureSpecRepository _furnitureSpecRepository;
    [SerializeField] GameObject _basketCanvas;  //장바구니 캔버스
    [SerializeField] Button _basketButton;      //장바구니 버튼
    [SerializeField] Transform _basketContent;  //장바구니 스크롤뷰 부모 오브젝트
    [SerializeField] GameObject _basketUIPrefab;//장바구니 UI 프리팹 

    List<BasketSlot> _basketSlots; //추가된 장바구니 슬롯 리스트
    public List<FurnitureSlotData> _basketSlotsDataList; //장바구니 데이터

    [SerializeField] Text _totalPrice; //총 가격 텍스트

    #endregion
    private void Start()
    {
        _basketCanvas.SetActive(false);
        _basketButton.onClick.AddListener(OnBasket);
        _basketSlots = new List<BasketSlot>();
        _basketSlotsDataList = new List<FurnitureSlotData>();
    }
    /// <summary>
    /// 장바구니 UI 버튼
    /// </summary>
    void OnBasket()
    {
        _basketCanvas.SetActive(true);
        UpdateBasketUI();
    }
    /// <summary>
    /// 장바구니 리스트에 담기
    /// </summary>
    /// <param name="furnitureSpec"></param>
    public void AddFurnitureBasket(FurnitureSpec furnitureSpec)
    {
        bool found = false;
        //리스트에 같은 항목이 있으면 멈춤
        for (int i = 0; i < _basketSlotsDataList.Count; i++)
        {
            if (_basketSlotsDataList[i].furnitureIndex == furnitureSpec.Index)
            {
                found = true;
                _basketSlotsDataList[i].furnitureCount++;
                break;
            }
        }
        //리스트에 없다면 인덱스 추가 수량은 1
        if (!found)
        {
            //_basketSlotsDataList 에 추가
            FurnitureSlotData slot = new FurnitureSlotData();
            slot.furnitureIndex = furnitureSpec.Index;
            slot.furnitureCount = 1;
            _basketSlotsDataList.Add(slot);

            //_basketSlots 프리팹 생성 및 리스트 추가
            GameObject instantiantePrefabs = Instantiate(_basketUIPrefab, _basketContent);
            BasketSlot newSlot = instantiantePrefabs.AddComponent<BasketSlot>();
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
        for (int i = 0; i < _basketSlotsDataList.Count; i++)
        {
            FurnitureSlotData slotData = _basketSlotsDataList[i];
            FurnitureSpec furnitureSpec = _furnitureSpecRepository.Get(slotData.furnitureIndex);

            _basketSlots[i].FurnitureImage = furnitureSpec.Image;
            _basketSlots[i].FurnitureName = furnitureSpec.Name;
            _basketSlots[i].FurnitureCount = slotData.furnitureCount;
            _basketSlots[i].FurniturePrice = furnitureSpec.Price;
        }
        TotalPrice();
    }
    public void UpdateBasketCount(int furnitureIndex, int furnitureCount)
    {
        for (int i = 0; i < _basketSlotsDataList.Count; i++)
        {
            if (_basketSlotsDataList[i].furnitureIndex == furnitureIndex)
            {
                _basketSlotsDataList[i].furnitureCount = furnitureCount;
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
        for (int i = 0; i < _basketSlotsDataList.Count; i++)
        {
            if (_basketSlotsDataList[i].furnitureIndex == furnitureIndex)
            {
                _basketSlotsDataList.RemoveAt(i);

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
        foreach (var slotData in _basketSlotsDataList)
        {
            FurnitureSpec furnitureSpec = _furnitureSpecRepository.Get(slotData.furnitureIndex);
            totalPrice += furnitureSpec.Price * slotData.furnitureCount;
        }
        _totalPrice.text = $"Total Price : {totalPrice:n0} 원 ";
    }
}