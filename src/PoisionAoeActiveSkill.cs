using System;
using System.Collections.Generic;

// Token: 0x020002F0 RID: 752
public class PoisionAoeActiveSkill : CoAoeActiveSkill
{
	// Token: 0x0600115D RID: 4445 RVA: 0x00064C24 File Offset: 0x00062E24
	protected override void UpdateLocalSkill(float time)
	{
		if (this.skillTime < this.checkTimer - (float)this.checkNum * this.checkOffset)
		{
			this.checkNum++;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.checkPos, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
				{
					this.AddPoisionBuff(roleBase, (float)skillDamage);
				}
			}
		}
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x00064CF1 File Offset: 0x00062EF1
	private void AddPoisionBuff(RoleBase roleBase, float damage)
	{
		if (roleBase.wudi)
		{
			return;
		}
		GameHelperClient.localPlayer.CmdAddBuff(roleBase.netId, this.attackRoleBase.netId, LocalBuffType.Poison, damage, 5f, 5);
	}
}
