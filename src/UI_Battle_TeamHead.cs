using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000309 RID: 777
public class UI_Battle_TeamHead : MonoBehaviour
{
	// Token: 0x06001208 RID: 4616 RVA: 0x0006AE14 File Offset: 0x00069014
	public void UpdatePlayerData(PlayerBase playerBase)
	{
		if (this.heroType != playerBase.heroType)
		{
			this.heroType = playerBase.heroType;
			this.headImg.sprite = Util.GetHeroIcon(playerBase.heroType);
		}
		string playerDisplayName = GameHelperClient.GetPlayerDisplayName(playerBase);
		if (this.playerName != playerDisplayName)
		{
			this.playerName = playerDisplayName;
			this.nameText.text = playerDisplayName;
		}
		this.killNumberText.text = playerBase.killEnemyNum.ToString();
		this.monsterNumberText.text = playerBase.enemyNum.ToString();
		int num = (int)((float)playerBase.hp * 1f / (float)playerBase.maxHp * 100f);
		this.hpText.text = num.ToString() + "%";
	}

	// Token: 0x04001012 RID: 4114
	public Image headImg;

	// Token: 0x04001013 RID: 4115
	public Text nameText;

	// Token: 0x04001014 RID: 4116
	public Text killNumberText;

	// Token: 0x04001015 RID: 4117
	public Text monsterNumberText;

	// Token: 0x04001016 RID: 4118
	public Text hpText;

	// Token: 0x04001017 RID: 4119
	private HeroType heroType;

	// Token: 0x04001018 RID: 4120
	private string playerName;
}
