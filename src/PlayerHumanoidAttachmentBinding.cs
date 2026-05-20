using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029E RID: 670
[Serializable]
public class PlayerHumanoidAttachmentBinding
{
	// Token: 0x04000DB6 RID: 3510
	public HumanBodyBones bone = HumanBodyBones.RightHand;

	// Token: 0x04000DB7 RID: 3511
	public List<Transform> attachments = new List<Transform>();
}
