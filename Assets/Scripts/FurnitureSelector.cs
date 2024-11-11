using System.Collections.Generic;
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

    GameObject obj;

    private void Start()
    {
        _tapStartPosition.action.started += OnTouch;
    }
    void Update()
    {
        //Debug.Log(Input.touchCount);
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray,out hit,100f,Furniture))
        //    {
        //        Debug.Log(hit.collider.name);
        //        if (hit.transform.CompareTag("Furniture"))  // AR 오브젝트 태그 확인
        //        {
        //            ActivateUIPanel(hit.transform.gameObject);
        //        }
        //    }
        //}

    }

    void OnTouch(InputAction.CallbackContext context)
    {
        Debug.Log("OnTouch Call");
        RaycastHit hit;
        Vector2 tapposition = context.ReadValue<Vector2>(); 
        if(Physics.Raycast(_xrCamera.ScreenPointToRay(tapposition),out hit, 100f, Furniture))
        {
            Debug.Log("Shot Ray");
            if(hit.collider.tag == "Furniture")
            {
                obj = hit.transform.gameObject;
                Debug.Log("Hit Furniture");
                ActivateUIPanel(obj);
                Debug.Log(hit.transform.gameObject.name);
            }
            else
            {
                DeActiveateUIPanel(hit.transform.gameObject);
            }
        }
    }

    void ActivateUIPanel(GameObject selectedObject)
    {
        uiPanel.SetActive(true);
        uiPanel.GetComponent<UIPanelController>().SetTargetObject(selectedObject);
    }

    void DeActiveateUIPanel(GameObject gameObject)
    {
        uiPanel.SetActive(false);
    }
}