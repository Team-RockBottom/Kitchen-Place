using CP.Furniture;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

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

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.GetComponent<ARPlane>())
        {
            TouchInputManager.instance.PrefebColliderCheckFalse();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(!other.gameObject.GetComponent<ARPlane>())
        {
            rb.linearVelocity = Vector3.zero; //떨어졌을때 움직이지 않게
            TouchInputManager.instance.PrefebColliderCheckTrue();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.GetComponent<ARPlane>())
        {
            rb.linearVelocity = Vector3.zero; //떨어졌을때 움직이지 않게

        }       
    }
}
