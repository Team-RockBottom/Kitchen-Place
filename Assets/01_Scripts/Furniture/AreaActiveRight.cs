using UnityEngine;

public class AreaActiveRight : MonoBehaviour
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
        rb.isKinematic = true;
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }
}
