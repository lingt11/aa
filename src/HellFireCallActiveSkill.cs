using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E0 RID: 736
public class HellFireCallActiveSkill : ActiveSkillBase
{
	// Token: 0x0600110E RID: 4366 RVA: 0x000619A0 File Offset: 0x0005FBA0
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.checkTimer = 0.5f;
		this.skillTime = 2f;
		this.checkPos = pos;
		this.effectTransform = AssetManager.LoadPrefab(EffectDefine.YunShi, null, true).transform;
		this.startPos = pos + this.attackRoleBase.MyTransform.forward * -5f + new Vector3(0f, 8f, 0f);
		this.effectTransform.position = this.startPos;
		this.effectTransform.localScale = rangeValue / 5f * Vector3.one;
		this.effectTransform.LookAt(this.checkPos);
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x00061A8C File Offset: 0x0005FC8C
	protected override void UpdateSkill(float time)
	{
		if (this.skillTime > 0.5f)
		{
			this.effectTransform.position = Vector3.Lerp(this.startPos, this.checkPos, (2f - this.skillTime) / 1.5f);
		}
		else if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
		if (!this.isCheck && this.skillTime < this.checkTimer)
		{
			this.isCheck = true;
			Game.EffectManager.PlayEffect(EffectDefine.SmokeEffect, 3f, this.checkPos + new Vector3(0f, 0.5f, 0f), this.attackRange / 2f);
			Game.CameraManager.ShakeCameraByPos(this.checkPos, 0.1f, 0.75f, 15, false);
			if (this.attackRoleBase.hasAuthority)
			{
				bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
				List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
				int count = attackRoles.Count;
				bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && this.GetDistanceV2(roleBase.MyTransform.position) < this.attackRange)
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
				this.attackRoleBase.StartSummonByNum(EnemyType.Goblin_HellFlameSummon, this.checkPos, GameHelperClient.localPlayer.netId, 3, 1f, ConstDefine.ClampBattleValue((double)this.attackRoleBase.maxHp * 0.2), ConstDefine.ClampIntValue((double)this.attackRoleBase.FinalAttackPower * 0.2), flag ? 0f : 30f, null, 0L, 0L, -1);
			}
		}
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x00061CFC File Offset: 0x0005FEFC
	public float GetDistanceV2(Vector3 pos)
	{
		Vector3 vector = this.checkPos;
		return Mathf.Sqrt(Mathf.Pow(pos.x - vector.x, 2f) + Mathf.Pow(pos.z - vector.z, 2f));
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x00061D44 File Offset: 0x0005FF44
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x04000F18 RID: 3864
	private float checkTimer;

	// Token: 0x04000F19 RID: 3865
	private bool isCheck;

	// Token: 0x04000F1A RID: 3866
	private Vector3 checkPos;

	// Token: 0x04000F1B RID: 3867
	private float attackRange;

	// Token: 0x04000F1C RID: 3868
	private Transform effectTransform;

	// Token: 0x04000F1D RID: 3869
	private Vector3 startPos;
}
