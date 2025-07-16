using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GetMartialsArt : MonoBehaviour
{
    [SerializeField] GameObject[] necessaryArrow;
    [SerializeField] GameObject[] targetArrow;

    Button button;

    int requirementArrow = 0;

    void Start()
    {
        button = GetComponent<Button>();
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
        CheckRequirement();
    }

    void CheckRequirement()
    {
        if (necessaryArrow.Length == 0)
        {
            return;
        }

        if (requirementArrow == 0)
        {
            button.enabled = true;
        }

        foreach (GameObject item in necessaryArrow)
        {
            if (item.activeInHierarchy)
            {
                requirementArrow--;
            }
        }
    }

    void ActiveTargetArrow(bool active)
    {
        foreach (GameObject item in targetArrow)
        {
            item.SetActive(active);
        }
        // button.enabled = false;
    }


    public void OnClick()
    {
        ActiveTargetArrow(true);
        button.enabled = false;
    }
}
