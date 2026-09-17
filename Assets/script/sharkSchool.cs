using System.Collections.Generic;
using UnityEngine;

public class sharkSchool : MonoBehaviour
{

    [Header("Shark Prefab")]
    public Shark sharkPrefab;
    public int schoolSizeShark = 50;
    public float spawnRadiusShark = 10f;

    [Header("Speed Settings")]
    public float minSpeedShark = 2f;
    public float maxSpeedShark = 5f;
    public float rotationSpeedShark = 4f;

    [Header("Detection Settings")]
    public float neighborRadiusShark = 4f;
    public float separationRadiusShark = 1.5f;

    [Header("Behavior Weights")]
    public float separationWeightShark = 1.5f;
    public float alignmentWeightShark = 1.0f;
    public float cohesionWeightShark = 1.0f;
    public float boundsWeightShark = 2.0f;

    public List<Shark> AllShark { get; private set; } = new List<Shark>();

    void Start()
    {
        for (int i = 0; i < schoolSizeShark; i++)
        {
            // Spawn random coordinates within our radius box
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadiusShark;
            Shark newShark = Instantiate(sharkPrefab, randomPos, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

            newShark.sharkSchool = this;
            AllShark.Add(newShark);
        }
    }
}
