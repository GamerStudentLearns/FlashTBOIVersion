using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHearts = 6;
    public float invulnTime = 1f;
    private int currentHearts;
    private bool invulnerable;
    void Awake()
    {
        currentHearts = maxHearts;
    }
    public void TakeDamage(int dmg)
    {
        if (invulnerable) return;
        currentHearts -= dmg;
        Debug.Log("Player took " + dmg + " damage. Current hearts: " + currentHearts);
        HitStopController.instance.Stop(0.05f);
        StartCoroutine(Invulnerability());
        if (currentHearts <= 0)
        {
            Die();
        }
    }
    IEnumerator Invulnerability()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnTime);
        invulnerable = false;
    }
    void Die()
    {
        Debug.Log("Player Dead");
        Destroy(gameObject);
        SceneManager.LoadScene("GameOverScene");
    }
}