using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentcontroller : MonoBehaviour
{
    public NavMeshAgent agent = null;
    //public Vector3 startingpoint;
    public Vector3 destination;
    public Vector3 cv;
    private Animator anima;
    public boundingstuff bs;
    public bool flag = false;
    public bool stop = false;
    private int stopcum = 0;
    //private bool start = false;
    // Start is called before the first frame update
    public void Start()
    {
        //if (start) {
        //    return;
        //}
        //start = true;
        cv = Vector3.zero;
        //Debug.Log("get agent");
        agent = GetComponent<NavMeshAgent>();
        //Debug.Log("Got agent");
        //agent.speed = Random.Range(5.0f, 10.0f);
        agent.speed = 5;
        agent.SetDestination(destination);
        agent.autoRepath = true;
        agent.autoBraking = true;
        bs = new boundingstuff(agent.speed, 0.5f, 0.3f, transform, agent.nextPosition);
        //Debug.Log("get anima");
        anima = GetComponent<Animator>();
        //Debug.Log(anima.runtimeAnimatorController.name);
        flag = true;
        Continue();
    }

    public void FixedUpdate()
    {
        if (agent != null)
            if (stop)
            {
                bs.Update(transform, transform.position);
                stopcum += 1;
                if (stopcum > 500) {
                    stopcum = 0;
                    Revert();
                }
            }
            else if (agent.nextPosition != null)
                bs.Update(transform, agent.nextPosition);
            else
                bs.Update(transform, transform.position);
        if (Vector3.Distance(agent.destination, transform.position) <= float.Epsilon || stop)
        {
            agent.SetDestination(transform.position);
            //Debug.Log("1");
            anima.SetBool("walking", false);
            stop = true;
            stopcum += 1;
            Stop();
        }
    }

    public void Stop()
    {
        //bs.front = 1;
        //Debug.Log("2");
        anima.SetBool("walking", false);
        stop = true;
    }

    private void Revert() 
    {
        Vector3 newdes;
        do
        {
            newdes = Initializer.GetRandomLocation();
        } while ((destination - newdes).sqrMagnitude < 100);
        agent.SetDestination(newdes);
        destination = newdes;
        stop = false;
        anima.SetBool("walking", true);
    }

    public void Continue()
    {
        bs.front = agent.speed;
        //Debug.Log("3");
        anima.SetBool("walking", true);
        stop = false;
    }
}
