using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
public class ThrowerBrotatoWeapon : BrotatoWeapon
{
	// Token: 0x06000313 RID: 787 RVA: 0x00014F8A File Offset: 0x0001318A
	public new void Clear()
	{
		this.ClearEffect();
		base.Clear();
	}

	// Token: 0x06000314 RID: 788 RVA: 0x00014F98 File Offset: 0x00013198
	public void ClearEffect()
	{
		if (this.throwerEffect != null)
		{
			AssetManager.UnLoadPrefab(this.throwerEffect, false);
			this.throwerEffect = null;
		}
	}

	// Token: 0x04000296 RID: 662
	public GameObject throwerEffect;

	// Token: 0x04000297 RID: 663
	public float checkOffset;
}
