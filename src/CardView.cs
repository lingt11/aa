using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000347 RID: 839
public class CardView : MonoBehaviour
{
	// Token: 0x170000CD RID: 205
	// (get) Token: 0x0600131D RID: 4893 RVA: 0x00073A0F File Offset: 0x00071C0F
	public int CardId
	{
		get
		{
			return this.cardId;
		}
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x00073A18 File Offset: 0x00071C18
	public void UpdateView(CardData cardData, bool isTeam, int overrideHaveNum = -1, bool showCardInfoOnLeft = false)
	{
		this.cardId = cardData.id;
		if (isTeam)
		{
			this.name.text = PathDefine.Concat(Game.Language.Get("队友", ""), Game.Language.Get(PathDefine.Concat("card_", cardData.id), ""));
		}
		else
		{
			this.name.text = Game.Language.Get(PathDefine.Concat("card_", cardData.id), "");
		}
		this.power.text = cardData.capacity.ToString();
		this.qualityImg.color = ColorDefine.QuaUIColor[cardData.quality];
		this.cardIcon.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Card/" + cardData.icon);
		if (this.info != null)
		{
			this.info.text = UI_MyCard.GetCardInfo(cardData);
		}
		if (this.haveNum != null)
		{
			int num = 0;
			CardManager.HaveCardData haveCardData;
			if (overrideHaveNum >= 0)
			{
				num = overrideHaveNum;
			}
			else if (SaveLoadManager.haveCardDataDic.TryGetValue(cardData.id, out haveCardData))
			{
				num = haveCardData.haveNum;
			}
			if (num == 0)
			{
				if (!this.lockGo.activeSelf)
				{
					this.lockGo.SetActive(true);
					this.haveNum.gameObject.SetActive(false);
				}
			}
			else
			{
				if (this.lockGo.activeSelf)
				{
					this.lockGo.SetActive(false);
					this.haveNum.gameObject.SetActive(true);
				}
				this.haveNum.text = PathDefine.Concat(StringDefine.X, num);
			}
		}
		if (this.equipCardTouch != null)
		{
			this.equipCardTouch.InitCardData(cardData, this.isEquip, showCardInfoOnLeft);
		}
	}

	// Token: 0x040011A9 RID: 4521
	public new Text name;

	// Token: 0x040011AA RID: 4522
	public Text info;

	// Token: 0x040011AB RID: 4523
	public Text power;

	// Token: 0x040011AC RID: 4524
	public Text haveNum;

	// Token: 0x040011AD RID: 4525
	public GameObject lockGo;

	// Token: 0x040011AE RID: 4526
	public Image qualityImg;

	// Token: 0x040011AF RID: 4527
	public Image cardIcon;

	// Token: 0x040011B0 RID: 4528
	public EquipCardTouch equipCardTouch;

	// Token: 0x040011B1 RID: 4529
	public bool isEquip;

	// Token: 0x040011B2 RID: 4530
	private int cardId;
}
