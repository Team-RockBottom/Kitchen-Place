using UnityEngine;
using UnityEngine.UI;

public class UI_StoreController : MonoBehaviour
{
    #region
    [SerializeField] Button _shopButton;     //상점 버튼
    [SerializeField] GameObject _mainMenuCanvas;

    [Header("ShopUI")]
    [SerializeField] GameObject[] _shopCanvasArray; //상점 캔버스 배열
    [SerializeField] Button[] _furnitureButtonArray;//가구 버튼 배열
    [SerializeField] Button[] _previousButtonArray; //이전 버튼 배열
    [SerializeField] Button[] _nextButtonArray;     //다음 버튼 배열
    [SerializeField] Text[] _pageCountArray;        //상점 페이지 텍스트 배열
    private int pageCount = 0;                      //상점 페이지 텍스트 인덱스

    [Header("FurnitureDataUI")]
    [SerializeField] GameObject _furnitureDataCanvas;       //가구 정보 캔버스
    [SerializeField] UI_FurnitureSpec[] _furnitureDataArray;//가구 데이터 배열
    [SerializeField] Image furnitureImage;                  //가구 이미지
    [SerializeField] Text furnitureNameText;                //가구 이름 텍스트
    [SerializeField] Text furnitureDescriptionText;         //가구 정보
    [SerializeField] Text furnitureSizeText;                //가구 사이즈
    [SerializeField] Text furniturePriceText;               //가구 가격

    UI_BasketController _uibasketController;
    private UI_FurnitureSpec _selectFurniture;     //선택된 가구 저장
    [SerializeField] Button _putInButton;       //담기 버튼
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
    /// 상점 UI 버튼 함수
    /// </summary>
    void OnShop()
    {
        _shopCanvasArray[0].SetActive(true);
        _mainMenuCanvas.SetActive(!_mainMenuCanvas.activeSelf);
    }
    /// <summary>
    /// 가구 정보 버튼 함수
    /// </summary>
    /// <param name="index"></param>
    void OnFurniture(int index)
    {
        FurnitureInformation(index);
        _shopCanvasArray[pageCount].SetActive(false);
        _furnitureDataCanvas.SetActive(true);
    }
    /// <summary>
    /// 상점 다음 페이지 버튼 함수
    /// </summary>
    void OnNext()
    {
        //상점 페이지 모두 비활성화 
        for (int i = 0; i < _shopCanvasArray.Length; i++)
        {
            _shopCanvasArray[i].SetActive(false);
        }
        //카운트 증가
        pageCount++;
        //캔버스 인덱스보다 크면 최대값 고정
        if (pageCount >= _shopCanvasArray.Length)
        {
            pageCount = _shopCanvasArray.Length - 1;
        }
        //상점 페이지 카운트 활성화
        _shopCanvasArray[pageCount].SetActive(true);
    }
    /// <summary>
    /// 상점 이전 페이지 버튼 함수
    /// </summary>
    void OnPrivious()
    {
        //상점 캔버스 모두 비활성화
        for (int i = 0; i < _shopCanvasArray.Length; i++)
        { 
            _shopCanvasArray[i].SetActive(false);
        }
        //카운트 감소
        pageCount--;
        //0이하면 0고정
        if (pageCount < 0)
        {
            pageCount = 0;
        }
        //상점 페이지 카운트 활성화 
        _shopCanvasArray[pageCount].SetActive(true);
    }
    /// <summary>
    /// 담기 버튼 함수
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
    /// 상점 페이지 텍스트 함수
    /// </summary>
    void PageCountUpdate()
    {
        _pageCountArray[pageCount].text = (pageCount + 1) + " / " + _shopCanvasArray.Length; 
    }
    /// <summary>
    /// 가구 정보 데이터를 불러오는 함수
    /// </summary>
    /// <param name="index"> 불러올 가구데이터 인덱스</param>
    void FurnitureInformation(int index)
    {
        _selectFurniture = _furnitureDataArray[index];
        furnitureImage.sprite = _selectFurniture.Image;
        furnitureNameText.text = _selectFurniture.Name;
        furnitureDescriptionText.text = _selectFurniture.Description;
        furnitureSizeText.text = $"규격 : W: {_selectFurniture.Size.x}, D: {_selectFurniture.Size.z}, H: {_selectFurniture.Size.y}";
        furniturePriceText.text = $"가격 : {_selectFurniture.Price:n0} 원";
    }
}