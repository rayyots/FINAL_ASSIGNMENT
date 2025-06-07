using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketTagFilter : XRSocketInteractor
{
    [Tooltip("Only objects with this tag can be snapped into this socket.")]
    public string targetTag;

    public Material highlightMaterial;
    private Material originalMaterial;
    private Renderer rend;

    protected override void Awake()
    {
        base.Awake();
        rend = GetComponent<Renderer>();
        if (rend == null)
            rend = GetComponentInChildren<Renderer>();

        if (rend != null)
            originalMaterial = rend.material;
    }

    public override bool CanHover(XRBaseInteractable interactable)
    {
        bool canHover = base.CanHover(interactable) && interactable.transform.CompareTag(targetTag);

        // Highlight socket if the right cube is hovering
        if (rend != null)
            rend.material = canHover ? highlightMaterial : originalMaterial;

        return canHover;
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        // Only allow selection if tags match
        return base.CanSelect(interactable) && interactable.transform.CompareTag(targetTag);
    }

    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        base.OnSelectEntered(interactable);

        // Snap the cube exactly to socket position and rotation
        if (interactable != null)
        {
            interactable.transform.position = transform.position;
            interactable.transform.rotation = transform.rotation;

            // Freeze cube's Rigidbody to keep it locked in place
            if (interactable.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        // Set socket to highlighted material permanently (locked)
        if (rend != null)
            rend.material = highlightMaterial;
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        base.OnSelectExited(interactable);

        // On cube removal, revert material and Rigidbody
        if (rend != null)
            rend.material = originalMaterial;

        if (interactable != null && interactable.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }
    }
}