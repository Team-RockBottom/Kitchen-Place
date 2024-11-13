using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace CP.Furniture
{
    public class FurnitureFactory : MonoBehaviour
    {
        [SerializeField] private FurnitureSpecRepository _repository;
        private GameObject _furnitures; //�θ�� ���� �� ������Ʈ
        private GraphicRaycaster _graycast;

        /// <summary>
        /// ������Ʈ ���� �Լ�
        /// </summary>
        /// <param name="name">�ҷ��� ���� �̸�</param>
        /// <param name="position">������ ��ġ</param>
        /// <param name="plane">��� ����</param>
        /// <returns></returns>
        public GameObject CreateFurniture(string name, Vector3 position, ARPlane plane)
        {
            //_repository.IfurnitureDic.TryGetValue(name, out FurnitureSpec furniture);
            FurnitureSpec furniture = _repository.GetSpec(name); //�̸��� ���缭 ������ ������ �ҷ��´�.
            GameObject obj = null; //������Ʈ ������ ����
            if (furniture != null) //������ ������ �ִٸ�
            {
                obj = Instantiate(furniture.furniturePrefeb, position, furniture.furniturePrefeb.transform.rotation); //����
                obj.transform.localScale = Vector3.one / 5; //������ ����(������ �������� ��� �������� ���Ƿ� ����)
                float planeY = plane.gameObject.transform.position.y + (obj.transform.localScale.y / 2);
                if(name == "Table") //���̺��� �Ǻ��� ���� �־ ����ó��
                {
                    planeY += (obj.transform.localScale.y / 3);
                }
                obj.transform.position = new Vector3(position.x, planeY, position.z);
                if (_furnitures == null) //�θ�� ���� �� ������Ʈ������ ����
                {
                    _furnitures = new GameObject("Furnitures");
                }
                obj.transform.SetParent(_furnitures.transform, true); //�ڽ����� ����.

            }
            return obj; //����
        }
        public GameObject CreatePreviewFurniture(string name, Vector3 position)
        {
            FurnitureSpec furniture = _repository.GetSpec(name); //�̸��� ���缭 ������ ������ �ҷ��´�.
            GameObject obj = null; //������Ʈ ������ ����
            if (furniture != null)
            {
                obj = Instantiate(furniture.furniturePrefeb, position, furniture.furniturePrefeb.transform.rotation); //����
                obj = Instantiate(furniture.furniturePrefeb, position, furniture.furniturePrefeb.transform.rotation); //����
                obj.transform.localScale = Vector3.one / 5; //������ ����(������ �������� ��� �������� ���Ƿ� ����)
                float planeY = obj.transform.localScale.y / 2;
                if (name == "Table") //���̺��� �Ǻ��� ���� �־ ����ó��
                {
                    planeY += (obj.transform.localScale.y / 3);
                }
                obj.transform.position = new Vector3(position.x, planeY, position.z);
            }

            return obj;
        }
    }
}
