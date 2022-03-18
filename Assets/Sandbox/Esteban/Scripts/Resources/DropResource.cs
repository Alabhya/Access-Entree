using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropResource : MonoBehaviour
{
    public string ObjectToAttachTo = "Insert Resource Name Prefab";
    [Space]
    public GameObject Prefab;
    public Transform SpawnPosition;
    private int HowManyToSpawn;
    public enum ResourceType { Wood, Stone }
    public ResourceType TypeOfResource;
    public float TimeTillDestroy;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = Quaternion.Euler(0,4,0);
        switch (TypeOfResource)
        {
            case ResourceType.Wood:
                HowManyToSpawn = 5;
                TimeTillDestroy = 1;
                break;
            case ResourceType.Stone:
                HowManyToSpawn = 4;
                TimeTillDestroy = 1.5f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnResource()
    {
        for (int i = 0; i < HowManyToSpawn; i++)
        {
            float RandomX = Random.Range(-275, 275);
            float RandomZ = Random.Range(-275, 275);
            GameObject Cube = Instantiate(Prefab, SpawnPosition.position, this.transform.rotation);
            Cube.GetComponent<Rigidbody>().AddForce(new Vector3(RandomX,0,RandomZ));
        }
        Destroy(this.gameObject);
    }
}
