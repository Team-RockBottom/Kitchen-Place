using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MoveMode : MonoBehaviour
{
    [SerializeField] InputActionReference _inputActionReference;
    [SerializeField] LayerMask _furniture;
    ARRaycastManager _arraycastManager;
    Camera _arCamera;
    private GameObject _targetObject;
    private bool _isMoving = false;
    List<ARRaycastHit> _hits = new List<ARRaycastHit>();


    //private void Start()
    //{
    //    _inputActionReference.action.performed += OnMoveClicked;
    //}
    private void OnEnable()
    {
        Debug.Log("MoveMode OnEnable");
        _inputActionReference.action.performed += OnMoveClicked;
    }

    private void OnDisable()
    {
        Debug.Log("MoveMode OnDisable");
        _isMoving = false;
        _inputActionReference.action.performed -= OnMoveClicked;
    }
    public void Activate(GameObject obj)
    {   
        _targetObject = obj;
        _isMoving = true;
    }

    private void OnMoveClicked(InputAction.CallbackContext context)
    {
        Debug.Log("OnmoveClicked Call");
        Vector2 tapPosition = context.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(tapPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,100f,_furniture))
        {
            Debug.Log("Move ray shot");
            if(hit.transform.gameObject ==_targetObject)
            {
                Debug.Log("hit selected object");
                if (tapPosition != null && _isMoving)
                {
                    Debug.Log("move trasnfor position");
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(tapPosition.x, tapPosition.y, Camera.main.WorldToScreenPoint(_targetObject.transform.position).z));
                    _targetObject.transform.position = new Vector3(touchPosition.x, _targetObject.transform.position.y, touchPosition.z);
                }
            }
        }
    }
}
