using System.Collections.Generic;
using UnityEngine;

public class SchoolingFish : MonoBehaviour
{
   // [SerializeField] int NumOfFish = 6;

    [SerializeField] Vector3 velocityFish;

    public dolphSchool dolphSchool;
    public fishSchool fishSchool;
    public sharkSchool sharkSchool;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //------------------ defines inital randomdirection and speed for each fish
        float speedFish = Random.Range(fishSchool.minSpeedFish, fishSchool.maxSpeedFish);
        velocityFish = transform.forward * speedFish;

    }

        // Update is called once per frame
    void Update()
    {

        List<SchoolingFish> closeFishs = GetCloseFishs(); // creates a list of other Fish close by to check ranges and react to

        //------------------ defines the forces that drive the directions of the schooling if the fish 
        Vector3 separationFish = CalculateSeparation(closeFishs);
        Vector3 alignmentFish = CalculateAlignment(closeFishs);
        Vector3 cohesionFish = CalculateCohesion(closeFishs);
        Vector3 boundsFish = AvoidBounds();


        //------------------applies the  user defined values to the generated weights        
        Vector3 accelerationFish = Vector3.zero;
        accelerationFish += separationFish * fishSchool.separationWeightFish;
        accelerationFish += alignmentFish * fishSchool.alignmentWeightFish;
        accelerationFish += cohesionFish * fishSchool.cohesionWeightFish;
        accelerationFish += boundsFish * fishSchool.boundsWeightFish;

        //------------------applies defined acceleration to the velocity vectors and ensures  it does not fall above or below defined minimums  
        velocityFish += accelerationFish * Time.deltaTime;
        float speedFish = velocityFish.magnitude;
        speedFish = Mathf.Clamp(speedFish, fishSchool.minSpeedFish, fishSchool.maxSpeedFish);
        velocityFish = velocityFish.normalized * speedFish;


        //------------------applies movement to the fish 

        transform.position += velocityFish * Time.deltaTime;
        if (velocityFish != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocityFish), fishSchool.rotationSpeedFish * Time.deltaTime);
        }
    }

    //----------------------------------------------------------
    private List<SchoolingFish> GetCloseFishs() // checks for close by fish and moves toward depending on defined values
    {
        List<SchoolingFish> context = new List<SchoolingFish>();
        foreach (SchoolingFish schoolingFish in fishSchool.AllSchoolingFishs)
        {
            if (schoolingFish == this) continue;

            float distance = Vector3.Distance(transform.position, schoolingFish.transform.position);
            if (distance <= fishSchool.closeFishRadius)
            {
                context.Add(schoolingFish);
            }
        }
        return context;
    }
    //----------------------------------------------------------
    private Vector3 CalculateSeparation(List<SchoolingFish> closeFishs)  // checks for other fish and moves toward depending on defined values
    {
        Vector3 separationMove = Vector3.zero;
        int count = 0;

        foreach (SchoolingFish closeFish in closeFishs)
        {
            float distance = Vector3.Distance(transform.position, closeFish.transform.position);
           
            if (distance < fishSchool.separationRadiusFish && distance > 0)
            {
                count++;
              
                Vector3 offset = transform.position - closeFish.transform.position;
                separationMove += offset.normalized / distance;
            }
        }

        if (count > 0) separationMove /= count;
        return separationMove;
    }
    public Vector3 CalculateSeparation(List<SchoolingShark> closeSharks) // checks for sharks nearby and moves away depending on defined values
    {
        Vector3 separationMove = Vector3.zero;
        int count = 0;

        foreach (SchoolingShark closeShark in closeSharks)
        {
            float distance = Vector3.Distance(transform.position, closeShark.transform.position);

            if (distance < sharkSchool.separationRadiusShark && distance > 0)
            {
                count++;

                Vector3 offset = transform.position - closeShark.transform.position;
                separationMove += offset.normalized / distance;
            }
        }

        if (count > 0) separationMove /= count;
        return separationMove;
    }
    public Vector3 CalculateSeparation(List<SchoolingDolph> closeDolphs) // checks for dolphins near by and moves away depending on defined values
    {
        Vector3 separationMove = Vector3.zero;
        int count = 0;

        foreach (SchoolingDolph closeDolph in closeDolphs)
        {
            float distance = Vector3.Distance(transform.position, closeDolph.transform.position);

            if (distance < dolphSchool.separationRadiusDolph && distance > 0)
            {
                count++;

                Vector3 offset = transform.position - closeDolph.transform.position;
                separationMove += offset.normalized / distance;
            }
        }

        if (count > 0) separationMove /= count;
        return separationMove;
    }

    //----------------------------------------------------------



    private Vector3 CalculateAlignment(List<SchoolingFish> closeFishs) //  sets directional  alignment  based  on other  close fish
    {
        if (closeFishs.Count == 0) return transform.forward;

        Vector3 alignmentMove = Vector3.zero;
        foreach (SchoolingFish closeFish in closeFishs)
        {
            alignmentMove += closeFish.velocityFish;
        }

        alignmentMove /= closeFishs.Count;
        return alignmentMove.normalized;
    }
    //----------------------------------------------------------
    private Vector3 CalculateCohesion(List<SchoolingFish> closeFishs) //sets directional alignment to center of largest close school
    {
        if (closeFishs.Count == 0) return Vector3.zero;

        Vector3 centerOfMass = Vector3.zero;
        foreach (SchoolingFish closeFish in closeFishs)
        {
            centerOfMass += closeFish.transform.position;
        }

        centerOfMass /= closeFishs.Count;
       
        Vector3 cohesionMove = centerOfMass - transform.position;
        return cohesionMove.normalized;
    }
    //----------------------------------------------------------
    private Vector3 AvoidBounds()//defines  how far from the spawning location the schooling can occur
    {
     
        Vector3 offset = fishSchool.transform.position - transform.position;
        float distance = offset.magnitude;

        if (distance > fishSchool.spawnRadiusFish)
        {
            return offset.normalized * (distance / fishSchool.spawnRadiusFish);
        }
        return Vector3.zero;
    }
}

