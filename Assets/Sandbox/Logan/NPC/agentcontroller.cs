using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentcontroller : MonoBehaviour
{
    public NavMeshAgent agent = null;
    public int walkRadius = 10; // how far NPCs will walk
    //public Vector3 startingpoint;
    public Vector3 destination;
    private Animator anima;
    public boundingstuff bs;
    public bool flag = false;
    public bool stop = false;
    public bool dialogue; // TODO check dialogue state through game manager
    private int stopCount = 0;
    //private bool start = false;
    // Start is called before the first frame update
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.speed = Random.Range(5.0f, 10.0f);
        agent.speed = 5;
        agent.SetDestination(destination);
        //agent.autoRepath = true;
        //agent.autoBraking = true;
        bs = new boundingstuff(agent.speed, 0.5f, 0.3f, transform, agent.nextPosition);
        anima = GetComponent<Animator>();
        flag = true;
        Continue();
    }

    public void FixedUpdate()
    {   // stop ncp if dialogue state is true
        if(GameManager.Instance.CurrentState == GameManager.GameState.TALKING) { // TODO check dialogue state through game manager
            Stop();
            dialogue = true;
            // Call dialogue method/class for Agent from here
        }
        else if (agent != null)
            if (stop)
            {
                bs.Update(transform, transform.position);
                stopCount += 1;
                if (stopCount > 500) {
                    stopCount = 0;
                    Revert();
                }
            }
            else if (agent.nextPosition != null)
                bs.Update(transform, agent.nextPosition);
            else
                bs.Update(transform, transform.position);
        // stop npc if it reaches destination 
        if (Vector3.Distance(agent.destination, transform.position) <= float.Epsilon || stop)
        {
            agent.SetDestination(transform.position);
            anima.SetBool("walking", false);
            stop = true;
            stopCount += 1;
            Stop();
        }
    }

    public void Stop()
    {
        //bs.front = 1;
        anima.SetBool("walking", false);
        stop = true;
    }

    private void Revert() 
    {
        Vector3 newdes = Initializer.getRandomDestination(destination,walkRadius);
        agent.SetDestination(newdes);
        destination = newdes;
        stop = false;
        anima.SetBool("walking", true);
    }

    public void Continue()
    {
        bs.front = agent.speed;
        anima.SetBool("walking", true);
        stop = false;
    }
}
