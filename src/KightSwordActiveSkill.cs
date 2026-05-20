using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class KightSwordActiveSkill : ActiveSkillBase
{
	// Token: 0x06001125 RID: 4389 RVA: 0x00062AC4 File Offset: 0x00060CC4
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, int syncData, float attackRotation)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = 2f;
		this.moveEffect = AssetManager.LoadPrefab(EffectDefine.CleaveDark, null, true).transform;
		this.moveEffect.localEulerAngles = new Vector3(0f, attackRotation, 0f);
		this.moveEffect.position = pos + new Vector3(0f, 0.35f, 0f);
		this.moveEffect.localScale = rangeValue / 1.5f * Vector3.one;
		this.startPos = pos - this.moveEffect.forward * (rangeValue / 3.5f);
		if (syncData == 0)
		{
			Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/KightSword", pos, 0.85f);
		}
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void UpdateSkill(float time)
	{
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x00062BBC File Offset: 0x00060DBC
	protected override void UpdateLocalSkill(float deltaTime)
	{
		if (this.moveTimer > 0.4f)
		{
			return;
		}
		this.moveTimer += deltaTime;
		this.startPos += this.moveEffect.forward * (deltaTime * 6.5f * this.attackRange);
		bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
		List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
		int count = attackRoles.Count;
		bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && !this.hitRoles.Contains(roleBase) && Util.NewCheckJuXing(this.startPos, this.moveEffect.eulerAngles.y, this.attackRange, this.attackRange, roleBase.MyTransform.position, roleBase.RoleModeBase.addRange, false, false))
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

	// Token: 0x06001128 RID: 4392 RVA: 0x00062D4D File Offset: 0x00060F4D
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

	// Token: 0x04000F2D RID: 3885
	private float attackRange;

	// Token: 0x04000F2E RID: 3886
	private float checkTimer;

	// Token: 0x04000F2F RID: 3887
	private Transform moveEffect;

	// Token: 0x04000F30 RID: 3888
	private List<RoleBase> hitRoles = new List<RoleBase>();

	// Token: 0x04000F31 RID: 3889
	private float moveTimer;

	// Token: 0x04000F32 RID: 3890
	private Vector3 startPos;
}
