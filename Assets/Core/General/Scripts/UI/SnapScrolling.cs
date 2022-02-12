using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using Unity_101_Mechanics.ClassCollection;

public class SnapScrolling : MonoBehaviour
{
        [Range(1,100)]
        [Header("Controllers")]
        public int panelCount;
        [Range(-500, 500)] public int panelOffset;
        [Range(0, 500)] public int panelOffsetScale;
        [Range(0, 5f)] public float scaleOffset;
        [Range(0,20f)] public float snapSpeed;
        [Range(0, 20f)] public float scaleSpeed;
        [Range(0, 1)] public float scaleY_adding;

        [Header("Other objects")]
        private SnapScrollingItem panelPrefab;
        private RectTransform panelRectTransform;
    
        public ScrollRect scrollRect;
        private RectTransform contentRect;
        private Vector2 contentVector;
    
        private SnapScrollingItem[] instPans;
        private Vector2[] pansPos;
        private Vector2[] pansScale;
        
        public Vector2 minMaxPanelsScale = new Vector2(0.9f, 1f);
        public float minInertiaCap = 400;
        
        private Action AutoScroll_PanelArrived;
        private Action<float> LightScrollDetected;

        private float startingScrollPos;
        private float endingScrollPos;

        [Range(0f, 1f)] public float lightScrollMinMoveInPercents = 0.2f;
        [ReadOnly][SerializeField] private int selectedPanelID;
        [ReadOnly][SerializeField] private float lightScrollMinMove = 200f;
        [ReadOnly][SerializeField] private float lightScrollMaxMove = 500f;  // should be halth of the panel and spacing
        [ReadOnly][SerializeField] private bool lightScrollPerforming;

        public bool allowScrollingThrough;
        public bool changePanelsScale;
        private bool panelBalanced;
        private bool isScrolling;
        private bool initialized;
        
        #region Initialize - Deinitialize
        
        public void SetUp(List<SceneSetup> setups)
        {
            InitialParams(setups, out int canHoldItems);
            BasicInitialization();
            PanelsInitialization(setups, canHoldItems);
            SubscribeToScrollingEvents(true);
            initialized = true;
        }

        void InitialParams(List<SceneSetup> setups, out int canHoldItems)
        {
            panelPrefab = MainManager.Instance.prefabsManager.snapScrollingItem_prefab;
            canHoldItems = panelPrefab.canHoldItems;

            panelCount = setups.Count / canHoldItems;
            if (setups.Count % canHoldItems != 0) panelCount += 1;
        }
        void BasicInitialization()
        {
            contentRect = GetComponent<RectTransform>();
            instPans = new SnapScrollingItem[panelCount];
            pansPos = new Vector2[panelCount];
            pansScale = new Vector2[panelCount];
            panelRectTransform = panelPrefab.GetComponent<RectTransform>();
            SetLightScrollMinMaxMove();
        }

        void PanelsInitialization(List<SceneSetup> setups, int canHoldItems)
        {
            for(int i=0; i< panelCount; i++)
            {
                instPans[i] = Instantiate(panelPrefab, transform, false);

                List<SceneSetup> setupsForScrollItem = new List<SceneSetup>();
                int iteratingFrom = i * canHoldItems;
                int iteratingUntil = iteratingFrom + canHoldItems;
                if (iteratingUntil >= setups.Count) iteratingUntil = setups.Count;
                for (int j = iteratingFrom; j < iteratingUntil; j++)
                    setupsForScrollItem.Add(setups[j]);
                
                instPans[i].SetUp(setupsForScrollItem);
        
                if (i == 0) continue;
                instPans[i].transform.localPosition = new Vector2(instPans[i-1].transform.localPosition.x + panelRectTransform.sizeDelta.x + panelOffset,
                    instPans[i].transform.localPosition.y);
                pansPos[i] = new Vector2((-instPans[i].GetComponent<RectTransform>().sizeDelta.x - panelOffset) * i, 0f);
            }
        }
        void SubscribeToScrollingEvents(bool state)
        {
            if (state)
            {
                LightScrollDetected += OnLightScroll;
                AutoScroll_PanelArrived += OnAutoScrollPanelArrived;
            }
            else
            {
                LightScrollDetected -= OnLightScroll;
                AutoScroll_PanelArrived += OnAutoScrollPanelArrived;
            }
        }
        void SetLightScrollMinMaxMove()
        {
            lightScrollMinMove = (panelRectTransform.sizeDelta.x * lightScrollMinMoveInPercents);
            lightScrollMaxMove = (panelRectTransform.sizeDelta.x + panelOffset) / 2f;
        }

        private void OnDestroy() { SubscribeToScrollingEvents(false); }
        
        #endregion
        
        #region Assigned in the Editor

        public void SwapPanel(bool right)
        {
            if (right)
            {
                if (selectedPanelID +1 < instPans.Length)
                    selectedPanelID += 1;
            }
            else
            {
                if(selectedPanelID-1> -1)
                    selectedPanelID -= 1;
            }
        }
        
        //public void SwapToLeft() => contentRect.localPosition = new Vector2(contentRect.localPosition.x + 895, contentRect.localPosition.y);
        //public void SwapToRight() => contentRect.localPosition = new Vector2(contentRect.localPosition.x - 895, contentRect.localPosition.y);
        //public void SwapToLeftFull() => contentRect.localPosition = new Vector2(0 + 25000, contentRect.localPosition.y);

        public void Scrolling(bool scroll)
        {
            if (scroll)
                panelBalanced = false;
            else lightScrollPerforming = false;

            isScrolling = scroll;
            if (scroll && allowScrollingThrough) scrollRect.inertia = true;
             
            CheckLightScroll(scroll);
        }

        #endregion

        #region Utils
        
        void CheckLightScroll(bool scroll)
        {
            if (scroll) startingScrollPos = contentRect.position.x;
            else endingScrollPos = contentRect.position.x;
             
            if(scroll) return;

            float scrollResult = endingScrollPos - startingScrollPos;

            float absScrollResult = Mathf.Abs(scrollResult);
             
            if (absScrollResult >= lightScrollMinMove && absScrollResult < lightScrollMaxMove)
            {
                LightScrollDetected?.Invoke(scrollResult);
            }
        }

        void OnLightScroll(float delta)
        {
            lightScrollPerforming = true;
            if (delta < 0 && selectedPanelID + 1 < panelCount) selectedPanelID++;
            else if (delta > 0 && selectedPanelID - 1 >= 0) selectedPanelID--;
        }

        void OnAutoScrollPanelArrived()
        {
            lightScrollPerforming = false;
        }
        
        #endregion

        #region UpdateLoop

         void Update()
         {
             if(!initialized) return;
             
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
             if(!changePanelsScale) return;
             for(int i = 0; i<panelCount; i++)
             {
                 float distance = Math.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
                 
                 float scale = Mathf.Clamp(1 / (distance / panelOffsetScale) * scaleOffset, minMaxPanelsScale.x, minMaxPanelsScale.y);
                 pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale+scaleY_adding, scaleSpeed * Time.deltaTime);
                 pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale+scaleY_adding, scaleSpeed * Time.deltaTime);
                 instPans[i].transform.localScale = pansScale[i];
             }
         }
         
         void SelectNearestPanelId()
         {
             if(lightScrollPerforming) return;
             
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

         // returning true will allow to Call AutoScrolling
         bool ManageScrollingMinSpeed()
         {
             float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
             if (allowScrollingThrough)
             {
                 if (scrollVelocity < minInertiaCap && !isScrolling)
                 {
                     scrollRect.inertia = false;
                     scrollRect.velocity = Vector2.zero;
                     return true;
                 }
                 else if (lightScrollPerforming) return false;
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
             if(panelBalanced) return;
             
             contentVector.x = Mathf.Lerp(contentRect.anchoredPosition.x, pansPos[selectedPanelID].x, snapSpeed*Time.deltaTime);
             contentRect.anchoredPosition = contentVector;
             
             if (Mathf.Abs(contentRect.anchoredPosition.x - pansPos[selectedPanelID].x) < 0.5f)
             {
                 AutoScroll_PanelArrived?.Invoke();
                 panelBalanced = true;
             }
         }

         #endregion
}
