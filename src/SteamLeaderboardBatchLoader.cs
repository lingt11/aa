using System;
using System.Collections.Generic;

// Token: 0x020003E6 RID: 998
public class SteamLeaderboardBatchLoader
{
	// Token: 0x060016FE RID: 5886 RVA: 0x0008F850 File Offset: 0x0008DA50
	public void GetTopPlayersData(string leaderboardName, int count, Action<List<SaveLoadManager.TeamBuildData>> onComplete)
	{
		BatchRequest request = new BatchRequest();
		this.m_ActiveRequests.Add(request);
		request.StartBatch(leaderboardName, count, delegate(List<SaveLoadManager.TeamBuildData> result)
		{
			this.m_ActiveRequests.Remove(request);
			Action<List<SaveLoadManager.TeamBuildData>> onComplete2 = onComplete;
			if (onComplete2 == null)
			{
				return;
			}
			onComplete2(result);
		});
	}

	// Token: 0x060016FF RID: 5887 RVA: 0x0008F8A8 File Offset: 0x0008DAA8
	public void GetPlayersDataByRankRange(string leaderboardName, int startRank, int endRank, Action<List<SaveLoadManager.TeamBuildData>> onComplete)
	{
		BatchRequest request = new BatchRequest();
		this.m_ActiveRequests.Add(request);
		request.StartRankRange(leaderboardName, startRank, endRank, delegate(List<SaveLoadManager.TeamBuildData> result)
		{
			this.m_ActiveRequests.Remove(request);
			Action<List<SaveLoadManager.TeamBuildData>> onComplete2 = onComplete;
			if (onComplete2 == null)
			{
				return;
			}
			onComplete2(result);
		});
	}

	// Token: 0x06001700 RID: 5888 RVA: 0x0008F900 File Offset: 0x0008DB00
	public void GetChallengePlayersData(string leaderboardName, Action<List<SaveLoadManager.TeamBuildData>> onComplete)
	{
		BatchRequest batchRequest = new BatchRequest();
		this.m_ActiveRequests.Add(batchRequest);
		batchRequest.StartChallengeCandidates(leaderboardName, onComplete);
	}

	// Token: 0x04001590 RID: 5520
	private readonly List<BatchRequest> m_ActiveRequests = new List<BatchRequest>();
}
