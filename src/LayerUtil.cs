using System;
using UnityEngine;

// Token: 0x02000123 RID: 291
public static class LayerUtil
{
	// Token: 0x040007D1 RID: 2001
	public static readonly int DefaultLayer = LayerMask.NameToLayer("Default");

	// Token: 0x040007D2 RID: 2002
	public static readonly int DefaultLayerMask = 1 << LayerUtil.DefaultLayer;

	// Token: 0x040007D3 RID: 2003
	public static readonly int EnemyLayer = LayerMask.NameToLayer("Enemy");

	// Token: 0x040007D4 RID: 2004
	public static readonly int EnemyLayerMask = 1 << LayerUtil.EnemyLayer;
}
