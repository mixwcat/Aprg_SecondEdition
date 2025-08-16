using UnityEngine;

public class KeyBoardTest : MonoBehaviour
{
    public GameObject testPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            testPrefab.SetActive(!testPrefab.activeSelf);
        }
    }
}
