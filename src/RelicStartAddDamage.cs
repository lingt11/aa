using System;
using UnityEngine;

// Token: 0x02000240 RID: 576
public class RelicStartAddDamage : RelicBase
{
	// Token: 0x06000A60 RID: 2656 RVA: 0x00036166 File Offset: 0x00034366
	public override void Enter()
	{
		MySystemEvent.Instance.RegisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x00036180 File Offset: 0x00034380
	public override void Update()
	{
		base.Update();
		if (GameHelperClient.isReady)
		{
			if (this.addDamage > 0f)
			{
				this.playerBase.addDamagePercent -= this.addDamage;
				this.addDamage = 0f;
			}
			return;
		}
		this.timer += Time.deltaTime;
		if (this.timer >= base.GetValue(0, 10f))
		{
			if (this.addDamage > 0f)
			{
				this.playerBase.addDamagePercent -= this.addDamage;
				this.addDamage = 0f;
				return;
			}
		}
		else if (Mathf.Approximately(this.addDamage, 0f))
		{
			this.playerBase.addDamagePercent += base.GetValue(1, 0.25f);
			this.addDamage = base.GetValue(1, 0.25f);
			base.AddShowBuff(base.GetValue(0, 10f));
		}
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00036277 File Offset: 0x00034477
	private void OnWaveLevelUp(Body body)
	{
		this.timer = 0f;
		if (this.addDamage > 0f)
		{
			this.playerBase.addDamagePercent -= this.addDamage;
			this.addDamage = 0f;
		}
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x000362B4 File Offset: 0x000344B4
	public override void Exit()
	{
		base.Exit();
		MySystemEvent.Instance.UnregisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
		if (this.addDamage > 0f)
		{
			this.playerBase.addDamagePercent -= this.addDamage;
			this.addDamage = 0f;
		}
	}

	// Token: 0x04000BE4 RID: 3044
	private float addDamage;

	// Token: 0x04000BE5 RID: 3045
	private float timer;
}
