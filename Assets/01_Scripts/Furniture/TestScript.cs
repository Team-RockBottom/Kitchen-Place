using CP.Furniture;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

namespace CP.Furniture
{
    public class TestScript : MonoBehaviour
    {
        [SerializeField] ARRaycastManager _arRaycastManager;
        [SerializeField] Camera _xrCamera;
        [SerializeField] InputActionReference _tapStartPosition;
        [SerializeField] private FurnitureFactory _furnitureFactory;
        private List<ARRaycastHit> _hits = new List<ARRaycastHit>(2);

        private void Start()
        {
            //_furnitureFactory = new GameObject("FurnitureFactory").AddComponent<FurnitureFactory>();
            _tapStartPosition.action.started += OnTouch;
        }

        private void OnTouch(InputAction.CallbackContext context)
        {
            Vector2 tapPostion = context.ReadValue<Vector2>();

            if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(tapPostion), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
                {
                    float planeY = plane.gameObject.transform.position.y;
                    _furnitureFactory.CreateFurniture("Cube", _hits[0].pose.position, plane);


                }
            }
        }
    }
}


