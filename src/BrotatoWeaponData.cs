using System;
using UnityEngine;

// Token: 0x0200008D RID: 141
[Serializable]
public struct BrotatoWeaponData
{
	// Token: 0x040002A3 RID: 675
	public BrotatoWeaponType brotatoWeaponType;

	// Token: 0x040002A4 RID: 676
	public BrotatoShootType brotatoShootType;

	// Token: 0x040002A5 RID: 677
	[Header("通用")]
	public float weaponPosZ;

	// Token: 0x040002A6 RID: 678
	public float weaponPosY;

	// Token: 0x040002A7 RID: 679
	public float autoAttackDistance;

	// Token: 0x040002A8 RID: 680
	public float attackCd;

	// Token: 0x040002A9 RID: 681
	public float attackTime;

	// Token: 0x040002AA RID: 682
	public float attackSpeedAdd;

	// Token: 0x040002AB RID: 683
	public float attackOverTime;

	// Token: 0x040002AC RID: 684
	public string[] weaponPrefabs;

	// Token: 0x040002AD RID: 685
	public AttackHitSound attackHitSound;

	// Token: 0x040002AE RID: 686
	[Header("射击")]
	public string bulletPrefab;

	// Token: 0x040002AF RID: 687
	public float bulletSpeed;

	// Token: 0x040002B0 RID: 688
	public float bulletFlyTime;

	// Token: 0x040002B1 RID: 689
	public string boomEffect;

	// Token: 0x040002B2 RID: 690
	public float boomRange;
}
