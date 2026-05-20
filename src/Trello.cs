using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class Trello
{
	// Token: 0x06000090 RID: 144 RVA: 0x0000471B File Offset: 0x0000291B
	public Trello(string key, string token)
	{
		this.userKey = key;
		this.userToken = token;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00004754 File Offset: 0x00002954
	private static string GetTrelloJson_Array(string originalJson, string arrayName)
	{
		Regex regex = new Regex("");
		if (!(arrayName == "boards"))
		{
			if (arrayName == "lists")
			{
				regex = new Regex("(?<=\"lists\":)\\[([^\\]])+\\]");
			}
		}
		else
		{
			regex = new Regex("(?<=\"boards\":)\\[([^\\]])+\\]");
		}
		Match match = regex.Match(originalJson);
		string result = string.Empty;
		if (match.Success)
		{
			result = "{\"data\":" + match.Value + "}";
		}
		return result;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x000047D0 File Offset: 0x000029D0
	private static string GetTrelloJson_String(string originalJson, string stringName)
	{
		Regex regex = new Regex("");
		if (!(stringName == "id"))
		{
			if (stringName == "board")
			{
				regex = new Regex("(?<=\"board\":\")([0-9,a-z,A-Z]+)(?=\")");
			}
		}
		else
		{
			regex = new Regex("(?<=\"id\":\")([0-9,a-z,A-Z]+)(?=\")");
		}
		Match match = regex.Match(originalJson);
		string result = string.Empty;
		if (match.Success)
		{
			result = match.Value;
		}
		return result;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0000483C File Offset: 0x00002A3C
	public Task<KeyValuePair<bool, string>> WebRequest_GetUserAllBoards()
	{
		Trello.<WebRequest_GetUserAllBoards>d__14 <WebRequest_GetUserAllBoards>d__;
		<WebRequest_GetUserAllBoards>d__.<>t__builder = AsyncTaskMethodBuilder<KeyValuePair<bool, string>>.Create();
		<WebRequest_GetUserAllBoards>d__.<>4__this = this;
		<WebRequest_GetUserAllBoards>d__.<>1__state = -1;
		<WebRequest_GetUserAllBoards>d__.<>t__builder.Start<Trello.<WebRequest_GetUserAllBoards>d__14>(ref <WebRequest_GetUserAllBoards>d__);
		return <WebRequest_GetUserAllBoards>d__.<>t__builder.Task;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00004880 File Offset: 0x00002A80
	public void SetCurrentBoard(string name)
	{
		foreach (TrelloBoard trelloBoard in this.userAllBoards)
		{
			if (trelloBoard.name == name)
			{
				this.currentBoardId = trelloBoard.id;
				return;
			}
		}
		Debug.LogError("错误: 请填写正确的看板名称!");
	}

	// Token: 0x06000095 RID: 149 RVA: 0x000048F4 File Offset: 0x00002AF4
	public Task<KeyValuePair<bool, string>> WebRequest_GetUserAllLists()
	{
		Trello.<WebRequest_GetUserAllLists>d__16 <WebRequest_GetUserAllLists>d__;
		<WebRequest_GetUserAllLists>d__.<>t__builder = AsyncTaskMethodBuilder<KeyValuePair<bool, string>>.Create();
		<WebRequest_GetUserAllLists>d__.<>4__this = this;
		<WebRequest_GetUserAllLists>d__.<>1__state = -1;
		<WebRequest_GetUserAllLists>d__.<>t__builder.Start<Trello.<WebRequest_GetUserAllLists>d__16>(ref <WebRequest_GetUserAllLists>d__);
		return <WebRequest_GetUserAllLists>d__.<>t__builder.Task;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00004938 File Offset: 0x00002B38
	private void CacheUserAllList()
	{
		foreach (TrelloList trelloList in this.userAllLists)
		{
			string name = trelloList.name;
			string id = trelloList.id;
			if (!this.cachedUserLists.ContainsKey(name))
			{
				this.cachedUserLists.Add(name, id);
			}
		}
	}

	// Token: 0x06000097 RID: 151 RVA: 0x000049AC File Offset: 0x00002BAC
	public Task<KeyValuePair<bool, string>> WebRequest_UploadNewUserList(TrelloList list)
	{
		Trello.<WebRequest_UploadNewUserList>d__18 <WebRequest_UploadNewUserList>d__;
		<WebRequest_UploadNewUserList>d__.<>t__builder = AsyncTaskMethodBuilder<KeyValuePair<bool, string>>.Create();
		<WebRequest_UploadNewUserList>d__.<>4__this = this;
		<WebRequest_UploadNewUserList>d__.list = list;
		<WebRequest_UploadNewUserList>d__.<>1__state = -1;
		<WebRequest_UploadNewUserList>d__.<>t__builder.Start<Trello.<WebRequest_UploadNewUserList>d__18>(ref <WebRequest_UploadNewUserList>d__);
		return <WebRequest_UploadNewUserList>d__.<>t__builder.Task;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x000049F7 File Offset: 0x00002BF7
	public TrelloList NewList(string title, bool isOnRight = true)
	{
		return new TrelloList
		{
			name = title,
			idBoard = this.currentBoardId,
			pos = (isOnRight ? "bottom" : "top")
		};
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00004A28 File Offset: 0x00002C28
	public TrelloCard NewCard(string title, string description, string listName, bool isOnTop = true)
	{
		if (this.cachedUserLists.ContainsKey(listName))
		{
			string listID = this.cachedUserLists[listName];
			return new TrelloCard
			{
				listID = listID,
				name = title,
				description = description,
				position = (isOnTop ? "top" : "bottom")
			};
		}
		Debug.LogError("未找到名为 " + listName + " 的列表, 请检查!");
		return null;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00004A9C File Offset: 0x00002C9C
	public Task<KeyValuePair<bool, string>> WebRequest_UploadNewUserCard(TrelloCard card)
	{
		Trello.<WebRequest_UploadNewUserCard>d__21 <WebRequest_UploadNewUserCard>d__;
		<WebRequest_UploadNewUserCard>d__.<>t__builder = AsyncTaskMethodBuilder<KeyValuePair<bool, string>>.Create();
		<WebRequest_UploadNewUserCard>d__.<>4__this = this;
		<WebRequest_UploadNewUserCard>d__.card = card;
		<WebRequest_UploadNewUserCard>d__.<>1__state = -1;
		<WebRequest_UploadNewUserCard>d__.<>t__builder.Start<Trello.<WebRequest_UploadNewUserCard>d__21>(ref <WebRequest_UploadNewUserCard>d__);
		return <WebRequest_UploadNewUserCard>d__.<>t__builder.Task;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00004AE8 File Offset: 0x00002CE8
	public Task WebRequest_UploadAttachmentToCard_Image(string cardId, string attachmentFileName, Texture2D image)
	{
		Trello.<WebRequest_UploadAttachmentToCard_Image>d__22 <WebRequest_UploadAttachmentToCard_Image>d__;
		<WebRequest_UploadAttachmentToCard_Image>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<WebRequest_UploadAttachmentToCard_Image>d__.<>4__this = this;
		<WebRequest_UploadAttachmentToCard_Image>d__.cardId = cardId;
		<WebRequest_UploadAttachmentToCard_Image>d__.attachmentFileName = attachmentFileName;
		<WebRequest_UploadAttachmentToCard_Image>d__.image = image;
		<WebRequest_UploadAttachmentToCard_Image>d__.<>1__state = -1;
		<WebRequest_UploadAttachmentToCard_Image>d__.<>t__builder.Start<Trello.<WebRequest_UploadAttachmentToCard_Image>d__22>(ref <WebRequest_UploadAttachmentToCard_Image>d__);
		return <WebRequest_UploadAttachmentToCard_Image>d__.<>t__builder.Task;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00004B44 File Offset: 0x00002D44
	public Task WebRequest_UploadAttachmentToCard_String(string cardId, string attachmentFileName, string text)
	{
		Trello.<WebRequest_UploadAttachmentToCard_String>d__23 <WebRequest_UploadAttachmentToCard_String>d__;
		<WebRequest_UploadAttachmentToCard_String>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<WebRequest_UploadAttachmentToCard_String>d__.<>4__this = this;
		<WebRequest_UploadAttachmentToCard_String>d__.cardId = cardId;
		<WebRequest_UploadAttachmentToCard_String>d__.attachmentFileName = attachmentFileName;
		<WebRequest_UploadAttachmentToCard_String>d__.text = text;
		<WebRequest_UploadAttachmentToCard_String>d__.<>1__state = -1;
		<WebRequest_UploadAttachmentToCard_String>d__.<>t__builder.Start<Trello.<WebRequest_UploadAttachmentToCard_String>d__23>(ref <WebRequest_UploadAttachmentToCard_String>d__);
		return <WebRequest_UploadAttachmentToCard_String>d__.<>t__builder.Task;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00004BA0 File Offset: 0x00002DA0
	public Task WebRequest_UploadAttachmentToCard_TextFile(string cardId, string attachmentFileName, string textFilePath)
	{
		Trello.<WebRequest_UploadAttachmentToCard_TextFile>d__24 <WebRequest_UploadAttachmentToCard_TextFile>d__;
		<WebRequest_UploadAttachmentToCard_TextFile>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<WebRequest_UploadAttachmentToCard_TextFile>d__.<>4__this = this;
		<WebRequest_UploadAttachmentToCard_TextFile>d__.cardId = cardId;
		<WebRequest_UploadAttachmentToCard_TextFile>d__.attachmentFileName = attachmentFileName;
		<WebRequest_UploadAttachmentToCard_TextFile>d__.textFilePath = textFilePath;
		<WebRequest_UploadAttachmentToCard_TextFile>d__.<>1__state = -1;
		<WebRequest_UploadAttachmentToCard_TextFile>d__.<>t__builder.Start<Trello.<WebRequest_UploadAttachmentToCard_TextFile>d__24>(ref <WebRequest_UploadAttachmentToCard_TextFile>d__);
		return <WebRequest_UploadAttachmentToCard_TextFile>d__.<>t__builder.Task;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00004BFC File Offset: 0x00002DFC
	private Task WebRequest_UploadAttachmentToCard_Bytes(string cardId, string attachmentFileName, byte[] bytes)
	{
		Trello.<WebRequest_UploadAttachmentToCard_Bytes>d__25 <WebRequest_UploadAttachmentToCard_Bytes>d__;
		<WebRequest_UploadAttachmentToCard_Bytes>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<WebRequest_UploadAttachmentToCard_Bytes>d__.<>4__this = this;
		<WebRequest_UploadAttachmentToCard_Bytes>d__.cardId = cardId;
		<WebRequest_UploadAttachmentToCard_Bytes>d__.attachmentFileName = attachmentFileName;
		<WebRequest_UploadAttachmentToCard_Bytes>d__.bytes = bytes;
		<WebRequest_UploadAttachmentToCard_Bytes>d__.<>1__state = -1;
		<WebRequest_UploadAttachmentToCard_Bytes>d__.<>t__builder.Start<Trello.<WebRequest_UploadAttachmentToCard_Bytes>d__25>(ref <WebRequest_UploadAttachmentToCard_Bytes>d__);
		return <WebRequest_UploadAttachmentToCard_Bytes>d__.<>t__builder.Task;
	}

	// Token: 0x04000090 RID: 144
	private const string MEMBER_BASE_URL = "https://api.trello.com/1/members/me";

	// Token: 0x04000091 RID: 145
	private const string BOARD_BASE_URL = "https://api.trello.com/1/boards/";

	// Token: 0x04000092 RID: 146
	private const string LIST_BASE_URL = "https://api.trello.com/1/lists/";

	// Token: 0x04000093 RID: 147
	private const string CARD_BASE_URL = "https://api.trello.com/1/cards/";

	// Token: 0x04000094 RID: 148
	private string userKey;

	// Token: 0x04000095 RID: 149
	private string userToken;

	// Token: 0x04000096 RID: 150
	private string currentBoardId = string.Empty;

	// Token: 0x04000097 RID: 151
	private List<TrelloBoard> userAllBoards;

	// Token: 0x04000098 RID: 152
	private List<TrelloList> userAllLists;

	// Token: 0x04000099 RID: 153
	private string uri = string.Empty;

	// Token: 0x0400009A RID: 154
	public readonly Dictionary<string, string> cachedUserLists = new Dictionary<string, string>();
}
