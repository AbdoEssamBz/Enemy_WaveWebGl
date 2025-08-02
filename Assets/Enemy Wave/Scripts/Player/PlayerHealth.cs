using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TMP_Text Health;
    [SerializeField] int maxhealth = 5;
    private int currentHealth;

    private bool isDead=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxhealth;
        HealthText();

    }

    // Update is called once per frame
    void Update()
    {
        HealthText();
    }
    public void TakeDamage( int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log("Player health" + currentHealth);
        HealthText();

        if (currentHealth <= 0) 
        {
            currentHealth = 0;
            isDead = true;
            Debug.Log("Player Died");
            GetComponent<MeshRenderer>().enabled = false; // Hide
            RestarGame();
        }
        HealthText();

    }

    void HealthText()
    {
        Health.text = "Health : " + Mathf.Max(currentHealth, 0);
    }
    void RestarGame()
    {

        StartCoroutine(RestartafterDealy(1f));
    }

    IEnumerator RestartafterDealy (float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
