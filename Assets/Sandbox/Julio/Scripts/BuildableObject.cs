using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObject : MonoBehaviour
{
    public bool canBeModified = false;

    // this will be the trigger to start the event, this reference may not be needed
    [SerializeField] private Mesh upgradeMesh;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private GameObject triggerPoint;
    [SerializeField] private GameObject particleSpawnLocation;
    [SerializeField] private List<string> requiredItems;
    [SerializeField] private List<uint> requiredQuantities;
    [SerializeField] private float EffectDuration = 0;
    // this is the mesh we will use once we upgrade this object
    
    private MeshFilter objectMesh;

    public bool hasBeenUpgraded { get; private set; } = false;

    void Start()
    {
        objectMesh = this.GetComponent<MeshFilter>();
        if (EffectDuration == 0)
        {
            EffectDuration = 1.0f;
        }
    }

    void Update()
    {
        if (triggerPoint != null)
        {
            // trigger point will only be active if we can upgrade this object
            triggerPoint.SetActive(canBeModified);
        }
    }

    // This function will start the construction/upgrade of this object where the mesh will be swapped
    void BeginUpgrade()
    {
        // safety check conditional
        int it = requiredItems.Count < requiredQuantities.Count ? requiredItems.Count : requiredQuantities.Count;
        // Debug.Log("Amount of items to check: " + it);
        for (int i = 0; i < it; ++i)
        {
            Inventory.ReduceItemCount(requiredItems[i], (int)requiredQuantities[i]);
        }

            // will swap the mesh half way thought the animation
            StartCoroutine(SwapMesh(EffectDuration / 2.0f));

        if (particlePrefab == null || particleSpawnLocation == null) { return; }

        // we will spawn the particle effect prefab and retrive the ParticleSystem
        particlePrefab = Instantiate(particlePrefab, particleSpawnLocation.transform.position, particleSpawnLocation.transform.rotation);
        ParticleSystem particles = particlePrefab.GetComponent<ParticleSystem>();

        if (particles != null)
        {
            // We will start playing the particles in case they are paused when spawned and start our coroutine to destroy them later
            particles.Play();
            StartCoroutine(ShutDownParticles(EffectDuration, particles));
        }
    }

    IEnumerator SwapMesh(float time)
    {
        yield return new WaitForSeconds(time);
        // BEWARE: that if upgradeMesh is null aka empty then your object will be mesh-less, will leave this behavior as it could be intended in certain scenarios
        if (objectMesh != null)
        {
            objectMesh.sharedMesh = upgradeMesh;
        }
    }

    IEnumerator ShutDownParticles(float time, ParticleSystem particles)
    {
        yield return new WaitForSeconds(time);
        particles.Stop();
        Destroy(particlePrefab);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            // we will not proccess any logic until we have unlocked the ability to upgrade this item
            if (canBeModified == false){
                // Debug.Log("The object cannot be upgraded/build at this time");
                return; 
            }

            // used to check if we have enough resources in the inventory, will assume yes until proven otherwise
            bool hasEnough = true;

            // safety check conditional
            int it = requiredItems.Count < requiredQuantities.Count ? requiredItems.Count : requiredQuantities.Count;
            // Debug.Log("Amount of items to check: " + it);
            for (int i = 0; i < it; ++i)
            {
                int currItemCount = Inventory.GetItemQuantity(requiredItems[i]);
                // checking the current item count requirement with the inventory amount, if not enought we will break the loop
                // Debug.Log("Checking Item: " + requiredItems[i] + " Needed: " + requiredQuantities[i] + " Have: " + currItemCount);
                if ( currItemCount < (int)requiredQuantities[i]) {
                    hasEnough = false;
                    break;
                }
            }

            if (hasEnough)
            {
                Debug.Log("Player has enought Items!");
                canBeModified = false;
                // start construction call
                BeginUpgrade();
            } 
            //else { // for debbuging purposes only, to be removed
            //    for (int i = 0; i < it; ++i)
            //    {
            //        int currItemCount = Inventory.GetItemQuantity(requiredItems[i]);

            //        Debug.Log("Not enought items. Index: " + i + " Item: " + requiredItems[i] + " Needed: " + requiredQuantities[i] + " Have: " + currItemCount);
            //    }
            //}
        }
    }
}
