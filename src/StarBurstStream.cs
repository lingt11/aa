using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F8 RID: 760
public class StarBurstStream : AnimationOverrideActiveSkill
{
	// Token: 0x06001193 RID: 4499 RVA: 0x00066990 File Offset: 0x00064B90
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = duration;
		this.checkTimer = duration - 0.8f;
		this.checkOffset = interval;
		base.LoadAnimatorController("Bundles/Animator/Frank_RPG_Dual_Skill04_InfinitySlash");
		this.buffGo = AssetManager.LoadPrefab(EffectDefine.CastSwordMan_Blue, null, true);
		Transform transform = this.buffGo.transform;
		transform.SetParent(attackRole.MyTransform);
		transform.localPosition = Vector3.zero;
		transform.localScale = attackRole.RoleModeBase.headUIHeight / 2f * Vector3.one;
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x00066A48 File Offset: 0x00064C48
	protected override void UpdateLocalSkill(float time)
	{
		if (!this.isStart)
		{
			return;
		}
		if (this.attackRoleBase == null)
		{
			return;
		}
		if (this.skillTime < this.checkTimer - (float)this.checkNum * this.checkOffset)
		{
			this.checkNum++;
			bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
			bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Normal);
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.effectTransform.position, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
				{
					if (flag)
					{
						roleBase.OnHit(this.attackRoleBase, (double)skillDamage, this.effectTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
					}
					else
					{
						Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, this.effectTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
					}
				}
			}
		}
	}

	// Token: 0x06001195 RID: 4501 RVA: 0x00066BA4 File Offset: 0x00064DA4
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null)
		{
			if (this.attackRoleBase.IsDead())
			{
				this.skillTime = -1f;
			}
			if (this.attackRoleBase.RoleModeBase.myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.608f && this.skillTime > 1.35f)
			{
				this.attackRoleBase.RoleModeBase.myAnim.Play(AnimDefine.Idle, 0, 0.297f);
			}
		}
		if (!this.isStart && this.skillTime < this.checkTimer)
		{
			this.checkTimer = this.skillTime - 0.1f;
			this.isStart = true;
			this.effectTransform = AssetManager.LoadPrefab(EffectDefine.StarBurstStream, null, true).transform;
			this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1.3f, 0f);
			this.effectTransform.localRotation = this.attackRoleBase.MyTransform.rotation;
			float num = this.attackRange / 3f;
			this.effectTransform.localScale = new Vector3(num, num, num);
		}
		if (this.skillTime < 1f)
		{
			this.effectTransform.position += Time.deltaTime * 15f * this.effectTransform.forward;
			if (this.buffGo != null)
			{
				AssetManager.UnLoadPrefab(this.buffGo, false);
				this.buffGo = null;
			}
		}
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x00066D4C File Offset: 0x00064F4C
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
		if (this.buffGo != null)
		{
			AssetManager.UnLoadPrefab(this.buffGo, false);
			this.buffGo = null;
		}
	}

	// Token: 0x04000FB8 RID: 4024
	private float attackRange;

	// Token: 0x04000FB9 RID: 4025
	private float checkTimer;

	// Token: 0x04000FBA RID: 4026
	private int checkNum;

	// Token: 0x04000FBB RID: 4027
	private float checkOffset;

	// Token: 0x04000FBC RID: 4028
	private Transform effectTransform;

	// Token: 0x04000FBD RID: 4029
	private bool isStart;

	// Token: 0x04000FBE RID: 4030
	private GameObject buffGo;
}
