using System;

// Token: 0x020000BF RID: 191
public class CardSkillDrawSword : CardSkillBase
{
	// Token: 0x06000375 RID: 885 RVA: 0x00016BC8 File Offset: 0x00014DC8
	public override void Enter()
	{
		CardData cardData;
		if (Game.GameData.CardDataDic.TryGetValue(this.cardId, out cardData))
		{
			string[] array = cardData.unlockData.Split("_", StringSplitOptions.None);
			int num = int.Parse(array[array.Length - 1]);
			if (this.playerBase.heroType == (HeroType)num)
			{
				this.canUse = true;
				this.playerBase.STRAdd += 2;
				this.playerBase.AGIAdd += 2;
				this.playerBase.STAAdd += 2;
			}
		}
		PlayerSwordMasterMode playerSwordMasterMode = this.playerBase.RoleModeBase as PlayerSwordMasterMode;
		if (playerSwordMasterMode != null)
		{
			this.roleMode = playerSwordMasterMode;
			this.roleMode.OpenSecret(Game.Language.Get("card_" + this.cardId.ToString(), ""));
		}
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00016CA4 File Offset: 0x00014EA4
	public override void Exit()
	{
		if (this.canUse)
		{
			this.playerBase.STRAdd -= 2;
			this.playerBase.AGIAdd -= 2;
			this.playerBase.STAAdd -= 2;
		}
		if (this.roleMode != null)
		{
			this.roleMode.CloseSecret();
		}
	}

	// Token: 0x0400037E RID: 894
	private bool canUse;

	// Token: 0x0400037F RID: 895
	private PlayerSwordMasterMode roleMode;
}
