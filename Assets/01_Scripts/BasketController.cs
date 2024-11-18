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
    [SerializeField] GameObject _basketCanvas;  //��ٱ��� ĵ����
    [SerializeField] Button _basketButton;      //��ٱ��� ��ư
    [SerializeField] Transform _basketContent;  //��ٱ��� ��ũ�Ѻ� �θ� ������Ʈ
    [SerializeField] GameObject _basketUIPrefab;//��ٱ��� UI ������ 

    List<BasketSlot> _basketSlots; //�߰��� ��ٱ��� ���� ����Ʈ
    public List<FurnitureSlotData> _basketSlotsDataList; //��ٱ��� ������

    [SerializeField] Text _totalPrice; //�� ���� �ؽ�Ʈ

    #endregion
    private void Start()
    {
        _basketCanvas.SetActive(false);
        _basketButton.onClick.AddListener(OnBasket);
        _basketSlots = new List<BasketSlot>();
        _basketSlotsDataList = new List<FurnitureSlotData>();
    }
    /// <summary>
    /// ��ٱ��� UI ��ư
    /// </summary>
    void OnBasket()
    {
        _basketCanvas.SetActive(true);
        UpdateBasketUI();
    }
    /// <summary>
    /// ��ٱ��� ����Ʈ�� ���
    /// </summary>
    /// <param name="furnitureSpec"></param>
    public void AddFurnitureBasket(FurnitureSpec furnitureSpec)
    {
        bool found = false;
        //����Ʈ�� ���� �׸��� ������ ����
        for (int i = 0; i < _basketSlotsDataList.Count; i++)
        {
            if (_basketSlotsDataList[i].furnitureIndex == furnitureSpec.Index)
            {
                found = true;
                _basketSlotsDataList[i].furnitureCount++;
                break;
            }
        }
        //����Ʈ�� ���ٸ� �ε��� �߰� ������ 1
        if (!found)
        {
            //_basketSlotsDataList �� �߰�
            FurnitureSlotData slot = new FurnitureSlotData();
            slot.furnitureIndex = furnitureSpec.Index;
            slot.furnitureCount = 1;
            _basketSlotsDataList.Add(slot);

            //_basketSlots ������ ���� �� ����Ʈ �߰�
            GameObject instantiantePrefabs = Instantiate(_basketUIPrefab, _basketContent);
            BasketSlot newSlot = instantiantePrefabs.AddComponent<BasketSlot>();
            newSlot._index = furnitureSpec.Index;
            newSlot._count = 1;
            _basketSlots.Add(newSlot);
        }
    }
    /// <summary>
    /// ��ٱ��� UI ������Ʈ �Լ�
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
    /// ��ٱ��� ���� �Լ�
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
    /// �� ���� �ؽ�Ʈ �Լ�
    /// </summary>
    public void TotalPrice()
    {
        int totalPrice = 0;
        foreach (var slotData in _basketSlotsDataList)
        {
            FurnitureSpec furnitureSpec = _furnitureSpecRepository.Get(slotData.furnitureIndex);
            totalPrice += furnitureSpec.Price * slotData.furnitureCount;
        }
        _totalPrice.text = $"Total Price : {totalPrice:n0} �� ";
    }
}