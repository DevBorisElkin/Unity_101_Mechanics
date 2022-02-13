using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
