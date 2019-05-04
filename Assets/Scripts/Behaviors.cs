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
                // Seek();
                WanderFunc();
                AI = GetComponent<Renderer>().material;
                AI.color = Color.red;
                maxvel = 0.2f;
            }
        } 

        if (tag == "Chased")
        {
            WanderFunc();
            Target = GameObject.FindGameObjectWithTag("Chaser");
            AI = GetComponent<Renderer>().material;
            AI.color = Color.green;
            maxvel = 0.1f;
           // Flee();
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
        if (collision.transform.gameObject.tag.Equals("Chaser") || collision.transform.gameObject.tag.Equals("Chased"))
        {

            timer = 0;
            if (tag == "Chaser")
            {
                hit = true;
                tag = "Chased";
                Target = GameObject.FindGameObjectWithTag("Chaser");
                AI.color = Color.red;

            }
            else if (tag == "Chased")
            {
                hit = true;
                tag = "Chaser";
                Target = GameObject.FindGameObjectWithTag("Chased");
                AI.color = Color.green;
            }

        }
    }
    public float MoveSpeed = .3f;
    public float RotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if(rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        else
        {
            isRotatingLeft = true;
        }
        isWandering = false;

    }
    void WanderFunc()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * RotSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -RotSpeed);
        }
        if (isWalking == true)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
    }
}
