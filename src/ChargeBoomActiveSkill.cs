using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CF RID: 719
public class ChargeBoomActiveSkill : ActiveSkillBase
{
	// Token: 0x060010D0 RID: 4304 RVA: 0x0005E9C4 File Offset: 0x0005CBC4
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, string effectName, float interval, float duration, float effectScale)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = 5f + rangeValue * 0.25f;
		this.checkTimer = duration - 0.25f;
		this.skillTime = duration;
		this.checkPos = pos;
		this.checkOffset = interval;
		Transform transform = Game.EffectManager.PlayEffect(effectName, duration, pos + new Vector3(0f, 0.1f, 0f), this.attackRange * effectScale);
		transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
		Game.CameraManager.ShakeCameraByPos(pos, 1f, 1f, 20, false);
		Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/DragonFireBoomAfter", this.checkPos, 1f);
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0005EAB8 File Offset: 0x0005CCB8
	protected override void UpdateLocalSkill(float time)
	{
		if (this.skillTime < this.checkTimer - (float)this.checkNum * this.checkOffset)
		{
			this.checkNum++;
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
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x0005EC34 File Offset: 0x0005CE34
	private void AddFireBuff(RoleBase roleBase, float damage)
	{
		if (roleBase.localRoleBuffDic.ContainsKey(LocalBuffType.DragonFire) || roleBase.wudi)
		{
			return;
		}
		GameHelperClient.localPlayer.CmdAddBuff(roleBase.netId, this.attackRoleBase.netId, LocalBuffType.DragonFire, (float)this.attackRoleBase.FinalAttackPower * 0.3f, 5f, 1);
	}

	// Token: 0x04000EC5 RID: 3781
	protected float attackRange;

	// Token: 0x04000EC6 RID: 3782
	private float checkTimer;

	// Token: 0x04000EC7 RID: 3783
	protected Vector3 checkPos;

	// Token: 0x04000EC8 RID: 3784
	private int checkNum;

	// Token: 0x04000EC9 RID: 3785
	private float checkOffset;
}
