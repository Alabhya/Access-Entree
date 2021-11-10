using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Diagnostics;

public class Initializer : MonoBehaviour
{
    public int number_of_agents = 20;
    //public GameObject NPC1;
    //public GameObject NPC2;
    public GameObject NPC;
    //private float[] heights = new float[3] { 0.66667f, 10.66667f, 20.66667f };
    private GameObject[] agents;
    private Vector3[] forces;

    private int[,] skipper;
    void Start()
    {
        skipper = new int[number_of_agents, number_of_agents];
        forces = new Vector3[number_of_agents];
        for (int i = 0; i < number_of_agents; i++)
        {
            forces[i] = Vector3.zero;
        }

        agents = new GameObject[number_of_agents];
        Vector3[] spots = new Vector3[2];
        agentcontroller ascript;
        //int switcher = 0;
        for (int i = 0; i < number_of_agents; i++)
        {
            spots = Startandend();
            //if (switcher == 0)
            //{
            //    agents[i] = Instantiate(NPC1, spots[0], Quaternion.identity);

            //}
            //else {
            //    agents[i] = Instantiate(NPC2, spots[0], Quaternion.identity);
            //}
            //switcher = 1 - switcher;
            agents[i] = Instantiate(NPC, spots[0], Quaternion.identity);
            ascript = agents[i].GetComponent<agentcontroller>();
            //ascript.startingpoint = spots[0];
            ascript.destination = spots[1];
            //ascript.Start();
        }
    }

    Vector3[] Startandend()
    {
        Vector3[] result = new Vector3[2];
        result[0] = GetRandomLocation();
        do
        {
            result[1] = GetRandomLocation();
        } while ((result[0] - result[1]).sqrMagnitude < 100);
        return result;
    }

    public static Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        //UnityEngine.Debug.Log(navMeshData.indices.Length);
        //UnityEngine.Debug.Log(navMeshData.vertices.Length);
        int t = Random.Range(0, navMeshData.indices.Length - 3);

        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        point = Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

        return point;
    }

    private void FixedUpdate()
    {
        Vector3 temp1;
        Vector3 temp2;
        float time;
        for (int i = 0; i < number_of_agents - 1; i++)
        {
            var aci = agents[i].GetComponent<agentcontroller>();
            if (!aci.flag)
                return;
            for (int j = i + 1; j < number_of_agents; j++)
            {
                var acj = agents[j].GetComponent<agentcontroller>();
                if (!acj.flag)
                    return;
                if (skipper[i, j] >= 1)
                {
                    skipper[i, j] -= 1;
                    continue;
                }
                time = (aci.transform.position - acj.transform.position).magnitude / (aci.bs.front + acj.bs.front);
                skipper[i, j] = (int)time;
                if (time > 1)
                {
                    continue;
                }
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
            else if (forces[i] != Vector3.zero)
            {
                cross = -Vector3.Cross(forces[i], agents[i].transform.up);
                aci.agent.velocity = aci.agent.velocity * 0.8f + (cross.normalized/20 + forces[i].normalized/10 - 0.05f*aci.transform.forward).normalized * aci.agent.speed / 10;
            }
            forces[i] = Vector3.zero;
            aci.bs.Update(aci.transform, aci.agent.nextPosition);
        }
    }
}
