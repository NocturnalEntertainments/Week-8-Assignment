using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverUI;
    public TextMeshProUGUI healthText;
    public bool isJumping;
    public int maxHealth = 100;
    private int currentHealth;

    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
        StartCoroutine(RegenerateHealth());
        UpdateHealthUI();
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void DamageHealth(int damageTaken)
    {
        currentHealth -= damageTaken;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
        GameOverHandler();
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += 1;
                UpdateHealthUI();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth + " / " + maxHealth;
        }
    }

    private void GameOverHandler()
    {
        if (currentHealth < 1)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
