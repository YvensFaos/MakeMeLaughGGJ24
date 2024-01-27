using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Utils;

namespace Core
{
    public class OSController : MonoBehaviour
    {
        [SerializeField]
        private List<PlayerLevelOSEventListPair> osEventsPerLevel;
        [SerializeField] 
        private OSEvent defaultEvent;
        [SerializeField] 
        private Vector2 interval;
        [SerializeField] 
        private bool onStart;
        [SerializeField, ReadOnly] 
        private bool executing;
        [SerializeField] 
        private BoxCollider2D spawnArea;
        [SerializeField] 
        private Transform spawnParent;
        
        private void Start()
        {
            osEventsPerLevel.ForEach(playerLevelOSEventListPair => playerLevelOSEventListPair.GenerateTupleList());
            
            if (onStart)
            {
                StartOSController();
            }
        }

        private void StartOSController()
        {
            executing = true;
            StartCoroutine(OSCoroutine());
        }

        private IEnumerator OSCoroutine()
        {
            while (executing)
            {
                yield return new WaitForSeconds(RandomChanceUtils.GetRandomInRange(interval));
                ExecuteOSEvent();
            }
        }

        private void ExecuteOSEvent()
        {
            var level = MainFrame.GetSingleton().CurrentPlayerLevel();
            var playerLevelOSEventListPair = osEventsPerLevel.Find(osEventsPerLevel => osEventsPerLevel.One.Equals(level));
            if (playerLevelOSEventListPair != null)
            {
                var tupleList = playerLevelOSEventListPair.TupleList();
                var osEvent = RandomChancePair<OSEvent>.GetRandomFromChanceList(tupleList, defaultEvent);
                osEvent.Execute(spawnArea, spawnParent);
            }
            else
            {
                DebugUtils.DebugLogErrorMsg($"Player Level Not Found! {level}");
            }
        }
    }
}