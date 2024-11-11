using UnityEngine;
using UnityEngine.UI;

public class RotateMode : MonoBehaviour
{
    public Slider rotationSlider;
    private GameObject targetObject;

    public void Activate(GameObject obj)
    {
        targetObject = obj;
        rotationSlider.gameObject.SetActive(true);
        rotationSlider.onValueChanged.AddListener(UpdateRotation);
    }

    private void UpdateRotation(float value)
    {
        if (targetObject != null)
            targetObject.transform.rotation = Quaternion.Euler(0, value, 0);
    }

    private void OnDisable()
    {
        rotationSlider.onValueChanged.RemoveListener(UpdateRotation);
    }
}