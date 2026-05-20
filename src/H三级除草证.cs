using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001BB RID: 443
public class H三级除草证 : PasssiveSkill
{
	// Token: 0x06000834 RID: 2100 RVA: 0x0002F212 File Offset: 0x0002D412
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
		this.hurtMonster.Clear();
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0002F246 File Offset: 0x0002D446
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0002F270 File Offset: 0x0002D470
	private float AttackEvent(RoleBase attackrole, RoleBase hurtRole, ref float damage)
	{
		if (this.hurtMonster.ContainsKey((int)hurtRole.netId))
		{
			Dictionary<int, int> dictionary = this.hurtMonster;
			int netId = (int)hurtRole.netId;
			int num = dictionary[netId];
			dictionary[netId] = num + 1;
			if (this.hurtMonster[(int)hurtRole.netId] < 3)
			{
				goto IL_184;
			}
			float distance = base.Distance;
			Vector3 position = hurtRole.MyTransform.position;
			position.y = 1f;
			this.roleBase.CmdPlayEffectObstruction(EffectDefine.WeedingBoom, 2.5f, position, distance / 2.5f);
			this.hurtMonster[(int)hurtRole.netId] = 0;
			List<RoleBase> list = (this.roleBase.roleType == RoleType.King) ? Game.PlayerManagerClient.GetRangeEnemy(distance, hurtRole.MyTransform.position) : Game.EnemyManagerClient.GetRangeEnemy(distance, hurtRole.MyTransform.position);
			long num2 = ConstDefine.ClampBattleValue((double)(this.skillValues[0] * (float)attackrole.STR));
			num2 = Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)num2, false);
			bool isAttackWeek = this.roleBase.GetIsAttackWeek(AttackType.Skill);
			using (List<RoleBase>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RoleBase roleBase = enumerator.Current;
					Util.OnLocalPlayerHit(this.roleBase, roleBase, (double)num2, Util.GetV2Angle(roleBase.MyTransform.position, this.roleBase.MyTransform.position), AttackType.Skill, isAttackWeek);
				}
				goto IL_184;
			}
		}
		this.hurtMonster.Add((int)hurtRole.netId, 1);
		IL_184:
		return damage;
	}

	// Token: 0x04000B78 RID: 2936
	public int count;

	// Token: 0x04000B79 RID: 2937
	private Dictionary<int, int> hurtMonster = new Dictionary<int, int>();
}
