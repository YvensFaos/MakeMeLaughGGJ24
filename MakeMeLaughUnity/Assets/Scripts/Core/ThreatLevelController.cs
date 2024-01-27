using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ThreatLevelController : MonoBehaviour
{
    [SerializeField]
    private Image threatLevelImage;
    [SerializeField]
    private Gradient threatGradient;

    private Tweener colorTweener;
    
    public void UpdateThreatLevel(float ratio)
    {
        colorTweener?.Kill();
        colorTweener = threatLevelImage.DOColor(threatGradient.Evaluate(ratio), 0.5f);
    }
}
