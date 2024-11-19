using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


public class FurnitureSelector : MonoBehaviour
{
    [SerializeField] GameObject uiPanel;  // ������, ȸ��, ũ�� ���� UI �г�
    [SerializeField] InputActionReference _tapStartPosition;
    [SerializeField] ARRaycastManager _raycastManager;
    [SerializeField] Camera _xrCamera;
    [SerializeField] GameObject _gameSpawnUI;
    [SerializeField] Button _furnitureSpawnButton;
    [SerializeField] GameObject _furnitureSpawnUI;
    [SerializeField] GameObject _mainMenuCanvas;
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
        _furnitureSpawnButton.onClick.AddListener(FurnitureSpawnUIOnOff);
        _furnitureSpawnUI.SetActive(false);
    }

    void OnTouch(InputAction.CallbackContext context)
    {
        Debug.Log("OnTouch Call");
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

        _gameSpawnUI.SetActive(false);
        uiPanel.GetComponent<UIPanelController>().SetTargetObject(selectedObject);
    }

    void DeActiveateUIPanel()
    {
        uiPanel.SetActive(false);
    }

    void FurnitureSpawnUIOnOff()
    {
        _mainMenuCanvas.SetActive(false);
        _furnitureSpawnUI.SetActive(!_furnitureSpawnUI.activeSelf);
    }
}