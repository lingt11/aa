using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class EquipSkillFire : EquipSkillBase
{
	// Token: 0x060004B8 RID: 1208 RVA: 0x0001CA27 File Offset: 0x0001AC27
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.skillEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.skillEnemyEvent, new RoleBase.AttackEnemy(this.SkillEnemyEvent));
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0001CA58 File Offset: 0x0001AC58
	private float SkillEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (this.fireSkillDataDic.ContainsKey(hurtrole.netId))
		{
			return damage;
		}
		EquipSkillFire.FireSkillData fireSkillData = new EquipSkillFire.FireSkillData();
		fireSkillData.roleBase = hurtrole;
		fireSkillData.damageTime = Time.time + 1f;
		this.fireSkillDataDic.Add(hurtrole.netId, fireSkillData);
		return damage;
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0001CAB0 File Offset: 0x0001ACB0
	public override void OnUpdate()
	{
		base.OnUpdate();
		for (int i = this.fireSkillDataDic.Count - 1; i >= 0; i--)
		{
			KeyValuePair<uint, EquipSkillFire.FireSkillData> keyValuePair = this.fireSkillDataDic.ElementAt(i);
			EquipSkillFire.FireSkillData value = keyValuePair.Value;
			if (value.roleBase == null || value.roleBase.IsDead())
			{
				this.fireSkillDataDic.Remove(keyValuePair.Key);
			}
			else if (Time.time > value.damageTime)
			{
				float num = ((float)this.equipNum * this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel) * 0.01f;
				if (value.roleBase.roleType == RoleType.Enemy && (value.roleBase as EnemyBase).isBoss)
				{
					num *= 0.2f;
				}
				float num2 = (float)value.roleBase.maxHp * num;
				bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Buff);
				Util.OnLocalPlayerHit(this.playerBase, value.roleBase, (double)((int)num2), Util.GetV2Angle(value.roleBase.MyTransform.position, this.playerBase.MyTransform.position), AttackType.Buff, isAttackWeek);
				if (value.damageNum == 4)
				{
					this.fireSkillDataDic.Remove(keyValuePair.Key);
				}
				else
				{
					value.damageNum++;
					value.damageTime += 1f;
				}
			}
		}
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0001CC23 File Offset: 0x0001AE23
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.skillEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.skillEnemyEvent, new RoleBase.AttackEnemy(this.SkillEnemyEvent));
	}

	// Token: 0x0400045B RID: 1115
	private Dictionary<uint, EquipSkillFire.FireSkillData> fireSkillDataDic = new Dictionary<uint, EquipSkillFire.FireSkillData>();

	// Token: 0x020000E5 RID: 229
	private class FireSkillData
	{
		// Token: 0x0400045C RID: 1116
		public RoleBase roleBase;

		// Token: 0x0400045D RID: 1117
		public float damageTime;

		// Token: 0x0400045E RID: 1118
		public int damageNum;
	}
}
