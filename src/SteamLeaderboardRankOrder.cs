using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003EC RID: 1004
public static class SteamLeaderboardRankOrder
{
	// Token: 0x0600171D RID: 5917 RVA: 0x000903E0 File Offset: 0x0008E5E0
	public static int EncodeLeaderboardScore(int rank, int order)
	{
		if (rank <= 0)
		{
			return rank;
		}
		int num = Mathf.Min(rank, SteamLeaderboardRankOrder.GetMaxEncodableRank());
		int num2 = Mathf.Clamp(((order > 0) ? order : 10000) / 10, 0, 999999);
		return num * 1000000 + (999999 - num2);
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x00090427 File Offset: 0x0008E627
	public static int DecodeLeaderboardRank(int leaderboardScore)
	{
		if (leaderboardScore < 1000000)
		{
			return leaderboardScore;
		}
		return leaderboardScore / 1000000;
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x0009043C File Offset: 0x0008E63C
	public static int DecodeLeaderboardOrder(int leaderboardScore)
	{
		if (leaderboardScore < 1000000)
		{
			return 10000;
		}
		int num = 999999 - leaderboardScore % 1000000;
		return Mathf.Max(1, num * 10);
	}

	// Token: 0x06001720 RID: 5920 RVA: 0x0009046F File Offset: 0x0008E66F
	private static int GetMaxEncodableRank()
	{
		return 2146;
	}

	// Token: 0x06001721 RID: 5921 RVA: 0x00090478 File Offset: 0x0008E678
	public static void NormalizeMissingOrders(List<SaveLoadManager.TeamBuildData> leaderboard)
	{
		if (leaderboard == null)
		{
			return;
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < leaderboard.Count; i++)
		{
			SaveLoadManager.TeamBuildData teamBuildData = leaderboard[i];
			if (teamBuildData != null)
			{
				int num;
				if (!dictionary.TryGetValue(teamBuildData.rank, out num))
				{
					num = 10000;
				}
				if (teamBuildData.order <= 0)
				{
					teamBuildData.isLegacyOrder = true;
					teamBuildData.order = num;
				}
				dictionary[teamBuildData.rank] = Mathf.Max(num + 10000, teamBuildData.order + 10000);
			}
		}
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x000904FC File Offset: 0x0008E6FC
	public static void Sort(List<SaveLoadManager.TeamBuildData> leaderboard)
	{
		if (leaderboard == null)
		{
			return;
		}
		leaderboard.Sort(new Comparison<SaveLoadManager.TeamBuildData>(SteamLeaderboardRankOrder.Compare));
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x00090514 File Offset: 0x0008E714
	public static int Compare(SaveLoadManager.TeamBuildData a, SaveLoadManager.TeamBuildData b)
	{
		if (a == b)
		{
			return 0;
		}
		if (a == null)
		{
			return 1;
		}
		if (b == null)
		{
			return -1;
		}
		int num = b.rank.CompareTo(a.rank);
		if (num != 0)
		{
			return num;
		}
		int num2 = SteamLeaderboardRankOrder.GetOrder(a).CompareTo(SteamLeaderboardRankOrder.GetOrder(b));
		if (num2 != 0)
		{
			return num2;
		}
		int num3 = SteamLeaderboardRankOrder.GetChallengeTimestamp(a).CompareTo(SteamLeaderboardRankOrder.GetChallengeTimestamp(b));
		if (num3 != 0)
		{
			return num3;
		}
		return SteamLeaderboardRankOrder.GetPrimarySteamId(a).CompareTo(SteamLeaderboardRankOrder.GetPrimarySteamId(b));
	}

	// Token: 0x06001724 RID: 5924 RVA: 0x00090592 File Offset: 0x0008E792
	public static bool HasCompleteBuildData(SaveLoadManager.TeamBuildData data)
	{
		return data != null && !data.isBuildDataIncomplete && data.members != null && data.members.Count > 0;
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x000905B8 File Offset: 0x0008E7B8
	public static void ApplyChallengeWin(List<SaveLoadManager.TeamBuildData> leaderboard, SaveLoadManager.TeamBuildData target, SaveLoadManager.TeamBuildData uploadData)
	{
		if (uploadData == null)
		{
			return;
		}
		if (target == null)
		{
			uploadData.rank++;
			uploadData.order = 10000;
			SteamLeaderboardRankOrder.EnsureChallengeTimestamp(uploadData);
			return;
		}
		if (target.isLegacyOrder || target.order <= 0)
		{
			uploadData.rank = target.rank + 1;
			uploadData.order = 10000;
			SteamLeaderboardRankOrder.EnsureChallengeTimestamp(uploadData);
			return;
		}
		SteamLeaderboardRankOrder.NormalizeMissingOrders(leaderboard);
		SteamLeaderboardRankOrder.Sort(leaderboard);
		if (SteamLeaderboardRankOrder.IsTopOfScoreGroup(leaderboard, target) && SteamLeaderboardRankOrder.HasCompleteSteamRankPrefix(leaderboard, target))
		{
			uploadData.rank = target.rank + 1;
			uploadData.order = SteamLeaderboardRankOrder.GetNextOrderInScoreGroup(leaderboard, uploadData.rank);
			SteamLeaderboardRankOrder.EnsureChallengeTimestamp(uploadData);
			return;
		}
		uploadData.rank = target.rank;
		uploadData.order = SteamLeaderboardRankOrder.GetOrderBeforeTarget(leaderboard, target);
		SteamLeaderboardRankOrder.EnsureChallengeTimestamp(uploadData);
	}

	// Token: 0x06001726 RID: 5926 RVA: 0x00090684 File Offset: 0x0008E884
	private static bool IsTopOfScoreGroup(List<SaveLoadManager.TeamBuildData> leaderboard, SaveLoadManager.TeamBuildData target)
	{
		if (leaderboard == null || leaderboard.Count == 0)
		{
			return true;
		}
		int num = SteamLeaderboardRankOrder.FindTargetIndex(leaderboard, target);
		if (num < 0)
		{
			return true;
		}
		for (int i = 0; i < leaderboard.Count; i++)
		{
			SaveLoadManager.TeamBuildData teamBuildData = leaderboard[i];
			if (teamBuildData != null && teamBuildData.rank == target.rank)
			{
				return i == num;
			}
		}
		return true;
	}

	// Token: 0x06001727 RID: 5927 RVA: 0x000906DC File Offset: 0x0008E8DC
	private static bool HasCompleteSteamRankPrefix(List<SaveLoadManager.TeamBuildData> leaderboard, SaveLoadManager.TeamBuildData target)
	{
		if (target == null || target.steamGlobalRank <= 0)
		{
			return false;
		}
		if (target.steamGlobalRank == 1)
		{
			return true;
		}
		if (leaderboard == null || leaderboard.Count == 0)
		{
			return false;
		}
		HashSet<int> hashSet = new HashSet<int>();
		for (int i = 0; i < leaderboard.Count; i++)
		{
			SaveLoadManager.TeamBuildData teamBuildData = leaderboard[i];
			if (teamBuildData != null && teamBuildData.steamGlobalRank > 0)
			{
				hashSet.Add(teamBuildData.steamGlobalRank);
			}
		}
		for (int j = 1; j <= target.steamGlobalRank; j++)
		{
			if (!hashSet.Contains(j))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001728 RID: 5928 RVA: 0x00090764 File Offset: 0x0008E964
	private static int GetNextOrderInScoreGroup(List<SaveLoadManager.TeamBuildData> leaderboard, int score)
	{
		int num = 0;
		if (leaderboard != null)
		{
			for (int i = 0; i < leaderboard.Count; i++)
			{
				SaveLoadManager.TeamBuildData teamBuildData = leaderboard[i];
				if (teamBuildData != null && teamBuildData.rank == score)
				{
					num = Mathf.Max(num, SteamLeaderboardRankOrder.GetOrder(teamBuildData));
				}
			}
		}
		if (num <= 0)
		{
			return 10000;
		}
		return num + 10000;
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x000907BC File Offset: 0x0008E9BC
	private static int GetOrderBeforeTarget(List<SaveLoadManager.TeamBuildData> leaderboard, SaveLoadManager.TeamBuildData target)
	{
		int num = SteamLeaderboardRankOrder.FindTargetIndex(leaderboard, target);
		if (num < 0)
		{
			return SteamLeaderboardRankOrder.GetNextOrderInScoreGroup(leaderboard, target.rank);
		}
		SaveLoadManager.TeamBuildData teamBuildData = null;
		for (int i = num - 1; i >= 0; i--)
		{
			SaveLoadManager.TeamBuildData teamBuildData2 = leaderboard[i];
			if (teamBuildData2 != null && teamBuildData2.rank == target.rank)
			{
				teamBuildData = teamBuildData2;
				break;
			}
		}
		int order = SteamLeaderboardRankOrder.GetOrder(target);
		if (teamBuildData == null)
		{
			if (order > 10000)
			{
				return 10000 + (order - 10000) / 2;
			}
			return Mathf.Max(1, order - 10000);
		}
		else
		{
			int order2 = SteamLeaderboardRankOrder.GetOrder(teamBuildData);
			int num2 = order - order2;
			if (num2 > 1)
			{
				return order2 + num2 / 2;
			}
			return order;
		}
	}

	// Token: 0x0600172A RID: 5930 RVA: 0x00090864 File Offset: 0x0008EA64
	private static int FindTargetIndex(List<SaveLoadManager.TeamBuildData> leaderboard, SaveLoadManager.TeamBuildData target)
	{
		if (leaderboard == null || target == null)
		{
			return -1;
		}
		for (int i = 0; i < leaderboard.Count; i++)
		{
			if (leaderboard[i] == target)
			{
				return i;
			}
		}
		ulong primarySteamId = SteamLeaderboardRankOrder.GetPrimarySteamId(target);
		for (int j = 0; j < leaderboard.Count; j++)
		{
			SaveLoadManager.TeamBuildData teamBuildData = leaderboard[j];
			if (teamBuildData != null && teamBuildData.rank == target.rank && SteamLeaderboardRankOrder.GetOrder(teamBuildData) == SteamLeaderboardRankOrder.GetOrder(target) && SteamLeaderboardRankOrder.GetPrimarySteamId(teamBuildData) == primarySteamId)
			{
				return j;
			}
		}
		return -1;
	}

	// Token: 0x0600172B RID: 5931 RVA: 0x000908E1 File Offset: 0x0008EAE1
	private static int GetOrder(SaveLoadManager.TeamBuildData data)
	{
		if (data == null || data.order <= 0)
		{
			return 10000;
		}
		return data.order;
	}

	// Token: 0x0600172C RID: 5932 RVA: 0x000908FB File Offset: 0x0008EAFB
	private static long GetChallengeTimestamp(SaveLoadManager.TeamBuildData data)
	{
		if (data == null)
		{
			return 0L;
		}
		return data.challengeTimestamp;
	}

	// Token: 0x0600172D RID: 5933 RVA: 0x0009090C File Offset: 0x0008EB0C
	private static void EnsureChallengeTimestamp(SaveLoadManager.TeamBuildData data)
	{
		if (data != null && data.challengeTimestamp <= 0L)
		{
			data.challengeTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		}
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x00090939 File Offset: 0x0008EB39
	private static ulong GetPrimarySteamId(SaveLoadManager.TeamBuildData data)
	{
		if (data != null && data.members != null && data.members.Count > 0)
		{
			return data.members[0].steamID;
		}
		if (data != null)
		{
			return data.leaderboardSteamID;
		}
		return 0UL;
	}

	// Token: 0x040015BB RID: 5563
	public const int FirstOrder = 10000;

	// Token: 0x040015BC RID: 5564
	public const int OrderStep = 10000;

	// Token: 0x040015BD RID: 5565
	public const int LeaderboardScoreScale = 1000000;

	// Token: 0x040015BE RID: 5566
	public const int LeaderboardOrderPrecision = 10;

	// Token: 0x040015BF RID: 5567
	private const int LeaderboardOrderBucketMax = 999999;
}
