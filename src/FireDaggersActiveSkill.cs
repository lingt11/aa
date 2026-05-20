using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class FireDaggersActiveSkill : ActiveSkillBase
{
	// Token: 0x060010FA RID: 4346 RVA: 0x000607F8 File Offset: 0x0005E9F8
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, float attackEulerY)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = 2f;
		this.eulerY = attackEulerY;
		Transform transform = Game.EffectManager.PlayEffect(EffectDefine.FireDaggers, 2f, pos + new Vector3(0f, 1f, 0f), rangeValue / 2f);
		transform.eulerAngles = new Vector3(0f, attackEulerY, 0f);
		this.checkPoints = new Vector3[3];
		this.startPos = pos;
		for (int i = 0; i < 3; i++)
		{
			this.checkPoints[i] = transform.GetChild(i).position;
		}
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x000608CC File Offset: 0x0005EACC
	protected override void UpdateLocalSkill(float time)
	{
		bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
		List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
		int count = attackRoles.Count;
		bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
		this.flyTimer += time;
		if (this.flyTimer < (this.isBack ? 0.35f : 0.4f))
		{
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && !this.hitRoles.Contains(roleBase))
				{
					float num = this.isBack ? (0.4f - this.flyTimer) : this.flyTimer;
					float num2 = Mathf.Min(this.attackRange, this.attackRange / 2f + num / 0.4f * this.attackRange / 2f);
					for (int j = 0; j < 3; j++)
					{
						Vector3 vector = this.checkPoints[j];
						vector = Vector3.Lerp(this.startPos, vector, Mathf.Min(1f, num / 0.4f));
						if (Util.NewCheckYuanXing(vector, roleBase.MyTransform.position, num2 + roleBase.RoleModeBase.addRange, false))
						{
							if (!this.hitRoles.Contains(roleBase))
							{
								this.hitRoles.Add(roleBase);
							}
							float num3 = (float)(roleBase.hp + roleBase.Shield);
							long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
							float num4;
							if (flag)
							{
								num4 = (float)roleBase.OnHit(this.attackRoleBase, (double)skillDamage, this.eulerY, AttackType.Skill, isAttackWeek);
							}
							else
							{
								num4 = (float)Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, this.eulerY, AttackType.Skill, isAttackWeek);
							}
							if (num3 > num4)
							{
								this.AddFireBuff(roleBase, (float)skillDamage);
							}
						}
					}
				}
			}
		}
		if (!this.isBack && this.flyTimer > 0.8f)
		{
			this.flyTimer = 0f;
			this.isBack = true;
			this.hitRoles.Clear();
		}
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x00060B10 File Offset: 0x0005ED10
	private void AddFireBuff(RoleBase roleBase, float damage)
	{
		if (roleBase.localRoleBuffDic.ContainsKey(LocalBuffType.DragonFire) || roleBase.wudi)
		{
			return;
		}
		GameHelperClient.localPlayer.CmdAddBuff(roleBase.netId, this.attackRoleBase.netId, LocalBuffType.DragonFire, (float)this.attackRoleBase.FinalAttackPower * 0.3f, 5f, 1);
	}

	// Token: 0x04000EF3 RID: 3827
	private float attackRange;

	// Token: 0x04000EF4 RID: 3828
	private List<RoleBase> hitRoles = new List<RoleBase>();

	// Token: 0x04000EF5 RID: 3829
	private float moveTimer;

	// Token: 0x04000EF6 RID: 3830
	private Vector3[] checkPoints;

	// Token: 0x04000EF7 RID: 3831
	private float eulerY;

	// Token: 0x04000EF8 RID: 3832
	private Vector3 startPos;

	// Token: 0x04000EF9 RID: 3833
	private float flyTimer;

	// Token: 0x04000EFA RID: 3834
	private const float OnceAttackTime = 0.4f;

	// Token: 0x04000EFB RID: 3835
	private bool isBack;
}
