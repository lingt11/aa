using System;

// Token: 0x020001F8 RID: 504
public class RelicAddSummon : RelicBase
{
	// Token: 0x06000910 RID: 2320 RVA: 0x000321A0 File Offset: 0x000303A0
	public override void Enter()
	{
		this.playerBase.addCallMonsterAttack += base.GetValue(0, 0.35f);
		this.playerBase.addCallMonsterHp += base.GetValue(0, 0.35f);
		this.playerBase.addCallMonsterSize += base.GetValue(0, 0.35f);
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00032208 File Offset: 0x00030408
	public override void Exit()
	{
		this.playerBase.addCallMonsterAttack -= base.GetValue(0, 0.35f);
		this.playerBase.addCallMonsterHp -= base.GetValue(0, 0.35f);
		this.playerBase.addCallMonsterSize -= base.GetValue(0, 0.35f);
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x00032270 File Offset: 0x00030470
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		float levelValueDelta = base.GetLevelValueDelta(0, 0.35f, oldLevel, newLevel);
		this.playerBase.addCallMonsterAttack += levelValueDelta;
		this.playerBase.addCallMonsterHp += levelValueDelta;
		this.playerBase.addCallMonsterSize += levelValueDelta;
	}
}
