using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EF RID: 751
public class PlayerDragonFireBoomEndActiveSkill : ActiveSkillBase
{
	// Token: 0x06001158 RID: 4440 RVA: 0x00064610 File Offset: 0x00062810
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

	// Token: 0x06001159 RID: 4441 RVA: 0x000646B8 File Offset: 0x000628B8
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
					long passSkillDamage = Util.GetPassSkillDamage(this.attackRoleBase, this.activeSkillData.attribute, (double)((float)this.attackRoleBase.STR * this.activeSkillData.damageExValue[0]), false);
					float num = (float)(roleBase.hp + roleBase.Shield);
					float num2;
					if (flag)
					{
						num2 = (float)roleBase.OnHit(this.attackRoleBase, (double)passSkillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
					else
					{
						num2 = (float)Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)passSkillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
					if (num > num2)
					{
						this.AddFireBuff(roleBase, (float)passSkillDamage);
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
							long passSkillDamage2 = Util.GetPassSkillDamage(this.attackRoleBase, this.activeSkillData.attribute, (double)((float)this.attackRoleBase.AGI * this.activeSkillData.damageExValue[1]), false);
							float num3 = (float)(roleBase2.hp + roleBase2.Shield);
							float num4;
							if (flag2)
							{
								num4 = (float)roleBase2.OnHit(this.attackRoleBase, (double)passSkillDamage2, Util.GetV2Angle(roleBase2.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek2);
							}
							else
							{
								num4 = (float)Util.OnLocalPlayerHit(this.attackRoleBase, roleBase2, (double)((int)passSkillDamage2), Util.GetV2Angle(roleBase2.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek2);
							}
							if (num3 > num4)
							{
								this.AddFireBuff(roleBase2, (float)passSkillDamage2);
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

	// Token: 0x0600115A RID: 4442 RVA: 0x00064A50 File Offset: 0x00062C50
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

	// Token: 0x0600115B RID: 4443 RVA: 0x00064BA8 File Offset: 0x00062DA8
	private void AddFireBuff(RoleBase roleBase, float damage)
	{
		if (roleBase.localRoleBuffDic.ContainsKey(LocalBuffType.DragonFire) || roleBase.wudi)
		{
			return;
		}
		GameHelperClient.localPlayer.CmdAddBuff(roleBase.netId, this.attackRoleBase.netId, LocalBuffType.DragonFire, (float)Util.GetPassSkillDamage(this.attackRoleBase, this.activeSkillData.attribute, (double)((float)this.attackRoleBase.STA * this.activeSkillData.damageExValue[2]), true), 5f, 1);
	}

	// Token: 0x04000F72 RID: 3954
	private float checkTimer;

	// Token: 0x04000F73 RID: 3955
	private bool isCheck;

	// Token: 0x04000F74 RID: 3956
	private Vector3 checkPos;

	// Token: 0x04000F75 RID: 3957
	private float attackRange;

	// Token: 0x04000F76 RID: 3958
	private float secondBoomTime;

	// Token: 0x04000F77 RID: 3959
	private bool isSecondBoom;

	// Token: 0x04000F78 RID: 3960
	private bool isSecondTip;
}
