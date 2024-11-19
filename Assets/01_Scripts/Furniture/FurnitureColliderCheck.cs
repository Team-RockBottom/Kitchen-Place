using UnityEngine;

public class FurnitureColliderCheck : MonoBehaviour
{
    public int Index
    {
        get => _index;
        set => _index = value;
    }

    private int _index;

    //private Rigidbody rb;


    //private void OnEnable()
    //{
    //    rb = gameObject.GetComponent<Rigidbody>();
    //}
    //private void OnCollisionStay(Collision collision)
    //{
        
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log("콜라이전 나감");
    //    rb.linearVelocity = Vector3.zero; //떨어졌을때 움직이지 않게
    //}
}
