using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public void Toggle(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}
