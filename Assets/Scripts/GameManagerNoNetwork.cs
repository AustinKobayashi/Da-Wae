using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using System.Linq;
using UnityEngine.UI;

public class GameManagerNoNetwork : MonoBehaviour{

    public int[] scores;
    UnityEngine.Networking.NetworkStartPosition[] spawnPoints;
    public List<GameObject> players = new List<GameObject>();
    LobbyManager lobbyManger;
    bool started;
    public GameObject[] scoreTexts;

    // Use this for initialization
    void OnLevelWasLoaded(int level){

        if (level != 1)
            return;
        
        lobbyManger = GetComponent<LobbyManager>();
        players = lobbyManger.gamePlayers;

        scores = new int[lobbyManger.numPlayers];
        Debug.Log(lobbyManger.numPlayers);
        spawnPoints = FindObjectsOfType<UnityEngine.Networking.NetworkStartPosition>();

        scoreTexts = new GameObject[4];
        scoreTexts[0] = GameObject.FindGameObjectWithTag("Score0");
        scoreTexts[1] = GameObject.FindGameObjectWithTag("Score1");
        scoreTexts[2] = GameObject.FindGameObjectWithTag("Score2");
        scoreTexts[3] = GameObject.FindGameObjectWithTag("Score3");

        started = true;
    }

    // Update is called once per frame
    void Update(){

        if (!started || players.Count != lobbyManger.numPlayers)
            return;
        
        if ((from n in players
             where n.activeSelf == true
             select n).Count() <= 1) {
            Restart();
        }
    }

    void Restart(){

        for (int i = 0; i < players.Count; i++){
            if (players[i].activeSelf){
                scores[i]++;
                scoreTexts[i].GetComponent<Text>().text = scores[i].ToString();
                if (scores[i] >= 5)
                  EndGame(players[i]);
            }
        }

        List<UnityEngine.Networking.NetworkStartPosition> tempSpawnPoints = new List<UnityEngine.Networking.NetworkStartPosition>();

        foreach (UnityEngine.Networking.NetworkStartPosition spawnPoint in spawnPoints)
            tempSpawnPoints.Add(spawnPoint);

        foreach (GameObject player in players){
            if (!player.activeSelf)
                player.SetActive(true);

            int spawnIndex = Random.Range(0, tempSpawnPoints.Count);
            player.transform.position = tempSpawnPoints[spawnIndex].transform.position;
            tempSpawnPoints.RemoveAt(spawnIndex);
        }
    }

    void EndGame(GameObject player){
        Debug.Log("player: " + player + " wins");
        Debug.Break();
    }
}
