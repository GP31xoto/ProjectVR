using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallete : MonoBehaviour {

    public GameObject pallete;
    public PhysicsPointer pp;
    private bool treeBool = false;
    
    public GameObject tree;
    private Vector3 treePos;
    private Vector3 treeScale;

    public GameObject stone;
    private Vector3 stonePos;
    private Vector3 stoneScale;

    public GameObject npc;
    private Vector3 npcPos;
    private Vector3 npcScale;


    // Start is called before the first frame update
    void Start() {
        //Tree
        treeScale = tree.transform.localScale;
        treePos = tree.transform.position;
        //Stone
        stoneScale = stone.transform.localScale;
        stonePos = stone.transform.position;
        //NPC
        npcScale = npc.transform.localScale;
        npcPos = npc.transform.position;
    }

    // Update is called once per frame
    void Update() {

        try { 
            if (tree.name.Contains(pp.DistanceGrabber.grabbedObject.gameObject.name)){
                treeBool = true;
            } 
        } catch { };

       //Debug.Log("treeBool - " + treeBool);
       // Debug.Log("released - " + pp.released.Equals(true)); 
       // Debug.Log("grabbed - " + pp.DistanceGrabber.grabbedObject.gameObject.name);

        if ((bool)pp.released) {
            if (treeBool) {
                Debug.Log("1");
                GameObject treeInst = Instantiate(pp.grabbableGOR,treePos,Quaternion.identity);
                Debug.Log("2");
                treeInst.transform.SetParent(pallete.transform);
                //tree = treeInst;
                treeBool = false;
                Debug.Log("3");
            }
        }
        
        //} else if (stone.transform.localScale != stoneScale) {
        //    GameObject stoneInst = Instantiate(stone,stonePos,Quaternion.identity);
        //    stone = stoneInst;
        //}
        //if (npc.transform.localScale != npcScale) {
        //    GameObject npcInst = Instantiate(npc,npcPos,Quaternion.identity);
        //    npc = npcInst;
        //}
    }
}