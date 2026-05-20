using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DF RID: 735
public class FlameshrowerActiveSkill : ActiveSkillBase
{
	// Token: 0x06001109 RID: 4361 RVA: 0x000614E0 File Offset: 0x0005F6E0
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = duration;
		this.checkTimer = duration - 0.1f;
		this.checkOffset = interval;
		this.effectTransform = AssetManager.LoadPrefab(EffectDefine.FlameshrowerSkill, null, true).transform;
		this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1.3f, 0f);
		this.effectTransform.localRotation = Quaternion.identity;
		this.effectTransform.localScale = new Vector3(this.attackRange * 1.5f, this.attackRange, this.attackRange);
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x000615BC File Offset: 0x0005F7BC
	protected override void UpdateLocalSkill(float time)
	{
		if (this.attackRoleBase == null)
		{
			return;
		}
		if (this.attackRoleBase.isLocalPlayer)
		{
			float v2Angle = Util.GetV2Angle(Game.EffectManager.GetMouseGroundPos(), this.attackRoleBase.MyTransform.position);
			this.effectTransform.eulerAngles = new Vector3(0f, v2Angle, 0f);
			this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1.3f, 0f);
			this.syncTimer += time;
			if (this.syncTimer > 0.1f)
			{
				this.attackRoleBase.UpdateSkillData(v2Angle);
				this.syncTimer = 0f;
			}
		}
		else if (this.attackRoleBase.trackRoleBase != null)
		{
			float v2Angle2 = Util.GetV2Angle(this.attackRoleBase.trackRoleBase.MyTransform.position, this.attackRoleBase.MyTransform.position);
			this.effectTransform.eulerAngles = new Vector3(0f, v2Angle2, 0f);
			this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1.3f, 0f);
			this.syncTimer += time;
			if (this.syncTimer > 0.1f)
			{
				this.attackRoleBase.UpdateSkillData(v2Angle2);
				this.syncTimer = 0f;
			}
		}
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
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckJuXing(this.attackRoleBase.MyTransform.position, this.effectTransform.eulerAngles.y, this.attackRange, this.attackRange * 5f, roleBase.MyTransform.position, roleBase.RoleModeBase.addRange, false, false))
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

	// Token: 0x0600110B RID: 4363 RVA: 0x000618B0 File Offset: 0x0005FAB0
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null)
		{
			if (this.attackRoleBase.IsDead())
			{
				this.skillTime = -1f;
			}
			if (!this.attackRoleBase.hasAuthority)
			{
				this.effectTransform.rotation = Quaternion.Lerp(this.effectTransform.rotation, Quaternion.Euler(0f, this.attackRoleBase.SyncSkillData, 0f), 10f * Time.deltaTime);
				this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1.3f, 0f);
			}
		}
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x0006196E File Offset: 0x0005FB6E
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x04000F12 RID: 3858
	private float attackRange;

	// Token: 0x04000F13 RID: 3859
	private float checkTimer;

	// Token: 0x04000F14 RID: 3860
	private int checkNum;

	// Token: 0x04000F15 RID: 3861
	private float checkOffset;

	// Token: 0x04000F16 RID: 3862
	private Transform effectTransform;

	// Token: 0x04000F17 RID: 3863
	private float syncTimer;
}
