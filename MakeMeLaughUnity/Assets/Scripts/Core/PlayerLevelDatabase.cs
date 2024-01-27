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

        public PlayerLevelSO GetCurrentPlayerLevel(int maxScore)
        {
            foreach (var levelScorePair in playerLevelScore.Where(levelScorePair => levelScorePair.Two <= maxScore))
            {
                return levelScorePair.One;
            }

            return playerLevelScore[0].One;
        }

        [Button("Sort")]
        private void Sort()
        {
            playerLevelScore.Sort();
        }
    }
}