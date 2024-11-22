using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RotateMode : MonoBehaviour
{
    [SerializeField] Slider rotationSlider;
    [SerializeField] TMP_Text _rotationSliderValueText;
    private GameObject _targetObject;

    public void Activate(GameObject obj)
    {
        _targetObject = obj;
        rotationSlider.gameObject.SetActive(true);
        rotationSlider.onValueChanged.AddListener(UpdateRotation);
        rotationSlider.minValue = 0;
        rotationSlider.maxValue = 360;
        rotationSlider.value = NormlizeAngle(obj.transform.eulerAngles.y);
        _targetObject.GetComponent<Outline>().enabled = true;
    }

    private void UpdateRotation(float value)
    {
        value = Mathf.Clamp(rotationSlider.value,rotationSlider.minValue,rotationSlider.maxValue);
        if (_targetObject != null)
        {
            _targetObject.transform.rotation = Quaternion.Euler(0, value, 0);
            _rotationSliderValueText.text = $"{value : ###}";
        }
    }

    private float NormlizeAngle(float angle)
    {
        if(angle < 0)
        {
            angle = 360f + angle;
        }
            return angle;
    }

    private void OnDisable()
    {
        rotationSlider.onValueChanged.RemoveListener(UpdateRotation);
    }
}