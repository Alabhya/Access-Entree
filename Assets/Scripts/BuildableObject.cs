using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

// Made by Julio Delgado
public class BuildableObject : InteractionObj
{
    public bool canBeModified = false;
    public bool isUpgrading = false;
    public bool hasBeenUpgraded { get; private set; } = false;

    // this will be the trigger to start the event, this reference may not be needed
    [SerializeField] private InventorySystem.Inventory inventoryObj;
    [SerializeField] private Mesh upgradeMesh;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private GameObject triggerPoint;
    [SerializeField] private GameObject particleSpawnLocation;
    [SerializeField] private List<string> requiredItems;
    [SerializeField] private List<InventoryItem> requiredInventoryItems;
    [SerializeField] private List<uint> requiredQuantities;
    [SerializeField] private SerializableDictionaryBase<InventoryItem, int> requiredInventory = new SerializableDictionaryBase<InventoryItem, int>();
    [SerializeField] private float EffectDuration = 0;

    private MeshFilter objectMesh;

    void Start()
    {
        base.Start();
        // todo contruct UI button
        objectMesh = this.GetComponent<MeshFilter>();
        if (EffectDuration <= 0)
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

    public SerializableDictionaryBase<InventoryItem, int> GetResourcesList() {
        return requiredInventory;
    }

    // This function will start the construction/upgrade of this object where the mesh will be swapped and particles spawned
    private void BeginUpgrade()
    {
        // safety check conditional in case there are more elements in one of the list
        int it = requiredItems.Count < requiredQuantities.Count ? requiredItems.Count : requiredQuantities.Count;
        for (int i = 0; i < it; ++i)
        {
            Inventory.ReduceItemCount(requiredItems[i], (int)requiredQuantities[i]);
        }

            // will swap the mesh half way thought the animation
            StartCoroutine(SwapMesh(EffectDuration / 2.0f));

        if (particlePrefab == null || particleSpawnLocation == null) { return; }

        // Spawning the particle effect prefab and retriving the ParticleSystem
        particlePrefab = Instantiate(particlePrefab, particleSpawnLocation.transform.position, particleSpawnLocation.transform.rotation);
        ParticleSystem particles = particlePrefab.GetComponent<ParticleSystem>();

        if (particles != null) {
            // Start playing the particles in case they are paused and start our coroutine to destroy them later
            particles.Play();
            StartCoroutine(ShutDownParticles(EffectDuration, particles));
        }
    }

    // handles swapping of the mesh
    private IEnumerator SwapMesh(float time)
    {
        yield return new WaitForSeconds(time);
        // BEWARE: that if upgradeMesh is null aka empty then your object will be mesh-less, will leave this behavior as it could be intended in certain scenarios
        if (objectMesh != null)
        {
            objectMesh.sharedMesh = upgradeMesh;
        }
    }

    // handles destroying the particles
    private IEnumerator ShutDownParticles(float time, ParticleSystem particles)
    {
        yield return new WaitForSeconds(time);
        particles.Stop();
        Destroy(particlePrefab);
        // we should stop any other upgrade events with this bool (ex: player animations, locked movement, camera cinematics, etc)
        isUpgrading = false;
    }

    private void OnTriggerEnter(Collider other) {
       // no longer needed
    }

    public override bool CanInteract() {
        // used to check if we have enough resources in the inventory, will assume yes until proven otherwise
        bool hasEnough = true;

        // safety check conditional
        int it = requiredItems.Count < requiredQuantities.Count ? requiredItems.Count : requiredQuantities.Count;
        for (int i = 0; i < requiredInventoryItems.Count; i++)
        {
            int inventoryItemAmount = inventoryObj.GetItemAmount(requiredInventoryItems[i]);
            // checking the current item count requirement with the inventory amount, if not enought we will break the loop
            if (inventoryItemAmount < (int)requiredInventoryItems[i].itemAmount)
            {
                hasEnough = false;
                break;
            }
        }

        return hasEnough && canBeModified;
    }

    public override void Interaction() {  
        canBeModified = false;
        isUpgrading = true;
        // start construction call
        BeginUpgrade();
    }


    public override void ActivateButtonUI()
    {
        // TODO: Add resource call for UI
        resourceInfoOpenEvent.RaiseEvent(requiredInventory, gameObject);
    }

    public override void DissableButtonUI()
    {
        // TODO: Dissable Button UI object
        resourceInfoCloseEvent.RaiseEvent();
    }
}
