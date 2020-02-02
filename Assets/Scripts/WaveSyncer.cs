using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSyncer : AudioSyncer
{
    [SerializeField] private float m_restHeight;
    [SerializeField] private float m_beatHeight;
    [SerializeField] private float m_restTimer;
    [SerializeField] private float m_beatTimer;


    private IEnumerator BeatScale(float target)
    {
        float initialHeight = GetComponent<LowPolyWater.LowPolyWater>().waveHeight;
        float timer = 0;

        while (GetComponent<LowPolyWater.LowPolyWater>().waveHeight != target)
        {
            GetComponent<LowPolyWater.LowPolyWater>().waveHeight = Mathf.Lerp(initialHeight, target, timer / m_beatTimer);
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
        StartCoroutine("BeatScale", m_beatHeight);
    }

    protected override void UpdateBeat()
    {
        base.UpdateBeat();
        if (m_Beat)
        {
            return;
        }
        GetComponent<LowPolyWater.LowPolyWater>().waveHeight = 
            Mathf.Lerp(GetComponent<LowPolyWater.LowPolyWater>().waveHeight, m_restHeight, m_restTimer * Time.deltaTime);
    }
}
