using UnityEngine;
using UnityEngine.InputSystem;

public class NPCFollowHand : MonoBehaviour
{
    public Transform npcRoot;
    public Transform playerHand;
    public InputActionProperty gripAction;

    public float followSpeed = 2f;
    private bool handInArmTrigger = false;

    void Update()
    {
        float grip = gripAction.action.ReadValue<float>();

        if (handInArmTrigger)
        {
            Debug.Log("Hand in arm trigger. Grip: " + grip);
        }

        if (handInArmTrigger && grip > 0.5f && npcRoot != null && playerHand != null)
        {
            Debug.Log("Moving NPC");

            Vector3 targetPosition = playerHand.position;
            targetPosition.y = npcRoot.position.y;

            npcRoot.position = Vector3.MoveTowards(
                npcRoot.position,
                targetPosition,
                followSpeed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name + " Tag: " + other.tag);

        if (other.CompareTag("PlayerHand"))
        {
            handInArmTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exited by: " + other.name + " Tag: " + other.tag);

        if (other.CompareTag("PlayerHand"))
        {
            handInArmTrigger = false;
        }
    }
}