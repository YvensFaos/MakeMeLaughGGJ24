using System.Collections;
using TMPro;
using UnityEngine;
using Utils;

public class HighscoreController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text highScoreText;

    private bool highScoreCollected;
    
    private void Start()
    {
        if (LootLockerSingleton.GetSingleton().IsOn())
        {
            GetHighScore();
            highScoreCollected = true;
        }
        else
        {
            StartCoroutine(GetHighScoreAttempt());
        }
    }

    private IEnumerator GetHighScoreAttempt()
    {
        do
        {
            yield return new WaitForSeconds(1.0f);
            DebugUtils.DebugLogMsg("Try to get high score.");
            if (!LootLockerSingleton.GetSingleton().IsOn()) continue;
            GetHighScore();
            highScoreCollected = true;
        } while (!highScoreCollected);
    }

    private void GetHighScore()
    {
        LootLockerSingleton.GetSingleton().GetHighScores(highScores =>
        {
            highScoreText.text = "High Scores: \r\n\r\n";
            highScores.ForEach(highScore =>
            {
                highScoreText.text += highScore + "\r\n\r\n";
            });
        });
    }
}
