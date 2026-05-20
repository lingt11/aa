using System;
using System.Collections.Generic;

// Token: 0x020003D5 RID: 981
public interface ISteamWorkshopService
{
	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x06001676 RID: 5750
	bool IsAvailable { get; }

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06001677 RID: 5751
	uint ConsumerAppId { get; }

	// Token: 0x06001678 RID: 5752
	void OpenWorkshopHome();

	// Token: 0x06001679 RID: 5753
	void OpenWorkshopItem(ulong publishedFileId);

	// Token: 0x0600167A RID: 5754
	void OpenWorkshopBrowseForHero(int heroId);

	// Token: 0x0600167B RID: 5755
	bool Subscribe(ulong publishedFileId);

	// Token: 0x0600167C RID: 5756
	bool Unsubscribe(ulong publishedFileId);

	// Token: 0x0600167D RID: 5757
	bool Download(ulong publishedFileId, bool highPriority = true);

	// Token: 0x0600167E RID: 5758
	List<WorkshopItemStatus> GetSubscribedItems();

	// Token: 0x0600167F RID: 5759
	bool TryGetInstalledItem(ulong publishedFileId, out WorkshopInstalledItem item);

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06001680 RID: 5760
	// (remove) Token: 0x06001681 RID: 5761
	event Action<WorkshopItemStatus> ItemSubscribed;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06001682 RID: 5762
	// (remove) Token: 0x06001683 RID: 5763
	event Action<ulong> ItemUnsubscribed;

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06001684 RID: 5764
	// (remove) Token: 0x06001685 RID: 5765
	event Action<WorkshopInstalledItem> ItemInstalledOrUpdated;

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06001686 RID: 5766
	// (remove) Token: 0x06001687 RID: 5767
	event Action<WorkshopDownloadProgress> ItemDownloadProgress;
}
