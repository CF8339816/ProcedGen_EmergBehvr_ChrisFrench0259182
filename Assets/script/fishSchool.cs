using System.Collections.Generic;
using UnityEngine;

public class fishSchool : MonoBehaviour
{

    [Header("Fish Prefab")]
    public Fish fishPrefab;
    public int schoolSizeFish = 50;
    public float spawnRadiusFish = 10f;

    [Header("Speed Settings")]
    public float minSpeedFish = 2f;
    public float maxSpeedFish = 5f;
    public float rotationSpeedFish = 4f;

    [Header("Detection Settings")]
    public float neighborRadiusFish = 4f;
    public float separationRadiusFish = 1.5f;

    [Header("Behavior Weights")]
    public float separationWeightFish = 1.5f;
    public float alignmentWeightFish = 1.0f;
    public float cohesionWeightFish = 1.0f;
    public float boundsWeightFish = 2.0f;

    public List<Fish> AllFish { get; private set; } = new List<Fish>();

    void Start()
    {
        for (int i = 0; i < schoolSizeFish; i++)
        {
            // Spawn random coordinates within our radius box
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadiusFish;
            Fish newFish = Instantiate(fishPrefab, randomPos, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

            newFish.fishSchool = this;
            AllFish.Add(newFish);
        }
    }
}

