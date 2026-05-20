using System;
using System.Text;
using Steamworks;
using UnityEngine;

// Token: 0x020003EB RID: 1003
public class UGCWorkItem
{
	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06001718 RID: 5912 RVA: 0x00090258 File Offset: 0x0008E458
	public UGCHandle_t Handle
	{
		get
		{
			return this.m_Handle;
		}
	}

	// Token: 0x06001719 RID: 5913 RVA: 0x00090260 File Offset: 0x0008E460
	public UGCWorkItem()
	{
		this.m_UGCDownloadResult = CallResult<RemoteStorageDownloadUGCResult_t>.Create(new CallResult<RemoteStorageDownloadUGCResult_t>.APIDispatchDelegate(this.OnUGCDownloaded));
	}

	// Token: 0x0600171A RID: 5914 RVA: 0x00090280 File Offset: 0x0008E480
	public void StartDownload(UGCHandle_t handle, Action<UGCWorkItem, SaveLoadManager.TeamBuildData> callback)
	{
		this.m_Handle = handle;
		this.m_OnComplete = callback;
		SteamAPICall_t hAPICall = SteamRemoteStorage.UGCDownload(this.m_Handle, 0U);
		this.m_UGCDownloadResult.Set(hAPICall, null);
	}

	// Token: 0x0600171B RID: 5915 RVA: 0x000902B8 File Offset: 0x0008E4B8
	private void OnUGCDownloaded(RemoteStorageDownloadUGCResult_t pCallback, bool bIOFailure)
	{
		if (pCallback.m_eResult != EResult.k_EResultOK || bIOFailure)
		{
			Debug.LogWarning(string.Format("[Worker] UGC download failed: {0}, result: {1}, ioFailure: {2}", this.m_Handle, pCallback.m_eResult, bIOFailure));
			this.Complete(null);
			return;
		}
		byte[] array = new byte[pCallback.m_nSizeInBytes];
		int num = SteamRemoteStorage.UGCRead(pCallback.m_hFile, array, pCallback.m_nSizeInBytes, 0U, EUGCReadAction.k_EUGCRead_ContinueReadingUntilFinished);
		if (num <= 0)
		{
			Debug.LogWarning(string.Format("[Worker] UGC read returned no data: {0}", this.m_Handle));
			this.Complete(null);
			return;
		}
		try
		{
			SaveLoadManager.TeamBuildData teamBuildData = JsonUtility.FromJson<SaveLoadManager.TeamBuildData>(Encoding.UTF8.GetString(array, 0, num));
			if (teamBuildData != null)
			{
				teamBuildData.isLegacyOrder = (teamBuildData.order <= 0);
				teamBuildData.isBuildDataIncomplete = false;
			}
			this.Complete(teamBuildData);
		}
		catch (Exception ex)
		{
			Debug.LogWarning(string.Format("[Worker] UGC parse failed: {0}, error: {1}", this.m_Handle, ex.Message));
			this.Complete(null);
		}
	}

	// Token: 0x0600171C RID: 5916 RVA: 0x000903C4 File Offset: 0x0008E5C4
	private void Complete(SaveLoadManager.TeamBuildData data)
	{
		Action<UGCWorkItem, SaveLoadManager.TeamBuildData> onComplete = this.m_OnComplete;
		this.m_OnComplete = null;
		if (onComplete == null)
		{
			return;
		}
		onComplete(this, data);
	}

	// Token: 0x040015B8 RID: 5560
	private readonly CallResult<RemoteStorageDownloadUGCResult_t> m_UGCDownloadResult;

	// Token: 0x040015B9 RID: 5561
	private Action<UGCWorkItem, SaveLoadManager.TeamBuildData> m_OnComplete;

	// Token: 0x040015BA RID: 5562
	private UGCHandle_t m_Handle;
}
