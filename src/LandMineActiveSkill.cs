using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public class LandMineActiveSkill : ActiveSkillBase
{
	// Token: 0x0600112E RID: 4398 RVA: 0x0006303C File Offset: 0x0006123C
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, ActiveSkillData activeSkillData, float range, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.attackRoleBase = attackRole;
		this.activeSkillData = activeSkillData;
		this.attackRange = range;
		this.skillTime = duration;
		this.checkTimer = this.skillTime - 1f;
		this.checkPos = pos;
		this.effectGo = AssetManager.LoadPrefab(EffectDefine.LandMineEffect, null, true);
		Transform transform = this.effectGo.transform;
		transform.position = this.checkPos + new Vector3(0f, 0.65f, 0f);
		transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
	}

	// Token: 0x0600112F RID: 4399 RVA: 0x000630E3 File Offset: 0x000612E3
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (GameHelperClient.isReady)
		{
			this.needClear = true;
			return;
		}
		if (this.needClear)
		{
			this.skillTime = -1f;
		}
	}

	// Token: 0x06001130 RID: 4400 RVA: 0x00063110 File Offset: 0x00061310
	protected override void UpdateLocalSkill(float time)
	{
		if (this.skillTime < this.checkTimer && !this.isCheck)
		{
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.checkPos, roleBase.MyTransform.position, 1.5f + roleBase.RoleModeBase.addRange, false))
				{
					this.isCheck = true;
					GameHelperClient.localPlayer.CmdClearSkill(this.skillId);
					return;
				}
			}
		}
	}

	// Token: 0x06001131 RID: 4401 RVA: 0x000631C0 File Offset: 0x000613C0
	public override void Clear(int clearData)
	{
		if (this.effectGo != null)
		{
			AssetManager.UnLoadPrefab(this.effectGo, false);
		}
		if (!this.needClear)
		{
			Game.EffectManager.PlayEffect(EffectDefine.LandMineBoom, 3f, this.checkPos - new Vector3(0f, 1.1f, 0f), this.attackRange / 2f);
			if (this.attackRoleBase != null && this.attackRoleBase.hasAuthority)
			{
				bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
				List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
				int count = attackRoles.Count;
				bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.checkPos, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
					{
						long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
						float num = (float)(roleBase.hp + roleBase.Shield);
						float num2;
						if (flag)
						{
							num2 = (float)roleBase.OnHit(this.attackRoleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
						}
						else
						{
							num2 = (float)Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
						}
						if (num > num2)
						{
							this.AddFireBuff(roleBase, (float)skillDamage);
						}
					}
				}
			}
		}
		base.Clear(clearData);
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x0006339C File Offset: 0x0006159C
	private void AddFireBuff(RoleBase roleBase, float damage)
	{
		if (roleBase.localRoleBuffDic.ContainsKey(LocalBuffType.Fire) || roleBase.wudi)
		{
			return;
		}
		GameHelperClient.localPlayer.CmdAddBuff(roleBase.netId, this.attackRoleBase.netId, LocalBuffType.Fire, damage * 0.2f, 5f, 1);
	}

	// Token: 0x04000F39 RID: 3897
	private Vector3 checkPos;

	// Token: 0x04000F3A RID: 3898
	private float attackRange;

	// Token: 0x04000F3B RID: 3899
	private bool isCheck;

	// Token: 0x04000F3C RID: 3900
	private GameObject effectGo;

	// Token: 0x04000F3D RID: 3901
	private float checkTimer;

	// Token: 0x04000F3E RID: 3902
	private int createWave;

	// Token: 0x04000F3F RID: 3903
	private bool needClear;
}
