using System;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public class AvatarDodgeActiveSkill : AnimationOverrideActiveSkill
{
	// Token: 0x060010B9 RID: 4281 RVA: 0x0005DE68 File Offset: 0x0005C068
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, ActiveSkillData activeSkillDataValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = activeSkillDataValue;
		this.attackRoleBase = attackRole;
		this.skillTime = 1.54f;
		base.LoadAnimatorController("Bundles/Animator/yinduFlash");
		this.attackRoleBase.UpdateAnimSpeed(1.5f);
		this.attackRoleBase.RoleModeBase.myAnim.Play(AnimDefine.Idle, 0, 0.03f);
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x0005DED4 File Offset: 0x0005C0D4
	protected override void UpdateLocalSkill(float time)
	{
		float normalizedTime = this.attackRoleBase.RoleModeBase.myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
		if (normalizedTime < 0.4f)
		{
			PlayerBase playerBase = this.attackRoleBase as PlayerBase;
			if (playerBase != null)
			{
				playerBase.CharacterController.Move(time * 35f * Mathf.Sqrt((normalizedTime > 0.2f) ? (0.4f - normalizedTime) : normalizedTime) * playerBase.MyTransform.forward + time * Vector3.down);
			}
		}
		if (normalizedTime > 0.75f && !this.isCheck)
		{
			this.isCheck = true;
			if (!this.attackRoleBase.IsDead())
			{
				PlayerBase playerBase2 = this.attackRoleBase as PlayerBase;
				if (playerBase2 != null)
				{
					float num = this.activeSkillData.damageExValue[0];
					this.attackRoleBase.roleBuffManager.AddOneBuff<Buff无敌>("Buff无敌", num);
					GameHelperClient.AddShowBuff(Game.Language.Get(PathDefine.Concat("a_", this.activeSkillData.id), ""), SkillBase.GetActiveSkillTip(this.activeSkillEnum), "Skill/" + this.activeSkillData.icon, num);
					AvatarDodgeActiveSkill.MyCallBack myCallBack = new AvatarDodgeActiveSkill.MyCallBack();
					myCallBack.roleBase = this.attackRoleBase;
					myCallBack.addValue = this.activeSkillData.damageExValue[1] / 100f;
					playerBase2.addDamagePercent += myCallBack.addValue;
					Game.TimerManager.AddTimer(num, new Action(myCallBack.OnCallBack));
				}
			}
		}
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x0005E070 File Offset: 0x0005C270
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		float normalizedTime = this.attackRoleBase.RoleModeBase.myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
		if (normalizedTime > 0.45f)
		{
			this.attackRoleBase.UpdateAnimSpeed(1.5f);
			return;
		}
		if (normalizedTime > 0.37f)
		{
			this.attackRoleBase.UpdateAnimSpeed(0.25f);
		}
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x0005E0D4 File Offset: 0x0005C2D4
	public override void Clear(int clearData)
	{
		if (this.attackRoleBase != null && this.attackRoleBase.hasAuthority && !this.attackRoleBase.IsDead())
		{
			this.attackRoleBase.SetRotationY(this.attackRoleBase.MyTransform.eulerAngles.y + 180f);
		}
		base.Clear(clearData);
	}

	// Token: 0x04000EB3 RID: 3763
	private const float MoveTime = 0.4f;

	// Token: 0x04000EB4 RID: 3764
	private bool isCheck;

	// Token: 0x020002C9 RID: 713
	private class MyCallBack
	{
		// Token: 0x060010BE RID: 4286 RVA: 0x0005E140 File Offset: 0x0005C340
		public void OnCallBack()
		{
			if (this.roleBase != null)
			{
				PlayerBase playerBase = this.roleBase as PlayerBase;
				if (playerBase != null)
				{
					playerBase.addDamagePercent -= this.addValue;
					this.roleBase = null;
				}
			}
		}

		// Token: 0x04000EB5 RID: 3765
		public RoleBase roleBase;

		// Token: 0x04000EB6 RID: 3766
		public float addValue;
	}
}
