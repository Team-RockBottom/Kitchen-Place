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
        private Vector2 _mousePosi;
        private GameObject _previewPrefeb;
        
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
                        _selectedSlot = slot;
                        _previewImage.sprite = slot.furnitureIcon;
                        Debug.Log(slot.name);
                        _scrollRect.enabled = false;
                        isSlot = true;
                        _previewPrefeb = _furnitureFactory.CreatePreviewFurniture(slot.furnitureIndex);
                        _previewPrefeb.layer = 0;

                    }
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

            _ped.position = _mousePosi; // ����ĳ��Ʈ ��ġ����
            _rrListStart.Clear(); //����Ʈ Ŭ����
            _graycast.Raycast(_ped, _rrListStart); //�׷��� ����ĳ��Ʈ�� ����� ����Ʈ�� ����

            if (_rrListStart.Count > 1)
            {
                _previewImage.enabled=false;
                if (_previewPrefeb)
                {
                    Destroy(_previewPrefeb);
                }
                return;
            }

            if (Physics.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), out _hit, 100f, _layerMask)) //������ �ν��ϰ� ��ġ�� �Ҽ� �����ϴ� ����ĳ��Ʈ
            {
                _previewImage.enabled=false;
                if (_previewPrefeb)
                {
                    Destroy(_previewPrefeb);
                }

                return;
            }

            if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
                {
                    if (plane.alignment != UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical) //����νı������ �������� ���� �ȵǰ�
                    {
                        if (_selectedSlot)
                        {
                            _previewImage.enabled = false;
                            if (_previewPrefeb)
                            {
                                Destroy(_previewPrefeb);
                            }
                            _objs[_listIndex] = _furnitureFactory.CreateFurniture(_selectedSlot.furnitureIndex, _hits[0].pose.position, plane);
                            _selectedSlot = null;
                            _listIndex++;
                        }
                    }
                }
                else
                {
                    _previewImage.enabled = false;
                    if(_previewPrefeb)
                    {
                        Destroy(_previewPrefeb);
                    }
                }
            }
        }

        private void Update() //��ġ�ε� �غ��� �� ����
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
                            if(_previewPrefeb)
                            {
                                _previewPrefeb.SetActive(true);
                            }
                            _previewImage.enabled = false;
                            _previewPrefeb.transform.position = _hits[0].pose.position;
                        }
                    }
                }
            }
        }
    }
}


