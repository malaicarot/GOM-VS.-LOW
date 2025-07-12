using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Main Stats")]
    [SerializeField] int resistance = 0;
    [SerializeField] int damage = 0;
    [SerializeField] float HP = 100;


    [Header("Attributes")]
    public List<BaseAttributes> enemyAttributes = new List<BaseAttributes>();

    void Start()
    {

    }

}
