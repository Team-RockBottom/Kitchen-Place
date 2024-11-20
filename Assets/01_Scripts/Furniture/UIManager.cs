using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private FurnitureSpecRepository _repository;
    [SerializeField] private GameObject _slotPrefeb;
    [SerializeField] private GameObject _InputManager;
    [SerializeField] private GridLayoutGroup _layoutGroup;
    private RectTransform _contentTransform;
    private Dictionary<int, FurnitureSpec> _slotDic = new Dictionary<int, FurnitureSpec>();
    private float _slotDistance;

    private void Start()
    {
        if(_repository == null)
        {
            _repository = _InputManager.GetComponentInChildren<FurnitureSpecRepository>();
        }
        _contentTransform = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        foreach(var spec in _repository.IfurnitureDic.Values)
        {
            _slotDic.Add(spec.Index, spec);
        }
        for (int i = 0; i < _slotDic.Count; i++)
        {
            GameObject _slot;
            _slot = Instantiate(_slotPrefeb);
            FurnitureSlot furnitureSlot = _slot.GetComponent<FurnitureSlot>();
            furnitureSlot.furnitureNameText = _slotDic[i].Name;
            furnitureSlot.furnitureIcon = _slotDic[i].Sprite;
            furnitureSlot.furniturePrefeb = _slotDic[i].Prefeb;
            furnitureSlot.furnitureIndex = i;

            _slot.gameObject.transform.SetParent(transform.GetChild(0).GetChild(0),true); //������Ʈ �׷��� �ڽ����� ����.
        }
        if (_slotDic.Count > 7) //������ ��ũ�� �亸�� �������� �Ⱥ�������
        {
            _slotDistance = _layoutGroup.cellSize.x + _layoutGroup.spacing.x;
            Vector2 size = _contentTransform.sizeDelta;
            size.x -= (_slotDistance*(7-_slotDic.Count)); //���� ������ŭ �÷��ش�.
            _contentTransform.sizeDelta = size;
        }
    }
}
