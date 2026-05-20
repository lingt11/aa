using System;
using UnityEngine;

// Token: 0x020000A5 RID: 165
[CreateAssetMenu(menuName = "ScriptableObject/SOWorkshopConfig")]
public class SOWorkshopConfig : ScriptableObject
{
	// Token: 0x0400032A RID: 810
	[Header("创意工坊目标 AppID")]
	public uint workshopConsumerAppId;
}
