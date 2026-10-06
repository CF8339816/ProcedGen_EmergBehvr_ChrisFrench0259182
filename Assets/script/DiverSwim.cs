using System.Collections.Generic;
using UnityEngine;

public class DiverSwim : MonoBehaviour
{
    [Header("DiverPlayer")]
    public DiverSwim SwimmingDiver;
    public int DivingGroup = 1;

    public List<SchoolingFish> AllSchoolingFishs { get; private set; } = new List<SchoolingFish>(); //Creates a list of divers the fish, sharks, and dolphins can  detect against





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
