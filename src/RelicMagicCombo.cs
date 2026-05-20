using System;
using UnityEngine;

// Token: 0x02000227 RID: 551
public class RelicMagicCombo : RelicBase
{
	// Token: 0x060009F6 RID: 2550 RVA: 0x00034C90 File Offset: 0x00032E90
	public override void Enter()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.useSkillEvent = (RoleBase.UseSkillEvent)Delegate.Combine(playerBase.useSkillEvent, new RoleBase.UseSkillEvent(this.UseSkillEvent));
		this.isTotalPercent = true;
		this.totals = new int[1];
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00034CCC File Offset: 0x00032ECC
	private ActiveSkillEnum UseSkillEvent(ActiveSkillEnum activeSkillEnum)
	{
		if (this.addSkillDamageIndex < base.GetIntValue(0, 3) && this.useSkillTime > 0f)
		{
			this.addSkillDamageIndex++;
			float value = base.GetValue(1, 0.25f);
			this.addSkillDamage += value;
			this.playerBase.skillExDamage += value;
			this.totals[0] = Mathf.RoundToInt(this.addSkillDamage * 100f);
		}
		if (this.roleBuff == null && this.playerBase.isLocalPlayer)
		{
			this.roleBuff = base.AddShowBuff(base.GetValue(2, 3f));
		}
		if (this.roleBuff != null)
		{
			this.roleBuff.lifeTime = base.GetValue(2, 3f);
		}
		this.useSkillTime = base.GetValue(2, 3f);
		return activeSkillEnum;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x00034DAC File Offset: 0x00032FAC
	public override void Update()
	{
		base.Update();
		if (this.useSkillTime > 0f)
		{
			this.useSkillTime -= Time.deltaTime;
			if (this.useSkillTime <= 0f)
			{
				this.playerBase.skillExDamage -= this.addSkillDamage;
				this.totals[0] = 0;
				this.addSkillDamageIndex = 0;
				this.addSkillDamage = 0f;
				this.roleBuff = null;
			}
		}
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00034E28 File Offset: 0x00033028
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.useSkillEvent = (RoleBase.UseSkillEvent)Delegate.Remove(playerBase.useSkillEvent, new RoleBase.UseSkillEvent(this.UseSkillEvent));
		this.playerBase.skillExDamage -= this.addSkillDamage;
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = null;
		}
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x00034E99 File Offset: 0x00033099
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = base.AddShowBuff(base.GetValue(2, 3f));
		}
	}

	// Token: 0x04000BCE RID: 3022
	private float useSkillTime;

	// Token: 0x04000BCF RID: 3023
	private int addSkillDamageIndex;

	// Token: 0x04000BD0 RID: 3024
	private RoleBuff roleBuff;

	// Token: 0x04000BD1 RID: 3025
	private float addSkillDamage;
}
