using System;
using UnityEngine;

// Token: 0x020001DA RID: 474
public class 应急护盾 : PasssiveSkill
{
	// Token: 0x0600089F RID: 2207 RVA: 0x00030D04 File Offset: 0x0002EF04
	public override void Enter()
	{
		this.xue = this.skillValues[0] * 0.01f;
		this.hudun = this.skillValues[1] * 0.01f;
		this.cdSet = (float)this.data.DIC("cd");
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00030D50 File Offset: 0x0002EF50
	public override void Update()
	{
		if (this.cd > 0f)
		{
			this.cd -= Time.deltaTime;
			if (this.roleBase.Shield == 0L)
			{
				this.roleBase.ClearShield(this.shieldNum);
				return;
			}
		}
		else if ((float)this.roleBase.hp * 1f / (float)this.roleBase.maxHp < this.xue)
		{
			this.shieldNum = ConstDefine.ClampBattleValue((double)((float)this.roleBase.maxHp * this.hudun));
			this.roleBase.AddShield(this.shieldNum);
			this.cd = this.cdSet;
		}
	}

	// Token: 0x04000B8F RID: 2959
	private float xue;

	// Token: 0x04000B90 RID: 2960
	private float hudun;

	// Token: 0x04000B91 RID: 2961
	private float cdSet;

	// Token: 0x04000B92 RID: 2962
	private float cd;

	// Token: 0x04000B93 RID: 2963
	private long shieldNum;
}
