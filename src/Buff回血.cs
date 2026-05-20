using System;
using UnityEngine;

// Token: 0x02000178 RID: 376
public class Buff回血 : RoleBuff
{
	// Token: 0x06000749 RID: 1865 RVA: 0x0002BD9C File Offset: 0x00029F9C
	public override void OnInit()
	{
		this.addHp = (float)this.roleBase.maxHp * this.hpRate;
		this.addaddMpPerSecondHp = this.addHp / this.lifeTime;
		if (this.effect == null)
		{
			this.effect = AssetManager.LoadPrefab(EffectDefine.HealLoop, null, true);
			this.effect.transform.SetParent(this.roleBase.transform);
			this.effect.transform.localPosition = new Vector3(0f, 0f, 0f);
		}
		this.icon = "Shop/hpPotion";
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x0002BE40 File Offset: 0x0002A040
	public override void Update()
	{
		this.time += Time.deltaTime;
		if (this.time >= 1f)
		{
			this.time = 0f;
			GameHelperClient.localPlayer.AddPlayerHp((double)this.addaddMpPerSecondHp);
		}
		base.Update();
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x0002BE8E File Offset: 0x0002A08E
	public override void OnExit()
	{
		this.effect.UnLoadPrefab();
		this.effect = null;
	}

	// Token: 0x04000B32 RID: 2866
	private float time;

	// Token: 0x04000B33 RID: 2867
	public float hpRate;

	// Token: 0x04000B34 RID: 2868
	private float addHp;

	// Token: 0x04000B35 RID: 2869
	private float addaddMpPerSecondHp;

	// Token: 0x04000B36 RID: 2870
	private GameObject effect;
}
