using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class EnemyGoblinBossMode : EnemyMeleeMode
{
	// Token: 0x06000B3F RID: 2879 RVA: 0x0003A76C File Offset: 0x0003896C
	public override void UpdateSkill1()
	{
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > 3.327273f)
		{
			if (this.enemyBase.hasAuthority)
			{
				this.enemyBase.UpdateRoleState(RoleState.Run);
				return;
			}
		}
		else
		{
			if (!this.enemyBase.isCheckAttack && this.enemyBase.timer > 2.227273f)
			{
				this.enemyBase.isCheckAttack = true;
				Vector3 vector = this.enemyBase.MyTransform.position + this.enemyBase.MyTransform.rotation * this.skill1Pos;
				Game.EffectManager.PlayEffect(EffectDefine.SmokeEffect, 3f, vector + new Vector3(0f, 0.5f, 0f), this.skill1Range / 2f);
				Game.CameraManager.ShakeCameraByPos(vector, 0.1f, 0.75f, 15, false);
				List<RoleBase> attackRoles = this.roleBase.GetAttackRoles();
				int count = attackRoles.Count;
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.GetV2Distance(roleBase.MyTransform.position, vector) < this.skill1Range)
					{
						roleBase.OnHit(this.enemyBase, (double)((float)this.enemyBase.FinalAttackPower * 5f), Util.GetV2Angle(roleBase.MyTransform.position, vector), AttackType.Skill, false);
					}
				}
			}
			if (this.enemyBase.timer > 0.727273f)
			{
				if (!this.isPlayerEffect)
				{
					this.isPlayerEffect = true;
					Game.EffectManager.PlayTipSector(this.enemyBase.MyTransform.position + this.enemyBase.MyTransform.rotation * this.skill1Pos, this.skill1Range * 2f, 0f, 360f, 1.5f, 0f);
					return;
				}
			}
			else if (this.enemyBase.timer < 0.62727284f && this.enemyBase.hasAuthority)
			{
				this.enemyBase.TrackRotation(3f);
			}
		}
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x0003A9C8 File Offset: 0x00038BC8
	public override void UpdateSkill2()
	{
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > 3.327273f)
		{
			if (this.enemyBase.hasAuthority)
			{
				this.enemyBase.UpdateRoleState(RoleState.Run);
				return;
			}
		}
		else
		{
			if (!this.enemyBase.isCheckAttack && this.enemyBase.timer > 1.9272729f)
			{
				this.enemyBase.isCheckAttack = true;
				if (this.enemyBase.hasAuthority)
				{
					this.enemyBase.CmdCreateSkill(ActiveSkillEnum.Boss_SwordMove, this.enemyBase.MyTransform.position, 0f, -1, 0);
				}
			}
			if (this.enemyBase.timer > 1.1272728f)
			{
				if (!this.isPlayerEffect)
				{
					this.isPlayerEffect = true;
					ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[ActiveSkillEnum.Boss_SwordMove];
					Game.EffectManager.PlayTipLine(this.enemyBase.MyTransform.position, new Vector3(activeSkillData.range * 2f, 1f, 12.5f + activeSkillData.range), this.enemyBase.MyTransform.localEulerAngles.y, 0.8f);
					return;
				}
			}
			else if (this.enemyBase.timer < 1.0272729f && this.enemyBase.hasAuthority)
			{
				this.enemyBase.TrackRotation(3f);
			}
		}
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x0003AB3C File Offset: 0x00038D3C
	public override void OnStartSkill()
	{
		base.OnStartSkill();
		this.enemyBase.isCheckAttack = false;
		this.isPlayerEffect = false;
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x0003AB58 File Offset: 0x00038D58
	public override void OnStartSkill2()
	{
		base.OnStartSkill2();
		this.enemyBase.timer = 0f;
		this.enemyBase.PlayAni(AnimDefine.Skill2, 1f, 0.1f);
		this.enemyBase.isCheckAttack = false;
		this.isPlayerEffect = false;
	}

	// Token: 0x04000C23 RID: 3107
	private const float skill1Time = 3.327273f;

	// Token: 0x04000C24 RID: 3108
	[SerializeField]
	private Vector3 skill1Pos;

	// Token: 0x04000C25 RID: 3109
	private float skill1Range = 5f;

	// Token: 0x04000C26 RID: 3110
	private bool isPlayerEffect;
}
