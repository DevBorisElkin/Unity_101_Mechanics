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
                pansPos[i] = new Vector2(-instPans[i].GetComponent<RectTransform>().sizeDelta.x - panelOffset, 0f);
            }
        }
        
        void ApplyData(int i)
        {
           //RawImage[] result = instPans[i].GetComponentsInChildren<RawImage>();
           //btnImages[i] = result[1];
           //setRawTexture(result[0], uni_load.levelsList.ElementAt(i).texture);
           //Text[] txtResult = instPans[i].GetComponentsInChildren<Text>();
           //txtResult[0].text = uni_load.levelsList.ElementAt(i).level;        // setting str name
           //
           //txtResult[1].text = uni_load.levelsList.ElementAt(i).introducingTo;  //setting str description
           //
           //
           //txtResult[2].text += uni_load.levelsList.ElementAt(i).difficulty;  // setting difficulty
        
           //txtResult[3].text = getCoinsAmountCollected(uni_load.levelsList.ElementAt(i).level_index) + "/20"; // setting coins left
        
        
           //manageAccess(i);
        
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
        
            void FixedUpdate()
            {
                if(!isScrolling && (contentRect.anchoredPosition.x >=pansPos[0].x || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x))
                {
                    scrollRect.inertia = false;
                }
        
                //for(int i = 0; i<panelCount; i++)
                //{
                //    float distance = Math.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
                //    
                //    float scale = Mathf.Clamp(1 / (distance / panelOffsetScale) * scaleOffset, 0.5f, 1f);
                //    pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale+scaleY_adding, scaleSpeed * Time.fixedDeltaTime);
                //    pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale+scaleY_adding, scaleSpeed * Time.fixedDeltaTime);
                //    instPans[i].transform.localScale = pansScale[i];
        //
                //    manageButton(scale, i); //removed for a while
                //}

                
                
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
                    Debug.Log($"nearestPos[{nearestPos}], contentRect.anchoredPosition.x[{contentRect.anchoredPosition.x}], pansPos[{i}].x[{pansPos[i].x}]");
                }
                selectedPanelID = closestPanId;

                float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
                if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
                if (isScrolling || scrollVelocity > 400) return;
                contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanelID].x, snapSpeed*Time.fixedDeltaTime);
                contentRect.anchoredPosition = contentVector;
            }
        
            void manageButton(float scale, int i)
            {
                float paintSpeed = 0.30f;
                float eraseSpeed = 10f;
        
                float paintSpeedPanel = 0.26f;
                float eraseSpeedPanel = 6f;
        // if (scale<0.9f)
        // {
        //     instPans[i].GetComponentsInChildren<RawImage>()[1].color = new Color(255, 255, 255, Mathf.SmoothStep(instPans[i].GetComponentsInChildren<RawImage>()[1].color.a, 0, eraseSpeed * Time.fixedDeltaTime));
        //     /*
        //     instPans[i].GetComponentsInChildren<RawImage>()[2].color = new Color(255, 255, 255, Mathf.SmoothStep(instPans[i].GetComponentsInChildren<RawImage>()[1].color.a, 0, eraseSpeed * Time.fixedDeltaTime));
        //     
        //     Color colorCurrent = instPans[i].GetComponentsInChildren<UnityEngine.UI.Image>()[1].color;
        //     instPans[i].GetComponentsInChildren<UnityEngine.UI.Image>()[1].color = new Color(colorCurrent.r, colorCurrent.g, colorCurrent.b, Mathf.SmoothStep(colorCurrent.a, 0, eraseSpeedPanel * Time.fixedDeltaTime));
        //
        //
        //     instPans[i].GetComponentsInChildren<Text>()[1].color = new Color(50, 50, 50, Mathf.SmoothStep(instPans[i].GetComponentsInChildren<Text>()[1].color.a, 0, eraseSpeedPanel * Time.fixedDeltaTime));
        //     instPans[i].GetComponentsInChildren<Text>()[2].color = new Color(50, 50, 50, Mathf.SmoothStep(instPans[i].GetComponentsInChildren<Text>()[2].color.a, 0, eraseSpeedPanel * Time.fixedDeltaTime));
        //
        //     instPans[i].GetComponentsInChildren<Text>()[3].color = new Color(50, 50, 50, Mathf.SmoothStep(instPans[i].GetComponentsInChildren<Text>()[2].color.a, 0, eraseSpeedPanel * Time.fixedDeltaTime));
        //     */
        // }// scale != 1.18f
        // else if(scale!= 0.6800002f)
        // {
        //     instPans[i].GetComponentsInChildren<RawImage>()[1].color = new Color(255, 255, 255, Mathf.SmoothStep(instPans[i].GetComponentsInChildren<RawImage>()[1].color.a, 255, paintSpeed * Time.fixedDeltaTime));
        //     /*
        //     instPans[i].GetComponentsInChildren<RawImage>()[2].color = new Color(255, 255, 255, Mathf.SmoothStep(instPans[i].GetComponentsInChildren<RawImage>()[1].color.a, 255, paintSpeed * Time.fixedDeltaTime));
        //     
        //     Color colorCurrent = instPans[i].GetComponentsInChildren<UnityEngine.UI.Image>()[1].color;
        //     instPans[i].GetComponentsInChildren<UnityEngine.UI.Image>()[1].color = new Color(colorCurrent.r, colorCurrent.g, colorCurrent.b, Mathf.SmoothStep(0, 4000, paintSpeed * Time.fixedDeltaTime));
        //
        //     instPans[i].GetComponentsInChildren<Text>()[1].color = new Color(50, 50, 50, Mathf.SmoothStep(instPans[i].GetComponentsInChildren<Text>()[1].color.a, 255, paintSpeed * Time.fixedDeltaTime));
        //     instPans[i].GetComponentsInChildren<Text>()[2].color = new Color(50, 50, 50, Mathf.SmoothStep(instPans[i].GetComponentsInChildren<Text>()[2].color.a, 255, paintSpeed * Time.fixedDeltaTime));
        //
        //     instPans[i].GetComponentsInChildren<Text>()[3].color = new Color(50, 50, 50, Mathf.SmoothStep(instPans[i].GetComponentsInChildren<Text>()[2].color.a, 255, paintSpeed * Time.fixedDeltaTime));
        //     */
        // }
            }
            public void Scrolling(bool scroll)
            {
                isScrolling = scroll;
                if (scroll) scrollRect.inertia = true;
            }
        
            void setTexture(GameObject obj, Texture texture)
            {
                obj.GetComponent<RawImage>().texture = texture;
            }
        
            void setRawTexture(RawImage raw, Texture texture)
            {
                raw.texture = texture;
            }
}
