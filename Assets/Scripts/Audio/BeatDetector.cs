using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDetector : MonoBehaviour
{
    public int m_RingBufferSize = 120;
    public int m_BufferSize = 1024;
    public int m_SamplingRate = 44100;
    //Whether you want an interval between beats
    public bool m_LimitBeats = false;
    public int m_LimitedAmount;
    public float m_BeatIndicationThreshold = 0.1f;

    private const int BANDS = 12;
    private const int MAXIMUM_LAG = 100;
    private const float SMOOTH_DECAY = 0.997f;

    private AudioSource m_AudioSource;
    private AudioData m_AudioData;

    private int m_FrameSinceBeat = 0;
    private float m_FramePeriod;

    private int m_CurrentRingBufferPosition = 0;
     
    private float[] m_SpectrumData;
    private float[] m_PreviousSpectrum;

    private float[] m_AveragePowerPerBand;
    private float[] m_Onsets;
    private float[] m_Notations;

    private void Awake()
    {
        m_Onsets = new float[m_RingBufferSize];
        m_Notations = new float[m_RingBufferSize];
        m_SpectrumData = new float[m_BufferSize];

        m_AveragePowerPerBand = new float[BANDS];
      
        m_AudioSource = GetComponent<AudioSource>();
        m_SamplingRate = m_AudioSource.clip.frequency;


        m_FramePeriod = (float)m_BufferSize / (float)m_SamplingRate;

        m_PreviousSpectrum = new float[BANDS];
        for(int i = 0; i < BANDS; ++i)
        {
            m_PreviousSpectrum[i] = 100.0f; 
        }

        m_AudioData = new AudioData(MAXIMUM_LAG,SMOOTH_DECAY, m_FramePeriod, BandWidth() * 2.0f);

    }

    private float BandWidth()
    {
        return (2.0f / m_BufferSize) * (m_SamplingRate / 2.0f) * 0.5f;
    }


    // Update is called once per frame
    void Update()
    {
        m_AudioSource.GetSpectrumData(m_SpectrumData, 0, FFTWindow.BlackmanHarris);

        //
        for(int i = 0; i < BANDS; ++i)
        {
            float averagePower = 0;
            int lowFrequencyIndex = (i == 0) ? 0 : Mathf.RoundToInt((m_SamplingRate * 0.5f) / Mathf.Pow(2,BANDS - i));
            int highFrequencyIndex = Mathf.RoundToInt((m_SamplingRate * 0.5f) / Mathf.Pow(2, BANDS - 1 - i));

            int lowBound = FrequencyByIndex(lowFrequencyIndex);
            int highBound = FrequencyByIndex(highFrequencyIndex);

            for(int j = lowBound; j <= highBound; ++j)
            {
                averagePower += m_SpectrumData[j];
            }

            averagePower /= (highBound - lowBound + 1);
            m_AveragePowerPerBand[i] = averagePower;
        }

        float onset = 0;
        for(int i = 0; i < BANDS; ++i)
        {
            float spectrumValue = Mathf.Max(- 100.0f, 20 *Mathf.Log10(m_AveragePowerPerBand[i] + 160.0f));
            spectrumValue *= 0.025f;

            float dbIncrement = spectrumValue - m_PreviousSpectrum[i];
            m_PreviousSpectrum[i] = spectrumValue;

            onset += dbIncrement;
        }

        float maxDelay = 0.0f;
        int tempo = 0;
        for (int i = 0; i < MAXIMUM_LAG; ++i)
        {
            float delayVal = Mathf.Sqrt(m_AudioData.DelayAtIndex(i));
            if (delayVal > maxDelay)
            {
                maxDelay = delayVal;
                tempo = i;
            }
        }

        m_Onsets[m_CurrentRingBufferPosition] = onset;
        m_AudioData.UpdateAudioData(onset); 

     

        float maximumNotation = -999999;
        int maximumNotationIndex = 0;

        for(int i = Mathf.RoundToInt(tempo * 0.5f); i < Mathf.Min(m_RingBufferSize, 2* tempo); ++i)
        {
            float notationValue = onset + m_Notations[(m_CurrentRingBufferPosition - i + m_RingBufferSize) % m_RingBufferSize]
                - (m_BeatIndicationThreshold * 100.0f) * Mathf.Pow(Mathf.Log((float)i / (float)tempo), 2);
            if(notationValue > maximumNotation)
            {
                maximumNotation = notationValue;
                maximumNotationIndex = i;
            }
        }

        m_Notations[m_CurrentRingBufferPosition] = maximumNotation;

        float minNotation = m_Notations[0];

        //Find the smallest notation
        for(int i = 0; i < m_RingBufferSize; ++i)
        {
            if(m_Notations[i] < minNotation)
            {
                minNotation = m_Notations[i];
            }
        }

        for(int i = 0; i < m_RingBufferSize; ++i)
        {
            m_Notations[i] -= minNotation;
        }

        maximumNotation = m_Notations[0];
        maximumNotationIndex = 0;
        for (int i = 0; i < m_RingBufferSize; i++)
        {
            if (m_Notations[i] > maximumNotation)
            {
                maximumNotation = m_Notations[i];
                maximumNotationIndex = i;
            }
        }

        ++m_FrameSinceBeat;

        if(maximumNotationIndex == m_CurrentRingBufferPosition)
        {
            if(m_LimitBeats)
            {
                if(m_FrameSinceBeat > tempo/m_LimitedAmount)
                {
                    //BEAT IS HERE
                    Debug.Log("Beat");
                    m_FrameSinceBeat = 0;
                }
            }
            else
            {
                Debug.Log("Beat");
                //BEAT IS HERE
            }

        }

        ++m_CurrentRingBufferPosition;
        if(m_CurrentRingBufferPosition >= m_RingBufferSize)
        {
            m_CurrentRingBufferPosition = 0;
        }
    }

    public int FrequencyByIndex(int frequencyIndex)
    {
        if(frequencyIndex < BandWidth())
        {
            return 0;
        }

        if(frequencyIndex > m_SamplingRate * 0.5f - BandWidth())
        {
            return m_BufferSize / 2;
        }

        float fraction = frequencyIndex / m_SamplingRate;
        return Mathf.RoundToInt(m_BufferSize * fraction);
    }

    class AudioData
    {
        private int m_Index = 0;
        private int m_DelayLength;
        private float m_Decay;

        private float[] m_Delays;
        private float[] m_OutputValues;
        private float[] m_Weights;
        private float[] m_BPMS;

        private float m_OctaveWidth;
        private float m_FramePeriod;

        public AudioData(int delayLength, float decay, float framePeriod, float octaveWidth)
        {
            m_OctaveWidth = octaveWidth;
            m_Decay = decay;
            m_DelayLength = delayLength;

            m_Delays = new float[m_DelayLength];
            m_OutputValues = new float[delayLength];

            m_BPMS = new float[m_DelayLength];
            m_Weights = new float[m_DelayLength];

            m_FramePeriod = framePeriod;

            AddWeights();
        }

        private void AddWeights()
        {
            for(int i = 0; i < m_DelayLength; ++i)
            {
                m_BPMS[i] = 60.0f / (m_FramePeriod * i);
                m_Weights[i] = Mathf.Exp(-0.5f * Mathf.Pow(Mathf.Log(m_BPMS[i] / 120.0f) / Mathf.Log(2.0f) / m_OctaveWidth,2.0f));
            }
        }

        public void UpdateAudioData(float updatedOnset)
        {
            m_Delays[m_Index] = updatedOnset;

            for(int i = 0; i < m_DelayLength; ++i)
            {
                int delayIndex = (m_Index - i + m_DelayLength) % m_DelayLength;
                m_OutputValues[i] += (1.0f - SMOOTH_DECAY) * (m_Delays[m_Index] * m_Delays[delayIndex] - m_OutputValues[i]); 
            }

            ++m_Index;
            if (m_Index >= m_DelayLength)
            {
                m_Index = 0;
            }
        }

        public float DelayAtIndex(int delayIndex)
        {
            return m_Weights[delayIndex] * m_OutputValues[delayIndex];
        }

    }

   



}
