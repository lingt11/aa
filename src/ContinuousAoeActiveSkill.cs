using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public class ContinuousAoeActiveSkill : ActiveSkillBase
{
	// Token: 0x060010DC RID: 4316 RVA: 0x0005EEEC File Offset: 0x0005D0EC
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.checkTimer = duration - 0.25f;
		this.skillTime = duration;
		this.checkPos = pos;
		this.checkOffset = interval;
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x0005EF4C File Offset: 0x0005D14C
	protected override void UpdateLocalSkill(float time)
	{
		if (this.skillTime < this.checkTimer - (float)this.checkNum * this.checkOffset)
		{
			this.checkNum++;
			this.hitRoles.Clear();
			bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && this.GetDistanceV2(roleBase.MyTransform.position) < this.attackRange)
				{
					this.hitRoles.Add(roleBase);
				}
			}
			count = this.hitRoles.Count;
			if (count > 0)
			{
				RoleBase roleBase2 = this.hitRoles[Random.Range(0, count)];
				long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
				bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
				if (flag)
				{
					roleBase2.OnHit(this.attackRoleBase, (double)skillDamage, 360f * Random.value, AttackType.Skill, isAttackWeek);
				}
				else
				{
					Util.OnLocalPlayerHit(this.attackRoleBase, roleBase2, (double)skillDamage, 360f * Random.value, AttackType.Skill, isAttackWeek);
				}
				GameHelperClient.localPlayer.CmdPlayEffect(EffectDefine.SpellThunder, 1f, roleBase2.MyTransform.position, 1f);
				return;
			}
			Vector2 pointByRadian = Util.GetPointByRadian(this.attackRange * Random.value, 0f, Random.value * 360f);
			GameHelperClient.localPlayer.CmdPlayEffect(EffectDefine.SpellThunder, 1f, new Vector3(this.checkPos.x + pointByRadian.x, 0f, this.checkPos.z + pointByRadian.y), 1f);
		}
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x0005F130 File Offset: 0x0005D330
	public float GetDistanceV2(Vector3 pos)
	{
		Vector3 vector = this.checkPos;
		return Mathf.Sqrt(Mathf.Pow(pos.x - vector.x, 2f) + Mathf.Pow(pos.z - vector.z, 2f));
	}

	// Token: 0x04000ECF RID: 3791
	private float attackRange;

	// Token: 0x04000ED0 RID: 3792
	private float checkTimer;

	// Token: 0x04000ED1 RID: 3793
	private Vector3 checkPos;

	// Token: 0x04000ED2 RID: 3794
	private int checkNum;

	// Token: 0x04000ED3 RID: 3795
	private float checkOffset;

	// Token: 0x04000ED4 RID: 3796
	private List<RoleBase> hitRoles = new List<RoleBase>();
}
