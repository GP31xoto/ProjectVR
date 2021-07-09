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

    private void Awake() {
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

    private void Walking() {

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
        //transform.LookAt(resource);
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
