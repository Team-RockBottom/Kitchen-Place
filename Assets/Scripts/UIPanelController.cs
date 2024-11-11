using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor;

public class UIPanelController : MonoBehaviour
{
    private GameObject targetObject;
    [SerializeField] Button _moveModeButton;
    [SerializeField] Button _rotateModeButton;
    [SerializeField] Button _ScaleModeButton;


    private void Awake()
    {
        _moveModeButton.onClick.AddListener(OnMoveButtonClicked);
        _rotateModeButton.onClick.AddListener(OnRotateButtonClicked);
        _ScaleModeButton.onClick.AddListener(OnScaleButtonClicked);
    }

    public void SetTargetObject(GameObject obj)
    {
        targetObject = obj;
    }

    public void OnMoveButtonClicked()
    {
        GetComponent<MoveMode>().Activate(targetObject);
    }

    public void OnRotateButtonClicked()
    {
        Debug.Log("RotateButtonClicked");
        Debug.Log(targetObject.transform.position);
        GetComponent<RotateMode>().Activate(targetObject);
    }

    public void OnScaleButtonClicked()
    {
        GetComponent<ScaleMode>().Activate(targetObject);
    }
}
