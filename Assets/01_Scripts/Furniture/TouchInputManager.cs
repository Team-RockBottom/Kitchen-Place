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
        private GameObject _objs; //������ ������Ʈ�� ����
        [SerializeField] private GraphicRaycaster _graycast; //UI�� �ִ� �׷��� ����ĳ��Ʈ�� �����´�.
        private PointerEventData _ped; //�׷��� ����ĳ��Ʈ�� �ʿ��� ����Ʈ�̺�Ʈ ������
        private List<RaycastResult> _rrListStart; //����ĳ��Ʈ ����� ������ ����Ʈ
        private FurnitureSlot _selectedSlot; // ���õ� ������ ������ �����ϴ� ����
        [SerializeField] private Image _previewImage; // �̸����� �̹���
        [SerializeField] private ScrollRect _scrollRect; // ��ũ�� ��
        [SerializeField] private GameObject _scrollDummyUI; // �巡�׷� UI�� ������ �������� �ٽ� Ui�ٰ� ��������� ����UI
        private Vector2 _mousePosi; // ������Ʈ���� �����ϴ� ���콺 ��ġ
        private GameObject _previewPrefeb; // �̸����� ������
        private GameObject _previewPrefebArea; // �̸����� ������ �Ʒ��� ��ġ���� ���θ� ǥ�����ִ� ������Ʈ
        [SerializeField] private Material _imposisvleArea; // �Ұ������� ǥ���ϴ� ���׸���(������)
        [SerializeField] private Material _posisvleArea; // �������� ǥ���ϴ� ���׸���(���)
        private bool _isPosivle = false; // ���׸����� ��ȯ�� ��Ʈ���ϴ� bool��
        private bool _isTrigger = false; // ��ġ�� ������Ʈ���� ���ƴ��� �Ȱ��ƴ��� �˷��ִ� bool��


        
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

            _dragCurrentPosition.action.started += OnDrag;
            _dragCurrentPosition.action.canceled += OffTouch;
            
            _ped = new PointerEventData(EventSystem.current);
            _rrListStart = new List<RaycastResult>(5);
        }

        private void OnDrag(InputAction.CallbackContext context)
        {
            Vector2 tapPostion = context.ReadValue<Vector2>();
            _ped.position = tapPostion; // ����ĳ��Ʈ ��ġ����
            _rrListStart.Clear(); //����Ʈ Ŭ����
            _graycast.Raycast(_ped, _rrListStart); //�׷��� ����ĳ��Ʈ�� ����� ����Ʈ�� ����
            
            // UI���� �ִ� ����
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
            if (_selectedSlot) // ���õ� ������ ������
            {
                if (isSlot) // ������ �ƴѰ��� �巡�������� ����°� �����ش�.
                {
                    _ped.position = _mousePosi; // ����ĳ��Ʈ ��ġ����
                    _rrListStart.Clear(); //����Ʈ Ŭ����
                    _graycast.Raycast(_ped, _rrListStart); //�׷��� ����ĳ��Ʈ�� ����� ����Ʈ�� ����

                    if (_rrListStart.Count > 1) // UI�� ���� ������(1>�� ������ �̸����� �̹����� Ŀ���� ��������־)
                    {
                        ScrollSpawn(); // ������������ ��ũ�Ѻ䰡 ��Ȱ��ȭ ���¸� Ȱ��ȭ
                        if (_previewPrefeb)
                        {
                            _previewPrefeb.SetActive(false); // �������� ��Ȱ��ȭ
                        }
                        _previewImage.enabled = true; // �̹��� Ȱ��ȭ
                        _previewImage.transform.position = _mousePosi; // �̹����� ���콺�� ���󰡰�
                        return; // ���Ͻ��� �� ���� �ڵ带 �����Ű���ʵ���
                    }
                    
                    if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
                    {
                        if (_hits[0].trackable.TryGetComponent(out ARPlane plane)) // AR����ĳ��Ʈ �ν�
                        {
                            if(!_isTrigger) // ������Ʈ�� ������Ʈ�� �ε������� ��Ȱ��ȭ
                            {
                                if (plane.alignment != UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical) //����νı��
                                {
                                    _isPosivle = true; // ��ġ����
                                }
                                else
                                {
                                    _isPosivle = false; // ��ġ�Ұ�
                                }
                            }
                            Vector3 direction = Camera.main.transform.position - _hits[0].pose.position; //ī�޶���� ���� ���
                            direction.y = 0; //Y�� ȸ���� �����Ͽ� ������Ʈ�� ���Ʒ��� �������� �ʵ��� ��
                            Quaternion rotation = Quaternion.LookRotation(direction); //������Ʈ�� ī�޶� �ٶ󺸵��� ȸ��
                            _previewPrefeb.transform.rotation = rotation; //PreviewPrefeb ȸ�� ����
                            if (_scrollRect.gameObject.activeSelf) // AR����ĳ��Ʈ�� �νĵǸ� ��ũ�Ѻ� ��Ȱ��ȭ
                            {
                                _scrollRect.gameObject.SetActive(false);
                                _scrollDummyUI.SetActive(true);
                            }
                            if (_previewPrefeb) // �̸����� ������ Ȱ��ȭ
                            {
                                _previewPrefeb.SetActive(true);
                            }
                            _previewImage.enabled = false; // �̹��� ��Ȱ��ȭ
                            _previewPrefeb.transform.position = _hits[0].pose.position; // �̸����� �������� Ŀ���� �������
                        }
                    }
                }
            }
            IsPosivleMaterial(); // isPosivle�� ���� ���� ���׸����� ����
        }

        private void OffTouch(InputAction.CallbackContext context)
        {
            _scrollRect.enabled = true; // ��ũ�Ѻ��� �巡�� Ȱ��ȭ
            isSlot = false; // ���� ��Ȱ��ȭ

            _ped.position = _mousePosi; // ����ĳ��Ʈ ��ġ����
            _rrListStart.Clear(); //����Ʈ Ŭ����
            _graycast.Raycast(_ped, _rrListStart); //�׷��� ����ĳ��Ʈ�� ����� ����Ʈ�� ����
            
            if (_rrListStart.Count > 1) return; // UI���̶�� ����

            if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(_mousePosi), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
                {
                    if (_isPosivle)
                    {
                        if (_selectedSlot)
                        {
                            _objs = _furnitureFactory.CreateFurniture(_selectedSlot.FurnitureIndex, _hits[0].pose.position, plane);
                            _objs.GetComponent<BoxCollider>().isTrigger = true; // �������Ĵ� Ʈ���Ÿ� Ȱ��ȭ ���� ���� �����־ �Լ��� ������ �ʵ���
                            _selectedSlot = null;
                        }
                    }
                }
            }
            PreviewDisable(); // �̸����⸦ �����Ѵ�.
            ScrollSpawn(); // ��Ȱ��ȭ�� ��ũ�Ѻ並 Ȱ��ȭ
        }

        private void SelecteSlot(FurnitureSlot slot) // ������ �����Ҷ� �ҷ����� �Լ�
        {
            _selectedSlot = slot;
            _previewImage.sprite = slot.FurnitureIcon;
            _scrollRect.enabled = false; // ��ũ�Ѻ��� �巡�� ��� ��Ȱ��ȭ
            isSlot = true;
            _previewPrefeb = _furnitureFactory.CreatePreviewFurniture(slot.FurnitureIndex); // �̸����� ������ ����
            _previewPrefebArea = _furnitureFactory.CreatePreviewFurniture(slot.FurnitureIndex); // �̸����� ������ ���� ����
            _previewPrefebArea.transform.localScale = new Vector3(1, 0.01f, 1); // ������ Y�������� 0.01�� �ϴ°ɷ� ǥ��
            _previewPrefebArea.transform.SetParent(_previewPrefeb.transform, true); // �ڽ����� �־� ���󰡰� �����.
        }

        private void PreviewDisable() // �̸����⸦ �ı��ϴ� �Լ�
        {
            if (_previewPrefeb)
            {
                Destroy(_previewPrefeb);
            }
            if(!_isPosivle)
            {
                _isPosivle = true;
            }
            _previewImage.enabled = false;
        }

        private void ScrollSpawn() // ��ũ�Ѻ並 Ȱ��ȭ �ϴ� �Լ�
        {
            if (!_scrollRect.gameObject.activeSelf)
            {
                _scrollRect.gameObject.SetActive(true);
                _scrollDummyUI.SetActive(false);
            }
        }

        private void IsPosivleMaterial() // ���ǹ����� ���׸����� �ٲ��ִ� �Լ�
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
        /// ������Ʈ�� ������Ʈ�� ������� ȣ���ϴ� �Լ�(��ġ ����)
        /// </summary>
        public void PrefebColliderCheckTrue() {_isPosivle=true; _isTrigger = false; }

        /// <summary>
        /// ������Ʈ�� ������Ʈ�� �������� ȣ���ϴ� �Լ�(��ġ �Ұ�)
        /// </summary>
        public void PrefebColliderCheckFalse() { _isPosivle = false; _isTrigger = true; }
    }
}


