using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000088 RID: 136
public class BulletBrotatoWeapon : BrotatoWeapon
{
	// Token: 0x0600030C RID: 780 RVA: 0x00014EE8 File Offset: 0x000130E8
	public new void Clear()
	{
		for (int i = 0; i < this.flyAttackList.Count; i++)
		{
			BulletBrotatoWeapon.FlyAttackData flyAttackData = this.flyAttackList[i];
			AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
			flyAttackData.myTransform = null;
		}
		this.flyAttackList = null;
		base.Clear();
	}

	// Token: 0x04000290 RID: 656
	public List<BulletBrotatoWeapon.FlyAttackData> flyAttackList = new List<BulletBrotatoWeapon.FlyAttackData>();

	// Token: 0x02000089 RID: 137
	public class FlyAttackData
	{
		// Token: 0x04000291 RID: 657
		public Transform myTransform;

		// Token: 0x04000292 RID: 658
		public float flyTime;
	}
}
