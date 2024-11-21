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
    [SerializeField] public GameObject _basketCanvas;  //��ٱ��� ĵ����
    [SerializeField] Button _basketButton;      //��ٱ��� ��ư
    [SerializeField] Transform _basketContent;  //��ٱ��� ��ũ�Ѻ� �θ� ������Ʈ
    [SerializeField] GameObject _basketUIPrefab;//��ٱ��� UI ������ 
    [SerializeField] Text _totalPrice; //�� ���� �ؽ�Ʈ
    [SerializeField] GameObject _totalBasketImage; //�� ��ٱ��� ���ӿ�����Ʈ 
    [SerializeField] Text _totalBasketCount; //�� ��ٱ��� ��� ���� �ؽ�Ʈ

    List<UI_BasketSlot> _basketSlots; //�߰��� ��ٱ��� ���� ����Ʈ
    public List<UI_FurnitureSlotData> _uibasketSlotsDataList; //��ٱ��� ����Ʈ ������

    [Header("SaveSystem")]
    string _basketDataPath; //��ٱ��� ������ ���� ���
    #endregion
    private void Awake()
    {
        //��ٱ��� ������ ���� ��� ����
        _basketDataPath = Application.persistentDataPath + "/BasketData.json";
    }
    private void Start()
    {
        _basketCanvas.SetActive(false);
        _basketButton.onClick.AddListener(OnBasket);
        _basketSlots = new List<UI_BasketSlot>();
        _uibasketSlotsDataList = new List<UI_FurnitureSlotData>();
        _totalBasketImage.SetActive(false);
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
        _totalPrice.text = $"Total Price : {totalPrice:n0} �� ";
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
}