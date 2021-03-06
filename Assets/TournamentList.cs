using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using System;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using Mirror;
using PlayFab.Helpers;
using PlayFab.PfEditor.Json;
using UnityEngine.Networking;

public class TournamentList : MonoBehaviour
{

	void Start(){
		//StartCoroutine(GetRequest("http://www.spaceballvr.com/api/model/GetAvailableTournaments.php"));
	}

    public void showTournaments()
    {
        StartCoroutine(GetRequest("http://www.spaceballvr.com/api/model/GetAvailableTournaments.php"));
    }
	IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                listTournaments(webRequest.downloadHandler.text);
            }
        }
    }

    public GameObject tournamentButton;
    public GameObject canvas;
    void listTournaments(String tournamentString){
    	string[] tournamentList = tournamentString.Split('/');
    	for(int i = 0; i < tournamentList.Length;i++){
            if (tournamentList[i] != "")
            {
                GameObject current = Instantiate(tournamentButton, canvas.transform.position, Quaternion.identity);
                current.GetComponent<TournamentButtonReciever>().tournamentID = tournamentList[i];
                current.transform.parent = canvas.transform;
            }
    	}

    }
}
