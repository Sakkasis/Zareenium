using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [Header("Sliders")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectsSlider;

    IDataService DataService = new JsonDataService();
    IDataClasses DataClasses = new DataClasses();

    const string audioDataFileName = "AudioSettingsData";

    void Start()
    {

        if (File.Exists(Application.persistentDataPath + "/" + audioDataFileName + ".json") == false)
        {

            masterSlider.value = 1f;
            musicSlider.value = 0.6f;
            effectsSlider.value = 0.5f;

            SaveAudioSettings();

        }
        else
        {

            LoadAudioSettings();

        }

    }

    public bool SaveAudioSettings()
    {

        AudioData audioDataClass = new AudioData();

        audioDataClass.masterVolume = masterSlider.value;
        audioDataClass.musicVolume = musicSlider.value;
        audioDataClass.effectsVolume = effectsSlider.value;

        if (DataService.SaveData(audioDataFileName, audioDataClass, false))
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    public bool LoadAudioSettings()
    {

        AudioData audioDataClass = DataClasses.AudioDataClass();

        masterSlider.value = audioDataClass.masterVolume;
        musicSlider.value = audioDataClass.musicVolume;
        effectsSlider.value = audioDataClass.effectsVolume;

        AudioListener.volume = masterSlider.value;

        List<GameObject> musicObjectsList = new List<GameObject>();
        List<GameObject> uiEffectObjectsList = new List<GameObject>();
        List<GameObject> effectObjectsList = new List<GameObject>();
        musicObjectsList.AddRange(GameObject.FindGameObjectsWithTag("SFX_Music"));
        uiEffectObjectsList.AddRange(GameObject.FindGameObjectsWithTag("SFX_UI_Effects"));
        effectObjectsList.AddRange(GameObject.FindGameObjectsWithTag("SFX_Effects"));

        foreach (GameObject musicObject in musicObjectsList)
        {

            AudioSource musicSource = musicObject.GetComponent<AudioSource>();
            musicSource.volume = musicSlider.value;

        }
        foreach (GameObject uiEffectsObject in uiEffectObjectsList)
        {

            AudioSource uiEffectsSource = uiEffectsObject.GetComponent<AudioSource>();
            uiEffectsSource.volume = effectsSlider.value;

        }
        foreach (GameObject effectsObject in effectObjectsList)
        {

            AudioSource effectsSource = effectsObject.GetComponent<AudioSource>();
            effectsSource.volume = effectsSlider.value;

        }

        return true;

    }

    public void TurnAudioOffOn(bool trueOnFalseOff, bool music, bool uiEffects, bool effects)
    {

        List<GameObject> musicObjectsList = new List<GameObject>();
        List<GameObject> uiEffectObjectsList = new List<GameObject>();
        List<GameObject> effectObjectsList = new List<GameObject>();
        musicObjectsList.AddRange(GameObject.FindGameObjectsWithTag("SFX_Music"));
        uiEffectObjectsList.AddRange(GameObject.FindGameObjectsWithTag("SFX_UI_Effects"));
        effectObjectsList.AddRange(GameObject.FindGameObjectsWithTag("SFX_Effects"));

        if (trueOnFalseOff == true)
        {

            if (music && musicObjectsList.Count != 0)
            {

                foreach (GameObject musicObject in musicObjectsList)
                {

                    AudioSource musicSource = musicObject.GetComponent<AudioSource>();

                    musicSource.Pause();

                }

            }

            if (uiEffects && uiEffectObjectsList.Count != 0)
            {

                foreach (GameObject uiEffectsObject in uiEffectObjectsList)
                {

                    AudioSource uiEffectsSource = uiEffectsObject.GetComponent<AudioSource>();

                    uiEffectsSource.mute = true;

                }

            }

            if (effects && effectObjectsList.Count != 0)
            {

                foreach (GameObject effectsObject in effectObjectsList)
                {

                    AudioSource effectsSource = effectsObject.GetComponent<AudioSource>();

                    effectsSource.mute = true;

                }

            }

        }
        else
        {

            if (music && musicObjectsList.Count != 0)
            {

                foreach (GameObject musicObject in musicObjectsList)
                {

                    AudioSource musicSource = musicObject.GetComponent<AudioSource>();

                    musicSource.Play();

                }

            }

            if (uiEffects && uiEffectObjectsList.Count != 0)
            {

                foreach (GameObject uiEffectsObject in uiEffectObjectsList)
                {

                    AudioSource uiEffectsSource = uiEffectsObject.GetComponent<AudioSource>();

                    uiEffectsSource.mute = false;

                }

            }

            if (effects && effectObjectsList.Count != 0)
            {

                foreach (GameObject effectsObject in effectObjectsList)
                {

                    AudioSource effectsSource = effectsObject.GetComponent<AudioSource>();

                    effectsSource.mute = false;

                }

            }

        }

    }

}

[Serializable]
public class AudioData
{

    public float masterVolume;
    public float musicVolume;
    public float effectsVolume;

}
