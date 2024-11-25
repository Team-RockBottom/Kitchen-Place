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
    [SerializeField] ARRaycastManager _arraycastManager;
    [SerializeField] Camera _arCamera;
    private GameObject _targetObject;
    private bool _isMoving = false;
    List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private void OnEnable()
    {
        _inputActionReference.action.performed += OnMoveClicked;
    }

    private void OnDisable()
    {
        _isMoving = false;
        _inputActionReference.action.performed -= OnMoveClicked;
    }
    public void Activate(GameObject obj)
    {   
        _targetObject = obj;
        _isMoving = true;
        _targetObject.GetComponent<Outline>().enabled = true;
    }

    private void OnMoveClicked(InputAction.CallbackContext context)
    {
        Vector2 tapPosition = context.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(tapPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,100f,_furniture))
        {
            if(hit.transform.gameObject ==_targetObject)
            {
                if (tapPosition != null && _isMoving)
                {
                    if (_arraycastManager.Raycast(ray, _hits, TrackableType.Planes))
                    {
                        if (_hits[0].hitType == TrackableType.Planes)
                        {
                            _targetObject.transform.position = new Vector3(_hits[0].pose.position.x, _targetObject.transform.position.y, _hits[0].pose.position.z);
                        }
                    }
                }
            }
        }
    }
}
