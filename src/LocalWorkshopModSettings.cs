using System;
using System.Collections.Generic;

// Token: 0x020003D3 RID: 979
public class LocalWorkshopModSettings : IApplicationQuit
{
	// Token: 0x06001662 RID: 5730 RVA: 0x0008B364 File Offset: 0x00089564
	public LocalWorkshopModSettings()
	{
		this.Load();
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x0008B380 File Offset: 0x00089580
	public bool TryGetEnabledItemId(int heroId, out ulong publishedFileId)
	{
		publishedFileId = 0UL;
		for (int i = 0; i < this.data.enabledHeroMods.Count; i++)
		{
			LocalWorkshopHeroModEntry localWorkshopHeroModEntry = this.data.enabledHeroMods[i];
			if (localWorkshopHeroModEntry != null && localWorkshopHeroModEntry.heroId == heroId)
			{
				publishedFileId = localWorkshopHeroModEntry.publishedFileId;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x0008B3D8 File Offset: 0x000895D8
	public void SetEnabledItem(int heroId, ulong publishedFileId)
	{
		if (heroId <= 0 || publishedFileId == 0UL)
		{
			return;
		}
		this.DisableLocalFileHero(heroId);
		for (int i = 0; i < this.data.enabledHeroMods.Count; i++)
		{
			LocalWorkshopHeroModEntry localWorkshopHeroModEntry = this.data.enabledHeroMods[i];
			if (localWorkshopHeroModEntry != null && localWorkshopHeroModEntry.heroId == heroId)
			{
				localWorkshopHeroModEntry.publishedFileId = publishedFileId;
				this.Save();
				LocalHeroModelService.TryPreloadOverrideForHero(heroId);
				return;
			}
		}
		this.data.enabledHeroMods.Add(new LocalWorkshopHeroModEntry
		{
			heroId = heroId,
			publishedFileId = publishedFileId
		});
		this.Save();
		LocalHeroModelService.TryPreloadOverrideForHero(heroId);
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x0008B474 File Offset: 0x00089674
	public bool TryGetEnabledLocalFileItemRoot(int heroId, out string itemRoot)
	{
		itemRoot = string.Empty;
		for (int i = 0; i < this.data.enabledLocalFileMods.Count; i++)
		{
			LocalWorkshopLocalFileModEntry localWorkshopLocalFileModEntry = this.data.enabledLocalFileMods[i];
			if (localWorkshopLocalFileModEntry != null && localWorkshopLocalFileModEntry.heroId == heroId && !string.IsNullOrWhiteSpace(localWorkshopLocalFileModEntry.itemRoot))
			{
				itemRoot = localWorkshopLocalFileModEntry.itemRoot;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x0008B4DC File Offset: 0x000896DC
	public void SetEnabledLocalFileItem(int heroId, string itemRoot)
	{
		if (heroId <= 0 || string.IsNullOrWhiteSpace(itemRoot))
		{
			return;
		}
		this.DisableHero(heroId);
		for (int i = 0; i < this.data.enabledLocalFileMods.Count; i++)
		{
			LocalWorkshopLocalFileModEntry localWorkshopLocalFileModEntry = this.data.enabledLocalFileMods[i];
			if (localWorkshopLocalFileModEntry != null && localWorkshopLocalFileModEntry.heroId == heroId)
			{
				localWorkshopLocalFileModEntry.itemRoot = itemRoot;
				this.Save();
				LocalHeroModelService.TryPreloadOverrideForHero(heroId);
				return;
			}
		}
		this.data.enabledLocalFileMods.Add(new LocalWorkshopLocalFileModEntry
		{
			heroId = heroId,
			itemRoot = itemRoot
		});
		this.Save();
		LocalHeroModelService.TryPreloadOverrideForHero(heroId);
	}

	// Token: 0x06001667 RID: 5735 RVA: 0x0008B57C File Offset: 0x0008977C
	public void DisableHero(int heroId)
	{
		for (int i = this.data.enabledHeroMods.Count - 1; i >= 0; i--)
		{
			LocalWorkshopHeroModEntry localWorkshopHeroModEntry = this.data.enabledHeroMods[i];
			if (localWorkshopHeroModEntry != null && localWorkshopHeroModEntry.heroId == heroId)
			{
				this.data.enabledHeroMods.RemoveAt(i);
			}
		}
		for (int j = this.data.enabledLocalFileMods.Count - 1; j >= 0; j--)
		{
			LocalWorkshopLocalFileModEntry localWorkshopLocalFileModEntry = this.data.enabledLocalFileMods[j];
			if (localWorkshopLocalFileModEntry != null && localWorkshopLocalFileModEntry.heroId == heroId)
			{
				this.data.enabledLocalFileMods.RemoveAt(j);
			}
		}
		this.Save();
	}

	// Token: 0x06001668 RID: 5736 RVA: 0x0008B628 File Offset: 0x00089828
	public void DisableLocalFileHero(int heroId)
	{
		for (int i = this.data.enabledLocalFileMods.Count - 1; i >= 0; i--)
		{
			LocalWorkshopLocalFileModEntry localWorkshopLocalFileModEntry = this.data.enabledLocalFileMods[i];
			if (localWorkshopLocalFileModEntry != null && localWorkshopLocalFileModEntry.heroId == heroId)
			{
				this.data.enabledLocalFileMods.RemoveAt(i);
			}
		}
		this.Save();
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x0008B688 File Offset: 0x00089888
	public void DisableLocalFileItemRoot(string itemRoot)
	{
		if (string.IsNullOrWhiteSpace(itemRoot))
		{
			return;
		}
		for (int i = this.data.enabledLocalFileMods.Count - 1; i >= 0; i--)
		{
			LocalWorkshopLocalFileModEntry localWorkshopLocalFileModEntry = this.data.enabledLocalFileMods[i];
			if (localWorkshopLocalFileModEntry != null && string.Equals(localWorkshopLocalFileModEntry.itemRoot, itemRoot, StringComparison.OrdinalIgnoreCase))
			{
				this.data.enabledLocalFileMods.RemoveAt(i);
			}
		}
		this.Save();
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x0008B6F8 File Offset: 0x000898F8
	public void DisableItem(ulong publishedFileId)
	{
		for (int i = this.data.enabledHeroMods.Count - 1; i >= 0; i--)
		{
			LocalWorkshopHeroModEntry localWorkshopHeroModEntry = this.data.enabledHeroMods[i];
			if (localWorkshopHeroModEntry != null && localWorkshopHeroModEntry.publishedFileId == publishedFileId)
			{
				this.data.enabledHeroMods.RemoveAt(i);
			}
		}
		this.Save();
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x0008B758 File Offset: 0x00089958
	public bool IsEnabledItem(ulong publishedFileId)
	{
		for (int i = 0; i < this.data.enabledHeroMods.Count; i++)
		{
			LocalWorkshopHeroModEntry localWorkshopHeroModEntry = this.data.enabledHeroMods[i];
			if (localWorkshopHeroModEntry != null && localWorkshopHeroModEntry.publishedFileId == publishedFileId)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x0008B7A4 File Offset: 0x000899A4
	public bool IsEnabledItemForHero(int heroId, ulong publishedFileId)
	{
		ulong num;
		return this.TryGetEnabledItemId(heroId, out num) && num == publishedFileId;
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x0008B7C4 File Offset: 0x000899C4
	public bool IsEnabledLocalFileItemForHero(int heroId, string itemRoot)
	{
		string a;
		return this.TryGetEnabledLocalFileItemRoot(heroId, out a) && string.Equals(a, itemRoot, StringComparison.OrdinalIgnoreCase);
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x0008B7E8 File Offset: 0x000899E8
	public List<LocalWorkshopHeroModEntry> GetEnabledHeroModsSnapshot()
	{
		List<LocalWorkshopHeroModEntry> list = new List<LocalWorkshopHeroModEntry>();
		if (this.data == null || this.data.enabledHeroMods == null)
		{
			return list;
		}
		for (int i = 0; i < this.data.enabledHeroMods.Count; i++)
		{
			LocalWorkshopHeroModEntry localWorkshopHeroModEntry = this.data.enabledHeroMods[i];
			if (localWorkshopHeroModEntry != null && localWorkshopHeroModEntry.heroId > 0 && localWorkshopHeroModEntry.publishedFileId != 0UL)
			{
				list.Add(new LocalWorkshopHeroModEntry
				{
					heroId = localWorkshopHeroModEntry.heroId,
					publishedFileId = localWorkshopHeroModEntry.publishedFileId
				});
			}
		}
		return list;
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x0008B878 File Offset: 0x00089A78
	public List<LocalWorkshopLocalFileModEntry> GetEnabledLocalFileModsSnapshot()
	{
		List<LocalWorkshopLocalFileModEntry> list = new List<LocalWorkshopLocalFileModEntry>();
		if (this.data == null || this.data.enabledLocalFileMods == null)
		{
			return list;
		}
		for (int i = 0; i < this.data.enabledLocalFileMods.Count; i++)
		{
			LocalWorkshopLocalFileModEntry localWorkshopLocalFileModEntry = this.data.enabledLocalFileMods[i];
			if (localWorkshopLocalFileModEntry != null && localWorkshopLocalFileModEntry.heroId > 0 && !string.IsNullOrWhiteSpace(localWorkshopLocalFileModEntry.itemRoot))
			{
				list.Add(new LocalWorkshopLocalFileModEntry
				{
					heroId = localWorkshopLocalFileModEntry.heroId,
					itemRoot = localWorkshopLocalFileModEntry.itemRoot
				});
			}
		}
		return list;
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x0008B90C File Offset: 0x00089B0C
	public void OnApplicationQuit()
	{
		this.Save();
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x0008B914 File Offset: 0x00089B14
	private void Load()
	{
		if (!Game.Save.Check("LocalWorkshopModSettingsV1"))
		{
			this.data = new LocalWorkshopModSettingsData();
			return;
		}
		this.data = Game.Save.Load<LocalWorkshopModSettingsData>("LocalWorkshopModSettingsV1");
		if (this.data == null || this.data.enabledHeroMods == null)
		{
			this.data = new LocalWorkshopModSettingsData();
			return;
		}
		if (this.data.enabledLocalFileMods == null)
		{
			this.data.enabledLocalFileMods = new List<LocalWorkshopLocalFileModEntry>();
		}
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x0008B994 File Offset: 0x00089B94
	private void Save()
	{
		if (this.data == null)
		{
			this.data = new LocalWorkshopModSettingsData();
		}
		if (this.data.enabledHeroMods == null)
		{
			this.data.enabledHeroMods = new List<LocalWorkshopHeroModEntry>();
		}
		if (this.data.enabledLocalFileMods == null)
		{
			this.data.enabledLocalFileMods = new List<LocalWorkshopLocalFileModEntry>();
		}
		Game.Save.Save("LocalWorkshopModSettingsV1", this.data);
	}

	// Token: 0x0400150A RID: 5386
	private const string SaveKey = "LocalWorkshopModSettingsV1";

	// Token: 0x0400150B RID: 5387
	private LocalWorkshopModSettingsData data = new LocalWorkshopModSettingsData();
}
