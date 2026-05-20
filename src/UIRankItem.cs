using System;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000356 RID: 854
public class UIRankItem : MonoBehaviour
{
	// Token: 0x06001393 RID: 5011 RVA: 0x00078F04 File Offset: 0x00077104
	public void SetData(SaveLoadManager.TeamBuildData data, int rank)
	{
		this.UploadHead(data);
		this.nameText.text = data.members[0].kingName;
		if (rank > 3)
		{
			this.rankText.gameObject.SetActive(true);
			this.rankImage.gameObject.SetActive(false);
			this.rankText.text = rank.ToString();
		}
		else
		{
			this.rankText.gameObject.SetActive(false);
			this.rankImage.gameObject.SetActive(true);
			this.rankImage.sprite = Resources.Load<Sprite>(PathDefine.Concat("Bundles/UI/RankUI/rank_list", rank));
			this.rankImage.rectTransform.sizeDelta = new Vector2((float)(80 - 5 * rank), (float)(80 - 5 * rank));
		}
		int count = data.members.Count;
		for (int i = 0; i < this.heroItems.Length; i++)
		{
			UIRankHeroItem uirankHeroItem = this.heroItems[i];
			if (i < count)
			{
				uirankHeroItem.gameObject.SetActive(true);
				uirankHeroItem.SetHeroHead(data.members[i]);
			}
			else
			{
				uirankHeroItem.gameObject.SetActive(false);
			}
		}
		float x = this.heroItems[0].GetComponent<RectTransform>().sizeDelta.x;
		this.heroRectTransform.sizeDelta = new Vector2(x * (float)count, x * (float)count);
		this.msgText.text = data.teamMessage;
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x0007906C File Offset: 0x0007726C
	private void UploadHead(SaveLoadManager.TeamBuildData data)
	{
		bool steamID = data.members[0].steamID != 0UL;
		this.fallbackSprite = Util.GetHeroIcon(data.members[0].heroType);
		if (!steamID)
		{
			this.steamHead.sprite = this.fallbackSprite;
			return;
		}
		int targetSize = 256;
		base.StartCoroutine(SteamAvatarLoader.LoadAvatarSprite(new CSteamID(data.members[0].steamID), delegate(Sprite sprite)
		{
			this.steamHead.sprite = (sprite ?? this.fallbackSprite);
			this.steamHead.preserveAspect = true;
		}, SteamAvatarLoader.AvatarSize.Large, this.fallbackSprite, targetSize));
	}

	// Token: 0x04001223 RID: 4643
	public Image steamHead;

	// Token: 0x04001224 RID: 4644
	public Text nameText;

	// Token: 0x04001225 RID: 4645
	public Text msgText;

	// Token: 0x04001226 RID: 4646
	public TextMeshProUGUI rankText;

	// Token: 0x04001227 RID: 4647
	public Image rankImage;

	// Token: 0x04001228 RID: 4648
	public RectTransform heroRectTransform;

	// Token: 0x04001229 RID: 4649
	public UIRankHeroItem[] heroItems;

	// Token: 0x0400122A RID: 4650
	private Sprite fallbackSprite;
}
