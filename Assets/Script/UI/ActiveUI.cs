using System.Collections.Generic;
using UnityEngine;

public class ActiveUI : MonoBehaviour
{
    [SerializeField] List<GameObject> UIlist;

    public void ActiveUIBaseName(string name)
    {
        for (int i = 0; i < UIlist.Count; i++)
        {
            UIlist[i].SetActive(false);
            if (UIlist[i].gameObject.name == name)
            {
                UIlist[i].SetActive(true);
            }
        }
    }
}
