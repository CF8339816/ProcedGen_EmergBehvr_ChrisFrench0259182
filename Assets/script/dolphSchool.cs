using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class dolphSchool : MonoBehaviour
{
    // A guilty pleasure when coding the Dolphins
    // was the truncation of Dolphin to Dolph
    // made  me envision a pod of Dolph Lungrins attacking Sharks
    // then proceeding to round up and catch fish with echo location and air bubbles

    [Header("Dolph Prefab")]
    //------------------ defines Dolphin school size and spread for prefab
    public SchoolingDolph schoolingDolphPrefab;
    public int schoolSizeDolph = 8;
    public float spawnRadiusDolph = 45f;

    [Header("Speed Settings")]
    //------------------ defines dolphin speeds
    public float minSpeedDolph = 3f;
    public float maxSpeedDolph = 8f;
    public float rotationSpeedDolph = 4f;

    [Header("Detection Settings")]
    //------------------ defines how close other dolphins can get
    public float closeDolphRadius = 4f;
    public float separationRadiusDolph = 1.5f;
    //------------------ defines how close sharks can get
    public float closeSharkRadius = 1f;
    public float separationRadiusShark = 1.5f;
    //------------------ defines how close other fish can get
    public float closeFishRadius = 1f;
    public float separationRadiusFish = 0.5f;

    [Header("Behavior Weights")]
    //------------------ defines behavior around othe dolphins 
    public float separationWeightDolph = 1.5f;
    public float alignmentWeightDolph = 1.0f;
    public float cohesionWeightDolph = 1.0f;
    public float boundsWeightDolph = 2.0f;
    //------------------ defines behavior around sharks
    public float separationWeightShark = 1.5f;
    public float alignmentWeightShark = 3.0f;
    public float cohesionWeightShark = 3.0f;
    //------------------ defines behavior around fish
    public float separationWeightFish = 1.5f;
    public float alignmentWeightFish = 5.0f;
    public float cohesionWeightFish = 5.0f;


    public List<SchoolingDolph> AllSchoolingDolphs { get; private set; } = new List<SchoolingDolph>();//Creates the Ref list based on the defined number for  the prefab generation

    void Start()
    {
        for (int i = 0; i < schoolSizeDolph; i++) // spawns the predetermined  number of dolphins in the pre defined box
        {
           
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadiusDolph;
            SchoolingDolph newSchoolingDolph = Instantiate(schoolingDolphPrefab, randomPos, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

            newSchoolingDolph.dolphSchool = this;
            AllSchoolingDolphs.Add(newSchoolingDolph);
        }
    }
}