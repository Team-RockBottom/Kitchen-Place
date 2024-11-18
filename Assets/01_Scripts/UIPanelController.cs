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

    private void Start()
    {
        _moveModeButton.onClick.AddListener(OnMoveButtonClicked);
        _rotateModeButton.onClick.AddListener(OnRotateButtonClicked);
        _scaleModeButton.onClick.AddListener(OnScaleButtonClicked);
        _DeletedButton.onClick.AddListener(OnFurnitureDeletedButton);
    }

    public void SetTargetObject(GameObject obj)
    {
        _targetObject = obj;
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
        Destroy(_targetObject);
        _modeSelectedCanvas.SetActive(false);
        _mainMenuCanvas.SetActive(true);
    }
}
