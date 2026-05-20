using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class DarkMonsterActiveSkill : ActiveSkillBase
{
	// Token: 0x060010E5 RID: 4325 RVA: 0x0005F4B4 File Offset: 0x0005D6B4
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, ActiveSkillData activeSkillDataValue, float range)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = activeSkillDataValue;
		this.attackRoleBase = attackRole;
		this.attackRange = range;
		this.skillTime = 7.2f;
		float syncEulerY = attackRole.SyncEulerY;
		Vector3 a = Quaternion.Euler(new Vector3(0f, syncEulerY, 0f)) * Vector3.forward;
		this.checkPos = pos;
		this.checkPos.y = this.attackRange / 10f;
		this.effectPos = pos - a * this.attackRange * 0.8f;
		Game.EffectManager.PlayEffect(EffectDefine.DarkMonster, 7.2f, this.effectPos, new Vector3(1f, 1f, 1f), new Vector3(0f, syncEulerY, 0f));
		Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 7.2f, this.effectPos - Vector3.up * 1.5f, 3f);
		this.checkTimeList = new float[]
		{
			5.6f,
			5.072f,
			4.577f,
			3.972f,
			3.477f,
			2.872f,
			2.377f,
			1.1335f
		};
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x0005F5DC File Offset: 0x0005D7DC
	protected override void UpdateSkill(float time)
	{
		int num = this.checkTimeList.Length;
		if (this.checkNum < num && this.skillTime < this.checkTimeList[this.checkNum])
		{
			this.checkNum++;
			if (this.checkNum == num)
			{
				this.attackRange *= 1.5f;
				this.checkPos.y = this.attackRange / 10f;
			}
			Game.EffectManager.PlayEffect(EffectDefine.SmokeEffect, 3f, this.checkPos, this.attackRange / 2f);
			if (this.attackRoleBase.hasAuthority)
			{
				bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
				bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
				List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
				int count = attackRoles.Count;
				long num2 = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
				if (this.checkNum == num)
				{
					num2 = (long)Mathf.RoundToInt((float)num2 * 1.5f);
				}
				PlayerBase playerBase = this.attackRoleBase as PlayerBase;
				if (playerBase != null)
				{
					num2 = (long)((int)((float)num2 * (1f + playerBase.addCallMonsterAttack)));
				}
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.checkPos, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
					{
						if (flag)
						{
							roleBase.OnHit(this.attackRoleBase, (double)num2, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
						}
						else
						{
							Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)num2, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
						}
					}
				}
			}
		}
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x0005F7E1 File Offset: 0x0005D9E1
	public override void Clear(int clearData)
	{
		Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 7.2f, this.effectPos - Vector3.up * 1.5f, 3f);
		base.Clear(clearData);
	}

	// Token: 0x04000ED6 RID: 3798
	private float attackRange;

	// Token: 0x04000ED7 RID: 3799
	private Vector3 checkPos;

	// Token: 0x04000ED8 RID: 3800
	private int checkNum;

	// Token: 0x04000ED9 RID: 3801
	private float[] checkTimeList;

	// Token: 0x04000EDA RID: 3802
	private Vector3 effectPos;
}
