using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveLoadConfirmer : MonoBehaviour
{

    List<GameObject> confirmerObjects = new List<GameObject>();
    List<TextMeshProUGUI> confirmerTexts = new List<TextMeshProUGUI>();

    [SerializeField] string saveSuccessMessage = "Saved Successfully!";
    [SerializeField] string loadSuccessMessage = "Loaded Successfully!";

    long timeTaken;
    float waitTime;

    ISystemTime TimeService = new SystemTime();

    public void ConfirmSaveVoid(float? waitTimeLocal, long? startTime)
    {

        if (waitTimeLocal.HasValue)
        {

            waitTime = waitTimeLocal.Value;

        }
        else
        {

            waitTime = 5f;

        }
        if (startTime.HasValue)
        {

            StartCoroutine(ConfirmToPlayerIE(startTime, true));

        }
        else
        {

            StartCoroutine(ConfirmToPlayerIE(null, true));

        }

    }

    public void ConfirmLoadVoid(float? waitTimeLocal, long? startTime)
    {

        if (waitTimeLocal.HasValue)
        {

            waitTime = waitTimeLocal.Value;

        }
        else
        {

            waitTime = 8f;

        }
        if (startTime.HasValue)
        {

            StartCoroutine(ConfirmToPlayerIE(startTime, false));

        }
        else
        {

            StartCoroutine(ConfirmToPlayerIE(null, false));

        }

    }

    IEnumerator ConfirmToPlayerIE(long? startTime, bool SL)
    {

        if (startTime.HasValue)
        {

            timeTaken = (DateTime.Now.Ticks - startTime.Value) / TimeSpan.TicksPerMillisecond;

        }
        else
        {

            timeTaken = 0;

        }

        foreach (GameObject obj in confirmerObjects)
        {

            obj.SetActive(true);

        }

        if (confirmerObjects.Count == 0)
        {

            var setReferences = SetObjectReferences();
            yield return StartCoroutine(setReferences);
            bool? result = setReferences.Current as bool?;

            if (result.Value)
            {

                if (SL)
                {

                    if (timeTaken != 0)
                    {

                        foreach (TextMeshProUGUI text in confirmerTexts)
                        {

                            text.SetText(saveSuccessMessage + "\nSave time: " + timeTaken.ToString() + " ms");

                        }

                    }
                    else
                    {

                        foreach (TextMeshProUGUI text in confirmerTexts)
                        {

                            text.SetText(saveSuccessMessage);

                        }

                    }

                }
                else
                {

                    if (timeTaken != 0)
                    {

                        foreach (TextMeshProUGUI text in confirmerTexts)
                        {

                            text.SetText(loadSuccessMessage + "\nSave time: " + timeTaken.ToString() + " ms");

                        }

                    }
                    else
                    {

                        foreach (TextMeshProUGUI text in confirmerTexts)
                        {

                            text.SetText(loadSuccessMessage);

                        }

                    }

                }

                foreach (GameObject obj in confirmerObjects)
                {

                    obj.SetActive(true);

                }

                yield return new WaitForSecondsRealtime(waitTime);

                foreach (GameObject obj in confirmerObjects)
                {

                    obj.SetActive(false);

                }

            }
            else
            {

                Debug.LogError("!ERROR! " + confirmerObjects.Count + " | " + confirmerTexts.Count + " | " + TimeService.FullDigitalClockTime());

            }

        }
        else
        {

            if (SL)
            {

                if (timeTaken != 0)
                {

                    foreach (TextMeshProUGUI text in confirmerTexts)
                    {

                        text.SetText(saveSuccessMessage + "\nSave time: " + timeTaken.ToString() + " ms");

                    }

                }
                else
                {

                    foreach (TextMeshProUGUI text in confirmerTexts)
                    {

                        text.SetText(saveSuccessMessage);

                    }

                }

            }
            else
            {

                if (timeTaken != 0)
                {

                    foreach (TextMeshProUGUI text in confirmerTexts)
                    {

                        text.SetText(loadSuccessMessage + "\nSave time: " + timeTaken.ToString() + " ms");

                    }

                }
                else
                {

                    foreach (TextMeshProUGUI text in confirmerTexts)
                    {

                        text.SetText(loadSuccessMessage);

                    }

                }

            }

            foreach (GameObject obj in confirmerObjects)
            {

                obj.SetActive(true);

            }

            yield return new WaitForSecondsRealtime(waitTime);

            foreach (GameObject obj in confirmerObjects)
            {

                obj.SetActive(false);

            }

        }

    }

    IEnumerator SetObjectReferences()
    {

        yield return new WaitForSecondsRealtime(1f);

        confirmerObjects.Clear();
        confirmerObjects.AddRange(GameObject.FindObjectsOfType<GameObject>(true));
        List<GameObject> objectsLocal = new List<GameObject>();

        foreach (GameObject obj in confirmerObjects)
        {

            if (obj.tag == "ConfirmerObject")
            {

                objectsLocal.Add(obj);

            }

        }

        confirmerObjects.Clear();

        foreach (GameObject obj in objectsLocal)
        {

            confirmerObjects.Add(obj);

        }

        for (int i = 0; i < confirmerObjects.Count; i++)
        {

            confirmerTexts.Add(confirmerObjects[i].GetComponent<TextMeshProUGUI>());

        }

        bool result;
        if (confirmerObjects.Count != 0 && confirmerTexts.Count != 0)
        {

            result = true;

        }
        else
        {

            result = false;

        }

        yield return result;

    }

}
