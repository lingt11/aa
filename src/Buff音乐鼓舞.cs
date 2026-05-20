using System;

// Token: 0x02000185 RID: 389
public class Buff音乐鼓舞 : RoleBuff
{
	// Token: 0x0600076C RID: 1900 RVA: 0x0002C334 File Offset: 0x0002A534
	public override void OnInit()
	{
		this.info = "受到了客服小祥的音乐鼓舞,攻击速度提高了";
		this.icon = "Skill/演奏音乐";
		PlayerBase playerBase = this.roleBase as PlayerBase;
		if (playerBase != null)
		{
			playerBase.StaAllAdd += 0.35f;
			playerBase.StrAllAdd += 0.35f;
			playerBase.AgiAllAdd += 0.35f;
		}
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x0002C39C File Offset: 0x0002A59C
	public override void OnExit()
	{
		PlayerBase playerBase = this.roleBase as PlayerBase;
		if (playerBase != null)
		{
			playerBase.StaAllAdd -= 0.35f;
			playerBase.StrAllAdd -= 0.35f;
			playerBase.AgiAllAdd -= 0.35f;
		}
	}
}
