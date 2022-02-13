using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPanelExpandableSafeArea : MonoBehaviour
{
    RectTransform panel;
    Rect safeAreaRect;

    void Awake()
    {
        panel = GetComponent<RectTransform>();

        if (panel == null)
        {
            Debug.LogError("Cannot apply safe area - no RectTransform found on " + name);
            Destroy(gameObject);
        }
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        safeAreaRect = Screen.safeArea;
        ApplySafeArea(safeAreaRect);
    }

    void ApplySafeArea(Rect r)
    {
        panel.sizeDelta = new Vector2 (panel.sizeDelta.x, panel.sizeDelta.y + ((Screen.height - r.height)/2));

        //     Debug.LogFormat("New safe area applied to {0}: x={1}, y={2}, w={3}, h={4} on full extents w={5}, h={6}", name, r.x, r.y, r.width, r.height, Screen.width, Screen.height);
    }
}
