using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020003C2 RID: 962
public class UI_WorkshopMods : UGUICtrl
{
	// Token: 0x060015F2 RID: 5618 RVA: 0x0008816A File Offset: 0x0008636A
	public UI_WorkshopMods()
	{
		this.selfView = new UI_WorkshopMods_View();
		base.OnCreate(this.selfView, "UI/Prefabs/UI_WorkshopMods", base.GetType());
	}

	// Token: 0x060015F3 RID: 5619 RVA: 0x000881A0 File Offset: 0x000863A0
	protected override void ButtonAddClick()
	{
		this.selfView.btn_back.AddButtonEvent(new UnityAction(this.OnBackClick));
		this.selfView.btn_refresh.AddButtonEvent(new UnityAction(this.RefreshView));
		this.selfView.btn_openWorkshop.AddButtonEvent(new UnityAction(this.OnOpenWorkshopClick));
		this.selfView.btn_openHeroWorkshop.AddButtonEvent(new UnityAction(this.OnOpenHeroWorkshopClick));
	}

	// Token: 0x060015F4 RID: 5620 RVA: 0x0008821D File Offset: 0x0008641D
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.filterHeroId = UI_WorkshopMods.ResolveFilterHeroId(data);
		this.RegisterWorkshopEvents();
		this.RefreshView();
	}

	// Token: 0x060015F5 RID: 5621 RVA: 0x0008823E File Offset: 0x0008643E
	protected override void ClosePanel()
	{
		base.ClosePanel();
		this.UnregisterWorkshopEvents();
	}

	// Token: 0x060015F6 RID: 5622 RVA: 0x0008824C File Offset: 0x0008644C
	public override void Update()
	{
		base.Update();
		if (!this.isOpen)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			base.CloseSelfPanel();
			return;
		}
		if (Time.unscaledTime >= this.nextRefreshTime)
		{
			this.nextRefreshTime = Time.unscaledTime + 1f;
			this.RefreshView();
		}
	}

	// Token: 0x060015F7 RID: 5623 RVA: 0x0008829C File Offset: 0x0008649C
	public void OnEnableItemClicked(WorkshopItemStatus status)
	{
		if (status == null)
		{
			return;
		}
		if (!status.installed)
		{
			this.OnDownloadItemClicked(status);
			Util.ShowTips("创意工坊内容尚未安装，已尝试开始下载。");
			return;
		}
		if (status.heroId <= 0)
		{
			Util.ShowTips("该创意工坊内容缺少 heroId 配置，无法启用。");
			return;
		}
		LocalWorkshopModSettings localWorkshopModSettings = Game.LocalWorkshopModSettings;
		if (localWorkshopModSettings != null)
		{
			localWorkshopModSettings.SetEnabledItem(status.heroId, status.publishedFileId);
		}
		LocalHeroModelService.RefreshLocalPlayerOverrideForHero(status.heroId);
		this.RefreshView();
	}

	// Token: 0x060015F8 RID: 5624 RVA: 0x00088309 File Offset: 0x00086509
	public void OnDisableItemClicked(WorkshopItemStatus status)
	{
		if (status == null || status.heroId <= 0)
		{
			return;
		}
		LocalWorkshopModSettings localWorkshopModSettings = Game.LocalWorkshopModSettings;
		if (localWorkshopModSettings != null)
		{
			localWorkshopModSettings.DisableHero(status.heroId);
		}
		LocalHeroModelService.RefreshLocalPlayerOverrideForHero(status.heroId);
		this.RefreshView();
	}

	// Token: 0x060015F9 RID: 5625 RVA: 0x00088340 File Offset: 0x00086540
	public void OnDownloadItemClicked(WorkshopItemStatus status)
	{
		if (status == null)
		{
			return;
		}
		SteamWorkshopService steamWorkshopService = Game.SteamWorkshopService;
		if (steamWorkshopService == null || !steamWorkshopService.IsAvailable)
		{
			Util.ShowTips("Steam 未初始化，无法开始下载。");
			return;
		}
		if (!steamWorkshopService.Download(status.publishedFileId, true))
		{
			Util.ShowTips("创意工坊下载请求失败。");
		}
		this.RefreshView();
	}

	// Token: 0x060015FA RID: 5626 RVA: 0x0008838C File Offset: 0x0008658C
	public void OnOpenItemClicked(WorkshopItemStatus status)
	{
		if (status == null)
		{
			return;
		}
		SteamWorkshopService steamWorkshopService = Game.SteamWorkshopService;
		if (steamWorkshopService == null)
		{
			return;
		}
		steamWorkshopService.OpenWorkshopItem(status.publishedFileId);
	}

	// Token: 0x060015FB RID: 5627 RVA: 0x000883A8 File Offset: 0x000865A8
	public void OnUnsubscribeItemClicked(WorkshopItemStatus status)
	{
		if (status == null)
		{
			return;
		}
		ulong num;
		bool flag = GameHelperClient.localPlayer != null && Game.LocalWorkshopModSettings != null && Game.LocalWorkshopModSettings.TryGetEnabledItemId((int)GameHelperClient.localPlayer.heroType, out num) && num == status.publishedFileId;
		LocalWorkshopModSettings localWorkshopModSettings = Game.LocalWorkshopModSettings;
		if (localWorkshopModSettings != null)
		{
			localWorkshopModSettings.DisableItem(status.publishedFileId);
		}
		if (flag && GameHelperClient.localPlayer != null)
		{
			LocalHeroModelService.RefreshLocalPlayerOverrideForHero((int)GameHelperClient.localPlayer.heroType);
		}
		SteamWorkshopService steamWorkshopService = Game.SteamWorkshopService;
		if (steamWorkshopService != null)
		{
			steamWorkshopService.Unsubscribe(status.publishedFileId);
		}
		this.RefreshView();
	}

	// Token: 0x060015FC RID: 5628 RVA: 0x00088443 File Offset: 0x00086643
	private void OnBackClick()
	{
		Game.UI.CloseUI<UI_WorkshopMods>();
	}

	// Token: 0x060015FD RID: 5629 RVA: 0x0008844F File Offset: 0x0008664F
	private void OnOpenWorkshopClick()
	{
		SteamWorkshopService steamWorkshopService = Game.SteamWorkshopService;
		if (steamWorkshopService == null)
		{
			return;
		}
		steamWorkshopService.OpenWorkshopHome();
	}

	// Token: 0x060015FE RID: 5630 RVA: 0x00088460 File Offset: 0x00086660
	private void OnOpenHeroWorkshopClick()
	{
		int browseHeroId = this.GetBrowseHeroId();
		if (browseHeroId > 0)
		{
			SteamWorkshopService steamWorkshopService = Game.SteamWorkshopService;
			if (steamWorkshopService == null)
			{
				return;
			}
			steamWorkshopService.OpenWorkshopBrowseForHero(browseHeroId);
			return;
		}
		else
		{
			SteamWorkshopService steamWorkshopService2 = Game.SteamWorkshopService;
			if (steamWorkshopService2 == null)
			{
				return;
			}
			steamWorkshopService2.OpenWorkshopHome();
			return;
		}
	}

	// Token: 0x060015FF RID: 5631 RVA: 0x00088498 File Offset: 0x00086698
	private void RefreshView()
	{
		this.visibleItems.Clear();
		this.selfView.pool_items.RemoveAllView();
		SteamWorkshopService steamWorkshopService = Game.SteamWorkshopService;
		if (steamWorkshopService == null || !steamWorkshopService.IsAvailable)
		{
			this.RefreshHeaderTexts(Game.Language.Get("Steam 未初始化，无法读取创意工坊订阅。", ""));
			this.selfView.text_empty.gameObject.SetActive(true);
			this.selfView.text_empty.text = Game.Language.Get("请先通过 Steam 启动游戏，再使用创意工坊功能。", "");
			return;
		}
		List<WorkshopItemStatus> subscribedItems = steamWorkshopService.GetSubscribedItems();
		if (subscribedItems != null)
		{
			for (int i = 0; i < subscribedItems.Count; i++)
			{
				WorkshopItemStatus workshopItemStatus = subscribedItems[i];
				if (workshopItemStatus != null && (this.filterHeroId <= 0 || workshopItemStatus.heroId == this.filterHeroId))
				{
					this.visibleItems.Add(workshopItemStatus);
				}
			}
		}
		this.visibleItems.Sort(new Comparison<WorkshopItemStatus>(UI_WorkshopMods.CompareStatus));
		int num = 0;
		int num2 = 0;
		int enabledLocalFileCountForFilter = this.GetEnabledLocalFileCountForFilter();
		for (int j = 0; j < this.visibleItems.Count; j++)
		{
			if (this.visibleItems[j].installed)
			{
				num++;
			}
			if (this.visibleItems[j].enabledInGame)
			{
				num2++;
			}
		}
		string text = (this.visibleItems.Count == 0) ? Game.Language.Get("当前没有匹配的已订阅内容。", "") : string.Concat(new string[]
		{
			string.Format(Game.Language.Get("已订阅", ""), this.visibleItems.Count),
			" ，",
			string.Format(Game.Language.Get("已安装", ""), num),
			" ，",
			string.Format(Game.Language.Get("已启用", ""), num2)
		});
		if (enabledLocalFileCountForFilter > 0)
		{
			text = text + Game.Language.Get("本地文件MOD启用中", "") + enabledLocalFileCountForFilter.ToString();
		}
		this.RefreshHeaderTexts(text);
		this.selfView.text_empty.gameObject.SetActive(this.visibleItems.Count == 0);
		if (this.visibleItems.Count == 0)
		{
			this.selfView.text_empty.text = ((this.filterHeroId > 0) ? Game.Language.Get("订阅提示1", "") : Game.Language.Get("订阅提示2", ""));
			return;
		}
		for (int k = 0; k < this.visibleItems.Count; k++)
		{
			GameObject gameObject = this.selfView.pool_items.AddView();
			UI_WorkshopModsItem ui_WorkshopModsItem = gameObject.GetComponent<UI_WorkshopModsItem>();
			if (ui_WorkshopModsItem == null)
			{
				ui_WorkshopModsItem = gameObject.AddComponent<UI_WorkshopModsItem>();
			}
			ui_WorkshopModsItem.SetData(this.visibleItems[k], this);
		}
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x0008879C File Offset: 0x0008699C
	private void RefreshHeaderTexts(string statusText)
	{
		if (this.selfView.text_title != null)
		{
			this.selfView.text_title.text = Game.Language.Get("创意工坊模型管理", "");
		}
		if (this.selfView.text_status != null)
		{
			this.selfView.text_status.text = statusText;
		}
		if (this.selfView.text_filterHero != null)
		{
			this.selfView.text_filterHero.text = Game.Language.Get("筛选英雄", "") + UI_WorkshopMods.GetHeroLabel(this.filterHeroId);
		}
	}

	// Token: 0x06001601 RID: 5633 RVA: 0x0008884C File Offset: 0x00086A4C
	private void RegisterWorkshopEvents()
	{
		if (this.eventRegistered || Game.SteamWorkshopService == null)
		{
			return;
		}
		Game.SteamWorkshopService.ItemSubscribed += this.OnWorkshopItemSubscribed;
		Game.SteamWorkshopService.ItemUnsubscribed += this.OnWorkshopItemUnsubscribed;
		Game.SteamWorkshopService.ItemInstalledOrUpdated += this.OnWorkshopItemInstalledOrUpdated;
		Game.SteamWorkshopService.ItemDownloadProgress += this.OnWorkshopItemDownloadProgress;
		this.eventRegistered = true;
	}

	// Token: 0x06001602 RID: 5634 RVA: 0x000888C8 File Offset: 0x00086AC8
	private void UnregisterWorkshopEvents()
	{
		if (!this.eventRegistered || Game.SteamWorkshopService == null)
		{
			return;
		}
		Game.SteamWorkshopService.ItemSubscribed -= this.OnWorkshopItemSubscribed;
		Game.SteamWorkshopService.ItemUnsubscribed -= this.OnWorkshopItemUnsubscribed;
		Game.SteamWorkshopService.ItemInstalledOrUpdated -= this.OnWorkshopItemInstalledOrUpdated;
		Game.SteamWorkshopService.ItemDownloadProgress -= this.OnWorkshopItemDownloadProgress;
		this.eventRegistered = false;
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x00088944 File Offset: 0x00086B44
	private void OnWorkshopItemSubscribed(WorkshopItemStatus status)
	{
		this.RefreshView();
	}

	// Token: 0x06001604 RID: 5636 RVA: 0x0008894C File Offset: 0x00086B4C
	private void OnWorkshopItemUnsubscribed(ulong publishedFileId)
	{
		ulong num;
		bool flag = GameHelperClient.localPlayer != null && Game.LocalWorkshopModSettings != null && Game.LocalWorkshopModSettings.TryGetEnabledItemId((int)GameHelperClient.localPlayer.heroType, out num) && num == publishedFileId;
		LocalWorkshopModSettings localWorkshopModSettings = Game.LocalWorkshopModSettings;
		if (localWorkshopModSettings != null)
		{
			localWorkshopModSettings.DisableItem(publishedFileId);
		}
		if (flag && GameHelperClient.localPlayer != null)
		{
			LocalHeroModelService.RefreshLocalPlayerOverrideForHero((int)GameHelperClient.localPlayer.heroType);
		}
		this.RefreshView();
	}

	// Token: 0x06001605 RID: 5637 RVA: 0x000889C4 File Offset: 0x00086BC4
	private void OnWorkshopItemInstalledOrUpdated(WorkshopInstalledItem installedItem)
	{
		if (installedItem != null && installedItem.manifest != null && Game.LocalWorkshopModSettings != null && Game.LocalWorkshopModSettings.IsEnabledItemForHero(installedItem.manifest.heroId, installedItem.publishedFileId))
		{
			LocalHeroModelService.RefreshLocalPlayerOverrideForHero(installedItem.manifest.heroId);
		}
		this.RefreshView();
	}

	// Token: 0x06001606 RID: 5638 RVA: 0x00088944 File Offset: 0x00086B44
	private void OnWorkshopItemDownloadProgress(WorkshopDownloadProgress progress)
	{
		this.RefreshView();
	}

	// Token: 0x06001607 RID: 5639 RVA: 0x00088A17 File Offset: 0x00086C17
	private int GetBrowseHeroId()
	{
		if (this.filterHeroId > 0)
		{
			return this.filterHeroId;
		}
		if (GameHelperClient.localPlayer != null)
		{
			return (int)GameHelperClient.localPlayer.heroType;
		}
		return 0;
	}

	// Token: 0x06001608 RID: 5640 RVA: 0x00088A44 File Offset: 0x00086C44
	private static int ResolveFilterHeroId(object data)
	{
		if (data is int)
		{
			int b = (int)data;
			return Mathf.Max(0, b);
		}
		if (data is HeroType)
		{
			HeroType b2 = (HeroType)data;
			return Mathf.Max(0, (int)b2);
		}
		return 0;
	}

	// Token: 0x06001609 RID: 5641 RVA: 0x00088A80 File Offset: 0x00086C80
	private static int CompareStatus(WorkshopItemStatus left, WorkshopItemStatus right)
	{
		if (left == null && right == null)
		{
			return 0;
		}
		if (left == null)
		{
			return 1;
		}
		if (right == null)
		{
			return -1;
		}
		int num = right.enabledInGame.CompareTo(left.enabledInGame);
		if (num != 0)
		{
			return num;
		}
		num = right.installed.CompareTo(left.installed);
		if (num != 0)
		{
			return num;
		}
		num = left.heroId.CompareTo(right.heroId);
		if (num != 0)
		{
			return num;
		}
		num = string.Compare(left.title, right.title, StringComparison.OrdinalIgnoreCase);
		if (num != 0)
		{
			return num;
		}
		return left.publishedFileId.CompareTo(right.publishedFileId);
	}

	// Token: 0x0600160A RID: 5642 RVA: 0x00088B10 File Offset: 0x00086D10
	private static string GetHeroLabel(int heroId)
	{
		if (heroId <= 0)
		{
			return Game.Language.Get("全部", "");
		}
		if (Enum.IsDefined(typeof(HeroType), heroId))
		{
			return Util.GetHeroName((HeroType)heroId) + " (Hero_" + heroId.ToString() + ")";
		}
		return "Hero_" + heroId.ToString();
	}

	// Token: 0x0600160B RID: 5643 RVA: 0x00088B7C File Offset: 0x00086D7C
	private int GetEnabledLocalFileCountForFilter()
	{
		if (Game.LocalWorkshopModSettings == null)
		{
			return 0;
		}
		List<LocalWorkshopLocalFileModEntry> enabledLocalFileModsSnapshot = Game.LocalWorkshopModSettings.GetEnabledLocalFileModsSnapshot();
		int num = 0;
		for (int i = 0; i < enabledLocalFileModsSnapshot.Count; i++)
		{
			LocalWorkshopLocalFileModEntry localWorkshopLocalFileModEntry = enabledLocalFileModsSnapshot[i];
			if (localWorkshopLocalFileModEntry != null && (this.filterHeroId <= 0 || localWorkshopLocalFileModEntry.heroId == this.filterHeroId))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x040014A8 RID: 5288
	private const float AutoRefreshInterval = 1f;

	// Token: 0x040014A9 RID: 5289
	public UI_WorkshopMods_View selfView;

	// Token: 0x040014AA RID: 5290
	private readonly List<WorkshopItemStatus> visibleItems = new List<WorkshopItemStatus>();

	// Token: 0x040014AB RID: 5291
	private int filterHeroId;

	// Token: 0x040014AC RID: 5292
	private float nextRefreshTime;

	// Token: 0x040014AD RID: 5293
	private bool eventRegistered;
}
