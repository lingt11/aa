using System;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000381 RID: 897
public class Roguelike_Item : MonoBehaviour
{
	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x06001472 RID: 5234 RVA: 0x0007F23E File Offset: 0x0007D43E
	public RoguelikeUIData RoguelikeData
	{
		get
		{
			return this.roguelikeData;
		}
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x0007F246 File Offset: 0x0007D446
	private void Awake()
	{
		this.clickButton.AddButtonEvent(new UnityAction(this.OnBtnClick));
		this.refreshButton.AddButtonEvent(new UnityAction(this.OnRefreshBtnClick));
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x0007F276 File Offset: 0x0007D476
	public void SetKingBattle(bool isKingBattleValue)
	{
		this.isKingBattle = isKingBattleValue;
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x0007F27F File Offset: 0x0007D47F
	public void SetKingData(SaveLoadManager.TeamBuildData kingDataValue)
	{
		this.kingData = kingDataValue;
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x0007F288 File Offset: 0x0007D488
	public void UpdateView(RoguelikeUIData roguelikeUIData, int indexValue, bool canRefresh = false, int refreshNum = 0)
	{
		this.index = indexValue;
		this.roguelikeData = roguelikeUIData;
		this.nameText.text = roguelikeUIData.name;
		this.decText.text = roguelikeUIData.dec;
		if (this.isKingBattle)
		{
			this.UpdateKingBattle();
		}
		else if (roguelikeUIData.quality >= 0)
		{
			this.iconImg.sprite = Resources.Load<Sprite>(roguelikeUIData.icon);
			this.iconImg.color = ColorDefine.QuaUIColor[roguelikeUIData.quality];
			this.nameText.color = ColorDefine.QuaUIColor[roguelikeUIData.quality];
			if (!this.exDecText.gameObject.activeSelf)
			{
				this.exDecText.gameObject.SetActive(true);
			}
			this.exDecText.color = ColorDefine.QuaUIColor[roguelikeUIData.quality];
			this.exDecText.text = Game.Language.Get(PathDefine.Concat("quality_", roguelikeUIData.quality), "");
			this.decText.fontSize = 21;
		}
		else
		{
			this.iconImg.sprite = Resources.Load<Sprite>(roguelikeUIData.icon);
			this.iconImg.color = Color.white;
			this.nameText.color = new Color(1f, 0.7462285f, 0.1462264f, 1f);
			if (this.exDecText.gameObject.activeSelf)
			{
				this.exDecText.gameObject.SetActive(false);
			}
			this.decText.fontSize = 19;
		}
		if (canRefresh)
		{
			if (!this.refreshButton.gameObject.activeSelf)
			{
				this.refreshButton.gameObject.SetActive(true);
			}
			this.UpateRefreshNum(refreshNum);
			return;
		}
		if (this.refreshButton.gameObject.activeSelf)
		{
			this.refreshButton.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x0007F478 File Offset: 0x0007D678
	private void UpdateKingBattle()
	{
		this.iconImg.color = Color.white;
		this.nameText.color = Color.white;
		if (!this.exDecText.gameObject.activeSelf)
		{
			this.exDecText.gameObject.SetActive(true);
		}
		this.exDecText.color = ColorDefine.QuaUIColor[3];
		int num = (this.roguelikeData.displayRank > 0) ? this.roguelikeData.displayRank : (int.Parse(this.roguelikeData.data) + 1);
		this.exDecText.text = PathDefine.Concat(Game.Language.Get("王者排名", ""), num);
		this.UploadHead(this.kingData.members[0].steamID);
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x0007F552 File Offset: 0x0007D752
	private void OnBtnClick()
	{
		Game.UI.GetUI<UI_Roguelike>().OnClickRoguelike(this.roguelikeData);
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x0007F569 File Offset: 0x0007D769
	private void OnRefreshBtnClick()
	{
		Game.UI.GetUI<UI_Roguelike>().OnRefreshBtn(this.index);
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x0007F580 File Offset: 0x0007D780
	public void UpateRefreshNum(int refreshNum)
	{
		this.refreshButtonText.text = refreshNum.ToString();
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x0007F594 File Offset: 0x0007D794
	private void UploadHead(ulong id64)
	{
		this.fallbackSprite = Util.GetHeroIcon(this.kingData.members[0].heroType);
		if (id64 == 0UL)
		{
			this.iconImg.sprite = this.fallbackSprite;
			return;
		}
		int targetSize = 256;
		base.StartCoroutine(SteamAvatarLoader.LoadAvatarSprite(new CSteamID(id64), delegate(Sprite sprite)
		{
			this.iconImg.sprite = (sprite ?? this.fallbackSprite);
			this.iconImg.preserveAspect = true;
		}, SteamAvatarLoader.AvatarSize.Large, this.fallbackSprite, targetSize));
	}

	// Token: 0x0400131C RID: 4892
	public Text nameText;

	// Token: 0x0400131D RID: 4893
	public Text decText;

	// Token: 0x0400131E RID: 4894
	public Image iconImg;

	// Token: 0x0400131F RID: 4895
	public Button clickButton;

	// Token: 0x04001320 RID: 4896
	private RoguelikeUIData roguelikeData;

	// Token: 0x04001321 RID: 4897
	public Text exDecText;

	// Token: 0x04001322 RID: 4898
	public Button refreshButton;

	// Token: 0x04001323 RID: 4899
	public TextMeshProUGUI refreshButtonText;

	// Token: 0x04001324 RID: 4900
	private int index;

	// Token: 0x04001325 RID: 4901
	private bool isKingBattle;

	// Token: 0x04001326 RID: 4902
	private SaveLoadManager.TeamBuildData kingData;

	// Token: 0x04001327 RID: 4903
	private Sprite fallbackSprite;

	// Token: 0x04001328 RID: 4904
	public RoguelikeKingHead roguelikeKingHead;
}
