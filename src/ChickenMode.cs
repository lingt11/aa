using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200028C RID: 652
public class ChickenMode : RemotePlayerMode
{
	// Token: 0x06000C31 RID: 3121 RVA: 0x00046BF0 File Offset: 0x00044DF0
	public override void OnStartSkill2()
	{
		this.playerBase.PlayAni(AnimDefine.Skill2, 1f, 0.1f);
		this.playerBase.timer = 0f;
		this.playerBase.isCheckAttack = false;
		if (this.playerBase.hasAuthority)
		{
			this.myAnim.applyRootMotion = true;
			Game.AudioManager.PlayAudio("Audio/Battle_Audio/Skill/ChickenGround", 1f, 3f);
			this.playerBase.roleBuffManager.AddOneBuff<Buff无敌>("Buff无敌", 2.4f);
		}
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x00046C81 File Offset: 0x00044E81
	public override void OnExitSkill2()
	{
		base.OnExitSkill2();
		if (this.playerBase.hasAuthority)
		{
			this.myAnim.applyRootMotion = false;
		}
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x00046CA2 File Offset: 0x00044EA2
	private void OnAnimatorMove()
	{
		if (this.myAnim.applyRootMotion)
		{
			this.playerBase.CharacterController.Move(this.myAnim.deltaPosition * 4f);
		}
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x00046CD8 File Offset: 0x00044ED8
	public override void UpdateSkill2()
	{
		float deltaTime = Time.deltaTime;
		this.playerBase.timer += deltaTime;
		if (!this.playerBase.isCheckAttack && this.playerBase.timer > 1.3998001f)
		{
			this.playerBase.isCheckAttack = true;
			Vector3 position = this.playerBase.MyTransform.position;
			float num = 5f;
			position.y = 0.5f;
			Game.EffectManager.PlayEffect(EffectDefine.SmokeEffect, 3f, position, num / 2f);
			if (this.playerBase.hasAuthority)
			{
				Game.CameraManager.ShakeCameraByPos(position, 0.1f, 0.75f, 15, false);
				List<RoleBase> attackRoles = this.roleBase.GetAttackRoles();
				int count = attackRoles.Count;
				bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Skill);
				long skillDamage = Util.GetSkillDamage(Game.GameData.ActiveSkillDataDic[ActiveSkillEnum.ChickenDance], this.playerBase);
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.GetV2Distance(roleBase.MyTransform.position, position) < num)
					{
						Util.OnLocalPlayerHit(this.playerBase, roleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.playerBase.MyTransform.position), AttackType.Skill, isAttackWeek);
					}
				}
			}
		}
		if (this.playerBase.hasAuthority && this.playerBase.timer > 2.333f)
		{
			this.playerBase.UpdateRoleState(RoleState.Idle);
		}
	}
}
