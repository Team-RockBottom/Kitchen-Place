using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class FurnitureSelector : MonoBehaviour
{
    [SerializeField] GameObject uiPanel;  // 움직임, 회전, 크기 조절 UI 패널
    [SerializeField] InputActionReference _tapStartPosition;
    [SerializeField] ARRaycastManager _raycastManager;
    [SerializeField] Camera _xrCamera;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    [SerializeField] LayerMask Furniture;
    //private bool isSelected = false;
    private GameObject obj;

    public enum UIState
    {
        Normal,
        ObjectSelect,
        TransformChange
    }
    UIState currentUIState;


    private void Start()
    {
        _tapStartPosition.action.started += OnTouch;
    }

    void OnTouch(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        Vector2 tapposition = context.ReadValue<Vector2>(); 
        if(Physics.Raycast(_xrCamera.ScreenPointToRay(tapposition),out hit, 100f, Furniture))
        {
            if (hit.collider.tag == "Furniture")
            {
                obj = hit.transform.gameObject;
                Debug.Log("Hit Furniture");
                ActivateUIPanel(obj);
                Debug.Log("ActivateUIPanel");
                Debug.Log(hit.transform.gameObject.name);
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material material = renderer.material;
                    Color color = material.color;
                }
                Debug.Log("end ray");
            }
            else
                Debug.Log("else");
        }
        
    }

    void ActivateUIPanel(GameObject selectedObject)
    {
        Debug.Log("ActivateUIPanel");
        uiPanel.SetActive(true);
        uiPanel.GetComponent<UIPanelController>().SetTargetObject(selectedObject);
    }

    void DeActiveateUIPanel()
    {
        uiPanel.SetActive(false);
    }
}