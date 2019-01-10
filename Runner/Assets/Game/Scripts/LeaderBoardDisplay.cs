using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LeaderBoardDisplay : MonoBehaviour
{
    public List<LeaderBoardCell> leaderboardCells = new List<LeaderBoardCell>();

    string highScores;

    void OnEnable()
    {
        LoadLeaderboard();
    }

    public void LoadLeaderboard()
    {
        highScores = PlayerPrefs.GetString("Score");

        string[] rows = highScores.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        rows = rows.OrderBy(r => long.Parse(r.Split(new char[] { ' ' }, System.StringSplitOptions.None)[1])).Reverse().ToArray();

        for (int i = 0; i < rows.Length && i < leaderboardCells.Count; i++)
        {
            string[] values = rows[i].Split(new char[] { ' ' }, System.StringSplitOptions.None);

            LeaderBoardCell cell = leaderboardCells[i];
            
            cell.UpdateProperties(long.Parse(values[1]), values[0]);
        }
    } 
}
