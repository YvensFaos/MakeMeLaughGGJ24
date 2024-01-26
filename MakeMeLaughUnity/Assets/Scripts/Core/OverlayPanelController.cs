using TMPro;
using UnityEngine;

namespace Core
{
    public class OverlayPanelController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text overlayText;
        
        private void OnEnable()
        {
            var mainFrame = MainFrame.GetSingleton();
            var text = $"CURRENT RECEPTOR COUNT: {mainFrame.ReceptorCount()}\r\n\r\n";
            text += mainFrame.GetListOfReceptorCommands();

            overlayText.text = text;
        }
    }
}