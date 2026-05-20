using System;

// Token: 0x020001E8 RID: 488
public class 猎物标记破 : PasssiveSkill
{
	// Token: 0x060008D2 RID: 2258 RVA: 0x0003186A File Offset: 0x0002FA6A
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x00031893 File Offset: 0x0002FA93
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x000318BC File Offset: 0x0002FABC
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		this.count++;
		if (this.count >= 3)
		{
			this.count = 0;
			long num = (long)ConstDefine.ClampIntValue((double)((float)attackrole.FinalAttackPower * this.skillValues[0]));
			num = Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)num, false);
			bool isAttackWeek = GameHelperClient.localPlayer.GetIsAttackWeek(AttackType.AttackEffect);
			Util.OnLocalPlayerHit(attackrole, hurtrole, (double)num, Util.GetV2Angle(hurtrole.MyTransform.position, attackrole.MyTransform.position), AttackType.AttackEffect, isAttackWeek);
		}
		return damage;
	}

	// Token: 0x04000B9F RID: 2975
	public int count;
}
