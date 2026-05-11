using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    // Trigger 输入
    public InputActionProperty triggerValue;

    // Grip 输入
    public InputActionProperty gripValue;

    // Animator
    public Animator handAnimator;

    void Start()
    {
        // 开启输入
        triggerValue.action.Enable();
        gripValue.action.Enable();
    }

    void Update()
    {
        // 读取 Trigger 数值
        float trigger = triggerValue.action.ReadValue<float>();

        // 读取 Grip 数值
        float grip = gripValue.action.ReadValue<float>();

        // 传给 Animator 参数
        handAnimator.SetFloat("Trigger", trigger);
        handAnimator.SetFloat("Grip", grip);
    }
}