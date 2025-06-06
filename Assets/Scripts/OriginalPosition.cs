using UnityEngine;

public class OriginalPosition : MonoBehaviour
{
    public Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }
}
