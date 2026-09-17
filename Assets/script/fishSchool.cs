using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fishSchool : MonoBehaviour
{

    [Header("Fish Prefab")]
    public SchoolingFish schoolingFishPrefab;
    public int schoolSizeFish = 350;
    public float spawnRadiusFish = 40f;

    [Header("Speed Settings")]
    public float minSpeedFish = 2f;
    public float maxSpeedFish = 15f;
    public float rotationSpeedFish = 4f;

    [Header("Detection Settings")]
    //------------------ defines how close Fish can get
    public float closeFishRadius = 4f;
    public float separationRadiusFish = 1.5f;
    //------------------ defines how close sharks can get
    public float closeSharkRadius = 8f;
    public float separationRadiusShark = 4.5f;
    //------------------ defines how close  dolphins can get
    public float closeDolphRadius = 4f;
    public float separationRadiusDolph = 1.5f;


    [Header("Behavior Weights")]
    //------------------ defines behavior around fish
    public float separationWeightFish = 1.5f;
    public float alignmentWeightFish = 1.0f;
    public float cohesionWeightFish = 1.0f;
    public float boundsWeightFish = 4.0f;
    //------------------ defines behavior around sharks
    public float separationWeightShark = 6.5f;
    public float alignmentWeightShark = 0.0f;
    public float cohesionWeightShark = 0.0f;

    //------------------ defines behavior around dolphins
    public float separationWeightDolph = 1.5f;
    public float alignmentWeightDolph = 1.0f;
    public float cohesionWeightDolph = 1.0f;
 

    public List<SchoolingFish> AllSchoolingFishs { get; private set; } = new List<SchoolingFish>(); //Creates the Ref list based on the defined number for  the prefab generation

    void Start()
    {
        for (int i = 0; i < schoolSizeFish; i++)   // spawns the predetermined  number of Fish in the pre defined box
        {
            // Spawn random coordinates within our radius box
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadiusFish;
            SchoolingFish newSchoolingFish = Instantiate(schoolingFishPrefab, randomPos, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

            newSchoolingFish.fishSchool = this;
            AllSchoolingFishs.Add(newSchoolingFish);
        }
    }
}

