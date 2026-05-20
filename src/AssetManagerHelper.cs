using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public static class AssetManagerHelper
{
	// Token: 0x06000018 RID: 24 RVA: 0x00002B71 File Offset: 0x00000D71
	public static void UnLoadPrefab(this GameObject go)
	{
		AssetManager.UnLoadPrefab(go, false);
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002B7A File Offset: 0x00000D7A
	public static void UnLoadPrefabNotMove(this GameObject go)
	{
		AssetManager.UnLoadPrefab(go, true);
	}
}
