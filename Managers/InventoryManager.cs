using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] List<GameObject> weaponsUIElementsList;
    [SerializeField] List<GameObject> spellBookUIElementsList;
    [SerializeField] List<GameObject> materialsUIElementsList;
    [SerializeField] List<GameObject> questLogUIElementsList;

    [field: NonSerialized] public int openCategory;

    void Start()
    {

        ChangeCategory(1);

    }

    public void ChangeCategory(int categoryInt)
    {

        openCategory = categoryInt;

        if (categoryInt == 1)
        {

            foreach (GameObject uiObject in weaponsUIElementsList)
            {

                uiObject.SetActive(true);

            }

            foreach (GameObject uiObject in spellBookUIElementsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in materialsUIElementsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in questLogUIElementsList)
            {

                uiObject.SetActive(false);

            }

        }
        else if (categoryInt == 2)
        {

            foreach (GameObject uiObject in weaponsUIElementsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in spellBookUIElementsList)
            {

                uiObject.SetActive(true);

            }

            foreach (GameObject uiObject in materialsUIElementsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in questLogUIElementsList)
            {

                uiObject.SetActive(false);

            }

        }
        else if (categoryInt == 3)
        {

            foreach (GameObject uiObject in weaponsUIElementsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in spellBookUIElementsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in materialsUIElementsList)
            {

                uiObject.SetActive(true);

            }

            foreach (GameObject uiObject in questLogUIElementsList)
            {

                uiObject.SetActive(false);

            }

        }
        else if (categoryInt == 4)
        {

            foreach (GameObject uiObject in weaponsUIElementsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in spellBookUIElementsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in materialsUIElementsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in questLogUIElementsList)
            {

                uiObject.SetActive(true);

            }

        }
        else
        {

            Debug.LogError("!ERROR! InventoryManager ChangeCategory categoryInt index out of range error! + " + categoryInt.ToString());

        }

    }

}
