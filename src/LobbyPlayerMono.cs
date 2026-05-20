using System;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000166 RID: 358
public class LobbyPlayerMono : MonoBehaviour
{
	// Token: 0x06000706 RID: 1798 RVA: 0x0002AF0A File Offset: 0x0002910A
	private void Awake()
	{
		this.fallbackSprite = this.playerHead.sprite;
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0002AF20 File Offset: 0x00029120
	public void UploadHead(string playerSteamId64)
	{
		if (string.IsNullOrEmpty(playerSteamId64) || playerSteamId64.Equals(this.otherSteamId64))
		{
			return;
		}
		this.otherSteamId64 = playerSteamId64;
		int targetSize = 256;
		ulong ulSteamID;
		if (!string.IsNullOrEmpty(this.otherSteamId64) && ulong.TryParse(this.otherSteamId64, out ulSteamID))
		{
			base.StartCoroutine(SteamAvatarLoader.LoadAvatarSprite(new CSteamID(ulSteamID), delegate(Sprite sprite)
			{
				this.playerHead.sprite = (sprite ?? this.fallbackSprite);
				this.playerHead.preserveAspect = true;
			}, SteamAvatarLoader.AvatarSize.Large, this.fallbackSprite, targetSize));
			return;
		}
		base.StartCoroutine(SteamAvatarLoader.LoadMyAvatarSprite(delegate(Sprite sprite)
		{
			this.playerHead.sprite = (sprite ?? this.fallbackSprite);
			this.playerHead.preserveAspect = true;
		}, SteamAvatarLoader.AvatarSize.Large, this.fallbackSprite, targetSize));
	}

	// Token: 0x04000B19 RID: 2841
	public Text playerName;

	// Token: 0x04000B1A RID: 2842
	public Image playerHead;

	// Token: 0x04000B1B RID: 2843
	[Header("可选：指定别人头像；为空则取自己")]
	private string otherSteamId64 = "";

	// Token: 0x04000B1C RID: 2844
	private SteamAvatarLoader.AvatarSize size = SteamAvatarLoader.AvatarSize.Large;

	// Token: 0x04000B1D RID: 2845
	private Sprite fallbackSprite;
}
