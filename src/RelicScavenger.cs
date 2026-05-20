using System;
using UnityEngine;

// Token: 0x02000236 RID: 566
public class RelicScavenger : RelicBase
{
	// Token: 0x06000A38 RID: 2616 RVA: 0x00035AA0 File Offset: 0x00033CA0
	public override void Update()
	{
		base.Update();
		if (this.checkTime < 0.1f)
		{
			this.checkTime += Time.deltaTime;
			return;
		}
		if (GameHelperClient.YaLiValue < base.GetValue(0, 0.3f))
		{
			this.playerBase.addDamagePercent -= this.addDamage;
			this.addDamage = base.GetValue(1, 0.2f);
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

	// Token: 0x06000A39 RID: 2617 RVA: 0x00035BA3 File Offset: 0x00033DA3
	public override void Exit()
	{
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = null;
		}
		this.playerBase.addDamagePercent -= this.addDamage;
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00035BE2 File Offset: 0x00033DE2
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = base.AddShowBuff(-1f);
		}
	}

	// Token: 0x04000BDD RID: 3037
	private RoleBuff roleBuff;

	// Token: 0x04000BDE RID: 3038
	private float addDamage;

	// Token: 0x04000BDF RID: 3039
	private float checkTime;
}
