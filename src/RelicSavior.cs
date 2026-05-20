using System;
using UnityEngine;

// Token: 0x02000235 RID: 565
public class RelicSavior : RelicBase
{
	// Token: 0x06000A34 RID: 2612 RVA: 0x0003592C File Offset: 0x00033B2C
	public override void Update()
	{
		base.Update();
		if (this.checkTime < 0.1f)
		{
			this.checkTime += Time.deltaTime;
			return;
		}
		if (GameHelperClient.YaLiValue > base.GetValue(0, 0.7f))
		{
			this.playerBase.addDamagePercent -= this.addDamage;
			this.addDamage = base.GetValue(1, 0.35f);
			this.playerBase.addDamagePercent += this.addDamage;
			if (this.roleBuff == null && this.playerBase.isLocalPlayer)
			{
				this.roleBuff = base.AddShowBuff(-1f);
			}
		}
		else
		{
			this.playerBase.addDamagePercent -= this.addDamage;
			this.addDamage = 0f;
			if (this.roleBuff != null)
			{
				this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
				this.roleBuff = null;
			}
		}
		this.checkTime = 0f;
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x00035A2F File Offset: 0x00033C2F
	public override void Exit()
	{
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = null;
		}
		this.playerBase.addDamagePercent -= this.addDamage;
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00035A6E File Offset: 0x00033C6E
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = base.AddShowBuff(-1f);
		}
	}

	// Token: 0x04000BDA RID: 3034
	private RoleBuff roleBuff;

	// Token: 0x04000BDB RID: 3035
	private float addDamage;

	// Token: 0x04000BDC RID: 3036
	private float checkTime;
}
