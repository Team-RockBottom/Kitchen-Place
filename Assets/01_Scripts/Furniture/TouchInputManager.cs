using CP.Furniture;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

namespace CP.Furniture
{
    public class TouchInputManager : MonoBehaviour
    {
        public static TouchInputManager instance;
        [SerializeField] ARRaycastManager _arRaycastManager;
        [SerializeField] Camera _xrCamera;
        [SerializeField] InputActionReference _tapStartPosition;
        [SerializeField] private FurnitureFactory _furnitureFactory; //팩토리 어떻게 해야 따로 안붙일수있으려나
        private List<ARRaycastHit> _hits = new List<ARRaycastHit>(2);
        private string _name; //불러올 프리펩 이름 UI에서 건들면 바뀌게 만들고싶음
        public LayerMask layerMask; //레이어 마스크 일반 레이캐스트로 인식
        private RaycastHit _hit; //레이캐스트 저장
        private bool isClick = false; //연속된 생성을 막는 bool
        private bool isSelecte = false; // 선택 상태
        private GameObject[] _objs = new GameObject[10]; //생성된 오브젝트를 저장
        private int _listIndex = 0; //인덱스

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            //_furnitureFactory = new GameObject("FurnitureFactory").AddComponent<FurnitureFactory>();
            _tapStartPosition.action.started += OnTouch;
            _tapStartPosition.action.performed += OffTouch; //마우스를 뗐을때 bool형을 false로
            

        }

        private void OnTouch(InputAction.CallbackContext context)
        {
            Vector2 tapPostion = context.ReadValue<Vector2>();
            
            if(Physics.Raycast(_xrCamera.ScreenPointToRay(tapPostion),out _hit,100f,layerMask)) //아직은 쓸모없는 일반 레이캐스트
            {
                if(_hit.transform.tag == "Return")
                {
                    return;
                }
            }
            if (isSelecte)
            {
                if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(tapPostion), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
                {
                    if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
                    {
                        if (plane.alignment != UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical) //평면인식기능으로 벽에서는 생성 안되게
                        {
                            if (!isClick)
                            {
                                _objs[_listIndex] = _furnitureFactory.CreateFurniture(_name, _hits[0].pose.position, plane); //AR레이캐스트로 생성
                                isClick = true;
                                isSelecte = false;
                            }
                            else
                            {
                                _objs[_listIndex].transform.position = _hits[0].pose.position; //true면 움직임
                            }

                        }
                    }
                }
            }
            
        }
        private void OffTouch(InputAction.CallbackContext context)
        {
            if(isClick)
            {
                Debug.Log($"OffTouch{_listIndex}");
                isClick = false;
                _listIndex++;
            }
            
            
        }

        public void SetName(string name)
        {
            _name = name;
            Debug.Log(_name);
        }
        
        public void TrueIsSelect()
        {
            isSelecte=true;
        }
        //private void Update() //터치로도 해볼까 한 흔적
        //{
        //    if (Input.touchCount > 0)
        //    {
        //        Touch touch = Input.touches[0];
        //        if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(touch.position),_hits,UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        //        {
        //            if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
        //            {
        //                if (plane.alignment != UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical)
        //                {
                           
        //                    _furnitureFactory.CreateFurniture("Cube", _hits[0].pose.position, plane);

        //                }                    
        //            }
        //        }
        //    }
        //}
    }
}


