using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviors : MonoBehaviour {
    public float maxvel;
    private GameObject Target;
    private Vector3 Force;
    private float timer;
    Material AI;
    bool hit = false;
	void Start () {}
	
	void Update () {
        timer += 1 * Time.deltaTime;

        //Seek(Target.transform.position);
        Target = GameObject.FindGameObjectWithTag("Chased");
        if (tag == "Chaser")
        {
            if (timer > 2)
            {
                Seek();
                AI = GetComponent<Renderer>().material;
                AI.color = Color.red;
                maxvel = 0.2f;
            }
        }

        if (tag == "Chased")
        {
            Target = GameObject.FindGameObjectWithTag("Chaser");
            AI = GetComponent<Renderer>().material;
            AI.color = Color.green;
            maxvel = 0.1f;
            Flee();
        }
    }

    void Seek()
    {
        Vector3 Velocity;
        Velocity = Vector3.Normalize(Target.transform.position - transform.position) * maxvel;
        transform.position = transform.position + Velocity;
        //Force = Velocity - transform.forward;
        //Force = Vector3.ClampMagnitude(Force, maxvel);
        //Velocity = Vector3.ClampMagnitude(Velocity + Force, maxvel);

    }
    void Flee()
    {
        Vector3 Velocity;
        Velocity = Vector3.Normalize(transform.position - Target.transform.position ) * maxvel;
        transform.position = transform.position + Velocity;

    }
    private void OnCollisionEnter(Collision collision)
    {

    
            timer = 0;
            if (tag == "Chaser")
            {
                hit = true;
                tag = "Chased";
                Target = GameObject.FindGameObjectWithTag("Chaser");
               // AI.color = Color.red;
                
            }
           else if (tag == "Chased")
            {
                hit = true;
                tag = "Chaser";
                Target = GameObject.FindGameObjectWithTag("Chased");
               // AI.color = Color.green;
            }
            
        
    }
}
