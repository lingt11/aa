using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200037F RID: 895
public class KingHeadItem : MonoBehaviour
{
	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x0600146C RID: 5228 RVA: 0x0007F194 File Offset: 0x0007D394
	public SaveLoadManager.PlayerKingData KingData
	{
		get
		{
			return this.kingData;
		}
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x0007F19C File Offset: 0x0007D39C
	public void SetKingData(SaveLoadManager.PlayerKingData kingDataValue)
	{
		this.kingData = kingDataValue;
		this.image.sprite = Util.GetHeroIcon(kingDataValue.heroType);
	}

	// Token: 0x04001319 RID: 4889
	public Image image;

	// Token: 0x0400131A RID: 4890
	private SaveLoadManager.PlayerKingData kingData;
}
