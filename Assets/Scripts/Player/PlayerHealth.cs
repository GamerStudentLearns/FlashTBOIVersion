using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour

{

    public int maxHearts = 6;
    [HideInInspector] public bool justTeleported = false;
    public float invulnTime = 1f;

    private int currentHearts;

    private bool invulnerable;

    public HeartUI heartUI;

    void Awake()

    {

        currentHearts = maxHearts;

        heartUI.Initialize(maxHearts);

        heartUI.UpdateHearts(currentHearts);

    }

    public void TakeDamage(int dmg)

    {

        if (invulnerable) return;

        currentHearts -= dmg;

        currentHearts = Mathf.Max(currentHearts, 0);

        heartUI.UpdateHearts(currentHearts);

        HitStopController.instance.Stop(0.05f);

        StartCoroutine(Invulnerability());

        if (currentHearts <= 0)

            Die();

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

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");

    }

    public void Heal(int amount)
    {
        currentHearts += amount;
        if(currentHearts > maxHearts)
        {
            currentHearts = maxHearts;
        }
        heartUI.UpdateHearts(currentHearts);

        Debug.Log("Healed! Current hearts: " + currentHearts);
    }

}
