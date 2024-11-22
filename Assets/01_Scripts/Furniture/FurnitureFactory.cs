using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace CP.Furniture
{
    public class FurnitureFactory : MonoBehaviour
    {
        private FurnitureSpecRepository _repository; 
        private GameObject _furnitures; //�θ�� ���� �� ������Ʈ
        private GraphicRaycaster _graycast;
        [SerializeField] UI_BasketController _basketController;

        private void Awake()
        {
            _repository = GetComponentInChildren<FurnitureSpecRepository>();

        }


        /// <summary>
        /// ������Ʈ ���� �Լ�
        /// </summary>
        /// <param name="name">�ҷ��� ���� �̸�</param>
        /// <param name="position">������ ��ġ</param>
        /// <param name="plane">��� ����</param>
        /// <returns></returns>
        public GameObject CreateFurniture(int index, Vector3 position, ARPlane plane)
        {
            FurnitureSpec furniture = _repository.GetSpec(index); //���� ��ȣ�� ���缭 ������ ������ �ҷ��´�.
            GameObject obj = null; //������Ʈ ������ ����
            if (furniture != null) //������ ������ �ִٸ�
            {
                obj = Instantiate(furniture.Prefeb, position, furniture.Prefeb.transform.rotation); //����
                _basketController.AddFurnitureBasket(furniture); //��ٱ��� ����Ʈ�� ����
                //obj.transform.localScale = Vector3.one / 5; //������ ����(������ �������� ��� �������� ���Ƿ� ����)

                Vector3 direction = Camera.main.transform.position - position; //ī�޶���� ���� ���
                direction.y = 0; //Y�� ȸ���� �����Ͽ� UI�� ���Ʒ��� �������� �ʵ��� ��
                Quaternion rotation = Quaternion.LookRotation(direction); //UI�� ī�޶� �ٶ󺸵��� ȸ��
                obj.transform.rotation = rotation; //UIImage ȸ�� ����
                obj.transform.position = position;
                if(obj.TryGetComponent(out FurnitureObject furobj))
                {
                    furobj.Spec = furniture;
                }

                if (_furnitures == null) //�θ�� ���� �� ������Ʈ������ ����
                {
                    _furnitures = new GameObject("Furnitures");
                }
                obj.transform.SetParent(_furnitures.transform, true); //�ڽ����� ����.
                obj.SetActive(true);
            }
            return obj; //����
        }
        public GameObject CreatePreviewFurniture(int index)
        {
            FurnitureSpec furniture = _repository.GetSpec(index); //���� ��ȣ�� ���缭 ������ ������ �ҷ��´�.
            GameObject obj = null; //������Ʈ ������ ����
            if (furniture != null)
            {
                obj = Instantiate(furniture.Prefeb);
                //obj.transform.localScale = Vector3.one / 5;
                obj.layer = 0;
            }
            return obj;
        }
    }
}
