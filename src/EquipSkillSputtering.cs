using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class EquipSkillSputtering : EquipSkillBase
{
	// Token: 0x06000524 RID: 1316 RVA: 0x0001E5A7 File Offset: 0x0001C7A7
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x0001E5D8 File Offset: 0x0001C7D8
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		List<RoleBase> attackRoles = this.playerBase.GetAttackRoles();
		int count = attackRoles.Count;
		Vector3 position = hurtrole.MyTransform.position;
		bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.AttackEffect);
		float num = 1f + this.playerBase.skillRange;
		int num2 = (int)(damage * ((float)this.equipNum * this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel) * 0.01f);
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && roleBase != hurtrole && Util.NewCheckYuanXing(position, roleBase.MyTransform.position, num * 2.5f + roleBase.RoleModeBase.addRange, false))
			{
				Util.OnLocalPlayerHit(this.playerBase, roleBase, (double)num2, Util.GetV2Angle(roleBase.MyTransform.position, this.playerBase.MyTransform.position), AttackType.AttackEffect, isAttackWeek);
			}
		}
		GameHelperClient.localPlayer.CmdPlayEffect(EffectDefine.SwordImpactEpicGold, 1f, position, num * 0.833f);
		return damage;
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x0001E719 File Offset: 0x0001C919
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}
}
