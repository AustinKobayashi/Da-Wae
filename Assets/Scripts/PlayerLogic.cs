using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerLogic : NetworkBehaviour {

    public GameObject[] scoreTexts;

	// Use this for initialization
	void Start () {
        scoreTexts = new GameObject[4];
        scoreTexts[0] = GameObject.FindGameObjectWithTag("Score0");
        scoreTexts[1] = GameObject.FindGameObjectWithTag("Score1");
        scoreTexts[2] = GameObject.FindGameObjectWithTag("Score2");
        scoreTexts[3] = GameObject.FindGameObjectWithTag("Score3");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ClientRpc]
    public void RpcUpdateScoreText(int[] scores){

        for (int i = 0; i < scores.Length; i++)
            scoreTexts[i].GetComponent<Text>().text = scores[i].ToString();
    }

    [ClientRpc]
    public void RpcRespawn(Vector2 position){
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        transform.position = position;
    }
}
