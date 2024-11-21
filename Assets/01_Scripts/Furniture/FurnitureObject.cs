using UnityEngine;

public class FurnitureObject : MonoBehaviour
{
    public FurnitureSpec Spec
    {
        get => _spec;
        set => _spec = value;
    }

    private FurnitureSpec _spec;
    private Rigidbody rb;


    private void OnEnable()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("�ݶ����� �ͽ�Ʈ");
        rb.linearVelocity = Vector3.zero; //���������� �������� �ʰ�
    }
}
