using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    [SerializeField]private float m_Threshold;
    [SerializeField]private float m_CountDownTime;
    public int m_SpectrumPoint;
    private float m_AudioValue;
    private float m_PreviousValue; 

    protected bool m_Beat = false;
    private float m_Timer;

    //When the spectrum goes over the threshold
    protected virtual void TriggerBeat()
    {
        m_Beat = true;
        m_Timer = m_CountDownTime;
       // Debug.Log("Beat");
    }


   protected virtual void UpdateBeat()
	{
 
        m_PreviousValue = m_AudioValue;
		m_AudioValue = SpectrumGenerator.m_SpectrumData[m_SpectrumPoint];

       // Debug.Log(m_AudioValue);
        if (m_PreviousValue > m_Threshold && m_AudioValue <= m_Threshold && m_Timer <= 0)
		{
            TriggerBeat();
		}

        if (m_PreviousValue <= m_Threshold && m_AudioValue > m_Threshold && m_Timer <= 0)
        {
            TriggerBeat();
        }

        if(m_Timer >= 0)
        {
            CountDown();
        }

    }

    private void CountDown()
    {
        m_Timer -= Time.deltaTime;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateBeat();
    }
}
