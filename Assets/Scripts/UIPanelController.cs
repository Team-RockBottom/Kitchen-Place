using UnityEngine;
using static UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor;

public class UIPanelController : MonoBehaviour
{
    private GameObject targetObject;

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
        GetComponent<RotateMode>().Activate(targetObject);
    }

    public void OnScaleButtonClicked()
    {
        GetComponent<ScaleMode>().Activate(targetObject);
    }
}
