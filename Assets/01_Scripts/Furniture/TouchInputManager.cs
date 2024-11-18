using CP.Furniture;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace CP.Furniture
{
    public class TouchInputManager : MonoBehaviour
    {
        public static TouchInputManager instance;
        [SerializeField] ARRaycastManager _arRaycastManager;
        [SerializeField] Camera _xrCamera;
        [SerializeField] InputActionReference _tapStartPosition;
        [SerializeField] InputActionReference _dragCurrentPosition;
        private FurnitureFactory _furnitureFactory; //팩토리
        private List<ARRaycastHit> _hits = new List<ARRaycastHit>(2);
        [SerializeField] private LayerMask _layerMask; //레이어 마스크 일반 레이캐스트로 인식
        private RaycastHit _hit; //레이캐스트 저장
        private bool isSlot = false; //연속된 생성을 막는 bool
        private GameObject[] _objs = new GameObject[10]; //생성된 오브젝트를 저장
        private int _listIndex = 0; //인덱스
        [SerializeField] private GraphicRaycaster _graycast; //UI에 있는 그래픽 레이캐스트를 가져온다.
        private PointerEventData _ped; //그래픽 레이캐스트에 필요한 포인트이벤트 데이터
        private List<RaycastResult> _rrListStart; //레이캐스트 결과를 저장할 리스트
        private bool _isUI = false;
        private FurnitureSlot _selectedSlot;
        [SerializeField] private Image _previewImage;
        [SerializeField] private ScrollRect _scrollRect;
        private Vector2 _mousePosi;
        [SerializeField] private GameObject _previewPrefeb;
        

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
            if(_xrCamera == null)
            {
                _xrCamera = Camera.main;
            }
            _furnitureFactory = GetComponent<FurnitureFactory>();
            //_furnitureFactory = new GameObject("FurnitureFactory").AddComponent<FurnitureFactory>();
            _tapStartPosition.action.started += OnTouch;
            //_dragCurrentPosition.action.started += OnDrag;
            _dragCurrentPosition.action.canceled += OffTouch;
            
            _ped = new PointerEventData(EventSystem.current);
            _rrListStart = new List<RaycastResult>(5);
        }

        private void OnTouch(InputAction.CallbackContext context)
        {
            //Debug.Log(context.ReadValueAsButton());
            Debug.Log(context.phase);
            Vector2 tapPostion = context.ReadValue<Vector2>();
            _ped.position = tapPostion; // 레이캐스트 위치설정
            _rrListStart.Clear(); //리스트 클리어
            _graycast.Raycast(_ped, _rrListStart); //그래픽 레이캐스트의 결과를 리스트에 저장
            
            if(_selectedSlot)
            {
                // UI위에 있는 상태
                if (_rrListStart.Count > 0)
                {
                    if (_rrListStart[0].gameObject.TryGetComponent(out FurnitureSlot slot))
                    {
                        _selectedSlot = slot;
                        _previewImage.sprite = slot.furnitureIcon;
                        Debug.Log(slot.name);
                        _scrollRect.enabled = false;
                        isSlot = true;
                        _previewPrefeb = _furnitureFactory.CreatePreviewFurniture(slot.furnitureIndex);
                        _previewPrefeb.layer = 0;
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    
                    _selectedSlot = null;
                }
            }
            else
            {
                // UI위에 있는 상태
                if (_rrListStart.Count > 0)
                {
                    if (_rrListStart[0].gameObject.TryGetComponent(out FurnitureSlot slot))
                    {
                        _selectedSlot = slot;
                        _previewImage.sprite = slot.furnitureIcon;
                        Debug.Log(slot.name);
                        _scrollRect.enabled = false;
                        isSlot = true;
                        _previewPrefeb = _furnitureFactory.CreatePreviewFurniture(slot.furnitureIndex);
                        _previewPrefeb.layer = 0;

                    }
                }
                else
                {

                }
            }
        }
        private void OnDrag(InputAction.CallbackContext context)
        {
            
        }


        private void OffTouch(InputAction.CallbackContext context)
        {
            _scrollRect.enabled = true;
            isSlot = false;


            _ped.position = _mousePosi; // 레이캐스트 위치설정
            _rrListStart.Clear(); //리스트 클리어
            _graycast.Raycast(_ped, _rrListStart); //그래픽 레이캐스트의 결과를 리스트에 저장

            if (_rrListStart.Count > 1)
            {
                _previewImage.enabled=false;
                Destroy(_previewPrefeb);
                return;
            }

            if (Physics.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), out _hit, 100f, _layerMask)) //가구를 인식하고 설치를 할수 없게하는 레이캐스트
            {
                Debug.Log("일반 레이캐스트");

                _previewImage.enabled=false;
                Destroy(_previewPrefeb);

                return;
            }

            if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                Debug.Log("AR레이캐스트");
                if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
                {
                    Debug.Log("플레인 인식");
                    if (plane.alignment != UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical) //평면인식기능으로 벽에서는 생성 안되게
                    {
                        Debug.Log("평면 인식");
                        if (_selectedSlot)
                        {
                            Debug.Log("생성");
                            _previewImage.enabled = false;
                            Destroy(_previewPrefeb);
                            _objs[_listIndex] = _furnitureFactory.CreateFurniture(_selectedSlot.furnitureIndex, _hits[0].pose.position, plane);
                            _selectedSlot = null;
                            _listIndex++;
                        }
                    }
                }
                else
                {
                    _previewImage.enabled = false;
                    Destroy(_previewPrefeb);
                }
            }
        }

        private void Update() //터치로도 해볼까 한 흔적
        {
            _mousePosi = _dragCurrentPosition.action.ReadValue<Vector2>();
            if (_selectedSlot)
            {
                if (isSlot)
                {
                    _ped.position = _mousePosi; // 레이캐스트 위치설정
                    _rrListStart.Clear(); //리스트 클리어
                    _graycast.Raycast(_ped, _rrListStart); //그래픽 레이캐스트의 결과를 리스트에 저장

                    if (_rrListStart.Count > 1)
                    {
                        _previewImage.enabled = true;
                        _previewImage.transform.position = _mousePosi;
                        return;
                    }
                    if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
                    {
                        Debug.Log("AR레이캐스트");
                        if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
                        {
                            _previewPrefeb.SetActive(true);
                            _previewImage.enabled = false;
                            _previewPrefeb.transform.position = _hits[0].pose.position;
                        }
                    }
                }
            }
        }
            
            //Debug.Log(_tapStartPosition.action.phase);

            //if (Input.touchCount > 0)
            //{
            //    Touch touch = Input.touches[0];
            //    if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(touch.position), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            //    {
            //        if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
            //        {
            //            if (plane.alignment != UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical)
            //            {

            //                _furnitureFactory.CreateFurniture("Cube", _hits[0].pose.position, plane);

            //            }
            //        }
            //    }
            //}
        
    }
}


