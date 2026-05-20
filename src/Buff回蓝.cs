using System;
using UnityEngine;

// Token: 0x02000177 RID: 375
public class Buff回蓝 : RoleBuff
{
	// Token: 0x06000746 RID: 1862 RVA: 0x0002BCE8 File Offset: 0x00029EE8
	public override void OnInit()
	{
		this.recoverTime = 10;
		this.mpRate = 0.4f;
		this.addMp = (float)(this.roleBase as PlayerBase).maxMp * this.mpRate;
		this.addaddMpPerSecondMp = this.addMp / (float)this.recoverTime;
		this.icon = "Shop/mpPotion";
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x0002BD48 File Offset: 0x00029F48
	public override void Update()
	{
		this.time += Time.deltaTime;
		if (this.time >= 1f)
		{
			this.time = 0f;
			(this.roleBase as PlayerBase).AddMp((int)this.addaddMpPerSecondMp);
		}
		base.Update();
	}

	// Token: 0x04000B2D RID: 2861
	private float time;

	// Token: 0x04000B2E RID: 2862
	private float addMp;

	// Token: 0x04000B2F RID: 2863
	private float addaddMpPerSecondMp;

	// Token: 0x04000B30 RID: 2864
	public int recoverTime;

	// Token: 0x04000B31 RID: 2865
	public float mpRate;
}
