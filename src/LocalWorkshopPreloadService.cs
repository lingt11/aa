using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003D4 RID: 980
public class LocalWorkshopPreloadService : IUpdate
{
	// Token: 0x06001673 RID: 5747 RVA: 0x0008BA03 File Offset: 0x00089C03
	public void Update()
	{
		if (Time.unscaledTime < this.nextPreloadTime)
		{
			return;
		}
		this.nextPreloadTime = Time.unscaledTime + 2f;
		this.PreloadEnabledHeroMods();
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x0008BA2C File Offset: 0x00089C2C
	private void PreloadEnabledHeroMods()
	{
		LocalWorkshopModSettings localWorkshopModSettings = Game.LocalWorkshopModSettings;
		if (localWorkshopModSettings == null)
		{
			return;
		}
		List<LocalWorkshopHeroModEntry> enabledHeroModsSnapshot = localWorkshopModSettings.GetEnabledHeroModsSnapshot();
		for (int i = 0; i < enabledHeroModsSnapshot.Count; i++)
		{
			LocalWorkshopHeroModEntry localWorkshopHeroModEntry = enabledHeroModsSnapshot[i];
			if (localWorkshopHeroModEntry != null && localWorkshopHeroModEntry.heroId > 0)
			{
				LocalHeroModelService.TryPreloadOverrideForHero(localWorkshopHeroModEntry.heroId);
			}
		}
		List<LocalWorkshopLocalFileModEntry> enabledLocalFileModsSnapshot = localWorkshopModSettings.GetEnabledLocalFileModsSnapshot();
		for (int j = 0; j < enabledLocalFileModsSnapshot.Count; j++)
		{
			LocalWorkshopLocalFileModEntry localWorkshopLocalFileModEntry = enabledLocalFileModsSnapshot[j];
			if (localWorkshopLocalFileModEntry != null && localWorkshopLocalFileModEntry.heroId > 0)
			{
				LocalHeroModelService.TryPreloadOverrideForHero(localWorkshopLocalFileModEntry.heroId);
			}
		}
	}

	// Token: 0x0400150C RID: 5388
	private const float PreloadInterval = 2f;

	// Token: 0x0400150D RID: 5389
	private float nextPreloadTime;
}
