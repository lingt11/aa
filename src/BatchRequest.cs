using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

// Token: 0x020003E9 RID: 1001
public class BatchRequest
{
	// Token: 0x06001706 RID: 5894 RVA: 0x0008F990 File Offset: 0x0008DB90
	public BatchRequest()
	{
		this.m_LeaderboardFindResult = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.OnLeaderboardFound));
		this.m_ScoresDownloadedResult = CallResult<LeaderboardScoresDownloaded_t>.Create(new CallResult<LeaderboardScoresDownloaded_t>.APIDispatchDelegate(this.OnScoresDownloaded));
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x0008FA08 File Offset: 0x0008DC08
	public void StartBatch(string leaderboardName, int count, Action<List<SaveLoadManager.TeamBuildData>> callback)
	{
		SteamManager steamManager = EntityStatic.Get<SteamManager>();
		if (steamManager == null || !steamManager.Initialized)
		{
			Debug.LogError("[Batch] Steam is not initialized.");
			if (callback != null)
			{
				callback(null);
			}
			return;
		}
		this.m_LeaderboardName = leaderboardName;
		this.m_FinalCallback = callback;
		this.m_TargetCount = Mathf.Max(1, count);
		this.m_PageSize = Mathf.Clamp(this.m_TargetCount, 1, 100);
		this.m_NextRangeStart = 1;
		this.m_RangeEnd = int.MaxValue;
		this.m_IsRankRangeRequest = false;
		this.m_IsSelectedRangeRequest = false;
		this.m_OnlyCompleteBuildData = false;
		Debug.Log(string.Format("[Batch] Start leaderboard load: {0}, target count: {1}", this.m_LeaderboardName, this.m_TargetCount));
		SteamAPICall_t hAPICall = SteamUserStats.FindLeaderboard(this.m_LeaderboardName);
		this.m_LeaderboardFindResult.Set(hAPICall, null);
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x0008FACC File Offset: 0x0008DCCC
	public void StartRankRange(string leaderboardName, int startRank, int endRank, Action<List<SaveLoadManager.TeamBuildData>> callback)
	{
		SteamManager steamManager = EntityStatic.Get<SteamManager>();
		if (steamManager == null || !steamManager.Initialized)
		{
			Debug.LogError("[Batch] Steam is not initialized.");
			if (callback != null)
			{
				callback(null);
			}
			return;
		}
		startRank = Mathf.Max(1, startRank);
		endRank = Mathf.Max(startRank, endRank);
		this.m_LeaderboardName = leaderboardName;
		this.m_FinalCallback = callback;
		this.m_TargetCount = endRank - startRank + 1;
		this.m_PageSize = Mathf.Clamp(this.m_TargetCount, 1, 100);
		this.m_NextRangeStart = startRank;
		this.m_RangeEnd = endRank;
		this.m_IsRankRangeRequest = true;
		this.m_IsSelectedRangeRequest = false;
		this.m_OnlyCompleteBuildData = false;
		Debug.Log(string.Format("[Batch] Start leaderboard range load: {0}, range: {1}-{2}", this.m_LeaderboardName, startRank, endRank));
		SteamAPICall_t hAPICall = SteamUserStats.FindLeaderboard(this.m_LeaderboardName);
		this.m_LeaderboardFindResult.Set(hAPICall, null);
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x0008FBA0 File Offset: 0x0008DDA0
	public void StartChallengeCandidates(string leaderboardName, Action<List<SaveLoadManager.TeamBuildData>> callback)
	{
		SteamManager steamManager = EntityStatic.Get<SteamManager>();
		if (steamManager == null || !steamManager.Initialized)
		{
			Debug.LogError("[Batch] Steam is not initialized.");
			if (callback != null)
			{
				callback(null);
			}
			return;
		}
		this.m_LeaderboardName = leaderboardName;
		this.m_FinalCallback = callback;
		this.m_TargetCount = 20;
		this.m_IsRankRangeRequest = false;
		this.m_IsSelectedRangeRequest = true;
		this.m_OnlyCompleteBuildData = false;
		this.EnqueueChallengeRanges();
		Debug.Log("[Batch] Start challenge candidate load: " + this.m_LeaderboardName);
		SteamAPICall_t hAPICall = SteamUserStats.FindLeaderboard(this.m_LeaderboardName);
		this.m_LeaderboardFindResult.Set(hAPICall, null);
	}

	// Token: 0x0600170A RID: 5898 RVA: 0x0008FC34 File Offset: 0x0008DE34
	private void OnLeaderboardFound(LeaderboardFindResult_t pCallback, bool bIOFailure)
	{
		if (pCallback.m_bLeaderboardFound == 0 || bIOFailure)
		{
			Debug.LogError(string.Format("[Batch] Leaderboard not found: {0}, ioFailure: {1}", this.m_LeaderboardName, bIOFailure));
			this.Finish();
			return;
		}
		this.m_Leaderboard = pCallback.m_hSteamLeaderboard;
		this.RequestNextLeaderboardPage();
	}

	// Token: 0x0600170B RID: 5899 RVA: 0x0008FC84 File Offset: 0x0008DE84
	private void RequestNextLeaderboardPage()
	{
		if (this.m_Finished)
		{
			return;
		}
		int num;
		int num2;
		if (this.m_IsSelectedRangeRequest)
		{
			if (this.m_SelectedRankRanges.Count == 0)
			{
				this.m_NoMoreLeaderboardEntries = true;
				this.StartNextDownloads();
				return;
			}
			BatchRequest.RankRange rankRange = this.m_SelectedRankRanges.Dequeue();
			num = rankRange.start;
			num2 = rankRange.end;
		}
		else
		{
			num = this.m_NextRangeStart;
			num2 = num + this.m_PageSize - 1;
			if (this.m_IsRankRangeRequest)
			{
				num2 = Mathf.Min(num2, this.m_RangeEnd);
			}
		}
		SteamAPICall_t hAPICall = SteamUserStats.DownloadLeaderboardEntries(this.m_Leaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, num, num2);
		this.m_ScoresDownloadedResult.Set(hAPICall, null);
	}

	// Token: 0x0600170C RID: 5900 RVA: 0x0008FD1C File Offset: 0x0008DF1C
	private void OnScoresDownloaded(LeaderboardScoresDownloaded_t pCallback, bool bIOFailure)
	{
		if (bIOFailure)
		{
			Debug.LogWarning("[Batch] Download leaderboard entries failed: " + this.m_LeaderboardName);
			this.Finish();
			return;
		}
		int cEntryCount = pCallback.m_cEntryCount;
		if (cEntryCount == 0)
		{
			this.m_NoMoreLeaderboardEntries = true;
			this.StartNextDownloads();
			return;
		}
		this.m_TotalLeaderboardEntries += cEntryCount;
		if (this.m_IsSelectedRangeRequest)
		{
			if (this.m_SelectedRankRanges.Count == 0)
			{
				this.m_NoMoreLeaderboardEntries = true;
			}
		}
		else
		{
			this.m_NextRangeStart += cEntryCount;
			if (cEntryCount < this.m_PageSize || (this.m_IsRankRangeRequest && this.m_NextRangeStart > this.m_RangeEnd))
			{
				this.m_NoMoreLeaderboardEntries = true;
			}
		}
		Debug.Log(string.Format("[Batch] Leaderboard page loaded: {0}, entries: {1}, scanned: {2}, checking UGC...", this.m_LeaderboardName, cEntryCount, this.m_TotalLeaderboardEntries));
		for (int i = 0; i < cEntryCount; i++)
		{
			LeaderboardEntry_t leaderboardEntry_t;
			if (!SteamUserStats.GetDownloadedLeaderboardEntry(pCallback.m_hSteamLeaderboardEntries, i, out leaderboardEntry_t, new int[0], 0))
			{
				this.m_InvalidUGCEntries++;
			}
			else
			{
				SaveLoadManager.TeamBuildData teamBuildData = BatchRequest.CreateScoreOnlyData(leaderboardEntry_t);
				if (leaderboardEntry_t.m_hUGC == UGCHandle_t.Invalid)
				{
					this.m_InvalidUGCEntries++;
					if (!this.m_OnlyCompleteBuildData)
					{
						this.m_ResultList.Add(teamBuildData);
					}
				}
				else
				{
					this.m_TotalUGCEntries++;
					this.m_PlaceholderByUGCHandle[leaderboardEntry_t.m_hUGC] = teamBuildData;
					this.m_PendingUGCHandles.Enqueue(leaderboardEntry_t.m_hUGC);
				}
			}
		}
		int totalUGCEntries = this.m_TotalUGCEntries;
		this.StartNextDownloads();
	}

	// Token: 0x0600170D RID: 5901 RVA: 0x0008FE9C File Offset: 0x0008E09C
	private void StartNextDownloads()
	{
		while (this.m_ActiveDownloads < 8 && this.m_PendingUGCHandles.Count > 0)
		{
			UGCHandle_t handle = this.m_PendingUGCHandles.Dequeue();
			UGCWorkItem ugcworkItem = new UGCWorkItem();
			this.m_Workers.Add(ugcworkItem);
			this.m_ActiveDownloads++;
			ugcworkItem.StartDownload(handle, new Action<UGCWorkItem, SaveLoadManager.TeamBuildData>(this.OnSingleDownloadComplete));
		}
		if (this.m_ActiveDownloads == 0 && this.m_PendingUGCHandles.Count == 0)
		{
			if (this.m_ResultList.Count >= this.m_TargetCount || this.m_NoMoreLeaderboardEntries)
			{
				this.Finish();
				return;
			}
			this.RequestNextLeaderboardPage();
		}
	}

	// Token: 0x0600170E RID: 5902 RVA: 0x0008FF40 File Offset: 0x0008E140
	private void OnSingleDownloadComplete(UGCWorkItem worker, SaveLoadManager.TeamBuildData data)
	{
		this.m_Workers.Remove(worker);
		this.m_ActiveDownloads = Mathf.Max(0, this.m_ActiveDownloads - 1);
		SaveLoadManager.TeamBuildData teamBuildData = null;
		if (worker != null)
		{
			this.m_PlaceholderByUGCHandle.TryGetValue(worker.Handle, out teamBuildData);
		}
		if (data != null)
		{
			BatchRequest.ApplySteamEntryInfo(data, teamBuildData);
			this.m_ResultList.Add(data);
			if (this.m_IsSelectedRangeRequest)
			{
				BatchRequest.LogChallengeBuildAdded(data);
			}
		}
		else
		{
			BatchRequest.LogUGCLoadFailed(teamBuildData);
			if (!this.m_OnlyCompleteBuildData && teamBuildData != null)
			{
				this.m_ResultList.Add(teamBuildData);
			}
			this.m_FailedDownloads++;
		}
		if (worker != null)
		{
			this.m_PlaceholderByUGCHandle.Remove(worker.Handle);
		}
		this.StartNextDownloads();
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x0008FFF4 File Offset: 0x0008E1F4
	private void Finish()
	{
		if (this.m_Finished)
		{
			return;
		}
		this.m_Finished = true;
		this.m_Workers.Clear();
		this.m_PendingUGCHandles.Clear();
		this.m_PlaceholderByUGCHandle.Clear();
		SteamLeaderboardRankOrder.NormalizeMissingOrders(this.m_ResultList);
		SteamLeaderboardRankOrder.Sort(this.m_ResultList);
		if (this.m_ResultList.Count > this.m_TargetCount)
		{
			this.m_ResultList.RemoveRange(this.m_TargetCount, this.m_ResultList.Count - this.m_TargetCount);
		}
		Debug.Log(string.Format("[Batch] Complete: {0}, scanned entries: {1}, entries with UGC: {2}, success builds: {3}, failed downloads: {4}, invalid/no-UGC entries: {5}", new object[]
		{
			this.m_LeaderboardName,
			this.m_TotalLeaderboardEntries,
			this.m_TotalUGCEntries,
			this.m_ResultList.Count,
			this.m_FailedDownloads,
			this.m_InvalidUGCEntries
		}));
		Action<List<SaveLoadManager.TeamBuildData>> finalCallback = this.m_FinalCallback;
		if (finalCallback != null)
		{
			finalCallback(this.m_ResultList);
		}
		this.m_FinalCallback = null;
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x00090108 File Offset: 0x0008E308
	private static SaveLoadManager.TeamBuildData CreateScoreOnlyData(LeaderboardEntry_t entry)
	{
		return new SaveLoadManager.TeamBuildData
		{
			rank = SteamLeaderboardRankOrder.DecodeLeaderboardRank(entry.m_nScore),
			order = SteamLeaderboardRankOrder.DecodeLeaderboardOrder(entry.m_nScore),
			steamGlobalRank = entry.m_nGlobalRank,
			leaderboardSteamID = entry.m_steamIDUser.m_SteamID,
			isBuildDataIncomplete = true,
			isLegacyOrder = false,
			teamMessage = string.Empty
		};
	}

	// Token: 0x06001711 RID: 5905 RVA: 0x00090172 File Offset: 0x0008E372
	private static void ApplySteamEntryInfo(SaveLoadManager.TeamBuildData data, SaveLoadManager.TeamBuildData placeholder)
	{
		if (data == null || placeholder == null)
		{
			return;
		}
		data.steamGlobalRank = placeholder.steamGlobalRank;
		data.leaderboardSteamID = placeholder.leaderboardSteamID;
		data.rank = placeholder.rank;
	}

	// Token: 0x06001712 RID: 5906 RVA: 0x00002D1D File Offset: 0x00000F1D
	private static void LogChallengeBuildAdded(SaveLoadManager.TeamBuildData data)
	{
	}

	// Token: 0x06001713 RID: 5907 RVA: 0x00002D1D File Offset: 0x00000F1D
	private static void LogUGCLoadFailed(SaveLoadManager.TeamBuildData placeholder)
	{
	}

	// Token: 0x06001714 RID: 5908 RVA: 0x000901A0 File Offset: 0x0008E3A0
	private void EnqueueChallengeRanges()
	{
		this.m_SelectedRankRanges.Clear();
		this.m_SelectedRankRanges.Enqueue(new BatchRequest.RankRange(1, 10));
		List<BatchRequest.RankRange> list = BatchRequest.CreateRandomChallengeRanges();
		for (int i = 0; i < list.Count; i++)
		{
			this.m_SelectedRankRanges.Enqueue(list[i]);
		}
	}

	// Token: 0x06001715 RID: 5909 RVA: 0x000901F4 File Offset: 0x0008E3F4
	private static List<BatchRequest.RankRange> CreateRandomChallengeRanges()
	{
		return new List<BatchRequest.RankRange>
		{
			BatchRequest.CreateRandomRange(11, 50),
			BatchRequest.CreateRandomRange(51, 100)
		};
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x0009021C File Offset: 0x0008E41C
	private static BatchRequest.RankRange CreateRandomRange(int minRank, int maxRank)
	{
		int num = Mathf.Max(minRank, maxRank - 5 + 1);
		int num2 = Random.Range(minRank, num + 1);
		return new BatchRequest.RankRange(num2, num2 + 5 - 1);
	}

	// Token: 0x04001597 RID: 5527
	private const int MaxConcurrentDownloads = 8;

	// Token: 0x04001598 RID: 5528
	private const int ChallengeTopRankEnd = 10;

	// Token: 0x04001599 RID: 5529
	private const int ChallengeMiddleRankStart = 11;

	// Token: 0x0400159A RID: 5530
	private const int ChallengeMiddleRankEnd = 50;

	// Token: 0x0400159B RID: 5531
	private const int ChallengeLowRankStart = 51;

	// Token: 0x0400159C RID: 5532
	private const int ChallengeLowRankEnd = 100;

	// Token: 0x0400159D RID: 5533
	private const int ChallengeRandomSegmentSize = 5;

	// Token: 0x0400159E RID: 5534
	private readonly CallResult<LeaderboardFindResult_t> m_LeaderboardFindResult;

	// Token: 0x0400159F RID: 5535
	private readonly CallResult<LeaderboardScoresDownloaded_t> m_ScoresDownloadedResult;

	// Token: 0x040015A0 RID: 5536
	private readonly List<SaveLoadManager.TeamBuildData> m_ResultList = new List<SaveLoadManager.TeamBuildData>();

	// Token: 0x040015A1 RID: 5537
	private readonly List<UGCWorkItem> m_Workers = new List<UGCWorkItem>();

	// Token: 0x040015A2 RID: 5538
	private readonly Queue<UGCHandle_t> m_PendingUGCHandles = new Queue<UGCHandle_t>();

	// Token: 0x040015A3 RID: 5539
	private readonly Dictionary<UGCHandle_t, SaveLoadManager.TeamBuildData> m_PlaceholderByUGCHandle = new Dictionary<UGCHandle_t, SaveLoadManager.TeamBuildData>();

	// Token: 0x040015A4 RID: 5540
	private readonly Queue<BatchRequest.RankRange> m_SelectedRankRanges = new Queue<BatchRequest.RankRange>();

	// Token: 0x040015A5 RID: 5541
	private Action<List<SaveLoadManager.TeamBuildData>> m_FinalCallback;

	// Token: 0x040015A6 RID: 5542
	private string m_LeaderboardName;

	// Token: 0x040015A7 RID: 5543
	private SteamLeaderboard_t m_Leaderboard;

	// Token: 0x040015A8 RID: 5544
	private int m_TargetCount;

	// Token: 0x040015A9 RID: 5545
	private int m_PageSize;

	// Token: 0x040015AA RID: 5546
	private int m_NextRangeStart;

	// Token: 0x040015AB RID: 5547
	private int m_RangeEnd;

	// Token: 0x040015AC RID: 5548
	private int m_ActiveDownloads;

	// Token: 0x040015AD RID: 5549
	private int m_TotalLeaderboardEntries;

	// Token: 0x040015AE RID: 5550
	private int m_TotalUGCEntries;

	// Token: 0x040015AF RID: 5551
	private int m_InvalidUGCEntries;

	// Token: 0x040015B0 RID: 5552
	private int m_FailedDownloads;

	// Token: 0x040015B1 RID: 5553
	private bool m_NoMoreLeaderboardEntries;

	// Token: 0x040015B2 RID: 5554
	private bool m_Finished;

	// Token: 0x040015B3 RID: 5555
	private bool m_IsRankRangeRequest;

	// Token: 0x040015B4 RID: 5556
	private bool m_IsSelectedRangeRequest;

	// Token: 0x040015B5 RID: 5557
	private bool m_OnlyCompleteBuildData;

	// Token: 0x020003EA RID: 1002
	private struct RankRange
	{
		// Token: 0x06001717 RID: 5911 RVA: 0x00090248 File Offset: 0x0008E448
		public RankRange(int start, int end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x040015B6 RID: 5558
		public int start;

		// Token: 0x040015B7 RID: 5559
		public int end;
	}
}
