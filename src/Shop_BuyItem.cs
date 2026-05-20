using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003A6 RID: 934
public class Shop_BuyItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001553 RID: 5459 RVA: 0x00084083 File Offset: 0x00082283
	private void Awake()
	{
		this.myButton = base.GetComponent<Button>();
		this.myImage = base.GetComponent<Image>();
	}

	// Token: 0x06001554 RID: 5460 RVA: 0x0008409D File Offset: 0x0008229D
	public void SetShopId(string value)
	{
		this.shopId = value;
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x000840A6 File Offset: 0x000822A6
	public void OnPointerEnter(PointerEventData eventData)
	{
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.ShowBagItemInfo(true, base.transform.position, this.shopId, true, BagItemType.Book);
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x000840D0 File Offset: 0x000822D0
	public void OnPointerExit(PointerEventData eventData)
	{
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.ShowBagItemInfo(false, base.transform.position, this.shopId, true, BagItemType.Book);
	}

	// Token: 0x06001557 RID: 5463 RVA: 0x000840FA File Offset: 0x000822FA
	public void UpdateShow(bool isShow)
	{
		if (this.root.gameObject.activeSelf != isShow)
		{
			this.root.gameObject.SetActive(isShow);
			this.myButton.enabled = isShow;
			this.myImage.enabled = isShow;
		}
	}

	// Token: 0x04001405 RID: 5125
	public new Text name;

	// Token: 0x04001406 RID: 5126
	public GameObject frame;

	// Token: 0x04001407 RID: 5127
	public Image cdImg;

	// Token: 0x04001408 RID: 5128
	public Image icon;

	// Token: 0x04001409 RID: 5129
	public Transform goldGo;

	// Token: 0x0400140A RID: 5130
	public Transform gemGo;

	// Token: 0x0400140B RID: 5131
	public ShopItem shopItem;

	// Token: 0x0400140C RID: 5132
	public RectTransform root;

	// Token: 0x0400140D RID: 5133
	private string shopId;

	// Token: 0x0400140E RID: 5134
	private Button myButton;

	// Token: 0x0400140F RID: 5135
	private Image myImage;
}
