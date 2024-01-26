using System;
using UnityEngine;
using Utils;

public class MainFrame : Singleton<MainFrame>
{
   [SerializeField]
   private int dataCollected;
   
   public void CollectData(DataPackageController data)
   {
      dataCollected++;
   }
}
