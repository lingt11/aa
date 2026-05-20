using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003A7 RID: 935
public class UI_Shop : UGUICtrl
{
	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x06001559 RID: 5465 RVA: 0x00084138 File Offset: 0x00082338
	public UI_Shop.ShopType GetShopType
	{
		get
		{
			return this.shopType;
		}
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x00084140 File Offset: 0x00082340
	public UI_Shop()
	{
		this.shopType = UI_Shop.ShopType.Equip;
		this.selfView = new UI_Shop_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_shop", base.GetType());
		this.shopJoy = new UI_Shop_Joy(this);
		this.itemGridLayoutGroup = this.selfView.pool_buyItem.GetComponent<GridLayoutGroup>();
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x000841AC File Offset: 0x000823AC
	protected override void ButtonAddClick()
	{
		this.selfView.btn_book.AddButtonEvent(delegate
		{
			this.shopType = UI_Shop.ShopType.Book;
			this.UpdateShopTypeButtonState(UI_Shop.ShopType.Book);
			this.AddShopItem(this.selfView.pool_buyItem, EntityStatic.Get<ShopManager>().buyBookList);
			this.selfView.trans_sbg.GetComponent<RectTransform>().sizeDelta = new Vector2(1273f, 655f);
			RectTransform component = this.selfView.trans_info.GetComponent<RectTransform>();
			component.anchoredPosition = new Vector2(component.anchoredPosition.x, -270f);
			this.selfView.trans_info.gameObject.SetActive(false);
			this.UpdateGirdSize(false);
		});
		this.selfView.btn_item.AddButtonEvent(new UnityAction(this.OnBtnItemClick));
		this.selfView.btn_close.AddButtonEvent(delegate
		{
			this.CloseAnim(false, true);
		});
		this.selfView.btn_buy.AddButtonEvent(delegate
		{
			if (this.curShopItem != null)
			{
				this.BuyItem(this.curShopItem);
			}
		});
		this.selfView.btn_shop.AddButtonEvent(delegate
		{
			if (this.isOpenShop)
			{
				this.CloseAnim(false, true);
				return;
			}
			this.OpenAnim(true);
		});
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x00084248 File Offset: 0x00082448
	public void OnBtnItemClick()
	{
		this.shopType = UI_Shop.ShopType.Equip;
		this.UpdateShopTypeButtonState(UI_Shop.ShopType.Equip);
		this.AddShopItem(this.selfView.pool_buyItem, EntityStatic.Get<ShopManager>().buyItemList);
		this.selfView.trans_sbg.GetComponent<RectTransform>().sizeDelta = new Vector2(1273f, 655f);
		RectTransform component = this.selfView.trans_info.GetComponent<RectTransform>();
		component.anchoredPosition = new Vector2(component.anchoredPosition.x, -270f);
		this.selfView.trans_info.gameObject.SetActive(false);
		this.UpdateGirdSize(true);
	}

	// Token: 0x0600155D RID: 5469 RVA: 0x000842EC File Offset: 0x000824EC
	public void UpdateGirdSize(bool isEquip)
	{
		if (isEquip)
		{
			this.scrollRect.vertical = true;
			this.itemGridLayoutGroup.cellSize = new Vector2(99f, 110f);
		}
		else
		{
			this.itemGridLayoutGroup.cellSize = new Vector2(180f, 200f);
			this.scrollRect.vertical = false;
		}
		this.buyItemTransform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
	}

	// Token: 0x0600155E RID: 5470 RVA: 0x00084370 File Offset: 0x00082570
	private Image GetButtonIconImage(Button button)
	{
		Image[] componentsInChildren = button.GetComponentsInChildren<Image>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].transform != button.transform)
			{
				return componentsInChildren[i];
			}
		}
		return button.targetGraphic as Image;
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x000843B8 File Offset: 0x000825B8
	private void InitShopTypeButtonIcons()
	{
		if (this.isShopTypeButtonIconInit)
		{
			return;
		}
		this.bookButtonIcon = this.GetButtonIconImage(this.selfView.btn_book);
		this.itemButtonIcon = this.GetButtonIconImage(this.selfView.btn_item);
		if (this.bookButtonIcon != null)
		{
			this.bookButtonDefaultColor = this.bookButtonIcon.color;
		}
		if (this.itemButtonIcon != null)
		{
			this.itemButtonDefaultColor = this.itemButtonIcon.color;
		}
		this.isShopTypeButtonIconInit = true;
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x00084444 File Offset: 0x00082644
	private void UpdateShopTypeButtonState(UI_Shop.ShopType selectShopType)
	{
		this.InitShopTypeButtonIcons();
		if (this.bookButtonIcon != null)
		{
			this.bookButtonIcon.color = ((selectShopType == UI_Shop.ShopType.Book) ? UI_Shop.ShopTypeButtonSelectColor : this.bookButtonDefaultColor);
		}
		if (this.itemButtonIcon != null)
		{
			this.itemButtonIcon.color = ((selectShopType == UI_Shop.ShopType.Equip) ? UI_Shop.ShopTypeButtonSelectColor : this.itemButtonDefaultColor);
		}
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x000844AC File Offset: 0x000826AC
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.scrollRect = this.selfView.trans_scroll.GetComponent<ScrollRect>();
		this.buyItemTransform = this.selfView.pool_buyItem.GetComponent<RectTransform>();
		this.shopType = UI_Shop.ShopType.Equip;
		this.UpdateShopTypeButtonState(UI_Shop.ShopType.Equip);
		this.AddShopItem(this.selfView.pool_buyItem, EntityStatic.Get<ShopManager>().buyItemList);
		this.CloseAnim(true, false);
		this.selfView.trans_info.gameObject.SetActive(false);
		this.selfView.trans_ItemDetail.gameObject.SetActive(false);
		this.selfView.trans_sbg.GetComponent<RectTransform>().sizeDelta = new Vector2(1273f, 655f);
		RectTransform component = this.selfView.trans_info.GetComponent<RectTransform>();
		component.anchoredPosition = new Vector2(component.anchoredPosition.x, -270f);
		this.UpdateGirdSize(true);
		if (GameHelperClient.isSaveShop)
		{
			this.selfView.btn_shop.gameObject.SetActive(false);
			this.selfView.trans_bg.gameObject.SetActive(false);
		}
		this.UpdateRefreshNum();
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ClosePanel()
	{
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x000845D7 File Offset: 0x000827D7
	public void ShowShopPanel()
	{
		this.AddShopItem(this.selfView.pool_buyItem, this.curShopItemList);
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x000845F0 File Offset: 0x000827F0
	private void AddShopItem(PoolView poolView, List<ShopItem> listData)
	{
		if (this.selectObject != null)
		{
			this.selectObject.SetActive(false);
		}
		this.curShopItemList = listData;
		poolView.RemoveAllView();
		this.shopSelectList.Clear();
		this.shopJoy.shopSelectIndex = 0;
		this.shopJoy.isActive = false;
		this.selfView.ltext_info.text = "";
		int i = 0;
		while (i < listData.Count)
		{
			GameObject gameObject = poolView.AddView();
			Shop_BuyItem component = gameObject.GetComponent<Shop_BuyItem>();
			component.shopItem = listData[i];
			ShopItem data = listData[i];
			if (this.shopType != UI_Shop.ShopType.Equip)
			{
				component.root.localScale = Vector3.one;
				goto IL_F6;
			}
			component.root.localScale = new Vector3(0.55f, 0.55f, 0.55f);
			if (!data.id.Equals("empty"))
			{
				goto IL_F6;
			}
			component.UpdateShow(false);
			IL_368:
			i++;
			continue;
			IL_F6:
			component.UpdateShow(true);
			component.SetShopId(data.id);
			string text = Game.Language.Get(data.id, "");
			component.name.text = text;
			GameObject frame = component.frame;
			this.shopSelectList.Add(component);
			frame.SetActive(false);
			data.cdImage = component.cdImg;
			string iconPath = data.iconPath;
			component.icon.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Shop/" + iconPath);
			int num = data.gold;
			if (!Mathf.Approximately(GameHelperClient.localPlayer.GetShopDiscountAdd(), 0f))
			{
				num = Mathf.RoundToInt((float)num * (1f + GameHelperClient.localPlayer.GetShopDiscountAdd()));
			}
			int num2 = data.gem;
			if (num2 > 0 && !Mathf.Approximately(GameHelperClient.localPlayer.GetShopDiscountAdd(), 0f))
			{
				num2 = Mathf.Max(1, Mathf.RoundToInt((float)num2 * (1f + GameHelperClient.localPlayer.GetShopDiscountAdd())));
			}
			Transform goldGo = component.goldGo;
			if (num > 0)
			{
				goldGo.GetComponent<Text>().text = num.ToString();
				if (!goldGo.gameObject.activeSelf)
				{
					goldGo.gameObject.SetActive(true);
				}
			}
			else if (goldGo.gameObject.activeSelf)
			{
				goldGo.gameObject.SetActive(false);
			}
			Transform gemGo = component.gemGo;
			if (num2 > 0)
			{
				gemGo.GetComponent<Text>().text = num2.ToString();
				if (!gemGo.gameObject.activeSelf)
				{
					gemGo.gameObject.SetActive(true);
				}
			}
			else if (gemGo.gameObject.activeSelf)
			{
				gemGo.gameObject.SetActive(false);
			}
			if (num == 0 && num2 == 0)
			{
				component.name.rectTransform.anchoredPosition = new Vector2(0f, -80f);
			}
			else
			{
				component.name.rectTransform.anchoredPosition = new Vector2(0f, -42f);
			}
			gameObject.transform.GetComponent<Button>().AddButtonEvent(delegate
			{
				if (this.shopType == UI_Shop.ShopType.Equip)
				{
					this.selfView.ltext_info.text = EquipBase.GetEquipInfo(data.id);
				}
				else if (this.shopType == UI_Shop.ShopType.Book)
				{
					this.selfView.ltext_info.text = Game.Language.Get(data.id + "_m", "");
				}
				else if (this.shopType == UI_Shop.ShopType.Medicine)
				{
					string text2 = Game.Language.Get(data.id + "_m", "");
					if (data.strValues != null)
					{
						string format = text2;
						object[] strValues = data.strValues;
						text2 = string.Format(format, strValues);
					}
					string str = string.Format(Game.Language.Get("持续回合", ""), string.Format(ColorDefine.NormalColor, data.times));
					text2 = text2 + "\n" + str;
					this.selfView.ltext_info.text = text2;
				}
				this.curShopItem = data;
				if (this.selectObject != null)
				{
					this.selectObject.SetActive(false);
				}
				this.selectObject = frame;
				this.selectObject.SetActive(true);
				if (!this.selfView.trans_info.gameObject.activeSelf)
				{
					this.selfView.trans_info.gameObject.SetActive(true);
				}
				Shop_BuyItem component2 = this.selfView.trans_buyItem.GetComponent<Shop_BuyItem>();
				component2.name.text = Game.Language.Get(data.id, "");
				component2.icon.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Shop/" + this.curShopItem.iconPath);
				int num3 = data.gold;
				if (!Mathf.Approximately(GameHelperClient.localPlayer.GetShopDiscountAdd(), 0f))
				{
					num3 = Mathf.RoundToInt((float)num3 * (1f + GameHelperClient.localPlayer.GetShopDiscountAdd()));
				}
				if (num3 > 0)
				{
					component2.goldGo.GetComponent<Text>().text = num3.ToString();
					if (!component2.goldGo.gameObject.activeSelf)
					{
						component2.goldGo.gameObject.SetActive(true);
					}
				}
				else if (component2.goldGo.gameObject.activeSelf)
				{
					component2.goldGo.gameObject.SetActive(false);
				}
				int num4 = data.gem;
				if (num4 > 0 && !Mathf.Approximately(GameHelperClient.localPlayer.GetShopDiscountAdd(), 0f))
				{
					num4 = Mathf.Max(1, Mathf.RoundToInt((float)num4 * (1f + GameHelperClient.localPlayer.GetShopDiscountAdd())));
				}
				if (num4 > 0)
				{
					component2.gemGo.GetComponent<Text>().text = num4.ToString();
					if (!component2.gemGo.gameObject.activeSelf)
					{
						component2.gemGo.gameObject.SetActive(true);
						return;
					}
				}
				else if (component2.gemGo.gameObject.activeSelf)
				{
					component2.gemGo.gameObject.SetActive(false);
				}
			});
			gameObject.transform.GetComponent<UIDoubleClick>().clickEvent = delegate()
			{
				this.BuyItem(data);
			};
			gameObject.transform.GetComponent<UIDoubleClick>().rightClickEvent = delegate()
			{
				this.BuyItem(data);
			};
			goto IL_368;
		}
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x00084978 File Offset: 0x00082B78
	public void BuyItem(ShopItem shopItem)
	{
		if (this.selfView.trans_info.gameObject.activeSelf)
		{
			this.selfView.trans_info.gameObject.SetActive(false);
		}
		string id = shopItem.id;
		int num = shopItem.gold;
		if (!Mathf.Approximately(GameHelperClient.localPlayer.GetShopDiscountAdd(), 0f))
		{
			num = Mathf.RoundToInt((float)num * (1f + GameHelperClient.localPlayer.GetShopDiscountAdd()));
		}
		int num2 = shopItem.gem;
		if (num2 > 0 && !Mathf.Approximately(GameHelperClient.localPlayer.GetShopDiscountAdd(), 0f))
		{
			num2 = Mathf.Max(1, Mathf.RoundToInt((float)num2 * (1f + GameHelperClient.localPlayer.GetShopDiscountAdd())));
		}
		if (GameHelperClient.localPlayer.gold < num)
		{
			Util.ShowTips("noJin");
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
			return;
		}
		if (GameHelperClient.localPlayer.gem < num2)
		{
			Util.ShowTips("noTou");
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
			return;
		}
		if (shopItem.cd > 0f)
		{
			Util.ShowTips("tip_buyCD");
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
			return;
		}
		if (!Util.CheckCanRoguelike())
		{
			return;
		}
		if (this.shopType == UI_Shop.ShopType.Book && GameHelperClient.localPlayer.playerAttribute.BagIsFull())
		{
			Util.ShowTips("背包已满");
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
			return;
		}
		if (this.shopType == UI_Shop.ShopType.Equip)
		{
			ShopManager.ShopBuyResult shopBuyResult = EntityStatic.Get<ShopManager>().OnBuyItem(shopItem);
			if (shopBuyResult == ShopManager.ShopBuyResult.Fail)
			{
				Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
				return;
			}
			if (shopBuyResult == ShopManager.ShopBuyResult.SuccessNoShopCost)
			{
				Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
				return;
			}
			if (num != 0)
			{
				GameHelperClient.localPlayer.AddGold(GameHelperClient.localPlayer.GetHeadUIPos(), -num, true);
			}
			if (num2 != 0)
			{
				GameHelperClient.localPlayer.AddGem(GameHelperClient.localPlayer.GetHeadUIPos(), -num2, false);
			}
			shopItem.cd = shopItem.cdSet;
			PlayerBase.BuyItem buyItemEvent = GameHelperClient.localPlayer.buyItemEvent;
			if (buyItemEvent != null)
			{
				buyItemEvent();
			}
			if ((shopItem.goldAdd > 0 || shopItem.gemAdd > 0) && shopItem.ApplyPriceGrowth())
			{
				UI_Shop ui = Game.UI.GetUI<UI_Shop>();
				if (ui == null)
				{
					return;
				}
				ui.ShowShopPanel();
				return;
			}
		}
		else if (this.shopType == UI_Shop.ShopType.Book)
		{
			EntityStatic.Get<ShopManager>().BuyBook(shopItem.id);
			if (num != 0)
			{
				GameHelperClient.localPlayer.AddGold(GameHelperClient.localPlayer.GetHeadUIPos(), -num, true);
			}
			if (num2 != 0)
			{
				GameHelperClient.localPlayer.AddGem(GameHelperClient.localPlayer.GetHeadUIPos(), -num2, false);
			}
			shopItem.cd = shopItem.cdSet;
			PlayerBase.BuyItem buyItemEvent2 = GameHelperClient.localPlayer.buyItemEvent;
			if (buyItemEvent2 == null)
			{
				return;
			}
			buyItemEvent2();
			return;
		}
		else if (this.shopType == UI_Shop.ShopType.Medicine)
		{
			EntityStatic.Get<ShopManager>().BuyMedicine(shopItem);
			if (num != 0)
			{
				GameHelperClient.localPlayer.AddGold(GameHelperClient.localPlayer.GetHeadUIPos(), -num, true);
			}
			if (num2 != 0)
			{
				GameHelperClient.localPlayer.AddGem(GameHelperClient.localPlayer.GetHeadUIPos(), -num2, false);
			}
			shopItem.cd = shopItem.cdSet;
			PlayerBase.BuyItem buyItemEvent3 = GameHelperClient.localPlayer.buyItemEvent;
			if (buyItemEvent3 == null)
			{
				return;
			}
			buyItemEvent3();
		}
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x00084CC4 File Offset: 0x00082EC4
	public void CloseAnim(bool isInit = false, bool isPlaySound = true)
	{
		if (isInit)
		{
			RectTransform component = this.selfView.trans_bg.GetComponent<RectTransform>();
			component.anchoredPosition = new Vector2(-1000f, component.anchoredPosition.y);
		}
		else
		{
			if (this.isOpenShop && isPlaySound)
			{
				EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/关闭商店", 0.5f, 3f);
			}
			this.selfView.trans_bg.GetComponent<RectTransform>().DOAnchorPosX(-1000f, 0.2f, false);
		}
		this.isOpenShop = false;
		Game.UI.GetUI<UI_Msg>().Show();
		this.shopJoy.Close();
		UI_DropGold ui = Game.UI.GetUI<UI_DropGold>();
		if (ui != null && ui.isOpen)
		{
			Game.UI.CloseUI<UI_DropGold>();
		}
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x00084D88 File Offset: 0x00082F88
	public void OpenAnim(bool isPlaySound)
	{
		if (!GameHelperClient.isReady || GameHelperClient.isSaveShop)
		{
			return;
		}
		if (isPlaySound)
		{
			EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/打开商店", 0.45f, 3f);
		}
		this.isOpenShop = true;
		this.selfView.trans_bg.GetComponent<RectTransform>().anchoredPosition = new Vector2(-900f, -475f);
		this.selfView.trans_bg.GetComponent<RectTransform>().DOAnchorPosX(23f, 0.2f, false);
		this.UpdateRefreshNum();
		Game.UI.GetUI<UI_Msg>().Hide();
		this.shopJoy.Open();
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x00084E2D File Offset: 0x0008302D
	public void UpdateRefreshNum()
	{
		this.selfView.text_refresh.text = GameHelperClient.RefreshNum.ToString();
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x00084E4C File Offset: 0x0008304C
	public override void Update()
	{
		if (!this.isOpenShop)
		{
			return;
		}
		this.scrollRect.content.sizeDelta = new Vector2(this.scrollRect.content.sizeDelta.x, this.buyItemTransform.sizeDelta.y);
		if (this.curShopItemList != null)
		{
			for (int i = 0; i < this.curShopItemList.Count; i++)
			{
				ShopItem shopItem = this.curShopItemList[i];
				if (shopItem.cdImage)
				{
					shopItem.cdImage.fillAmount = shopItem.cd / shopItem.cdSet;
				}
			}
		}
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x00084EEC File Offset: 0x000830EC
	public void OnBuyMonsterBtnClick(int curBuyMonsterIndex)
	{
		this.buyMonsterIndex = curBuyMonsterIndex;
		string[] monsterList = GameHelperClient.spawnConfig.BuyMonsterTime[this.buyMonsterIndex].monsterList;
		for (int i = 0; i < monsterList.Length; i++)
		{
			int num = Random.Range(0, monsterList.Length);
			ref string ptr = ref monsterList[i];
			string[] array = monsterList;
			int num2 = num;
			string text = monsterList[num];
			string text2 = monsterList[i];
			ptr = text;
			array[num2] = text2;
		}
		UI_Roguelike ui_Roguelike = Game.UI.OpenUI<UI_Roguelike>(null) as UI_Roguelike;
		RoguelikeUIData[] array2 = new RoguelikeUIData[3];
		for (int j = 0; j < 3; j++)
		{
			RoguelikeUIData roguelikeUIData = default(RoguelikeUIData);
			string text3 = monsterList[j];
			object dic = ExcelManager.allExcelData["shop"].DIC(text3);
			roguelikeUIData.name = Game.Language.Get(text3, "");
			roguelikeUIData.icon = "Bundles/UI/Icon/Shop/" + dic.DIC("icon");
			roguelikeUIData.dec = Game.Language.Get(text3 + "_m", "");
			int num3 = dic.DIC("type");
			if (num3 > 0)
			{
				string key = text3.Equals("monster_11") ? EnemyType.NPC_King.ToString() : (text3.Equals("monster_10") ? EnemyType.SaiYa.ToString() : ((EnemyType)(num3 + this.buyMonsterIndex)).ToString());
				RoleAttribute roleAttribute;
				if (Game.GameData.HeroAttributeDic.TryGetValue(key, out roleAttribute))
				{
					SOSpawnConfig.EnemySpawnTime enemySpawnTime = GameHelperClient.spawnConfig.enemySpawnData[GameHelperClient.WaveNum];
					roguelikeUIData.dec = string.Format(ColorDefine.QuaRelicText[4], string.Concat(new string[]
					{
						"HP:",
						Mathf.RoundToInt((float)roleAttribute.hp * enemySpawnTime.bossHpLevel).ToString(),
						"   ",
						Game.Language.Get("attack", ""),
						":",
						Mathf.RoundToInt((float)roleAttribute.attackPower * enemySpawnTime.bossAttackLevel).ToString()
					})) + "\n\n" + roguelikeUIData.dec;
				}
			}
			roguelikeUIData.data = text3;
			roguelikeUIData.quality = -1;
			array2[j] = roguelikeUIData;
		}
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/购买怪物", 1f, 3f);
		ui_Roguelike.ShowRoguelike(array2, new Action<RoguelikeUIData>(this.OnMonsterRoguelike), Game.Language.Get("挑战选择", ""), null, null, 0f, null, "monster_challenge");
	}

	// Token: 0x0600156B RID: 5483 RVA: 0x000851AC File Offset: 0x000833AC
	private void OnMonsterRoguelike(RoguelikeUIData roguelikeUIData)
	{
		if (roguelikeUIData.data.Equals("monster_10"))
		{
			Game.TimerManager.AddTimer(0.1f, new Action(this.OnSaiYaMonster));
			return;
		}
		this.monsterRoguelikeUIData = roguelikeUIData;
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/购买怪物", 1f, 3f);
		GameHelperClient.localPlayer.CmdChat(Game.Language.Get("选择挑战提示", "") + string.Format(ColorDefine.NormalColor, roguelikeUIData.name));
	}

	// Token: 0x0600156C RID: 5484 RVA: 0x0008523C File Offset: 0x0008343C
	private void StartBuyMonster(RoguelikeUIData roguelikeUIData, bool isQiJiXingZhe, int index)
	{
		string data = roguelikeUIData.data;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(data);
		if (num <= 2498750696U)
		{
			if (num <= 474772178U)
			{
				if (num != 441216940U)
				{
					if (num != 457994559U)
					{
						if (num != 474772178U)
						{
							return;
						}
						if (!(data == "monster_10_2"))
						{
							return;
						}
						if (!isQiJiXingZhe)
						{
							List<EquipBase> equipList = GameHelperClient.localPlayer.playerAttribute.equipList;
							if (equipList != null && equipList.Count > 0)
							{
								EquipBase equipBase = equipList[Random.Range(0, equipList.Count)];
								Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("失去说明", ""), equipBase.name));
								GameHelperClient.localPlayer.playerAttribute.SellEquip(equipBase, true);
							}
							GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.SaiYa, false);
							return;
						}
					}
					else
					{
						if (!(data == "monster_10_1"))
						{
							return;
						}
						if (!isQiJiXingZhe)
						{
							List<SkillBase> roleSkillList = GameHelperClient.localPlayer.roleSkillList;
							if (roleSkillList != null && roleSkillList.Count > 1)
							{
								SkillBase skillBase = roleSkillList[Random.Range(1, roleSkillList.Count)];
								Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("失去说明", ""), skillBase.languageName));
								Util.RemoveSkill(skillBase);
								UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
								if (ui != null)
								{
									ui.RefreshPlayerSkill();
								}
							}
							GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.SaiYa, false);
							return;
						}
					}
				}
				else
				{
					if (!(data == "monster_10_0"))
					{
						return;
					}
					if (!isQiJiXingZhe)
					{
						List<RelicBase> relicList = GameHelperClient.localPlayer.playerAttribute.relicList;
						if (relicList != null && relicList.Count > 0)
						{
							RelicBase relicBase = relicList[Random.Range(0, relicList.Count)];
							string str = relicBase.relicData.DIC("id");
							Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("失去说明", ""), Game.Language.Get("pickitem_" + str, "")));
							GameHelperClient.localPlayer.RemoveRelic(relicBase);
						}
						GameHelperClient.localPlayer.CmdCreateEnemy(EnemyType.SaiYa, false);
						return;
					}
				}
			}
			else if (num != 2398084982U)
			{
				if (num != 2414862601U)
				{
					if (num != 2498750696U)
					{
						return;
					}
					if (!(data == "monster_2"))
					{
						return;
					}
					EntityStatic.Get<ShopManager>().monster_2();
					return;
				}
				else
				{
					if (!(data == "monster_9"))
					{
						return;
					}
					EntityStatic.Get<ShopManager>().monster_9(this.buyMonsterIndex);
					return;
				}
			}
			else
			{
				if (!(data == "monster_8"))
				{
					return;
				}
				EntityStatic.Get<ShopManager>().monster_8(this.buyMonsterIndex);
				return;
			}
		}
		else if (num <= 2565861172U)
		{
			if (num != 2515528315U)
			{
				if (num != 2549083553U)
				{
					if (num != 2565861172U)
					{
						return;
					}
					if (!(data == "monster_6"))
					{
						return;
					}
					if (isQiJiXingZhe)
					{
						Game.TimerManager.AddTimer((float)(index + 1) * 2f, delegate()
						{
							EntityStatic.Get<ShopManager>().monster_6(this.buyMonsterIndex);
						});
						return;
					}
					EntityStatic.Get<ShopManager>().monster_6(this.buyMonsterIndex);
					return;
				}
				else
				{
					if (!(data == "monster_1"))
					{
						return;
					}
					if (!isQiJiXingZhe)
					{
						EntityStatic.Get<ShopManager>().monster_1();
						return;
					}
					if (index == 0)
					{
						EntityStatic.Get<ShopManager>().monster_2();
						return;
					}
					if (index == 1)
					{
						EntityStatic.Get<ShopManager>().monster_3();
						return;
					}
					if (index == 2)
					{
						EntityStatic.Get<ShopManager>().monster_4();
						return;
					}
				}
			}
			else
			{
				if (!(data == "monster_3"))
				{
					return;
				}
				EntityStatic.Get<ShopManager>().monster_3();
				return;
			}
		}
		else if (num <= 2599416410U)
		{
			if (num != 2582638791U)
			{
				if (num != 2599416410U)
				{
					return;
				}
				if (!(data == "monster_4"))
				{
					return;
				}
				EntityStatic.Get<ShopManager>().monster_4();
				return;
			}
			else
			{
				if (!(data == "monster_7"))
				{
					return;
				}
				EntityStatic.Get<ShopManager>().monster_7(this.buyMonsterIndex);
				return;
			}
		}
		else if (num != 2616194029U)
		{
			if (num != 3199400368U)
			{
				return;
			}
			if (!(data == "monster_11"))
			{
				return;
			}
			if (!isQiJiXingZhe)
			{
				RoleAttribute roleAttribute = Game.GameData.HeroAttributeDic[EnemyType.NPC_King.ToString()];
				GameHelperClient.localPlayer.StartSummon(EnemyType.NPC_King, GameHelperClient.localPlayer.MyTransform.position + GameHelperClient.localPlayer.MyTransform.forward * 3f, GameHelperClient.localPlayer.netId, roleAttribute.attackSpeed, (long)roleAttribute.hp, roleAttribute.attackPower, 9999f, null, 0L, 0L, -1);
			}
		}
		else
		{
			if (!(data == "monster_5"))
			{
				return;
			}
			EntityStatic.Get<ShopManager>().monster_5(this.buyMonsterIndex);
			return;
		}
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x000856E0 File Offset: 0x000838E0
	public void OnSaiYaMonster()
	{
		UI_Roguelike ui_Roguelike = Game.UI.OpenUI<UI_Roguelike>(null) as UI_Roguelike;
		RoguelikeUIData[] array = new RoguelikeUIData[3];
		string[] array2 = new string[]
		{
			"献上遗物",
			"献上能力",
			"献上宝物"
		};
		string[] array3 = new string[]
		{
			"remain_remains",
			"remain_skill",
			"remain_equip"
		};
		string[] array4 = new string[]
		{
			"献上遗物说明",
			"献上能力说明",
			"献上宝物说明"
		};
		for (int i = 0; i < 3; i++)
		{
			RoguelikeUIData roguelikeUIData = default(RoguelikeUIData);
			roguelikeUIData.name = Game.Language.Get(array2[i], "");
			roguelikeUIData.icon = "Bundles/UI/Icon/Remains/" + array3[i];
			roguelikeUIData.dec = Game.Language.Get(array4[i], "");
			roguelikeUIData.data = "monster_10_" + i.ToString();
			roguelikeUIData.quality = 3;
			array[i] = roguelikeUIData;
		}
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/购买怪物", 1f, 3f);
		ui_Roguelike.ShowRoguelike(array, new Action<RoguelikeUIData>(this.OnSaiYaRoguelike), Game.Language.Get("塞亚挑战", ""), null, null, 0f, null, "saiya_challenge");
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x00085844 File Offset: 0x00083A44
	private void OnSaiYaRoguelike(RoguelikeUIData roguelikeUIData)
	{
		this.monsterRoguelikeUIData = roguelikeUIData;
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/购买怪物", 1f, 3f);
		UI_Battle ui = Game.UI.GetUI<UI_Battle>();
		if (ui != null)
		{
			ui.readySyncData = this.monsterRoguelikeUIData.data;
		}
	}

	// Token: 0x0600156F RID: 5487 RVA: 0x00085894 File Offset: 0x00083A94
	public void OnStartRest()
	{
		this.monsterRoguelikeUIData.data = "";
		if (!GameHelperClient.isSaveShop)
		{
			this.selfView.btn_shop.gameObject.SetActive(true);
			this.selfView.trans_bg.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x000858E4 File Offset: 0x00083AE4
	public void OnStartGame()
	{
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui != null)
		{
			ui.ShowEquipInfo(false, Vector3.zero, new EquipBase());
		}
		this.OnBtnItemClick();
		this.selfView.btn_shop.gameObject.SetActive(false);
		this.selfView.trans_bg.gameObject.SetActive(false);
		if (!string.IsNullOrEmpty(this.monsterRoguelikeUIData.data))
		{
			this.StartBuyMonster(this.monsterRoguelikeUIData, false, 0);
			if (GameHelperClient.IsQiJiXingZhe > 0)
			{
				for (int i = 0; i < GameHelperClient.IsQiJiXingZhe; i++)
				{
					this.StartBuyMonster(this.monsterRoguelikeUIData, true, i);
				}
			}
		}
	}

	// Token: 0x04001410 RID: 5136
	public UI_Shop_View selfView;

	// Token: 0x04001411 RID: 5137
	private ShopItem curShopItem;

	// Token: 0x04001412 RID: 5138
	private List<ShopItem> curShopItemList;

	// Token: 0x04001413 RID: 5139
	private GameObject selectObject;

	// Token: 0x04001414 RID: 5140
	public bool isOpenShop;

	// Token: 0x04001415 RID: 5141
	private UI_Shop_Joy shopJoy;

	// Token: 0x04001416 RID: 5142
	private GridLayoutGroup itemGridLayoutGroup;

	// Token: 0x04001417 RID: 5143
	private int buyMonsterIndex;

	// Token: 0x04001418 RID: 5144
	private RoguelikeUIData monsterRoguelikeUIData;

	// Token: 0x04001419 RID: 5145
	private ScrollRect scrollRect;

	// Token: 0x0400141A RID: 5146
	private RectTransform buyItemTransform;

	// Token: 0x0400141B RID: 5147
	private const float EquipItemScale = 0.55f;

	// Token: 0x0400141C RID: 5148
	private static readonly Color ShopTypeButtonSelectColor = new Color(0.9137255f, 0.6235294f, 0.2313726f, 0.75f);

	// Token: 0x0400141D RID: 5149
	private Image bookButtonIcon;

	// Token: 0x0400141E RID: 5150
	private Image itemButtonIcon;

	// Token: 0x0400141F RID: 5151
	private Color bookButtonDefaultColor;

	// Token: 0x04001420 RID: 5152
	private Color itemButtonDefaultColor;

	// Token: 0x04001421 RID: 5153
	private bool isShopTypeButtonIconInit;

	// Token: 0x04001422 RID: 5154
	private UI_Shop.ShopType shopType;

	// Token: 0x04001423 RID: 5155
	public List<Shop_BuyItem> shopSelectList = new List<Shop_BuyItem>(8);

	// Token: 0x020003A8 RID: 936
	public enum ShopType
	{
		// Token: 0x04001425 RID: 5157
		Book,
		// Token: 0x04001426 RID: 5158
		Equip,
		// Token: 0x04001427 RID: 5159
		Medicine
	}
}
