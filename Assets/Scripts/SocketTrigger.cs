using UnityEngine;

public class SocketTrigger : MonoBehaviour
{
    public Material highlightMaterial;
    private Material originalMaterial;
    private Renderer rend;

    private bool isLocked = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend == null)
            rend = GetComponentInChildren<Renderer>();

        if (rend != null)
            originalMaterial = rend.material;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isLocked) return;

        if (other.CompareTag("Red") || other.CompareTag("Green") || other.CompareTag("Blue"))
        {
            // call PuzzleManager safely
            PuzzleManager manager = FindObjectOfType<PuzzleManager>();
            if (manager != null)
                manager.CheckPlacement(other.gameObject, gameObject);

            if (other.CompareTag(tag)) // correct placement
            {
                // snap cube to socket position
                other.transform.position = transform.position;

                if (other.TryGetComponent<Rigidbody>(out var rb))
                {
                    if (!rb.isKinematic)
                    {
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        rb.isKinematic = true;
                    }
                }

                if (rend != null)
                    rend.material = highlightMaterial;

                isLocked = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isLocked) return;

        if (other.CompareTag(tag))
        {
            if (rend != null)
                rend.material = originalMaterial;
        }
    }
}
