using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class LoaderController : MonoBehaviour
    {
        [SerializeField]
        private Image loaderMask;
        [SerializeField] 
        private GameObject readyText;

        public void UpdateRatio(float ratio)
        {
            var fullyLoaded = ratio > 1.0f;
            ratio = Mathf.Clamp01(ratio);
            loaderMask.fillAmount = ratio;
            readyText.SetActive(fullyLoaded);
        }
    }
}
