using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Cow_wanderTimer : MonoBehaviour
{
    public Vector3 wanderPoint;
    public float Speed;
    public float pointDistance;
    public GameObject wanderTarget;
    public int AI_CowCase;

    private float Timer;
    private float rotAngleNav;
    public float wanderSwitch;

    void Start()
    {
        Speed = 1;
        rotAngleNav = Random.Range(0, 300);
        Timer = Random.Range(5, 20);
        AI_CowCase = GetComponent<CowMove>().AI_Case;
        wanderPoint = transform.position;
        wanderSwitch = Random.Range(0, 100);
        
        

    }

    private void cowWander()
    {
        if (Timer <= 0.0 && AI_CowCase == 1 && wanderSwitch <= 20)
        {
            pointDistance = Random.Range(0, 10);
            rotAngleNav = Random.Range(0, 300);
            wanderTarget.transform.rotation = Quaternion.Euler(0, rotAngleNav, 0);
            wanderPoint = wanderTarget.transform.position + (wanderTarget.transform.forward * pointDistance);
            
            Timer = Random.Range(5, 20);
            
        }
        else if(AI_CowCase != 1)
        {
            wanderPoint = transform.position;
        }
    }


    void Update()
    {

        AI_CowCase = GetComponent<CowMove>().AI_Case;
        if (Timer <= 0.0)
        {
            wanderSwitch = Random.Range(0, 100);
        }
        cowWander();
       
        Timer -= Time.deltaTime;
        if(AI_CowCase == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rotAngleNav, 0), Speed * Time.deltaTime);
        }
        
        

    }
}
