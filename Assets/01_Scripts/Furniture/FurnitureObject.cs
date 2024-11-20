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
        rb.linearVelocity = Vector3.zero; //떨어졌을때 움직이지 않게
    }
}
