using UnityEngine;
using UnityEngine.UI;

public class ScaleMode : MonoBehaviour
{
    [SerializeField] Slider _scaleSlider;
    [SerializeField] Button _xButton;
    [SerializeField] Button _zButton;

    private GameObject _targetObject;
    private string _scaleAxis = "x";

    private float _sclaexValue;

    public void Activate(GameObject obj)
    {
        _targetObject = obj;
        _scaleSlider.gameObject.SetActive(true);
        _xButton.gameObject.SetActive(true);
        _zButton.gameObject.SetActive(true);
        _xButton.onClick.AddListener(() => SetScaleAxis("x"));
        _zButton.onClick.AddListener(() => SetScaleAxis("z"));
        _scaleSlider.onValueChanged.AddListener(UpdateScale);
    }

    private void SetScaleAxis(string axis)
    {
        _scaleSlider.minValue = 0.5f;
        _scaleSlider.maxValue = 2f;
        _scaleAxis = axis;
        if (axis == "x")
            _scaleSlider.value = _targetObject.transform.localScale.x;

        if (axis == "z")
            _scaleSlider.value = _targetObject.transform.localScale.z;
    }

    private void UpdateScale(float value)
    {
        Debug.Log(_scaleSlider.value);
        value = Mathf.Clamp(value,_scaleSlider.minValue,_scaleSlider.maxValue);
        if (_targetObject != null)
        {
            Vector3 newScale = _targetObject.transform.localScale;
            if (_scaleAxis == "x")
            {
                newScale.x = value;
                //_scaleSlider.value = _targetObject.transform.localScale.x;
            }

            else if (_scaleAxis == "z")
            {
                newScale.z = value;
                //_scaleSlider.value = _targetObject.transform.localScale.z;
            }

            _targetObject.transform.localScale = newScale;
        }
    }

    private void OnDisable()
    {
        _scaleSlider.onValueChanged.RemoveListener(UpdateScale);
        _xButton.onClick.RemoveListener(() => SetScaleAxis("x"));
        _zButton.onClick.RemoveListener(() => SetScaleAxis("z"));
    }
}
