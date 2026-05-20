using System;
using UnityEngine;

// Token: 0x02000283 RID: 643
public class HollofiedIchigoMode : MeleePlayerMode
{
	// Token: 0x06000C01 RID: 3073 RVA: 0x00042614 File Offset: 0x00040814
	public override void OnInitMode()
	{
		base.OnInitMode();
		if (this.roleBase.hasAuthority)
		{
			this.addSTA = Mathf.RoundToInt((float)this.roleBase.STA * 0.5f * (1f + this.playerBase.addHenshin));
			this.addSTR = Mathf.RoundToInt((float)this.roleBase.STR * 0.5f * (1f + this.playerBase.addHenshin));
			this.addAGI = Mathf.RoundToInt((float)this.roleBase.AGI * 0.5f * (1f + this.playerBase.addHenshin));
			this.roleBase.AddSTA(this.addSTA);
			this.roleBase.AddSTR(this.addSTR);
			this.roleBase.AddAGI(this.addAGI);
			this.addMoveSpeedPercent = 0.35f * (1f + this.playerBase.addHenshin);
			this.roleBase.moveSpeedPercent += this.addMoveSpeedPercent;
			this.addAttackSpeed = 1f + this.playerBase.addHenshin;
			this.roleBase.AddAttackSpeed(this.addAttackSpeed);
			this.roleBase.StartHealthHp((double)((float)this.roleBase.maxHp), this.roleBase);
		}
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x00042774 File Offset: 0x00040974
	public override void OnClearMode()
	{
		base.OnClearMode();
		if (this.roleBase.hasAuthority)
		{
			this.roleBase.moveSpeedPercent -= this.addMoveSpeedPercent;
			this.roleBase.AddAttackSpeed(-this.addAttackSpeed);
			this.roleBase.AddSTA(-this.addSTA);
			this.roleBase.AddSTR(-this.addSTR);
			this.roleBase.AddAGI(-this.addAGI);
		}
	}

	// Token: 0x04000CD1 RID: 3281
	private int addSTA;

	// Token: 0x04000CD2 RID: 3282
	private int addSTR;

	// Token: 0x04000CD3 RID: 3283
	private int addAGI;

	// Token: 0x04000CD4 RID: 3284
	private float addMoveSpeedPercent;

	// Token: 0x04000CD5 RID: 3285
	private float addAttackSpeed;
}
