using System.Collections.Generic;
using Cinemachine;
using Core;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class MainFrame : WeakSingleton<MainFrame>
{
   [Header("Player Data")]
   [SerializeField, ReadOnly] 
   private PlayerLevelScorePair playerLevel;
   [SerializeField, ReadOnly]
   private int dataCollected;
   [SerializeField]
   private int currentData;
   [SerializeField] 
   private float threatLevel;
   [SerializeField]
   private float levelUpRatio;

   [Header("Controls")] 
   [SerializeField]
   private float receptorCostMultiplier;
   [SerializeField]
   private int currentReceptorCost;
   [SerializeField]
   private float increaseCostChance;
   [SerializeField, ReadOnly]
   private float dataReceptorRatio;
   [SerializeField] 
   private Vector2 keyControllerAmount;
   
   [Header("References")]
   [SerializeField]
   private List<ReceptorController> currentReceptors;
   [SerializeField] 
   private ConsoleController console;
   [SerializeField] 
   private TMP_Text playerLevelText;
   [SerializeField] 
   private LoaderController loaderController;
   [SerializeField] 
   private CinemachineImpulseSource impulseSource;
   [SerializeField] 
   private BoxCollider2D spawnArea;
   [SerializeField] 
   private Transform spawnParent;
   [SerializeField] 
   private GameObject overlayPanel;
   [SerializeField] 
   private TMP_Text dataText;
   [SerializeField] 
   private Image levelProgressImage;

   [Header("Databases")] 
   [SerializeField] 
   private PlayerLevelDatabase playerLevelDatabase;
   [SerializeField] 
   private ReceptorController receptorPrefab;
   [SerializeField] 
   private ReceptorKeyInputDatabase keyInputDatabase;

   private Tweener playerLevelTweener;

   private void Start()
   {
      playerLevel = playerLevelDatabase.GetCurrentPlayerLevel(dataCollected);
      playerLevelText.text = $"LVL: {playerLevel.One.name}";
      dataText.text = $"<b>D:</b>{currentData}";
      levelProgressImage.fillAmount = 0.0f;
   }

   public void AddNewReceptor(ReceptorController newReceptor)
   {
      currentReceptors.Add(newReceptor);
   }
   
   public void CollectData(DataPackageController data)
   {
      dataCollected += data.DataValue();
      levelUpRatio = (float) dataCollected / playerLevel.Two;
      
      playerLevelTweener?.Kill();
      playerLevelTweener = levelProgressImage.DOFillAmount(Mathf.Clamp01(levelUpRatio), 0.2f);   
      if (levelUpRatio >= 1.0f)
      {
         playerLevel = playerLevelDatabase.GetCurrentPlayerLevel(dataCollected);   
      }

      currentData += data.DataValue();
      dataText.text = $"<b>D:</b>{currentData}";
      dataReceptorRatio = (float) currentData / currentReceptorCost;
      loaderController.UpdateRatio(dataReceptorRatio);
   }

   public string GetListOfReceptorCommands()
   {
      var listOfReceptorCommands = "";
      var i = 0;
      currentReceptors.ForEach(receptor =>
      {
         listOfReceptorCommands += $"RECEPTOR [{i++}] = {receptor}\r\n\r\n";
      });

      return listOfReceptorCommands;
   }

   private void Update()
   {
      if (Input.GetKeyUp(KeyCode.Space))
      {
         TrySpawnReceptor(1, out _);
      }
   }

   public bool TrySpawnReceptor(int amount, out int generatedAmount)
   {
      generatedAmount = 0;
      
      for (var i = 0; i < amount; i++)
      {
         if (dataReceptorRatio < 1.0f) return false;
         
         currentData -= currentReceptorCost;
         dataText.text = $"<b>D:</b>{currentData}";
         if (RandomChanceUtils.GetChance(increaseCostChance))
         {
            currentReceptorCost = (int)(currentReceptorCost * receptorCostMultiplier);
         }
         dataReceptorRatio = (float) currentData / currentReceptorCost;
         loaderController.UpdateRatio(dataReceptorRatio);

         impulseSource.GenerateImpulse();
         SpawnReceptor();
         generatedAmount++;
      }

      return true;
   }

   private void SpawnReceptor()
   {
      var position = RandomPointUtils.GetRandomPointWithBox2D(spawnArea);
      var receptorController = Instantiate(receptorPrefab, new Vector3(position.x, position.y, 0.0f), Quaternion.identity);

      var numberOfKeys = (int) RandomChanceUtils.GetRandomInRange(keyControllerAmount);
      var keys = new List<KeyCodePair>();
      for (var i = 0; i < numberOfKeys; i++)
      {
         keys.Add(keyInputDatabase.GetRandomKeyInput());
      }
      receptorController.Initialize(keys);

      spawnParent.SetParent(spawnParent);
      Console().AddConsoleLine("Receptor Installed.", "$");
      Console().AddConsoleLine(receptorController.ToSmallString());
      Console().AddConsoleLine($"Data.", "$");
   }

   public void ToggleOverlayPanel()
   {
      overlayPanel.SetActive(!overlayPanel.activeSelf);
   }
   
   public int ReceptorCount() => currentReceptors.Count;
   public PlayerLevelSO CurrentPlayerLevel() => playerLevel.One;
   public ConsoleController Console() => console;
}
