using System.Collections.Generic;
using UnityEngine;

public class sharkManager : MonoBehaviour
{
  
    [SerializeField] int NumOfShark = 6;

    [SerializeField] Vector3 velocityShark;

    public sharkSchool sharkSchool;
 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float speedShark = Random.Range(sharkSchool.minSpeedShark, sharkSchool.maxSpeedShark);
        velocityShark = transform.forward * speedShark;

      
    }

    // Update is called once per frame
    void Update()
    {

       
        List<sharkManager> shark = GetShark();

        // Calculate flocking steering forces
        Vector3 separationShark = CalculateSeparationShark(shark);
        Vector3 alignmentShark = CalculateAlignmentShark(shark);
        Vector3 cohesionShark = CalculateCohesionShark(shark);
        Vector3 boundsShark = AvoidBoundsShark();

        // Combine forces with user-defined weights
      
        Vector3 accelerationShark = Vector3.zero;
        accelerationShark += separationShark * sharkSchool.separationWeightShark;
        accelerationShark += alignmentShark * sharkSchool.alignmentWeightShark;
        accelerationShark += cohesionShark * sharkSchool.cohesionWeightShark;
        accelerationShark += boundsShark * sharkSchool.boundsWeightShark;

        // Apply acceleration to velocity and clamp to speed parameters
       
        velocityShark += accelerationShark * Time.deltaTime;
        float speedShark = velocityShark.magnitude;
        speedShark = Mathf.Clamp(speedShark, sharkSchool.minSpeedShark, sharkSchool.maxSpeedShark);
        velocityShark = velocityShark.normalized * speedShark;

        // Move and rotate the boid
        transform.position += velocityShark * Time.deltaTime;
        if (velocityShark != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocityShark), sharkSchool.rotationSpeedShark * Time.deltaTime);
        }




    }
}
