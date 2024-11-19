using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private FurnitureSpecRepository _repository;
    [SerializeField] private GameObject _slotPrefeb;
    [SerializeField] private GameObject _InputManager;
    private RectTransform _contentTransform;
    private Dictionary<int, FurnitureSpec> _slotDic = new Dictionary<int, FurnitureSpec>();

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
            _slot.gameObject.name = _slotDic[i].name;
            furnitureSlot.furnitureNameText = _slotDic[i].name;
            furnitureSlot.furnitureIcon = _slotDic[i].Sprite;
            furnitureSlot.furniturePrefeb = _slotDic[i].Prefeb;
            furnitureSlot.furnitureIndex = i;

            _slot.gameObject.transform.SetParent(transform.GetChild(0).GetChild(0),true); //오브젝트 그룹의 자식으로 들어간다.
        }
        if (_slotDic.Count > 7) //슬롯이 스크롤 뷰보다 많아져서 안보여지면
        {
            Vector2 size = _contentTransform.sizeDelta;
            size.x -= (150*(7-_slotDic.Count)); //넘은 갯수만큼 늘려준다.
            _contentTransform.sizeDelta = size;
        }
    }
}
