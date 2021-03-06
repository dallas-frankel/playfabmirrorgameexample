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

public class LeaderboardFetcher : MonoBehaviour
{
    private void Start()
    {
        GetLeaderboardRequest glr = new GetLeaderboardRequest();
        PlayFabClientAPI.GetLeaderboard(glr, onSucces, onFail);
            
    }

    void onSucces(GetLeaderboardResult glr)
    {

    }

    void onFail(PlayFabError pfe)
    {

    }
}
