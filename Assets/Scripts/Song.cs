using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Song : MonoBehaviour
{
    public float m_BeatInterval;
    [SerializeField] public AudioClip m_Clip;

    public string m_SongName;
    public string m_SongLength;

    private void Awake()
    {
        m_SongName = m_Clip.name;
        m_SongLength = ConvertToMinutes();
    }
    private string ConvertToMinutes()
    {
        TimeSpan t = TimeSpan.FromSeconds(m_Clip.length);
        string str = t.ToString(@"mm\:ss");
        return str;
    }
}
