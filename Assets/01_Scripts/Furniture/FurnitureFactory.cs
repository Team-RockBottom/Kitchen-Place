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


        private void Awake()
        {
            _repository = GetComponentInChildren<FurnitureSpecRepository>();
        }

        /// <summary>
        /// 오브젝트 생성 함수
        /// </summary>
        /// <param name="index"></param>
        /// <param name="position"></param>
        /// <param name="plane"></param>
        /// <returns></returns>
        public GameObject CreateFurniture(int index, Vector3 position, ARPlane plane)
        {
            //_repository.IfurnitureDic.TryGetValue(name, out FurnitureSpec furniture);
            FurnitureSpec furniture = _repository.GetSpec(index); //가구 번호에 맞춰서 가구의 정보를 불러온다.
            GameObject obj = null; //오브젝트 저장할 변수
            if (furniture != null) //가구의 정보가 있다면
            {
                obj = Instantiate(furniture.furniturePrefeb, position, furniture.furniturePrefeb.transform.rotation); //생성
                obj.transform.localScale = Vector3.one / 5; //생성후 조정(아직은 프리펩이 어떻게 될지몰라서 임의로 설정)
                float planeY = plane.gameObject.transform.position.y + (obj.transform.localScale.y / 2);
                if(index == 1) //테이블은 피봇이 위에 있어서 예외처리
                {
                    planeY += (obj.transform.localScale.y / 3);
                }
                obj.transform.position = new Vector3(position.x, planeY, position.z);
                if (_furnitures == null) //부모로 삼을 빈 오브젝트없을시 생성
                {
                    _furnitures = new GameObject("Furnitures");
                }
                obj.transform.SetParent(_furnitures.transform, true); //자식으로 들어간다.
                obj.SetActive(true); // 활성화

            }
            return obj; //리턴
        }

        /// <summary>
        /// 미리보기용 오브젝트 생성
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GameObject CreatePreviewFurniture(int index)
        {
            FurnitureSpec furniture = _repository.GetSpec(index); //가구 번호에 맞춰서 가구의 정보를 불러온다.
            GameObject obj = null; //오브젝트 저장할 변수
            if (furniture != null)
            {
                obj = Instantiate(furniture.furniturePrefeb);
                obj.transform.localScale = Vector3.one / 5;
            }
            return obj;
        }
    }
}
