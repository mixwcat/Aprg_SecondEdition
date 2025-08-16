using UnityEngine;

public class PlayerInputActions : MonoBehaviour
{
    /// <summary>
    /// playerInput是挂载player上的脚本
    /// playerInputSystem是unity输入系统
    /// </summary>
    //private PlayerInput playerInput;
    public InputSystem_Actions playerInputSystem;


    private void Awake()
    {
        playerInputSystem = new InputSystem_Actions();
    }


    /// <summary>
    /// 启用和禁用InputSystem
    /// </summary>
    private void OnEnable()
    {
        playerInputSystem.Enable();
        BindKey(null);
        EventCenter.Subscribe("OnPause", DisablePlayerInput);
        EventCenter.Subscribe("OnPause", DisBindKey);
        EventCenter.Subscribe("OnResume", EnablePlayerInput);
        EventCenter.Subscribe("OnResume", BindKey);
    }
    private void OnDisable()
    {
        playerInputSystem.Disable();
        DisBindKey(null); // 解除绑定按键事件
        EventCenter.Unsubscribe("OnPause", DisablePlayerInput);
        EventCenter.Unsubscribe("OnPause", DisBindKey);
        EventCenter.Unsubscribe("OnResume", EnablePlayerInput);
        EventCenter.Unsubscribe("OnResume", BindKey);
    }


    #region 绑定按键、启用输入系统
    private void EnablePlayerInput(object obj)
    {
        playerInputSystem.Player.Move.Enable();
        playerInputSystem.Player.Jump.Enable();
        playerInputSystem.Player.Dash.Enable();
        playerInputSystem.Player.Attack.Enable();
        playerInputSystem.Player.Defend.Enable();
    }

    private void DisablePlayerInput(object obj)
    {
        playerInputSystem.Player.Move.Disable();
        playerInputSystem.Player.Jump.Disable();
        playerInputSystem.Player.Dash.Disable();
        playerInputSystem.Player.Attack.Disable();
        playerInputSystem.Player.Defend.Disable();
    }

    private void BindKey(object obj)
    {
        playerInputSystem.Player.Dash.performed += ctx => EventCenter.Trigger("OnPlayerDash");
        playerInputSystem.Player.Jump.performed += ctx => EventCenter.Trigger("OnPlayerJump");
        playerInputSystem.Player.Attack.performed += ctx => EventCenter.Trigger("OnPlayerNextAttackQueued");
        playerInputSystem.Player.Attack.performed += ctx => EventCenter.Trigger("OnPlayerAttack");
        playerInputSystem.Player.Defend.performed += ctx => EventCenter.Trigger("OnPlayerDefend");
        playerInputSystem.Player.Defend.canceled += ctx => EventCenter.Trigger("OnPlayerDefendExit");
    }

    private void DisBindKey(object obj)
    {
        playerInputSystem.Player.Dash.performed -= ctx => EventCenter.Trigger("OnPlayerDash");
        playerInputSystem.Player.Jump.performed -= ctx => EventCenter.Trigger("OnPlayerJump");
        playerInputSystem.Player.Attack.performed -= ctx => EventCenter.Trigger("OnPlayerNextAttackQueued");
        playerInputSystem.Player.Attack.performed -= ctx => EventCenter.Trigger("OnPlayerAttack");
        playerInputSystem.Player.Defend.performed -= ctx => EventCenter.Trigger("OnPlayerDefend");
        playerInputSystem.Player.Defend.canceled -= ctx => EventCenter.Trigger("OnPlayerDefendExit");
    }
    #endregion


    private void FixedUpdate()
    {
        // 切换ActionMap，相同按键触发不同方法
        // 这里使用了Keyboard.current来检测按键输入
        // if (Keyboard.current.mKey.wasPressedThisFrame)
        // {
        //     Debug.Log("M key pressed, switching to Player ActionMap");
        //     playerInput.SwitchCurrentActionMap("Player");
        //     playerInputSystem.Player.Enable();
        //     playerInputSystem.UI.Disable();
        // }
        // if (Keyboard.current.nKey.wasPressedThisFrame)
        // {
        //     Debug.Log("N key pressed, switching to UI ActionMap");
        //     playerInput.SwitchCurrentActionMap("UI");
        //     playerInputSystem.Player.Disable();
        //     playerInputSystem.UI.Enable();
        // }
    }
}
