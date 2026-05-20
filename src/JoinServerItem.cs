using System;
using Mirror;
using Mirror.Discovery;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003B5 RID: 949
public class JoinServerItem : MonoBehaviour
{
	// Token: 0x060015A5 RID: 5541 RVA: 0x00086A06 File Offset: 0x00084C06
	private void Awake()
	{
		this.joinServerButton.AddButtonEvent(new UnityAction(this.OnJoinServer));
	}

	// Token: 0x060015A6 RID: 5542 RVA: 0x00086A1F File Offset: 0x00084C1F
	public void SetServerResponse(ServerResponse value)
	{
		this.isSteam = false;
		this.serverResponse = value;
		this.serverText.text = value.EndPoint.Address.ToString();
	}

	// Token: 0x060015A7 RID: 5543 RVA: 0x00086A4C File Offset: 0x00084C4C
	public void SetSteamServerResponse(SteamLobby.RoomInfo roomInfoValue)
	{
		this.isSteam = true;
		this.roomInfo = roomInfoValue;
		string text = this.roomInfo.isPlaying ? string.Format(ColorDefine.QuaText[4], Game.Language.Get("游戏中", "")) : "";
		this.serverText.text = string.Concat(new string[]
		{
			"<color=#FF9700>",
			this.roomInfo.roomName,
			"</color>",
			Game.Language.Get("房间显示", ""),
			" (",
			this.roomInfo.playerCount.ToString(),
			"/",
			NetworkManager.singleton.maxConnections.ToString(),
			")",
			text
		});
		this.passwordLock.gameObject.SetActive(!string.IsNullOrEmpty(roomInfoValue.password));
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x00086B48 File Offset: 0x00084D48
	private void OnJoinServer()
	{
		if (this.isSteam)
		{
			if (string.IsNullOrEmpty(this.roomInfo.password))
			{
				this.OnJoinSteamRoom();
				return;
			}
			(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText("", null, null, new Action<string>(this.RoomPasswordCheck), Game.Language.Get("请输入房间密码", ""));
			return;
		}
		else
		{
			UI_StartGame ui = Game.UI.GetUI<UI_StartGame>();
			if (ui != null)
			{
				ui.OnConnectBtnClick();
			}
			MyServerNetworkManager myServerNetworkManager = (MyServerNetworkManager)NetworkManager.singleton;
			if (myServerNetworkManager == null)
			{
				return;
			}
			myServerNetworkManager.Connect(this.serverResponse);
			return;
		}
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x00086BE2 File Offset: 0x00084DE2
	private void RoomPasswordCheck(string password)
	{
		if (password.Equals(this.roomInfo.password))
		{
			this.OnJoinSteamRoom();
			return;
		}
		Util.ShowTipsNoLanguage(string.Format(ColorDefine.RedForColor, Game.Language.Get("密码错误提示", "")));
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x00086C24 File Offset: 0x00084E24
	private void OnJoinSteamRoom()
	{
		if (this.roomInfo.playerCount < NetworkManager.singleton.maxConnections && !this.roomInfo.isPlaying)
		{
			UI_StartGame ui = Game.UI.GetUI<UI_StartGame>();
			if (ui != null)
			{
				ui.OnConnectBtnClick();
			}
			GameHelperClient.JoinLobbyID = this.roomInfo.lobbyID;
			SteamMatchmaking.JoinLobby(this.roomInfo.lobbyID);
		}
	}

	// Token: 0x0400145E RID: 5214
	[SerializeField]
	private Text serverText;

	// Token: 0x0400145F RID: 5215
	[SerializeField]
	private Button joinServerButton;

	// Token: 0x04001460 RID: 5216
	[SerializeField]
	private GameObject passwordLock;

	// Token: 0x04001461 RID: 5217
	private bool isSteam;

	// Token: 0x04001462 RID: 5218
	private SteamLobby.RoomInfo roomInfo;

	// Token: 0x04001463 RID: 5219
	private ServerResponse serverResponse;
}
