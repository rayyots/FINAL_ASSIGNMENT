using UnityEngine;

public class PlaceholderTrigger : MonoBehaviour
{
    public string correctCubeTag;
    private bool isPlaced = false;
    public AudioClip buzzSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (isPlaced) return;

        if (other.CompareTag(correctCubeTag))
        {
            // Lock cube in place
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = transform.position;
            isPlaced = true;

            PuzzleManager.instance.RegisterPlacement();
        }
        else
        {
            audioSource.PlayOneShot(buzzSound);
            other.transform.position = other.GetComponent<OriginalPosition>().startPos;
        }

    }
}
