using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScaleMode : MonoBehaviour
{
    [SerializeField] GameObject _noticePanel;
    [SerializeField] Slider _scaleSlider;
    [SerializeField] Button _xButton;
    [SerializeField] Button _zButton;
    [SerializeField] Button _check;
    [SerializeField] TMP_Text _xvalueText;
    [SerializeField] TMP_Text _zvalueText;

    private GameObject _targetObject;
    private string _scaleAxis = "x";
    private Vector3 _size;
    private float _sclaexValue;
    private bool _checkNoticePanel = false;

    public void Activate(GameObject obj)
    {
        if (!_checkNoticePanel)
        {
            _scaleSlider.enabled = false;
            _xButton.enabled = false;
            _zButton.enabled = false;
            _noticePanel.SetActive(true);
            _targetObject = obj;
            _check.onClick.AddListener(OnCheckButton);
        }
        else
        {
            _targetObject = obj;
            OnCheckButton();
        }

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
        _xvalueText.text = $"{_size.x * _targetObject.transform.localScale.x :W #,###}mm";
        _zvalueText.text = $"{_size.z * _targetObject.transform.localScale.z :D #,###}mm";
        value = Mathf.Clamp(value,_scaleSlider.minValue,_scaleSlider.maxValue);
        if (_targetObject != null)
        {
            Vector3 newScale = _targetObject.transform.localScale;
            if (_scaleAxis == "x")
            {
                newScale.x = value;
            }

            else if (_scaleAxis == "z")
            {
                newScale.z = value;
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
    private void OnCheckButton()
    {
        _scaleSlider.enabled = true;
        _xButton.enabled = true;
        _zButton.enabled = true;
        _checkNoticePanel = true;
        _noticePanel.SetActive(false);
        _scaleSlider.minValue = 0.5f;
        _scaleSlider.maxValue = 2f;
        _size = _targetObject.GetComponent<FurnitureObject>().Spec.Size;
        _xButton.onClick.AddListener(() => SetScaleAxis("x"));
        _zButton.onClick.AddListener(() => SetScaleAxis("z"));
        _scaleSlider.onValueChanged.AddListener(UpdateScale);
        if (_scaleAxis == "x")
        {
            _scaleSlider.value = _targetObject.transform.localScale.x;

        }
        else
            _scaleSlider.value = _targetObject.transform.localScale.z;

        _xvalueText.text = $"{_size.x * _targetObject.transform.localScale.x:W #,###}mm";
        _zvalueText.text = $"{_size.z * _targetObject.transform.localScale.z:D #,###}mm";
        _targetObject.GetComponent<Outline>().enabled = true;
    }
}
