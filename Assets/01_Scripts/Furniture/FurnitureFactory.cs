using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace CP.Furniture
{
    public class FurnitureFactory : MonoBehaviour
    {
        private FurnitureSpecRepository _repository; 
        private GameObject _furnitures; //부모로 삼을 빈 오브젝트
        private GraphicRaycaster _graycast;
        [SerializeField] UI_BasketController _basketController;

        private void Awake()
        {
            _repository = GetComponentInChildren<FurnitureSpecRepository>();

        }


        /// <summary>
        /// 오브젝트 생성 함수
        /// </summary>
        /// <param name="name">불러올 가구 이름</param>
        /// <param name="position">생성될 위치</param>
        /// <param name="plane">평면 정보</param>
        /// <returns></returns>
        public GameObject CreateFurniture(int index, Vector3 position, ARPlane plane)
        {
            FurnitureSpec furniture = _repository.GetSpec(index); //가구 번호에 맞춰서 가구의 정보를 불러온다.
            GameObject obj = null; //오브젝트 저장할 변수
            if (furniture != null) //가구의 정보가 있다면
            {
                obj = Instantiate(furniture.Prefeb, position, furniture.Prefeb.transform.rotation); //생성

                if(_basketController)
                {
                    _basketController.AddFurnitureBasket(furniture); //장바구니 리스트에 생성
                }
                //obj.transform.localScale = Vector3.one / 5; //생성후 조정(아직은 프리펩이 어떻게 될지몰라서 임의로 설정)

                //float planeY = plane.gameObject.transform.position.y + (obj.transform.localScale.y / 2);
                //if(index == 1) //테이블은 피봇이 위에 있어서 예외처리
                //{
                //    planeY += (obj.transform.localScale.y / 3);
                //}
                //obj.transform.position = new Vector3(position.x, planeY, position.z);
                obj.transform.position = position;
                if (_furnitures == null) //부모로 삼을 빈 오브젝트없을시 생성
                {
                    _furnitures = new GameObject("Furnitures");
                }
                obj.transform.SetParent(_furnitures.transform, true); //자식으로 들어간다.
                obj.SetActive(true);
            }
            return obj; //리턴
        }
        public GameObject CreatePreviewFurniture(int index)
        {
            FurnitureSpec furniture = _repository.GetSpec(index); //가구 번호에 맞춰서 가구의 정보를 불러온다.
            GameObject obj = null; //오브젝트 저장할 변수
            if (furniture != null)
            {
                obj = Instantiate(furniture.Prefeb);
                //obj.transform.localScale = Vector3.one / 5;
            }
            return obj;
        }
    }
}
