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
        [SerializeField] private FurnitureFactory _furnitureFactory; //팩토리 어떻게 해야 따로 안붙일수있으려나
        private List<ARRaycastHit> _hits = new List<ARRaycastHit>(2);
        private string _name; //불러올 프리펩 이름
        [SerializeField] private LayerMask _layerMask; //레이어 마스크 일반 레이캐스트로 인식
        private RaycastHit _hit; //레이캐스트 저장
        private bool isClick = false; //연속된 생성을 막는 bool
        private bool isSelecte = false; // 선택 상태
        private GameObject[] _objs = new GameObject[10]; //생성된 오브젝트를 저장
        private int _listIndex = 0; //인덱스
        [SerializeField] private GraphicRaycaster _graycast; //UI에 있는 그래픽 레이캐스트를 가져온다.
        private PointerEventData _ped; //그래픽 레이캐스트에 필요한 포인트이벤트 데이터
        private List<RaycastResult> _rrList; //레이캐스트 결과를 저장할 리스트
        private bool _isUI = false;
        

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
            //_tapStartPosition.action.started += OnTouch;
            //_tapStartPosition.action.performed += OffTouch; //마우스를 뗐을때 bool형을 false로
            _ped = new PointerEventData(EventSystem.current);
            _rrList = new List<RaycastResult>(5);
        }

        private void OnTouch(InputAction.CallbackContext context)
        {
            Vector2 tapPostion = context.ReadValue<Vector2>();
            _ped.position = tapPostion; // 레이캐스트 위치설정
            _rrList.Clear(); //리스트 클리어
            _graycast.Raycast(_ped, _rrList); //그래픽 레이캐스트의 결과를 리스트에 저장
            if (_rrList.Count > 0) // UI위에 있는 상태
            {
                if (_rrList[0].gameObject.name != "Viewport")
                {
                    _name = _rrList[0].gameObject.name; //UI창에서 드래그했을경우 생성되는 코드를 만들려고한 흔적
                    isSelecte = true;
                }
                _isUI = true;
            }
            else
            {
                _isUI = false;
            }

            if (Physics.Raycast(_xrCamera.ScreenPointToRay(tapPostion), out _hit, 100f, _layerMask)) //가구를 인식하고 설치를 할수 없게하는 레이캐스트
            {
                return;
            }


            if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(tapPostion), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
                {
                    if (plane.alignment != UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical) //평면인식기능으로 벽에서는 생성 안되게
                    {
                        if (isSelecte)
                        {
                            if (_isUI)
                            {


                            }
                            else
                            {
                                _objs[_listIndex] = _furnitureFactory.CreateFurniture(_name, _hits[0].pose.position, plane);
                                isClick = true;
                                isSelecte = false;
                            }
                        }
                        _objs[_listIndex].transform.position = _hits[0].pose.position; //true면 움직임


                    }
                }
            }


        }
        private void OffTouch(InputAction.CallbackContext context)
        {
            if(isClick)
            {
                _name = null;
                Debug.Log($"OffTouch{_listIndex}");
                isClick = false;
                _listIndex++;
                if (_listIndex >= _objs.Length - 1) //동적 배열
                {
                    GameObject[] temps = new GameObject[_listIndex * 2];
                    for (int i = 0; i < _objs.Length; i++)
                    {
                        temps[i] = _objs[i];
                    }
                    _objs = temps;
                }

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
        private void Update() //터치로도 해볼까 한 흔적
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(touch.position), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
                {
                    if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
                    {
                        if (plane.alignment != UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical)
                        {

                            _furnitureFactory.CreateFurniture("Cube", _hits[0].pose.position, plane);

                        }
                    }
                }
            }
        }
    }
}


