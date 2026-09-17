using System.Collections.Generic;
using UnityEngine;

public class fishManager : MonoBehaviour
{
    [SerializeField] int NumOfFish = 6;

    [SerializeField] Vector3 velocityFish;

    public fishSchool fishSchool;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float speedFish = Random.Range(fishSchool.minSpeedFish, fishSchool.maxSpeedFish);
        velocityFish = transform.forward * speedFish;



        // Update is called once per frame
        void Update()
        {

            List<fishManager> fish = GetFish();

            // Calculate flocking steering forces
            Vector3 separationFish = CalculateSeparationFish(fish);
            Vector3 alignmentFish = CalculateAlignmentFish(fish);
            Vector3 cohesionFish = CalculateCohesionFish(fish);
            Vector3 boundsFish = AvoidBoundsFish();


            // Combine forces with user-defined weights
            Vector3 accelerationFish = Vector3.zero;
            accelerationFish += separationFish * fishSchool.separationWeightFish;
            accelerationFish += alignmentFish * fishSchool.alignmentWeightFish;
            accelerationFish += cohesionFish * fishSchool.cohesionWeightFish;
            accelerationFish += boundsFish * fishSchool.boundsWeightFish;

            // Apply acceleration to velocity and clamp to speed parameters
            velocityFish += accelerationFish * Time.deltaTime;
            float speedFish = velocityFish.magnitude;
            speedFish = Mathf.Clamp(speedFish, fishSchool.minSpeedFish, fishSchool.maxSpeedFish);
            velocityFish = velocityFish.normalized * speedFish;


            // Move and rotate the boid
            transform.position += velocityFish * Time.deltaTime;
            if (velocityFish != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocityFish), fishSchool.rotationSpeedFish * Time.deltaTime);
            }




        }
    }
}
