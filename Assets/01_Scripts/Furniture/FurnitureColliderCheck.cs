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
    //    Debug.Log("�ݶ����� ����");
    //    rb.linearVelocity = Vector3.zero; //���������� �������� �ʰ�
    //}
}
