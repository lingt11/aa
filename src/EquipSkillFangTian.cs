using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class EquipSkillFangTian : EquipSkillBase
{
	// Token: 0x060004B4 RID: 1204 RVA: 0x0001C831 File Offset: 0x0001AA31
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x0001C860 File Offset: 0x0001AA60
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		List<RoleBase> attackRoles = this.playerBase.GetAttackRoles();
		int count = attackRoles.Count;
		Vector3 position = hurtrole.MyTransform.position;
		bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.AttackEffect);
		float v2Angle = Util.GetV2Angle(hurtrole.MyTransform.position, this.playerBase.MyTransform.position);
		float num = 1f + this.playerBase.skillRange;
		int num2 = (int)(damage * ((float)this.equipNum * this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel) * 0.01f);
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && roleBase != hurtrole && Util.NewCheckShanXing(position, roleBase.MyTransform.position, 90f, num * 6.5f + roleBase.RoleModeBase.addRange, v2Angle, false))
			{
				Util.OnLocalPlayerHit(this.playerBase, roleBase, (double)num2, Util.GetV2Angle(roleBase.MyTransform.position, this.playerBase.MyTransform.position), AttackType.AttackEffect, isAttackWeek);
			}
		}
		GameHelperClient.localPlayer.CmdPlayEffectEuler(EffectDefine.ShanXingHit, 1f, hurtrole.GetAttackPos(), new Vector3(num * 3.125f, num * 2.5f, num * 2.5f), new Vector3(90f, v2Angle, 0f));
		return damage;
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}
}
