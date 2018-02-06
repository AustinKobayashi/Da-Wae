using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour {

    public int[] scores;
    NetworkStartPosition[] spawnPoints;
    public List<GameObject> players = new List<GameObject>();
    public LobbyManager lobbyManger;
    bool started;

    void Start(){
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void OnLevelWasLoaded(int level){

        if (level != 1)
            return;

        players = lobbyManger.gamePlayers;

        scores = new int[lobbyManger.numPlayers];
        Debug.Log(lobbyManger.numPlayers);
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();



        started = true;
    }

    // Update is called once per frame
    void Update(){

        if (!started || players.Count != lobbyManger.numPlayers)
            return;

        if ((from n in players
             where n.activeSelf == true
             select n).Count() <= 1){
            Restart();
        }
    }

    [Server]
    void Restart(){

        for (int i = 0; i < players.Count; i++){
            if (players[i].activeSelf){
                scores[i]++;
                if (scores[i] >= 5)
                    EndGame(players[i]);
            }
        }

        foreach(GameObject player in players){
            PlayerLogic playerController = player.GetComponent<PlayerLogic>();
            playerController.RpcUpdateScoreText(scores);
        }

        List<NetworkStartPosition> tempSpawnPoints = new List<NetworkStartPosition>();

        foreach (NetworkStartPosition spawnPoint in spawnPoints)
            tempSpawnPoints.Add(spawnPoint);

        foreach (GameObject player in players){
            int spawnIndex = Random.Range(0, tempSpawnPoints.Count);
            player.GetComponent<PlayerLogic>().RpcRespawn(tempSpawnPoints[spawnIndex].transform.position);
            tempSpawnPoints.RemoveAt(spawnIndex);
        }
    }

    [Server]
    void EndGame(GameObject player){
        Debug.Log("player: " + player + " wins");
        Debug.Break();
    }
}

