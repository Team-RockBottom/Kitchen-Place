using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SlotDrag : MonoBehaviour
    //IBeginDragHandler, IEndDragHandler
{
    private Image _image;
    private GameObject _beingDraggedIcon;
    private Vector3 _startPosition;
    private Transform _onDragParent;
    private Transform _startParent;
    [SerializeField] InputActionReference _currentDragPosition;

    private void Start()
    {
        _image = GetComponent<Image>();
        //_currentDragPosition.action.started += OnDrag;
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    _beingDraggedIcon = gameObject;

    //    _startPosition = transform.position;
    //    _startParent = transform.parent;

    //    //GetComponent<CanvasGroup>().blocksRaycasts = false;

    //    transform.SetParent(_onDragParent);
    //}

    //private void OnDrag(InputAction.CallbackContext context)
    //{
    //    Vector2 dragPosition = context.ReadValue<Vector2>();
    //    transform.position = dragPosition;
    //}
    public GameObject Clone()
    {
        Color color = _image.color;
        GameObject obj = Instantiate(gameObject);
        color.a = 0.5f;
        _image.color = color;
        obj.transform.SetParent(transform, false);
        return obj;
    }
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    _beingDraggedIcon = null;
    //    //GetComponent<CanvasGroup>().blocksRaycasts = true;

    //    if(transform.parent == _onDragParent)
    //    {
    //        transform.position = _startPosition;
    //        transform.SetParent(_startParent);
    //    }
    //}
}
