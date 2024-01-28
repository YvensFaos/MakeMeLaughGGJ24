using System;
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
   [SerializeField, ReadOnly]
   private int currentData;
   [SerializeField] 
   private int threatLevel;
   [SerializeField, ReadOnly] 
   private int currentThreatLevel;
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
   [SerializeField]
   private ThreatLevelController threatLevelController;
   [SerializeField]
   private Material mainGameMaterial;
   [SerializeField]
   private AudioSource audioPlayer;
   
   [Header("Databases")] 
   [SerializeField] 
   private PlayerLevelDatabase playerLevelDatabase;
   [SerializeField] 
   private ReceptorController receptorPrefab;
   [SerializeField] 
   private ReceptorKeyInputDatabase keyInputDatabase;

   [Header("Sounds")] 
   [SerializeField] private AudioClip damageSound;
   [SerializeField] private AudioClip bonusSound;
   [SerializeField] private AudioClip collectSound;
   [SerializeField] private AudioClip receptorSound;

   [Header("Colors")] 
   [SerializeField] private Color damageColor;
   [SerializeField] private Color gateOpenColor;
   

   private Tweener playerLevelTweener;
   private int highscore;
   public event Action gameOverEvent;

   private void Start()
   {
      playerLevelDatabase.ResetStack();
      playerLevel = playerLevelDatabase.GetCurrentPlayerLevel(dataCollected);
      playerLevelText.text = $"LVL: {playerLevel.One.name}";
      dataText.text = $"<b>D:</b>{currentData}";
      levelProgressImage.fillAmount = 0.0f;
      currentThreatLevel = threatLevel;
      threatLevelController.UpdateThreatLevel((float) currentThreatLevel / threatLevel);
      
      ResetMainMaterial();
   }

   public void ResetMainMaterial()
   {
      mainGameMaterial.SetColor("_ScreenColor", Color.white);
      mainGameMaterial.SetColor("_AdditiveColor", Color.black);
      mainGameMaterial.SetInt("_ByPass", 0);
      mainGameMaterial.SetFloat("_NoiseFactor", 0.0f);
      mainGameMaterial.SetFloat("_NoiseScale", 0.0f);
      mainGameMaterial.SetFloat("_NoiseTimeFactor", 0.0f);
   }

   public void AddNewReceptor(ReceptorController newReceptor)
   {
      currentReceptors.Add(newReceptor);
   }

   public bool ConvertRandomReceptorToGate()
   {
      if (currentReceptors.Count <= 1) return false;

      var convertedReceptor = RandomHelper<ReceptorController>.GetRandomFromList(currentReceptors);
      ConvertReceptorToGate(convertedReceptor);
      return true;
   }

   private void ConvertReceptorToGate(ReceptorController receptorController)
   {
      currentReceptors.Remove(receptorController);
      impulseSource.GenerateImpulse();
      receptorController.ConvertToGate();
      Destroy(receptorController.gameObject, 0.1f);
      Console().AddConsoleLine("Gate opened.", "$");
      audioPlayer.PlayOneShot(bonusSound);
      
      currentThreatLevel = Mathf.Clamp(currentThreatLevel + 1, 0, threatLevel);
      threatLevelController.UpdateThreatLevel((float) currentThreatLevel / threatLevel);
   }
   
   public void DestroyReceptor(ReceptorController lostReceptor, int threatDamage = 1)
   {
      void GameOverConsoleText(Tweener damageTween)
      {
         Console().AddConsoleLine("U R COMPROMISED", "", true);
         Console().AddConsoleLine("+___+");
         Console().AddConsoleLine("HA HA HA HA");
         Console().AddConsoleLine("U R COMPROMISED");
         Console().AddConsoleLine("HA HA HA HA");
         Console().AddConsoleLine("+___+");
         Console().AddConsoleLine($"Final Score: {highscore}.");
         Console().AddConsoleLine("DON T MAKE ME LAUGH");
         Console().AddConsoleLine("+___+");
         Console().AddConsoleLine("File > Reset");
         Console().AddConsoleLine("File > Quit");
         Console().AddConsoleLine("+___+");
         
         gameOverEvent.Invoke();
         damageTween.Kill();
         AnimateMaterialProperty.AnimateProperty(mainGameMaterial, "_AdditiveColor",
            damageColor,
            0.2f, () => { });
      }

      void GameOverSendHighScore()
      {
         LootLockerSingleton.GetSingleton().SubmitHighScore(highscore, playerLevel.One.name);
      }

      void RemoveReceptor(ReceptorController receptor)
      {
         currentReceptors.Remove(receptor);
         impulseSource.GenerateImpulse();
         Destroy(receptor.gameObject);
         
         Console().AddConsoleLine("+___+");
         Console().AddConsoleLine("Receptor lost.");
         audioPlayer.PlayOneShot(damageSound);
      }
      RemoveReceptor(lostReceptor);
      var animateDamageTweener = AnimateDamage(damageColor);
      
      if (currentReceptors.Count == 0)
      {
         //Game Over
         currentThreatLevel = 0;
         threatLevelController.UpdateThreatLevel((float) currentThreatLevel / threatLevel);
         Console().AddConsoleLine("+___+");
         Console().AddConsoleLine("No more receptors.");
         GameOverConsoleText(animateDamageTweener);
         GameOverSendHighScore();
      }
      else
      {
         currentThreatLevel -= threatDamage;
         threatLevelController.UpdateThreatLevel((float) currentThreatLevel / threatLevel);
         if (currentThreatLevel > 0) return;

         foreach (var receptor in currentReceptors)
         {
            RemoveReceptor(receptor);
            animateDamageTweener.Kill();
            animateDamageTweener = AnimateDamage(damageColor);
         }
         
         //Game Over
         Console().AddConsoleLine("+___+");
         Console().AddConsoleLine("Threat level too high.");
         GameOverConsoleText(animateDamageTweener);
         GameOverSendHighScore();
      }
   }

   //new Vector4(0.4433962f, 0.04392132f, 0.04392132f, 1.0f),
   
   private Tweener AnimateDamage(Color toColor)
   {
      return AnimateMaterialProperty.AnimateProperty(mainGameMaterial, "_AdditiveColor", toColor,
         0.2f,
         () =>
         {
            AnimateMaterialProperty.AnimateProperty(mainGameMaterial, "_AdditiveColor",
               new Vector4(0.0f, 0.0f, 0.0f, 1.0f), 0.2f);
         });
   }
   
   public void CollectData(DataPackageController data)
   {
      highscore += data.DataValue();
         
      dataCollected += data.DataValue();
      levelUpRatio = (float) dataCollected / playerLevel.Two;
      
      playerLevelTweener?.Kill();
      playerLevelTweener = levelProgressImage.DOFillAmount(Mathf.Clamp01(levelUpRatio), 0.2f);   
      
      if (levelUpRatio >= 1.0f)
      {
         var oldMax = playerLevel.Two;
         playerLevel = playerLevelDatabase.GetCurrentPlayerLevel(dataCollected);
         dataCollected -= oldMax;
         DebugUtils.DebugLogMsg($"Level Up! Level now is {playerLevel.One.name}");
         playerLevelText.text = $"LVL: {playerLevel.One.name}";
         playerLevelTweener.Kill();
         levelProgressImage.fillAmount = 0.0f;
         audioPlayer.PlayOneShot(bonusSound);
      }

      currentData += data.DataValue();
      dataText.text = $"<b>D:</b>{currentData}";
      dataReceptorRatio = (float) currentData / currentReceptorCost;
      loaderController.UpdateRatio(dataReceptorRatio);
      audioPlayer.PlayOneShot(collectSound);
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
      audioPlayer.PlayOneShot(receptorSound);
   }

   public void ToggleOverlayPanel()
   {
      overlayPanel.SetActive(!overlayPanel.activeSelf);
   }
   
   public int ReceptorCount() => currentReceptors.Count;
   public PlayerLevelSO CurrentPlayerLevel() => playerLevel.One;
   public ConsoleController Console() => console;
   public Material MainMaterial() => mainGameMaterial;
}
