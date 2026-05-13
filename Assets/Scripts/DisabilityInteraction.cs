using UnityEngine;
using UnityEngine.InputSystem;

public class DisabilityInteraction : MonoBehaviour
{
    [Header("UI")]
    public GameObject canvasToShow;

    [Header("Player")]
    public Transform playerCamera;
    public Transform rightHand;
    public float showDistance = 4f;

    [Header("Inputs")]
    public InputActionProperty leftTriggerAction;   // Ask
    public InputActionProperty rightGripAction;     // Help / drag

    [Header("NPC")]
    public Transform npcToMove;
    public float followSpeed = 1.2f;
    public float followDistance = 1.5f;

    [Header("NPC Audio")]
    public AudioSource npcAudioSource;

    [Header("Ask - Agree")]
    public AudioClip[] agreeClips;

    [Header("Ask - Reject / Neutral")]
    public AudioClip[] rejectClips;

    [Header("No Ask Protest")]
    public AudioClip[] noAskClips;

    [Header("After Rejection Protest")]
    public AudioClip[] afterRejectProtestClips;

    [Header("Settings")]
    [Range(0f, 1f)]
    public float agreeChance = 0.3f;

    private bool playerNearby = false;
    private bool hasAsked = false;
    private bool permissionGranted = false;

    private bool leftPressedLastFrame = false;
    private bool rightPressedLastFrame = false;

    private bool isGuiding = false;

    void Start()
    {
        if (canvasToShow != null)
            canvasToShow.SetActive(false);
    }

    void Update()
    {
        if (canvasToShow == null || playerCamera == null)
            return;

        float distance = Vector3.Distance(transform.position, playerCamera.position);
        playerNearby = distance <= showDistance;

        canvasToShow.SetActive(playerNearby);

        if (playerNearby)
        {
            HandleAskInput();
            HandleGuideInput();
        }

        if (isGuiding)
        {
            FollowRightHand();
        }
    }

    void HandleAskInput()
    {
        float leftValue = leftTriggerAction.action != null
            ? leftTriggerAction.action.ReadValue<float>()
            : 0f;

        bool leftPressedNow = leftValue > 0.8f;

        if (leftPressedNow && !leftPressedLastFrame)
        {
            AskNPC();
        }

        leftPressedLastFrame = leftPressedNow;
    }

    void HandleGuideInput()
    {
        float rightValue = rightGripAction.action != null
            ? rightGripAction.action.ReadValue<float>()
            : 0f;

        bool rightPressedNow = rightValue > 0.8f;

        if (rightPressedNow && !rightPressedLastFrame)
        {
            TryGuideNPC();
        }

        if (!rightPressedNow)
        {
            isGuiding = false;
        }

        rightPressedLastFrame = rightPressedNow;
    }

    void AskNPC()
    {
        hasAsked = true;

        bool agrees = Random.value < agreeChance;

        if (agrees && agreeClips != null && agreeClips.Length > 0)
        {
            permissionGranted = true;
            PlayRandomClip(agreeClips);
            Debug.Log("NPC agreed to receive help.");
        }
        else
        {
            permissionGranted = false;
            PlayRandomClip(rejectClips);
            Debug.Log("NPC rejected or gave a neutral response.");
        }
    }

    void TryGuideNPC()
    {
        isGuiding = true;

        if (permissionGranted)
        {
            Debug.Log("Started guiding NPC with permission.");
        }
        else
        {
            if (!hasAsked)
            {
                PlayRandomClip(noAskClips);
                Debug.Log("Player dragged NPC without asking.");
            }
            else
            {
                PlayRandomClip(afterRejectProtestClips);
                Debug.Log("Player dragged NPC after rejection.");
            }
        }
    }

    void FollowRightHand()
    {
        if (npcToMove == null || playerCamera == null)
            return;

        Vector3 targetPosition =
            playerCamera.position +
            playerCamera.forward * followDistance;

        targetPosition.y = npcToMove.position.y;

        float distance = Vector3.Distance(npcToMove.position, targetPosition);

        if (distance > 0.2f)
        {
            npcToMove.position = Vector3.MoveTowards(
                npcToMove.position,
                targetPosition,
                followSpeed * Time.deltaTime
            );
        }

        Vector3 lookDirection = playerCamera.forward;
        lookDirection.y = 0;

        if (lookDirection != Vector3.zero)
        {
            npcToMove.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    void PlayRandomClip(AudioClip[] clips)
    {
        if (npcAudioSource == null || clips == null || clips.Length == 0)
        {
            Debug.LogWarning("Missing AudioSource or audio clips.");
            return;
        }

        int index = Random.Range(0, clips.Length);
        npcAudioSource.PlayOneShot(clips[index]);
    }
}