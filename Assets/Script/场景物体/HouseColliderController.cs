using UnityEngine;

public class HouseColliderController : MonoBehaviour
{
    public GameObject tips;

    private bool isPlayerInside;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tips.SetActive(true);
            isPlayerInside = true;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && isPlayerInside)
        {
            LoadScene.Instance.EnterScene("SecondScene");
            isPlayerInside = false; // 防止重复触发
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tips.SetActive(false);
            isPlayerInside = false;
        }
    }
}
