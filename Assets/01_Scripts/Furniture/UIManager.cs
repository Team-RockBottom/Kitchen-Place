using CP.Furniture;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private FurnitureSpecRepository _repository;
    [SerializeField] private Button _slotPrefeb;
    [SerializeField] private Vector3 _slotPosition;

    private Dictionary<int, FurnitureSpec> _slotDic = new Dictionary<int, FurnitureSpec>();

    private void Start()
    {
        foreach(var spec in _repository.IfurnitureDic.Values)
        {
            _slotDic.Add(spec.index, spec);
        }
        for (int i = 0; i < _slotDic.Count; i++)
        {
            Button _slot;
            _slot = Instantiate(_slotPrefeb, new Vector3(_slotPosition.x, _slotPosition.y - (80 * i), 0), Quaternion.identity);
            _slot.gameObject.transform.localScale = Vector3.one;
            _slot.gameObject.name = _slotDic[i].name;
            _slot.gameObject.transform.SetParent(transform,true);   


        }
    }
    
    

}
