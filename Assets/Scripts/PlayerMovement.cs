using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour{

    Rigidbody2D rigid;
    public int jumpHeight;
    public float moveSpeed;
    int jumpCount;
    AudioSource audio;
    public AudioClip do_you_know_da_wae;
    public AudioClip clicking;

    // Use this for initialization
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

        if (!isLocalPlayer)
            return;
        
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
            Jump();

        rigid.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigid.velocity.y);


        if (!audio.isPlaying) {
            audio.clip = clicking;
            audio.Play();
        }

        if(rigid.velocity == Vector2.zero && audio.clip != do_you_know_da_wae)
            audio.Stop();
    }

    void Jump() {

        if (audio.clip != do_you_know_da_wae) {
            audio.clip = do_you_know_da_wae;
            audio.Play();
        }

        jumpCount++;

        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(new Vector2(rigid.velocity.x, jumpHeight), ForceMode2D.Impulse);
    }


    public void ResetJumps(){
        jumpCount = 0;
    }
}
