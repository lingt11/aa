using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class EquipSkillNinjaScabbard : EquipSkillBase
{
	// Token: 0x060004F3 RID: 1267 RVA: 0x0001D720 File Offset: 0x0001B920
	public override void Init()
	{
		base.Init();
		this.addTime = this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel;
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
		PlayerBase playerBase2 = this.playerBase;
		playerBase2.useSkillEvent = (RoleBase.UseSkillEvent)Delegate.Combine(playerBase2.useSkillEvent, new RoleBase.UseSkillEvent(this.UseSkillEvent));
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0001D7A0 File Offset: 0x0001B9A0
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		this.checkTime = 0f;
		if (this.isAddAttack)
		{
			this.isAddAttack = false;
			this.playerBase.normalAttackAddDamage -= this.addNormalAttackDamage;
			if (this.roleBuff != null)
			{
				this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
				this.roleBuff = null;
			}
		}
		return damage;
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0001D808 File Offset: 0x0001BA08
	private ActiveSkillEnum UseSkillEvent(ActiveSkillEnum activeSkillEnum)
	{
		this.checkTime = 0f;
		if (this.isAddAttack)
		{
			this.isAddAttack = false;
			this.playerBase.normalAttackAddDamage -= this.addNormalAttackDamage;
			if (this.roleBuff != null)
			{
				this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
				this.roleBuff = null;
			}
		}
		return activeSkillEnum;
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0001D870 File Offset: 0x0001BA70
	public override void OnUpdate()
	{
		base.OnUpdate();
		if (!this.isAddAttack && this.checkTime < this.addTime)
		{
			this.checkTime += Time.deltaTime;
			if (this.checkTime >= this.addTime)
			{
				this.addTime = this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel;
				this.isAddAttack = true;
				this.checkTime = 0f;
				this.addNormalAttackDamage = (this.skillValueAry[1] + this.skillValueUpAry[1] * (float)this.strengLevel) * 0.01f;
				this.playerBase.normalAttackAddDamage += this.addNormalAttackDamage;
				if (this.playerBase.isLocalPlayer)
				{
					this.roleBuff = GameHelperClient.AddShowBuff(Game.Language.Get(PathDefine.Concat("equip_", this.equipIndex), ""), GameHelperClient.localPlayer.playerAttribute.GetEquipSkillInfo(this.equipSkillType), PathDefine.Concat("Shop/equip_", this.equipIndex), -1f);
				}
			}
		}
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x0001D994 File Offset: 0x0001BB94
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
		if (this.isAddAttack)
		{
			this.isAddAttack = false;
			this.playerBase.normalAttackAddDamage -= this.addNormalAttackDamage;
		}
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = null;
		}
	}

	// Token: 0x04000470 RID: 1136
	private float checkTime;

	// Token: 0x04000471 RID: 1137
	private bool isAddAttack;

	// Token: 0x04000472 RID: 1138
	private RoleBuff roleBuff;

	// Token: 0x04000473 RID: 1139
	private float addNormalAttackDamage;

	// Token: 0x04000474 RID: 1140
	private float addTime;
}
