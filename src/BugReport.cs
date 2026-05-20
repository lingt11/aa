using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class BugReport : MonoBehaviour
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000075 RID: 117 RVA: 0x00003CDC File Offset: 0x00001EDC
	private static string TRELLO_USER_TOKEN_BOARD
	{
		get
		{
			return "不可名状的地牢v" + Application.version;
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003CF0 File Offset: 0x00001EF0
	public void Start()
	{
		BugReport.<Start>d__16 <Start>d__;
		<Start>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<Start>d__.<>4__this = this;
		<Start>d__.<>1__state = -1;
		<Start>d__.<>t__builder.Start<BugReport.<Start>d__16>(ref <Start>d__);
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003D28 File Offset: 0x00001F28
	private void SyncList()
	{
		if (this.userListName == null || this.userListName.Count <= 0)
		{
			this.userListName = new List<string>();
			using (Dictionary<string, string>.KeyCollection.Enumerator enumerator = this.trello.cachedUserLists.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item = enumerator.Current;
					this.userListName.Add(item);
				}
				return;
			}
		}
		foreach (string item2 in this.trello.cachedUserLists.Keys)
		{
			if (!this.userListName.Contains(item2))
			{
				this.userListName.Add(item2);
			}
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00003E0C File Offset: 0x0000200C
	private Task<KeyValuePair<bool, string>> CreateNewList()
	{
		BugReport.<CreateNewList>d__18 <CreateNewList>d__;
		<CreateNewList>d__.<>t__builder = AsyncTaskMethodBuilder<KeyValuePair<bool, string>>.Create();
		<CreateNewList>d__.<>4__this = this;
		<CreateNewList>d__.<>1__state = -1;
		<CreateNewList>d__.<>t__builder.Start<BugReport.<CreateNewList>d__18>(ref <CreateNewList>d__);
		return <CreateNewList>d__.<>t__builder.Task;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00003E4F File Offset: 0x0000204F
	private IEnumerator CaptureScreenshot()
	{
		yield return new WaitForEndOfFrame();
		this.screenshot = ScreenCapture.CaptureScreenshotAsTexture();
		this.ReportError();
		yield break;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00003E60 File Offset: 0x00002060
	private void ReportError()
	{
		BugReport.<ReportError>d__20 <ReportError>d__;
		<ReportError>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<ReportError>d__.<>4__this = this;
		<ReportError>d__.<>1__state = -1;
		<ReportError>d__.<>t__builder.Start<BugReport.<ReportError>d__20>(ref <ReportError>d__);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00003E97 File Offset: 0x00002097
	private void RegisterLogCollect()
	{
		Application.logMessageReceivedThreaded += this.ApplicationOnLogMessageReceived;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00003EAC File Offset: 0x000020AC
	private void ApplicationOnLogMessageReceived(string currentCondition, string currentStacktrace, LogType currentType)
	{
		if ((currentType == LogType.Exception || currentType == LogType.Error) && !BugReport.isReporting)
		{
			BugReport.isReporting = true;
			this.cardTitle = "新增异常";
			this.cardDescription = string.Concat(new string[]
			{
				"平台",
				Application.platform.ToString(),
				"\n版本",
				Application.version,
				"\n游戏已运行时间",
				Time.time.ToString()
			});
			this.cardList = "Bug";
			this.condition = currentCondition;
			this.stacktrace = currentStacktrace;
			this.type = currentType;
			base.StartCoroutine(this.CaptureScreenshot());
		}
	}

	// Token: 0x0400006C RID: 108
	private Trello trello;

	// Token: 0x0400006D RID: 109
	private const string TRELLO_USER_KEY = "cd4fa6bbc0a4e04154163d6e3dbbb6a0";

	// Token: 0x0400006E RID: 110
	private const string TRELLO_USER_TOKEN = "58c3ba927bd006c7439bc3dc0400711d6b6e0e2af55be352ea7fe5c8461bc09d";

	// Token: 0x0400006F RID: 111
	private List<string> userListName;

	// Token: 0x04000070 RID: 112
	private Texture2D screenshot;

	// Token: 0x04000071 RID: 113
	private string cardTitle;

	// Token: 0x04000072 RID: 114
	private string cardDescription;

	// Token: 0x04000073 RID: 115
	private string cardList;

	// Token: 0x04000074 RID: 116
	private string condition;

	// Token: 0x04000075 RID: 117
	private string stacktrace;

	// Token: 0x04000076 RID: 118
	private LogType type;

	// Token: 0x04000077 RID: 119
	public static bool isReporting;

	// Token: 0x04000078 RID: 120
	public bool test;

	// Token: 0x04000079 RID: 121
	public bool close;
}
