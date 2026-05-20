using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public class AlchemyActiveSkill : ActiveSkillBase
{
	// Token: 0x060010AF RID: 4271 RVA: 0x0005D8BC File Offset: 0x0005BABC
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, int skillBookIdValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.skillBookId = skillBookIdValue;
		this.attackRange = rangeValue;
		this.checkTimer = 1.25f;
		this.skillTime = 1.5f;
		this.checkPos = pos;
		Game.EffectManager.PlayEffect(EffectDefine.Alchemy, 2f, pos + new Vector3(0f, 0.15f, 0f), new Vector3(rangeValue / 3f, rangeValue / 3f, rangeValue / 3f), new Vector3(0f, Random.value * 360f, 0f));
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0005D980 File Offset: 0x0005BB80
	protected override void UpdateLocalSkill(float time)
	{
		if (!this.isCheck && this.skillTime < this.checkTimer)
		{
			this.isCheck = true;
			bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.checkPos, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
				{
					long num = roleBase.hp + roleBase.Shield;
					long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
					float num2;
					if (flag)
					{
						num2 = (float)roleBase.OnHit(this.attackRoleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
					else
					{
						num2 = (float)Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
					if (this.attackRoleBase.isLocalPlayer && (float)num <= num2)
					{
						int num3 = GameHelperClient.localPlayer.AddGold(GameHelperClient.localPlayer.GetHeadUIPos(), (int)((this.activeSkillEnum / ActiveSkillEnum.C_SpellThunder + 1) * ActiveSkillEnum.D_FireTornado), true);
						SkillBase skillByBookId = GameHelperClient.localPlayer.GetSkillByBookId(this.skillBookId);
						if (skillByBookId != null)
						{
							skillByBookId.totals[0] += num3;
						}
					}
				}
			}
		}
	}

	// Token: 0x04000EA9 RID: 3753
	private float checkTimer;

	// Token: 0x04000EAA RID: 3754
	private bool isCheck;

	// Token: 0x04000EAB RID: 3755
	private Vector3 checkPos;

	// Token: 0x04000EAC RID: 3756
	private float attackRange;

	// Token: 0x04000EAD RID: 3757
	private int skillBookId;
}
