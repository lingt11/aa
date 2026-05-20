using System;
using UnityEngine;

// Token: 0x020003DC RID: 988
[Serializable]
public class WorkshopAttachmentEntry
{
	// Token: 0x04001542 RID: 5442
	public HumanBodyBones bone = HumanBodyBones.RightHand;

	// Token: 0x04001543 RID: 5443
	public WorkshopAttachmentMode mode;

	// Token: 0x04001544 RID: 5444
	public string targetAttachmentName = string.Empty;

	// Token: 0x04001545 RID: 5445
	public bool keepUntargetedOriginalAttachments = true;

	// Token: 0x04001546 RID: 5446
	public bool preserveWorldScale = true;

	// Token: 0x04001547 RID: 5447
	public Vector3 localPosition = Vector3.zero;

	// Token: 0x04001548 RID: 5448
	public Vector3 localEuler = Vector3.zero;

	// Token: 0x04001549 RID: 5449
	public Vector3 localScale = Vector3.one;

	// Token: 0x0400154A RID: 5450
	public GameObject replacementPrefab;

	// Token: 0x0400154B RID: 5451
	public Mesh replacementMesh;

	// Token: 0x0400154C RID: 5452
	public Material[] replacementMaterials = Array.Empty<Material>();
}
