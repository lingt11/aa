using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000269 RID: 617
public class EnemyGoblinWarriorMode : EnemyMeleeMode
{
	// Token: 0x06000B54 RID: 2900 RVA: 0x0003BFC4 File Offset: 0x0003A1C4
	public override void UpdateSkill1()
	{
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (!this.enemyBase.isCheckAttack && this.enemyBase.timer > 1f)
		{
			this.enemyBase.isCheckAttack = true;
			this.effectTransform = AssetManager.LoadPrefab(EffectDefine.WhirlwindSkill, null, true).transform;
			this.effectTransform.localPosition = this.enemyBase.MyTransform.position + new Vector3(0f, 1.65f, 0f);
			this.effectTransform.localRotation = Quaternion.identity;
			float num = this.skill1Range / 3f;
			this.effectTransform.localScale = new Vector3(num, num, num);
			this.effectTransform.SetParent(this.enemyBase.MyTransform);
		}
		else if (this.enemyBase.timer > 6.7f && this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		if (this.enemyBase.timer > this.skill1Time)
		{
			if (this.enemyBase.hasAuthority)
			{
				this.enemyBase.UpdateRoleState(RoleState.Run);
				return;
			}
		}
		else if (this.enemyBase.timer < 6.7f)
		{
			if (this.enemyBase.timer > this.checkTimer)
			{
				this.checkTimer += 0.333f;
				List<RoleBase> attackRoles = this.roleBase.GetAttackRoles();
				int count = attackRoles.Count;
				Vector3 position = this.enemyBase.MyTransform.position;
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.GetV2Distance(roleBase.MyTransform.position, position) < this.skill1Range)
					{
						roleBase.OnHit(this.enemyBase, (double)this.enemyBase.FinalAttackPower, Util.GetV2Angle(roleBase.MyTransform.position, position), AttackType.Skill, false);
					}
				}
			}
			this.enemyBase.TrackRotation(1f);
			if (this.enemyBase.trackRoleBase != null)
			{
				this.enemyBase.MyTranslate(this.enemyBase.GetMoveSpeed() * 0.5f * deltaTime);
				return;
			}
			this.enemyBase.GetTrackRole(false, 15f, false, false);
		}
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x0003C260 File Offset: 0x0003A460
	public override void OnStartSkill()
	{
		this.enemyBase.timer = 0f;
		this.enemyBase.PlayAni(AnimDefine.Skill, 1f, 0.1f);
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.enemyBase.MyTransform.position);
		this.checkTimer = 1f;
		this.enemyBase.isCheckAttack = false;
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x0003C2CE File Offset: 0x0003A4CE
	public override void OnExitSkill()
	{
		base.OnExitSkill();
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x04000C3E RID: 3134
	private float skill1Time = 7.7f;

	// Token: 0x04000C3F RID: 3135
	private float skill1Range = 4f;

	// Token: 0x04000C40 RID: 3136
	private Transform effectTransform;

	// Token: 0x04000C41 RID: 3137
	private float checkTimer;
}
