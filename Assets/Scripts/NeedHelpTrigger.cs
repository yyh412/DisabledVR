using UnityEngine;

public class NeedHelpTrigger : MonoBehaviour
{
    public GameObject canvasToShow;
    public Transform player;
    public float showDistance = 2.5f;

    void Start()
    {
        if (canvasToShow != null)
            canvasToShow.SetActive(false);
    }

    void Update()
    {
        if (canvasToShow == null || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= showDistance)
            canvasToShow.SetActive(true);
        else
            canvasToShow.SetActive(false);
    }
}