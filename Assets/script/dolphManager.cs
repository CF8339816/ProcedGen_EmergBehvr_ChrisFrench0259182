using System.Collections.Generic;
using UnityEngine;

public class dolphManager : MonoBehaviour
{
  
    [SerializeField] int NumOfDolph = 6;

    [SerializeField] Vector3 velocityDolph;
 
    public dolphSchool dolphSchool;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
        float speedDolph = Random.Range(dolphSchool.minSpeedDolph, dolphSchool.maxSpeedDolph);
        velocityDolph = transform.forward * speedDolph;

    }

    // Update is called once per frame
    void Update()
    {

       

        List<dolphManager> dolph = GetDolph();

        // Calculate flocking steering forces
        Vector3 separationDolph = CalculateSeparationDolph(dolph);
        Vector3 alignmentDolph = CalculateAlignmentDolphh(dolph);
        Vector3 cohesionDolph = CalculateCohesionDolph(dolph);
        Vector3 boundsDolph = AvoidBoundsDolph();

        // Combine forces with user-defined weights
     
        Vector3 accelerationDolph = Vector3.zero;
        accelerationDolph += separationDolph * dolphSchool.separationWeightDolph;
        accelerationDolph += alignmentDolph * dolphSchool.alignmentWeightDolph;
        accelerationDolph += cohesionDolph * dolphSchool.cohesionWeightDolph;
        accelerationDolph += boundsDolph * dolphSchool.boundsWeightDolph;

        // Apply acceleration to velocity and clamp to speed parameters
        
       
        velocityDolph += accelerationDolph * Time.deltaTime;
        float speedDolph = velocityDolph.magnitude;
        speedDolph = Mathf.Clamp(speedDolph, dolphSchool.minSpeedDolph, dolphSchool.maxSpeedDolph);
        velocityDolph = velocityDolph.normalized * speedDolph;

        // Move and rotate the boid
        transform.position += velocityDolph * Time.deltaTime;
        if (velocityDolph != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocityDolph), dolphSchool.rotationSpeedDolph * Time.deltaTime);
        }




    }
}
