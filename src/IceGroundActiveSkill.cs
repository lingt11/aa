using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E2 RID: 738
public class IceGroundActiveSkill : CoAoeActiveSkill
{
	// Token: 0x06001118 RID: 4376 RVA: 0x00062054 File Offset: 0x00060254
	public override void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, string effectName, float interval, float duration, float effectScale)
	{
		base.InitSkill(activeSkillType, attackRole, pos, rangeValue, effectName, interval, duration, effectScale);
		this.checkTimer -= 0.15f;
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x00062088 File Offset: 0x00060288
	protected override void UpdateLocalSkill(float time)
	{
		if (this.skillTime < this.checkTimer - (float)this.checkNum * this.checkOffset)
		{
			this.checkNum++;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.checkPos, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
				{
					this.AddFrostBuff(roleBase);
				}
			}
		}
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x0006213A File Offset: 0x0006033A
	private void AddFrostBuff(RoleBase roleBase)
	{
		GameHelperClient.localPlayer.CmdAddBuff(roleBase.netId, this.attackRoleBase.netId, LocalBuffType.Frost, 0.35f, 5f, 1);
	}
}
