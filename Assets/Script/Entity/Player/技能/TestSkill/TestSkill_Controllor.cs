using UnityEngine;

public class TestSkill_Controllor : MonoBehaviour
{
    public Rigidbody2D rb;
    private int playerFacingDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerFacingDir = PlayerManager.Instance.player.facingDir;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX = 5 * playerFacingDir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemyone_CharacterStats enemyStats = other.GetComponent<Enemyone_CharacterStats>();
            if (enemyStats != null)
            {
                enemyStats.OnAttacked(10); // Example damage value
            }
        }
    }
}
