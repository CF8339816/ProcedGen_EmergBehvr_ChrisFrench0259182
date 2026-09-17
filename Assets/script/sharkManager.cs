using System.Collections.Generic;
using UnityEngine;

public class SchoolingShark : MonoBehaviour
{

   // [SerializeField] int NumOfShark = 6;

    [SerializeField] Vector3 velocityShark;

    public dolphSchool dolphSchool;
    public fishSchool fishSchool;
    public sharkSchool sharkSchool;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //------------------ defines inital randomdirection and speed for each fish
        float speedShark = Random.Range(sharkSchool.minSpeedShark, sharkSchool.maxSpeedShark);
        velocityShark = transform.forward * speedShark;

    }

        // Update is called once per frame
    void Update()
    {

        List<SchoolingShark> closeSharks = GetCloseSharks();// creates a list of other Sharks close by to check ranges and react to

        //------------------ defines the forces that drive the directions of the schooling if the Sharks 
        Vector3 separationShark = CalculateSeparation(closeSharks);
        Vector3 alignmentShark = CalculateAlignment(closeSharks);
        Vector3 cohesionShark = CalculateCohesion(closeSharks);
        Vector3 boundsShark = AvoidBounds();


        //------------------applies the  user defined values to the generated weights   
        Vector3 accelerationShark = Vector3.zero;
        accelerationShark += separationShark * sharkSchool.separationWeightShark;
        accelerationShark += alignmentShark * sharkSchool.alignmentWeightShark;
        accelerationShark += cohesionShark * sharkSchool.cohesionWeightShark;
        accelerationShark += boundsShark * sharkSchool.boundsWeightShark;

        //------------------applies defined acceleration to the velocity vectors and ensures  it does not fall above or below defined minimums  
        velocityShark += accelerationShark * Time.deltaTime;
        float speedShark = velocityShark.magnitude;
        speedShark = Mathf.Clamp(speedShark, sharkSchool.minSpeedShark, sharkSchool.maxSpeedShark);
        velocityShark = velocityShark.normalized * speedShark;


        //------------------applies movement to the Shark
        transform.position += velocityShark * Time.deltaTime;
        if (velocityShark != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocityShark), sharkSchool.rotationSpeedShark * Time.deltaTime);
        }
    }

    //----------------------------------------------------------
    private List<SchoolingShark> GetCloseSharks()// checks for close by sharks and moves toward depending on defined values
    {
        List<SchoolingShark> context = new List<SchoolingShark>();
        foreach (SchoolingShark schoolingShark in sharkSchool.AllSchoolingSharks)
        {
            if (schoolingShark == this) continue;

            float distance = Vector3.Distance(transform.position, schoolingShark.transform.position);
            if (distance <= sharkSchool.closeSharkRadius)
            {
                context.Add(schoolingShark);
            }
        }
        return context;
    }


    public List<SchoolingDolph> GetCloseDolphs() // checks for close by Dolphins and moves toward depending on defined values
    {
        List<SchoolingDolph> context = new List<SchoolingDolph>();
        foreach (SchoolingDolph schoolingDolph in dolphSchool.AllSchoolingDolphs)
        {
            if (schoolingDolph == this) continue;

            float distance = Vector3.Distance(transform.position, schoolingDolph.transform.position);
            if (distance <= dolphSchool.closeDolphRadius)
            {
                context.Add(schoolingDolph);
            }
        }
        return context;
    }
    public List<SchoolingFish> GetCloseFishs() // checks for close by fishand moves toward depending on defined values
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
    private Vector3 CalculateSeparation(List<SchoolingShark> closeSharks)// checks for otrher sharks near by  and moves away depending on defined values
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
   public Vector3 CalculateSeparation(List<SchoolingFish> closeFishs) // checks for fish near by and moves away depending on defined values
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
    public Vector3 CalculateSeparation(List<SchoolingDolph> closeDolphs)  //checks for dolphins near by and moves away depending on defined values
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
    private Vector3 CalculateAlignment(List<SchoolingShark> closeSharks) //  sets directional  alignment  based  on other  close sharks
    {
        if (closeSharks.Count == 0) return transform.forward;

        Vector3 alignmentMove = Vector3.zero;
        foreach (SchoolingShark closeShark in closeSharks)
        {
            alignmentMove += closeShark.velocityShark;
        }

        alignmentMove /= closeSharks.Count;
        return alignmentMove.normalized;
    }

    //public Vector3 CalculateAlignment(List<SchoolingDolph> closeDolphs) // sets directional  alignment  based  on  close dolphins
    //{
    //    if (closeDolphs.Count == 0) return transform.forward;

    //    Vector3 alignmentMove = Vector3.zero;
    //    foreach (SchoolingDolph closeDolph in closeDolphs)
    //    {
    //        alignmentMove += closeDolph.velocityDolph;
    //    }

    //    alignmentMove /= closeDolphs.Count;
    //    return alignmentMove.normalized;
    //}

    //----------------------------------------------------------
    private Vector3 CalculateCohesion(List<SchoolingShark> closeSharks) //sets directional alignment to center of largest close school
    {
        if (closeSharks.Count == 0) return Vector3.zero;

        Vector3 centerOfMass = Vector3.zero;
        foreach (SchoolingShark closeShark in closeSharks)
        {
            centerOfMass += closeShark.transform.position;
        }

        centerOfMass /= closeSharks.Count;
     
        Vector3 cohesionMove = centerOfMass - transform.position;
        return cohesionMove.normalized;
    }
    //----------------------------------------------------------
    private Vector3 AvoidBounds() //defines  how far from the spawning location the schooling can occur
    {
       
        Vector3 offset = sharkSchool.transform.position - transform.position;
        float distance = offset.magnitude;

        if (distance > sharkSchool.spawnRadiusShark)
        {
            return offset.normalized * (distance / sharkSchool.spawnRadiusShark);
        }
        return Vector3.zero;
    }
}
