using UnityEngine;
using UnityEngine.UI;

public class UI_StoreController : MonoBehaviour
{
    #region
    [SerializeField] Button _shopButton;     //���� ��ư

    [Header("ShopUI")]
    [SerializeField] GameObject _mainMenuCanvas;    //���θ޴� ĵ����
    [SerializeField] GameObject[] _shopCanvasArray; //���� ĵ���� �迭
    [SerializeField] Button[] _furnitureButtonArray;//���� ��ư �迭
    [SerializeField] Button[] _previousButtonArray; //���� ��ư �迭
    [SerializeField] Button[] _nextButtonArray;     //���� ��ư �迭
    [SerializeField] Text[] _pageCountArray;        //���� ������ �ؽ�Ʈ �迭
    private int pageCount = 0;                      //���� ������ �ؽ�Ʈ �ε���

    [Header("FurnitureDataUI")]
    [SerializeField] GameObject _furnitureDataCanvas;       //���� ���� ĵ����
    [SerializeField] FurnitureSpec[] _furnitureDataArray;   //���� ������ �迭
    [SerializeField] Image furnitureImage;                  //���� �̹���
    [SerializeField] Text furnitureNameText;                //���� �̸� �ؽ�Ʈ
    [SerializeField] Text furnitureDescriptionText;         //���� ����
    [SerializeField] Text furnitureSizeText;                //���� ������
    [SerializeField] Text furniturePriceText;               //���� ����

    [Header("PopUpUI")]
    [SerializeField] GameObject _popUpImage;        //�˾� â ���ӿ�����Ʈ
    [SerializeField] Button _popUpShoppingButton;   //�˾� �����ϱ� ��ư
    [SerializeField] Button _popUpBasketButton;     //�˾� ��ٱ��� ��ư

    [SerializeField] BackButtonManager _backButtonManager;
    UI_BasketController _uibasketController;
    private FurnitureSpec _selectFurniture;     //���õ� ���� ����
    [SerializeField] Button _putInButton;       //��� ��ư
    [SerializeField] Button _urlButton;         //URL ��ư
    #endregion
    void Start()
    {
        _uibasketController = GetComponent<UI_BasketController>();
        _shopButton.onClick.AddListener(OnShop);
        _putInButton.onClick.AddListener(OnPutIn);
        _urlButton.onClick.AddListener(OnURL);
        _popUpShoppingButton.onClick.AddListener(OnPopUpDelete);
        _popUpBasketButton.onClick.AddListener(OnPopUpBasket);
        _popUpImage.SetActive(false);
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
        _popUpImage.gameObject.SetActive(true);
        _putInButton.GetComponent<Button>().interactable = false;
        _urlButton.GetComponent<Button>().interactable = false;
        _backButtonManager._furnitureDataExitButton.GetComponent<Button>().interactable = false;

    }
    /// <summary>
    /// ��ư URL �̵� �Լ� 
    /// </summary>
    public void OnURL()
    {
        OpenURL(_selectFurniture);
    }
    /// <summary>
    /// URL �̵� �Լ�
    /// </summary>
    /// <param name="furnitureSpec"></param>
    public void OpenURL(FurnitureSpec furnitureSpec)
    {
        if (furnitureSpec.URL == null)
        {
            Debug.Log("URL Not Found");
        }
        else
        {
            Application.OpenURL(furnitureSpec.URL);
        }
    }
    /// <summary>
    /// �˾� â �ݱ� ��ư �Լ�
    /// </summary>
    void OnPopUpDelete()
    {
        _popUpImage.gameObject.SetActive(false);
        _putInButton.GetComponent<Button>().interactable = true;
        _urlButton.GetComponent<Button>().interactable = true;
        _backButtonManager._furnitureDataExitButton.GetComponent<Button>().interactable = true;
    }
    /// <summary>
    /// �˾� â���� ��ٱ��� �̵� �� �Լ�
    /// </summary>
    void OnPopUpBasket()
    {
        _uibasketController._basketCanvas.SetActive(true);
        _popUpImage.SetActive(false);
        _furnitureDataCanvas.SetActive(false);
        _uibasketController.UpdateBasketUI();
        _putInButton.GetComponent<Button>().interactable = true;
        _urlButton.GetComponent<Button>().interactable = true;
        _backButtonManager._furnitureDataExitButton.GetComponent<Button>().interactable = true;
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
        furnitureImage.sprite = _selectFurniture.Sprite;
        furnitureNameText.text = _selectFurniture.Name;
        furnitureDescriptionText.text = _selectFurniture.Description;
        furnitureSizeText.text = $"�԰� : W: {_selectFurniture.Size.x}, D: {_selectFurniture.Size.z}, H: {_selectFurniture.Size.y}";
        furniturePriceText.text = $"���� : {_selectFurniture.Price:n0} ��";
    }
}