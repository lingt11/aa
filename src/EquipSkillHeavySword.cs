using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class EquipSkillHeavySword : EquipSkillBase
{
	// Token: 0x060004CE RID: 1230 RVA: 0x0001CEBF File Offset: 0x0001B0BF
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.criticalEvent = (RoleBase.Critical)Delegate.Combine(playerBase.criticalEvent, new RoleBase.Critical(this.CriticalEvent));
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x0001CEF0 File Offset: 0x0001B0F0
	private void CriticalEvent(RoleBase hurtRole, long damage)
	{
		float num = 1f - ((float)this.equipNum * this.skillValueAry[1] + this.skillValueUpAry[1] * (float)this.strengLevel) * 0.01f;
		for (int i = 0; i < this.playerBase.roleSkillList.Count; i++)
		{
			SkillBase skillBase = this.playerBase.roleSkillList[i];
			if (!(skillBase is PasssiveSkill))
			{
				skillBase.updateCd *= num;
			}
		}
		if (this.playerBase.roleType == RoleType.King)
		{
			this.playerBase.PlayerKingAI.UpdateSkillCd(num);
		}
		int num2 = Mathf.RoundToInt((float)this.equipNum * this.skillValueAry[2] + this.skillValueUpAry[2] * (float)this.strengLevel);
		this.playerBase.AddMp(num2);
		float num3 = ((float)this.equipNum * this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel) * 0.01f;
		if ((hurtRole.IsFromRoleType(RoleType.King) && this.playerBase.IsFromRoleType(RoleType.Player)) || (hurtRole.IsFromRoleType(RoleType.Player) && this.playerBase.IsFromRoleType(RoleType.King)))
		{
			num3 *= GameHelperClient.GetKingBattleDamageLevel() * GameHelperClient.GetKingBattleAttackPercentAddHpLevel();
		}
		this.playerBase.AddPlayerHp((double)((float)damage * num3));
		float num4 = ((float)this.equipNum * this.skillValueAry[3] + this.skillValueUpAry[3] * (float)this.strengLevel) * 0.01f;
		if (hurtRole.roleType == RoleType.Enemy && (hurtRole as EnemyBase).isBoss)
		{
			num4 *= 0.2f;
		}
		if (hurtRole.Shield > 0L)
		{
			num4 *= 0.25f;
		}
		float num5 = (float)hurtRole.hp * num4;
		bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.AttackEffect);
		Util.OnLocalPlayerHit(this.playerBase, hurtRole, (double)num5, 0f, AttackType.AttackEffect, isAttackWeek);
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0001D0CB File Offset: 0x0001B2CB
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.criticalEvent = (RoleBase.Critical)Delegate.Remove(playerBase.criticalEvent, new RoleBase.Critical(this.CriticalEvent));
	}
}
