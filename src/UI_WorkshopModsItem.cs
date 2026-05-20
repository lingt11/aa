using System;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003C3 RID: 963
public class UI_WorkshopModsItem : MonoBehaviour
{
	// Token: 0x0600160C RID: 5644 RVA: 0x00088BD7 File Offset: 0x00086DD7
	private void Awake()
	{
		this.EnsureRefs();
	}

	// Token: 0x0600160D RID: 5645 RVA: 0x00088BE0 File Offset: 0x00086DE0
	public void SetData(WorkshopItemStatus status, UI_WorkshopMods ownerCtrl)
	{
		this.EnsureRefs();
		this.currentStatus = status;
		this.owner = ownerCtrl;
		if (this.currentStatus == null)
		{
			return;
		}
		if (this.textTitle != null)
		{
			this.textTitle.text = UI_WorkshopModsItem.GetTitle(this.currentStatus);
		}
		if (this.textState != null)
		{
			this.textState.text = UI_WorkshopModsItem.GetStateText(this.currentStatus);
			this.textState.color = UI_WorkshopModsItem.GetStateColor(this.currentStatus);
		}
		if (this.textDetail != null)
		{
			this.textDetail.text = UI_WorkshopModsItem.BuildDetailText(this.currentStatus);
		}
		this.RefreshPreviewImage(this.currentStatus);
		if (this.btnEnable != null)
		{
			this.btnEnable.gameObject.SetActive(this.currentStatus.installed && !this.currentStatus.enabledInGame && this.currentStatus.heroId > 0);
			this.btnEnable.AddButtonEvent(new UnityAction(this.OnEnableClick));
		}
		if (this.btnDisable != null)
		{
			this.btnDisable.gameObject.SetActive(this.currentStatus.enabledInGame);
			this.btnDisable.AddButtonEvent(new UnityAction(this.OnDisableClick));
		}
		if (this.btnDownload != null)
		{
			bool active = this.currentStatus.subscribed && (!this.currentStatus.installed || this.currentStatus.updateAvailable || this.currentStatus.downloadPending || this.currentStatus.downloading);
			this.btnDownload.gameObject.SetActive(active);
			this.btnDownload.interactable = !this.currentStatus.downloading;
			this.btnDownload.AddButtonEvent(new UnityAction(this.OnDownloadClick));
		}
		if (this.btnOpen != null)
		{
			this.btnOpen.gameObject.SetActive(true);
			this.btnOpen.AddButtonEvent(new UnityAction(this.OnOpenClick));
		}
		if (this.btnUnsubscribe != null)
		{
			this.btnUnsubscribe.gameObject.SetActive(this.currentStatus.subscribed);
			this.btnUnsubscribe.AddButtonEvent(new UnityAction(this.OnUnsubscribeClick));
		}
		UI_WorkshopModsItem.SetButtonLabel(this.textEnable, UI_WorkshopModsItem.HasEnabledLocalFileMod(this.currentStatus.heroId) ? Game.Language.Get("切换为Steam", "") : Game.Language.Get("启用", ""));
		UI_WorkshopModsItem.SetButtonLabel(this.textDisable, Game.Language.Get("停用", ""));
		UI_WorkshopModsItem.SetButtonLabel(this.textDownload, this.currentStatus.updateAvailable ? Game.Language.Get("更新", "") : Game.Language.Get("下载", ""));
		UI_WorkshopModsItem.SetButtonLabel(this.textOpen, Game.Language.Get("查看", ""));
		UI_WorkshopModsItem.SetButtonLabel(this.textUnsubscribe, Game.Language.Get("取消订阅", ""));
	}

	// Token: 0x0600160E RID: 5646 RVA: 0x00088F2C File Offset: 0x0008712C
	private void OnEnableClick()
	{
		UI_WorkshopMods ui_WorkshopMods = this.owner;
		if (ui_WorkshopMods == null)
		{
			return;
		}
		ui_WorkshopMods.OnEnableItemClicked(this.currentStatus);
	}

	// Token: 0x0600160F RID: 5647 RVA: 0x00088F44 File Offset: 0x00087144
	private void OnDisableClick()
	{
		UI_WorkshopMods ui_WorkshopMods = this.owner;
		if (ui_WorkshopMods == null)
		{
			return;
		}
		ui_WorkshopMods.OnDisableItemClicked(this.currentStatus);
	}

	// Token: 0x06001610 RID: 5648 RVA: 0x00088F5C File Offset: 0x0008715C
	private void OnDownloadClick()
	{
		UI_WorkshopMods ui_WorkshopMods = this.owner;
		if (ui_WorkshopMods == null)
		{
			return;
		}
		ui_WorkshopMods.OnDownloadItemClicked(this.currentStatus);
	}

	// Token: 0x06001611 RID: 5649 RVA: 0x00088F74 File Offset: 0x00087174
	private void OnOpenClick()
	{
		UI_WorkshopMods ui_WorkshopMods = this.owner;
		if (ui_WorkshopMods == null)
		{
			return;
		}
		ui_WorkshopMods.OnOpenItemClicked(this.currentStatus);
	}

	// Token: 0x06001612 RID: 5650 RVA: 0x00088F8C File Offset: 0x0008718C
	private void OnUnsubscribeClick()
	{
		UI_WorkshopMods ui_WorkshopMods = this.owner;
		if (ui_WorkshopMods == null)
		{
			return;
		}
		ui_WorkshopMods.OnUnsubscribeItemClicked(this.currentStatus);
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x00088FA4 File Offset: 0x000871A4
	private void EnsureRefs()
	{
		if (this.textTitle != null)
		{
			return;
		}
		this.textTitle = this.GetText("top_row/text_title");
		this.textState = this.GetText("top_row/text_state");
		this.textDetail = this.GetText("text_detail");
		this.previewImg = this.GetImage("previewImg");
		this.btnEnable = this.GetButton("button_row/btn_enable");
		this.btnDisable = this.GetButton("button_row/btn_disable");
		this.btnDownload = this.GetButton("button_row/btn_download");
		this.btnOpen = this.GetButton("button_row/btn_open");
		this.btnUnsubscribe = this.GetButton("button_row/btn_unsubscribe");
		this.textEnable = UI_WorkshopModsItem.GetButtonLabel(this.btnEnable);
		this.textDisable = UI_WorkshopModsItem.GetButtonLabel(this.btnDisable);
		this.textDownload = UI_WorkshopModsItem.GetButtonLabel(this.btnDownload);
		this.textOpen = UI_WorkshopModsItem.GetButtonLabel(this.btnOpen);
		this.textUnsubscribe = UI_WorkshopModsItem.GetButtonLabel(this.btnUnsubscribe);
	}

	// Token: 0x06001614 RID: 5652 RVA: 0x000890B0 File Offset: 0x000872B0
	private Text GetText(string path)
	{
		Transform transform = base.transform.Find(path);
		if (!(transform == null))
		{
			return transform.GetComponent<Text>();
		}
		return null;
	}

	// Token: 0x06001615 RID: 5653 RVA: 0x000890DC File Offset: 0x000872DC
	private Image GetImage(string path)
	{
		Transform transform = base.transform.Find(path);
		if (transform == null)
		{
			transform = UI_WorkshopModsItem.FindChildRecursive(base.transform, path);
		}
		if (!(transform == null))
		{
			return transform.GetComponent<Image>();
		}
		return null;
	}

	// Token: 0x06001616 RID: 5654 RVA: 0x00089120 File Offset: 0x00087320
	private Button GetButton(string path)
	{
		Transform transform = base.transform.Find(path);
		if (!(transform == null))
		{
			return transform.GetComponent<Button>();
		}
		return null;
	}

	// Token: 0x06001617 RID: 5655 RVA: 0x0008914B File Offset: 0x0008734B
	private static Text GetButtonLabel(Button button)
	{
		if (!(button == null))
		{
			return button.GetComponentInChildren<Text>(true);
		}
		return null;
	}

	// Token: 0x06001618 RID: 5656 RVA: 0x00089160 File Offset: 0x00087360
	private static Transform FindChildRecursive(Transform root, string childName)
	{
		if (root == null || string.IsNullOrEmpty(childName))
		{
			return null;
		}
		for (int i = 0; i < root.childCount; i++)
		{
			Transform child = root.GetChild(i);
			if (child.name == childName)
			{
				return child;
			}
			Transform transform = UI_WorkshopModsItem.FindChildRecursive(child, childName);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}

	// Token: 0x06001619 RID: 5657 RVA: 0x000891BD File Offset: 0x000873BD
	private static void SetButtonLabel(Text target, string value)
	{
		if (target != null)
		{
			target.text = value;
		}
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x000891CF File Offset: 0x000873CF
	private static string GetTitle(WorkshopItemStatus status)
	{
		if (!string.IsNullOrEmpty(status.title))
		{
			return status.title;
		}
		return "Workshop Item " + status.publishedFileId.ToString();
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x000891FC File Offset: 0x000873FC
	private static string GetStateText(WorkshopItemStatus status)
	{
		if (UI_WorkshopModsItem.HasEnabledLocalFileMod(status.heroId) && !status.enabledInGame)
		{
			return Game.Language.Get("本地MOD启用中", "");
		}
		if (status.enabledInGame)
		{
			return Game.Language.Get("已启用(Steam)", "");
		}
		if (status.downloading || status.downloadPending)
		{
			return Game.Language.Get("下载中", "");
		}
		if (status.updateAvailable)
		{
			return Game.Language.Get("已安装 / 待更新", "");
		}
		if (status.installed)
		{
			return Game.Language.Get("已安装提示", "");
		}
		if (status.subscribed)
		{
			return Game.Language.Get("订阅中", "");
		}
		return Game.Language.Get("未订阅", "");
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x000892E0 File Offset: 0x000874E0
	private static Color GetStateColor(WorkshopItemStatus status)
	{
		if (UI_WorkshopModsItem.HasEnabledLocalFileMod(status.heroId) && !status.enabledInGame)
		{
			return new Color(1f, 0.65f, 0.25f);
		}
		if (status.enabledInGame)
		{
			return new Color(0.35f, 0.85f, 0.45f);
		}
		if (status.downloading || status.downloadPending)
		{
			return new Color(1f, 0.75f, 0.25f);
		}
		if (status.updateAvailable)
		{
			return new Color(1f, 0.45f, 0.25f);
		}
		if (status.installed)
		{
			return new Color(0.55f, 0.85f, 1f);
		}
		return new Color(0.9f, 0.9f, 0.9f);
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x000893A8 File Offset: 0x000875A8
	private static string BuildDetailText(WorkshopItemStatus status)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(UI_WorkshopModsItem.GetHeroLabel(status.heroId));
		stringBuilder.Append("  |  FileId: ").Append(status.publishedFileId);
		stringBuilder.Append("  |  ").Append(Game.Language.Get("来源", "")).Append(status.enabledInGame ? Game.Language.Get("Steam下载", "") : Game.Language.Get("Steam订阅", ""));
		if (UI_WorkshopModsItem.HasEnabledLocalFileMod(status.heroId))
		{
			stringBuilder.Append("\n" + Game.Language.Get("当前英雄已启用本地文件MOD", ""));
		}
		if (status.sizeOnDisk > 0UL)
		{
			stringBuilder.Append("  |  ").Append(UI_WorkshopModsItem.FormatBytes(status.sizeOnDisk));
		}
		if (status.updateAvailable)
		{
			stringBuilder.Append("\n" + Game.Language.Get("检测到远端更新", ""));
			if (status.timestamp > 0U || status.remoteTimestamp > 0U)
			{
				stringBuilder.Append(Game.Language.Get("本地", "")).Append(status.timestamp);
				stringBuilder.Append(Game.Language.Get("远端", "")).Append(status.remoteTimestamp);
			}
		}
		if (!string.IsNullOrEmpty(status.installFolder))
		{
			stringBuilder.Append("\n").Append(status.installFolder);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x00089550 File Offset: 0x00087750
	private void RefreshPreviewImage(WorkshopItemStatus status)
	{
		if (this.previewImg == null)
		{
			return;
		}
		LocalWorkshopManifest manifest;
		Sprite sprite;
		if (status != null && !string.IsNullOrEmpty(status.installFolder) && LocalWorkshopManifestLoader.TryLoad(status.installFolder, out manifest) && LocalHeroModelService.TryLoadPackagePreviewSprite(status.installFolder, manifest, out sprite))
		{
			this.previewImg.sprite = sprite;
			this.previewImg.preserveAspect = true;
			this.previewImg.enabled = true;
			return;
		}
		this.previewImg.sprite = null;
		this.previewImg.enabled = false;
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x000895D8 File Offset: 0x000877D8
	private static bool HasEnabledLocalFileMod(int heroId)
	{
		string text;
		return heroId > 0 && Game.LocalWorkshopModSettings != null && Game.LocalWorkshopModSettings.TryGetEnabledLocalFileItemRoot(heroId, out text);
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x00089600 File Offset: 0x00087800
	private static string GetHeroLabel(int heroId)
	{
		if (heroId <= 0)
		{
			return Game.Language.Get("未识别英雄", "");
		}
		if (Enum.IsDefined(typeof(HeroType), heroId))
		{
			return Util.GetHeroName((HeroType)heroId) + " (Hero_" + heroId.ToString() + ")";
		}
		return "Hero_" + heroId.ToString();
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x0008966C File Offset: 0x0008786C
	private static string FormatBytes(ulong bytes)
	{
		if (bytes >= 1073741824UL)
		{
			return (bytes / 1.0737418E+09f).ToString("F1") + " GB";
		}
		if (bytes >= 1048576UL)
		{
			return (bytes / 1048576f).ToString("F1") + " MB";
		}
		if (bytes >= 1024UL)
		{
			return (bytes / 1024f).ToString("F1") + " KB";
		}
		return bytes.ToString() + " B";
	}

	// Token: 0x040014AE RID: 5294
	private Text textTitle;

	// Token: 0x040014AF RID: 5295
	private Text textState;

	// Token: 0x040014B0 RID: 5296
	private Text textDetail;

	// Token: 0x040014B1 RID: 5297
	private Image previewImg;

	// Token: 0x040014B2 RID: 5298
	private Button btnEnable;

	// Token: 0x040014B3 RID: 5299
	private Button btnDisable;

	// Token: 0x040014B4 RID: 5300
	private Button btnDownload;

	// Token: 0x040014B5 RID: 5301
	private Button btnOpen;

	// Token: 0x040014B6 RID: 5302
	private Button btnUnsubscribe;

	// Token: 0x040014B7 RID: 5303
	private Text textEnable;

	// Token: 0x040014B8 RID: 5304
	private Text textDisable;

	// Token: 0x040014B9 RID: 5305
	private Text textDownload;

	// Token: 0x040014BA RID: 5306
	private Text textOpen;

	// Token: 0x040014BB RID: 5307
	private Text textUnsubscribe;

	// Token: 0x040014BC RID: 5308
	private WorkshopItemStatus currentStatus;

	// Token: 0x040014BD RID: 5309
	private UI_WorkshopMods owner;
}
