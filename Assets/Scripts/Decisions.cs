using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDecision
{
    IDecision MakeDecision();
}

public class Decisions : MonoBehaviour
{
    void Start()
    {
        // var first = new FirstDecision();
        // var botch = new Chaser();
        FirstDecision first = new FirstDecision();
        
        IDecision test = first;

        while (test != null)
        {
            test = test.MakeDecision();
        }

    }


    public class FirstDecision : MonoBehaviour, IDecision
    {
        public FirstDecision() { }
        private GameObject Target;
        private float Timer;
        public float MaxVel;
        bool hit = false;
        Material AI;

        public IDecision truebranch;
        IDecision falsebranch;
        
        public IDecision MakeDecision()
        {
        Target = GameObject.FindGameObjectWithTag("Chased");
            if (gameObject.tag == "Chaser")
            {
                truebranch = new Chaser().MakeDecision();
                return truebranch;
            }

            if (gameObject.tag == "Chased")
            {
                truebranch = new Chased().MakeDecision();
                return truebranch;
            }
            else
            {
                return null;
            }
        }
        
    }
    public class Chaser : MonoBehaviour, IDecision
    {
        private GameObject Target;
        private float timer;
        Material AI;
        bool hit = false;
        public float MaxVel;

        IDecision truebranch;
        IDecision falsebranch;
        public IDecision MakeDecision()
        {
            if (tag == "Chaser")
            {
                if (timer > 2)
                {
                    Seek();
                    AI = GetComponent<Renderer>().material;
                    AI.color = Color.red;
                    MaxVel = 0.2f;
                }
            }
            if (tag == "Chased")
            {
                truebranch = new Chased().MakeDecision();
            }
            
            
                return null;
            
        }
        void Seek()
        {
            Vector3 Velocity;
            Velocity = Vector3.Normalize(Target.transform.position - transform.position) * MaxVel;
            transform.position = transform.position + Velocity;


        }
    }
    public class Chased : MonoBehaviour, IDecision
    {
        private GameObject Target;
        Material AI;
        bool hit = false;
        public float MaxVel;

        IDecision truebranch;
        IDecision falsebranch;
        public IDecision MakeDecision()
        {
            if (tag == "Chased")
            {
                Target = GameObject.FindGameObjectWithTag("Chaser");
                AI = GetComponent<Renderer>().material;
                AI.color = Color.green;
                MaxVel = 0.1f;
                Flee();
            }
            if (tag == "Chaser")
            {
                truebranch = new Chaser().MakeDecision();
                return truebranch;
            }else
            {
            return null;
            }
        }
        void Flee()
        {
            Vector3 Velocity;
            Velocity = Vector3.Normalize(transform.position - Target.transform.position) * MaxVel;
            transform.position = transform.position + Velocity;

        }
    }
}


