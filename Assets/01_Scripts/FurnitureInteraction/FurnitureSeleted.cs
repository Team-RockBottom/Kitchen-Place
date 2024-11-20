using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


public class FurnitureSelected : MonoBehaviour
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
    [SerializeField] GraphicRaycaster _graphicRaycaster;
    private PointerEventData _pointerEventData;

    private void Start()
    {
        _furnitureSpawnButton.onClick.AddListener(FurnitureSpawnUIOnOff);
        _pointerEventData = new PointerEventData(EventSystem.current);
    }

    private void OnEnable()
    {
        _tapStartPosition.action.started += OnTouch;
    }

    private void OnDisable()
    {
        _tapStartPosition.action.started -= OnTouch;
    }
    void OnTouch(InputAction.CallbackContext context)
    {
        Debug.Log("OnTouch Call");
        RaycastHit hit;
        Vector2 tapposition = context.ReadValue<Vector2>();
        _pointerEventData.position = tapposition;
        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(_pointerEventData, results);


        if (true)
        {
            Debug.Log("if true");
            if (results.Count > 0)
            {
                Debug.Log("results.count > 0");
                return;
            }
            else
            {
                Debug.Log("results.count > 0 else");
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
        }
        else
        {

        }
    }

    void ActivateUIPanel(GameObject selectedObject)
    {
        uiPanel.SetActive(true);
        _gameSpawnUI.SetActive(false);
        uiPanel.GetComponent<UIPanelController>().SetTargetObject(selectedObject);
        _mainMenuCanvas.SetActive(false);
    }

    void DeActiveateUIPanel()
    {
        uiPanel.SetActive(false);
    }
    void FurnitureSpawnUIOnOff()
    {
        _mainMenuCanvas.SetActive(false);
        _furnitureSpawnUI.SetActive(true);
    }
    //public void FunitureInteraction()
    //{
    //    _isInteraction = !_isInteraction;
    //}
}