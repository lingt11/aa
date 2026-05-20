using System;

// Token: 0x020000B8 RID: 184
public class CardSkillAddEquip : CardSkillBase
{
	// Token: 0x06000365 RID: 869 RVA: 0x00016A48 File Offset: 0x00014C48
	public override void Enter()
	{
		GameHelperClient.MaxEquipNum++;
	}

	// Token: 0x06000366 RID: 870 RVA: 0x00002D1D File Offset: 0x00000F1D
	public override void Exit()
	{
	}
}
