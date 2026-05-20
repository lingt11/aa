using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E6 RID: 742
public class KingsTreasureActiveSkill : ActiveSkillBase
{
	// Token: 0x0600112A RID: 4394 RVA: 0x00062D98 File Offset: 0x00060F98
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = duration;
		this.checkTimer = duration - 0.45f;
		this.checkOffset = interval;
		this.effectTransform = AssetManager.LoadPrefab(EffectDefine.KingsTreasure, null, true).transform;
		this.effectTransform.position = pos + new Vector3(0f, 0f, 0f);
		this.effectTransform.eulerAngles = new Vector3(-90f, attackRole.MyTransform.eulerAngles.y, 0f);
		this.effectTransform.localScale = new Vector3(rangeValue / 4f, rangeValue / 6f, rangeValue / 4f);
		this.checkPos = this.effectTransform.position - rangeValue * 1.75f * attackRole.MyTransform.forward;
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x00062EAC File Offset: 0x000610AC
	protected override void UpdateLocalSkill(float deltaTime)
	{
		if (this.skillTime < this.checkTimer - (float)this.checkNum * this.checkOffset)
		{
			this.checkNum++;
			bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
			bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckJuXing(this.checkPos, this.effectTransform.eulerAngles.y, this.attackRange * 2.25f, this.attackRange * 10f, roleBase.MyTransform.position, roleBase.RoleModeBase.addRange, false, false))
				{
					if (flag)
					{
						roleBase.OnHit(this.attackRoleBase, (double)skillDamage, this.effectTransform.eulerAngles.y, AttackType.Skill, isAttackWeek);
					}
					else
					{
						Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, this.effectTransform.eulerAngles.y, AttackType.Skill, isAttackWeek);
					}
				}
			}
		}
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x0006300D File Offset: 0x0006120D
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x04000F33 RID: 3891
	private float attackRange;

	// Token: 0x04000F34 RID: 3892
	private float checkTimer;

	// Token: 0x04000F35 RID: 3893
	private int checkNum;

	// Token: 0x04000F36 RID: 3894
	private float checkOffset;

	// Token: 0x04000F37 RID: 3895
	private Transform effectTransform;

	// Token: 0x04000F38 RID: 3896
	private Vector3 checkPos;
}
