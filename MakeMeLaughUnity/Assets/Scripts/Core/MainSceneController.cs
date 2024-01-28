using TMPro;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text consoleInputName;
    [SerializeField]
    private TMP_Text consoleText;

    [SerializeField] private Material mainMaterial;
    [SerializeField] private Material resetMaterial;

    private void Start()
    {
        mainMaterial.CopyPropertiesFromMaterial(resetMaterial);
    }

    public void SetUserName()
    {
        var newPlayerName = consoleInputName.text.Trim();
        if (newPlayerName.Length <= 0)
        {
            consoleText.text += "Invalid user name.\r\n\r\n";
            return;
        }
        
        consoleText.text += $"User name set to: {newPlayerName} \r\n\r\n";
        LootLockerSingleton.GetSingleton().SetPlayerName(newPlayerName);
        consoleInputName.text = "";
    }
}
