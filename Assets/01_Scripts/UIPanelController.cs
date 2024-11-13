using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor;

public class UIPanelController : MonoBehaviour
{
    private GameObject targetObject;
    [SerializeField] Button _moveModeButton;
    [SerializeField] Button _rotateModeButton;
    [SerializeField] Button _scaleModeButton;
    [SerializeField] GameObject _modeSelectedCanvas;
    [SerializeField] GameObject _moveModeCanvas;
    [SerializeField] GameObject _rotateModeCanvas;
    [SerializeField] GameObject _scaleModeCanvas;

    private void Start()
    {
        _moveModeButton.onClick.AddListener(OnMoveButtonClicked);
        _rotateModeButton.onClick.AddListener(OnRotateButtonClicked);
        _scaleModeButton.onClick.AddListener(OnScaleButtonClicked);
    }

    public void SetTargetObject(GameObject obj)
    {
        targetObject = obj;
    }

    public void OnMoveButtonClicked()
    {
        _moveModeCanvas.SetActive(true);
        _moveModeCanvas.GetComponent<MoveMode>().Activate(targetObject);
        _modeSelectedCanvas.SetActive(false);
    }

    public void OnRotateButtonClicked()
    {
        _rotateModeCanvas.SetActive(true);
        _rotateModeCanvas.GetComponent<RotateMode>().Activate(targetObject);
        _modeSelectedCanvas.SetActive(false);
    }

    public void OnScaleButtonClicked()
    {
        _scaleModeCanvas.SetActive(true);
        _scaleModeCanvas.GetComponent<ScaleMode>().Activate(targetObject);
        _modeSelectedCanvas.SetActive(false);
    }
}
