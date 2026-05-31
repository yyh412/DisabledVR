using UnityEngine;

public class NPCPushReaction : MonoBehaviour
{
    public Animator animator;
    public string triggerName = "Fall";

    private bool hasReacted = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched by: " + other.name + " | Tag: " + other.tag);

        if (hasReacted) return;

        if (other.CompareTag("PlayerHand"))
        {
            hasReacted = true;
            animator.SetTrigger(triggerName);
            Debug.Log("Fall triggered");
        }
    }
}