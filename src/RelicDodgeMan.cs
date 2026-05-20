using System;
using UnityEngine;

// Token: 0x02000211 RID: 529
public class RelicDodgeMan : RelicBase
{
	// Token: 0x0600099D RID: 2461 RVA: 0x00033C2C File Offset: 0x00031E2C
	public override void Enter()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.dogeEvent = (RoleBase.DogeEvent)Delegate.Combine(playerBase.dogeEvent, new RoleBase.DogeEvent(this.OnDogeEvent));
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x00033C58 File Offset: 0x00031E58
	private void OnDogeEvent()
	{
		if (this.roleBuff == null && this.playerBase.isLocalPlayer)
		{
			this.roleBuff = base.AddShowBuff(base.GetValue(0, 3f));
		}
		this.useSkillTime = base.GetValue(0, 3f);
		this.playerBase.addDamagePercent -= this.addDamage;
		this.addDamage = base.GetValue(1, 0.3f);
		this.playerBase.addDamagePercent += this.addDamage;
		if (this.roleBuff != null)
		{
			this.roleBuff.lifeTime = base.GetValue(0, 3f);
		}
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x00033D08 File Offset: 0x00031F08
	public override void Update()
	{
		base.Update();
		if (this.useSkillTime > 0f)
		{
			this.useSkillTime -= Time.deltaTime;
			if (this.useSkillTime <= 0f)
			{
				this.playerBase.addDamagePercent -= this.addDamage;
				this.addDamage = 0f;
				this.roleBuff = null;
			}
		}
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x00033D74 File Offset: 0x00031F74
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.dogeEvent = (RoleBase.DogeEvent)Delegate.Remove(playerBase.dogeEvent, new RoleBase.DogeEvent(this.OnDogeEvent));
		this.playerBase.addDamagePercent -= this.addDamage;
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = null;
		}
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x00033DE5 File Offset: 0x00031FE5
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = base.AddShowBuff(base.GetValue(0, 3f));
		}
	}

	// Token: 0x04000BC6 RID: 3014
	private RoleBuff roleBuff;

	// Token: 0x04000BC7 RID: 3015
	private float useSkillTime;

	// Token: 0x04000BC8 RID: 3016
	private float addDamage;
}
