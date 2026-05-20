using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000354 RID: 852
public class UIRankHeroItem : MonoBehaviour
{
	// Token: 0x170000CE RID: 206
	// (get) Token: 0x0600138B RID: 5003 RVA: 0x00078E64 File Offset: 0x00077064
	public SaveLoadManager.PlayerKingData PlayerKingData
	{
		get
		{
			return this.playerKingData;
		}
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x00078E6C File Offset: 0x0007706C
	public void SetHeroHead(SaveLoadManager.PlayerKingData playerKingDataValue)
	{
		this.playerKingData = playerKingDataValue;
		this.heroHead.sprite = Util.GetHeroIcon(playerKingDataValue.heroType);
		this.nameText.text = Util.GetHeroName(playerKingDataValue.heroType);
	}

	// Token: 0x0400121F RID: 4639
	public Image heroHead;

	// Token: 0x04001220 RID: 4640
	public Text nameText;

	// Token: 0x04001221 RID: 4641
	private SaveLoadManager.PlayerKingData playerKingData;
}
