using UnityEngine;

public class NewInputSystemTest : MonoBehaviour
{
    public InputSystem_Actions playerInputSystem;
    public GameObject testObject;
    private void Awake()
    {
        playerInputSystem = new InputSystem_Actions();
        playerInputSystem.Enable();
    }

    private void Start()
    {
        playerInputSystem.Player.Test.performed += ctx => PrefabTest();
    }

    void PrefabTest()
    {
        Debug.Log("PrefabTest called");
        testObject.SetActive(!testObject.activeSelf);
    }
}
