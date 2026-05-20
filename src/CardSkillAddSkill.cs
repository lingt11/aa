using System;

// Token: 0x020000B9 RID: 185
public class CardSkillAddSkill : CardSkillBase
{
	// Token: 0x06000368 RID: 872 RVA: 0x00016A5E File Offset: 0x00014C5E
	public override void Enter()
	{
		GameHelperClient.MaxSkillNum++;
	}

	// Token: 0x06000369 RID: 873 RVA: 0x00002D1D File Offset: 0x00000F1D
	public override void Exit()
	{
	}
}
