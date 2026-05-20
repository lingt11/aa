using System;
using UnityEngine;

// Token: 0x02000200 RID: 512
public class RelicBalanced : RelicBase
{
	// Token: 0x06000930 RID: 2352 RVA: 0x00002D1D File Offset: 0x00000F1D
	public override void Enter()
	{
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x000324EC File Offset: 0x000306EC
	public override void Update()
	{
		base.Update();
		if (this.checkTime < 0.15f)
		{
			this.checkTime += Time.deltaTime;
			return;
		}
		float num = (float)this.playerBase.STA / (float)this.playerBase.STR;
		float num2 = (float)this.playerBase.STA / (float)this.playerBase.AGI;
		float num3 = (float)this.playerBase.STR / (float)this.playerBase.AGI;
		float value = base.GetValue(0, 0.2f);
		float num4 = 1f - value;
		float num5 = 1f + value;
		bool flag = num > num4 && num < num5 && num2 > num4 && num2 < num5 && num3 > num4 && num3 < num5;
		if (flag != this.isActive)
		{
			this.isActive = flag;
			if (this.isActive)
			{
				this.playerBase.addDamagePercent += base.GetValue(1, 0.2f);
				this.playerBase.AddArmor(base.GetIntValue(2, 35));
				this.playerBase.AddMaxMp(base.GetIntValue(3, 200));
				if (this.roleBuff == null && this.playerBase.isLocalPlayer)
				{
					this.roleBuff = base.AddShowBuff(-1f);
					return;
				}
			}
			else
			{
				this.playerBase.addDamagePercent -= base.GetValue(1, 0.2f);
				this.playerBase.AddArmor(-base.GetIntValue(2, 35));
				this.playerBase.AddMaxMp(-base.GetIntValue(3, 200));
				if (this.roleBuff != null)
				{
					this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
					this.roleBuff = null;
				}
			}
		}
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x000326B4 File Offset: 0x000308B4
	public override void Exit()
	{
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = null;
		}
		if (this.isActive)
		{
			this.playerBase.addDamagePercent -= base.GetValue(1, 0.2f);
			this.playerBase.AddArmor(-base.GetIntValue(2, 35));
			this.playerBase.AddMaxMp(-base.GetIntValue(3, 200));
		}
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x0003273C File Offset: 0x0003093C
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = base.AddShowBuff(-1f);
		}
		if (this.isActive)
		{
			this.playerBase.addDamagePercent += base.GetLevelValueDelta(1, 0.2f, oldLevel, newLevel);
			this.playerBase.AddArmor(base.GetLevelIntValueDelta(2, 35, oldLevel, newLevel));
			this.playerBase.AddMaxMp(base.GetLevelIntValueDelta(3, 200, oldLevel, newLevel));
			if (this.roleBuff == null && this.playerBase.isLocalPlayer)
			{
				this.roleBuff = base.AddShowBuff(-1f);
			}
		}
	}

	// Token: 0x04000BA5 RID: 2981
	private RoleBuff roleBuff;

	// Token: 0x04000BA6 RID: 2982
	private bool isActive;

	// Token: 0x04000BA7 RID: 2983
	private float checkTime;
}
