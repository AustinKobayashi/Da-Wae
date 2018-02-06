using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour {

    PlayerMovement playerMovement;

    void Start(){
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void OnCollisionEnter2D(Collision2D coll){

        if(coll.collider.gameObject.tag == "Head"){
            coll.collider.gameObject.GetComponent<HeadController>().Die();
        }

        playerMovement.ResetJumps();
    }
}
