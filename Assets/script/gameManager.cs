using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    
    [SerializeField] int NumOfFish = 6;
    [SerializeField] int NumOfShark = 6;
    [SerializeField] int NumOfDolph = 6;
    [SerializeField] Vector3 velocityFish;
    [SerializeField] Vector3 velocityShark;
    [SerializeField] Vector3 velocityDolph;
    public SchoolingManager fishSchool;
    public SchoolingManager sharkSchool;
    public SchoolingManager dolphSchool;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float speedFish = Random.Range(fishSchool.minSpeedFish, fishSchool.maxSpeedFish);
        fishSchool = transform.forward * speedFish;
       
        float speedShark = Random.Range(sharkSchool.minSpeedShark, sharkSchool.maxSpeedShark);
        fishSchool = transform.forward * speedShark;

        float speedDolph = Random.Range(dolphSchool.minSpeedDolph, dolphSchool.maxSpeedDolph);
        fishSchool = transform.forward * speedDolph;

    }

    // Update is called once per frame
    void Update()
    {

        List<Fish> fish = GetFish();

        // Calculate flocking steering forces
        Vector3 separationFish = CalculateSeparationFish(fish);
        Vector3 alignmentFish = CalculateAlignmentFish(fish);
        Vector3 cohesionFish = CalculateCohesionFish(fish);
        Vector3 boundsFish = AvoidBoundsFish();

        List<Shark> shark = GetShark();

        // Calculate flocking steering forces
        Vector3 separationShark = CalculateSeparationShark(shark);
        Vector3 alignmentShark = CalculateAlignmentSharkh(shark);
        Vector3 cohesionShark = CalculateCohesionShark(shark);
        Vector3 boundsShark = AvoidBoundsShark();

        List<Dolph> dolph = GetDolph();

        // Calculate flocking steering forces
        Vector3 separationDolph = CalculateSeparationDolph(dolph);
        Vector3 alignmentDolph = CalculateAlignmentDolphh(dolph);
        Vector3 cohesionDolph = CalculateCohesionDolph(dolph);
        Vector3 boundsDolph = AvoidBoundsFish();

        // Combine forces with user-defined weights
        Vector3 accelerationFish = Vector3.zero;
        accelerationFish += separationFish * fishSchool.separationWeightFish;
        accelerationFish += alignmentFish * fishSchool.alignmentWeightFish;
        accelerationFish += cohesionFish * fishSchool.cohesionWeightFish;
        accelerationFish += boundsFish * fishSchool.boundsWeightFish;

        Vector3 accelerationShark = Vector3.zero;
        accelerationShark += separationShark * fishSchool.separationWeightShark;
        accelerationShark += alignmentShark * fishSchool.alignmentWeightShark;
        accelerationShark += cohesionShark * fishSchool.cohesionWeightShark;
        accelerationShark += boundsShark * fishSchool.boundsWeightShark;

        Vector3 accelerationDolph = Vector3.zero;
        accelerationDolph += separationDolph * fishSchool.separationWeightDolph;
        accelerationDolph += alignmentDolph * fishSchool.alignmentWeightDolph;
        accelerationDolph += cohesionDolph * fishSchool.cohesionWeightDolph;
        accelerationDolph += boundsDolph * fishSchool.boundsWeightDolph;

        // Apply acceleration to velocity and clamp to speed parameters
        velocityFish += accelerationFish * Time.deltaTime;
        float speedFish = velocityFish.magnitude;
        speedFish = Mathf.Clamp(speedFish, fishSchool.minSpeedFish, fishSchool.maxSpeedFish);
        velocityFish = velocityFish.normalized * speedFish;

        velocityShark += accelerationShark * Time.deltaTime;
        float speedShark = velocityShark.magnitude;
        speedShark = Mathf.Clamp(speedShark, sharkSchool.minSpeedShark, sharkSchool.maxSpeedShark);
        velocityShark = velocityShark.normalized * speedShark;

        velocityDolph += accelerationDolph * Time.deltaTime;
        float speedDolph = velocityDolph.magnitude;
        speedDolph = Mathf.Clamp(speedDolph, dolphSchool.minSpeedDolph, dolphSchool.maxSpeedDolph);
        velocityDolph = velocityDolph.normalized * speedDolph;

        // Move and rotate the boid
        transform.position += velocityFish * Time.deltaTime;
        if (velocityFish != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocityFish), fishSchool.rotationSpeedFish * Time.deltaTime);
        }




    }
}
