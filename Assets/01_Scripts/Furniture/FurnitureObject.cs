using UnityEngine;

public class FurnitureObject : MonoBehaviour
{
    public FurnitureSpec Spec
    {
        get => _spec;
        set => _spec = value;
    }

    private FurnitureSpec _spec;
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
