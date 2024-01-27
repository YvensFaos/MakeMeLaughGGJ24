using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Utils;

namespace Core
{
    [CreateAssetMenu(fileName = "New Player Level Database", menuName = "MML/Player Level Database", order = 0)]
    public class PlayerLevelDatabase : ScriptableObject
    {
        [SerializeField]
        private List<PlayerLevelScorePair> playerLevelScore;
        private Stack<PlayerLevelScorePair> playerLevelScoreStack;
        
        public PlayerLevelScorePair GetCurrentPlayerLevel(int maxScore)
        {
            if (playerLevelScoreStack == null)
            {
                InitiatePlayerScoreStack();
            }

            var peek = playerLevelScoreStack.Peek();
            if (peek.IsFinalLevel() || maxScore < peek.Two)
            {
                return peek;
            }
            
            playerLevelScoreStack.Pop();
            return playerLevelScoreStack.Peek();
        }

        private void InitiatePlayerScoreStack()
        {
            Sort();
            playerLevelScoreStack = new Stack<PlayerLevelScorePair>();
            for (var i = playerLevelScore.Count -1; i >= 0; i--)
            {
                playerLevelScoreStack.Push(playerLevelScore[i]);
                DebugUtils.DebugLogMsg($"[{i}]: {playerLevelScore[i].One.name}.");
            }
        }

        public void ResetStack()
        {
            InitiatePlayerScoreStack();
        }
        
        [Button("Sort")]
        private void Sort()
        {
            playerLevelScore.Sort();
        }
    }
}