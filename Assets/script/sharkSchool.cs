using System.Collections.Generic;
using UnityEngine;

public class sharkSchool : MonoBehaviour
{
    [Header("Shark Prefab")]
    //------------------ defines shark school for prefabs
    public SchoolingShark schoolingSharkPrefab;
    public int schoolSizeShark = 4;
    public float spawnRadiusShark = 50f;

    [Header("Speed Settings")]
    //------------------ defines shark speed
    public float minSpeedShark = 4f;
    public float maxSpeedShark = 8f;
    public float rotationSpeedShark = 4f;

    [Header("Detection Settings")]
    //------------------ defines how close Sharks can get
    public float closeSharkRadius = 4f;
    public float separationRadiusShark = 1.5f;
    //------------------defines how close Fish can get
    public float closeFishRadius = 1f;
    public float separationRadiusFish = 0.5f;
    //------------------defines how close Dolphins can get
    public float closeDolphRadius = 4f;
    public float separationRadiusDolph = 1.5f;

    [Header("Behavior Weights")]
    //------------------ defines behavior around sharks
    public float separationWeightShark = 1.5f;
    public float alignmentWeightShark = 1.0f;
    public float cohesionWeightShark = 1.0f;
    public float boundsWeightShark = 2.0f;
    //------------------ defines behavior around fish
    public float separationWeightFish = 0.5f;
    public float alignmentWeightFish = 5.0f;
    public float cohesionWeightFish = 5.0f;

    //------------------ defines behavior around dolphins
    public float separationWeightDolph = 6.5f;
    public float alignmentWeightDolph = 0.5f;
    public float cohesionWeightDolph = 0.5f;
   


    public List<SchoolingShark> AllSchoolingSharks { get; private set; } = new List<SchoolingShark>();//Creates the Ref list based on the defined number for  the prefab generation

    void Start()
    {
        for (int i = 0; i < schoolSizeShark; i++) // spawns the predetermined  number of Sharks in the pre defined box
        {
           
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadiusShark;
            SchoolingShark newSchoolingShark = Instantiate(schoolingSharkPrefab, randomPos, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

            newSchoolingShark.sharkSchool = this;
            AllSchoolingSharks.Add(newSchoolingShark);
        }
    }
}
