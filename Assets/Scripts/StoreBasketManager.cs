using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    #region
    [SerializeField] Button _shopButton;     //���� ��ư
    [SerializeField] Button _basketButton;   //��ٱ��� ��ư

    [Header("ShopUI")]
    [SerializeField] GameObject[] _shopCanvasArray; //���� ĵ���� �迭
    [SerializeField] Button[] _furnitureButtonArray;//���� ��ư �迭
    [SerializeField] Button[] _previousButtonArray; //���� ��ư �迭
    [SerializeField] Button[] _nextButtonArray;     //���� ��ư �迭
    [SerializeField] Text[] _pageCountArray;        //���� ������ �ؽ�Ʈ �迭
    private int pageCount = 0;                      //���� ������ �ؽ�Ʈ �ε���

    [Header("FurnitureDataUI")]
    [SerializeField] GameObject _furnitureDataCanvas;       //���� ���� ĵ����
    [SerializeField] FurnitureData[] _furnitureDataArray;   //���� ������ �迭
    [SerializeField] Image furnitureImage;                  //���� �̹���
    [SerializeField] Text furnitureNameText;                //���� �̸� �ؽ�Ʈ
    [SerializeField] Text furnitureDescriptionText;         //���� ����
    [SerializeField] Text furnitureSizeText;                //���� ������
    [SerializeField] Text furniturePriceText;               //���� ����

    [Header("BasketSystem")]
    [SerializeField] GameObject _basketCanvas;  //��ٱ��� ĵ����
    [SerializeField] GameObject _basketUIPrefabs; //��ٱ��� UI ������
    [SerializeField] Button _putInButton;       //��� ��ư

    private Dictionary<FurnitureData, int> _baseketFurniture = new Dictionary<FurnitureData, int>(); //��ٱ��� ����Ʈ 
    private FurnitureData _selectFurniture;     //���õ� ���� ����
    #endregion
    void Start()
    {
        _shopButton.onClick.AddListener(OnShop);
        _basketButton.onClick.AddListener(OnBasket);
        _furnitureDataCanvas.SetActive(false);
        _putInButton.onClick.AddListener(OnPutIn);
        
        for(int i = 0; i < _shopCanvasArray.Length; i++)
        {
            _shopCanvasArray[i].SetActive(false);
        }
        _basketCanvas.SetActive(false);
        for (int i = 0;i < _furnitureButtonArray.Length; i++)
        {
            int index = i;
            _furnitureButtonArray[i].onClick.AddListener(() => OnFurniture(index));
        }
        for (int i = 0;i < _nextButtonArray.Length; i++)
        {
            _nextButtonArray[i].onClick.AddListener(OnNext);
        }
        for (int i = 0; i < _previousButtonArray.Length; i++)
        {
            _previousButtonArray[i].onClick.AddListener(OnPrivious);
        }
    }
    private void Update()
    {
        PageCountUpdate();
    }
    void OnShop()
    {
        _shopCanvasArray[0].SetActive(true);
    }
    void OnBasket()
    {
        _basketCanvas.SetActive(true);
    }
    /// <summary>
    /// ���� ���� ��ư �Լ�
    /// </summary>
    /// <param name="index"></param>
    void OnFurniture(int index)
    {
        FurnitureInformation(index);
        _shopCanvasArray[pageCount].SetActive(false);
        _furnitureDataCanvas.SetActive(true);
    }
    /// <summary>
    /// ���� ���� ������ ��ư �Լ�
    /// </summary>
    void OnNext()
    {
        //���� ������ ��� ��Ȱ��ȭ 
        for (int i = 0; i < _shopCanvasArray.Length; i++)
        {
            _shopCanvasArray[i].SetActive(false);
        }
        //ī��Ʈ ����
        pageCount++;
        //ĵ���� �ε������� ũ�� �ִ밪 ����
        if (pageCount >= _shopCanvasArray.Length)
        {
            pageCount = _shopCanvasArray.Length - 1;
        }
        //���� ������ ī��Ʈ Ȱ��ȭ
        _shopCanvasArray[pageCount].SetActive(true);
    }
    /// <summary>
    /// ���� ���� ������ ��ư �Լ�
    /// </summary>
    void OnPrivious()
    {
        //���� ĵ���� ��� ��Ȱ��ȭ
        for (int i = 0; i < _shopCanvasArray.Length; i++)
        { 
            _shopCanvasArray[i].SetActive(false);
        }
        //ī��Ʈ ����
        pageCount--;
        //0���ϸ� 0����
        if (pageCount < 0)
        {
            pageCount = 0;
        }
        //���� ������ ī��Ʈ Ȱ��ȭ 
        _shopCanvasArray[pageCount].SetActive(true);
    }
    /// <summary>
    /// ��ٱ��� ��� ��ư �Լ�
    /// </summary>
    void OnPutIn()
    {
        Debug.Log("Put In");
        //��ųʸ��� �̹� ����ִٸ� ���� ����
        if (!_baseketFurniture.ContainsKey((_selectFurniture)))
        {
            //��ųʸ��� ���� ���õ� ���� �߰�
            _baseketFurniture.ContainsKey((_selectFurniture));
            Debug.Log("Furniture Add : " + _selectFurniture.Name);
        }
        //��ųʸ��� ����ִٸ� ���� ����
        else if (_baseketFurniture.ContainsKey((_selectFurniture)))
        {
            _baseketFurniture[_selectFurniture]++;
        }
    }   
    /// <summary>
    /// ���� ������ �ؽ�Ʈ �Լ�
    /// </summary>
    void PageCountUpdate()
    {
        _pageCountArray[pageCount].text = (pageCount + 1) + " / " + _shopCanvasArray.Length; 
    }
    /// <summary>
    /// ���� ���� �����͸� �ҷ����� �Լ�
    /// </summary>
    /// <param name="index"> �ҷ��� ���������� �ε���</param>
    void FurnitureInformation(int index)
    {
        _selectFurniture = _furnitureDataArray[index];
        furnitureImage.sprite = _selectFurniture.Image;
        furnitureNameText.text = _selectFurniture.Name;
        furnitureDescriptionText.text = _selectFurniture.Description;
        furnitureSizeText.text = $"�԰� : W: {_selectFurniture.Size.x}, D: {_selectFurniture.Size.z}, H: {_selectFurniture.Size.y}";
        furniturePriceText.text = $"���� : {_selectFurniture.Price: ###,###,###} ��";
    }
    /// <summary>
    /// ��ٱ��� UI ������ �ʱ�ȭ
    /// </summary>
    void BasketUILoad()
    {
        foreach (var item in _baseketFurniture)
        {

        }
    }
}