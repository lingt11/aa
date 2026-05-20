using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using UnityEngine;

// Token: 0x020003D9 RID: 985
public class SteamWorkshopService : ISteamWorkshopService, IUpdate
{
	// Token: 0x14000005 RID: 5
	// (add) Token: 0x0600168B RID: 5771 RVA: 0x0008BAFC File Offset: 0x00089CFC
	// (remove) Token: 0x0600168C RID: 5772 RVA: 0x0008BB34 File Offset: 0x00089D34
	public event Action<WorkshopItemStatus> ItemSubscribed;

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x0600168D RID: 5773 RVA: 0x0008BB6C File Offset: 0x00089D6C
	// (remove) Token: 0x0600168E RID: 5774 RVA: 0x0008BBA4 File Offset: 0x00089DA4
	public event Action<ulong> ItemUnsubscribed;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x0600168F RID: 5775 RVA: 0x0008BBDC File Offset: 0x00089DDC
	// (remove) Token: 0x06001690 RID: 5776 RVA: 0x0008BC14 File Offset: 0x00089E14
	public event Action<WorkshopInstalledItem> ItemInstalledOrUpdated;

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x06001691 RID: 5777 RVA: 0x0008BC4C File Offset: 0x00089E4C
	// (remove) Token: 0x06001692 RID: 5778 RVA: 0x0008BC84 File Offset: 0x00089E84
	public event Action<WorkshopDownloadProgress> ItemDownloadProgress;

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x06001693 RID: 5779 RVA: 0x0008BCBC File Offset: 0x00089EBC
	public bool IsAvailable
	{
		get
		{
			SteamManager steamManager = EntityStatic.Get<SteamManager>();
			return steamManager != null && steamManager.Initialized;
		}
	}

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x06001694 RID: 5780 RVA: 0x0008BCDA File Offset: 0x00089EDA
	public uint ConsumerAppId
	{
		get
		{
			return WorkshopAppIdConfig.GetEffectiveConsumerAppId(this.IsAvailable ? SteamUtils.GetAppID().m_AppId : 0U);
		}
	}

	// Token: 0x06001695 RID: 5781 RVA: 0x0008BCF8 File Offset: 0x00089EF8
	public SteamWorkshopService()
	{
		this.EnsureCallbacksRegistered();
	}

	// Token: 0x06001696 RID: 5782 RVA: 0x0008BD53 File Offset: 0x00089F53
	public void Update()
	{
		if (!this.IsAvailable)
		{
			return;
		}
		this.WarnIfConfiguredWorkshopAppIdDiffers();
		this.EnsureCallbacksRegistered();
		if (Time.unscaledTime < this.nextRefreshTime)
		{
			return;
		}
		this.nextRefreshTime = Time.unscaledTime + 1f;
		this.RefreshSubscribedItemsInternal(true);
	}

	// Token: 0x06001697 RID: 5783 RVA: 0x0008BD91 File Offset: 0x00089F91
	public void OpenWorkshopHome()
	{
		this.OpenWorkshopUrl(this.BuildWorkshopHomeUrl());
	}

	// Token: 0x06001698 RID: 5784 RVA: 0x0008BD9F File Offset: 0x00089F9F
	public void OpenWorkshopItem(ulong publishedFileId)
	{
		if (publishedFileId == 0UL)
		{
			return;
		}
		this.OpenWorkshopUrl(this.BuildWorkshopItemUrl(publishedFileId));
	}

	// Token: 0x06001699 RID: 5785 RVA: 0x0008BDB2 File Offset: 0x00089FB2
	public void OpenWorkshopBrowseForHero(int heroId)
	{
		this.OpenWorkshopUrl(this.BuildWorkshopHeroBrowseUrl(heroId));
	}

	// Token: 0x0600169A RID: 5786 RVA: 0x0008BDC4 File Offset: 0x00089FC4
	public bool Subscribe(ulong publishedFileId)
	{
		if (!this.IsAvailable || publishedFileId == 0UL)
		{
			return false;
		}
		SteamAPICall_t steamAPICall_t = SteamUGC.SubscribeItem(new PublishedFileId_t(publishedFileId));
		if (steamAPICall_t == SteamAPICall_t.Invalid)
		{
			return false;
		}
		this.subscribeItemResult.Set(steamAPICall_t, null);
		return true;
	}

	// Token: 0x0600169B RID: 5787 RVA: 0x0008BE08 File Offset: 0x0008A008
	public bool Unsubscribe(ulong publishedFileId)
	{
		if (!this.IsAvailable || publishedFileId == 0UL)
		{
			return false;
		}
		SteamAPICall_t steamAPICall_t = SteamUGC.UnsubscribeItem(new PublishedFileId_t(publishedFileId));
		if (steamAPICall_t == SteamAPICall_t.Invalid)
		{
			return false;
		}
		this.unsubscribeItemResult.Set(steamAPICall_t, null);
		return true;
	}

	// Token: 0x0600169C RID: 5788 RVA: 0x0008BE4C File Offset: 0x0008A04C
	public bool Download(ulong publishedFileId, bool highPriority = true)
	{
		if (!this.IsAvailable || publishedFileId == 0UL)
		{
			return false;
		}
		bool flag = SteamUGC.DownloadItem(new PublishedFileId_t(publishedFileId), highPriority);
		WorkshopItemStatus workshopItemStatus;
		if (flag && this.itemStatusCache.TryGetValue(publishedFileId, out workshopItemStatus) && workshopItemStatus != null)
		{
			workshopItemStatus.downloadPending = true;
		}
		return flag;
	}

	// Token: 0x0600169D RID: 5789 RVA: 0x0008BE8F File Offset: 0x0008A08F
	public List<WorkshopItemStatus> GetSubscribedItems()
	{
		return this.RefreshSubscribedItemsInternal(true);
	}

	// Token: 0x0600169E RID: 5790 RVA: 0x0008BE98 File Offset: 0x0008A098
	public bool TryGetInstalledItem(ulong publishedFileId, out WorkshopInstalledItem item)
	{
		item = null;
		if (!this.IsAvailable || publishedFileId == 0UL)
		{
			return false;
		}
		ulong sizeOnDisk;
		string text;
		uint timestamp;
		if (!SteamUGC.GetItemInstallInfo(new PublishedFileId_t(publishedFileId), out sizeOnDisk, out text, 2048U, out timestamp))
		{
			this.installedItemCache.Remove(publishedFileId);
			return false;
		}
		if (string.IsNullOrEmpty(text))
		{
			this.installedItemCache.Remove(publishedFileId);
			return false;
		}
		item = new WorkshopInstalledItem
		{
			publishedFileId = publishedFileId,
			installFolder = text,
			manifestPath = Path.Combine(text, "manifest.json"),
			sizeOnDisk = sizeOnDisk,
			timestamp = timestamp
		};
		LocalWorkshopManifest manifest;
		if (LocalWorkshopManifestLoader.TryLoad(text, out manifest))
		{
			item.manifest = manifest;
		}
		this.installedItemCache[publishedFileId] = item;
		return true;
	}

	// Token: 0x0600169F RID: 5791 RVA: 0x0008BF48 File Offset: 0x0008A148
	private void EnsureCallbacksRegistered()
	{
		if (this.callbacksRegistered || !this.IsAvailable)
		{
			return;
		}
		this.itemInstalledCallback = Callback<ItemInstalled_t>.Create(new Callback<ItemInstalled_t>.DispatchDelegate(this.OnItemInstalled));
		this.downloadItemResultCallback = Callback<DownloadItemResult_t>.Create(new Callback<DownloadItemResult_t>.DispatchDelegate(this.OnDownloadItemResult));
		this.remoteSubscribedCallback = Callback<RemoteStoragePublishedFileSubscribed_t>.Create(new Callback<RemoteStoragePublishedFileSubscribed_t>.DispatchDelegate(this.OnRemoteSubscribed));
		this.remoteUnsubscribedCallback = Callback<RemoteStoragePublishedFileUnsubscribed_t>.Create(new Callback<RemoteStoragePublishedFileUnsubscribed_t>.DispatchDelegate(this.OnRemoteUnsubscribed));
		this.subscribeItemResult = CallResult<RemoteStorageSubscribePublishedFileResult_t>.Create(new CallResult<RemoteStorageSubscribePublishedFileResult_t>.APIDispatchDelegate(this.OnSubscribeItemResult));
		this.unsubscribeItemResult = CallResult<RemoteStorageUnsubscribePublishedFileResult_t>.Create(new CallResult<RemoteStorageUnsubscribePublishedFileResult_t>.APIDispatchDelegate(this.OnUnsubscribeItemResult));
		this.queryDetailsResult = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnQueryDetailsCompleted));
		this.callbacksRegistered = true;
	}

	// Token: 0x060016A0 RID: 5792 RVA: 0x0008C010 File Offset: 0x0008A210
	private List<WorkshopItemStatus> RefreshSubscribedItemsInternal(bool requestMissingDetails)
	{
		List<WorkshopItemStatus> list = new List<WorkshopItemStatus>();
		if (!this.IsAvailable)
		{
			return list;
		}
		uint numSubscribedItems = SteamUGC.GetNumSubscribedItems();
		if (numSubscribedItems == 0U)
		{
			this.itemStatusCache.Clear();
			this.installedItemCache.Clear();
			this.downloadProgressCache.Clear();
			this.SyncEnabledSettingsWithSubscribedItems(new HashSet<ulong>());
			return list;
		}
		PublishedFileId_t[] array = new PublishedFileId_t[numSubscribedItems];
		uint subscribedItems = SteamUGC.GetSubscribedItems(array, numSubscribedItems);
		HashSet<ulong> hashSet = new HashSet<ulong>();
		List<PublishedFileId_t> list2 = new List<PublishedFileId_t>();
		int num = 0;
		while ((long)num < (long)((ulong)subscribedItems))
		{
			ulong publishedFileId = array[num].m_PublishedFileId;
			hashSet.Add(publishedFileId);
			WorkshopItemStatus workshopItemStatus = this.BuildItemStatus(publishedFileId);
			this.TryDownloadSubscribedItem(workshopItemStatus);
			list.Add(workshopItemStatus);
			if (this.ShouldRequestDetails(publishedFileId))
			{
				list2.Add(new PublishedFileId_t(publishedFileId));
			}
			num++;
		}
		this.RemoveStaleItems(hashSet);
		this.SyncEnabledSettingsWithSubscribedItems(hashSet);
		if (requestMissingDetails && list2.Count > 0 && this.activeQueryHandle == UGCQueryHandle_t.Invalid)
		{
			this.RequestDetails(list2);
		}
		return list;
	}

	// Token: 0x060016A1 RID: 5793 RVA: 0x0008C118 File Offset: 0x0008A318
	private WorkshopItemStatus BuildItemStatus(ulong publishedFileId)
	{
		WorkshopItemStatus workshopItemStatus;
		if (!this.itemStatusCache.TryGetValue(publishedFileId, out workshopItemStatus) || workshopItemStatus == null)
		{
			workshopItemStatus = new WorkshopItemStatus
			{
				publishedFileId = publishedFileId
			};
			this.itemStatusCache[publishedFileId] = workshopItemStatus;
		}
		uint itemState = SteamUGC.GetItemState(new PublishedFileId_t(publishedFileId));
		workshopItemStatus.subscribed = SteamWorkshopService.HasItemState(itemState, EItemState.k_EItemStateSubscribed);
		workshopItemStatus.installed = SteamWorkshopService.HasItemState(itemState, EItemState.k_EItemStateInstalled);
		workshopItemStatus.needsUpdate = SteamWorkshopService.HasItemState(itemState, EItemState.k_EItemStateNeedsUpdate);
		workshopItemStatus.downloading = SteamWorkshopService.HasItemState(itemState, EItemState.k_EItemStateDownloading);
		workshopItemStatus.downloadPending = SteamWorkshopService.HasItemState(itemState, EItemState.k_EItemStateDownloadPending);
		WorkshopInstalledItem workshopInstalledItem;
		if (this.TryGetInstalledItem(publishedFileId, out workshopInstalledItem))
		{
			workshopItemStatus.installFolder = workshopInstalledItem.installFolder;
			workshopItemStatus.sizeOnDisk = workshopInstalledItem.sizeOnDisk;
			workshopItemStatus.timestamp = workshopInstalledItem.timestamp;
			if (workshopInstalledItem.manifest != null)
			{
				if (!string.IsNullOrEmpty(workshopInstalledItem.manifest.title))
				{
					workshopItemStatus.title = workshopInstalledItem.manifest.title;
				}
				workshopItemStatus.heroId = workshopInstalledItem.manifest.heroId;
			}
		}
		else
		{
			workshopItemStatus.installFolder = string.Empty;
			workshopItemStatus.sizeOnDisk = 0UL;
			workshopItemStatus.timestamp = 0U;
		}
		SteamUGCDetails_t steamUGCDetails_t;
		if (this.detailCache.TryGetValue(publishedFileId, out steamUGCDetails_t))
		{
			if (string.IsNullOrEmpty(workshopItemStatus.title))
			{
				workshopItemStatus.title = steamUGCDetails_t.m_rgchTitle;
			}
			if (workshopItemStatus.heroId <= 0)
			{
				workshopItemStatus.heroId = SteamWorkshopService.ParseHeroIdFromTags(steamUGCDetails_t.m_rgchTags);
			}
			workshopItemStatus.remoteTimestamp = steamUGCDetails_t.m_rtimeUpdated;
		}
		else
		{
			workshopItemStatus.remoteTimestamp = 0U;
		}
		workshopItemStatus.updateAvailable = (workshopItemStatus.needsUpdate || (workshopItemStatus.installed && workshopItemStatus.remoteTimestamp > 0U && workshopItemStatus.timestamp > 0U && workshopItemStatus.remoteTimestamp > workshopItemStatus.timestamp));
		workshopItemStatus.needsUpdate = workshopItemStatus.updateAvailable;
		workshopItemStatus.enabledInGame = this.ComputeEnabledState(workshopItemStatus);
		this.UpdateDownloadProgress(workshopItemStatus);
		return workshopItemStatus;
	}

	// Token: 0x060016A2 RID: 5794 RVA: 0x0008C2E0 File Offset: 0x0008A4E0
	private void UpdateDownloadProgress(WorkshopItemStatus status)
	{
		if (status == null || status.publishedFileId == 0UL)
		{
			return;
		}
		if (!status.downloading && !status.downloadPending && !status.needsUpdate)
		{
			this.downloadProgressCache.Remove(status.publishedFileId);
			return;
		}
		ulong num;
		ulong num2;
		if (!SteamUGC.GetItemDownloadInfo(new PublishedFileId_t(status.publishedFileId), out num, out num2))
		{
			return;
		}
		WorkshopDownloadProgress workshopDownloadProgress;
		if (this.downloadProgressCache.TryGetValue(status.publishedFileId, out workshopDownloadProgress) && workshopDownloadProgress.bytesDownloaded == num && workshopDownloadProgress.bytesTotal == num2)
		{
			return;
		}
		WorkshopDownloadProgress workshopDownloadProgress2 = new WorkshopDownloadProgress
		{
			publishedFileId = status.publishedFileId,
			bytesDownloaded = num,
			bytesTotal = num2
		};
		this.downloadProgressCache[status.publishedFileId] = workshopDownloadProgress2;
		Action<WorkshopDownloadProgress> itemDownloadProgress = this.ItemDownloadProgress;
		if (itemDownloadProgress == null)
		{
			return;
		}
		itemDownloadProgress(workshopDownloadProgress2);
	}

	// Token: 0x060016A3 RID: 5795 RVA: 0x0008C3A8 File Offset: 0x0008A5A8
	private bool ComputeEnabledState(WorkshopItemStatus status)
	{
		LocalWorkshopModSettings localWorkshopModSettings = EntityStatic.Get<LocalWorkshopModSettings>();
		if (localWorkshopModSettings == null)
		{
			return false;
		}
		if (status.heroId > 0)
		{
			return localWorkshopModSettings.IsEnabledItemForHero(status.heroId, status.publishedFileId);
		}
		return localWorkshopModSettings.IsEnabledItem(status.publishedFileId);
	}

	// Token: 0x060016A4 RID: 5796 RVA: 0x0008C3E8 File Offset: 0x0008A5E8
	private void RequestDetails(List<PublishedFileId_t> missingDetailIds)
	{
		if (missingDetailIds == null || missingDetailIds.Count == 0)
		{
			return;
		}
		float value = Time.unscaledTime + 30f;
		for (int i = 0; i < missingDetailIds.Count; i++)
		{
			this.detailRefreshTimeCache[missingDetailIds[i].m_PublishedFileId] = value;
		}
		this.activeQueryHandle = SteamUGC.CreateQueryUGCDetailsRequest(missingDetailIds.ToArray(), (uint)missingDetailIds.Count);
		if (this.activeQueryHandle == UGCQueryHandle_t.Invalid)
		{
			return;
		}
		SteamUGC.SetReturnMetadata(this.activeQueryHandle, true);
		SteamAPICall_t steamAPICall_t = SteamUGC.SendQueryUGCRequest(this.activeQueryHandle);
		if (steamAPICall_t == SteamAPICall_t.Invalid)
		{
			SteamUGC.ReleaseQueryUGCRequest(this.activeQueryHandle);
			this.activeQueryHandle = UGCQueryHandle_t.Invalid;
			return;
		}
		this.queryDetailsResult.Set(steamAPICall_t, null);
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x0008C4AC File Offset: 0x0008A6AC
	private bool ShouldRequestDetails(ulong publishedFileId)
	{
		float num;
		return publishedFileId != 0UL && (!this.detailCache.ContainsKey(publishedFileId) || !this.detailRefreshTimeCache.TryGetValue(publishedFileId, out num) || Time.unscaledTime >= num);
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x0008C4EC File Offset: 0x0008A6EC
	private void RemoveStaleItems(HashSet<ulong> activeIds)
	{
		List<ulong> list = new List<ulong>();
		foreach (KeyValuePair<ulong, WorkshopItemStatus> keyValuePair in this.itemStatusCache)
		{
			if (!activeIds.Contains(keyValuePair.Key))
			{
				list.Add(keyValuePair.Key);
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			ulong key = list[i];
			this.itemStatusCache.Remove(key);
			this.installedItemCache.Remove(key);
			this.detailCache.Remove(key);
			this.detailRefreshTimeCache.Remove(key);
			this.downloadProgressCache.Remove(key);
		}
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x0008C5BC File Offset: 0x0008A7BC
	private void OnSubscribeItemResult(RemoteStorageSubscribePublishedFileResult_t callback, bool ioFailure)
	{
		if (ioFailure || callback.m_eResult != EResult.k_EResultOK)
		{
			return;
		}
		ulong publishedFileId = callback.m_nPublishedFileId.m_PublishedFileId;
		WorkshopItemStatus workshopItemStatus = this.BuildItemStatus(publishedFileId);
		this.TryDownloadSubscribedItem(workshopItemStatus);
		Action<WorkshopItemStatus> itemSubscribed = this.ItemSubscribed;
		if (itemSubscribed == null)
		{
			return;
		}
		itemSubscribed(workshopItemStatus);
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x0008C604 File Offset: 0x0008A804
	private void OnUnsubscribeItemResult(RemoteStorageUnsubscribePublishedFileResult_t callback, bool ioFailure)
	{
		if (ioFailure || callback.m_eResult != EResult.k_EResultOK)
		{
			return;
		}
		ulong publishedFileId = callback.m_nPublishedFileId.m_PublishedFileId;
		this.HandleUnsubscribedItem(publishedFileId);
		this.itemStatusCache.Remove(publishedFileId);
		this.installedItemCache.Remove(publishedFileId);
		this.detailCache.Remove(publishedFileId);
		this.detailRefreshTimeCache.Remove(publishedFileId);
		this.downloadProgressCache.Remove(publishedFileId);
		Action<ulong> itemUnsubscribed = this.ItemUnsubscribed;
		if (itemUnsubscribed == null)
		{
			return;
		}
		itemUnsubscribed(publishedFileId);
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x0008C684 File Offset: 0x0008A884
	private void OnRemoteSubscribed(RemoteStoragePublishedFileSubscribed_t callback)
	{
		if (!this.IsCurrentApp(callback.m_nAppID))
		{
			return;
		}
		ulong publishedFileId = callback.m_nPublishedFileId.m_PublishedFileId;
		WorkshopItemStatus workshopItemStatus = this.BuildItemStatus(publishedFileId);
		this.TryDownloadSubscribedItem(workshopItemStatus);
		Action<WorkshopItemStatus> itemSubscribed = this.ItemSubscribed;
		if (itemSubscribed == null)
		{
			return;
		}
		itemSubscribed(workshopItemStatus);
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x0008C6CC File Offset: 0x0008A8CC
	private void OnRemoteUnsubscribed(RemoteStoragePublishedFileUnsubscribed_t callback)
	{
		if (!this.IsCurrentApp(callback.m_nAppID))
		{
			return;
		}
		ulong publishedFileId = callback.m_nPublishedFileId.m_PublishedFileId;
		this.HandleUnsubscribedItem(publishedFileId);
		this.itemStatusCache.Remove(publishedFileId);
		this.installedItemCache.Remove(publishedFileId);
		this.detailCache.Remove(publishedFileId);
		this.detailRefreshTimeCache.Remove(publishedFileId);
		this.downloadProgressCache.Remove(publishedFileId);
		Action<ulong> itemUnsubscribed = this.ItemUnsubscribed;
		if (itemUnsubscribed == null)
		{
			return;
		}
		itemUnsubscribed(publishedFileId);
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x0008C750 File Offset: 0x0008A950
	private void OnItemInstalled(ItemInstalled_t callback)
	{
		if (!this.IsCurrentApp(callback.m_unAppID))
		{
			return;
		}
		WorkshopInstalledItem workshopInstalledItem;
		if (this.TryGetInstalledItem(callback.m_nPublishedFileId.m_PublishedFileId, out workshopInstalledItem))
		{
			this.detailRefreshTimeCache.Remove(callback.m_nPublishedFileId.m_PublishedFileId);
			this.BuildItemStatus(callback.m_nPublishedFileId.m_PublishedFileId);
			this.ApplyInstalledItemToGameplay(workshopInstalledItem);
			Action<WorkshopInstalledItem> itemInstalledOrUpdated = this.ItemInstalledOrUpdated;
			if (itemInstalledOrUpdated == null)
			{
				return;
			}
			itemInstalledOrUpdated(workshopInstalledItem);
		}
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x0008C7C4 File Offset: 0x0008A9C4
	private void OnDownloadItemResult(DownloadItemResult_t callback)
	{
		if (!this.IsCurrentApp(callback.m_unAppID) || callback.m_eResult != EResult.k_EResultOK)
		{
			return;
		}
		WorkshopInstalledItem workshopInstalledItem;
		if (this.TryGetInstalledItem(callback.m_nPublishedFileId.m_PublishedFileId, out workshopInstalledItem))
		{
			this.detailRefreshTimeCache.Remove(callback.m_nPublishedFileId.m_PublishedFileId);
			this.BuildItemStatus(callback.m_nPublishedFileId.m_PublishedFileId);
			this.ApplyInstalledItemToGameplay(workshopInstalledItem);
			Action<WorkshopInstalledItem> itemInstalledOrUpdated = this.ItemInstalledOrUpdated;
			if (itemInstalledOrUpdated == null)
			{
				return;
			}
			itemInstalledOrUpdated(workshopInstalledItem);
		}
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x0008C840 File Offset: 0x0008AA40
	private void OnQueryDetailsCompleted(SteamUGCQueryCompleted_t callback, bool ioFailure)
	{
		if (ioFailure || callback.m_eResult != EResult.k_EResultOK)
		{
			this.ReleaseActiveQuery();
			return;
		}
		for (uint num = 0U; num < callback.m_unNumResultsReturned; num += 1U)
		{
			SteamUGCDetails_t steamUGCDetails_t;
			if (SteamUGC.GetQueryUGCResult(callback.m_handle, num, out steamUGCDetails_t))
			{
				this.detailCache[steamUGCDetails_t.m_nPublishedFileId.m_PublishedFileId] = steamUGCDetails_t;
			}
		}
		this.ReleaseActiveQuery();
		this.RefreshSubscribedItemsInternal(false);
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x0008C8A6 File Offset: 0x0008AAA6
	private void TryDownloadSubscribedItem(WorkshopItemStatus status)
	{
		if (status == null || status.publishedFileId == 0UL || status.downloading || status.downloadPending)
		{
			return;
		}
		if (status.installed)
		{
			return;
		}
		this.Download(status.publishedFileId, true);
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x0008C8DC File Offset: 0x0008AADC
	private void ApplyInstalledItemToGameplay(WorkshopInstalledItem item)
	{
		if (item == null || item.manifest == null)
		{
			return;
		}
		LocalWorkshopModSettings localWorkshopModSettings = EntityStatic.Get<LocalWorkshopModSettings>();
		if (localWorkshopModSettings == null || !localWorkshopModSettings.IsEnabledItemForHero(item.manifest.heroId, item.publishedFileId))
		{
			return;
		}
		LocalHeroModelService.TryPreloadOverrideForHero(item.manifest.heroId);
		LocalHeroModelService.RefreshLocalPlayerOverrideForHero(item.manifest.heroId);
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x0008C93C File Offset: 0x0008AB3C
	private void HandleUnsubscribedItem(ulong publishedFileId)
	{
		LocalWorkshopModSettings localWorkshopModSettings = EntityStatic.Get<LocalWorkshopModSettings>();
		if (localWorkshopModSettings == null || publishedFileId == 0UL)
		{
			return;
		}
		int num = (int)((GameHelperClient.localPlayer != null) ? GameHelperClient.localPlayer.heroType : HeroType.None);
		bool flag = num > 0 && localWorkshopModSettings.IsEnabledItemForHero(num, publishedFileId);
		localWorkshopModSettings.DisableItem(publishedFileId);
		if (flag)
		{
			LocalHeroModelService.RefreshLocalPlayerOverrideForHero(num);
		}
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x0008C994 File Offset: 0x0008AB94
	private void SyncEnabledSettingsWithSubscribedItems(HashSet<ulong> activeIds)
	{
		LocalWorkshopModSettings localWorkshopModSettings = EntityStatic.Get<LocalWorkshopModSettings>();
		if (localWorkshopModSettings == null)
		{
			return;
		}
		List<LocalWorkshopHeroModEntry> enabledHeroModsSnapshot = localWorkshopModSettings.GetEnabledHeroModsSnapshot();
		if (enabledHeroModsSnapshot.Count == 0)
		{
			return;
		}
		int num = (int)((GameHelperClient.localPlayer != null) ? GameHelperClient.localPlayer.heroType : HeroType.None);
		bool flag = false;
		for (int i = 0; i < enabledHeroModsSnapshot.Count; i++)
		{
			LocalWorkshopHeroModEntry localWorkshopHeroModEntry = enabledHeroModsSnapshot[i];
			if (localWorkshopHeroModEntry != null && !activeIds.Contains(localWorkshopHeroModEntry.publishedFileId))
			{
				localWorkshopModSettings.DisableItem(localWorkshopHeroModEntry.publishedFileId);
				if (localWorkshopHeroModEntry.heroId == num)
				{
					flag = true;
				}
			}
		}
		if (flag && num > 0)
		{
			LocalHeroModelService.RefreshLocalPlayerOverrideForHero(num);
		}
	}

	// Token: 0x060016B2 RID: 5810 RVA: 0x0008CA31 File Offset: 0x0008AC31
	private void ReleaseActiveQuery()
	{
		if (this.activeQueryHandle != UGCQueryHandle_t.Invalid)
		{
			SteamUGC.ReleaseQueryUGCRequest(this.activeQueryHandle);
			this.activeQueryHandle = UGCQueryHandle_t.Invalid;
		}
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x0008CA5C File Offset: 0x0008AC5C
	private bool IsCurrentApp(AppId_t appId)
	{
		return this.IsAvailable && appId == SteamUtils.GetAppID();
	}

	// Token: 0x060016B4 RID: 5812 RVA: 0x0008CA74 File Offset: 0x0008AC74
	private void WarnIfConfiguredWorkshopAppIdDiffers()
	{
		if (this.loggedExternalWorkshopAppIdWarning || !this.IsAvailable)
		{
			return;
		}
		uint configuredConsumerAppId = WorkshopAppIdConfig.GetConfiguredConsumerAppId();
		uint appId = SteamUtils.GetAppID().m_AppId;
		if (configuredConsumerAppId == 0U || configuredConsumerAppId == appId)
		{
			return;
		}
		this.loggedExternalWorkshopAppIdWarning = true;
		Debug.LogWarning(string.Concat(new string[]
		{
			"SteamWorkshopService: 已配置创意工坊目标 AppID = ",
			configuredConsumerAppId.ToString(),
			"，当前运行中的 Steam AppID = ",
			appId.ToString(),
			"。游戏内打开的创意工坊网页会使用目标 AppID，但 Steam 原生订阅枚举/安装回调仍受当前运行 App 上下文限制。"
		}));
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x0008CAEE File Offset: 0x0008ACEE
	private static bool HasItemState(uint rawState, EItemState itemState)
	{
		return (rawState & (uint)itemState) > 0U;
	}

	// Token: 0x060016B6 RID: 5814 RVA: 0x0008CAF8 File Offset: 0x0008ACF8
	private static int ParseHeroIdFromTags(string tags)
	{
		if (string.IsNullOrEmpty(tags))
		{
			return 0;
		}
		string[] array = tags.Split(',', StringSplitOptions.None);
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i].Trim();
			int result;
			if (text.StartsWith("hero:", StringComparison.OrdinalIgnoreCase) && int.TryParse(text.Substring(5), out result))
			{
				return result;
			}
		}
		return 0;
	}

	// Token: 0x060016B7 RID: 5815 RVA: 0x0008CB51 File Offset: 0x0008AD51
	private void OpenWorkshopUrl(string url)
	{
		if (string.IsNullOrEmpty(url))
		{
			Debug.LogError("SteamWorkshopService: 无法打开创意工坊，未配置有效的 Workshop AppID。");
			return;
		}
		Debug.Log("SteamWorkshopService: Open workshop url: " + url);
		if (this.IsAvailable)
		{
			SteamFriends.ActivateGameOverlayToWebPage(url, EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default);
			return;
		}
		Application.OpenURL(url);
	}

	// Token: 0x060016B8 RID: 5816 RVA: 0x0008CB8C File Offset: 0x0008AD8C
	private string BuildWorkshopHomeUrl()
	{
		return this.BuildWorkshopBrowseUrl(null);
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x0008CB95 File Offset: 0x0008AD95
	private string BuildWorkshopItemUrl(ulong publishedFileId)
	{
		return "https://steamcommunity.com/sharedfiles/filedetails/?id=" + publishedFileId.ToString();
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x0008CBA8 File Offset: 0x0008ADA8
	private string BuildWorkshopHeroBrowseUrl(int heroId)
	{
		if (heroId <= 0)
		{
			return this.BuildWorkshopHomeUrl();
		}
		return this.BuildWorkshopBrowseUrl("hero:" + heroId.ToString());
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x0008CBCC File Offset: 0x0008ADCC
	private string BuildWorkshopBrowseUrl(string searchText)
	{
		uint consumerAppId = this.ConsumerAppId;
		if (consumerAppId == 0U)
		{
			return string.Empty;
		}
		string text = "https://steamcommunity.com/workshop/browse/?appid=" + consumerAppId.ToString();
		if (!string.IsNullOrEmpty(searchText))
		{
			text = text + "&searchtext=" + Uri.EscapeDataString(searchText);
		}
		return text;
	}

	// Token: 0x04001525 RID: 5413
	private const float RefreshInterval = 1f;

	// Token: 0x04001526 RID: 5414
	private const float DetailRefreshInterval = 30f;

	// Token: 0x04001527 RID: 5415
	private const uint InstallPathBufferSize = 2048U;

	// Token: 0x04001528 RID: 5416
	private readonly Dictionary<ulong, WorkshopItemStatus> itemStatusCache = new Dictionary<ulong, WorkshopItemStatus>();

	// Token: 0x04001529 RID: 5417
	private readonly Dictionary<ulong, WorkshopInstalledItem> installedItemCache = new Dictionary<ulong, WorkshopInstalledItem>();

	// Token: 0x0400152A RID: 5418
	private readonly Dictionary<ulong, SteamUGCDetails_t> detailCache = new Dictionary<ulong, SteamUGCDetails_t>();

	// Token: 0x0400152B RID: 5419
	private readonly Dictionary<ulong, float> detailRefreshTimeCache = new Dictionary<ulong, float>();

	// Token: 0x0400152C RID: 5420
	private readonly Dictionary<ulong, WorkshopDownloadProgress> downloadProgressCache = new Dictionary<ulong, WorkshopDownloadProgress>();

	// Token: 0x0400152D RID: 5421
	private Callback<ItemInstalled_t> itemInstalledCallback;

	// Token: 0x0400152E RID: 5422
	private Callback<DownloadItemResult_t> downloadItemResultCallback;

	// Token: 0x0400152F RID: 5423
	private Callback<RemoteStoragePublishedFileSubscribed_t> remoteSubscribedCallback;

	// Token: 0x04001530 RID: 5424
	private Callback<RemoteStoragePublishedFileUnsubscribed_t> remoteUnsubscribedCallback;

	// Token: 0x04001531 RID: 5425
	private CallResult<RemoteStorageSubscribePublishedFileResult_t> subscribeItemResult;

	// Token: 0x04001532 RID: 5426
	private CallResult<RemoteStorageUnsubscribePublishedFileResult_t> unsubscribeItemResult;

	// Token: 0x04001533 RID: 5427
	private CallResult<SteamUGCQueryCompleted_t> queryDetailsResult;

	// Token: 0x04001534 RID: 5428
	private bool callbacksRegistered;

	// Token: 0x04001535 RID: 5429
	private bool loggedExternalWorkshopAppIdWarning;

	// Token: 0x04001536 RID: 5430
	private float nextRefreshTime;

	// Token: 0x04001537 RID: 5431
	private UGCQueryHandle_t activeQueryHandle = UGCQueryHandle_t.Invalid;
}
