using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    private int collectedCoins;
    private int requiredCoins;
    private int score;

    private bool gameCompleted = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        collectedCoins = 0;
        gameCompleted = false;

        // 🔹 Level-wise coin requirement
        switch (scene.buildIndex)
        {
            case 0: requiredCoins = 3; break;   // Level 1
            case 1: requiredCoins = 5; break;   // Level 2
            case 2: requiredCoins = 8; break;   // Level 3
            default: requiredCoins = 0; break;
        }

        // 🔹 Reconnect ScoreText after scene load
        if (scoreText == null)
            scoreText = GameObject.Find("ScoreText")
                ?.GetComponent<TextMeshProUGUI>();

        UpdateScoreUI();
    }

    public void CoinCollected()
    {
        if (gameCompleted) return;

        collectedCoins++;
        score += 10;

        UpdateScoreUI();

        if (collectedCoins >= requiredCoins)
        {
            Invoke(nameof(LoadNextLevel), 0.5f);
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text =
                $"Score : {score}\nCoins : {collectedCoins}/{requiredCoins}";
        }
    }

    void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            gameCompleted = true;
            scoreText.text = "GAME OVER";
            scoreText.color = Color.red;
            scoreText.fontSize = 30;
            Time.timeScale = 0f;
            /*Color.red
            Color.green
            Color.yellow
            Color.blue
            Color.white
            Color.cyan
            Color.magenta */

        }
    }
}
