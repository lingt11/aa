using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Mirror;
using UnityEngine;

// Token: 0x0200033F RID: 831
public class UI_LobbyMsg : UGUICtrl
{
	// Token: 0x060012F9 RID: 4857 RVA: 0x00072E70 File Offset: 0x00071070
	public UI_LobbyMsg()
	{
		this.selfView = new UI_LobbyMsg_View();
		base.OnCreate(this.selfView, "UI/Prefabs/UI_LobbyMsg", base.GetType());
		this.selfView.ltext_content.text = "";
		this.SetLobbyChatInputMode(false);
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x00072ECC File Offset: 0x000710CC
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.selfView.linput_msg.text = "";
		this.selfView.linput_msg.gameObject.SetActive(false);
		GameHelperClient.IsInputChat = false;
		this.SetLobbyChatInputMode(false);
		this.UpdateContent(false);
		this.ResetScrollPosition(true);
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x00072F26 File Offset: 0x00071126
	protected override void ClosePanel()
	{
		GameHelperClient.IsInputChat = false;
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x00072F30 File Offset: 0x00071130
	private void SendLobbyMsg()
	{
		string text = this.selfView.linput_msg.text;
		if (string.IsNullOrWhiteSpace(text))
		{
			return;
		}
		this.selfView.linput_msg.text = "";
		if (!NetworkClient.isConnected || NetworkClient.connection == null)
		{
			return;
		}
		NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
		{
			serverNetOperation = ServerNetOperation.LobbyChat,
			strData = text
		}, 0);
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x00072FA0 File Offset: 0x000711A0
	public override void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			bool flag = !this.selfView.linput_msg.gameObject.activeSelf;
			this.selfView.linput_msg.gameObject.SetActive(flag);
			if (flag)
			{
				this.selfView.linput_msg.ActivateInputField();
			}
			else
			{
				this.SendLobbyMsg();
			}
			GameHelperClient.IsInputChat = flag;
			this.SetLobbyChatInputMode(flag);
			this.UpdateContent(flag);
			this.ResetScrollPosition(!flag);
		}
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x00073020 File Offset: 0x00071220
	public void ShowLobbyChat(string playerName, string textStr, int colorIndex)
	{
		if (string.IsNullOrWhiteSpace(playerName))
		{
			playerName = "Player";
		}
		int num = Mathf.Min(ColorDefine.ChatColor.Length - 1, Mathf.Max(0, colorIndex));
		string arg = PathDefine.Concat(playerName, StringDefine.Colon, textStr);
		this.ShowMsg(string.Format(ColorDefine.ChatColor[num], arg), true);
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x00073073 File Offset: 0x00071273
	public void ShowMsg(string str, bool isChat = false)
	{
		this.ShowMsg2(str, isChat);
	}

	// Token: 0x06001300 RID: 4864 RVA: 0x00073080 File Offset: 0x00071280
	private void ShowMsg2(string str, bool isChat)
	{
		UI_LobbyMsg.<ShowMsg2>d__11 <ShowMsg2>d__;
		<ShowMsg2>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<ShowMsg2>d__.<>4__this = this;
		<ShowMsg2>d__.str = str;
		<ShowMsg2>d__.isChat = isChat;
		<ShowMsg2>d__.<>1__state = -1;
		<ShowMsg2>d__.<>t__builder.Start<UI_LobbyMsg.<ShowMsg2>d__11>(ref <ShowMsg2>d__);
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x000730C8 File Offset: 0x000712C8
	private void TrimLobbyNormalMsg()
	{
		int i = 0;
		for (int j = 0; j < this.list.Count; j++)
		{
			if (!this.list[j].IsChat)
			{
				i++;
			}
		}
		while (i > 6)
		{
			for (int k = 0; k < this.list.Count; k++)
			{
				if (!this.list[k].IsChat)
				{
					this.list.RemoveAt(k);
					i--;
					break;
				}
			}
		}
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x00073148 File Offset: 0x00071348
	private void UpdateContent(bool isInputChat)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (isInputChat)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (this.list[i].IsChat)
				{
					stringBuilder.Append(this.list[i].Str);
				}
			}
		}
		else
		{
			for (int j = Mathf.Max(0, this.list.Count - 6); j < this.list.Count; j++)
			{
				stringBuilder.Append(this.list[j].Str);
			}
		}
		this.selfView.ltext_content.text = stringBuilder.ToString();
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x000731F8 File Offset: 0x000713F8
	private void SetLobbyChatInputMode(bool isInputChat)
	{
		this.selfView.sr_content.enabled = isInputChat;
		if (this.selfView.sr_content.verticalScrollbar != null)
		{
			this.selfView.sr_content.verticalScrollbar.gameObject.SetActive(isInputChat);
		}
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x0007324C File Offset: 0x0007144C
	private void ResetScrollPosition(bool resetContentOffset)
	{
		bool isInputChat = GameHelperClient.IsInputChat;
		this.selfView.sr_content.enabled = true;
		Canvas.ForceUpdateCanvases();
		this.selfView.sr_content.StopMovement();
		if (resetContentOffset && this.selfView.sr_content.content != null)
		{
			this.selfView.sr_content.content.anchoredPosition = Vector2.zero;
		}
		this.selfView.sr_content.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
		this.selfView.sr_content.enabled = isInputChat;
	}

	// Token: 0x0400118D RID: 4493
	private const int MsgMaxCount = 6;

	// Token: 0x0400118E RID: 4494
	public UI_LobbyMsg_View selfView;

	// Token: 0x0400118F RID: 4495
	private readonly List<UI_LobbyMsg.MsgData> list = new List<UI_LobbyMsg.MsgData>();

	// Token: 0x02000340 RID: 832
	private class MsgData
	{
		// Token: 0x04001190 RID: 4496
		public string Str;

		// Token: 0x04001191 RID: 4497
		public bool IsChat;
	}
}
