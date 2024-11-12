using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace CP.Furniture
{
    public class FurnitureFactory : MonoBehaviour
    {
        [SerializeField] private FurnitureSpecRepository _repository;


        public void CreateFurniture(string name, Vector3 position, ARPlane plane)
        {
            //_repository.IfurnitureDic.TryGetValue(name, out FurnitureSpec furniture);
            FurnitureSpec furniture = _repository.GetSpec(name);
            if (furniture != null)
            {
                Debug.Log($"{name}按眉 积己");
                GameObject obj = Instantiate(furniture.furniturePrefeb, position, furniture.furniturePrefeb.transform.rotation);
                obj.transform.localScale = Vector3.one / 5;
                float planeY = plane.gameObject.transform.position.y + (obj.transform.localScale.y / 2);
                obj.transform.position = new Vector3(position.x, planeY, position.z);
            }
            Debug.Log($"Create 角青");

        }
    }
}
