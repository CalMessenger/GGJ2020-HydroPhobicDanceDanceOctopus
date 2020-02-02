using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarPlacement : MonoBehaviour
{
    public GameObject m_AudioBar;
    public GameObject m_Panel;

    public float m_Width;

    private void Start()
    {
        for(int i = 0; i < SpectrumGenerator.m_SpectrumData.Length; ++i)
        {
            GameObject bar = Instantiate(m_AudioBar, new Vector3((m_Width * i) + m_Width/2, m_Panel.transform.position.y - 12.5f),Quaternion.identity, m_Panel.transform);
            var rectTransform = bar.transform as RectTransform;
            rectTransform.sizeDelta = new Vector2(m_Width, rectTransform.sizeDelta.y);
            bar.GetComponent<BarSyncer>().m_SpectrumPoint = i;
        }
    }
}
