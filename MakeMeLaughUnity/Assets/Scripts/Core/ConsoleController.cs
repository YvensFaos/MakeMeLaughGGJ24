using TMPro;
using UnityEngine;

public class ConsoleController : MonoBehaviour
{
    [SerializeField] 
    private GameObject consoleObject;
    [SerializeField]
    private TMP_Text consoleOutput;
    
    public void AddConsoleLine(string line, string start = "", bool forceOpenConsole = false)
    {
        if (forceOpenConsole)
        {
            consoleObject.SetActive(true);
        }

        consoleOutput.text += $"\r\n\r\n{start}{line}";
    }
}
