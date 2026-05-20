using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000207 RID: 519
public class RelicBloodMan : RelicBase
{
	// Token: 0x06000972 RID: 2418 RVA: 0x0003332C File Offset: 0x0003152C
	public override void Enter()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.healthHpEvent = (RoleBase.HealthHp)Delegate.Combine(playerBase.healthHpEvent, new RoleBase.HealthHp(this.HealthHpEvent));
		this.startCheckTime = Time.time + 1f;
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x00033366 File Offset: 0x00031566
	private void HealthHpEvent(long updateValue)
	{
		if (updateValue > 0L)
		{
			this.addValue += (float)updateValue;
		}
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0003337C File Offset: 0x0003157C
	public override void Update()
	{
		base.Update();
		if (Time.time > this.startCheckTime)
		{
			this.startCheckTime += 1f;
			if (GameHelperClient.isReady || this.playerBase.IsDead())
			{
				return;
			}
			List<RoleBase> attackRoles = this.playerBase.GetAttackRoles();
			int count = attackRoles.Count;
			Vector3 position = this.playerBase.MyTransform.position;
			bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Buff);
			this.addValue = Mathf.Clamp(this.addValue * base.GetValue(0, 1f), -100000000f, 100000000f);
			long passSkillDamage = Util.GetPassSkillDamage(this.playerBase, SkillAttribute.None, (double)this.addValue, false);
			float num = 2.25f * (1f + this.playerBase.skillRange) + this.playerBase.haloRangeAdd;
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(position, roleBase.MyTransform.position, num + roleBase.RoleModeBase.addRange, false))
				{
					Util.OnLocalPlayerHit(this.playerBase, roleBase, (double)passSkillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.playerBase.MyTransform.position), AttackType.Buff, isAttackWeek);
				}
			}
			GameHelperClient.localPlayer.CmdPlayEffect(EffectDefine.BloodMan, 1f, position, num / 3f);
			this.addValue = 0f;
		}
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0003351B File Offset: 0x0003171B
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.healthHpEvent = (RoleBase.HealthHp)Delegate.Remove(playerBase.healthHpEvent, new RoleBase.HealthHp(this.HealthHpEvent));
	}

	// Token: 0x04000BC0 RID: 3008
	private float startCheckTime;

	// Token: 0x04000BC1 RID: 3009
	private float addValue;
}
