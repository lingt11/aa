using System;
using UnityEngine;

// Token: 0x020003C6 RID: 966
[Serializable]
public class LocalHeroAttachmentRestoreData
{
	// Token: 0x040014C8 RID: 5320
	public Transform attachment;

	// Token: 0x040014C9 RID: 5321
	public Transform originalParent;

	// Token: 0x040014CA RID: 5322
	public int siblingIndex;

	// Token: 0x040014CB RID: 5323
	public bool activeSelf;

	// Token: 0x040014CC RID: 5324
	public Vector3 localPosition;

	// Token: 0x040014CD RID: 5325
	public Quaternion localRotation;

	// Token: 0x040014CE RID: 5326
	public Vector3 localScale;
}
