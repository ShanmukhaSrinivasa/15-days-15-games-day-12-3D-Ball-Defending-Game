using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Script Reference")]
    [SerializeField] private SpawnManager spawnManager;

    [Header("Score info")]
    private int score = 0;
    private int highScore = 0;
    [SerializeField] private TextMeshProUGUI scoreCount;
    [SerializeField] private TextMeshProUGUI go_scoreCount;
    [SerializeField] private TextMeshProUGUI highScoreCount;
    [SerializeField] private TextMeshProUGUI go_highScoreCount;
    public TextMeshProUGUI waveNumberCount;
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI bossWaveIndicator;

    [Header("Canvas Groups")]
    [SerializeField] public CanvasGroup startGameCG;
    [SerializeField] public CanvasGroup gameCG;
    [SerializeField] public CanvasGroup gameOverCG;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 0;
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        highScoreCount.text = highScore.ToString();

        if (spawnManager == null)
        {
            Debug.Log("Spawn Manager Reference is not set in the GameManager Inspector");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        StartCoroutine(GameStartDelayCoroutine());
    }

    private IEnumerator GameStartDelayCoroutine()
    {
        ShowCG(gameCG);
        HideCG(startGameCG);
        HideCG(gameOverCG);

        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1;

        if (spawnManager != null)
        {
            spawnManager.BeginSpawning();
        }

        InvokeRepeating("IncrementScore", 2f, 1f);
    }

    public void GameOver()
    {
        ShowCG(gameOverCG);
        HideCG(gameCG);
        HideCG(startGameCG);

        Time.timeScale = 0;
        go_scoreCount.text = score.ToString();
        go_highScoreCount.text = highScore.ToString();
    }

    public void IncrementScore()
    {
        score++;
        scoreCount.text = score.ToString();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
    }

    public void ShowCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void HideCG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void PlayAgainButtonCallBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
