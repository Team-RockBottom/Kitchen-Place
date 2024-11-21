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
            furnitureSlot.FurnitureNameText = _slotDic[i].Name;
            furnitureSlot.FurnitureIcon = _slotDic[i].Sprite;
            furnitureSlot.FurniturePrefeb = _slotDic[i].Prefeb;
            furnitureSlot.FurnitureIndex = i;
            furnitureSlot.FurnitureSize = _slotDic[i].Size;
            furnitureSlot.FurnitureSizeText = $"{_slotDic[i].Size.x} X {_slotDic[i].Size.z} X {_slotDic[i].Size.y}";

            _slot.gameObject.transform.SetParent(transform.GetChild(0).GetChild(0),true); //오브젝트 그룹의 자식으로 들어간다.
        }

        _slotDistance = _layoutGroup.cellSize.x + _layoutGroup.spacing.x;
        Debug.Log(_layoutGroup.cellSize.y);
        Debug.Log(_layoutGroup.spacing.x);
        Vector2 size = _contentTransform.sizeDelta;
        size.x -= (_slotDistance*(4-_slotDic.Count)); //넘은 갯수만큼 늘려준다.
        _contentTransform.sizeDelta = size;
    }
}
