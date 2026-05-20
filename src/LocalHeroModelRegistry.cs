using System;
using UnityEngine;

// Token: 0x020003C8 RID: 968
public static class LocalHeroModelRegistry
{
	// Token: 0x0600162E RID: 5678 RVA: 0x00089BD4 File Offset: 0x00087DD4
	public static void SetItemRoot(int heroId, string itemRoot)
	{
		if (heroId < 1)
		{
			return;
		}
		string key = LocalHeroModelRegistry.GetKey(heroId);
		if (string.IsNullOrWhiteSpace(itemRoot))
		{
			PlayerPrefs.DeleteKey(key);
		}
		else
		{
			PlayerPrefs.SetString(key, itemRoot);
		}
		PlayerPrefs.Save();
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x00089C0C File Offset: 0x00087E0C
	public static bool TryGetItemRoot(int heroId, out string itemRoot)
	{
		itemRoot = string.Empty;
		if (heroId < 1)
		{
			return false;
		}
		string @string = PlayerPrefs.GetString(LocalHeroModelRegistry.GetKey(heroId), string.Empty);
		if (string.IsNullOrWhiteSpace(@string))
		{
			return false;
		}
		itemRoot = @string;
		return true;
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x00089C45 File Offset: 0x00087E45
	public static void Clear(int heroId)
	{
		if (heroId < 1)
		{
			return;
		}
		PlayerPrefs.DeleteKey(LocalHeroModelRegistry.GetKey(heroId));
		PlayerPrefs.Save();
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x00089C5C File Offset: 0x00087E5C
	private static string GetKey(int heroId)
	{
		return "Workshop.LocalHeroModel." + heroId.ToString();
	}

	// Token: 0x040014DA RID: 5338
	private const string PrefKeyPrefix = "Workshop.LocalHeroModel.";
}
