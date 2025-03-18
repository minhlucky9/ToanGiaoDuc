using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackWindow : MonoBehaviour
{
    public static BlackWindow Instance;
    public float timeChangeAlpha = 0.3f;
    public float alphaOnValue = 0.7f;
    public float alphaOffValue = 0f;
    private Image img;
    private SpriteRenderer sprd;
    public bool isImage = false;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        if (isImage)
        {
            img = GetComponent<Image>();
        }
        else
        {
            sprd = GetComponent<SpriteRenderer>();
        }
    }
    public void On()
    {
        StartCoroutine(OnChangeAlphaIE(alphaOnValue, timeChangeAlpha));
    }
    public void Off()
    {
        StartCoroutine(OnChangeAlphaIE(alphaOffValue, timeChangeAlpha));
    }
    private IEnumerator OnChangeAlphaIE(float wantedAlpha, float time)
    {
        float t = 0f;
        Color cl;
        if (isImage)
        {
            cl = img.color;
        }
        else
        {
            cl = sprd.color;
        }
        float aC = cl.a;
        while (t < time)
        {
            aC = Mathf.Lerp(cl.a, wantedAlpha, t / time);
            cl.a = aC;
            if (isImage)
            {
                img.color = cl;
            }
            else
            {
                sprd.color = cl;
            }
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
