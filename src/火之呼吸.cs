using System;
using UnityEngine;

// Token: 0x020001E5 RID: 485
public class 火之呼吸 : PasssiveSkill
{
	// Token: 0x060008C7 RID: 2247 RVA: 0x0003163B File Offset: 0x0002F83B
	public override void Enter()
	{
		this.rate = this.skillValues[0];
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x00031672 File Offset: 0x0002F872
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0003169C File Offset: 0x0002F89C
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (hurtrole.IsDead() || hurtrole.localRoleBuffDic.ContainsKey(LocalBuffType.Fire) || this.roleBase.wudi)
		{
			return damage;
		}
		long num = (long)ConstDefine.ClampIntValue((double)attackrole.FinalAttackPower);
		num = Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)num, true);
		GameHelperClient.localPlayer.CmdAddBuff(hurtrole.netId, attackrole.netId, LocalBuffType.Fire, (float)num * this.rate, (float)Mathf.RoundToInt(this.skillValues[1]), 1);
		return damage;
	}

	// Token: 0x04000B9C RID: 2972
	private float rate;
}
