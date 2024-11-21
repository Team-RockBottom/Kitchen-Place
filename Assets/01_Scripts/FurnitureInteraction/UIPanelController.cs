using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor;
public class UIPanelController : MonoBehaviour
{
    private GameObject _targetObject;
    [SerializeField] Button _moveModeButton;
    [SerializeField] Button _rotateModeButton;
    [SerializeField] Button _scaleModeButton;
    [SerializeField] Button _DeletedButton;
    [SerializeField] GameObject _modeSelectedCanvas;
    [SerializeField] GameObject _moveModeCanvas;
    [SerializeField] GameObject _rotateModeCanvas;
    [SerializeField] GameObject _scaleModeCanvas;
    [SerializeField] GameObject _mainMenuCanvas;
    FurnitureSelector _furnitureSelector;
    [SerializeField] UI_BasketController _basketController;
    private int _furnitureIndex;
    

    private void Start()
    {
        _furnitureSelector = GetComponentInParent<FurnitureSelector>();
        _moveModeButton.onClick.AddListener(OnMoveButtonClicked);
        _rotateModeButton.onClick.AddListener(OnRotateButtonClicked);
        _scaleModeButton.onClick.AddListener(OnScaleButtonClicked);
        _DeletedButton.onClick.AddListener(OnFurnitureDeletedButton);
    }

    public void SetTargetObject(GameObject obj)
    {
        _targetObject = obj;
        _furnitureIndex = _targetObject.GetComponent<FurnitureObject>().Spec.Index;
    }

    private void OnMoveButtonClicked()
    {
        _moveModeCanvas.SetActive(true);
        _moveModeCanvas.GetComponent<MoveMode>().Activate(_targetObject);
        _modeSelectedCanvas.SetActive(false);
    }

    private void OnRotateButtonClicked()
    {
        _rotateModeCanvas.SetActive(true);
        _rotateModeCanvas.GetComponent<RotateMode>().Activate(_targetObject);
        _modeSelectedCanvas.SetActive(false);
    }

    private void OnScaleButtonClicked()
    {
        _scaleModeCanvas.SetActive(true);
        _scaleModeCanvas.GetComponent<ScaleMode>().Activate(_targetObject);
        _modeSelectedCanvas.SetActive(false);
    }

    private void OnFurnitureDeletedButton()
    {

        if (_basketController._uibasketSlotsDataList.Count > 0)
        {
            for (int i = 0; i < _basketController._uibasketSlotsDataList.Count; i++)
            {
                if (_basketController._uibasketSlotsDataList[i].furnitureIndex == _furnitureIndex)
                {
                    _basketController._uibasketSlotsDataList[i].furnitureCount--;
                    if (_basketController._uibasketSlotsDataList[i].furnitureCount <= 0)
                    {
                        _basketController.DeleteBasket(_furnitureIndex);
                    }
                }
            }
            //_basketController._uibasketSlotsDataList[_furnitureIndex].furnitureCount--;
            //if (_basketController._uibasketSlotsDataList[_furnitureIndex].furnitureCount == 0)
            //{
            //    _basketController.DeleteBasket(_furnitureIndex);
            //}
        }
        else
            _basketController.DeleteBasket(_furnitureIndex);

        Destroy(_targetObject);
        _modeSelectedCanvas.SetActive(false);
        _mainMenuCanvas.SetActive(true);
    }
}
