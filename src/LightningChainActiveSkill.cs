using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E8 RID: 744
public class LightningChainActiveSkill : ActiveSkillBase
{
	// Token: 0x06001134 RID: 4404 RVA: 0x000633EC File Offset: 0x000615EC
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.checkTimer = duration - 0.15f;
		this.skillTime = duration;
		this.checkPos = pos;
		this.checkOffset = interval;
		Transform transform = Game.EffectManager.PlayEffect(EffectDefine.Lighting, duration, this.attackRoleBase.GetAttackPos(), 1f);
		this.lineRenderer = transform.GetComponent<LineRenderer>();
		this.hitRoles.Add(this.attackRoleBase);
		this.lineRenderer.positionCount = 0;
		this.lineRenderer.SetPositions(new Vector3[0]);
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x000634A8 File Offset: 0x000616A8
	protected override void UpdateSkill(float time)
	{
		if (this.attackRoleBase == null)
		{
			return;
		}
		if (this.skillTime > 0.35f && this.skillTime < this.checkTimer - (float)this.checkNum * this.checkOffset)
		{
			this.checkNum++;
			RoleBase roleBase = this.hitRoles[this.hitRoles.Count - 1];
			if (roleBase != null && roleBase != this.attackRoleBase)
			{
				this.checkPos = roleBase.MyTransform.position;
			}
			float num = this.attackRange;
			bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			bool flag2 = false;
			RoleBase roleBase2 = null;
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase3 = attackRoles[i];
				if (roleBase3 != null && roleBase3.gameObject.activeSelf && !roleBase3.IsDead() && this.hitRoles.IndexOf(roleBase3) == -1)
				{
					float distanceV = this.GetDistanceV2(roleBase3.MyTransform.position);
					if (distanceV < num)
					{
						num = distanceV;
						roleBase2 = roleBase3;
						flag2 = true;
					}
				}
			}
			if (flag2)
			{
				this.hitRoles.Add(roleBase2);
				Game.EffectManager.PlayEffect(EffectDefine.LightingHit, 2f, roleBase2.GetAttackPos(), 0.25f);
				if (this.attackRoleBase.hasAuthority)
				{
					bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
					long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
					if (flag)
					{
						roleBase2.OnHit(this.attackRoleBase, (double)skillDamage, Util.GetV2Angle(roleBase2.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
					else
					{
						Util.OnLocalPlayerHit(this.attackRoleBase, roleBase2, (double)skillDamage, Util.GetV2Angle(roleBase2.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
				}
			}
			else
			{
				this.skillTime = -1f;
			}
		}
		this.UpdateLineRenderer();
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x000636B8 File Offset: 0x000618B8
	private void UpdateLineRenderer()
	{
		int count = this.hitRoles.Count;
		if (count > 1)
		{
			Vector3[] array = new Vector3[count];
			for (int i = 0; i < count; i++)
			{
				if (this.hitRoles[i] != null)
				{
					array[i] = this.hitRoles[i].GetAttackPos();
				}
			}
			this.lineRenderer.positionCount = array.Length;
			this.lineRenderer.SetPositions(array);
		}
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x00063730 File Offset: 0x00061930
	public float GetDistanceV2(Vector3 pos)
	{
		Vector3 vector = this.checkPos;
		return Mathf.Sqrt(Mathf.Pow(pos.x - vector.x, 2f) + Mathf.Pow(pos.z - vector.z, 2f));
	}

	// Token: 0x04000F40 RID: 3904
	private float attackRange;

	// Token: 0x04000F41 RID: 3905
	private float checkTimer;

	// Token: 0x04000F42 RID: 3906
	private Vector3 checkPos;

	// Token: 0x04000F43 RID: 3907
	private int checkNum;

	// Token: 0x04000F44 RID: 3908
	private float checkOffset;

	// Token: 0x04000F45 RID: 3909
	private List<RoleBase> hitRoles = new List<RoleBase>();

	// Token: 0x04000F46 RID: 3910
	private LineRenderer lineRenderer;
}
