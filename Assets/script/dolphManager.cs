using System.Collections.Generic;
using UnityEngine;

public class SchoolingDolph : MonoBehaviour
{

   // [SerializeField] int NumOfDolph = 6;

    [SerializeField] Vector3 velocityDolph;

    public dolphSchool dolphSchool;
    public fishSchool fishSchool;
    public sharkSchool sharkSchool;



    void Start()
    {
        //------------------ defines inital random direction and speed for each Dolphin
        float speedDolph = Random.Range(dolphSchool.minSpeedDolph, dolphSchool.maxSpeedDolph);
        velocityDolph = transform.forward * speedDolph;

    }

       
    void Update()
    {

        List<SchoolingDolph> closeDolphs = GetCloseDolphs();// creates a list of other dopphins close by to check ranges and react to

        //------------------ defines the forces that drive the directions of the schooling if the dolphins or Dolph Luingrins if you prefer   
        Vector3 separationDolph = CalculateSeparation(closeDolphs);
        Vector3 alignmentDolph = CalculateAlignment(closeDolphs);
        Vector3 cohesionDolph = CalculateCohesion(closeDolphs);
        Vector3 boundsDolph = AvoidBounds();


        //------------------applies the  user defined values to the generated weights       
        Vector3 accelerationDolph = Vector3.zero;
        accelerationDolph += separationDolph * dolphSchool.separationWeightDolph;
        accelerationDolph += alignmentDolph * dolphSchool.alignmentWeightDolph;
        accelerationDolph += cohesionDolph * dolphSchool.cohesionWeightDolph;
        accelerationDolph += boundsDolph * dolphSchool.boundsWeightDolph;

        //------------------applies defined acceleration to the velocity vectors and ensures  it does not fall above or below defined minimums  
        velocityDolph += accelerationDolph * Time.deltaTime;
        float speedDolph = velocityDolph.magnitude;
        speedDolph = Mathf.Clamp(speedDolph, dolphSchool.minSpeedDolph, dolphSchool.maxSpeedDolph);
        velocityDolph = velocityDolph.normalized * speedDolph;


        //------------------applies movement to the Dolphin
        transform.position += velocityDolph * Time.deltaTime;
        if (velocityDolph != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocityDolph), dolphSchool.rotationSpeedDolph * Time.deltaTime);
        }
    }

    //----------------------------------------------------------
    private List<SchoolingDolph> GetCloseDolphs() // checks for close by Dolphins and moves toward depending on defined values
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
    public List<SchoolingShark> GetCloseSharks()// checks for close by sharks and moves toward depending on defined values
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




    //----------------------------------------------------------
    private Vector3 CalculateSeparation(List<SchoolingDolph> closeDolphs) // checks for other dolphins and moves away depending on defined values
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

    public Vector3 CalculateSeparation(List<SchoolingFish> closeFishs) // checks for fishs near by and moves away depending on defined values 
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
    public Vector3 CalculateSeparation(List<SchoolingShark> closeSharks)// checks for sharks near by and moves away depending on defined values  
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

//----------------------------------------------------------

  private Vector3 CalculateAlignment(List<SchoolingDolph> closeDolphs) //  sets directional  alignment based  on other  close Dolphins
    {
        if (closeDolphs.Count == 0) return transform.forward;

        Vector3 alignmentMove = Vector3.zero;
        foreach (SchoolingDolph closeDolph in closeDolphs)
        {
            alignmentMove += closeDolph.velocityDolph;
        }

        alignmentMove /= closeDolphs.Count;
        return alignmentMove.normalized;
    }
//----------------------------------------------------------
    private Vector3 CalculateCohesion(List<SchoolingDolph> closeDolphs) //sets directional alignment to center of largest close school
    {
        if (closeDolphs.Count == 0) return Vector3.zero;

        Vector3 centerOfMass = Vector3.zero;
        foreach (SchoolingDolph closeDolph in closeDolphs)
        {
            centerOfMass += closeDolph.transform.position;
        }

        centerOfMass /= closeDolphs.Count;
    
        Vector3 cohesionMove = centerOfMass - transform.position;
        return cohesionMove.normalized;
    }
//----------------------------------------------------------
    private Vector3 AvoidBounds() //defines  how far from the spawning location the schooling can occur
    {
        
        Vector3 offset = dolphSchool.transform.position - transform.position;
        float distance = offset.magnitude;

        if (distance > dolphSchool.spawnRadiusDolph)
        {
            return offset.normalized * (distance / dolphSchool.spawnRadiusDolph);
        }
        return Vector3.zero;
    }
}
