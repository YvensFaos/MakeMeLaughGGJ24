using DG.Tweening;
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

        private Tweener doFill;
        
        public void UpdateRatio(float ratio)
        {
            var fullyLoaded = ratio >= 1.0f;
            ratio = Mathf.Clamp01(ratio);
            doFill?.Kill();
            doFill = loaderMask.DOFillAmount(ratio, .3f);
            readyText.SetActive(fullyLoaded);
        }
    }
}
