using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

// Token: 0x02000343 RID: 835
public class UI_Msg : UGUICtrl
{
	// Token: 0x0600130A RID: 4874 RVA: 0x0007346C File Offset: 0x0007166C
	public UI_Msg()
	{
		this.selfView = new UI_Msg_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_msg", base.GetType());
		this.selfView.ltext_content.text = "";
		this.SetChatInputMode(false);
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x000734C8 File Offset: 0x000716C8
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.selfView.linput_msg.text = "";
		this.selfView.linput_msg.gameObject.SetActive(false);
		GameHelperClient.IsInputChat = false;
		this.SetChatInputMode(false);
		this.UpdateContent(false);
		this.ResetScrollPosition(true);
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ClosePanel()
	{
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x00073524 File Offset: 0x00071724
	private void SendMsg()
	{
		string text = this.selfView.linput_msg.text;
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		this.selfView.linput_msg.text = "";
		GameHelperClient.localPlayer.CmdChat(text);
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x0007356C File Offset: 0x0007176C
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
				this.SendMsg();
			}
			GameHelperClient.IsInputChat = flag;
			this.SetChatInputMode(flag);
			this.UpdateContent(flag);
			this.ResetScrollPosition(!flag);
		}
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x000735EA File Offset: 0x000717EA
	public void ShowMsg(string str, bool isChat = false)
	{
		this.ShowMsg2(str, isChat);
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x000735F4 File Offset: 0x000717F4
	private void ShowMsg2(string str, bool isChat)
	{
		UI_Msg.<ShowMsg2>d__11 <ShowMsg2>d__;
		<ShowMsg2>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<ShowMsg2>d__.<>4__this = this;
		<ShowMsg2>d__.str = str;
		<ShowMsg2>d__.isChat = isChat;
		<ShowMsg2>d__.<>1__state = -1;
		<ShowMsg2>d__.<>t__builder.Start<UI_Msg.<ShowMsg2>d__11>(ref <ShowMsg2>d__);
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x0007363C File Offset: 0x0007183C
	private void TrimNormalMsg()
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

	// Token: 0x06001313 RID: 4883 RVA: 0x000736BC File Offset: 0x000718BC
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

	// Token: 0x06001314 RID: 4884 RVA: 0x0007376C File Offset: 0x0007196C
	private void SetChatInputMode(bool isInputChat)
	{
		this.selfView.sr_content.enabled = isInputChat;
		if (this.selfView.sr_content.verticalScrollbar != null)
		{
			this.selfView.sr_content.verticalScrollbar.gameObject.SetActive(isInputChat);
		}
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x000737C0 File Offset: 0x000719C0
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

	// Token: 0x06001316 RID: 4886 RVA: 0x00073859 File Offset: 0x00071A59
	public void Hide()
	{
		this.selfView.sr_content.gameObject.SetActive(false);
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x00073871 File Offset: 0x00071A71
	public void Show()
	{
		this.selfView.sr_content.gameObject.SetActive(true);
	}

	// Token: 0x0400119B RID: 4507
	private const int MsgMaxCount = 6;

	// Token: 0x0400119C RID: 4508
	public UI_Msg_View selfView;

	// Token: 0x0400119D RID: 4509
	private List<UI_Msg.MsgData> list = new List<UI_Msg.MsgData>();

	// Token: 0x02000344 RID: 836
	private class MsgData
	{
		// Token: 0x0400119E RID: 4510
		public string Str;

		// Token: 0x0400119F RID: 4511
		public bool IsChat;
	}
}
