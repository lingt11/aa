using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D7 RID: 727
public class DragonFireBoomActiveSkill : ActiveSkillBase
{
	// Token: 0x060010EE RID: 4334 RVA: 0x0005FBD0 File Offset: 0x0005DDD0
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = 3.5f;
		this.checkTimer = this.skillTime - 0.05f;
		this.checkPos = pos;
		this.secondBoomTime = this.skillTime - 1.5f;
		Game.EffectManager.PlayEffect(EffectDefine.DragonFireBoom, 5f, pos + new Vector3(0f, 0.25f, 0f), rangeValue / 7f);
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x0005FC78 File Offset: 0x0005DE78
	protected override void UpdateLocalSkill(float time)
	{
		if (!this.isCheck && this.skillTime < this.checkTimer)
		{
			this.isCheck = true;
			Game.CameraManager.ShakeCameraByPos(this.checkPos, 0.5f, 0.65f, 20, false);
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
		if (this.isSecondBoom && this.skillTime < this.secondBoomTime)
		{
			this.secondBoomTime -= 0.15f;
			bool flag2 = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles2 = this.attackRoleBase.GetAttackRoles();
			int count2 = attackRoles2.Count;
			bool isAttackWeek2 = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
			for (int j = 0; j < count2; j++)
			{
				RoleBase roleBase2 = attackRoles2[j];
				if (roleBase2 != null && roleBase2.gameObject.activeSelf && !roleBase2.IsDead())
				{
					int k = 0;
					while (k < 6)
					{
						Vector2 pointByRadian = Util.GetPointByRadian(9f, 0f, (float)(k * 60));
						if (Util.NewCheckYuanXing(this.checkPos + new Vector3(pointByRadian.x, 0.01f, pointByRadian.y), roleBase2.MyTransform.position, this.attackRange / 2f + roleBase2.RoleModeBase.addRange, false))
						{
							float num3 = (float)Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase) * 0.25f;
							float num4 = (float)(roleBase2.hp + roleBase2.Shield);
							float num5;
							if (flag2)
							{
								num5 = (float)roleBase2.OnHit(this.attackRoleBase, (double)num3, Util.GetV2Angle(roleBase2.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek2);
							}
							else
							{
								num5 = (float)Util.OnLocalPlayerHit(this.attackRoleBase, roleBase2, (double)((int)num3), Util.GetV2Angle(roleBase2.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek2);
							}
							if (num4 > num5)
							{
								this.AddFireBuff(roleBase2, num3);
								break;
							}
							break;
						}
						else
						{
							k++;
						}
					}
				}
			}
		}
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x0005FFD4 File Offset: 0x0005E1D4
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (!this.isSecondTip && this.skillTime < this.secondBoomTime + 1f)
		{
			this.isSecondTip = true;
			for (int i = 0; i < 6; i++)
			{
				Vector2 pointByRadian = Util.GetPointByRadian(9f, 0f, (float)(i * 60));
				Game.EffectManager.PlayEffect(EffectDefine.DragonFireTip, 1f, this.checkPos + new Vector3(pointByRadian.x, 0.1f, pointByRadian.y), this.attackRange / 7f);
			}
		}
		if (!this.isSecondBoom && this.skillTime < this.secondBoomTime)
		{
			this.isSecondBoom = true;
			Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/DragonFireBoomAfter", this.checkPos, 1f);
			Game.CameraManager.ShakeCameraByPos(this.checkPos, 0.5f, 1f, 20, false);
			for (int j = 0; j < 6; j++)
			{
				Vector2 pointByRadian2 = Util.GetPointByRadian(9f, 0f, (float)(j * 60));
				Game.EffectManager.PlayEffect(EffectDefine.DragonFireAfterBoom, 5f, this.checkPos + new Vector3(pointByRadian2.x, 0.01f, pointByRadian2.y), this.attackRange / 7f);
			}
		}
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x0006012C File Offset: 0x0005E32C
	private void AddFireBuff(RoleBase roleBase, float damage)
	{
		if (roleBase.localRoleBuffDic.ContainsKey(LocalBuffType.DragonFire) || roleBase.wudi)
		{
			return;
		}
		GameHelperClient.localPlayer.CmdAddBuff(roleBase.netId, this.attackRoleBase.netId, LocalBuffType.DragonFire, (float)this.attackRoleBase.FinalAttackPower * 0.3f, 5f, 1);
	}

	// Token: 0x04000EE1 RID: 3809
	private float checkTimer;

	// Token: 0x04000EE2 RID: 3810
	private bool isCheck;

	// Token: 0x04000EE3 RID: 3811
	private Vector3 checkPos;

	// Token: 0x04000EE4 RID: 3812
	private float attackRange;

	// Token: 0x04000EE5 RID: 3813
	private float secondBoomTime;

	// Token: 0x04000EE6 RID: 3814
	private bool isSecondBoom;

	// Token: 0x04000EE7 RID: 3815
	private bool isSecondTip;
}
