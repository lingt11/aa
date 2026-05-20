using System;
using UnityEngine;

// Token: 0x02000096 RID: 150
[Serializable]
public struct NormalEnemyDropType
{
	// Token: 0x040002EF RID: 751
	[Header("技能书")]
	public float book;

	// Token: 0x040002F0 RID: 752
	[Header("属性书")]
	public float attribute;

	// Token: 0x040002F1 RID: 753
	[Header("神符")]
	public float talisman;
}
