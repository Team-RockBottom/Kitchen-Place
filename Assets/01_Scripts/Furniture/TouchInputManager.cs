using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine;

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
        private GameObject _objs; //생성된 오브젝트를 저장
        [SerializeField] private GraphicRaycaster _graycast; //UI에 있는 그래픽 레이캐스트를 가져온다.
        private PointerEventData _ped; //그래픽 레이캐스트에 필요한 포인트이벤트 데이터
        private List<RaycastResult> _rrListStart; //레이캐스트 결과를 저장할 리스트
        private FurnitureSlot _selectedSlot; // 선택된 슬롯의 정보를 저장하는 변수
        [SerializeField] private Image _previewImage; // 미리보기 이미지
        [SerializeField] private ScrollRect _scrollRect; // 스크롤 뷰
        [SerializeField] private GameObject _scrollDummyUI; // 드래그로 UI바 밖으로 나갔을때 다시 Ui바가 생기게해줄 더미UI
        private Vector2 _mousePosi; // 업데이트에서 관리하는 마우스 위치
        private GameObject _previewPrefeb; // 미리보기 프리펩
        private GameObject _previewPrefebArea; // 미리보기 프리펩 아래의 설치가능 여부를 표시해주는 오브젝트
        [SerializeField] private Material _imposisvleArea; // 불가능함을 표시하는 마테리얼(빨간색)
        [SerializeField] private Material _posisvleArea; // 가능함을 표시하는 마테리얼(녹색)
        private bool _isPosivle = false; // 마테리얼의 변환을 컨트롤하는 bool형
        private bool _isTrigger = false; // 설치된 오브젝트에서 겹쳤는지 안겹쳤는지 알려주는 bool형
        private int _checkLRN = 0;


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
            if (_xrCamera == null)
            {
                _xrCamera = Camera.main;
            }
            _furnitureFactory = GetComponent<FurnitureFactory>();

            _dragCurrentPosition.action.started += OnDrag;
            _dragCurrentPosition.action.canceled += OffTouch;

            _ped = new PointerEventData(EventSystem.current);
            _rrListStart = new List<RaycastResult>(5);
        }

        private void OnDrag(InputAction.CallbackContext context)
        {
            Vector2 tapPostion = context.ReadValue<Vector2>();
            _ped.position = tapPostion; // 레이캐스트 위치설정
            _rrListStart.Clear(); //리스트 클리어
            _graycast.Raycast(_ped, _rrListStart); //그래픽 레이캐스트의 결과를 리스트에 저장

            // UI위에 있는 상태
            if (_rrListStart.Count > 0)
            {
                if (_rrListStart[0].gameObject.TryGetComponent(out FurnitureSlot slot))
                {
                    SelecteSlot(slot);
                }
            }
        }

        private void Update()
        {
            _mousePosi = _dragCurrentPosition.action.ReadValue<Vector2>();
            if (_selectedSlot) // 선택된 슬롯이 있을때
            {
                if (isSlot) // 슬롯이 아닌곳을 드래그했을때 생기는걸 막아준다.
                {
                    _ped.position = _mousePosi; // 레이캐스트 위치설정
                    _rrListStart.Clear(); //리스트 클리어
                    _graycast.Raycast(_ped, _rrListStart); //그래픽 레이캐스트의 결과를 리스트에 저장

                    if (_rrListStart.Count > 1) // UI바 위에 있을때(1>인 이유는 미리보기 이미지가 커서를 따라오고있어서)
                    {
                        ScrollSpawn(); // 재진입했을때 스크롤뷰가 비활성화 상태면 활성화
                        if (_previewPrefeb)
                        {
                            _previewPrefeb.SetActive(false); // 프리팹은 비활성화
                        }
                        _previewImage.enabled = true; // 이미지 활성화
                        _previewImage.transform.position = _mousePosi; // 이미지가 마우스를 따라가게
                        return; // 리턴시켜 이 밑의 코드를 실행시키지않도록
                    }
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(_mousePosi), out _hit, 100f, _layerMask))
                    {
                        
                        _previewPrefeb.transform.SetParent(_hit.transform.parent, true);
                        
                        float prefebSizeX = _previewPrefeb.GetComponent<FurnitureObject>().Spec.Size.x / 2000;
                        float targetSizeX = _hit.transform.parent.GetComponent<FurnitureObject>().Spec.Size.x / 2000;
                        float newSizeX =  prefebSizeX + targetSizeX;
                        if (_hit.collider.GetComponent<AreaActiveRight>())
                        {
                            _checkLRN = 1;
                            _previewPrefeb.transform.localPosition = new Vector3(-newSizeX, 0, 0);
                        }
                        else if (_hit.collider.gameObject.GetComponent<AreaActiveLeft>())
                        {
                            _checkLRN = 2;
                            _previewPrefeb.transform.localPosition = new Vector3(newSizeX, 0, 0);
                        }
                        _previewPrefeb.transform.localRotation = Quaternion.identity;
                        if(!_isTrigger)
                        {
                            _isPosivle = true;
                        }
                    }
                    else
                    {
                        if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
                        {
                            if (_hits[0].trackable.TryGetComponent(out ARPlane plane)) // AR레이캐스트 인식
                            {
                                _checkLRN = 0;
                                if (!_isTrigger) // 오브젝트와 오브젝트가 부딪힐때는 비활성화
                                {
                                    if (plane.alignment != UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical) //평면인식기능
                                    {
                                        _isPosivle = true; // 설치가능
                                    }
                                    else
                                    {
                                        _isPosivle = false; // 설치불가
                                    }
                                }
                                Vector3 direction = Camera.main.transform.position - _hits[0].pose.position; //카메라와의 방향 계산
                                direction.y = 0; //Y축 회전을 고정하여 오브젝트가 위아래로 기울어지지 않도록 함
                                Quaternion rotation = Quaternion.LookRotation(direction); //오브젝트가 카메라를 바라보도록 회전
                                _previewPrefeb.transform.rotation = rotation; //PreviewPrefeb 회전 적용
                                if (_scrollRect.gameObject.activeSelf) // AR레이캐스트가 인식되면 스크롤뷰 비활성화
                                {
                                    _scrollRect.gameObject.SetActive(false);
                                    _scrollDummyUI.SetActive(true);
                                }
                                if (_previewPrefeb) // 미리보기 프리펩 활성화
                                {
                                    _previewPrefeb.SetActive(true);
                                }
                                _previewImage.enabled = false; // 이미지 비활성화
                                _previewPrefeb.transform.position = _hits[0].pose.position; // 미리보기 프리펩이 커서를 따라오게
                            }
                        }
                    }
                    
                }
            }
            IsPosivleMaterial(); // isPosivle의 값에 따라 마테리얼을 변경
        }

        private void OffTouch(InputAction.CallbackContext context)
        {
            _scrollRect.enabled = true; // 스크롤뷰의 드래그 활성화
            isSlot = false; // 조건 비활성화

            _ped.position = _mousePosi; // 레이캐스트 위치설정
            _rrListStart.Clear(); //리스트 클리어
            _graycast.Raycast(_ped, _rrListStart); //그래픽 레이캐스트의 결과를 리스트에 저장
            PreviewDisable(); // 미리보기를 삭제한다.
            ScrollSpawn(); // 비활성화된 스크롤뷰를 활성화

            if (_rrListStart.Count > 1) return; // UI안이라면 리턴

            

            if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
                {
                    if (_isPosivle)
                    {
                        if (_previewPrefeb)
                        {
                            
                            if (_objs)
                            {
                                if(_objs.GetComponentInChildren<AreaActiveRight>())
                                {
                                    _objs.GetComponentInChildren<AreaActiveRight>().OffArea();
                                }
                                if(_objs.GetComponentInChildren<AreaActiveLeft>())
                                {
                                    _objs.GetComponentInChildren<AreaActiveLeft>().OffArea();
                                }
                            }
                            _objs = _furnitureFactory.CreateFurniture(_selectedSlot.FurnitureIndex, _hits[0].pose.position, plane);
                            _objs.GetComponent<BoxCollider>().isTrigger = true; // 생성된후는 트리거를 활성화 시켜 서로 겹쳐있어도 함수를 콜하지 않도록
                            _objs.GetComponentInChildren<AreaActiveRight>().OnArea();
                            _objs.GetComponentInChildren<AreaActiveLeft>().OnArea();

                            _objs.transform.position = _previewPrefeb.transform.position;
                            _objs.transform.rotation = _previewPrefeb.transform.rotation;
                            if (_checkLRN == 1)
                            {
                                _objs.GetComponentInChildren<AreaActiveLeft>().OffArea();
                            }
                            else if (_checkLRN == 2)
                            {
                                _objs.GetComponentInChildren<AreaActiveRight>().OffArea();
                            }

                            _selectedSlot = null;
                        }
                    }
                }
            }
            if (!_isPosivle)
            {
                _isPosivle = true;
            }
        }

        private void SelecteSlot(FurnitureSlot slot) // 슬롯을 선택할때 불러오는 함수
        {
            _selectedSlot = slot;
            _previewImage.sprite = slot.FurnitureIcon;
            _scrollRect.enabled = false; // 스크롤뷰의 드래그 기능 비활성화
            isSlot = true;
            _previewPrefeb = _furnitureFactory.CreatePreviewFurniture(slot.FurnitureIndex); // 미리보기 프리펩 생성
            _previewPrefebArea = _furnitureFactory.CreatePreviewFurniture(slot.FurnitureIndex); // 미리보기 프리펩 영역 생성
            _previewPrefebArea.transform.localScale = new Vector3(1, 0.01f, 1); // 영역을 Y스케일을 0.01로 하는걸로 표시
            _previewPrefebArea.transform.SetParent(_previewPrefeb.transform, true); // 자식으로 넣어 따라가게 만든다.
        }

        private void PreviewDisable() // 미리보기를 파괴하는 함수
        {
            if (_previewPrefeb)
            {
                Destroy(_previewPrefeb);
            }
            
            _previewImage.enabled = false;
        }

        private void ScrollSpawn() // 스크롤뷰를 활성화 하는 함수
        {
            if (!_scrollRect.gameObject.activeSelf)
            {
                _scrollRect.gameObject.SetActive(true);
                _scrollDummyUI.SetActive(false);
            }
        }

        private void IsPosivleMaterial() // 조건문으로 마테리얼을 바꿔주는 함수
        {
            if (_previewPrefebArea)
            {
                if (_isPosivle)
                {

                    _previewPrefebArea.GetComponent<Renderer>().material = _posisvleArea;
                }
                else
                {
                    _previewPrefebArea.GetComponent<Renderer>().material = _imposisvleArea;
                }
            }
        }

        /// <summary>
        /// 오브젝트와 오브젝트가 벗어났을때 호출하는 함수(설치 가능)
        /// </summary>
        public void PrefebColliderCheckTrue() { _isPosivle = true; _isTrigger = false; }

        /// <summary>
        /// 오브젝트와 오브젝트가 겹쳤을때 호출하는 함수(설치 불가)
        /// </summary>
        public void PrefebColliderCheckFalse() { _isPosivle = false; _isTrigger = true; }

        public void OffArea()
        {
            if (_objs)
            {
                if (_objs.GetComponentInChildren<AreaActiveRight>())
                {
                    _objs.GetComponentInChildren<AreaActiveRight>().OffArea();
                }
                if (_objs.GetComponentInChildren<AreaActiveLeft>())
                {
                    _objs.GetComponentInChildren<AreaActiveLeft>().OffArea();
                }
            }
        }
    }
}