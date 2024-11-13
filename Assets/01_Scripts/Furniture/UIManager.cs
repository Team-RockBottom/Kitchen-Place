using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] private FurnitureSpecRepository _repository;
    [SerializeField] private GameObject _slotPrefeb;
    private RectTransform _contentTransform;
    private Dictionary<int, FurnitureSpec> _slotDic = new Dictionary<int, FurnitureSpec>();

    private void Start()
    {
        _contentTransform = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        foreach(var spec in _repository.IfurnitureDic.Values)
        {
            _slotDic.Add(spec.index, spec);
        }
        for (int i = 0; i < _slotDic.Count; i++)
        {
            GameObject _slot;
            _slot = Instantiate(_slotPrefeb);
            _slot.gameObject.name = _slotDic[i].name;
            _slot.gameObject.transform.SetParent(transform.GetChild(0).GetChild(0),true); //오브젝트 그룹의 자식으로 들어간다.
        }
        if (_slotDic.Count > 7) //슬롯이 스크롤 뷰보다 많아져서 안보여지면
        {
            Vector2 size = _contentTransform.sizeDelta;
            size.x -= (110*(7-_slotDic.Count)); //넘은 갯수만큼 늘려준다.
            _contentTransform.sizeDelta = size;
        }
    }
}
