using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E9 RID: 745
public class MoveAoeActiveSkill : ActiveSkillBase
{
	// Token: 0x06001139 RID: 4409 RVA: 0x0006378C File Offset: 0x0006198C
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, string effectName, Quaternion attackRotaion)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = 1f;
		this.moveEffect = AssetManager.LoadPrefab(effectName, null, true).transform;
		this.moveEffect.position = pos + new Vector3(0f, 1.25f, 0f);
		this.moveEffect.rotation = attackRotaion;
		this.moveEffect.localScale = rangeValue / 2f * Vector3.one;
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x00063834 File Offset: 0x00061A34
	protected override void UpdateSkill(float time)
	{
		this.moveTimer += time;
		this.moveEffect.position += this.moveEffect.forward * (this.moveTimer * time * 25f);
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x00063884 File Offset: 0x00061A84
	protected override void UpdateLocalSkill(float deltaTime)
	{
		bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
		List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
		int count = attackRoles.Count;
		bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && !this.hitRoles.Contains(roleBase) && Util.NewCheckShanXing(this.moveEffect.position, roleBase.MyTransform.position, 180f, this.attackRange + roleBase.RoleModeBase.addRange, this.moveEffect.eulerAngles.y, false))
			{
				this.hitRoles.Add(roleBase);
				long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
				if (flag)
				{
					roleBase.OnHit(this.attackRoleBase, (double)skillDamage, this.moveEffect.eulerAngles.y, AttackType.Skill, isAttackWeek);
				}
				else
				{
					Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, this.moveEffect.eulerAngles.y, AttackType.Skill, isAttackWeek);
				}
			}
		}
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x000639CE File Offset: 0x00061BCE
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.moveEffect != null)
		{
			AssetManager.UnLoadPrefab(this.moveEffect.gameObject, false);
			this.moveEffect = null;
		}
		this.hitRoles = null;
	}

	// Token: 0x04000F47 RID: 3911
	private float attackRange;

	// Token: 0x04000F48 RID: 3912
	private float checkTimer;

	// Token: 0x04000F49 RID: 3913
	private Transform moveEffect;

	// Token: 0x04000F4A RID: 3914
	private List<RoleBase> hitRoles = new List<RoleBase>();

	// Token: 0x04000F4B RID: 3915
	private float moveTimer;
}
