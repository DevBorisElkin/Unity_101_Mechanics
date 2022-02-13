using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

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

    #region TopPanel Slide Up And Down
    
    public static void Animate_TopPanelSlideUpDown(this RectTransform rectTransform, bool up)
    {
        float endValue = up? rectTransform.sizeDelta.y : 0f;
        Tween t = DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, new Vector2(0, endValue), 0.5f);
    }
    public static void Animate_TopPanelSlideUpDown(this RectTransform rectTransform, bool up, float time)
    {
        float endValue = up? rectTransform.sizeDelta.y : 0f;
        Tween t = DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, new Vector2(0, endValue), time);
    }
    public static void Animate_TopPanelSlideUpDown(this RectTransform rectTransform, bool up, Action onComplete)
    {
        float endValue = up? rectTransform.sizeDelta.y : 0f;
        Tween t = DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, new Vector2(0, endValue), 0.5f);
        if (onComplete != null)
            t.OnComplete(onComplete.Invoke);
    }
    public static void Animate_TopPanelSlideUpDown(this RectTransform rectTransform, bool up, float time, Action onComplete)
    {
        float endValue = up? rectTransform.sizeDelta.y : 0f;
        Tween t = DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, new Vector2(0, endValue), time);
        if (onComplete != null)
            t.OnComplete(onComplete.Invoke);
    }
    public static void Animate_TopPanelSlideUpDown(this RectTransform rectTransform, bool up, float time, Ease ease)
    {
        float endValue = up? rectTransform.sizeDelta.y : 0f;
        Tween t = DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, new Vector2(0, endValue), time).SetEase(ease);
    }
    public static void Animate_TopPanelSlideUpDown(this RectTransform rectTransform, bool up, float time, Action onComplete, Ease ease)
    {
        float endValue = up? rectTransform.sizeDelta.y : 0f;
        Tween t = DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, new Vector2(0, endValue), time).SetEase(ease);
        if (onComplete != null)
            t.OnComplete(onComplete.Invoke);
    }
    
    #endregion

    #region Animate Modal Window Pup In and Out, Without background
    public static void Animate_ModalWindow(this Transform modalWindow, bool open)
    {
        float endValue = PrepareModalWindow(modalWindow, open);
        Tween t = modalWindow.DOScale(endValue, 0.5f);
    }
    public static void Animate_ModalWindow(this Transform modalWindow, bool open, float time)
    {
        float endValue = PrepareModalWindow(modalWindow, open);
        Tween t = modalWindow.DOScale(endValue, time);
    }
    public static void Animate_ModalWindow(this Transform modalWindow, bool open, float time, Ease ease)
    {
        float endValue = PrepareModalWindow(modalWindow, open);
        Tween t = modalWindow.DOScale(endValue, time).SetEase(ease);
    }
    public static void Animate_ModalWindow(this Transform modalWindow, bool open, float time, Action onComplete)
    {
        float endValue = PrepareModalWindow(modalWindow, open);
        Tween t = modalWindow.DOScale(endValue, time);
        if (onComplete != null)
            t.OnComplete(onComplete.Invoke);
    }
    public static void Animate_ModalWindow(this Transform modalWindow, bool open, float time, Action onComplete, Ease ease)
    {
        float endValue = PrepareModalWindow(modalWindow, open);
        Tween t = modalWindow.DOScale(endValue, time).SetEase(ease);
        if (onComplete != null)
            t.OnComplete(onComplete.Invoke);
    }

    /// <summary>
    /// returns end value, 1 or 0
    /// </summary>
    
    #endregion

    #region  Animate Background

    public static void Animate_Background(this Image background, bool open, float alphaOnOpened, float time)
    {
        float endValueBackgroundAlpha = PrepareModalWindowBackground(background, open, alphaOnOpened);
        background.DOFade(endValueBackgroundAlpha, time).OnComplete(() =>
        {
            if (!open) background.gameObject.SetActive(false);
        });
    }
    public static void Animate_Background(this Image background, bool open, float alphaOnOpened, float time, Ease ease)
    {
        float endValueBackgroundAlpha = PrepareModalWindowBackground(background, open, alphaOnOpened);
        background.DOFade(endValueBackgroundAlpha, time).SetEase(ease).OnComplete(() =>
        {
            if (!open) background.gameObject.SetActive(false);
        });
    }
    public static void Animate_Background(this Image background, bool open, float alphaOnOpened, float time, Ease ease, Action onComplete)
    {
        float endValueBackgroundAlpha = PrepareModalWindowBackground(background, open, alphaOnOpened);
        background.DOFade(endValueBackgroundAlpha, time).SetEase(ease).OnComplete(() =>
        {
            if (!open) background.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }

    #endregion
    
    #region Helpers for modal window and background animations
    
    static float PrepareModalWindow(Transform modalWindow, bool open)
    {
        modalWindow.gameObject.SetActive(true);
        modalWindow.localScale = open ? Vector3.zero : Vector3.one;
        float endValue = open? 1f : 0f;
        return endValue;
    }
    
    /// <summary>
    /// Prepares Background And Returns Backround Alpha End Value, 1 or 0
    /// </summary>
    static float PrepareModalWindowBackground(Image background, bool open)
    {
        float alphaInitialValue = open? 0f : 1f;
        float alphaEndValue = open? 1f : 0f;
        background.gameObject.SetActive(true);
        background.color = new Color(background.color.r, background.color.g, background.color.b, alphaInitialValue);
        return alphaEndValue;
    }
    /// <summary>
    /// Prepares Background And Returns Backround Alpha End Value, [alphaOnOpened] or 0
    /// </summary>
    static float PrepareModalWindowBackground(Image background, bool open, float alphaOnOpened)
    {
        float alphaInitialValue = open? 0f : alphaOnOpened;
        float alphaEndValue = open? alphaOnOpened : 0f;
        background.gameObject.SetActive(true);
        background.color = new Color(background.color.r, background.color.g, background.color.b, alphaInitialValue);
        return alphaEndValue;
    }
    
    #endregion
    
    #endregion
    
    
    #region Unneccessary code but for later use
    // Unnecessary code but could be useful later
    //CheckAndRemoveOngoingAnimation(rectTransform);
    //t.OnComplete(() => { t.RemoveAnimatedObjectOnAnimationFinished(); });
    //animatedObjects.Add(new AnimatedObject(t, rectTransform));
    
    private static List<AnimatedObject> animatedObjects = new List<AnimatedObject>();
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
    
    public class AnimatedObject
    {
        public Tween currentTween;
        public object obj;

        public AnimatedObject(Tween currentTween, object obj)
        {
            this.currentTween = currentTween;
            this.obj = obj;
        }
    }
    #endregion
}
