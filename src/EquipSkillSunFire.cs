using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class EquipSkillSunFire : EquipSkillBase
{
	// Token: 0x06000538 RID: 1336 RVA: 0x0001E990 File Offset: 0x0001CB90
	public override void Init()
	{
		base.Init();
		this.checkTime = Time.time + 0.5f;
		GameHelperClient.localPlayer.CmdAddBuff(GameHelperClient.localPlayer.netId, GameHelperClient.localPlayer.netId, LocalBuffType.SunFire, 2f, 99999f, 0);
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x0001E9E0 File Offset: 0x0001CBE0
	public override void OnUpdate()
	{
		base.OnUpdate();
		if (Time.time > this.checkTime)
		{
			this.checkTime += 0.5f;
			if (!this.playerBase.IsDead())
			{
				List<RoleBase> attackRoles = this.playerBase.GetAttackRoles();
				int count = attackRoles.Count;
				Vector3 position = this.playerBase.MyTransform.position;
				float num = ((float)this.equipNum * this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel) * 0.01f;
				long num2 = ConstDefine.ClampBattleValue((double)((float)this.playerBase.maxHp * num * 0.5f));
				bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Buff);
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(position, roleBase.MyTransform.position, this.GetAttackRange() + roleBase.RoleModeBase.addRange, false))
					{
						Util.OnLocalPlayerHit(this.playerBase, roleBase, (double)num2, Util.GetV2Angle(roleBase.MyTransform.position, this.playerBase.MyTransform.position), AttackType.Buff, isAttackWeek);
					}
				}
			}
		}
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x0001EB39 File Offset: 0x0001CD39
	private float GetAttackRange()
	{
		return 2f * (1f + this.playerBase.skillRange) + this.playerBase.RoleModeBase.addRange + this.playerBase.haloRangeAdd;
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x0001EB6F File Offset: 0x0001CD6F
	public override void Clear()
	{
		base.Clear();
		GameHelperClient.localPlayer.CmdRemoveuff(GameHelperClient.localPlayer.netId, LocalBuffType.SunFire);
	}

	// Token: 0x04000488 RID: 1160
	private float checkTime;

	// Token: 0x04000489 RID: 1161
	private const float CheckOffset = 0.5f;
}
