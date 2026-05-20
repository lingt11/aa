using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E3 RID: 739
public class IceWallActiveSkill : ActiveSkillBase
{
	// Token: 0x0600111C RID: 4380 RVA: 0x00062164 File Offset: 0x00060364
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.checkTimer = 2.85f;
		this.skillTime = 3f;
		this.checkPos = pos;
		this.checkBufftimer = this.checkTimer - 0.05f;
		this.attackSyncEulerY = attackRole.SyncEulerY;
		Game.EffectManager.PlayEffect(EffectDefine.IceWall, 3f, pos, new Vector3(rangeValue / 1.5f, rangeValue / 2.5f, rangeValue / 1.5f), new Vector3(0f, this.attackSyncEulerY, 0f));
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x00062220 File Offset: 0x00060420
	protected override void UpdateLocalSkill(float time)
	{
		if (!this.isCheck && this.skillTime < this.checkTimer)
		{
			this.isCheck = true;
			bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckJuXing(this.checkPos, this.attackSyncEulerY, this.attackRange * 2.25f, this.attackRange * 1.15f, roleBase.MyTransform.position, roleBase.RoleModeBase.addRange, true, false))
				{
					long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
					if (flag)
					{
						roleBase.OnHit(this.attackRoleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
					else
					{
						Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
				}
			}
		}
		if (this.skillTime < this.checkBufftimer)
		{
			this.checkBufftimer -= 0.15f;
			List<RoleBase> attackRoles2 = this.attackRoleBase.GetAttackRoles();
			int count2 = attackRoles2.Count;
			for (int j = 0; j < count2; j++)
			{
				RoleBase roleBase2 = attackRoles2[j];
				if (roleBase2 != null && roleBase2.gameObject.activeSelf && !roleBase2.IsDead() && Util.NewCheckJuXing(this.checkPos, this.attackSyncEulerY, this.attackRange * 2.25f, this.attackRange * 1.15f, roleBase2.MyTransform.position, roleBase2.RoleModeBase.addRange, true, false))
				{
					this.AddIceBuff(roleBase2);
				}
			}
		}
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x00062440 File Offset: 0x00060640
	private void AddIceBuff(RoleBase roleBase)
	{
		if (roleBase.localRoleBuffDic.ContainsKey(LocalBuffType.Frost))
		{
			return;
		}
		GameHelperClient.localPlayer.CmdAddBuff(roleBase.netId, this.attackRoleBase.netId, LocalBuffType.Frost, 0.35f, 3f, 1);
	}

	// Token: 0x04000F20 RID: 3872
	private float checkTimer;

	// Token: 0x04000F21 RID: 3873
	private bool isCheck;

	// Token: 0x04000F22 RID: 3874
	private Vector3 checkPos;

	// Token: 0x04000F23 RID: 3875
	private float attackRange;

	// Token: 0x04000F24 RID: 3876
	private float attackSyncEulerY;

	// Token: 0x04000F25 RID: 3877
	private float checkBufftimer;
}
