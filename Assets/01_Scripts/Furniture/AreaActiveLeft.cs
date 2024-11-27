using UnityEngine;

public class AreaActiveLeft : MonoBehaviour
{
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;
    private Rigidbody rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnArea()
    {
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
    }
    public void OffArea()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        rb.isKinematic  = true;
    }
}
