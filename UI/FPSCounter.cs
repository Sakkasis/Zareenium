using System;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{

    [field: NonSerialized]
    public bool fpsCounterBool = true;

    [SerializeField] GameObject fpsCounterObject;

    TextMeshProUGUI fpsCounterText;

    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    float m_refreshTime = 0.5f;

    private void Start()
    {

        fpsCounterText = fpsCounterObject.GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {

        if (fpsCounterBool == true)
        {

            fpsCounterObject.SetActive(true);

            if (m_timeCounter < m_refreshTime)
            {

                m_timeCounter += Time.deltaTime;
                m_frameCounter++;

            }
            else
            {

                m_lastFramerate = (float)m_frameCounter / m_timeCounter;
                m_frameCounter = 0;
                m_timeCounter = 0.0f;

            }

            fpsCounterText.SetText("FPS: " + m_lastFramerate.ToString());

        }
        else
        {

            fpsCounterObject.SetActive(false);

        }

    }

}
