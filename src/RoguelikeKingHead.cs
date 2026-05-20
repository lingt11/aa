using System;
using UnityEngine;

// Token: 0x02000380 RID: 896
public class RoguelikeKingHead : MonoBehaviour
{
	// Token: 0x0600146F RID: 5231 RVA: 0x0007F1BC File Offset: 0x0007D3BC
	public void SetTeamBuildData(SaveLoadManager.TeamBuildData teamBuildData)
	{
		base.gameObject.SetActive(true);
		int num = this.kingHeadItems.Length;
		int count = teamBuildData.members.Count;
		for (int i = 0; i < count; i++)
		{
			KingHeadItem kingHeadItem = this.kingHeadItems[i];
			kingHeadItem.gameObject.SetActive(true);
			kingHeadItem.SetKingData(teamBuildData.members[i]);
		}
		if (count < num)
		{
			for (int j = count; j < num; j++)
			{
				this.kingHeadItems[j].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x0006898B File Offset: 0x00066B8B
	public void Hide()
	{
		if (base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0400131B RID: 4891
	public KingHeadItem[] kingHeadItems;
}
