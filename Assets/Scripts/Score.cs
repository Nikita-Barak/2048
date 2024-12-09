using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;
    private TextMeshProUGUI tm; // Use TextMeshProUGUI for UI text
    public int maxScore { get; private set; }

    private void Awake()
    {
        maxScore = 0;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return; // Avoid running further code on the destroyed instance
        }

        tm = GetComponent<TextMeshProUGUI>();
        if (tm != null)
        {
            tm.text = maxScore.ToString();
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is missing!");
        }
    }

    public void SetScore(int score)
    {
        if (score > maxScore)
        {
            maxScore = score;
            if (tm != null)
            {
                tm.text = maxScore.ToString();
            }
        }
    }
}