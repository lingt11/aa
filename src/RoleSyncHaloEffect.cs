using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class RoleSyncHaloEffect : RoleBuffBase
{
	// Token: 0x06000351 RID: 849 RVA: 0x00015C94 File Offset: 0x00013E94
	public override void InitBuff()
	{
		base.InitBuff();
		this.playerBase = (this.roleBase as PlayerBase);
		this.checkTime = Time.time + 0.5f;
		this.haloEffectTrans = AssetManager.LoadPrefab(this.haloEffect, null, true).transform;
		this.haloEffectTrans.localScale = Vector3.one * this.GetAttackRange();
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00015CFC File Offset: 0x00013EFC
	private float GetAttackRange()
	{
		return this.buffValue * (1f + this.playerBase.skillRange) + this.playerBase.RoleModeBase.addRange + this.playerBase.haloRangeAdd;
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00015D34 File Offset: 0x00013F34
	public override void UpdateBuff()
	{
		base.UpdateBuff();
		if (Time.time > this.checkTime)
		{
			this.checkTime += 0.5f;
			if (this.haloEffectTrans != null)
			{
				this.haloEffectTrans.localScale = Vector3.one * this.GetAttackRange();
			}
		}
		if (this.haloEffectTrans != null)
		{
			this.haloEffectTrans.position = this.playerBase.MyTransform.position + new Vector3(0f, 0.1f, 0f);
		}
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00015DD1 File Offset: 0x00013FD1
	public override void ClearBuff()
	{
		base.ClearBuff();
		if (this.haloEffectTrans.gameObject != null)
		{
			this.haloEffectTrans.gameObject.UnLoadPrefab();
			this.haloEffectTrans = null;
		}
	}

	// Token: 0x04000347 RID: 839
	public string haloEffect;

	// Token: 0x04000348 RID: 840
	private Transform haloEffectTrans;

	// Token: 0x04000349 RID: 841
	private const float CheckOffset = 0.5f;

	// Token: 0x0400034A RID: 842
	private float checkTime;

	// Token: 0x0400034B RID: 843
	private PlayerBase playerBase;
}
