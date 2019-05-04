using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteSM : MonoBehaviour {
    public enum States
    {
        Patrol,
        Seek,
        Flee
    }

     Vector3[] patrolLocations;
     int currentPatrolIndex;
    public List<GameObject> Targets;
        public Transform fleeTarget;
        public Transform seekTarget;
    public float maxvel;
        private States currentState;
    private void Start()
    {
      
        patrolLocations[0] = Targets[0].transform.position;
        patrolLocations[1] = Targets[1].transform.position;
        patrolLocations[2] = Targets[2].transform.position;
        patrolLocations[3] = Targets[3].transform.position;
        maxvel = 0.2f;
        currentState = States.Patrol;
        currentPatrolIndex = 0;
    }
    void Update()
        {
            switch (currentState)
            {
                case States.Patrol:
                    Patrol();
                    break;
                case States.Seek:
                    Seek();
                    break;
                case States.Flee:
                    Flee();
                    break;
                default:
                    Debug.LogError("Invalid state!");
                    break;
            }

        }

        void Patrol()
        {
        Vector3 Velocity;
        Velocity = Vector3.Normalize(patrolLocations[currentPatrolIndex] - transform.position) * maxvel;
        transform.position = transform.position + Velocity;
        
    }



        void Seek()
        {
        //Vector3 Velocity;
        //Velocity = Vector3.Normalize(Target.transform.position - transform.position) * maxvel;
        //transform.position = transform.position + Velocity;

    }




        void Flee() { }
    
}
