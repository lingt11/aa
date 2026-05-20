using System;
using System.Text;
using Steamworks;
using UnityEngine;

// Token: 0x020003ED RID: 1005
public class SteamLeaderboardManager
{
	// Token: 0x0600172F RID: 5935 RVA: 0x00090974 File Offset: 0x0008EB74
	public void Init()
	{
		this.m_FileShareResult = CallResult<RemoteStorageFileShareResult_t>.Create(new CallResult<RemoteStorageFileShareResult_t>.APIDispatchDelegate(this.OnFileShared));
		this.m_LeaderboardFindResult = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.OnLeaderboardFound));
		this.m_ScoreUploadedResult = CallResult<LeaderboardScoreUploaded_t>.Create(new CallResult<LeaderboardScoreUploaded_t>.APIDispatchDelegate(this.OnScoreUploaded));
		this.m_LeaderboardUGCSetResult = CallResult<LeaderboardUGCSet_t>.Create(new CallResult<LeaderboardUGCSet_t>.APIDispatchDelegate(this.OnUGCAttached));
	}

	// Token: 0x06001730 RID: 5936 RVA: 0x000909E0 File Offset: 0x0008EBE0
	public void UploadResult(int score, int playerCount, SaveLoadManager.TeamBuildData teamData)
	{
		SteamManager steamManager = EntityStatic.Get<SteamManager>();
		if (steamManager == null || !steamManager.Initialized)
		{
			return;
		}
		if (teamData.order <= 0)
		{
			teamData.order = 10000;
		}
		if (teamData.challengeTimestamp <= 0L)
		{
			teamData.challengeTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		}
		teamData.isBuildDataIncomplete = false;
		teamData.rank = score;
		this.m_pendingScore = SteamLeaderboardRankOrder.EncodeLeaderboardScore(score, teamData.order);
		string pchFile = "last_run_build.json";
		string s = JsonUtility.ToJson(teamData);
		byte[] bytes = Encoding.UTF8.GetBytes(s);
		SteamRemoteStorage.FileWrite(pchFile, bytes, bytes.Length);
		Debug.Log(string.Format("[Steam] 开始上传 Build 文件: {0}人模式", playerCount));
		SteamAPICall_t hAPICall = SteamRemoteStorage.FileShare(pchFile);
		this.m_FileShareResult.Set(hAPICall, null);
		this.m_currentPlayerCount = playerCount;
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x00090AA4 File Offset: 0x0008ECA4
	private void OnFileShared(RemoteStorageFileShareResult_t pCallback, bool bIOFailure)
	{
		if (bIOFailure || pCallback.m_eResult != EResult.k_EResultOK)
		{
			Debug.LogError("[Steam] 文件上传失败: " + pCallback.m_eResult.ToString());
			return;
		}
		this.m_currentUGCHandle = pCallback.m_hFile;
		Debug.Log(string.Format("[Steam] 文件上传成功，Handle: {0}", this.m_currentUGCHandle));
		string pchLeaderboardName = "Rank_Solo";
		switch (this.m_currentPlayerCount)
		{
		case 2:
			pchLeaderboardName = "Rank_Duo";
			break;
		case 3:
			pchLeaderboardName = "Rank_Trio";
			break;
		case 4:
			pchLeaderboardName = "Rank_Squad";
			break;
		}
		SteamAPICall_t hAPICall = SteamUserStats.FindLeaderboard(pchLeaderboardName);
		this.m_LeaderboardFindResult.Set(hAPICall, null);
	}

	// Token: 0x06001732 RID: 5938 RVA: 0x00090B54 File Offset: 0x0008ED54
	private void OnLeaderboardFound(LeaderboardFindResult_t pCallback, bool bIOFailure)
	{
		if (pCallback.m_bLeaderboardFound == 0 || bIOFailure)
		{
			Debug.LogError("[Steam] 找不到排行榜，请检查后台 API Name");
			return;
		}
		SteamLeaderboard_t hSteamLeaderboard = pCallback.m_hSteamLeaderboard;
		Debug.Log("[Steam] 找到排行榜，准备上传分数...");
		SteamAPICall_t hAPICall = SteamUserStats.UploadLeaderboardScore(hSteamLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, this.m_pendingScore, null, 0);
		this.m_ScoreUploadedResult.Set(hAPICall, null);
	}

	// Token: 0x06001733 RID: 5939 RVA: 0x00090BA8 File Offset: 0x0008EDA8
	private void OnScoreUploaded(LeaderboardScoreUploaded_t pCallback, bool bIOFailure)
	{
		if (pCallback.m_bSuccess == 0 || bIOFailure)
		{
			Debug.LogError("[Steam] 分数上传失败");
			return;
		}
		Debug.Log(string.Format("[Steam] 分数上传成功! 当前排名: {0}", pCallback.m_nGlobalRankNew));
		Util.ShowTips("上传成功");
		SteamAPICall_t hAPICall = SteamUserStats.AttachLeaderboardUGC(pCallback.m_hSteamLeaderboard, this.m_currentUGCHandle);
		this.m_LeaderboardUGCSetResult.Set(hAPICall, null);
	}

	// Token: 0x06001734 RID: 5940 RVA: 0x00090C10 File Offset: 0x0008EE10
	private void OnUGCAttached(LeaderboardUGCSet_t pCallback, bool bIOFailure)
	{
		if (pCallback.m_eResult != EResult.k_EResultOK || bIOFailure)
		{
			Debug.LogError("[Steam] 挂载 Build 数据失败");
			return;
		}
		Debug.Log("[Steam] 流程全部完成！分数与构筑已同步到排行榜。");
	}

	// Token: 0x06001735 RID: 5941 RVA: 0x00090C37 File Offset: 0x0008EE37
	public static string GetLEADERBOARDName(int playerNum)
	{
		if (playerNum == 1)
		{
			return "Rank_Solo";
		}
		if (playerNum == 2)
		{
			return "Rank_Duo";
		}
		if (playerNum == 3)
		{
			return "Rank_Trio";
		}
		if (playerNum == 4)
		{
			return "Rank_Squad";
		}
		return "Rank_Solo";
	}

	// Token: 0x040015C0 RID: 5568
	private const string LEADERBOARD_SOLO = "Rank_Solo";

	// Token: 0x040015C1 RID: 5569
	private const string LEADERBOARD_DUO = "Rank_Duo";

	// Token: 0x040015C2 RID: 5570
	private const string LEADERBOARD_TRIO = "Rank_Trio";

	// Token: 0x040015C3 RID: 5571
	private const string LEADERBOARD_SQUAD = "Rank_Squad";

	// Token: 0x040015C4 RID: 5572
	private UGCHandle_t m_currentUGCHandle;

	// Token: 0x040015C5 RID: 5573
	private CallResult<RemoteStorageFileShareResult_t> m_FileShareResult;

	// Token: 0x040015C6 RID: 5574
	private CallResult<LeaderboardFindResult_t> m_LeaderboardFindResult;

	// Token: 0x040015C7 RID: 5575
	private CallResult<LeaderboardScoreUploaded_t> m_ScoreUploadedResult;

	// Token: 0x040015C8 RID: 5576
	private CallResult<LeaderboardUGCSet_t> m_LeaderboardUGCSetResult;

	// Token: 0x040015C9 RID: 5577
	private int m_pendingScore;

	// Token: 0x040015CA RID: 5578
	private int m_currentPlayerCount;
}
