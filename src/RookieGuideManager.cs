using System;
using UnityEngine;

// Token: 0x0200014E RID: 334
public class RookieGuideManager
{
	// Token: 0x06000673 RID: 1651 RVA: 0x000271A4 File Offset: 0x000253A4
	public void InitRookieGuideManager(int localGoldDashGuide)
	{
		if ((localGoldDashGuide & 4) == 0 && !GameHelperClient.IsJoyStick)
		{
			this.StartGuide(RookieGuideManager.RookieGuideMask.UseSkillBookKey);
		}
		if ((localGoldDashGuide & 1) == 0 && !GameHelperClient.IsJoyStick)
		{
			this.StartGuide(RookieGuideManager.RookieGuideMask.MoveKey);
			return;
		}
		if ((localGoldDashGuide & 2) == 0 && !GameHelperClient.IsJoyStick)
		{
			this.StartGuide(RookieGuideManager.RookieGuideMask.ShopKey);
			return;
		}
		if ((localGoldDashGuide & 8) == 0 && !GameHelperClient.IsJoyStick)
		{
			this.StartGuide(RookieGuideManager.RookieGuideMask.ShopSkillKey);
			return;
		}
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00027200 File Offset: 0x00025400
	public void StartGuide(RookieGuideManager.RookieGuideMask rookieGuideMask)
	{
		this.currentMask = rookieGuideMask;
		if (this.currentMask == RookieGuideManager.RookieGuideMask.MoveKey)
		{
			Game.UI.GetUI<UI_Battle>().ShowGameStartBtn(false);
			Game.UI.OpenUI<UI_RookieMoveKey>(null);
			return;
		}
		if (this.currentMask == RookieGuideManager.RookieGuideMask.ShopKey)
		{
			Game.UI.GetUI<UI_Battle>().ShowGameStartBtn(false);
			Game.UI.OpenUI<UI_OpenShopKey>(null);
			return;
		}
		if (this.currentMask == RookieGuideManager.RookieGuideMask.UseSkillBookKey)
		{
			PlayerBase localPlayer = GameHelperClient.localPlayer;
			localPlayer.addBagItemEvent = (PlayerBase.AddBagItem)Delegate.Combine(localPlayer.addBagItemEvent, new PlayerBase.AddBagItem(this.OnAddBagItemEvent));
			return;
		}
		if (this.currentMask == RookieGuideManager.RookieGuideMask.ShopSkillKey && Game.UI.GetUI<UI_Shop>().isOpenShop)
		{
			Game.UI.OpenUI<UI_ShopSkillKey>(null);
		}
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x000272B8 File Offset: 0x000254B8
	private void OnAddBagItemEvent(ItemType itemType)
	{
		if (itemType >= ItemType.Active_Book_D && itemType < ItemType.GoldCoinBag_LV1)
		{
			Game.UI.OpenUI<UI_UseSkillBookKey>(null);
			PlayerBase localPlayer = GameHelperClient.localPlayer;
			localPlayer.addBagItemEvent = (PlayerBase.AddBagItem)Delegate.Remove(localPlayer.addBagItemEvent, new PlayerBase.AddBagItem(this.OnAddBagItemEvent));
			PlayerBase localPlayer2 = GameHelperClient.localPlayer;
			localPlayer2.useItemEvent = (PlayerBase.UseItem)Delegate.Combine(localPlayer2.useItemEvent, new PlayerBase.UseItem(this.OnUseItemEvent));
		}
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x00027330 File Offset: 0x00025530
	private void OnUseItemEvent(ItemType itemType)
	{
		if (itemType >= ItemType.Active_Book_D && itemType < ItemType.GoldCoinBag_LV1)
		{
			UI_UseSkillBookKey ui = Game.UI.GetUI<UI_UseSkillBookKey>();
			if (ui != null)
			{
				ui.AfterDestroy();
			}
			PlayerBase localPlayer = GameHelperClient.localPlayer;
			localPlayer.useItemEvent = (PlayerBase.UseItem)Delegate.Remove(localPlayer.useItemEvent, new PlayerBase.UseItem(this.OnUseItemEvent));
			this.CompleteGuide(RookieGuideManager.RookieGuideMask.UseSkillBookKey);
		}
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x00027390 File Offset: 0x00025590
	private void CompleteGuide(RookieGuideManager.RookieGuideMask rookieGuideMask)
	{
		int num = Game.Save.Check("RookieGuideMask") ? Game.Save.LoadInt("RookieGuideMask") : 0;
		num = (int)(num + rookieGuideMask);
		Game.Save.SaveInt("RookieGuideMask", num);
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x000273D8 File Offset: 0x000255D8
	public void Update()
	{
		if (this.currentMask == RookieGuideManager.RookieGuideMask.None)
		{
			return;
		}
		if (this.currentMask == RookieGuideManager.RookieGuideMask.MoveKey)
		{
			float horizontal = InputManager.Horizontal;
			float vertical = InputManager.Vertical;
			if (!Mathf.Approximately(horizontal, 0f) || !Mathf.Approximately(vertical, 0f))
			{
				this.CompleteGuide(this.currentMask);
				this.currentMask = RookieGuideManager.RookieGuideMask.None;
				UI_RookieMoveKey ui = Game.UI.GetUI<UI_RookieMoveKey>();
				if (ui == null)
				{
					return;
				}
				ui.AfterDestroy();
				return;
			}
		}
		else if (this.currentMask == RookieGuideManager.RookieGuideMask.ShopKey)
		{
			if (Game.UI.GetUI<UI_Shop>().isOpenShop)
			{
				this.CompleteGuide(this.currentMask);
				this.currentMask = RookieGuideManager.RookieGuideMask.None;
				Game.UI.GetUI<UI_Battle>().ShowGameStartBtn(true);
				UI_OpenShopKey ui2 = Game.UI.GetUI<UI_OpenShopKey>();
				if (ui2 == null)
				{
					return;
				}
				ui2.AfterDestroy();
				return;
			}
		}
		else if (this.currentMask == RookieGuideManager.RookieGuideMask.ShopSkillKey)
		{
			UI_Shop ui3 = Game.UI.GetUI<UI_Shop>();
			if (ui3 != null)
			{
				if (ui3.isOpenShop && ui3.GetShopType == UI_Shop.ShopType.Book)
				{
					this.CompleteGuide(this.currentMask);
					this.currentMask = RookieGuideManager.RookieGuideMask.None;
					UI_ShopSkillKey ui4 = Game.UI.GetUI<UI_ShopSkillKey>();
					if (ui4 == null)
					{
						return;
					}
					ui4.AfterDestroy();
					return;
				}
				else if (!ui3.isOpenShop)
				{
					if (Game.UI.GetUI<UI_ShopSkillKey>() != null)
					{
						UI_ShopSkillKey ui5 = Game.UI.GetUI<UI_ShopSkillKey>();
						if (ui5.isOpen)
						{
							ui5.CloseSelfPanel();
							return;
						}
					}
				}
				else if (Game.UI.GetUI<UI_ShopSkillKey>() != null)
				{
					if (!Game.UI.GetUI<UI_ShopSkillKey>().isOpen)
					{
						Game.UI.OpenUI<UI_ShopSkillKey>(null);
						return;
					}
				}
				else
				{
					Game.UI.OpenUI<UI_ShopSkillKey>(null);
				}
			}
		}
	}

	// Token: 0x04000940 RID: 2368
	private RookieGuideManager.RookieGuideMask currentMask;

	// Token: 0x0200014F RID: 335
	public enum RookieGuideMask
	{
		// Token: 0x04000942 RID: 2370
		None,
		// Token: 0x04000943 RID: 2371
		MoveKey,
		// Token: 0x04000944 RID: 2372
		ShopKey,
		// Token: 0x04000945 RID: 2373
		UseSkillBookKey = 4,
		// Token: 0x04000946 RID: 2374
		ShopSkillKey = 8
	}
}
