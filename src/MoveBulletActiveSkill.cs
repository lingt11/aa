using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EA RID: 746
public class MoveBulletActiveSkill : ActiveSkillBase
{
	// Token: 0x0600113E RID: 4414 RVA: 0x00063A18 File Offset: 0x00061C18
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, string effectName, string hitEffect, float moveSpeedValue, Quaternion attackRotaion, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = duration;
		this.moveEffect = AssetManager.LoadPrefab(effectName, null, true).transform;
		this.moveEffect.position = pos;
		this.moveEffect.rotation = attackRotaion;
		this.moveEffect.localScale = rangeValue * 2f * Vector3.one;
		this.hitEffectName = hitEffect;
		this.moveSpeed = moveSpeedValue;
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x00063AB4 File Offset: 0x00061CB4
	protected override void UpdateSkill(float time)
	{
		this.moveTimer += time;
		this.moveEffect.position += this.moveEffect.forward * (this.moveTimer * time * this.moveSpeed);
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x00063B04 File Offset: 0x00061D04
	protected override void UpdateLocalSkill(float deltaTime)
	{
		if (this.isCheck)
		{
			return;
		}
		bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
		List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
		int count = attackRoles.Count;
		bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckShanXing(this.moveEffect.position, roleBase.MyTransform.position, 180f, this.attackRange + roleBase.RoleModeBase.addRange, this.moveEffect.eulerAngles.y, false))
			{
				long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
				if (flag)
				{
					roleBase.OnHit(this.attackRoleBase, (double)skillDamage, this.moveEffect.eulerAngles.y, AttackType.Skill, isAttackWeek);
				}
				else
				{
					Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, this.moveEffect.eulerAngles.y, AttackType.Skill, isAttackWeek);
				}
				this.isCheck = true;
				this.PlayHitEffect(roleBase.GetAttackPos());
				GameHelperClient.localPlayer.CmdClearSkill(this.skillId);
				return;
			}
		}
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x00063C60 File Offset: 0x00061E60
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.moveEffect != null)
		{
			if (!this.isCheck)
			{
				this.PlayHitEffect(this.moveEffect.position);
			}
			AssetManager.UnLoadPrefab(this.moveEffect.gameObject, false);
			this.moveEffect = null;
		}
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x00063CB3 File Offset: 0x00061EB3
	private void PlayHitEffect(Vector3 pos)
	{
		Game.EffectManager.PlayEffect(this.hitEffectName, 2f, pos, 1.5f);
	}

	// Token: 0x04000F4C RID: 3916
	private float attackRange;

	// Token: 0x04000F4D RID: 3917
	private float checkTimer;

	// Token: 0x04000F4E RID: 3918
	private Transform moveEffect;

	// Token: 0x04000F4F RID: 3919
	private float moveTimer;

	// Token: 0x04000F50 RID: 3920
	private bool isCheck;

	// Token: 0x04000F51 RID: 3921
	private string hitEffectName;

	// Token: 0x04000F52 RID: 3922
	private float moveSpeed;
}
