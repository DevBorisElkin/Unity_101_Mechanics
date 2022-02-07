using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    [Range(1,100)]
        [Header("Controllers")]
        public int panelCount;
        [Range(-500, 500)]
        public int panelOffset;
        [Range(0, 500)]
        public int panelOffsetScale;
    
        [Range(0, 5f)]
        public float scaleOffset;
    
        [Range(0,20f)]
        public float snapSpeed;
    
        [Range(0, 20f)]
        public float scaleSpeed;
    
        [Range(0, 1)]
        public float scaleY_adding;
    
        [Header("Other objects")]
        public GameObject panelPrefab;
    
        private RectTransform contentRect;
        private Vector2 contentVector;
    
        private GameObject[] instPans;
    
        [SerializeField] private Vector2[] pansPos;
        private Vector2[] pansScale;
    
        [SerializeField] private int selectedPanelID;
    
        private bool isScrolling;
    
    
        public ScrollRect scrollRect;

        public bool allowScrollingThrough;
        public float minInertiaCap = 400;
        
        void Start()
        {
            contentRect = GetComponent<RectTransform>();
            instPans = new GameObject[panelCount];
            pansPos = new Vector2[panelCount];
            pansScale = new Vector2[panelCount];
            
            for(int i=0; i< panelCount; i++)
            {
                instPans[i] = Instantiate(panelPrefab, transform, false);
        
                ApplyData(i);
        
                if (i == 0) continue;
                instPans[i].transform.localPosition = new Vector2(instPans[i-1].transform.localPosition.x + panelPrefab.GetComponent<RectTransform>().sizeDelta.x + panelOffset,
                     instPans[i].transform.localPosition.y);
                pansPos[i] = new Vector2((-instPans[i].GetComponent<RectTransform>().sizeDelta.x - panelOffset) * i, 0f);
            }
        }
        
        void ApplyData(int i)
        {
           
        }
        
            public void SwapLevel(bool right)
            {   // For some reason works incorrectly
                if (right)
                {
                    if (selectedPanelID+1< instPans.Length)
                        selectedPanelID += 1;
                }
                else
                {
                    if(selectedPanelID-1>-1)
                    selectedPanelID -= 1;
                }
                //scrollRect.content.localPosition = GetSnapToPositionToBringChildIntoView(scrollRect, instPans[selectedPanelID].GetComponent<RectTransform>());
                SwapToRight();
            }
        
            public void SwapToLeft()
            {
                contentRect.localPosition = new Vector2(contentRect.localPosition.x + 895, contentRect.localPosition.y);
            }
            public void SwapToRight()
            {
                contentRect.localPosition = new Vector2(contentRect.localPosition.x - 895, contentRect.localPosition.y);
            }
        
            public void SwapToLeftFull()
            {
                contentRect.localPosition = new Vector2(0 + 25000, contentRect.localPosition.y);
            }
        
            bool forbidSettingPanelId;
            float timeForWhichForbidToSetPanelId = 1f;
            
            void FixedUpdate()
            {
                CapInertiaOnBorders();
                ManageScaling();
                SelectNearestPanelId();
                if(ManageScrollingMinSpeed())
                    PerformAutoScroll();
            }

            void CapInertiaOnBorders()
            {
                if(!isScrolling && (contentRect.anchoredPosition.x >=pansPos[0].x || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x))
                    scrollRect.inertia = false;
            }
            
            void ManageScaling()
            {
                //for(int i = 0; i<panelCount; i++)
                //{
                //    float distance = Math.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
                //    
                //    float scale = Mathf.Clamp(1 / (distance / panelOffsetScale) * scaleOffset, 0.5f, 1f);
                //    pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale+scaleY_adding, scaleSpeed * Time.fixedDeltaTime);
                //    pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale+scaleY_adding, scaleSpeed * Time.fixedDeltaTime);
                //    instPans[i].transform.localScale = pansScale[i];
                //
                //    
                //}
            }
            
            void SelectNearestPanelId()
            {
                float nearestPos = float.MaxValue;
                int closestPanId = 0;
                for (int i = 0; i < panelCount; i++)
                {
                    float distance = Math.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
                    if(distance< nearestPos)
                    {
                        nearestPos = distance;
                        closestPanId = i;
                    }
                }
                selectedPanelID = closestPanId;
            }

            bool ManageScrollingMinSpeed()
            {
                float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
                if (allowScrollingThrough)
                {
                    if (scrollVelocity < minInertiaCap && !isScrolling)
                    {
                        scrollRect.inertia = false;
                        scrollRect.velocity = Vector2.zero;
                    }
                    else if (isScrolling || scrollVelocity > minInertiaCap)
                        return false;
                }
                else
                {
                    if (isScrolling)
                        return false;
                }

                return true;
            }

            void PerformAutoScroll()
            {
                contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanelID].x, snapSpeed*Time.fixedDeltaTime);
                contentRect.anchoredPosition = contentVector;
            }
            public void Scrolling(bool scroll)
            {
                isScrolling = scroll;
                if (scroll && allowScrollingThrough) scrollRect.inertia = true;
            }
}
