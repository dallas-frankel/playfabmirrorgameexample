using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentButtonReciever : MonoBehaviour
{
	public string tournamentID;
    public void callJoinTournament(){
    	TournamentJoiner.tj.StartJoinTournament(tournamentID);

    }
}