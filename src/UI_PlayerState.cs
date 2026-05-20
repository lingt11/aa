using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Token: 0x0200036D RID: 877
public class UI_PlayerState : UGUICtrl
{
	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0007AAE1 File Offset: 0x00078CE1
	public bool IsSwitchSkill
	{
		get
		{
			return this.isSwitchSkill && this.selfView.trans_switchSkill.gameObject.activeSelf;
		}
	}

	// Token: 0x060013EA RID: 5098 RVA: 0x0007AB04 File Offset: 0x00078D04
	public UI_PlayerState()
	{
		this.selfView = new UI_PlayerState_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_playerState", base.GetType());
		this.upLevelAnimator = this.selfView.trans_PlayerLevel.gameObject.GetComponent<Animator>();
		this.playerStateJoy = new UI_PlayerState_Joy(this);
		this.skillPanelTransform = this.selfView.pool_skillPanel.GetComponent<RectTransform>();
		this.skillPanelLayoutGroup = this.selfView.pool_skillPanel.GetComponent<HorizontalLayoutGroup>();
		this.defaultSkillPanelPosition = this.skillPanelTransform.anchoredPosition;
		this.defaultSkillPanelLayoutGroupSpacing = this.skillPanelLayoutGroup.spacing;
		this.equipPanelTransform = this.selfView.pool_equip.GetComponent<RectTransform>();
		this.myTalkUIPrefab = this.selfView.trans_talk.GetChild(0).GetComponent<MyTalkUI>();
	}

	// Token: 0x060013EB RID: 5099 RVA: 0x0007AC30 File Offset: 0x00078E30
	private void FindUI()
	{
		this.InitSkillUI();
		this.UpdateEquipUI();
		if (GameHelperClient.MaxSkillNum > 4)
		{
			this.UpdateSkillMax();
		}
		if (GameHelperClient.MaxEquipNum > 6)
		{
			this.UpdateEquipMax();
		}
		this.GetMap().enemyTransList = Game.EnemyManagerClient.clientEnemyList;
		this.GetHeroMap().enemyTransList = Game.PlayerManagerClient.clientPlayerList;
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x0007AC90 File Offset: 0x00078E90
	public void InitSkillUI()
	{
		int maxSkillNum = GameHelperClient.MaxSkillNum;
		int count = this.skillList.Count;
		if (maxSkillNum > count)
		{
			for (int i = count; i < maxSkillNum; i++)
			{
				this.skillList.Add(new SkillUI());
			}
			for (int j = count; j < maxSkillNum; j++)
			{
				PlayerState_Skill component = this.selfView.pool_skillPanel.AddView().transform.GetComponent<PlayerState_Skill>();
				SkillUI skillUI = this.skillList[j];
				skillUI.icon = component.icon;
				skillUI.imgCD = component.cdImg;
				skillUI.textCDNum = component.cdText;
				skillUI.skillUITouch = component.skillUITouch;
				skillUI.costMp = component.textCostMp;
				skillUI.customInputUI = component.customInputUI;
				skillUI.customInputUI.transform.parent.gameObject.SetActive(false);
				skillUI.icon.gameObject.SetActive(false);
				skillUI.costMp.text = "";
				skillUI.switchGo = component.switchGo;
				skillUI.playerStateSkill = component;
				skillUI.textCDNum.text = "";
				skillUI.imgCD.fillAmount = 0f;
				component.SetSkillIndex(j, skillUI);
			}
		}
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x0007ADE4 File Offset: 0x00078FE4
	public void UpdateSkillMax()
	{
		this.defaultSkillPanelLayoutGroupSpacing = 40f;
		this.defaultSkillPanelPosition = new Vector2(-305f, this.skillPanelTransform.anchoredPosition.y);
		this.defaultSkillItemScale = 0.9f;
		this.skillPanelTransform.anchoredPosition = this.defaultSkillPanelPosition;
		this.skillPanelLayoutGroup.spacing = this.defaultSkillPanelLayoutGroupSpacing;
		for (int i = 0; i < GameHelperClient.MaxSkillNum; i++)
		{
			this.skillList[i].playerStateSkill.transform.localScale = new Vector3(this.defaultSkillItemScale, this.defaultSkillItemScale, this.defaultSkillItemScale);
		}
	}

	// Token: 0x060013EE RID: 5102 RVA: 0x0007AE8C File Offset: 0x0007908C
	public void UpdateEquipMax()
	{
		this.selfView.trans_exEquip.gameObject.SetActive(true);
		this.equipPanelTransform.sizeDelta = new Vector2(340f, this.equipPanelTransform.sizeDelta.y);
		RectTransform component = this.selfView.trans_teleportTip.GetComponent<RectTransform>();
		component.anchoredPosition = new Vector2(595f, component.anchoredPosition.y);
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x0007AF00 File Offset: 0x00079100
	public void ShowEquipJoyBtn()
	{
		for (int i = 0; i < this.equipUIList.Count; i++)
		{
			this.equipUIList[i].image_joy.gameObject.SetActive(true);
		}
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x0007AF40 File Offset: 0x00079140
	public void HideEquipJoyBtn()
	{
		for (int i = 0; i < this.equipUIList.Count; i++)
		{
			this.equipUIList[i].image_joy.gameObject.SetActive(false);
		}
	}

	// Token: 0x060013F1 RID: 5105 RVA: 0x0007AF7F File Offset: 0x0007917F
	public void PlayerLevelUp()
	{
		this.upLevelAnimator.SetTrigger(AnimDefine.LevelUp);
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x0007AF94 File Offset: 0x00079194
	protected override void ButtonAddClick()
	{
		CommonUITouch component = this.selfView.trans_touch.GetComponent<CommonUITouch>();
		component.touchPointerEnter = delegate()
		{
			Game.UI.GetUI<UI_DecTip>().StartOpen();
		};
		component.touchPointerExit = delegate()
		{
			Game.UI.GetUI<UI_DecTip>().StartClose();
		};
		CommonUITouch component2 = this.selfView.trans_touchATK.GetComponent<CommonUITouch>();
		component2.touchPointerEnter = delegate()
		{
			UI_DecTip.TipInfo tipInfo = new UI_DecTip.TipInfo
			{
				nameStr = Game.Language.Get("attack", ""),
				info = Game.Language.Get("攻击力说明", ""),
				iconPath = "Bundles/UI/Icon/Other/function_icon_sword_1",
				showPos = this.selfView.trans_touchATK.position - new Vector3(0f, 10f, 0f),
				quality = -1
			};
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.ShowTipInfo(true, tipInfo);
		};
		component2.touchPointerExit = delegate()
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.ShowTipInfo(false, default(UI_DecTip.TipInfo));
		};
		CommonUITouch component3 = this.selfView.trans_touchArm.GetComponent<CommonUITouch>();
		component3.touchPointerEnter = delegate()
		{
			float num = 1f - Util.GetArmorLevel(GameHelperClient.localPlayer.armor);
			UI_DecTip.TipInfo tipInfo = new UI_DecTip.TipInfo
			{
				nameStr = Game.Language.Get("armor", ""),
				info = string.Format(Game.Language.Get("护甲说明", ""), (num * 100f).ToString("F1")),
				iconPath = "Bundles/UI/Icon/Other/function_icon_shield",
				showPos = this.selfView.trans_touchArm.position - new Vector3(0f, 10f, 0f),
				quality = -1
			};
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.ShowTipInfo(true, tipInfo);
		};
		component3.touchPointerExit = delegate()
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.ShowTipInfo(false, default(UI_DecTip.TipInfo));
		};
		CommonUITouch component4 = this.selfView.trans_touchSTR.GetComponent<CommonUITouch>();
		component4.touchPointerEnter = delegate()
		{
			UI_DecTip.TipInfo tipInfo = new UI_DecTip.TipInfo
			{
				nameStr = Game.Language.Get("str", ""),
				info = string.Format(Game.Language.Get("力量说明", ""), 1),
				iconPath = "Bundles/UI/Icon/Other/function_icon_fist",
				showPos = this.selfView.trans_touchSTR.position - new Vector3(0f, 10f, 0f),
				quality = -1
			};
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.ShowTipInfo(true, tipInfo);
		};
		component4.touchPointerExit = delegate()
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.ShowTipInfo(false, default(UI_DecTip.TipInfo));
		};
		CommonUITouch component5 = this.selfView.trans_touchSTA.GetComponent<CommonUITouch>();
		component5.touchPointerEnter = delegate()
		{
			UI_DecTip.TipInfo tipInfo = new UI_DecTip.TipInfo
			{
				nameStr = Game.Language.Get("sta", ""),
				info = string.Format(Game.Language.Get("耐力说明", ""), 10),
				iconPath = "Bundles/UI/Icon/Other/function_icon_life",
				showPos = this.selfView.trans_touchSTA.position - new Vector3(0f, 10f, 0f),
				quality = -1
			};
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.ShowTipInfo(true, tipInfo);
		};
		component5.touchPointerExit = delegate()
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.ShowTipInfo(false, default(UI_DecTip.TipInfo));
		};
		CommonUITouch component6 = this.selfView.trans_touchAGI.GetComponent<CommonUITouch>();
		component6.touchPointerEnter = delegate()
		{
			UI_DecTip.TipInfo tipInfo = new UI_DecTip.TipInfo
			{
				nameStr = Game.Language.Get("dex", ""),
				info = string.Format(Game.Language.Get("敏捷说明", ""), 0.2f),
				iconPath = "Bundles/UI/Icon/Other/function_icon_thunder",
				showPos = this.selfView.trans_touchAGI.position - new Vector3(0f, 10f, 0f),
				quality = -1
			};
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.ShowTipInfo(true, tipInfo);
		};
		component6.touchPointerExit = delegate()
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.ShowTipInfo(false, default(UI_DecTip.TipInfo));
		};
		this.selfView.btn_giveup.AddButtonEvent(new UnityAction(this.OnGiveupBtnClick));
		this.playerStateBag = this.selfView.pool_bag.GetComponent<PlayerState_Bag>();
		this.playerStateBag.Init();
	}

	// Token: 0x060013F3 RID: 5107 RVA: 0x0007B198 File Offset: 0x00079398
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.FindUI();
		this.RefreshPlayerSkill();
		this.RefreshPlayerEquip();
		this.RefreshGemCoin();
		this.RefreshPlayerStateUI();
		this.selfView.trans_bagDetail.gameObject.SetActive(false);
		this.selfView.img_HeadIcon.sprite = Util.GetHeroIcon(GameHelperClient.localPlayer.heroType);
		this.playerStateJoy.Open();
		MySystemEvent.Instance.RegisterMessage(5, new Action<Body>(this.JoyCrossLeft));
		MySystemEvent.Instance.RegisterMessage(6, new Action<Body>(this.JoyCrossRight));
		MySystemEvent.Instance.RegisterMessage(4, new Action<Body>(this.JoyCrossDown));
		MySystemEvent.Instance.RegisterMessage(3, new Action<Body>(this.JoyCrossUp));
		MySystemEvent.Instance.RegisterMessage(19, new Action<Body>(this.JoyLeftTrigger));
		if (string.IsNullOrEmpty(this.xuanYunImmunityStr))
		{
			this.xuanYunImmunityStr = Game.Language.Get("控制免疫", "");
		}
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x0007B2A8 File Offset: 0x000794A8
	protected override void ClosePanel()
	{
		this.playerStateJoy.Close();
		MySystemEvent.Instance.UnregisterMessage(5, new Action<Body>(this.JoyCrossLeft));
		MySystemEvent.Instance.UnregisterMessage(6, new Action<Body>(this.JoyCrossRight));
		MySystemEvent.Instance.UnregisterMessage(4, new Action<Body>(this.JoyCrossDown));
		MySystemEvent.Instance.UnregisterMessage(3, new Action<Body>(this.JoyCrossUp));
		MySystemEvent.Instance.UnregisterMessage(19, new Action<Body>(this.JoyLeftTrigger));
		this.ClearControlImmunityTips();
	}

	// Token: 0x060013F5 RID: 5109 RVA: 0x0007B33C File Offset: 0x0007953C
	public void RefreshRelic()
	{
		this.selfView.pool_buff.RemoveAllView();
		foreach (RelicBase relicBase in GameHelperClient.localPlayer.playerAttribute.relicList)
		{
			PlayerState_Buff component = this.selfView.pool_buff.AddView().GetComponent<PlayerState_Buff>();
			relicBase.relicData.DIC("id");
			relicBase.myTextNum = component.num;
			relicBase.myTextNum.text = "";
			component.buffUITouch.relicBase = relicBase;
			component.buffUITouch.roleBuff = null;
			component.icon.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/" + relicBase.icon);
			component.icon.color = ColorDefine.QuaUIColor[relicBase.quality];
			component.icon.fillAmount = 1f;
		}
		foreach (RoleBuff roleBuff in GameHelperClient.localPlayer.roleBuffManager.buffList)
		{
			PlayerState_Buff component2 = this.selfView.pool_buff.AddView().GetComponent<PlayerState_Buff>();
			component2.buffUITouch.roleBuff = roleBuff;
			component2.buffUITouch.relicBase = null;
			component2.icon.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/" + roleBuff.icon);
			component2.icon.color = Color.white;
			roleBuff.cdImage = component2.icon;
			component2.icon.fillAmount = 1f;
			Text num = component2.num;
			roleBuff.myText = num;
			if (!string.IsNullOrEmpty(roleBuff.specialStr))
			{
				roleBuff.myText.text = roleBuff.specialStr;
			}
			else
			{
				roleBuff.myText.text = "";
			}
		}
	}

	// Token: 0x060013F6 RID: 5110 RVA: 0x0007B564 File Offset: 0x00079764
	private void ShowEquip()
	{
		this.selfView.pool_equip.RemoveAllView();
		for (int i = 0; i < 3; i++)
		{
			this.selfView.pool_equip.AddView();
		}
	}

	// Token: 0x060013F7 RID: 5111 RVA: 0x0007B5A0 File Offset: 0x000797A0
	public void RefreshHPUI()
	{
		this.selfView.text_hp.text = PathDefine.Concat(GameHelperClient.localPlayer.hp, "/", GameHelperClient.localPlayer.maxHp);
		this.selfView.text_mp.text = PathDefine.Concat(GameHelperClient.localPlayer.mp, "/", GameHelperClient.localPlayer.maxMp);
		float needExp = GameHelperClient.localPlayer.playerAttribute.GetNeedExp(GameHelperClient.localPlayer.Level - 1);
		float num = (float)GameHelperClient.localPlayer.playerAttribute.NowExp * 1f - needExp;
		float num2 = (float)GameHelperClient.localPlayer.playerAttribute.maxExp - needExp;
		this.selfView.slider_exp.value = num / num2;
		this.selfView.text_XP.text = PathDefine.Concat(num, "/", num2);
		this.targetHp = (float)GameHelperClient.localPlayer.hp * 1f / (float)GameHelperClient.localPlayer.maxHp;
		this.targetMp = (float)GameHelperClient.localPlayer.mp * 1f / (float)GameHelperClient.localPlayer.maxMp;
	}

	// Token: 0x060013F8 RID: 5112 RVA: 0x0007B6E4 File Offset: 0x000798E4
	public void RefreshPlayerStateUI()
	{
		string roleName = GameHelperClient.localPlayer.roleName;
		this.selfView.ltext_UserName.text = roleName;
		string text = GameHelperClient.localPlayer.FinalAttackPower.ToString();
		string text2 = GameHelperClient.localPlayer.armor.ToString();
		string text3 = GameHelperClient.localPlayer.STR.ToString();
		string text4 = GameHelperClient.localPlayer.AGI.ToString();
		string text5 = GameHelperClient.localPlayer.STA.ToString();
		this.selfView.text_ATK.text = text;
		this.selfView.text_armor.text = text2;
		this.selfView.text_str.text = text3;
		this.selfView.text_AGI.text = text4;
		this.selfView.text_STA.text = text5;
		this.selfView.text_level.text = GameHelperClient.localPlayer.Level.ToString();
	}

	// Token: 0x060013F9 RID: 5113 RVA: 0x0007B7F0 File Offset: 0x000799F0
	public void RefreshPlayerSkill()
	{
		int num = 0;
		List<SkillBase> roleSkillList = GameHelperClient.localPlayer.roleSkillList;
		int count = roleSkillList.Count;
		for (int i = 0; i < count; i++)
		{
			SkillBase skillBase = roleSkillList[i];
			InputAction inputAction = null;
			if (!(skillBase is PasssiveSkill))
			{
				inputAction = this.GetKey(num);
				num++;
			}
			Game.UI.GetUI<UI_PlayerState>().skillList[i].SetSkill(skillBase, inputAction);
		}
		int count2 = Game.UI.GetUI<UI_PlayerState>().skillList.Count;
		if (count2 > count)
		{
			for (int j = count; j < count2; j++)
			{
				Game.UI.GetUI<UI_PlayerState>().skillList[j].HideSkill();
			}
		}
	}

	// Token: 0x060013FA RID: 5114 RVA: 0x0007B8A8 File Offset: 0x00079AA8
	private InputAction GetKey(int index)
	{
		InputManager inputManager = EntityStatic.Get<InputManager>();
		switch (index)
		{
		case 0:
		{
			bool isJoyStick = GameHelperClient.IsJoyStick;
			return inputManager.controls.Gameplay.Skill1;
		}
		case 1:
		{
			bool isJoyStick2 = GameHelperClient.IsJoyStick;
			return inputManager.controls.Gameplay.Skill2;
		}
		case 2:
		{
			bool isJoyStick3 = GameHelperClient.IsJoyStick;
			return inputManager.controls.Gameplay.Skill3;
		}
		case 3:
		{
			bool isJoyStick4 = GameHelperClient.IsJoyStick;
			return inputManager.controls.Gameplay.Skill4;
		}
		case 4:
		{
			bool isJoyStick5 = GameHelperClient.IsJoyStick;
			return inputManager.controls.Gameplay.Skill5;
		}
		default:
			return inputManager.controls.Gameplay.Skill1;
		}
	}

	// Token: 0x060013FB RID: 5115 RVA: 0x0007B96F File Offset: 0x00079B6F
	public void RefreshPlayerEquip()
	{
		GameHelperClient.localPlayer.playerAttribute.RefreshEquipPower();
		this.UpdateEquipUI();
		UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
		if (ui != null)
		{
			ui.RefreshBaoJi();
		}
		RoleBase.OnEquipChange onEquipChange = GameHelperClient.localPlayer.onEquipChange;
		if (onEquipChange == null)
		{
			return;
		}
		onEquipChange();
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x0007B9B0 File Offset: 0x00079BB0
	private void UpdateEquipUI()
	{
		int count = this.equipUIList.Count;
		int count2 = GameHelperClient.localPlayer.playerAttribute.equipList.Count;
		for (int i = 0; i < count2; i++)
		{
			if (count <= i)
			{
				EquipBase equipBase = GameHelperClient.localPlayer.playerAttribute.equipList[i];
				PlayerState_Equip component = this.selfView.pool_equip.AddView().GetComponent<PlayerState_Equip>();
				string str = "A";
				if (i == 0)
				{
					str = "A";
				}
				else if (i == 1)
				{
					str = "X";
				}
				else if (i == 2)
				{
					str = "Y";
				}
				component.image_joy.sprite = Resources.Load<Sprite>("Bundles/Images/Joy/Joy_" + str);
				component.image_joy.gameObject.SetActive(false);
				this.equipUIList.Add(component);
				equipBase.iconImg = component.icon;
				component.icon.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Shop/" + equipBase.iconName);
				component.equipUITouch.equipBase = equipBase;
			}
		}
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x0007BAC8 File Offset: 0x00079CC8
	public void RemoveEquip(EquipBase equipBase)
	{
		for (int i = this.equipUIList.Count - 1; i > -1; i--)
		{
			PlayerState_Equip playerState_Equip = this.equipUIList[i];
			if (playerState_Equip.equipUITouch.equipBase == equipBase)
			{
				this.equipUIList.RemoveAt(i);
				this.selfView.pool_equip.RemoveView(playerState_Equip.gameObject);
			}
		}
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x0007BB2C File Offset: 0x00079D2C
	public void RefreshGemCoin()
	{
		this.selfView.text_gold.text = GameHelperClient.localPlayer.gold.ToString();
		this.selfView.text_gem.text = GameHelperClient.localPlayer.gem.ToString();
	}

	// Token: 0x060013FF RID: 5119 RVA: 0x0007BB80 File Offset: 0x00079D80
	public void ShowEquipInfo(bool show, Vector3 pos, EquipBase equipBase)
	{
		if (this.isShowBagItemDetail && this.isShowEquipDetail)
		{
			return;
		}
		Transform trans_ItemDetail = Game.UI.GetUI<UI_Shop>().selfView.trans_ItemDetail;
		if (!show)
		{
			trans_ItemDetail.gameObject.SetActive(false);
			return;
		}
		trans_ItemDetail.position = pos;
		trans_ItemDetail.GetComponent<RectTransform>().anchoredPosition += new Vector2(-60f, 50f);
		trans_ItemDetail.gameObject.SetActive(true);
		string equipIndex = equipBase.equipIndex;
		string sellStr;
		if (equipBase.IsMyth())
		{
			sellStr = Mathf.RoundToInt((float)int.Parse(ExcelManager.allExcelData["shop"].DIC("equip_0").DIC("sell")) * (1f + GameHelperClient.localPlayer.GetShopDiscountAdd())).ToString();
		}
		else
		{
			sellStr = Mathf.RoundToInt((float)int.Parse(ExcelManager.allExcelData["shop"].DIC(PathDefine.Concat("equip_", equipIndex)).DIC("sell")) * (1f + GameHelperClient.localPlayer.GetShopDiscountAdd())).ToString();
		}
		string text = equipBase.name;
		if (equipBase.level > 0)
		{
			text = string.Format("{0}(+{1})", text, equipBase.level);
		}
		string text2 = equipBase.GetEquipInfo(0);
		if (equipBase.totals != null)
		{
			int num = equipBase.totals.Length;
			string[] array = new string[num];
			for (int i = 0; i < num; i++)
			{
				TotalNumType totalNumType = TotalNumType.NormalNum;
				if (equipBase.isTotalsPercent != null)
				{
					totalNumType = equipBase.isTotalsPercent[i];
				}
				string arg = "";
				if (totalNumType == TotalNumType.NormalNum)
				{
					arg = equipBase.totals[i].ToString();
				}
				else if (totalNumType == TotalNumType.PercentNum)
				{
					arg = PathDefine.Concat(equipBase.totals[i], StringDefine.Percent);
				}
				else if (totalNumType == TotalNumType.PointNum)
				{
					arg = ((float)equipBase.totals[i] / 10f).ToString();
				}
				array[i] = string.Format(ColorDefine.NormalColor, arg);
			}
			object a = text2;
			object wrap = StringDefine.Wrap;
			string format = Game.Language.Get(equipBase.GetEquipSkillTotalKey(), "");
			object[] args = array;
			text2 = PathDefine.Concat(a, wrap, string.Format(format, args));
		}
		trans_ItemDetail.GetComponent<BagItemDetail>().ShowInfo(BagItemDetail.ShowDetailType.Equip, text, text2, sellStr, "Bundles/UI/Icon/Shop/" + equipBase.iconName, false, Color.white, -1);
		this.selfView.trans_bagDetail.gameObject.SetActive(false);
	}

	// Token: 0x06001400 RID: 5120 RVA: 0x0007BE03 File Offset: 0x0007A003
	public void RefreshBookUI()
	{
		this.RefreshBag();
	}

	// Token: 0x06001401 RID: 5121 RVA: 0x0007BE0C File Offset: 0x0007A00C
	private void RefreshBag()
	{
		this.playerStateBag.ClearBag();
		this.selfView.trans_bagDetail.gameObject.SetActive(false);
		List<BagItem> bagItemList = GameHelperClient.localPlayer.playerAttribute.bagItemList;
		for (int i = 0; i < bagItemList.Count; i++)
		{
			this.selfView.pool_bag.GetComponent<PlayerState_Bag>().AddItem(bagItemList[i]);
		}
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x0007BE78 File Offset: 0x0007A078
	public ClipController GetMap()
	{
		if (this.miniMap == null)
		{
			this.miniMap = this.selfView.trans_map.GetComponent<ClipController>();
			this.miniMap.mapWidth = GameHelperClient.CanSpellArea.x * 2f + 2.5f;
		}
		return this.miniMap;
	}

	// Token: 0x06001403 RID: 5123 RVA: 0x0007BED0 File Offset: 0x0007A0D0
	public ClipController GetHeroMap()
	{
		if (this.heroMap == null)
		{
			this.heroMap = this.selfView.trans_heroMap.GetComponent<ClipController>();
			this.heroMap.mapWidth = GameHelperClient.CanSpellArea.x * 2f + 2.5f;
		}
		return this.heroMap;
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x0007BF28 File Offset: 0x0007A128
	public override void Update()
	{
		foreach (SkillUI skillUI in this.skillList)
		{
			skillUI.Update();
		}
		this.selfView.slider_hp.value = Mathf.Lerp(this.selfView.slider_hp.value, this.targetHp, this.lerpSpeed * Time.deltaTime);
		this.selfView.slider_mp.value = Mathf.Lerp(this.selfView.slider_mp.value, this.targetMp, this.lerpSpeed * Time.deltaTime);
		if (GameHelperClient.localPlayer != null && GameHelperClient.localPlayer.roleBuffManager != null)
		{
			foreach (RoleBuff roleBuff in GameHelperClient.localPlayer.roleBuffManager.buffList)
			{
				if (roleBuff.cdImage != null && !roleBuff.isNoLife)
				{
					roleBuff.cdImage.fillAmount = roleBuff.lifeTime / roleBuff.lifeTimeSet;
					roleBuff.myText.text = Mathf.Ceil(roleBuff.lifeTime).ToString();
				}
			}
		}
		this.playerStateJoy.Update();
		this.RefreshGemCoin();
		this.RefreshHPUI();
		this.UpdateTalkUI();
	}

	// Token: 0x06001405 RID: 5125 RVA: 0x0007C0B4 File Offset: 0x0007A2B4
	public void UseSkill(int index)
	{
		int activeIndexByKeyIndex = Util.GetActiveIndexByKeyIndex(index);
		if (activeIndexByKeyIndex != -1)
		{
			this.skillList[activeIndexByKeyIndex].Use();
		}
	}

	// Token: 0x06001406 RID: 5126 RVA: 0x0007C0E0 File Offset: 0x0007A2E0
	public void BtnRightSkill(SkillUI skillUI)
	{
		if (skillUI.skillBase == null)
		{
			return;
		}
		ActiveSkillEnum activeSkillEnum = skillUI.skillBase.activeSkillEnum;
		if (activeSkillEnum != ActiveSkillEnum.None && Game.GameData.ActiveSkillDataDic[activeSkillEnum].canAuto)
		{
			skillUI.skillBase.UpdateAuto();
		}
	}

	// Token: 0x06001407 RID: 5127 RVA: 0x0007C128 File Offset: 0x0007A328
	public void SetCDTime()
	{
		this.skillList[this.skillIndex].skillBase.SetCDTime();
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x0007C148 File Offset: 0x0007A348
	public void ShowDamageNum(long damage, Vector3 worldPos, bool isCrit, AttackType attackType)
	{
		if (!GameHelperClient.IsShowDamage)
		{
			return;
		}
		damage = Math.Min(damage, 999999999999999999L);
		GameObject gameObject = AssetManager.LoadPrefab("UI/Prefabs/DamageNum", this.selfView.trans_damage, true);
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
		Vector2 v = Game.Camera.WorldToScreenPoint(worldPos);
		v.x += (float)Random.Range(-20, 20);
		v.y += (float)Random.Range(-20, 20);
		gameObject.transform.position = v;
		gameObject.transform.GetChild(0).localPosition = Vector3.zero;
		gameObject.transform.GetChild(0).GetComponent<DamageNum>().Init(damage, isCrit, attackType);
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x0007C218 File Offset: 0x0007A418
	public void ShowDoge(Vector3 worldPos)
	{
		GameObject gameObject = AssetManager.LoadPrefab("UI/Prefabs/DamageNum", this.selfView.trans_damage, true);
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
		Vector2 v = Game.Camera.WorldToScreenPoint(worldPos);
		v.x += (float)Random.Range(-20, 20);
		v.y += (float)Random.Range(-20, 20);
		gameObject.transform.position = v;
		gameObject.transform.GetChild(0).localPosition = Vector3.zero;
		gameObject.transform.GetChild(0).GetComponent<DamageNum>().ShowDoge();
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x0007C2CC File Offset: 0x0007A4CC
	public void ShowXuanYunImmunity(Vector3 worldPos)
	{
		UI_PlayerState.ControlImmunityTip tip = this.GetControlImmunityTip();
		this.activeControlImmunityTips.Add(tip);
		tip.Root.SetActive(true);
		tip.Root.transform.SetAsLastSibling();
		Sequence sequence = tip.Sequence;
		if (sequence != null)
		{
			sequence.Kill(false);
		}
		tip.TextRect.DOKill(false);
		tip.Text.DOKill(false);
		tip.Text.text = this.GetXuanYunImmunityStr();
		tip.Text.color = new Color(0.9921569f, 0.1176471f, 0.1803922f, 1f);
		tip.TextRect.localPosition = Vector3.zero;
		tip.TextRect.localScale = Vector3.one;
		Vector2 v = Game.Camera.WorldToScreenPoint(worldPos);
		v.x += (float)Random.Range(-20, 20);
		v.y += (float)Random.Range(-20, 20);
		tip.Root.transform.position = v;
		tip.Sequence = DOTween.Sequence();
		tip.Sequence.Append(tip.TextRect.DOLocalMove(new Vector3(0f, 60f, 0f), 1f, false));
		tip.Sequence.onComplete = delegate()
		{
			this.RecycleControlImmunityTip(tip);
		};
	}

	// Token: 0x0600140B RID: 5131 RVA: 0x0007C48A File Offset: 0x0007A68A
	private UI_PlayerState.ControlImmunityTip GetControlImmunityTip()
	{
		if (this.controlImmunityTipPool.Count > 0)
		{
			return this.controlImmunityTipPool.Dequeue();
		}
		return this.CreateControlImmunityTip();
	}

	// Token: 0x0600140C RID: 5132 RVA: 0x0007C4AC File Offset: 0x0007A6AC
	private UI_PlayerState.ControlImmunityTip CreateControlImmunityTip()
	{
		GameObject gameObject = new GameObject("ControlImmunityTip", new Type[]
		{
			typeof(RectTransform)
		});
		gameObject.layer = this.selfView.trans_damage.gameObject.layer;
		gameObject.transform.SetParent(this.selfView.trans_damage, false);
		gameObject.SetActive(false);
		GameObject gameObject2 = new GameObject("Text", new Type[]
		{
			typeof(RectTransform),
			typeof(CanvasRenderer),
			typeof(Text),
			typeof(Outline)
		});
		gameObject2.layer = gameObject.layer;
		gameObject2.transform.SetParent(gameObject.transform, false);
		RectTransform component = gameObject2.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(260f, 60f);
		component.localPosition = Vector3.zero;
		Text component2 = gameObject2.GetComponent<Text>();
		component2.font = this.GetControlImmunityFont();
		component2.fontSize = 21;
		component2.resizeTextForBestFit = true;
		component2.resizeTextMinSize = 18;
		component2.resizeTextMaxSize = 30;
		component2.alignment = TextAnchor.MiddleCenter;
		component2.horizontalOverflow = HorizontalWrapMode.Overflow;
		component2.verticalOverflow = VerticalWrapMode.Overflow;
		component2.raycastTarget = false;
		Outline component3 = gameObject2.GetComponent<Outline>();
		component3.effectColor = new Color(0f, 0f, 0f, 0.6f);
		component3.effectDistance = new Vector2(1.5f, -1.5f);
		return new UI_PlayerState.ControlImmunityTip
		{
			Root = gameObject,
			TextRect = component,
			Text = component2
		};
	}

	// Token: 0x0600140D RID: 5133 RVA: 0x0007C63C File Offset: 0x0007A83C
	private void RecycleControlImmunityTip(UI_PlayerState.ControlImmunityTip tip)
	{
		if (!this.activeControlImmunityTips.Remove(tip))
		{
			return;
		}
		Sequence sequence = tip.Sequence;
		if (sequence != null)
		{
			sequence.Kill(false);
		}
		tip.Sequence = null;
		tip.TextRect.DOKill(false);
		tip.Text.DOKill(false);
		tip.Root.SetActive(false);
		this.controlImmunityTipPool.Enqueue(tip);
	}

	// Token: 0x0600140E RID: 5134 RVA: 0x0007C6A4 File Offset: 0x0007A8A4
	private void ClearControlImmunityTips()
	{
		for (int i = this.activeControlImmunityTips.Count - 1; i >= 0; i--)
		{
			this.RecycleControlImmunityTip(this.activeControlImmunityTips[i]);
		}
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x0007C6DB File Offset: 0x0007A8DB
	private string GetXuanYunImmunityStr()
	{
		if (string.IsNullOrEmpty(this.xuanYunImmunityStr))
		{
			this.xuanYunImmunityStr = Game.Language.Get("控制免疫", "");
		}
		return this.xuanYunImmunityStr;
	}

	// Token: 0x06001410 RID: 5136 RVA: 0x0007C70A File Offset: 0x0007A90A
	private Font GetControlImmunityFont()
	{
		if (this.controlImmunityFont == null)
		{
			this.controlImmunityFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
		}
		return this.controlImmunityFont;
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x0007C730 File Offset: 0x0007A930
	public void ShowAddum(int num, Vector3 worldPos)
	{
		GameObject gameObject = AssetManager.LoadPrefab("UI/Prefabs/DamageNum", this.selfView.trans_damage, true);
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
		Vector2 v = Game.Camera.WorldToScreenPoint(worldPos);
		v.x += (float)Random.Range(-20, 20);
		v.y += (float)Random.Range(-20, 20);
		gameObject.transform.position = v;
		gameObject.transform.GetChild(0).localPosition = Vector3.zero;
		gameObject.transform.GetChild(0).GetComponent<DamageNum>().Init(num);
	}

	// Token: 0x06001412 RID: 5138 RVA: 0x0007C7E4 File Offset: 0x0007A9E4
	public void ShowBagItemInfo(bool show, Vector3 pos, string shopId, bool isShop, BagItemType bagItemType)
	{
		if (this.isShowBagItemDetail && !isShop && !this.isShowEquipDetail)
		{
			return;
		}
		UI_Shop ui = Game.UI.GetUI<UI_Shop>();
		Transform trans_ItemDetail = ui.selfView.trans_ItemDetail;
		if (!show)
		{
			trans_ItemDetail.gameObject.SetActive(false);
			return;
		}
		trans_ItemDetail.position = pos;
		trans_ItemDetail.GetComponent<RectTransform>().anchoredPosition += new Vector2(-60f, 0f);
		trans_ItemDetail.gameObject.SetActive(show);
		Color iconColor = Color.white;
		bool flag = bagItemType == BagItemType.Remains;
		bool flag2 = bagItemType == BagItemType.Card;
		ItemType itemType = ItemType.None;
		if (flag)
		{
			itemType = (ItemType)Enum.Parse(typeof(ItemType), shopId);
			int num = (int)itemType;
			shopId = num.ToString();
			iconColor = ColorDefine.QuaUIColor[Game.GameData.RemainsDataDic[itemType].grade];
		}
		string sellStr = "0";
		int exQua = -1;
		string infoStr;
		string text;
		string nameStr;
		if (flag2)
		{
			ItemType itemType2 = (ItemType)Enum.Parse(typeof(ItemType), shopId);
			CardData cardData = Game.GameData.CardDataDic[itemType2 - ItemType.Card_0];
			infoStr = PathDefine.Concat(Game.Language.Get("卡牌使用说明", ""), StringDefine.Wrap, UI_MyCard.GetCardInfo(cardData));
			text = "Bundles/UI/Icon/Card/" + cardData.icon;
			nameStr = PathDefine.Concat(Game.Language.Get("【卡牌】", ""), Game.Language.Get(PathDefine.Concat("card_", cardData.id), ""));
			exQua = cardData.quality;
		}
		else
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)(flag ? ExcelManager.allExcelData["remains"].DIC(shopId) : ExcelManager.allExcelData["shop"].DIC(shopId));
			string text2 = dictionary.DIC("id");
			text = dictionary.DIC("icon");
			if (isShop && ui.GetShopType == UI_Shop.ShopType.Equip)
			{
				infoStr = EquipBase.GetEquipInfo(shopId);
				sellStr = Mathf.RoundToInt((float)int.Parse(dictionary.DIC("sell")) * (1f + GameHelperClient.localPlayer.GetShopDiscountAdd())).ToString();
			}
			else if (isShop && ui.GetShopType == UI_Shop.ShopType.Medicine)
			{
				string text3 = Game.Language.Get(text2 + "_m", "");
				string text4 = dictionary.DIC("value");
				if (!string.IsNullOrEmpty(text4))
				{
					string format = text3;
					object[] args = text4.Split('|', StringSplitOptions.None);
					text3 = string.Format(format, args);
				}
				int num2 = dictionary.DIC("time");
				string str = string.Format(Game.Language.Get("持续回合", ""), string.Format(ColorDefine.NormalColor, num2));
				infoStr = text3 + "\n" + str;
				sellStr = Mathf.RoundToInt((float)int.Parse(dictionary.DIC("sell")) * (1f + GameHelperClient.localPlayer.GetShopDiscountAdd())).ToString();
			}
			else if (flag)
			{
				infoStr = RelicBase.GetFormatDec(Game.Language.Get("pickitem_" + shopId + "_m", ""), dictionary);
				RemainsData remainsData;
				if (Game.GameData.RemainsDataDic.TryGetValue(itemType, out remainsData))
				{
					sellStr = ConstDefine.RelicSellGold[remainsData.grade].ToString();
				}
			}
			else if (text2.StartsWith("Medicine_"))
			{
				string text5 = Game.Language.Get(text2 + "_m", "");
				string text6 = dictionary.DIC("value");
				if (!string.IsNullOrEmpty(text6))
				{
					string format2 = text5;
					object[] args = text6.Split('|', StringSplitOptions.None);
					text5 = string.Format(format2, args);
				}
				int num3 = dictionary.DIC("time");
				string str2 = string.Format(Game.Language.Get("持续回合", ""), string.Format(ColorDefine.NormalColor, num3));
				infoStr = text5 + "\n" + str2;
				sellStr = dictionary.DIC("sell");
			}
			else
			{
				infoStr = Game.Language.Get(text2 + "_m", "");
				sellStr = dictionary.DIC("sell");
			}
			nameStr = (flag ? Game.Language.Get("pickitem_" + shopId, "") : Game.Language.Get(shopId, ""));
			if (flag)
			{
				text = "Bundles/UI/Icon/Remains/" + text;
			}
			else
			{
				text = "Bundles/UI/Icon/Shop/" + text;
			}
		}
		trans_ItemDetail.GetComponent<BagItemDetail>().ShowInfo(isShop ? BagItemDetail.ShowDetailType.Shop : BagItemDetail.ShowDetailType.Normal, nameStr, infoStr, sellStr, text, isShop, iconColor, exQua);
		this.selfView.trans_bagDetail.gameObject.SetActive(false);
	}

	// Token: 0x06001413 RID: 5139 RVA: 0x0007CCEC File Offset: 0x0007AEEC
	public void ShowBagItemDetail(bool show, Vector3 pos, Dictionary<string, object> dic, BagItem bagItem)
	{
		if (show)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
			this.isShowBagItemDetail = true;
			this.isShowEquipDetail = false;
			Game.UI.GetUI<UI_Shop>().selfView.trans_ItemDetail.gameObject.SetActive(false);
		}
		else
		{
			this.isShowBagItemDetail = false;
			if (GameHelperClient.IsJoyStick)
			{
				this.playerStateBag.bagList[this.playerStateJoy.bagIndex].transform.localScale = Vector3.one;
			}
		}
		if (!show)
		{
			this.selfView.trans_bagDetail.gameObject.SetActive(false);
			return;
		}
		this.selfView.trans_bagDetail.gameObject.SetActive(show);
		this.selfView.trans_bagDetail.GetComponent<BagItemBtnList>().SetBagItem(bagItem, dic);
		this.selfView.trans_bagDetail.position = pos;
		this.selfView.trans_bagDetail.GetComponent<RectTransform>().anchoredPosition += new Vector2(-50f, 0f);
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x0007CE08 File Offset: 0x0007B008
	public void ShowEquipDetail(bool show, Vector3 pos, EquipBase equipBase)
	{
		if (show)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
			this.isShowBagItemDetail = true;
			this.isShowEquipDetail = true;
			Game.UI.GetUI<UI_Shop>().selfView.trans_ItemDetail.gameObject.SetActive(false);
		}
		else
		{
			this.isShowBagItemDetail = false;
			if (GameHelperClient.IsJoyStick)
			{
				this.playerStateBag.bagList[this.playerStateJoy.bagIndex].transform.localScale = Vector3.one;
			}
		}
		if (!show)
		{
			this.selfView.trans_bagDetail.gameObject.SetActive(false);
			return;
		}
		this.selfView.trans_bagDetail.gameObject.SetActive(show);
		this.selfView.trans_bagDetail.GetComponent<BagItemBtnList>().SetEquipItem(equipBase);
		this.selfView.trans_bagDetail.position = pos;
		this.selfView.trans_bagDetail.GetComponent<RectTransform>().anchoredPosition += new Vector2(-50f, 35f);
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x0007CF1F File Offset: 0x0007B11F
	public Button GetBagDetailBtn(int index)
	{
		return this.selfView.trans_bagDetail.GetComponent<BagItemBtnList>().GetButton(index);
	}

	// Token: 0x06001416 RID: 5142 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void JoyCrossLeft(Body body)
	{
	}

	// Token: 0x06001417 RID: 5143 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void JoyCrossRight(Body body)
	{
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void JoyCrossDown(Body body)
	{
	}

	// Token: 0x06001419 RID: 5145 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void JoyCrossUp(Body body)
	{
	}

	// Token: 0x0600141A RID: 5146 RVA: 0x0007CF38 File Offset: 0x0007B138
	private void JoyLeftTrigger(Body body)
	{
		if (Game.UI.GetUI<UI_Shop>() == null)
		{
			Game.UI.OpenUI<UI_Shop>(null);
			return;
		}
		if (Game.UI.GetUI<UI_Shop>().isOpenShop)
		{
			Game.UI.GetUI<UI_Shop>().CloseAnim(false, true);
			return;
		}
		Game.UI.GetUI<UI_Shop>().OpenAnim(true);
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x0007CF94 File Offset: 0x0007B194
	public void OnSwitchSkill(RoguelikeUIData roguelikeUIData, Action<RoguelikeUIData, SkillBase> callback)
	{
		this.onRoguelikeClick = callback;
		this.currentRoguelikeData = roguelikeUIData;
		this.isSwitchSkill = true;
		this.skillPanelLayoutGroup.spacing = 160f;
		this.skillPanelTransform.anchoredPosition = new Vector2(-415f, 222f);
		this.selfView.trans_switchSkill.gameObject.SetActive(true);
		for (int i = 1; i < GameHelperClient.MaxSkillNum; i++)
		{
			this.skillList[i].playerStateSkill.CanSwitch(true, this.defaultSkillItemScale);
		}
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x0007D024 File Offset: 0x0007B224
	public void OnSwitchSkillCallBack(int index)
	{
		Action<RoguelikeUIData, SkillBase> action = this.onRoguelikeClick;
		RoguelikeUIData arg = this.currentRoguelikeData;
		SkillBase arg2 = GameHelperClient.localPlayer.roleSkillList[index];
		this.CloseSwitchSkillPanel();
		try
		{
			if (action != null)
			{
				action(arg, arg2);
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		this.NotifySwitchSkillClosed();
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x0007D080 File Offset: 0x0007B280
	public void OnSkillBtnUp(int index)
	{
		if (index >= 0)
		{
			GameHelperClient.localPlayer.OnSkillKeyUp(index);
		}
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x0007D091 File Offset: 0x0007B291
	public void OnSkillBtnClick(int index)
	{
		if (index >= 0 && index < this.skillList.Count)
		{
			this.skillList[index].Use();
		}
	}

	// Token: 0x0600141F RID: 5151 RVA: 0x0007D0B6 File Offset: 0x0007B2B6
	private void OnGiveupBtnClick()
	{
		this.CloseSwitchSkillPanel();
		this.NotifySwitchSkillClosed();
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x0007D0C4 File Offset: 0x0007B2C4
	private void CloseSwitchSkillPanel()
	{
		this.skillPanelLayoutGroup.spacing = this.defaultSkillPanelLayoutGroupSpacing;
		this.isSwitchSkill = false;
		this.onRoguelikeClick = null;
		this.currentRoguelikeData = default(RoguelikeUIData);
		this.skillPanelTransform.anchoredPosition = this.defaultSkillPanelPosition;
		this.selfView.trans_switchSkill.gameObject.SetActive(false);
		for (int i = 1; i < GameHelperClient.MaxSkillNum; i++)
		{
			this.skillList[i].playerStateSkill.CanSwitch(false, this.defaultSkillItemScale);
		}
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x0007D150 File Offset: 0x0007B350
	private void NotifySwitchSkillClosed()
	{
		Action onGiveupBtnClickCallback = this.OnGiveupBtnClickCallback;
		if (onGiveupBtnClickCallback == null)
		{
			return;
		}
		onGiveupBtnClickCallback();
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x0007D164 File Offset: 0x0007B364
	public void InitTeleport()
	{
		int count = Game.PlayerManagerClient.clientPlayerList.Count;
		int num = 0;
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = Game.PlayerManagerClient.clientPlayerList[i];
			if (!(roleBase == null) && roleBase.roleType == RoleType.Player)
			{
				Transform child = this.selfView.trans_teleportTip.GetChild(num);
				child.gameObject.SetActive(true);
				child.GetChild(0).GetComponent<Image>().sprite = Util.GetHeroIcon((roleBase as PlayerBase).heroType);
				num++;
				if (num >= 4)
				{
					return;
				}
			}
		}
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x0007D1F8 File Offset: 0x0007B3F8
	private void UpdateTalkUI()
	{
		for (int i = this.uiTalkList.Count - 1; i >= 0; i--)
		{
			MyTalkUI myTalkUI = this.uiTalkList[i];
			if (myTalkUI.Role == null || myTalkUI.IsOver)
			{
				myTalkUI.gameObject.SetActive(false);
				this.uiTalkList.RemoveAt(i);
			}
			else
			{
				myTalkUI.UpdatePosition();
			}
		}
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x0007D260 File Offset: 0x0007B460
	public void ShowTalkUI(RoleBase role, string[] showStrs, float showTime = 5f)
	{
		MyTalkUI myTalkUI = this.GetMyTalkUI(role);
		myTalkUI.ShowTalkText(role, showStrs, showTime);
		myTalkUI.UpdatePosition();
		this.uiTalkList.Add(myTalkUI);
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x0007D290 File Offset: 0x0007B490
	public void RemoveTalkUI(RoleBase role)
	{
		for (int i = this.uiTalkList.Count - 1; i > -1; i--)
		{
			MyTalkUI myTalkUI = this.uiTalkList[i];
			if (myTalkUI.Role == role)
			{
				myTalkUI.gameObject.SetActive(false);
				this.uiTalkList.RemoveAt(i);
			}
		}
	}

	// Token: 0x06001426 RID: 5158 RVA: 0x0007D2E8 File Offset: 0x0007B4E8
	private MyTalkUI GetMyTalkUI(RoleBase role)
	{
		int count = this.uiTalkList.Count;
		for (int i = 0; i < count; i++)
		{
			MyTalkUI myTalkUI = this.uiTalkList[i];
			if (myTalkUI.Role == role)
			{
				return myTalkUI;
			}
		}
		int childCount = this.selfView.trans_talk.childCount;
		if (childCount > count + 1)
		{
			for (int j = 1; j < childCount; j++)
			{
				Transform child = this.selfView.trans_talk.GetChild(j);
				if (!child.gameObject.activeSelf)
				{
					MyTalkUI component = child.GetComponent<MyTalkUI>();
					child.gameObject.SetActive(true);
					return component;
				}
			}
		}
		MyTalkUI myTalkUI2 = Object.Instantiate<MyTalkUI>(this.myTalkUIPrefab, this.selfView.trans_talk);
		myTalkUI2.gameObject.SetActive(true);
		return myTalkUI2;
	}

	// Token: 0x04001294 RID: 4756
	public UI_PlayerState_View selfView;

	// Token: 0x04001295 RID: 4757
	private ClipController miniMap;

	// Token: 0x04001296 RID: 4758
	private ClipController heroMap;

	// Token: 0x04001297 RID: 4759
	private Transform skill1UI;

	// Token: 0x04001298 RID: 4760
	public List<SkillUI> skillList = new List<SkillUI>(3);

	// Token: 0x04001299 RID: 4761
	public List<PlayerState_Equip> equipUIList = new List<PlayerState_Equip>(3);

	// Token: 0x0400129A RID: 4762
	private float lerpSpeed = 4f;

	// Token: 0x0400129B RID: 4763
	private Animator upLevelAnimator;

	// Token: 0x0400129C RID: 4764
	public PlayerState_Bag playerStateBag;

	// Token: 0x0400129D RID: 4765
	private UI_PlayerState_Joy playerStateJoy;

	// Token: 0x0400129E RID: 4766
	private Action<RoguelikeUIData, SkillBase> onRoguelikeClick;

	// Token: 0x0400129F RID: 4767
	private RoguelikeUIData currentRoguelikeData;

	// Token: 0x040012A0 RID: 4768
	private bool isSwitchSkill;

	// Token: 0x040012A1 RID: 4769
	private RectTransform skillPanelTransform;

	// Token: 0x040012A2 RID: 4770
	private HorizontalLayoutGroup skillPanelLayoutGroup;

	// Token: 0x040012A3 RID: 4771
	private Vector2 defaultSkillPanelPosition;

	// Token: 0x040012A4 RID: 4772
	private float defaultSkillPanelLayoutGroupSpacing;

	// Token: 0x040012A5 RID: 4773
	private float defaultSkillItemScale = 1f;

	// Token: 0x040012A6 RID: 4774
	private RectTransform equipPanelTransform;

	// Token: 0x040012A7 RID: 4775
	private MyTalkUI myTalkUIPrefab;

	// Token: 0x040012A8 RID: 4776
	private List<MyTalkUI> uiTalkList = new List<MyTalkUI>();

	// Token: 0x040012A9 RID: 4777
	private readonly Queue<UI_PlayerState.ControlImmunityTip> controlImmunityTipPool = new Queue<UI_PlayerState.ControlImmunityTip>();

	// Token: 0x040012AA RID: 4778
	private readonly List<UI_PlayerState.ControlImmunityTip> activeControlImmunityTips = new List<UI_PlayerState.ControlImmunityTip>();

	// Token: 0x040012AB RID: 4779
	private Font controlImmunityFont;

	// Token: 0x040012AC RID: 4780
	public Action OnGiveupBtnClickCallback;

	// Token: 0x040012AD RID: 4781
	private string xuanYunImmunityStr;

	// Token: 0x040012AE RID: 4782
	private float targetHp;

	// Token: 0x040012AF RID: 4783
	private float targetMp;

	// Token: 0x040012B0 RID: 4784
	public int skillIndex;

	// Token: 0x040012B1 RID: 4785
	private bool isShowBagItemDetail;

	// Token: 0x040012B2 RID: 4786
	private bool isShowEquipDetail;

	// Token: 0x0200036E RID: 878
	private class ControlImmunityTip
	{
		// Token: 0x040012B3 RID: 4787
		public GameObject Root;

		// Token: 0x040012B4 RID: 4788
		public RectTransform TextRect;

		// Token: 0x040012B5 RID: 4789
		public Text Text;

		// Token: 0x040012B6 RID: 4790
		public Sequence Sequence;
	}
}
