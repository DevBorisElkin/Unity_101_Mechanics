using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// A static class for general helpful methods
/// </summary>
public static class Helpers 
{
    /// <summary>
    /// Destroy all child objects of this transform (Unintentionally evil sounding).
    /// Use it like so:
    /// <code>
    /// transform.DestroyChildren();
    /// </code>
    /// </summary>
    public static void DestroyChildren(this Transform t) {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }

    #region DOTween UI Animations

    public static List<AnimatedObject> animatedObjects = new List<AnimatedObject>();
    public static void Animate_TopPanelSlideUpDown(this RectTransform rectTransform, bool up, float time = 0.5f)
    {
        CheckAndRemoveOngoingAnimation(rectTransform);
        
        float endValue = up? rectTransform.sizeDelta.y : 0f;
        Tween t = DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, new Vector2(0, endValue), time);
        t.OnComplete(() => { t.RemoveAnimatedObjectOnAnimationFinished(); });
        
        animatedObjects.Add(new AnimatedObject(t, rectTransform));
    }
    static void CheckAndRemoveOngoingAnimation(object obj)
    {
        AnimatedObject animatedObject = null;
        for (int i = 0; i < animatedObjects.Count; i++)
        {
            if(animatedObjects[i].obj != obj) continue;

            DOTween.Kill(animatedObjects[i].currentTween);
            animatedObject = animatedObjects[i];
            break;
        }

        if (animatedObject != null) animatedObjects.Remove(animatedObject);
    }
    static void RemoveAnimatedObjectOnAnimationFinished(this Tween t)
    {
        AnimatedObject animatedObject = null;
        for (int i = 0; i < animatedObjects.Count; i++)
        {
            if(animatedObjects[i].currentTween != t) continue;

            DOTween.Kill(animatedObjects[i].currentTween);
            animatedObject = animatedObjects[i];
            break;
        }
        if (animatedObject != null) animatedObjects.Remove(animatedObject);
    }
    #endregion
}
