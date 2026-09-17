using System.Collections.Generic;
using UnityEngine;

public class dolphSchool : MonoBehaviour
{

    [Header("Dolph Prefab")]
    public Dolph dolphPrefab;
    public int schoolSizeDolph = 50;
    public float spawnRadiusDolph = 10f;

    [Header("Speed Settings")]
    public float minSpeedDolph = 2f;
    public float maxSpeedDolph = 5f;
    public float rotationSpeedDolph = 4f;

    [Header("Detection Settings")]
    public float neighborRadiusDolph = 4f;
    public float separationRadiusDolph = 1.5f;

    [Header("Behavior Weights")]
    public float separationWeightDolph = 1.5f;
    public float alignmentWeightDolph = 1.0f;
    public float cohesionWeightDolph = 1.0f;
    public float boundsWeightDolph = 2.0f;

    public List<Dolph> AllDolph { get; private set; } = new List<Dolph>();

    void Start()
    {
        for (int i = 0; i < schoolSizeDolph; i++)
        {
            // Spawn random coordinates within our radius box
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadiusDolph;
            Dolph newDolph = Instantiate(dolphPrefab, randomPos, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

            newDolph.dolphSchool = this;
            AllDolph.Add(newDolph);
        }
    }
}
