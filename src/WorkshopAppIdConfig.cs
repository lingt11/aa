using System;
using UnityEngine;

// Token: 0x020003DA RID: 986
public static class WorkshopAppIdConfig
{
	// Token: 0x060016BC RID: 5820 RVA: 0x0008CC16 File Offset: 0x0008AE16
	public static uint GetConfiguredConsumerAppId()
	{
		if (WorkshopAppIdConfig.cachedConfig == null)
		{
			WorkshopAppIdConfig.cachedConfig = Resources.Load<SOWorkshopConfig>("Bundles/SO/SOWorkshopConfig");
		}
		if (!(WorkshopAppIdConfig.cachedConfig != null))
		{
			return 0U;
		}
		return WorkshopAppIdConfig.cachedConfig.workshopConsumerAppId;
	}

	// Token: 0x060016BD RID: 5821 RVA: 0x0008CC50 File Offset: 0x0008AE50
	public static uint GetEffectiveConsumerAppId(uint fallbackAppId)
	{
		uint configuredConsumerAppId = WorkshopAppIdConfig.GetConfiguredConsumerAppId();
		if (configuredConsumerAppId == 0U)
		{
			return fallbackAppId;
		}
		return configuredConsumerAppId;
	}

	// Token: 0x0400153C RID: 5436
	private const string ResourcePath = "Bundles/SO/SOWorkshopConfig";

	// Token: 0x0400153D RID: 5437
	private static SOWorkshopConfig cachedConfig;
}
