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
        private FurnitureFactory _furnitureFactory; //���丮
        private List<ARRaycastHit> _hits = new List<ARRaycastHit>(2);
        [SerializeField] private LayerMask _layerMask; //���̾� ����ũ �Ϲ� ����ĳ��Ʈ�� �ν�
        private RaycastHit _hit; //����ĳ��Ʈ ����
        private bool isSlot = false; //���ӵ� ������ ���� bool
        private GameObject[] _objs = new GameObject[10]; //������ ������Ʈ�� ����
        private int _listIndex = 0; //�ε���
        [SerializeField] private GraphicRaycaster _graycast; //UI�� �ִ� �׷��� ����ĳ��Ʈ�� �����´�.
        private PointerEventData _ped; //�׷��� ����ĳ��Ʈ�� �ʿ��� ����Ʈ�̺�Ʈ ������
        private List<RaycastResult> _rrListStart; //����ĳ��Ʈ ����� ������ ����Ʈ
        private FurnitureSlot _selectedSlot;
        [SerializeField] private Image _previewImage;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private GameObject _scrollDummyUI;
        private Vector2 _mousePosi;
        private GameObject _previewPrefeb;
        private GameObject _previewPrefebArea;
        [SerializeField] private Material _imposisvleArea;
        [SerializeField] private Material _posisvleArea;
        private bool _isPosivle = false;
        private bool _isTrigger = false;


        
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
            //_tapStartPosition.action.started += OnTouch;

            _dragCurrentPosition.action.started += OnTouch;
            _dragCurrentPosition.action.canceled += OffTouch;
            
            _ped = new PointerEventData(EventSystem.current);
            _rrListStart = new List<RaycastResult>(5);
        }

        private void OnTouch(InputAction.CallbackContext context)
        {
            Vector2 tapPostion = context.ReadValue<Vector2>();
            _ped.position = tapPostion; // ����ĳ��Ʈ ��ġ����
            _rrListStart.Clear(); //����Ʈ Ŭ����
            _graycast.Raycast(_ped, _rrListStart); //�׷��� ����ĳ��Ʈ�� ����� ����Ʈ�� ����
            
            if(_selectedSlot)
            {
                // UI���� �ִ� ����
                if (_rrListStart.Count > 0)
                {
                    if (_rrListStart[0].gameObject.TryGetComponent(out FurnitureSlot slot))
                    {
                        SelecteSlot(slot);
                    }
                }
                else
                {
                    _selectedSlot = null;
                }
            }
            else
            {
                // UI���� �ִ� ����
                if (_rrListStart.Count > 0)
                {
                    if (_rrListStart[0].gameObject.TryGetComponent(out FurnitureSlot slot))
                    {
                        SelecteSlot(slot);
                    }
                }
            }
        }
        private void SelecteSlot(FurnitureSlot slot)
        {
            _selectedSlot = slot;
            _previewImage.sprite = slot.FurnitureIcon;
            _scrollRect.enabled = false;
            isSlot = true;
            _previewPrefeb = _furnitureFactory.CreatePreviewFurniture(slot.FurnitureIndex);
            _previewPrefebArea = _furnitureFactory.CreatePreviewFurniture(slot.FurnitureIndex);
            _previewPrefebArea.transform.localScale = new Vector3(1, 0.01f, 1);
            _previewPrefebArea.transform.SetParent(_previewPrefeb.transform, true);
        }
        private void OnDrag(InputAction.CallbackContext context)
        {
            
        }

        private void OffTouch(InputAction.CallbackContext context)
        {
            _scrollRect.enabled = true;
            isSlot = false;

            _ped.position = _mousePosi; // ����ĳ��Ʈ ��ġ����
            _rrListStart.Clear(); //����Ʈ Ŭ����
            _graycast.Raycast(_ped, _rrListStart); //�׷��� ����ĳ��Ʈ�� ����� ����Ʈ�� ����

            if (_rrListStart.Count > 1)
            {
                _previewImage.enabled=false;
                PreviewPrefebDestroy();
                return;
            }

            if (Physics.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), out _hit, 100f, _layerMask)) //������ �ν��ϰ� ��ġ�� �Ҽ� �����ϴ� ����ĳ��Ʈ
            {
                _previewImage.enabled=false;
                PreviewPrefebDestroy();
                ScrollSpawn();

                return;
            }

            if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
                {
                    if (_isPosivle) //����νı������ �������� ���� �ȵǰ�
                    {
                        if (_selectedSlot)
                        {
                            ScrollSpawn();
                            PreviewPrefebDestroy();
                            _previewImage.enabled = false;
                            _objs[_listIndex] = _furnitureFactory.CreateFurniture(_selectedSlot.FurnitureIndex, _hits[0].pose.position, plane);
                            _objs[_listIndex].GetComponent<BoxCollider>().isTrigger = true;
                            _selectedSlot = null;
                            _listIndex++;
                        }
                    }
                    else
                    {
                        _previewImage.enabled = false;
                        PreviewPrefebDestroy();
                        ScrollSpawn();
                    }
                    ScrollSpawn();
                }
                else
                {
                    _previewImage.enabled = false;
                    PreviewPrefebDestroy();
                    ScrollSpawn();
                }
            }
        }

        public void PrefebColliderCheckFalse() { _isPosivle = false; _isTrigger = true; }

        public void PrefebColliderCheckTrue() {_isPosivle=true; _isTrigger = false; }

        private void PreviewPrefebDestroy()
        {
            if (_previewPrefeb)
            {
                Destroy(_previewPrefeb);
            }
            if(!_isPosivle)
            {
                _isPosivle = true;
            }
        }

        private void ScrollSpawn()
        {
            if (!_scrollRect.gameObject.activeSelf)
            {
                _scrollRect.gameObject.SetActive(true);
                _scrollDummyUI.SetActive(false);
            }
        }

        private void IsPosivleMaterial()
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

        private void Update()
        {
            _mousePosi = _dragCurrentPosition.action.ReadValue<Vector2>();
            if (_selectedSlot)
            {
                if (isSlot)
                {
                    _ped.position = _mousePosi; // ����ĳ��Ʈ ��ġ����
                    _rrListStart.Clear(); //����Ʈ Ŭ����
                    _graycast.Raycast(_ped, _rrListStart); //�׷��� ����ĳ��Ʈ�� ����� ����Ʈ�� ����

                    if (_rrListStart.Count > 1)
                    {
                        ScrollSpawn();
                        if (_previewPrefeb)
                        {
                            _previewPrefeb.SetActive(false);
                        }
                        _previewImage.enabled = true;
                        _previewImage.transform.position = _mousePosi;
                        return;
                    }
                    
                    if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
                    {
                        if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
                        {
                            if(!_isTrigger)
                            {
                                if (plane.alignment != UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical) //����νı������ �������� ���� �ȵǰ�
                                {
                                    _isPosivle = true;
                                }
                                else
                                {
                                    _isPosivle = false;
                                }
                            }
                            Vector3 direction = Camera.main.transform.position - _hits[0].pose.position; //ī�޶���� ���� ���
                            direction.y = 0; //Y�� ȸ���� �����Ͽ� UI�� ���Ʒ��� �������� �ʵ��� ��
                            Quaternion rotation = Quaternion.LookRotation(direction); //UI�� ī�޶� �ٶ󺸵��� ȸ��
                            _previewPrefeb.transform.rotation = rotation; //UIImage ȸ�� ����
                            if (_scrollRect.gameObject.activeSelf)
                            {
                                _scrollRect.gameObject.SetActive(false);
                                _scrollDummyUI.SetActive(true);
                            }
                            if (_previewPrefeb)
                            {
                                _previewPrefeb.SetActive(true);
                            }
                            _previewImage.enabled = false;
                            _previewPrefeb.transform.position = _hits[0].pose.position;
                        }
                    }
                    if (Physics.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), out _hit, 100f, _layerMask)) //������ �ν��ϰ� ��ġ�� �Ҽ� �����ϴ� ����ĳ��Ʈ
                    {
                        if (_isTrigger)
                        {
                            _isPosivle = false;
                        }
                    }
                }
            }
            IsPosivleMaterial();
        }
    }
}


