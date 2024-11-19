using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RotateMode : MonoBehaviour
{
    [SerializeField] Slider rotationSlider;
    [SerializeField] TMP_Text _rotationSliderValueText;
    private GameObject targetObject;

    public void Activate(GameObject obj)
    {
        targetObject = obj;
        rotationSlider.gameObject.SetActive(true);
        rotationSlider.onValueChanged.AddListener(UpdateRotation);
        rotationSlider.value = obj.transform.localEulerAngles.y;
        rotationSlider.minValue = 0;
        rotationSlider.maxValue = 360;
    }

    private void UpdateRotation(float value)
    {
        value = Mathf.Clamp(rotationSlider.value,rotationSlider.minValue,rotationSlider.maxValue);
        if (targetObject != null)
        {
            targetObject.transform.rotation = Quaternion.Euler(0, value, 0);
            _rotationSliderValueText.text = $"{value : ###}";
        }
    }

    private void OnDisable()
    {
        rotationSlider.onValueChanged.RemoveListener(UpdateRotation);
    }
}