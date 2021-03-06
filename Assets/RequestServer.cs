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

public class RequestServer : MonoBehaviour
{


    void Start()
    {

        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            CreateAccount = true,
            CustomId = GUIDUtility.getUniqueID()
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnPlayFabLoginSuccess, OnLoginError);
    }

    private void OnLoginError(PlayFabError response)
    {
        Debug.Log(response.ToString());
    }

    String sessionID;
    private void OnPlayFabLoginSuccess(LoginResult response)
    {
        sessionID = response.SessionTicket;
        Debug.Log(response.ToString());
        requestMultiplayerServer();
    }

    private void requestMultiplayerServer()
    {
        RequestMultiplayerServerRequest requestData = new RequestMultiplayerServerRequest();
        requestData.BuildId = "1ba80645-10ff-492d-93e1-b31f9e56c1d6";
        requestData.SessionId = "c03b9f6a-a392-4e72-82c5-655ed6be4395";
        requestData.PreferredRegions = new List<AzureRegion>() { AzureRegion.NorthEurope };
        PlayFabMultiplayerAPI.RequestMultiplayerServer(requestData, OnRequestMultiplayerServer, OnRequestMultiplayerServerError);
    }

    private void OnRequestMultiplayerServer(RequestMultiplayerServerResponse response)
    {
        Debug.Log(response.ToString());
        Debug.Log(response.IPV4Address);
        Debug.Log((ushort)response.Ports[0].Num);
    }

    private void OnRequestMultiplayerServerError(PlayFabError error)
    {
        Debug.Log(error.ToString());
    }



}
