using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpectrumGenerator : MonoBehaviour
{
    //Size of the spectrum
    [SerializeField] private int m_SpectrumSize = 64;
    [SerializeField] private int m_VisibleSpectrum = 32;

    private static float[] m_Spectrum;

    public static float[] m_SpectrumData;

    private void Awake()
    {
        m_Spectrum = new float[m_SpectrumSize];
        m_SpectrumData = new float[m_VisibleSpectrum];
    }

    void Update()
    {
        AudioListener.GetSpectrumData(m_Spectrum, 0, FFTWindow.BlackmanHarris);
        for (int i = 0; i < m_VisibleSpectrum; ++i)
        {
            m_SpectrumData[i] = m_Spectrum[i] * 100;
        }
    }
}

