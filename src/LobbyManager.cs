using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000147 RID: 327
public class LobbyManager : IUpdate
{
	// Token: 0x0600064C RID: 1612 RVA: 0x000263B4 File Offset: 0x000245B4
	public void LoadLobbyObject()
	{
		MySystemEvent.Instance.RegisterMessage<string>(26, new Action<Body, string>(this.OnPlayerDisconnectInServer));
		MySystemEvent.Instance.RegisterMessage<LobbyManager.LobbyPlayerInfo>(27, new Action<Body, LobbyManager.LobbyPlayerInfo>(this.OnPlayerConnectInServer));
		MySystemEvent.Instance.RegisterMessage<List<LobbyManager.LobbyPlayerInfo>>(28, new Action<Body, List<LobbyManager.LobbyPlayerInfo>>(this.OnUpdatePlayerLobbyList));
		this.isLobby = true;
		this.map = AssetManager.LoadPrefab("Prefabs/Map", null, true);
		this.playerManager = AssetManager.LoadPrefab("Prefabs/PlayerManager", null, true);
		this.playerManager.transform.position = new Vector3(2.66f, -4.79f, 73.29f);
		PlayerLooby component = this.playerManager.GetComponent<PlayerLooby>();
		for (int i = 0; i < component.playerObjectList.Count; i++)
		{
			if (i != 0)
			{
				component.playerObjectList[i].SetActive(false);
			}
		}
		LobbyPlayerMono component2 = component.playerObjectList[0].GetComponent<LobbyPlayerMono>();
		LobbyManager.LobbyPlayerInfo lobbyPlayerInfo = LobbyManager.BuildLocalLobbyPlayerInfo();
		component2.playerName.text = lobbyPlayerInfo.playerName;
		component2.UploadHead(lobbyPlayerInfo.playerHead);
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x000264C4 File Offset: 0x000246C4
	public void UnLoadLobbyObject()
	{
		MySystemEvent.Instance.UnregisterMessage<string>(26, new Action<Body, string>(this.OnPlayerDisconnectInServer));
		MySystemEvent.Instance.UnregisterMessage<LobbyManager.LobbyPlayerInfo>(27, new Action<Body, LobbyManager.LobbyPlayerInfo>(this.OnPlayerConnectInServer));
		MySystemEvent.Instance.UnregisterMessage<List<LobbyManager.LobbyPlayerInfo>>(28, new Action<Body, List<LobbyManager.LobbyPlayerInfo>>(this.OnUpdatePlayerLobbyList));
		this.isLobby = false;
		if (this.map != null)
		{
			Object.Destroy(this.map);
			this.map = null;
		}
		if (this.playerManager != null)
		{
			Object.Destroy(this.playerManager);
			this.playerManager = null;
		}
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x00026560 File Offset: 0x00024760
	public void RefreshData(string[] data)
	{
		this.lobbyPlayers.Clear();
		for (int i = 0; i < data.Length; i += 3)
		{
			string text = data[i];
			if (!string.IsNullOrEmpty(text) && !this.lobbyPlayers.ContainsKey(text))
			{
				LobbyManager.LobbyPlayerInfo value = default(LobbyManager.LobbyPlayerInfo);
				value.playerAddress = text;
				value.playerName = data[i + 1];
				value.playerHead = data[i + 2];
				this.lobbyPlayers.Add(text, value);
			}
		}
		PlayerLooby component = this.playerManager.GetComponent<PlayerLooby>();
		int count = this.lobbyPlayers.Count;
		for (int j = 0; j < component.playerObjectList.Count; j++)
		{
			GameObject gameObject = component.playerObjectList[j];
			if (j < count)
			{
				if (!gameObject.gameObject.activeSelf)
				{
					gameObject.SetActive(true);
				}
				LobbyManager.LobbyPlayerInfo value2 = this.lobbyPlayers.ElementAt(j).Value;
				LobbyPlayerMono component2 = gameObject.GetComponent<LobbyPlayerMono>();
				component2.playerName.text = value2.playerName;
				component2.UploadHead(value2.playerHead);
			}
			else if (gameObject.gameObject.activeSelf)
			{
				gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x00026690 File Offset: 0x00024890
	public void Update()
	{
		if (this.isLobby && Input.GetMouseButtonDown(0))
		{
			if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}
			Ray ray = Game.Camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				SceneBox component = raycastHit.collider.gameObject.GetComponent<SceneBox>();
				if (component != null)
				{
					component.OpenBox();
				}
				Debug.DrawLine(ray.origin, raycastHit.point, Color.red, 1f);
			}
		}
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0002671F File Offset: 0x0002491F
	private void OnPlayerDisconnectInServer(Body body, string playerAddress)
	{
		if (!this.lobbyPlayers.ContainsKey(playerAddress))
		{
			return;
		}
		this.lobbyPlayers.Remove(playerAddress);
		this.SyncPlayerData();
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x00026743 File Offset: 0x00024943
	private void OnPlayerConnectInServer(Body body, LobbyManager.LobbyPlayerInfo lobbyPlayerInfo)
	{
		if (this.lobbyPlayers.ContainsKey(lobbyPlayerInfo.playerAddress))
		{
			return;
		}
		this.lobbyPlayers.Add(lobbyPlayerInfo.playerAddress, lobbyPlayerInfo);
		this.SyncPlayerData();
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x00026774 File Offset: 0x00024974
	private void OnUpdatePlayerLobbyList(Body body, List<LobbyManager.LobbyPlayerInfo> lobbyPlayerList)
	{
		this.lobbyPlayers.Clear();
		for (int i = 0; i < lobbyPlayerList.Count; i++)
		{
			this.lobbyPlayers.Add(lobbyPlayerList[i].playerAddress, lobbyPlayerList[i]);
		}
		this.SyncPlayerData();
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x000267C4 File Offset: 0x000249C4
	private void SyncPlayerData()
	{
		if (this.lobbyPlayers.Count > 0)
		{
			string[] array = new string[this.lobbyPlayers.Count * 3];
			for (int i = 0; i < this.lobbyPlayers.Count; i++)
			{
				LobbyManager.LobbyPlayerInfo value = this.lobbyPlayers.ElementAt(i).Value;
				array[i * 3] = value.playerAddress;
				array[i * 3 + 1] = value.playerName;
				array[i * 3 + 2] = value.playerHead;
			}
			MyServerNetworkManager myServerNetworkManager = NetworkManager.singleton as MyServerNetworkManager;
			if (myServerNetworkManager == null)
			{
				return;
			}
			myServerNetworkManager.ServerSendAllPlayer(new ClientNetMessage
			{
				clientNetOperation = ClientNetOperation.LobbyPlayerData,
				strs = array
			});
		}
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x00026874 File Offset: 0x00024A74
	private static LobbyManager.LobbyPlayerInfo BuildLocalLobbyPlayerInfo()
	{
		LobbyManager.LobbyPlayerInfo result = default(LobbyManager.LobbyPlayerInfo);
		SteamManager steamManager = EntityStatic.Get<SteamManager>();
		if (steamManager != null && steamManager.Initialized)
		{
			string text = SteamUser.GetSteamID().ToString();
			string personaName = SteamFriends.GetPersonaName();
			result.playerAddress = text;
			result.playerName = (string.IsNullOrWhiteSpace(personaName) ? "Player" : personaName);
			result.playerHead = text;
			return result;
		}
		result.playerAddress = "local";
		result.playerName = "Player";
		result.playerHead = string.Empty;
		return result;
	}

	// Token: 0x04000923 RID: 2339
	private bool isLobby;

	// Token: 0x04000924 RID: 2340
	private GameObject map;

	// Token: 0x04000925 RID: 2341
	private GameObject playerManager;

	// Token: 0x04000926 RID: 2342
	private Dictionary<string, LobbyManager.LobbyPlayerInfo> lobbyPlayers = new Dictionary<string, LobbyManager.LobbyPlayerInfo>();

	// Token: 0x02000148 RID: 328
	public struct LobbyPlayerInfo
	{
		// Token: 0x04000927 RID: 2343
		public string playerAddress;

		// Token: 0x04000928 RID: 2344
		public string playerName;

		// Token: 0x04000929 RID: 2345
		public string playerHead;
	}
}
