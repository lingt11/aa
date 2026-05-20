using System;

// Token: 0x0200025D RID: 605
[Serializable]
public struct AIAttackCheck
{
	// Token: 0x04000C0B RID: 3083
	public float checkDistance;

	// Token: 0x04000C0C RID: 3084
	public float minDinstance;

	// Token: 0x04000C0D RID: 3085
	public float attackCd;

	// Token: 0x04000C0E RID: 3086
	public float initCdMin;

	// Token: 0x04000C0F RID: 3087
	public float initCdMax;

	// Token: 0x04000C10 RID: 3088
	public bool isOnlyPlayer;

	// Token: 0x04000C11 RID: 3089
	public RoleState[] attackStateList;
}
