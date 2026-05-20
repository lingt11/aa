using System;
using UnityEngine;

// Token: 0x02000127 RID: 295
public struct GamePlayItemData
{
	// Token: 0x0400080B RID: 2059
	public int id;

	// Token: 0x0400080C RID: 2060
	public GamePlayItemType gamePlayItemType;

	// Token: 0x0400080D RID: 2061
	public Vector3 pos;

	// Token: 0x0400080E RID: 2062
	public RoleBase targetRole;

	// Token: 0x0400080F RID: 2063
	public Action actionCallback;
}
