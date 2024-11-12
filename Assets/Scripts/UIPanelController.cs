using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor;

public class UIPanelController : MonoBehaviour
{
    private GameObject targetObject;
    [SerializeField] Button _moveModeButton;
    [SerializeField] Button _rotateModeButton;
    [SerializeField] Button _scaleModeButton;
    [SerializeField] Button _backButton;
    [SerializeField] GameObject _modeSelectedCanvas;


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
        GetComponent<MoveMode>().Activate(targetObject);
        _modeSelectedCanvas.SetActive(false);
    }

    public void OnRotateButtonClicked()
    {
        GetComponent<RotateMode>().Activate(targetObject);
        _modeSelectedCanvas.SetActive(false);
    }

    public void OnScaleButtonClicked()
    {
        GetComponent<ScaleMode>().Activate(targetObject);
        _modeSelectedCanvas.SetActive(false);
    }
}
