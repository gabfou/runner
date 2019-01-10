using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public InputField username;

    public LeaderBoardDisplay leaderBoardDisplay;

    public void AddEntry()
    {
        AddEntryLong(username.text, LevelAndScoreManager.instance.score);
    }

    public void AddEntryLong(string pseudo, float score)
    {
        pseudo = Clean(pseudo);

        PlayerPrefs.SetString("Score", PlayerPrefs.GetString("Score") + "\n" + pseudo + " " + score.ToString());
        leaderBoardDisplay.gameObject.SetActive(true);
        leaderBoardDisplay.LoadLeaderboard();
        username.gameObject.SetActive(false);
    }

    string Clean(string s)
    {
        s = s.Replace("\n", "");
        s = s.Replace(" ", "");
        return s;

    }
}
