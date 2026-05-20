using System;
using UnityEngine;

// Token: 0x020002EE RID: 750
public class PlayerDragonFireBoomActiveSkill : AnimationOverrideActiveSkill
{
	// Token: 0x06001154 RID: 4436 RVA: 0x00064400 File Offset: 0x00062600
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, ActiveSkillData activeSkillDataValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = activeSkillDataValue;
		this.attackRoleBase = attackRole;
		this.attackPos = pos;
		this.skillTime = 3.633f;
		this.checkTimer = this.skillTime - 1.16256f;
		base.LoadAnimatorController("Bundles/Animator/GreatSword_FocusEnergy_R_Attack03_Root");
		this.attackRoleBase.UpdateAnimSpeed(1f);
		Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/SaiYaSkill1", this.attackRoleBase.MyTransform.position, 0.8f);
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x00064488 File Offset: 0x00062688
	protected override void UpdateLocalSkill(float time)
	{
		if (this.attackRoleBase == null)
		{
			return;
		}
		if (this.skillTime < this.checkTimer && !this.isCheck)
		{
			this.isCheck = true;
			this.attackRoleBase.CmdCreateSkill(ActiveSkillEnum.PlayerDrangonFireBoomEnd, this.attackPos, 0f, -1, 0);
		}
		if (this.updateTimer < 1.16256f)
		{
			this.updateTimer += time;
			float v2Angle = this.attackRoleBase.GetV2Angle(this.attackPos);
			this.attackRoleBase.PingHuaZhuanShen(v2Angle, 2f);
			PlayerBase playerBase = this.attackRoleBase as PlayerBase;
			if (playerBase != null)
			{
				Vector3 motion = Vector3.Lerp(this.attackRoleBase.MyTransform.position, this.attackPos - this.attackRoleBase.MyTransform.forward * 1f, this.updateTimer * 0.3f) - this.attackRoleBase.MyTransform.position;
				playerBase.CharacterController.Move(motion);
				return;
			}
			this.attackRoleBase.MyTransform.position = Vector3.Lerp(this.attackRoleBase.MyTransform.position, this.attackPos - this.attackRoleBase.MyTransform.forward * 1f, this.updateTimer * 0.3f);
		}
	}

	// Token: 0x06001156 RID: 4438 RVA: 0x000645EE File Offset: 0x000627EE
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.skillTime < 1.35f)
		{
			this.skillTime = -1f;
		}
	}

	// Token: 0x04000F6C RID: 3948
	private float checkTimer;

	// Token: 0x04000F6D RID: 3949
	private bool isCheck;

	// Token: 0x04000F6E RID: 3950
	private Vector3 attackPos;

	// Token: 0x04000F6F RID: 3951
	private const float SkillAniTime = 3.633f;

	// Token: 0x04000F70 RID: 3952
	private const float SkillCheckTime = 1.16256f;

	// Token: 0x04000F71 RID: 3953
	private float updateTimer;
}
