using System;
using System.Collections.Generic;

// Token: 0x020003D0 RID: 976
[Serializable]
public class LocalWorkshopModSettingsData
{
	// Token: 0x04001504 RID: 5380
	public List<LocalWorkshopHeroModEntry> enabledHeroMods = new List<LocalWorkshopHeroModEntry>();

	// Token: 0x04001505 RID: 5381
	public List<LocalWorkshopLocalFileModEntry> enabledLocalFileMods = new List<LocalWorkshopLocalFileModEntry>();
}
