using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000349 RID: 841
public class EquipCardTouch : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001328 RID: 4904 RVA: 0x00073D39 File Offset: 0x00071F39
	private void OnDisable()
	{
		this.OnPointerExit(null);
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x00073D44 File Offset: 0x00071F44
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.isEquip)
		{
			UGUIManager ui = Game.UI;
			if (ui == null)
			{
				return;
			}
			UI_MyCard ui2 = ui.GetUI<UI_MyCard>();
			if (ui2 == null)
			{
				return;
			}
			ui2.ShowEquipCardInfo(base.transform.position, this.cardData);
			return;
		}
		else
		{
			UGUIManager ui3 = Game.UI;
			if (ui3 == null)
			{
				return;
			}
			UI_MyCard ui4 = ui3.GetUI<UI_MyCard>();
			if (ui4 == null)
			{
				return;
			}
			ui4.ShowCardInfo(base.transform.position, this.cardData, this.showCardInfoOnLeft);
			return;
		}
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x00073DB4 File Offset: 0x00071FB4
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.isEquip)
		{
			UGUIManager ui = Game.UI;
			if (ui == null)
			{
				return;
			}
			UI_MyCard ui2 = ui.GetUI<UI_MyCard>();
			if (ui2 == null)
			{
				return;
			}
			ui2.HideEquipCardInfo();
			return;
		}
		else
		{
			UGUIManager ui3 = Game.UI;
			if (ui3 == null)
			{
				return;
			}
			UI_MyCard ui4 = ui3.GetUI<UI_MyCard>();
			if (ui4 == null)
			{
				return;
			}
			ui4.HideCardInfo();
			return;
		}
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x00073DF1 File Offset: 0x00071FF1
	public void InitCardData(CardData cardDataValue, bool isEquipValue, bool showCardInfoOnLeftValue = false)
	{
		this.cardData = cardDataValue;
		this.isEquip = isEquipValue;
		this.showCardInfoOnLeft = showCardInfoOnLeftValue;
	}

	// Token: 0x040011B8 RID: 4536
	private CardData cardData;

	// Token: 0x040011B9 RID: 4537
	private bool isEquip;

	// Token: 0x040011BA RID: 4538
	private bool showCardInfoOnLeft;
}
