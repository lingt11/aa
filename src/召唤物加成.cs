using System;

// Token: 0x020001D8 RID: 472
public class 召唤物加成 : PasssiveSkill
{
	// Token: 0x06000899 RID: 2201 RVA: 0x00030BC0 File Offset: 0x0002EDC0
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		if (roleBase != null)
		{
			roleBase.addCallMonsterSize += this.skillValues[0] * 0.01f;
			roleBase.addCallMonsterHp += this.skillValues[0] * 0.01f;
			roleBase.addCallMonsterAttack += this.skillValues[1] * 0.01f;
		}
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x00030C28 File Offset: 0x0002EE28
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		if (roleBase != null)
		{
			roleBase.addCallMonsterSize -= this.skillValues[0] * 0.01f;
			roleBase.addCallMonsterHp -= this.skillValues[0] * 0.01f;
			roleBase.addCallMonsterAttack -= this.skillValues[1] * 0.01f;
		}
	}
}
