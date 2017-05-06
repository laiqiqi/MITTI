using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamScript : MonoBehaviour {
	protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;
	private CallResult<NumberOfCurrentPlayers_t> m_NumberOfCurrentPlayers;
	// Use this for initialization
	void Start () {
		if(SteamManager.Initialized) {
			string name = SteamFriends.GetPersonaName();
			Debug.Log("Hi "+ name);
		}
	}
	
	// Update is called once per frame
	// void Update () {
		
	// }

	private void OnEnable() {
		Debug.Log("Enable");
		if (SteamManager.Initialized) {
			m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
		}
	}

	private void OnGameOverlayActivated(GameOverlayActivated_t pCallback) {
		if(pCallback.m_bActive != 0) {
			Debug.Log("Steam Overlay has been activated");
		}
		else {
			Debug.Log("Steam Overlay has been closed");
		}
	}
}
