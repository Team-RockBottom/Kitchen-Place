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
        [SerializeField] private FurnitureFactory _furnitureFactory; //���丮 ��� �ؾ� ���� �Ⱥ��ϼ���������
        private List<ARRaycastHit> _hits = new List<ARRaycastHit>(2);
        private string _name; //�ҷ��� ������ �̸� UI���� �ǵ�� �ٲ�� ��������
        [SerializeField] private LayerMask _layerMask; //���̾� ����ũ �Ϲ� ����ĳ��Ʈ�� �ν�
        private RaycastHit _hit; //����ĳ��Ʈ ����
        private bool isClick = false; //���ӵ� ������ ���� bool
        private bool isSelecte = false; // ���� ����
        private GameObject[] _objs = new GameObject[10]; //������ ������Ʈ�� ����
        private int _listIndex = 0; //�ε���
        [SerializeField] private GraphicRaycaster _graycast; //UI�� �ִ� �׷��� ����ĳ��Ʈ�� �����´�.
        private PointerEventData _ped; //�׷��� ����ĳ��Ʈ�� �ʿ��� ����Ʈ�̺�Ʈ ������
        private List<RaycastResult> _rrList; //����ĳ��Ʈ ����� ������ ����Ʈ

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
            _tapStartPosition.action.performed += OffTouch; //���콺�� ������ bool���� false��
            _ped = new PointerEventData(EventSystem.current);
            _rrList = new List<RaycastResult>(5);
        }

        private void OnTouch(InputAction.CallbackContext context)
        {
            Vector2 tapPostion = context.ReadValue<Vector2>();
            _ped.position = tapPostion; // ����ĳ��Ʈ ��ġ����
            _rrList.Clear(); //����Ʈ Ŭ����
            _graycast.Raycast(_ped, _rrList); //�׷��� ����ĳ��Ʈ�� ����� ����Ʈ�� ����
            if (_rrList.Count > 0) // ����Ʈ�� �߰��Ǹ� �ߵ����� ���ϰ� ���´�.
            {
                _name = _rrList[0].gameObject.name; //UIâ���� �巡��������� �����Ǵ� �ڵ带 ��������� ����
                
                return;
            }

            if(Physics.Raycast(_xrCamera.ScreenPointToRay(tapPostion),out _hit,100f,_layerMask)) //������ �ν��ϰ� ��ġ�� �Ҽ� �����ϴ� ����ĳ��Ʈ
            {
                 return;
            }

            if (isSelecte)
            {
                if (_arRaycastManager.Raycast(_xrCamera.ScreenPointToRay(tapPostion), _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
                {
                    if (_hits[0].trackable.TryGetComponent(out ARPlane plane))
                    {
                        if (plane.alignment != UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical) //����νı������ �������� ���� �ȵǰ�
                        {
                            if (!isClick)
                            {
                                _objs[_listIndex] = _furnitureFactory.CreateFurniture(_name, _hits[0].pose.position, plane); //AR����ĳ��Ʈ�� ����
                                isClick = true;
                                isSelecte = false;
                            }
                            else
                            {
                                _objs[_listIndex].transform.position = _hits[0].pose.position; //true�� ������
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
                if (_listIndex >= _objs.Length - 1) //���� �迭
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
        //private void Update() //��ġ�ε� �غ��� �� ����
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


