using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Diagnostics;

/**
* Abstract: This class initializes a group of nav mesh agents to walk around randomly.
* Note: Yu did not explain or document his code, so any edits or improvements to code is welcome. This code is difficult to read.
* Author: Yu Wong
*/
public class Initializer : MonoBehaviour
{
    public int number_of_agents = 20;
    private int initialWalkRadius = 5; // Will set how far the NPCs will walk initially, however, the agent controller walk radius will control after first itteration. 
    public GameObject NPC;
    private GameObject[] agents;
    private Vector3[] forces;

    private int[,] skipper;
    void Start()
    {
        skipper = new int[number_of_agents, number_of_agents];
        forces = new Vector3[number_of_agents];
        agents = new GameObject[number_of_agents];
        Vector3[] spots = new Vector3[2];
        agentcontroller ascript;

        for (int i = 0; i < number_of_agents; i++)
        {
            forces[i] = Vector3.zero;
            Vector3 start = initStartLocation();
            agents[i] = Instantiate(NPC, start, Quaternion.identity);
            ascript = agents[i].GetComponent<agentcontroller>();
            //ascript.startingpoint = spots[0];
            ascript.destination = getRandomDestination(start,initialWalkRadius);
            //ascript.Start();
        }
    }

    Vector3 initStartLocation() // this is causing an infinite loop when GetRandomLocation() gets invalid data
    {
        Vector3 finalPosition = Vector3.zero;
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        int t = Random.Range(0, navMeshData.indices.Length - 3);
        if (t > 0) {
            finalPosition = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
            finalPosition = Vector3.Lerp(finalPosition, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);
        }
        return finalPosition;
    }

    public static Vector3 getRandomDestination(Vector3 currentPos, int walkRadius)
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += currentPos;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        finalPosition = hit.position;            
        
        return finalPosition;
    }
    /**
    * Abstract: Every set interval will check if NPCs are going to walk into one another and makes them step around instead.
    * Note: FixedUpdate is used instead of update to avoid more freq calls in case of high frame rate
    * Author - Yu Wong (no longer on project)
    */
    private void FixedUpdate()
    {
        Vector3 temp1;
        Vector3 temp2;
        float time;
        for (int i = 0; i < number_of_agents - 1; i++)
        {
            var aci = agents[i].GetComponent<agentcontroller>();
            if (!aci.flag) { return; }
            for (int j = i + 1; j < number_of_agents; j++)
            {
                var acj = agents[j].GetComponent<agentcontroller>();
                if (!acj.flag)
                    return;
                // skip calculating time if not enough time has passed
                if (skipper[i, j] >= 1)
                {
                    skipper[i, j] -= 1;
                    continue;
                }
                time = (aci.transform.position - acj.transform.position).magnitude / (aci.bs.front + acj.bs.front);
                skipper[i, j] = (int)time;
                if (time > 1) // check if NPCs are going to walk into one another
                {
                    continue;
                }
                // make NPCs step around by calculating "forces"
                temp1 = aci.bs.CDs(acj.bs.Steparound(aci.transform.position), aci.transform.position, 3);
                temp2 = acj.bs.CDs(aci.bs.Steparound(acj.transform.position), acj.transform.position, 3);
                if (temp1 != Vector3.zero || temp2 != Vector3.zero)
                {
                    float ratioi;
                    float ratioj;
                    if (aci.stop&&acj.stop)
                    {
                        ratioi=0;
                        ratioj=0;
                    }
                    else if (aci.stop)
                    {
                        ratioi=0;
                        ratioj=1;
                    }
                    else if (acj.stop)
                    {
                        ratioi=1;
                        ratioj=0;
                    }
                    else
                    {
                        ratioi = aci.agent.speed / (aci.agent.speed + acj.agent.speed);
                        ratioj = 1 - ratioi;
                    }
                    // cpair.Add(new Vector2Int(i, j));
                    if (temp2 != Vector3.zero && ratioj > float.Epsilon)
                        forces[i] += ratioi * (temp2-temp1)/2;
                    else if (temp2 != Vector3.zero)
                        forces[i] += temp2;
                    if (temp1 != Vector3.zero && ratioi > float.Epsilon)
                        forces[j] += ratioj * (temp1-temp2)/2;
                    else if (temp1 != Vector3.zero)
                        forces[j] += temp1;
                }
            }
        }
        Vector3 cross;
        for (int i = 0; i < number_of_agents; i++)
        {
            var aci = agents[i].GetComponent<agentcontroller>();
            if (aci.stop)
                continue;
            else if (forces[i] != Vector3.zero) // apply forces to agent
            {
                cross = -Vector3.Cross(forces[i], agents[i].transform.up);
                aci.agent.velocity = aci.agent.velocity * 0.8f + (cross.normalized/20 + forces[i].normalized/10 - 0.05f*aci.transform.forward).normalized * aci.agent.speed / 10;
            }
            forces[i] = Vector3.zero;
            aci.bs.Update(aci.transform, aci.agent.nextPosition);
        }
    }
}
