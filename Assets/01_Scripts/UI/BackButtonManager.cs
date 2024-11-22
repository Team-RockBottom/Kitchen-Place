using UnityEngine;
using UnityEngine.UI;

public class BackButtonManager : MonoBehaviour
{
    [SerializeField] Button _tryapplicationExitButton; //���ø����̼� ���� �õ� ��ư(�ش� ��ư�� ������ ��¥ �����Ұ����� Ȯ�� UI ����)
    [SerializeField] Button _applicationExitButton; //���ø����̼� ���� "Ȯ��" ��ư
    [SerializeField] Button _applicationExitCansleButton; //���ø����̼� ���� "���" ��ư
    [SerializeField] Button _modeSelectExitButton; //������Ʈ ������ ��� ����â���� �������� ������ ��ư
    [SerializeField] Button _moveModeExitButton; //MoveMode���� ��� ����â���� ������ ��ư
    [SerializeField] Button _rotateModeExitButton; //rotateMode���� ��� ����â���� ������ ��ư
    [SerializeField] Button _scaleModeExitButton; //scaleMode���� ��� ����â���� ������ ��ư
    [SerializeField] Button _shop1ExitButton;
    [SerializeField] Button _shop2ExitButton;
    [SerializeField] Button _basketExitButton;
    [SerializeField] public Button _furnitureDataExitButton;
    [SerializeField] Button _furnitureSpawnExitButton;
    [SerializeField] GameObject _tryExitCanvas; //���ø����̼� ���Ḧ �õ��ϸ� ������ Ȯ�� Canvas
    [SerializeField] GameObject _modeSelectCanvas; //��� ����â ĵ����
    [SerializeField] GameObject _moveModeCanvas;//MoveMode ĵ����
    [SerializeField] GameObject _rotateModeCanvas;//RotateMode ĵ����
    [SerializeField] GameObject _scaleModeCanvas;//ScaleMode ĵ����
    [SerializeField] GameObject _shop1;
    [SerializeField] GameObject _shop2;
    [SerializeField] GameObject _basket;
    [SerializeField] GameObject _furnitureData;
    [SerializeField] GameObject _furnitureSpawnUI;
    [SerializeField] GameObject _mainMenuCanvas;
    [SerializeField] FurnitureSelector _furnitureSelector;
    [SerializeField] UI_BasketController _uIBasketController;
    [SerializeField] UI_StoreController _uiStoreController;

    private GameObject _selectedFurniture;

    void Start()
    {
        _tryapplicationExitButton.onClick.AddListener(OnTryApplicationExitButton);
        _applicationExitButton.onClick.AddListener(OnApplicationExitButton);
        _applicationExitCansleButton.onClick.AddListener(OnCansleApplicationExit);
        _modeSelectExitButton.onClick.AddListener(OnModeSelectExitButton);
        _moveModeExitButton.onClick.AddListener(OnMoveModeExitButton);
        _rotateModeExitButton.onClick.AddListener(OnRotateModeExitButton);
        _scaleModeExitButton.onClick.AddListener(OnScaleModeExitButton);
        _shop1ExitButton.onClick.AddListener(OnExitShop1);
        _shop2ExitButton.onClick.AddListener(OnExitShop2);
        _basketExitButton.onClick.AddListener(OnBasketExit);
        _furnitureDataExitButton.onClick.AddListener(OnFurnitureDataExit);
        _furnitureSpawnExitButton.onClick.AddListener(OnFurnitureSpawnExit);
    }

    private void OnTryApplicationExitButton()
    {
        _tryExitCanvas.SetActive(true);
        _mainMenuCanvas.SetActive(false);
    }
    private void OnCansleApplicationExit()
    {
        _tryExitCanvas.SetActive(false);
        _mainMenuCanvas.SetActive(true);
        _uIBasketController.TotalBasketCount();
    }
    private void OnApplicationExitButton()
    {
        Application.Quit();
    }
    //========================================
    private void OnFurnitureSpawnExit()
    {
        _furnitureSpawnUI.SetActive(false);
        _mainMenuCanvas.SetActive(true);
        _uIBasketController.TotalBasketCount();
    }
    private void OnModeSelectExitButton()
    {
        _furnitureSpawnUI.SetActive(true);
        _modeSelectCanvas.SetActive(false);
    }
    private void OnMoveModeExitButton()
    {
        _modeSelectCanvas.SetActive(true);
        _moveModeCanvas.SetActive(false);
    }
    private void OnRotateModeExitButton()
    {
        _modeSelectCanvas.SetActive(true);
        _rotateModeCanvas.SetActive(false);
    }
    private void OnScaleModeExitButton()
    {
        _modeSelectCanvas.SetActive(true);
        _scaleModeCanvas.SetActive(false);
    }
    private void OnExitShop1()
    {
        _shop1.SetActive(false);
        _mainMenuCanvas.SetActive(true) ;
    }
    private void OnExitShop2()
    {
        _shop2.SetActive(false);
        _mainMenuCanvas.SetActive(true);
        _uIBasketController.TotalBasketCount();
    }
    private void OnBasketExit()
    {
        _basket.SetActive(false);
        _mainMenuCanvas.SetActive(true);
        _uIBasketController.TotalBasketCount();
    }
    private void OnFurnitureDataExit()
    {
        _furnitureData.SetActive(false);
        _uiStoreController._shopCanvasArray[_uiStoreController.pageCount].SetActive(true);
    }
}
