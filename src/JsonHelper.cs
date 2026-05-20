using System;
using UnityEngine;

// Token: 0x02000070 RID: 112
public static class JsonHelper
{
	// Token: 0x06000230 RID: 560 RVA: 0x0000C252 File Offset: 0x0000A452
	public static Vector3Json ToVector3Json(this Vector3 v3)
	{
		return new Vector3Json
		{
			x = v3.x,
			y = v3.y,
			z = v3.z
		};
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0000C280 File Offset: 0x0000A480
	public static Vector3 ToVector3(this Vector3Json v3)
	{
		return new Vector3
		{
			x = v3.x,
			y = v3.y,
			z = v3.z
		};
	}
}
