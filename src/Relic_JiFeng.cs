using System;
using UnityEngine;

// Token: 0x02000256 RID: 598
public class Relic_JiFeng : RelicBase
{
	// Token: 0x06000ABB RID: 2747 RVA: 0x00037034 File Offset: 0x00035234
	public override void Update()
	{
		if (this.lifeTime > 0f)
		{
			this.lifeTime -= Time.deltaTime;
			if (this.lifeTime <= 0f)
			{
				GameHelperClient.localPlayer.AddAttackSpeed(-0.05f * (float)this.count);
				this.count = 0;
				this.myTextNum.text = "";
			}
		}
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x0003709C File Offset: 0x0003529C
	public override void BaoJi(RoleBase enemy)
	{
		this.count++;
		this.myTextNum.text = this.count.ToString();
		this.lifeTime = 10f;
		if (this.count > 10)
		{
			this.count = 10;
			this.myTextNum.text = this.count.ToString();
			return;
		}
		GameHelperClient.localPlayer.AddAttackSpeed(0.05f);
	}

	// Token: 0x04000BEC RID: 3052
	private int count;

	// Token: 0x04000BED RID: 3053
	private float lifeTime = 10f;
}
