using UnityEngine;
using UnityEngine.UI;

public class RotateMode : MonoBehaviour
{
    [SerializeField] Slider rotationSlider;
    private GameObject targetObject;

    public void Activate(GameObject obj)
    {
        targetObject = obj;
        rotationSlider.gameObject.SetActive(true);
        rotationSlider.onValueChanged.AddListener(UpdateRotation);
    }

    private void UpdateRotation(float value)
    {

        Debug.Log("UpdateRotation targetObject");
        if (targetObject != null)
        {
            Debug.Log("tartgetObject != null");
            targetObject.transform.rotation = Quaternion.Euler(0, value, 0);
        }
        else
        {
            Debug.Log("targetObject : NULL");
        }
    }

    private void OnDisable()
    {
        rotationSlider.onValueChanged.RemoveListener(UpdateRotation);
    }
}