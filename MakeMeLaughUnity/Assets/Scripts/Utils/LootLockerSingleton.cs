using System;
using System.Collections.Generic;
using LootLocker.Requests;
using NaughtyAttributes;
using UnityEngine;
using Utils;

public class LootLockerSingleton : Singleton<LootLockerSingleton>
{
    [SerializeField, ReadOnly] private string playerID;
    [SerializeField] private string leaderboardKey;
    [SerializeField] private string playerName = "133T";
    [SerializeField, ReadOnly] private bool isOn;

    private void Awake()
    {
        ControlSingleton();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GuestLogin();
    }

    private void GuestLogin()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            playerID = response.player_id.ToString();
            isOn = true;
            DebugUtils.DebugLogMsg("Loot Locker is On!");
        });
    }

    public void SubmitHighScore(int score, string metadata)
    {
        metadata += $" {playerName}";
        LootLockerSDKManager.SubmitScore(playerName, score, leaderboardKey, metadata, (response) =>
        {
            if (response.success)
            {
                DebugUtils.DebugLogMsg("High score sent.");
            }
        });
    }

    public void GetHighScores(Action<List<string>> callback)
    {
        if (!isOn) return;
        LootLockerSDKManager.GetScoreList(leaderboardKey, 10, (response) =>
        {
            if (!response.success)
            {
                DebugUtils.DebugLogErrorMsg("No high score!");
                return;
            }
            var leaderboardText = "";
            var results = new List<string>();
            foreach (var currentEntry in response.items)
            {
                leaderboardText += currentEntry.rank + ".";
                leaderboardText += currentEntry.player.id;
                leaderboardText += " - ";
                leaderboardText += currentEntry.score;
                leaderboardText += " - ";
                leaderboardText += currentEntry.metadata;
                leaderboardText += "\n";
                    
                results.Add(leaderboardText);
                callback.Invoke(results);
            }
        });
    }

    public void SetPlayerName(string newPlayerName)
    {
        playerName = newPlayerName;
    }

    public bool IsOn() => isOn;
}