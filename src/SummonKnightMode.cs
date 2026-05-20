using System;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class SummonKnightMode : EnemyMeleeMode
{
	// Token: 0x060011B9 RID: 4537 RVA: 0x00067EE8 File Offset: 0x000660E8
	public override void AttackUpdate()
	{
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		float realOffsetInAttack = this.enemyBase.GetRealOffsetInAttack();
		if (this.enemyBase.timer > realOffsetInAttack)
		{
			this.enemyBase.UpdateRoleState(RoleState.Run);
			return;
		}
		float num = this.enemyBase.timer / realOffsetInAttack;
		if (!this.enemyBase.isCheckAttack && num > 0.23f && this.enemyBase.trackRoleBase != null)
		{
			this.enemyBase.isCheckAttack = true;
			this.enemyBase.CmdCreateSkillBySyncData(this.activeSkill, this.enemyBase.MyTransform.position, 0, this.enemyBase.MyTransform.localEulerAngles.y, -1, 0);
		}
		if (num < 0.5f)
		{
			if (num < 0.3f)
			{
				this.enemyBase.TrackRotation(3f);
			}
			if (this.enemyBase.trackRoleBase != null)
			{
				float num2 = base.GetAttackDistance() + this.enemyBase.trackRoleBase.RoleModeBase.addRange;
				if (this.enemyBase.GetDistanceV2(this.enemyBase.trackRoleBase.MyTransform.position) > num2 * 0.75f)
				{
					this.enemyBase.MyTranslate(deltaTime * 40f * (0.25f - Mathf.Abs(0.25f - num)));
				}
			}
		}
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x00068060 File Offset: 0x00066260
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		switch (this.enemyBase.enemyType)
		{
		case EnemyType.Summon_Knight_D:
			this.enemyBase.animTransform.localScale = Vector3.one * 1.5f;
			base.UpdateBaseAttackDistance(2.625f);
			this.activeSkill = ActiveSkillEnum.D_Kight_Sword;
			break;
		case EnemyType.Summon_Knight_C:
			this.enemyBase.animTransform.localScale = Vector3.one * 1.75f;
			base.UpdateBaseAttackDistance(3.0625f);
			this.enemyBase.RoleModeBase.addRange = 0.25f;
			this.activeSkill = ActiveSkillEnum.C_Kight_Sword;
			break;
		case EnemyType.Summon_Knight_B:
			this.enemyBase.animTransform.localScale = Vector3.one * 1.9f;
			base.UpdateBaseAttackDistance(3.325f);
			this.enemyBase.RoleModeBase.addRange = 0.5f;
			this.activeSkill = ActiveSkillEnum.B_Kight_Sword;
			break;
		case EnemyType.Summon_Knight_A:
			this.enemyBase.animTransform.localScale = Vector3.one * 2.25f;
			base.UpdateBaseAttackDistance(3.9375f);
			this.enemyBase.RoleModeBase.addRange = 0.8f;
			this.activeSkill = ActiveSkillEnum.A_Kight_Sword;
			break;
		case EnemyType.Summon_Knight_S:
			this.enemyBase.animTransform.localScale = Vector3.one * 2.75f;
			base.UpdateBaseAttackDistance(4.8125f);
			this.enemyBase.RoleModeBase.addRange = 1.15f;
			this.activeSkill = ActiveSkillEnum.S_Kight_Sword;
			break;
		}
		Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/SummonKnight", this.enemyBase.MyTransform.position, 1f);
	}

	// Token: 0x04000FD8 RID: 4056
	private ActiveSkillEnum activeSkill;
}
