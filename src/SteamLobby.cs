using System;
using System.Collections.Generic;
using Mirror;
using Steamworks;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class SteamLobby
{
	// Token: 0x060006CE RID: 1742 RVA: 0x00029FF8 File Offset: 0x000281F8
	public SteamLobby()
	{
		if (!EntityStatic.Get<SteamManager>().Initialized)
		{
			return;
		}
		this.lobbyCreated = Callback<LobbyCreated_t>.Create(new Callback<LobbyCreated_t>.DispatchDelegate(this.OnLobbyCreated));
		this.gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(new Callback<GameLobbyJoinRequested_t>.DispatchDelegate(this.OnGameLobbyJoinRequested));
		this.lobbyEntered = Callback<LobbyEnter_t>.Create(new Callback<LobbyEnter_t>.DispatchDelegate(this.OnLobbyEntered));
		this.lobbyListCallback = Callback<LobbyMatchList_t>.Create(new Callback<LobbyMatchList_t>.DispatchDelegate(this.OnGetLobbiesList));
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x0002A08A File Offset: 0x0002828A
	public void HostLobby(string password = null)
	{
		this.pendingLobbyPassword = (password ?? string.Empty);
		SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, NetworkManager.singleton.maxConnections);
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x0002A0B0 File Offset: 0x000282B0
	private void OnLobbyCreated(LobbyCreated_t callback)
	{
		if (callback.m_eResult != EResult.k_EResultOK)
		{
			Debug.Log("Steam lobby create failed");
			this.pendingLobbyPassword = string.Empty;
			return;
		}
		CSteamID csteamID = new CSteamID(callback.m_ulSteamIDLobby);
		GameHelperClient.LobbyID = csteamID;
		string personaName = SteamFriends.GetPersonaName();
		NetworkManager.singleton.StartHost();
		SteamMatchmaking.SetLobbyData(csteamID, "hostName", personaName);
		SteamMatchmaking.SetLobbyData(csteamID, "HostAddress", SteamUser.GetSteamID().ToString());
		SteamMatchmaking.SetLobbyData(csteamID, "playState", "waiting");
		SteamMatchmaking.SetLobbyData(csteamID, "password", this.pendingLobbyPassword ?? string.Empty);
		SteamMatchmaking.SetLobbyJoinable(csteamID, true);
		this.pendingLobbyPassword = string.Empty;
		Debug.Log(string.Format("Steam lobby created: id={0}, host={1}", csteamID, personaName));
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0002A180 File Offset: 0x00028380
	private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
	{
		Debug.Log("Joining Steam lobby from invite");
		GameHelperClient.JoinLobbyID = callback.m_steamIDLobby;
		SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x0002A1A4 File Offset: 0x000283A4
	private void OnLobbyEntered(LobbyEnter_t callback)
	{
		if (NetworkServer.active)
		{
			return;
		}
		string lobbyData = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostAddress");
		NetworkManager.singleton.networkAddress = lobbyData;
		NetworkManager.singleton.StartClient();
		Debug.Log("Steam client started, host=" + lobbyData);
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x0002A1F4 File Offset: 0x000283F4
	public void SearchLobbies()
	{
		SteamMatchmaking.AddRequestLobbyListDistanceFilter(ELobbyDistanceFilter.k_ELobbyDistanceFilterWorldwide);
		SteamMatchmaking.AddRequestLobbyListFilterSlotsAvailable(1);
		SteamMatchmaking.AddRequestLobbyListStringFilter("playState", "waiting", ELobbyComparison.k_ELobbyComparisonEqual);
		SteamMatchmaking.AddRequestLobbyListResultCountFilter(200);
		Debug.Log(string.Format("Requesting Steam lobbies: state={0}, slots>=1, maxResults={1}", "waiting", 200));
		SteamMatchmaking.RequestLobbyList();
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x0002A24C File Offset: 0x0002844C
	private void OnGetLobbiesList(LobbyMatchList_t result)
	{
		this.roomList.Clear();
		uint nLobbiesMatching = result.m_nLobbiesMatching;
		Debug.Log("Steam lobby matches: " + nLobbiesMatching.ToString());
		int num = 0;
		while ((long)num < (long)((ulong)nLobbiesMatching))
		{
			CSteamID lobbyByIndex = SteamMatchmaking.GetLobbyByIndex(num);
			string lobbyData = SteamMatchmaking.GetLobbyData(lobbyByIndex, "hostName");
			string lobbyData2 = SteamMatchmaking.GetLobbyData(lobbyByIndex, "HostAddress");
			string lobbyData3 = SteamMatchmaking.GetLobbyData(lobbyByIndex, "playState");
			string lobbyData4 = SteamMatchmaking.GetLobbyData(lobbyByIndex, "password");
			int numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(lobbyByIndex);
			Debug.Log(string.Format("Steam lobby {0}: id={1}, name={2}, host={3}, state={4}, members={5}/{6}", new object[]
			{
				num,
				lobbyByIndex,
				lobbyData,
				lobbyData2,
				lobbyData3,
				numLobbyMembers,
				NetworkManager.singleton.maxConnections
			}));
			SteamLobby.RoomInfo item = default(SteamLobby.RoomInfo);
			item.lobbyID = lobbyByIndex;
			item.roomName = lobbyData;
			item.playerCount = numLobbyMembers;
			item.isPlaying = lobbyData3.Equals("playing");
			item.password = (string.IsNullOrEmpty(lobbyData4) ? string.Empty : lobbyData4);
			this.roomList.Add(item);
			num++;
		}
		this.roomList.Sort((SteamLobby.RoomInfo left, SteamLobby.RoomInfo right) => left.isPlaying.CompareTo(right.isPlaying));
		UI_StartGame ui = Game.UI.GetUI<UI_StartGame>();
		if (ui == null)
		{
			return;
		}
		ui.UpdateSteamServerList(this.roomList);
	}

	// Token: 0x04000AE6 RID: 2790
	protected Callback<LobbyCreated_t> lobbyCreated;

	// Token: 0x04000AE7 RID: 2791
	protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;

	// Token: 0x04000AE8 RID: 2792
	protected Callback<LobbyEnter_t> lobbyEntered;

	// Token: 0x04000AE9 RID: 2793
	private const string HostAddressKey = "HostAddress";

	// Token: 0x04000AEA RID: 2794
	private const string LobbyPasswordKey = "password";

	// Token: 0x04000AEB RID: 2795
	private const string PlayStateKey = "playState";

	// Token: 0x04000AEC RID: 2796
	private const string PlayStateWaiting = "waiting";

	// Token: 0x04000AED RID: 2797
	private const string PlayStatePlaying = "playing";

	// Token: 0x04000AEE RID: 2798
	private const int MaxLobbySearchResults = 200;

	// Token: 0x04000AEF RID: 2799
	private Callback<LobbyMatchList_t> lobbyListCallback;

	// Token: 0x04000AF0 RID: 2800
	private readonly List<SteamLobby.RoomInfo> roomList = new List<SteamLobby.RoomInfo>();

	// Token: 0x04000AF1 RID: 2801
	private string pendingLobbyPassword = string.Empty;

	// Token: 0x02000159 RID: 345
	public struct RoomInfo
	{
		// Token: 0x04000AF2 RID: 2802
		public CSteamID lobbyID;

		// Token: 0x04000AF3 RID: 2803
		public string roomName;

		// Token: 0x04000AF4 RID: 2804
		public int playerCount;

		// Token: 0x04000AF5 RID: 2805
		public bool isPlaying;

		// Token: 0x04000AF6 RID: 2806
		public string password;
	}
}
