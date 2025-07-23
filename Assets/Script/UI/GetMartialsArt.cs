using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GetMartialsArt : MonoBehaviour
{
    [SerializeField] GameObject[] necessaryArrow;
    [SerializeField] GameObject[] targetArrow;

    List<GameObject> currentNecessaryArrowList;

    Button button;

    int requirementArrow = 0;

    void Start()
    {
        button = GetComponent<Button>();
        currentNecessaryArrowList = new List<GameObject>();
        button.enabled = false;
        requirementArrow = necessaryArrow.Length;
        if (necessaryArrow.Length == 0)
        {
            button.enabled = true;
        }

        ActiveTargetArrow(false);
    }


    void Update()
    {
        if (requirementArrow > 0)
        {
            CheckRequirement();
        }
    }

    void CheckRequirement()
    {
        if (necessaryArrow.Length > 0)
        {
            foreach (GameObject item in necessaryArrow)
            {
                if (item.activeInHierarchy && !currentNecessaryArrowList.Contains(item))
                {
                    currentNecessaryArrowList.Add(item);
                    requirementArrow--;
                }
            }

            if (requirementArrow == 0)
            {
                button.enabled = true;
                requirementArrow = -1;
                return;
            }
        }
    }

    void ActiveTargetArrow(bool active)
    {
        foreach (GameObject item in targetArrow)
        {
            item.SetActive(active);
        }
    }


    public void OnClick()
    {
        ActiveTargetArrow(true);
        button.enabled = false;
    }
}
