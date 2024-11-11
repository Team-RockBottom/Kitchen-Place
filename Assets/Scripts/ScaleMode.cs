using UnityEngine;
using UnityEngine.UI;

public class ScaleMode : MonoBehaviour
{
    [SerializeField] Slider scaleSlider;
    [SerializeField] Button xButton;
    [SerializeField] Button zButton;

    private GameObject targetObject;
    private string scaleAxis = "x";

    public void Activate(GameObject obj)
    {
        targetObject = obj;
        scaleSlider.gameObject.SetActive(true);
        xButton.gameObject.SetActive(true);
        zButton.gameObject.SetActive(true);
        xButton.onClick.AddListener(() => SetScaleAxis("x"));
        zButton.onClick.AddListener(() => SetScaleAxis("z"));
        scaleSlider.onValueChanged.AddListener(UpdateScale);
    }

    private void SetScaleAxis(string axis)
    {
        scaleAxis = axis;
    }

    private void UpdateScale(float value)
    {
        if (targetObject != null)
        {
            Vector3 newScale = targetObject.transform.localScale;
            if (scaleAxis == "x")
                newScale.x = value;
            else if (scaleAxis == "z")
                newScale.z = value;

            targetObject.transform.localScale = newScale;
        }
    }

    private void OnDisable()
    {
        scaleSlider.onValueChanged.RemoveListener(UpdateScale);
        xButton.onClick.RemoveListener(() => SetScaleAxis("x"));
        zButton.onClick.RemoveListener(() => SetScaleAxis("z"));
    }
}
