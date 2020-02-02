using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSyncer : AudioSyncer
{
    [SerializeField] private Vector3 m_restScale;
    [SerializeField] private Vector3 m_beatScale;
    [SerializeField] private float m_restTimer;
    [SerializeField] private float m_beatTimer;

    public Gradient m_Gradient;
    GradientColorKey[] m_ColorKey;
    GradientAlphaKey[] m_AlphaKey;


    void Start()
    {
        m_Gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        m_ColorKey = new GradientColorKey[3];
        m_ColorKey[0].color = Color.green;
        m_ColorKey[0].time = 0.0f;
        m_ColorKey[2].color = Color.magenta;
        m_ColorKey[2].time = 0.5f;
        m_ColorKey[2].color = Color.blue;
        m_ColorKey[2].time = 5.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        m_AlphaKey = new GradientAlphaKey[2];
        m_AlphaKey[0].alpha = 1.0f;
        m_AlphaKey[0].time = 0.0f;
        m_AlphaKey[1].alpha = 1.0f;
        m_AlphaKey[1].time = 1.0f;
    }

    private IEnumerator BeatScale(Vector3 _target)
    {
        Vector3 initialScale = transform.localScale;
        float timer = 0;

        while (transform.localScale != _target)
        {
            transform.localScale = Vector3.Lerp(initialScale, _target, timer / m_beatTimer);
            timer += Time.deltaTime;

            yield return null;
        }

        m_Beat = false;
    }
    //When the spectrum goes over the threshold
    protected override void TriggerBeat()
    {
        base.TriggerBeat();

        StopCoroutine("BeatScale");
        StartCoroutine("BeatScale", m_beatScale);
    }

    protected override void UpdateBeat()
    {
        base.UpdateBeat();
        if (m_Beat)
        {
            return;
        }
        transform.localScale = Vector3.Lerp(transform.localScale, m_restScale, m_restTimer * Time.deltaTime);
    }

}
