using UnityEngine;

public class KeepNPCUpright : MonoBehaviour
{
    private Quaternion startRotation;
    private float startY;

    void Start()
    {
        startRotation = transform.rotation;
        startY = transform.position.y;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = startY;
        transform.position = pos;

        transform.rotation = startRotation;
    }
}