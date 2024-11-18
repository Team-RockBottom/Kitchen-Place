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
    [SerializeField] GameObject _basketCanvas;  //��ٱ��� ĵ����
    [SerializeField] Button _basketButton;      //��ٱ��� ��ư
    [SerializeField] Transform _basketContent;  //��ٱ��� ��ũ�Ѻ� �θ� ������Ʈ
    [SerializeField] GameObject _basketUIPrefab;//��ٱ��� UI ������ 

    List<UI_BasketSlot> _basketSlots; //�߰��� ��ٱ��� ���� ����Ʈ
    public List<UI_FurnitureSlotData> _uibasketSlotsDataList; //��ٱ��� ������

    [SerializeField] Text _totalPrice; //�� ���� �ؽ�Ʈ

    #endregion
    private void Start()
    {
        _basketCanvas.SetActive(false);
        _basketButton.onClick.AddListener(OnBasket);
        _basketSlots = new List<UI_BasketSlot>();
        _uibasketSlotsDataList = new List<UI_FurnitureSlotData>();
    }
    /// <summary>
    /// ��ٱ��� UI ��ư
    /// </summary>
    void OnBasket()
    {
        _basketCanvas.SetActive(true);
        _mainMenuCanvas.SetActive(!_mainMenuCanvas.activeSelf);
        UpdateBasketUI();
    }
    /// <summary>
    /// ��ٱ��� ����Ʈ�� ���
    /// </summary>
    /// <param name="furnitureSpec"></param>
    public void AddFurnitureBasket(UI_FurnitureSpec furnitureSpec)
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
    }
    /// <summary>
    /// ��ٱ��� UI ������Ʈ �Լ�
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
    }
    /// <summary>
    /// �� ���� �ؽ�Ʈ �Լ�
    /// </summary>
    public void TotalPrice()
    {
        int totalPrice = 0;
        foreach (var slotData in _uibasketSlotsDataList)
        {
            UI_FurnitureSpec furnitureSpec = _uifurnitureSpecRepository.Get(slotData.furnitureIndex);
            totalPrice += furnitureSpec.Price * slotData.furnitureCount;
        }
        _totalPrice.text = $"Total Price : {totalPrice:n0} �� ";
    }
}