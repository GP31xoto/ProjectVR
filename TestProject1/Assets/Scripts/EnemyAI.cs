using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    
    //Base
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsResource;

    //Patrolling
    public Vector3 resourcePos;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Resource Gathering
    public bool resourceNull;
    bool alreadyCollected;

    //States
    public float collectRange;
    public bool ResourceInSightRange, ResourceInCollectRange;
    public bool foundResource;

    //Audio
    private AudioSource musicSource;//maybe switch this to various musicsources istead of just one (possible upgrade) https://answers.unity.com/questions/175995/can-i-play-multiple-audiosources-from-one-gameobje.html
    public AudioClip talking, footsteps, kissing;
    public AudioClip workBuilding,workIron,workFood,workWood, treeFalling;
    /*Spatial Blend from 2D to 3D.
    Volume Rolloff from logarithmic to linear.
    Set Minimum and Maximum distance.*/ //to make it so it only when close

    private void Awake() {
        musicSource = this.GetComponent<AudioSource>();//add a audio source to the doll, its where the sounds will be coming out of
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {

        ResourceInSightRange = this.gameObject.GetComponent<FieldOfView>().canSeeResource;
        ResourceInCollectRange = Physics.CheckSphere(transform.position, collectRange, whatIsResource);

        if (!ResourceInSightRange && !ResourceInCollectRange) Walking();
        if (ResourceInSightRange && !ResourceInCollectRange) FoundResource();
        if (ResourceInSightRange && ResourceInCollectRange) CollectResources();
    }

    void playSoundEffect(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    private void Walking() {
        playSoundEffect(footsteps);

        if (!walkPointSet) SearchWalkPoint();

        if (foundResource) {
            agent.SetDestination(resourcePos);
        } else if (walkPointSet) {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint;
        if (foundResource) {
            distanceToWalkPoint = resourcePos - walkPoint;
        } else {
            distanceToWalkPoint = transform.position - walkPoint;
        }

        if (distanceToWalkPoint.magnitude < 1f) { 
            walkPointSet = false;
            foundResource = false;
        }
    }

    private void FoundResource() {
        foundResource = true;
        //resourcePos = res.position; // Necessario definir resource
        //agent.SetDestination(res.position); // Necessario definir resource
    }

    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange,walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y,transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        }
    }

    private void CollectResources() {
        //transform.LookAt(resource); check wht resource and play sound or put sound in the resource itself and send message
        if (!resourceNull) {
            Vector3 distanceToResource = transform.position - player.position;
            print(distanceToResource.magnitude);
            if (distanceToResource.magnitude < 3f) {
                print("Collecting");
            }
            //Stay in place??
        }
    }

}
