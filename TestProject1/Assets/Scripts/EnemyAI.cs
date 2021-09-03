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

    //Animations (more clips to follow)
    [SerializeField] private Animator npcAnimator;
    public float idleTime;
    public float walkTime;
    public float buildTime;
    public float collectTime;
    public float death1Time;
    public float death2Time;
    public float death3Time;
    public float death4Time;
    public float idleWarTime;
    public float attackTime;
    public float damageTime;
    public float runTime;

    //Resource Gathering
    public bool resourceNull;
    bool alreadyCollected;

    //States
    public float collectRange;
    public bool ResourceInSightRange, ResourceInCollectRange;
    public bool foundResource;
    public bool atWar;

    //Audio
    AudioSource workAudio;//based on how they are in the gameobject
    AudioSource dollAudio;
    public AudioClip talking, footsteps, kissing;
    public AudioClip workBuilding,workIron,workFood,workWood, treeFalling;
    /*Spatial Blend from 2D to 3D.
    Volume Rolloff from logarithmic to linear.
    Set Minimum and Maximum distance.*/ //to make it so it only when close

    private void Awake() {
        AudioSource[] audios = GetComponents<AudioSource>();
        workAudio = audios[0];
        dollAudio = audios[1];//add a audio source to the doll, its where the sounds will be coming out of
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        npcAnimator = GetComponent<Animator>();
        AnimationClip[] clips = npcAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Idle":
                    idleTime = clip.length;
                    break;
                case "Walk":
                    walkTime = clip.length;
                    break;
                case "Build":
                    buildTime = clip.length;
                    break;
                case "CollectResource":
                    collectTime = clip.length;
                    break;
                case "Death1":
                    death1Time = clip.length;
                    break;
                case "Death2":
                    death2Time = clip.length;
                    break;
                case "Death3":
                    death3Time = clip.length;
                    break;
                case "Death4":
                    death4Time = clip.length;
                    break;
                case "Idle_War":
                    idleWarTime = clip.length;
                    break;
                case "Run":
                    runTime = clip.length;
                    break;
                case "Attack":
                    attackTime = clip.length;
                    break;
                case "Damage":
                    damageTime = clip.length;
                    break;
            }
        }
    }

    void Update() {

        ResourceInSightRange = this.gameObject.GetComponent<FieldOfView>().canSeeResource;
        ResourceInCollectRange = Physics.CheckSphere(transform.position, collectRange, whatIsResource);

        if (!ResourceInSightRange && !ResourceInCollectRange) Walking();
        if (ResourceInSightRange && !ResourceInCollectRange) FoundResource();
        if (ResourceInSightRange && ResourceInCollectRange) CollectResources();
        GoToWar();
    }

    void playSoundEffectDoll(AudioClip clip)
    {
        dollAudio.clip = clip;
        dollAudio.Play();
    }

    void playSoundEffectWork(AudioClip clip)
    {
        workAudio.clip = clip;
        workAudio.Play();
    }

    private void Walking() {
        playSoundEffectDoll(footsteps);

        if (!walkPointSet) SearchWalkPoint();
        if (npcAnimator.GetLayerWeight(npcAnimator.GetLayerIndex("War Layer")) == 0f)
        {
            if(npcAnimator.GetFloat("Speed") != 0.5f)
            {
                npcAnimator.SetFloat("Speed", 0.5f);
            }
        }
        else
        {
            if (npcAnimator.GetFloat("Speed") != 1f)
            {
                npcAnimator.SetFloat("Speed", 1f);
            }
        }

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
            if (npcAnimator.GetFloat("Speed") > 0f)
            {
                npcAnimator.SetFloat("Speed", 0f);
            }
            //when does it stop?
            if (!npcAnimator.GetBool("CollectResource"))
            {
                npcAnimator.SetBool("CollectResource", true);
            }
        }
        else
        {
            if (npcAnimator.GetBool("CollectResource"))
            {
                npcAnimator.SetBool("CollectResource", false);
            }
        }
    }

    private void GoToWar()
    {
        if (atWar)
        {
            if(npcAnimator.GetLayerWeight(npcAnimator.GetLayerIndex("War Layer")) <= 0f)
            {
                npcAnimator.SetLayerWeight(npcAnimator.GetLayerIndex("War Layer"), 1f);
            }
        }
        else
        {
            if (npcAnimator.GetLayerWeight(npcAnimator.GetLayerIndex("War Layer")) > 0f)
            {
                npcAnimator.SetLayerWeight(npcAnimator.GetLayerIndex("War Layer"), 0f);
            }
        }
    }

    public void death(int type)
    {
        float timeForDeathAnim = Time.deltaTime;
        //if it's not war death and war layer is active, reset to base; if it is war death and war layer isn't active, activate it
        if (type < 3)
        {
            if (npcAnimator.GetLayerWeight(npcAnimator.GetLayerIndex("War Layer")) > 0f)
            {
                npcAnimator.SetLayerWeight(npcAnimator.GetLayerIndex("War Layer"), 0f);
            }
        }
        else
        {
            if (npcAnimator.GetLayerWeight(npcAnimator.GetLayerIndex("War Layer")) <= 0f)
            {
                npcAnimator.SetLayerWeight(npcAnimator.GetLayerIndex("War Layer"), 1f);
            }
        }
        switch (type)
        {
            case 0:
                timeForDeathAnim = death1Time;
                //in case transition below doesn't work, decomment this:
                //npcAnimator.Play("Base Layer.Death1", 0, 0f);
                break;
            case 1:
                timeForDeathAnim = death2Time;
                //in case transition below doesn't work, decomment this:
                //npcAnimator.Play("Base Layer.Death2", 0, 0f);
                break;
            case 2:
                timeForDeathAnim = death3Time;
                //in case transition below doesn't work, decomment this:
                //npcAnimator.Play("Base Layer.Death3", 0, 0f);
                break;
            case 3:
                timeForDeathAnim = death4Time;
                //in case transition below doesn't work, decomment this:
                //npcAnimator.Play("War Layer.Death4", 0, 0f);
                break;
        }
        //transits to correct death type
        npcAnimator.SetInteger("DeathType", type);
        Destroy(gameObject,timeForDeathAnim);
    }

}
