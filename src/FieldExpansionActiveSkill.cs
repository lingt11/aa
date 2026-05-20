using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class FieldExpansionActiveSkill : ActiveSkillBase
{
	// Token: 0x060010F6 RID: 4342 RVA: 0x00060524 File Offset: 0x0005E724
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, string effectName, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.checkTimer = duration - 0.25f;
		this.skillTime = duration;
		this.checkPos = pos;
		this.checkOffset = interval;
		pos.y = 0f;
		this.effectTransform = Game.EffectManager.PlayEffect(effectName, 10.5f, pos, 1f);
		this.effectTransform.localScale = Vector3.zero;
		this.effectTransform.localEulerAngles = new Vector3(0f, Random.value * 360f, 0f);
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x000605E0 File Offset: 0x0005E7E0
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.effectTransform != null)
		{
			if (this.skillTime < 0.3f)
			{
				this.effectTransform.localScale = Vector3.Lerp(this.effectTransform.localScale, Vector3.zero, time * 8f);
				return;
			}
			this.effectTransform.localScale = Vector3.Lerp(this.effectTransform.localScale, this.attackRange * 1.7f * Vector3.one, time * 5f);
			if (this.skillTime < this.checkTimer - (float)this.checkNum * this.checkOffset)
			{
				this.checkNum++;
				List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
				int count = attackRoles.Count;
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && !this.checkRoles.Contains(roleBase) && Util.NewCheckYuanXing(this.checkPos, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
					{
						this.checkRoles.Add(roleBase);
						if (this.attackRoleBase.HasAuthority)
						{
							Util.CmdXuanYun(roleBase, this.skillTime - 0.3f);
						}
						if (roleBase.HasAuthority)
						{
							roleBase.AddArmor(-100);
						}
					}
				}
			}
		}
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x00060764 File Offset: 0x0005E964
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			this.effectTransform.localScale = Vector3.zero;
		}
		int count = this.checkRoles.Count;
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = this.checkRoles[i];
			if (roleBase != null && !roleBase.IsDead() && roleBase.HasAuthority)
			{
				roleBase.AddArmor(100);
			}
		}
		this.checkRoles = null;
	}

	// Token: 0x04000EEC RID: 3820
	protected float attackRange;

	// Token: 0x04000EED RID: 3821
	private float checkTimer;

	// Token: 0x04000EEE RID: 3822
	protected Vector3 checkPos;

	// Token: 0x04000EEF RID: 3823
	private int checkNum;

	// Token: 0x04000EF0 RID: 3824
	private float checkOffset;

	// Token: 0x04000EF1 RID: 3825
	private Transform effectTransform;

	// Token: 0x04000EF2 RID: 3826
	private List<RoleBase> checkRoles = new List<RoleBase>();
}
