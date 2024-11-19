using UnityEngine;
using System.Collections.Generic;

    public class FurnitureSpecRepository : MonoBehaviour
    {
        [SerializeField] private List<FurnitureSpec> _furnitureSpecs = new List<FurnitureSpec>();
        public IDictionary<int, FurnitureSpec> IfurnitureDic => _furnitureDic;
        private Dictionary<int, FurnitureSpec> _furnitureDic = new Dictionary<int, FurnitureSpec>();

        private void Awake()
        {
            foreach (var spec in _furnitureSpecs)
            {
                _furnitureDic.Add(spec.Index, spec);
            }
        }
        public FurnitureSpec GetSpec(int index)
        {
            return _furnitureDic[index];
        }
    }

