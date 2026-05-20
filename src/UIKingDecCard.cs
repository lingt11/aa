using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200033B RID: 827
public class UIKingDecCard : MonoBehaviour
{
	// Token: 0x060012EE RID: 4846 RVA: 0x00071194 File Offset: 0x0006F394
	public void SetCard(int cardId1, int cardId2)
	{
		CardData cardData = Game.GameData.CardDataDic[cardId1];
		this.text1.text = Game.Language.Get(PathDefine.Concat("card_", cardData.id), "");
		this.power1.text = cardData.capacity.ToString();
		this.qualityImg1.color = ColorDefine.QuaUIColor[cardData.quality];
		this.image1.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Card/" + cardData.icon);
		if (cardId2 == -1)
		{
			if (this.go2.activeSelf)
			{
				this.go2.SetActive(false);
				return;
			}
		}
		else
		{
			if (!this.go2.activeSelf)
			{
				this.go2.SetActive(true);
			}
			cardData = Game.GameData.CardDataDic[cardId2];
			this.text2.text = Game.Language.Get(PathDefine.Concat("card_", cardData.id), "");
			this.power2.text = cardData.capacity.ToString();
			this.qualityImg2.color = ColorDefine.QuaUIColor[cardData.quality];
			this.image2.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Card/" + cardData.icon);
		}
	}

	// Token: 0x04001146 RID: 4422
	public GameObject go1;

	// Token: 0x04001147 RID: 4423
	public Image image1;

	// Token: 0x04001148 RID: 4424
	public Text text1;

	// Token: 0x04001149 RID: 4425
	public Text power1;

	// Token: 0x0400114A RID: 4426
	public Image qualityImg1;

	// Token: 0x0400114B RID: 4427
	public GameObject go2;

	// Token: 0x0400114C RID: 4428
	public Image image2;

	// Token: 0x0400114D RID: 4429
	public Text text2;

	// Token: 0x0400114E RID: 4430
	public Text power2;

	// Token: 0x0400114F RID: 4431
	public Image qualityImg2;
}
