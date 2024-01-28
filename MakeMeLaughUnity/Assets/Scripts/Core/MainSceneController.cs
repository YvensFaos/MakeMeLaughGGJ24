using TMPro;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text consoleInputName;
    [SerializeField]
    private TMP_Text consoleText;
    
    public void SetUserName()
    {
        var newPlayerName = consoleInputName.text.Trim();
        consoleText.text += newPlayerName + "\r\n\r\n";
        LootLockerSingleton.GetSingleton().SetPlayerName(newPlayerName);
    }
}
