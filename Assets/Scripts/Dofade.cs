using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dofade : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private Tween fadeTween;
    public Tween FadeIn(float duration)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1, duration));
        sequence.Join(Fade(1f, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }));
        return sequence;
    }
    public Tween FadeOut(float duration)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0, duration));
        sequence.Join(Fade(0, duration, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }));
        return sequence;
    }

    private Tween Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false); // false : off tween
        }

        fadeTween = canvasGroup.DOFade(endValue, duration).OnComplete(onEnd);
        return fadeTween;
    }
}
