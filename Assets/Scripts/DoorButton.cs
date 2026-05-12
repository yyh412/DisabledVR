using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public SceneTransitionManager transitionManager;
    private bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched by: " + other.name + " | Tag: " + other.tag);

        if (used) return;

        if (other.CompareTag("PlayerHand"))
        {
            used = true;
            Debug.Log("Enter button triggered. Loading Office.");
            transitionManager.GoToScene(1);
        }
    }
}