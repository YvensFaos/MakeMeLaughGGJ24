using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "New Player Level Database", menuName = "MML/Player Level Database", order = 0)]
    public class PlayerLevelDatabase : ScriptableObject
    {
        [SerializeField]
        private List<PlayerLevelScorePair> playerLevelScore;

        public PlayerLevelScorePair GetCurrentPlayerLevel(int maxScore)
        {
            if (maxScore >= playerLevelScore[^1].Two && playerLevelScore[^1].IsFinalLevel())
            {
                return playerLevelScore[^1];
            }
            
            foreach (var levelScorePair in playerLevelScore.Where(levelScorePair => maxScore < levelScorePair.Two ))
            {
                return levelScorePair;
            }

            return playerLevelScore[0];
        }
        
        [Button("Sort")]
        private void Sort()
        {
            playerLevelScore.Sort();
        }
    }
}