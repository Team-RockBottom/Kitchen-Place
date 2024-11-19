using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


public class FurnitureSelector : MonoBehaviour
{
    [SerializeField] GameObject uiPanel;  // 움직임, 회전, 크기 조절 UI 패널
    [SerializeField] InputActionReference _tapStartPosition;
    [SerializeField] ARRaycastManager _raycastManager;
    [SerializeField] Camera _xrCamera;
    [SerializeField] GameObject _gameSpawnUI;
    [SerializeField] Button _furnitureSpawnButton;
    [SerializeField] GameObject _furnitureSpawnUI;
    [SerializeField] GameObject _mainMenuCanvas;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    [SerializeField] LayerMask Furniture;
    private GameObject obj;
    private bool _isInteraction = false;

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
        RaycastHit hit;
        Vector2 tapposition = context.ReadValue<Vector2>(); 

        if (!_isInteraction)
        {
            if (Physics.Raycast(_xrCamera.ScreenPointToRay(tapposition), out hit, 100f, Furniture))
            {
                if (hit.collider.tag == "Furniture")
                {
                    obj = hit.transform.gameObject;
                    ActivateUIPanel(obj);
                    Debug.Log(hit.transform.gameObject.name);
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        Material material = renderer.material;
                        Color color = material.color;
                    }
                }
            }
        }
    }

    void ActivateUIPanel(GameObject selectedObject)
    {
        _mainMenuCanvas.SetActive(false);
        FunitureInteraction();
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
    public void FunitureInteraction()
    {
        _isInteraction = !_isInteraction;
    }
}