using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MainFrame : WeakSingleton<MainFrame>
{
   [SerializeField]
   private int dataCollected;
   [SerializeField]
   private List<ReceptorController> currentReceptors;

   public void AddNewReceptor(ReceptorController newReceptor)
   {
      currentReceptors.Add(newReceptor);
   }
   
   public void CollectData(DataPackageController data)
   {
      dataCollected++;
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

   public int ReceptorCount() => currentReceptors.Count;
}
