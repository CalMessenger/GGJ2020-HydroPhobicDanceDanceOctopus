using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongController : MonoBehaviour
{
    [SerializeField]public  SongType m_SongSelection;
    private SongType m_PreviousSong;
    [SerializeField]public  List<Song> m_Songs;

    public List<GameObject> m_Spawners;

    private float m_CountDown = 0.0f;
    private float m_CurrentCountDownLimit;

    [SerializeField] private GameObject m_Ocean;

    [SerializeField] private GameObject m_SongName;
    [SerializeField] private GameObject m_Duration;
    public enum SongType
    {
        Ouroboros
    }

    void Awake()
    {
        PlaySong();
        m_PreviousSong = m_SongSelection;
    }

    private void Update()
    {
       // m_Ocean.GetComponent<LowPolyWater.LowPolyWater>().waveHeight = Mathf.Clamp(SpectrumGenerator.m_SpectrumData[0],0.0f,8.0f);
       if(m_SongSelection != m_PreviousSong)
       {
            SwitchSongs(m_SongSelection);
       }
       else
       {
            m_CountDown -= Time.deltaTime;
            if (m_CountDown <= 0)
            {
                ResetTimer();
                System.Random rand = new System.Random();
                m_Spawners[rand.Next(0, m_Spawners.Count)].GetComponent<Spawner>().Spawn();
            }
        }
        m_PreviousSong = m_SongSelection;
    }

    private void ResetTimer()
    {
        m_CountDown = m_CurrentCountDownLimit;
    }

    private void PlaySong()
    { 
        m_Songs[(int)m_SongSelection].enabled = true;

        m_CurrentCountDownLimit = m_Songs[(int)m_SongSelection].m_BeatInterval;

        m_Duration.GetComponent<Text>().text = "Duration: " + m_Songs[(int)m_SongSelection].m_SongLength;

        m_SongName.GetComponent<Text>().text = "Song Name: " +  m_Songs[(int)m_SongSelection].m_SongName;

        m_Ocean.GetComponent<LowPolyWater.LowPolyWater>().waveFrequency = m_CurrentCountDownLimit;

        ResetTimer();

        GetComponent<AudioSource>().clip = m_Songs[(int)m_SongSelection].m_Clip;
        GetComponent<AudioSource>().Play();
    }

    public void SwitchSongs(SongType song)
    {
        m_Songs[(int)m_SongSelection].enabled = false;
        m_Songs[(int)song].enabled = true;
        m_SongSelection = song;
        PlaySong();
    }
}
