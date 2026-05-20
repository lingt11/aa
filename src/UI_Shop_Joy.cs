using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003AB RID: 939
public class UI_Shop_Joy
{
	// Token: 0x0600157F RID: 5503 RVA: 0x00085EDC File Offset: 0x000840DC
	public UI_Shop_Joy(UI_Shop shop)
	{
		this.shop = shop;
		this.tabButtons.Clear();
		this.tabButtons.Add(shop.selfView.btn_book.gameObject);
		this.tabButtons.Add(shop.selfView.btn_item.gameObject);
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x00085F44 File Offset: 0x00084144
	public void Open()
	{
		this.isOpen = true;
		MySystemEvent.Instance.RegisterMessage(1, new Action<Body>(this.JoyA));
		MySystemEvent.Instance.RegisterMessage(3, new Action<Body>(this.JoyCrossUp));
		MySystemEvent.Instance.RegisterMessage(4, new Action<Body>(this.JoyCrossDown));
		MySystemEvent.Instance.RegisterMessage(5, new Action<Body>(this.JoyCrossLeft));
		MySystemEvent.Instance.RegisterMessage(6, new Action<Body>(this.JoyCrossRight));
		MySystemEvent.Instance.RegisterMessage(7, new Action<Body>(this.JoyLeftShoulder));
		MySystemEvent.Instance.RegisterMessage(8, new Action<Body>(this.JoyRightShoulder));
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x00085FFC File Offset: 0x000841FC
	public void Close()
	{
		this.isOpen = false;
		MySystemEvent.Instance.UnregisterMessage(1, new Action<Body>(this.JoyA));
		MySystemEvent.Instance.UnregisterMessage(3, new Action<Body>(this.JoyCrossUp));
		MySystemEvent.Instance.UnregisterMessage(4, new Action<Body>(this.JoyCrossDown));
		MySystemEvent.Instance.UnregisterMessage(5, new Action<Body>(this.JoyCrossLeft));
		MySystemEvent.Instance.UnregisterMessage(6, new Action<Body>(this.JoyCrossRight));
		MySystemEvent.Instance.UnregisterMessage(7, new Action<Body>(this.JoyLeftShoulder));
		MySystemEvent.Instance.UnregisterMessage(8, new Action<Body>(this.JoyRightShoulder));
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x000860B1 File Offset: 0x000842B1
	public void ClearShopItemSelect()
	{
		if (this.isActive)
		{
			this.isActive = false;
			this.shop.shopSelectList[this.shopSelectIndex].frame.SetActive(false);
		}
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x000860E4 File Offset: 0x000842E4
	private void SelectShopItem(int add)
	{
		if (!this.isActive)
		{
			this.isActive = true;
			this.shopSelectIndex = 0;
			this.shop.shopSelectList[this.shopSelectIndex].frame.SetActive(true);
			return;
		}
		this.shop.shopSelectList[this.shopSelectIndex].frame.SetActive(false);
		this.shopSelectIndex += add;
		if (this.shopSelectIndex < 0)
		{
			this.shopSelectIndex = 0;
		}
		if (this.shopSelectIndex >= this.shop.shopSelectList.Count)
		{
			this.shopSelectIndex = this.shop.shopSelectList.Count - 1;
		}
		this.shop.shopSelectList[this.shopSelectIndex].frame.SetActive(true);
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x000861B9 File Offset: 0x000843B9
	private void JoyCrossUp(Body body)
	{
		this.SelectShopItem(-4);
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x000861C3 File Offset: 0x000843C3
	private void JoyCrossDown(Body body)
	{
		this.SelectShopItem(4);
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x000861CC File Offset: 0x000843CC
	private void JoyCrossLeft(Body body)
	{
		this.SelectShopItem(-1);
	}

	// Token: 0x06001587 RID: 5511 RVA: 0x000861D5 File Offset: 0x000843D5
	private void JoyCrossRight(Body body)
	{
		this.SelectShopItem(1);
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x000861E0 File Offset: 0x000843E0
	private void SetTabButton(int add)
	{
		this.tabIndex += add;
		if (this.tabIndex < 0)
		{
			this.tabIndex = 0;
		}
		if (this.tabIndex > this.tabButtons.Count - 1)
		{
			this.tabIndex = this.tabButtons.Count - 1;
		}
		if (this.preTabButton == null)
		{
			this.tabIndex = 0;
			this.tabButtons[this.tabIndex].transform.localScale = Vector3.one * 1.2f;
			this.preTabButton = this.tabButtons[this.tabIndex];
			this.shop.selfView.btn_book.onClick.Invoke();
		}
		else
		{
			this.preTabButton.transform.localScale = Vector3.one;
			this.tabButtons[this.tabIndex].transform.localScale = Vector3.one * 1.2f;
			this.preTabButton = this.tabButtons[this.tabIndex];
			if (this.tabIndex == 0)
			{
				this.shop.selfView.btn_book.onClick.Invoke();
			}
			else if (this.tabIndex == 1)
			{
				this.shop.selfView.btn_item.onClick.Invoke();
			}
		}
		this.ClearShopItemSelect();
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x0008634B File Offset: 0x0008454B
	private void JoyLeftShoulder(Body body)
	{
		this.SetTabButton(-1);
	}

	// Token: 0x0600158A RID: 5514 RVA: 0x00086354 File Offset: 0x00084554
	private void JoyRightShoulder(Body body)
	{
		this.SetTabButton(1);
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x0008635D File Offset: 0x0008455D
	private void JoyA(Body body)
	{
		if (this.isActive)
		{
			this.shop.BuyItem(this.shop.shopSelectList[this.shopSelectIndex].shopItem);
		}
	}

	// Token: 0x0400143B RID: 5179
	private UI_Shop shop;

	// Token: 0x0400143C RID: 5180
	public int shopSelectIndex;

	// Token: 0x0400143D RID: 5181
	public bool isActive;

	// Token: 0x0400143E RID: 5182
	private List<GameObject> tabButtons = new List<GameObject>();

	// Token: 0x0400143F RID: 5183
	public int tabIndex;

	// Token: 0x04001440 RID: 5184
	public GameObject preTabButton;

	// Token: 0x04001441 RID: 5185
	private bool isOpen;
}
