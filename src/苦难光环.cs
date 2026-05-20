using System;
using UnityEngine;

// Token: 0x020001EB RID: 491
public class 苦难光环 : PasssiveSkill
{
	// Token: 0x060008DC RID: 2268 RVA: 0x00031A30 File Offset: 0x0002FC30
	public override void Enter()
	{
		this.damage = this.skillValues[0] * 0.01f;
		GameHelperClient.localPlayer.CmdAddBuff(GameHelperClient.localPlayer.netId, GameHelperClient.localPlayer.netId, LocalBuffType.SufferingHaloD + this.quality, this.distance, 99999f, 0);
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x00031A84 File Offset: 0x0002FC84
	public override void Exit()
	{
		GameHelperClient.localPlayer.CmdRemoveuff(GameHelperClient.localPlayer.netId, LocalBuffType.SufferingHaloD + this.quality);
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00031AA4 File Offset: 0x0002FCA4
	public override void Update()
	{
		this.myTime += Time.deltaTime;
		if (this.myTime >= 0.5f)
		{
			this.myTime = 0f;
			if (!GameHelperClient.localPlayer.IsDead())
			{
				float attackRange = this.GetAttackRange();
				foreach (RoleBase roleBase in ((this.roleBase.roleType == RoleType.King) ? Game.PlayerManagerClient.GetRangeEnemy(attackRange, this.roleBase.MyTransform.position) : Game.EnemyManagerClient.GetRangeEnemy(attackRange, this.roleBase.MyTransform.position)))
				{
					if (!(roleBase == null) && !roleBase.IsDead())
					{
						float num = (float)roleBase.maxHp * this.damage;
						if (roleBase.roleType == RoleType.Enemy && (roleBase as EnemyBase).isBoss)
						{
							num *= 0.2f;
						}
						num = (float)Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)num, false) * 0.5f;
						bool isAttackWeek = GameHelperClient.localPlayer.GetIsAttackWeek(AttackType.Buff);
						Util.OnLocalPlayerHit(GameHelperClient.localPlayer, roleBase, (double)((int)num), Util.GetV2Angle(roleBase.MyTransform.position, GameHelperClient.localPlayer.MyTransform.position), AttackType.Buff, isAttackWeek);
					}
				}
			}
		}
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00031C18 File Offset: 0x0002FE18
	private float GetAttackRange()
	{
		return base.Distance + this.roleBase.RoleModeBase.addRange + GameHelperClient.localPlayer.haloRangeAdd;
	}

	// Token: 0x04000BA1 RID: 2977
	private float myTime;

	// Token: 0x04000BA2 RID: 2978
	private float damage;

	// Token: 0x04000BA3 RID: 2979
	private const float CheckOffset = 0.5f;
}
