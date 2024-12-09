using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance;
    
    [SerializeField] private int goalScore = 256;
    [SerializeField] public float leftBorder;
    [SerializeField] public float rightBorder;
    [SerializeField] public string nextLevel;
    private int currentLevel = 1; 

    
    private void Awake()
    {
        // Ensure there is only one instance of the spawner
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate spawners
        }
    }
    
    void Update()
    {
        CheckLevelCompletion();
    }

    // Check if the goal score for the current level has been achieved
    private void CheckLevelCompletion()
    {
        if (Score.instance != null && Score.instance.maxScore >= goalScore)
        {
            AdvanceToNextLevel();
        }
    }

    // Advance to the next level and update the goal score
    private void AdvanceToNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Level1");
    }
}