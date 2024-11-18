using UnityEngine;
using UnityEngine.UI;

public class UI_StoreController : MonoBehaviour
{
    #region
    [SerializeField] Button _shopButton;     //���� ��ư
    [SerializeField] GameObject _mainMenuCanvas;

    [Header("ShopUI")]
    [SerializeField] GameObject[] _shopCanvasArray; //���� ĵ���� �迭
    [SerializeField] Button[] _furnitureButtonArray;//���� ��ư �迭
    [SerializeField] Button[] _previousButtonArray; //���� ��ư �迭
    [SerializeField] Button[] _nextButtonArray;     //���� ��ư �迭
    [SerializeField] Text[] _pageCountArray;        //���� ������ �ؽ�Ʈ �迭
    private int pageCount = 0;                      //���� ������ �ؽ�Ʈ �ε���

    [Header("FurnitureDataUI")]
    [SerializeField] GameObject _furnitureDataCanvas;       //���� ���� ĵ����
    [SerializeField] UI_FurnitureSpec[] _furnitureDataArray;//���� ������ �迭
    [SerializeField] Image furnitureImage;                  //���� �̹���
    [SerializeField] Text furnitureNameText;                //���� �̸� �ؽ�Ʈ
    [SerializeField] Text furnitureDescriptionText;         //���� ����
    [SerializeField] Text furnitureSizeText;                //���� ������
    [SerializeField] Text furniturePriceText;               //���� ����

    UI_BasketController _uibasketController;
    private UI_FurnitureSpec _selectFurniture;     //���õ� ���� ����
    [SerializeField] Button _putInButton;       //��� ��ư
    #endregion
    void Start()
    {
        _uibasketController = GetComponent<UI_BasketController>();
        _shopButton.onClick.AddListener(OnShop);
        _putInButton.onClick.AddListener(OnPutIn);
        for (int i = 0;i < _nextButtonArray.Length; i++)
        {
            _nextButtonArray[i].onClick.AddListener(OnNext);
        }

        for (int i = 0; i < _previousButtonArray.Length; i++)
        {
            _previousButtonArray[i].onClick.AddListener(OnPrivious);
        }

        for (int i = 0;i < _furnitureButtonArray.Length; i++)
        {
            int index = i;
            _furnitureButtonArray[i].onClick.AddListener(() => OnFurniture(index));
        }

        _furnitureDataCanvas.SetActive(false);
        
        for(int i = 0; i < _shopCanvasArray.Length; i++)
        {
            _shopCanvasArray[i].SetActive(false);
        }
    }
    private void Update()
    {
        PageCountUpdate();
    }
    /// <summary>
    /// ���� UI ��ư �Լ�
    /// </summary>
    void OnShop()
    {
        _shopCanvasArray[0].SetActive(true);
        _mainMenuCanvas.SetActive(!_mainMenuCanvas.activeSelf);
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
    /// ��� ��ư �Լ�
    /// </summary>
    void OnPutIn()
    {
        _uibasketController.AddFurnitureBasket(_selectFurniture);
        /*foreach (var i in _basketController._basketSlotsDataList)
        {
            Debug.Log($"FurnitureIndex: {i.furnitureIndex}, FurnitureCount: {i.furnitureCount}");
        }*/
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
        furniturePriceText.text = $"���� : {_selectFurniture.Price:n0} ��";
    }
}