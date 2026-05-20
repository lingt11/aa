using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200034A RID: 842
public class UI_MyCard : UGUICtrl
{
	// Token: 0x0600132D RID: 4909 RVA: 0x00073E08 File Offset: 0x00072008
	public UI_MyCard()
	{
		this.selfView = new UI_MyCard_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_myCard", base.GetType());
		this.equipInfoView = this.selfView.trans_equipInfo.GetComponent<CardView>();
		this.tipInfoRect = this.selfView.trans_tipInfo.GetComponent<RectTransform>();
		this.allDropdown = this.selfView.trans_allDropdown.gameObject.GetComponent<Dropdown>();
		this.allDropdown.ClearOptions();
		this.allDropdown.AddOptions(new List<string>
		{
			Game.Language.Get("全部", ""),
			Game.Language.Get("已拥有", ""),
			Game.Language.Get("未拥有", "")
		});
		this.allDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnAllDropdownChanged));
		this.sortDropdown = this.selfView.trans_listDropdown.gameObject.GetComponent<Dropdown>();
		this.sortDropdown.ClearOptions();
		this.sortDropdown.AddOptions(new List<string>
		{
			Game.Language.Get("获取途径", ""),
			Game.Language.Get("时间", ""),
			Game.Language.Get("品质↑", ""),
			Game.Language.Get("品质↓", ""),
			Game.Language.Get("quality_0", ""),
			Game.Language.Get("quality_1", ""),
			Game.Language.Get("quality_2", ""),
			Game.Language.Get("quality_3", ""),
			Game.Language.Get("quality_4", "")
		});
		this.sortDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnSortDropdownChanged));
		this.saveCardPresetDropdown = this.selfView.trans_saveCardPreset.gameObject.GetComponent<Dropdown>();
		this.saveCardPresetDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnSaveCardPresetDropdownChanged));
		this.loadCardPresetDropdown = this.selfView.trans_loadCardPreset.gameObject.GetComponent<Dropdown>();
		this.loadCardPresetDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnLoadCardPresetDropdownChanged));
		this.makeCardDropdown = this.selfView.trans_makeCard.gameObject.GetComponent<Dropdown>();
		this.makeCardDropdown.ClearOptions();
		this.makeCardDropdown.AddOptions(new List<string>
		{
			Game.Language.Get("分解", ""),
			Game.Language.Get("合成", ""),
			Game.Language.Get("取消", "")
		});
		this.makeCardDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnMakeCardDropdownChanged));
		this.RefreshMakeCardCaption();
		this.SetMakeCardDropdownCallbacks(new Action(this.MakeCardDecomposeCallback), new Action(this.MakeCardComposeCallback), new Action(this.MakeCardCancelCallback));
		this.makeCardNumInputField = this.selfView.trans_inputNum.GetComponent<TMP_InputField>();
		this.InitMakeCardNumInputField();
		this.makeCardInfoView = this.selfView.trans_cardMakeeIInfo.GetComponent<CardView>();
		if (this.makeCardInfoView == null)
		{
			this.makeCardInfoView = this.selfView.trans_cardMakeeIInfo.GetComponentInChildren<CardView>(true);
		}
		this.makeCardButtonText = this.selfView.btn_make.GetComponentInChildren<Text>(true);
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x0007422C File Offset: 0x0007242C
	protected override void ButtonAddClick()
	{
		this.selfView.btn_back.AddButtonEvent(new UnityAction(this.ApplyData));
		this.selfView.btn_addRoom.AddButtonEvent(new UnityAction(this.AddRoom));
		this.selfView.btn_make.AddButtonEvent(new UnityAction(this.OnMakeCardConfirmClick));
		this.selfView.btn_cancel.AddButtonEvent(new UnityAction(this.MakeCardCancelCallback));
		this.AddMakeCardNumHoldButtonEvent(this.selfView.btn_addNum, 1);
		this.AddMakeCardNumHoldButtonEvent(this.selfView.btn_redNum, -1);
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x000742D0 File Offset: 0x000724D0
	public override void Update()
	{
		base.Update();
		if (Input.GetKeyDown(KeyCode.Escape) && this.isOpen)
		{
			this.ApplyData();
		}
		if (!this.isOpen || !this.isHoldingMakeCardNum || Time.unscaledTime < this.nextMakeCardNumHoldTime)
		{
			return;
		}
		this.ChangeMakeCardInputValue(this.holdMakeCardNumStep);
		this.nextMakeCardNumHoldTime = Time.unscaledTime + 0.08f;
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x00074338 File Offset: 0x00072538
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.selfView.trans_equipInfo.gameObject.SetActive(false);
		this.selfView.trans_tipInfo.gameObject.SetActive(false);
		this.SetCardMakeState(UI_MyCard.CardMakeState.Normal);
		this.UpdateCardView();
		this.selfView.pool_equip.RemoveAllView();
		foreach (int key in SaveLoadManager.gameSaveData.equipCards)
		{
			GameObject go = this.selfView.pool_equip.AddView();
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(key, out cardData))
			{
				this.SetCard(go, cardData, false);
			}
		}
		foreach (int key2 in EntityStatic.Get<CardManager>().teamCards)
		{
			GameObject go2 = this.selfView.pool_equip.AddView();
			CardData cardData2;
			if (Game.GameData.CardDataDic.TryGetValue(key2, out cardData2))
			{
				this.SetCard(go2, cardData2, true);
			}
		}
		if (GameHelperClient.InDungeon)
		{
			this.selfView.trans_loadCardPreset.gameObject.SetActive(false);
			this.selfView.trans_saveCardPreset.gameObject.SetActive(false);
		}
		else
		{
			this.RefreshCardPresetDropdowns(0, 0);
			Game.TimerManager.AddLateUpdateAction(new Action(this.RefreshSaveCardPresetCaption));
			Game.TimerManager.AddLateUpdateAction(new Action(this.RefreshMakeCardCaption));
		}
		this.RefreshGoldAndJiYi();
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x000744E8 File Offset: 0x000726E8
	private void RefreshSaveCardPresetCaption()
	{
		this.ResetDropdownCaption(this.saveCardPresetDropdown, Game.Language.Get("预设操作", ""), true);
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x0007450B File Offset: 0x0007270B
	private void RefreshMakeCardCaption()
	{
		this.ResetDropdownCaption(this.makeCardDropdown, Game.Language.Get("制作", ""), true);
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x0007452E File Offset: 0x0007272E
	public void SetMakeCardDropdownCallbacks(Action decomposeCallback, Action composeCallback, Action cancelCallback)
	{
		this.onMakeCardDecomposeCallback = decomposeCallback;
		this.onMakeCardComposeCallback = composeCallback;
		this.onMakeCardCancelCallback = cancelCallback;
	}

	// Token: 0x06001334 RID: 4916 RVA: 0x00074545 File Offset: 0x00072745
	private void MakeCardDecomposeCallback()
	{
		this.SetCardMakeState(UI_MyCard.CardMakeState.Decompose);
	}

	// Token: 0x06001335 RID: 4917 RVA: 0x0007454E File Offset: 0x0007274E
	private void MakeCardComposeCallback()
	{
		this.SetCardMakeState(UI_MyCard.CardMakeState.Compose);
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x00074557 File Offset: 0x00072757
	private void MakeCardCancelCallback()
	{
		this.SetCardMakeState(UI_MyCard.CardMakeState.Normal);
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x00074560 File Offset: 0x00072760
	private void SetCardMakeState(UI_MyCard.CardMakeState state)
	{
		this.StopMakeCardNumHold();
		this.cardMakeState = state;
		this.hasSelectedMakeCardData = false;
		this.SetMakeCardInputValue(0L);
		if (this.selfView.trans_EquipCard != null)
		{
			this.selfView.trans_EquipCard.gameObject.SetActive(this.cardMakeState == UI_MyCard.CardMakeState.Normal);
		}
		if (this.selfView.trans_cardMake != null)
		{
			this.selfView.trans_cardMake.gameObject.SetActive(this.cardMakeState > UI_MyCard.CardMakeState.Normal);
		}
		this.RefreshMakeCardView();
		this.RefreshMakeCardCaption();
		this.RefreshMakeCardButtonText();
		Game.TimerManager.AddLateUpdateAction(new Action(this.RefreshMakeCardButtonText));
		this.UpdateCardView();
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x0007461C File Offset: 0x0007281C
	private void RefreshMakeCardButtonText()
	{
		if (this.makeCardButtonText == null && this.selfView.btn_make != null)
		{
			this.makeCardButtonText = this.selfView.btn_make.GetComponentInChildren<Text>(true);
		}
		if (this.makeCardButtonText == null)
		{
			return;
		}
		if (this.cardMakeState == UI_MyCard.CardMakeState.Decompose)
		{
			this.makeCardButtonText.text = Game.Language.Get("分解", "");
			return;
		}
		if (this.cardMakeState == UI_MyCard.CardMakeState.Compose)
		{
			this.makeCardButtonText.text = Game.Language.Get("合成", "");
		}
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x000746C4 File Offset: 0x000728C4
	private void InitMakeCardNumInputField()
	{
		if (this.makeCardNumInputField == null)
		{
			return;
		}
		this.makeCardNumInputField.contentType = TMP_InputField.ContentType.IntegerNumber;
		this.makeCardNumInputField.text = "0";
		this.makeCardNumInputField.onEndEdit.AddListener(delegate(string _)
		{
			this.ClampMakeCardInputValue();
		});
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x00074718 File Offset: 0x00072918
	private void AddMakeCardNumHoldButtonEvent(Button button, int step)
	{
		if (button == null)
		{
			return;
		}
		EventTrigger eventTrigger = button.GetComponent<EventTrigger>();
		if (eventTrigger == null)
		{
			eventTrigger = button.gameObject.AddComponent<EventTrigger>();
		}
		this.AddMakeCardNumTriggerEvent(eventTrigger, EventTriggerType.PointerDown, delegate(BaseEventData _)
		{
			this.StartMakeCardNumHold(step);
		});
		this.AddMakeCardNumTriggerEvent(eventTrigger, EventTriggerType.PointerUp, delegate(BaseEventData _)
		{
			this.StopMakeCardNumHold();
		});
		this.AddMakeCardNumTriggerEvent(eventTrigger, EventTriggerType.PointerExit, delegate(BaseEventData _)
		{
			this.StopMakeCardNumHold();
		});
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x0007479C File Offset: 0x0007299C
	private void AddMakeCardNumTriggerEvent(EventTrigger trigger, EventTriggerType eventType, UnityAction<BaseEventData> action)
	{
		EventTrigger.Entry entry = new EventTrigger.Entry
		{
			eventID = eventType
		};
		entry.callback.AddListener(action);
		trigger.triggers.Add(entry);
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x000747D0 File Offset: 0x000729D0
	private void StartMakeCardNumHold(int step)
	{
		if (this.cardMakeState == UI_MyCard.CardMakeState.Normal)
		{
			return;
		}
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		this.ChangeMakeCardInputValue(step);
		this.holdMakeCardNumStep = step;
		this.nextMakeCardNumHoldTime = Time.unscaledTime + 0.35f;
		this.isHoldingMakeCardNum = true;
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x0007482D File Offset: 0x00072A2D
	private void StopMakeCardNumHold()
	{
		this.isHoldingMakeCardNum = false;
		this.holdMakeCardNumStep = 0;
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x0007483D File Offset: 0x00072A3D
	private void ChangeMakeCardInputValue(int step)
	{
		this.SetMakeCardInputValue((long)(this.GetMakeCardInputValue() + step));
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x0007484E File Offset: 0x00072A4E
	private void ClampMakeCardInputValue()
	{
		this.SetMakeCardInputValue((long)this.GetMakeCardInputValue());
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x00074860 File Offset: 0x00072A60
	private int GetMakeCardInputValue()
	{
		if (this.makeCardNumInputField == null || string.IsNullOrEmpty(this.makeCardNumInputField.text))
		{
			return 0;
		}
		long value;
		if (!long.TryParse(this.makeCardNumInputField.text, out value))
		{
			return 0;
		}
		return this.ClampMakeCardInputValue(value);
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x000748AC File Offset: 0x00072AAC
	private int ClampMakeCardInputValue(long value)
	{
		if (value <= 0L)
		{
			return 0;
		}
		int selectedMakeCardMaxNum = this.GetSelectedMakeCardMaxNum();
		if (value >= (long)selectedMakeCardMaxNum)
		{
			return selectedMakeCardMaxNum;
		}
		return (int)value;
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x000748D0 File Offset: 0x00072AD0
	private void SetMakeCardInputValue(long value)
	{
		if (this.makeCardNumInputField == null)
		{
			return;
		}
		this.makeCardNumInputField.SetTextWithoutNotify(this.ClampMakeCardInputValue(value).ToString());
		this.RefreshMakeCardView();
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x0007490C File Offset: 0x00072B0C
	private void SelectMakeCard(CardData cardData)
	{
		this.selectedMakeCardData = cardData;
		this.hasSelectedMakeCardData = true;
		this.SetMakeCardInputValue(0L);
		this.RefreshMakeCardView();
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x0007492C File Offset: 0x00072B2C
	private void RefreshMakeCardView()
	{
		if (this.makeCardInfoView != null)
		{
			this.makeCardInfoView.gameObject.SetActive(this.hasSelectedMakeCardData);
			if (this.hasSelectedMakeCardData)
			{
				this.makeCardInfoView.UpdateView(this.selectedMakeCardData, false, this.GetMakeCardDisplayCount(this.selectedMakeCardData.id), true);
			}
		}
		if (this.selfView.ltext_makeDec == null || this.selfView.ltext_UseDec == null)
		{
			return;
		}
		if (this.cardMakeState == UI_MyCard.CardMakeState.Normal)
		{
			this.selfView.ltext_makeDec.text = "";
			this.selfView.ltext_UseDec.text = "";
			this.SetMakeUseNumText(0);
			return;
		}
		if (!this.hasSelectedMakeCardData)
		{
			this.selfView.ltext_makeDec.text = ((this.cardMakeState == UI_MyCard.CardMakeState.Decompose) ? Game.Language.Get("请选择要分解的卡牌", "") : Game.Language.Get("请选择要合成的卡牌", ""));
			this.selfView.ltext_UseDec.text = this.GetMakeUseDecText();
			this.SetMakeUseNumText(0);
			return;
		}
		int makeCardInputValue = this.GetMakeCardInputValue();
		string text = Game.Language.Get(PathDefine.Concat("card_", this.selectedMakeCardData.id), "");
		if (this.cardMakeState == UI_MyCard.CardMakeState.Decompose)
		{
			int cardDecomposeDustReward = this.GetCardDecomposeDustReward(this.selectedMakeCardData);
			this.selfView.ltext_makeDec.text = string.Format("{0}{1}{2}{3}", new object[]
			{
				Game.Language.Get("分解", ""),
				text,
				StringDefine.X,
				makeCardInputValue
			});
			this.selfView.ltext_UseDec.text = this.GetMakeUseDecText();
			this.SetMakeUseNumText(cardDecomposeDustReward * makeCardInputValue);
			return;
		}
		int cardComposeDustCost = this.GetCardComposeDustCost(this.selectedMakeCardData);
		this.selfView.ltext_makeDec.text = string.Format("{0}{1}{2}{3}", new object[]
		{
			Game.Language.Get("合成", ""),
			text,
			StringDefine.X,
			makeCardInputValue
		});
		this.selfView.ltext_UseDec.text = this.GetMakeUseDecText();
		this.SetMakeUseNumText(cardComposeDustCost * Mathf.Max(1, makeCardInputValue));
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x00074B84 File Offset: 0x00072D84
	private string GetMakeUseDecText()
	{
		if (this.cardMakeState == UI_MyCard.CardMakeState.Decompose)
		{
			return Game.Language.Get("get", "");
		}
		if (this.cardMakeState == UI_MyCard.CardMakeState.Compose)
		{
			return Game.Language.Get("合成消耗", "");
		}
		return "";
	}

	// Token: 0x06001346 RID: 4934 RVA: 0x00074BD2 File Offset: 0x00072DD2
	private void SetMakeUseNumText(int value)
	{
		if (this.selfView.ltext_makeUseNum == null)
		{
			return;
		}
		this.selfView.ltext_makeUseNum.text = PathDefine.Concat(StringDefine.X, Mathf.Max(0, value));
	}

	// Token: 0x06001347 RID: 4935 RVA: 0x00074C10 File Offset: 0x00072E10
	private int GetSelectedMakeCardMaxNum()
	{
		if (!this.hasSelectedMakeCardData)
		{
			return 0;
		}
		if (this.cardMakeState == UI_MyCard.CardMakeState.Decompose)
		{
			return this.GetOwnedCardCountForMake(this.selectedMakeCardData.id);
		}
		if (this.cardMakeState != UI_MyCard.CardMakeState.Compose)
		{
			return 0;
		}
		int cardComposeDustCost = this.GetCardComposeDustCost(this.selectedMakeCardData);
		if (cardComposeDustCost <= 0)
		{
			return 0;
		}
		int a = this.ClampLongToInt(SaveLoadManager.gameSaveData.cardDust / (long)cardComposeDustCost);
		int b = Mathf.Max(0, 999 - this.GetWarehouseCardCount(this.selectedMakeCardData.id));
		return Mathf.Min(a, b);
	}

	// Token: 0x06001348 RID: 4936 RVA: 0x00074C97 File Offset: 0x00072E97
	private int GetOwnedCardCountForMake(int cardId)
	{
		return this.GetWarehouseCardCount(cardId) + this.CountCardInList(SaveLoadManager.gameSaveData.equipCards, cardId);
	}

	// Token: 0x06001349 RID: 4937 RVA: 0x00074CB2 File Offset: 0x00072EB2
	private int GetMakeCardDisplayCount(int cardId)
	{
		if (this.cardMakeState != UI_MyCard.CardMakeState.Normal)
		{
			return this.GetOwnedCardCountForMake(cardId);
		}
		return -1;
	}

	// Token: 0x0600134A RID: 4938 RVA: 0x00074CC8 File Offset: 0x00072EC8
	private int GetWarehouseCardCount(int cardId)
	{
		CardManager.HaveCardData haveCardData;
		if (SaveLoadManager.haveCardDataDic != null && SaveLoadManager.haveCardDataDic.TryGetValue(cardId, out haveCardData))
		{
			return Mathf.Max(0, haveCardData.haveNum);
		}
		return 0;
	}

	// Token: 0x0600134B RID: 4939 RVA: 0x00074CFC File Offset: 0x00072EFC
	private int GetCardDecomposeDustReward(CardData cardData)
	{
		int cardDustValue = this.GetCardDustValue(UI_MyCard.CardDecomposeDustRewards, cardData);
		return Mathf.Max(0, Mathf.FloorToInt((float)cardDustValue * Mathf.Max(0f, cardData.dustBreakLevel)));
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x00074D34 File Offset: 0x00072F34
	private int GetCardComposeDustCost(CardData cardData)
	{
		return this.GetCardDustValue(UI_MyCard.CardComposeDustCosts, cardData);
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x00074D44 File Offset: 0x00072F44
	private int GetCardDustValue(int[] values, CardData cardData)
	{
		if (values == null || values.Length == 0)
		{
			return 0;
		}
		int num = Mathf.Clamp(cardData.quality, 0, values.Length - 1);
		return Mathf.Max(0, values[num]);
	}

	// Token: 0x0600134E RID: 4942 RVA: 0x00074D75 File Offset: 0x00072F75
	public void RefreshDeckView()
	{
		this.RefreshEquipCardView();
		this.UpdateCardView();
		this.RefreshCardPresetDropdowns(0, 0);
		this.RefreshGoldAndJiYi();
	}

	// Token: 0x0600134F RID: 4943 RVA: 0x00074D94 File Offset: 0x00072F94
	public void RefreshEquipCardView()
	{
		this.selfView.pool_equip.RemoveAllView();
		foreach (int key in SaveLoadManager.gameSaveData.equipCards)
		{
			GameObject go = this.selfView.pool_equip.AddView();
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(key, out cardData))
			{
				this.SetCard(go, cardData, false);
			}
		}
		foreach (int key2 in EntityStatic.Get<CardManager>().teamCards)
		{
			GameObject go2 = this.selfView.pool_equip.AddView();
			CardData cardData2;
			if (Game.GameData.CardDataDic.TryGetValue(key2, out cardData2))
			{
				this.SetCard(go2, cardData2, true);
			}
		}
	}

	// Token: 0x06001350 RID: 4944 RVA: 0x00074E94 File Offset: 0x00073094
	private void RefreshCardPresetDropdowns(int selectedSavePresetIndex = 0, int selectedLoadPresetIndex = 0)
	{
		if (this.saveCardPresetDropdown == null || this.loadCardPresetDropdown == null)
		{
			return;
		}
		SaveLoadManager.EnsureEquipCardPresets();
		int equipCardPresetCount = SaveLoadManager.GetEquipCardPresetCount();
		bool flag = !GameHelperClient.InDungeon;
		this.currentSelectedCardPresetIndex = SaveLoadManager.GetCurrentEquipCardPresetIndex();
		if (this.currentSelectedCardPresetIndex > 0 && !SaveLoadManager.IsEquipCardPresetSaved(this.currentSelectedCardPresetIndex))
		{
			this.currentSelectedCardPresetIndex = 0;
			SaveLoadManager.SetCurrentEquipCardPresetIndex(0);
		}
		this.isRefreshingCardPresetDropdown = true;
		this.selfView.trans_saveCardPreset.gameObject.SetActive(flag);
		this.saveCardPresetDropdown.ClearOptions();
		this.saveCardPresetOperations.Clear();
		List<string> list = this.BuildCardPresetOperationOptions();
		this.saveCardPresetDropdown.AddOptions(list);
		if (list.Count > 0)
		{
			this.saveCardPresetDropdown.SetValueWithoutNotify(0);
			this.saveCardPresetDropdown.RefreshShownValue();
		}
		this.RefreshSaveCardPresetCaption();
		this.loadCardPresetDropdown.ClearOptions();
		this.loadCardPresetIndexes.Clear();
		List<string> list2 = new List<string>
		{
			Game.Language.Get("空预设", "")
		};
		this.loadCardPresetIndexes.Add(0);
		for (int i = 1; i <= equipCardPresetCount; i++)
		{
			if (SaveLoadManager.IsEquipCardPresetSaved(i))
			{
				SaveLoadManager.EquipCardPresetData equipCardPreset = SaveLoadManager.GetEquipCardPreset(i);
				this.loadCardPresetIndexes.Add(i);
				string item = (equipCardPreset != null) ? equipCardPreset.presetName : i.ToString();
				list2.Add(item);
			}
		}
		this.loadCardPresetDropdown.AddOptions(list2);
		this.SetupCardPresetDropdownTemplate(this.loadCardPresetDropdown, list2.Count, true);
		this.selfView.trans_loadCardPreset.gameObject.SetActive(flag);
		this.loadCardPresetDropdown.interactable = flag;
		int item2 = (selectedLoadPresetIndex > 0) ? selectedLoadPresetIndex : this.currentSelectedCardPresetIndex;
		int num = this.loadCardPresetIndexes.IndexOf(item2);
		if (num < 0)
		{
			num = 0;
		}
		this.loadCardPresetDropdown.SetValueWithoutNotify(num);
		this.loadCardPresetDropdown.RefreshShownValue();
		this.isRefreshingCardPresetDropdown = false;
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x00075084 File Offset: 0x00073284
	private List<string> BuildCardPresetOperationOptions()
	{
		List<string> list = new List<string>();
		this.saveCardPresetOperations.Add(UI_MyCard.CardPresetOperationType.SaveAsNew);
		list.Add(Game.Language.Get("保存为新预设", ""));
		if (this.currentSelectedCardPresetIndex > 0 && SaveLoadManager.IsEquipCardPresetSaved(this.currentSelectedCardPresetIndex))
		{
			this.saveCardPresetOperations.Add(UI_MyCard.CardPresetOperationType.SaveCurrent);
			list.Add(Game.Language.Get("保存当前预设", ""));
			this.saveCardPresetOperations.Add(UI_MyCard.CardPresetOperationType.RenameCurrent);
			list.Add(Game.Language.Get("重命名预设", ""));
			this.saveCardPresetOperations.Add(UI_MyCard.CardPresetOperationType.DeleteCurrent);
			list.Add(Game.Language.Get("删除预设", ""));
		}
		return list;
	}

	// Token: 0x06001352 RID: 4946 RVA: 0x00075146 File Offset: 0x00073346
	private void ResetDropdownCaption(Dropdown dropdown, string caption, bool resetValue)
	{
		if (dropdown == null)
		{
			return;
		}
		if (resetValue)
		{
			FieldInfo dropdownValueField = UI_MyCard.DropdownValueField;
			if (dropdownValueField != null)
			{
				dropdownValueField.SetValue(dropdown, -1);
			}
		}
		if (dropdown.captionText != null)
		{
			dropdown.captionText.text = caption;
		}
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x00075188 File Offset: 0x00073388
	private void SetupCardPresetDropdownTemplate(Dropdown dropdown, int optionCount, bool canVertical)
	{
		if (dropdown == null || dropdown.template == null)
		{
			return;
		}
		float size = Mathf.Min(480f, Mathf.Max(55f, (float)optionCount * 55f));
		dropdown.template.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
		ScrollRect component = dropdown.template.GetComponent<ScrollRect>();
		if (component != null)
		{
			component.horizontal = false;
			component.vertical = canVertical;
			component.scrollSensitivity = 55f;
			component.movementType = ScrollRect.MovementType.Clamped;
		}
	}

	// Token: 0x06001354 RID: 4948 RVA: 0x0007520C File Offset: 0x0007340C
	private int GetSelectedLoadCardPresetIndex()
	{
		if (this.loadCardPresetDropdown == null || this.loadCardPresetDropdown.value < 0 || this.loadCardPresetDropdown.value >= this.loadCardPresetIndexes.Count)
		{
			return 0;
		}
		return this.loadCardPresetIndexes[this.loadCardPresetDropdown.value];
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x00075268 File Offset: 0x00073468
	public void RefreshGoldAndJiYi()
	{
		this.selfView.ltext_jiyi.text = EntityStatic.Get<CardManager>().curPower.ToString() + "/" + SaveLoadManager.gameSaveData.maxCapacity.ToString();
		this.selfView.ltext_gold.text = SaveLoadManager.gameSaveData.memory.ToString();
		this.selfView.ltext_dust.text = SaveLoadManager.gameSaveData.cardDust.ToString();
		int num = (SaveLoadManager.gameSaveData.maxCapacity >= 40) ? 0 : this.UpLevelNeed[SaveLoadManager.gameSaveData.maxCapacity];
		this.selfView.ltext_upLevel.text = Game.Language.Get("升级容量", "") + StringDefine.Wrap + "   " + num.ToString();
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x00075348 File Offset: 0x00073548
	protected override void ClosePanel()
	{
		base.ClosePanel();
		this.StopMakeCardNumHold();
		if (!GameHelperClient.InDungeon)
		{
			SaveLoadManager.SaveGameData();
		}
		MySystemEvent.Instance.DispatchMessage(20);
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x00075370 File Offset: 0x00073570
	private void AddEquipCard(CardData cardData, GameObject cardObj)
	{
		Dictionary<int, CardManager.HaveCardData> haveCardDataDic = SaveLoadManager.haveCardDataDic;
		CardManager.HaveCardData haveCardData;
		if (!haveCardDataDic.TryGetValue(cardData.id, out haveCardData))
		{
			Util.ShowTips("卡牌数量不足");
			return;
		}
		if (haveCardData.haveNum == 0)
		{
			Util.ShowTips("卡牌数量不足");
			return;
		}
		int capacity = cardData.capacity;
		if (EntityStatic.Get<CardManager>().curPower + capacity > SaveLoadManager.gameSaveData.maxCapacity)
		{
			Util.ShowTips("记忆容量不足");
			return;
		}
		if (cardData.limit > 0)
		{
			List<int> equipCards = SaveLoadManager.gameSaveData.equipCards;
			int num = 0;
			using (List<int>.Enumerator enumerator = equipCards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == cardData.id)
					{
						num++;
					}
				}
			}
			if (num >= cardData.limit)
			{
				Util.ShowTips("超出携带上限");
				return;
			}
		}
		haveCardData.haveNum--;
		haveCardDataDic[cardData.id] = haveCardData;
		GameObject go = this.selfView.pool_equip.AddView();
		this.SetCard(go, cardData, false);
		cardObj.transform.GetComponent<CardView>().UpdateView(cardData, false, -1, false);
		EntityStatic.Get<CardManager>().AddEquipCard(cardData.id);
		EventSystem.current.SetSelectedGameObject(null);
	}

	// Token: 0x06001358 RID: 4952 RVA: 0x000754B0 File Offset: 0x000736B0
	private void SetCard(GameObject go, CardData cardData, bool isTeam)
	{
		go.transform.GetComponent<CardView>().UpdateView(cardData, isTeam, this.GetMakeCardDisplayCount(cardData.id), false);
		go.GetComponent<Button>().AddButtonEvent(delegate
		{
			if (GameHelperClient.InDungeon)
			{
				Util.ShowTips("不能在地下城使用");
				return;
			}
			this.selfView.pool_equip.RemoveView(go);
			EntityStatic.Get<CardManager>().RemoveEquipCard(cardData.id);
		});
	}

	// Token: 0x06001359 RID: 4953 RVA: 0x00075524 File Offset: 0x00073724
	private void OnMakeCardConfirmClick()
	{
		if (this.cardMakeState == UI_MyCard.CardMakeState.Normal)
		{
			return;
		}
		if (GameHelperClient.InDungeon)
		{
			Util.ShowTips("不能在地下城使用");
			return;
		}
		if (!this.hasSelectedMakeCardData)
		{
			Util.ShowTips((this.cardMakeState == UI_MyCard.CardMakeState.Decompose) ? "请选择要分解的卡牌" : "请选择要合成的卡牌");
			return;
		}
		if (this.cardMakeState == UI_MyCard.CardMakeState.Compose)
		{
			this.ComposeSelectedCard();
			return;
		}
		this.ConfirmDecomposeSelectedCard(false);
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x00075588 File Offset: 0x00073788
	private void ConfirmDecomposeSelectedCard(bool confirmedRisk)
	{
		int makeCardInputValue = this.GetMakeCardInputValue();
		if (makeCardInputValue <= 0)
		{
			Util.ShowTips("请输入数量");
			return;
		}
		int id = this.selectedMakeCardData.id;
		int ownedCardCountForMake = this.GetOwnedCardCountForMake(id);
		if (makeCardInputValue > ownedCardCountForMake)
		{
			Util.ShowTips("卡牌数量不足");
			this.SetMakeCardInputValue((long)ownedCardCountForMake);
			return;
		}
		int remainingCount = ownedCardCountForMake - makeCardInputValue;
		string text;
		if (!confirmedRisk && this.TryBuildDecomposeRiskText(id, makeCardInputValue, remainingCount, out text))
		{
			(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText(text, delegate
			{
				this.ConfirmDecomposeSelectedCard(true);
			}, delegate
			{
				this.RefreshMakeCardView();
			}, null, "");
			return;
		}
		this.DecomposeSelectedCard(makeCardInputValue, remainingCount);
	}

	// Token: 0x0600135B RID: 4955 RVA: 0x00075628 File Offset: 0x00073828
	private bool TryBuildDecomposeRiskText(int cardId, int decomposeNum, int remainingCount, out string riskText)
	{
		riskText = "";
		string text = Game.Language.Get(PathDefine.Concat("card_", cardId), "");
		int warehouseCardCount = this.GetWarehouseCardCount(cardId);
		int num = Mathf.Max(0, decomposeNum - warehouseCardCount);
		if (num > 0)
		{
			riskText = string.Format("{0}{1}{2}{3}{4}{5}", new object[]
			{
				riskText,
				Game.Language.Get("会从当前装备中分解", ""),
				text,
				StringDefine.X,
				num,
				StringDefine.Wrap
			});
		}
		foreach (string str in this.BuildPresetOverLimitWarnings(cardId, remainingCount, text))
		{
			riskText = riskText + str + StringDefine.Wrap;
		}
		if (string.IsNullOrEmpty(riskText))
		{
			return false;
		}
		riskText = riskText + StringDefine.Wrap + Game.Language.Get("分解会永久删除卡牌，是否继续？", "");
		return true;
	}

	// Token: 0x0600135C RID: 4956 RVA: 0x00075748 File Offset: 0x00073948
	private List<string> BuildPresetOverLimitWarnings(int cardId, int maxCount, string cardName)
	{
		List<string> list = new List<string>();
		SaveLoadManager.EnsureEquipCardPresets();
		if (SaveLoadManager.gameSaveData.equipCardPresets == null)
		{
			return list;
		}
		foreach (SaveLoadManager.EquipCardPresetData equipCardPresetData in SaveLoadManager.gameSaveData.equipCardPresets)
		{
			if (equipCardPresetData != null && equipCardPresetData.isSaved && equipCardPresetData.equipCards != null)
			{
				int num = this.CountCardInList(equipCardPresetData.equipCards, cardId);
				if (num > maxCount)
				{
					list.Add(string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", new object[]
					{
						Game.Language.Get("预设", ""),
						equipCardPresetData.presetName,
						Game.Language.Get("包含", ""),
						cardName,
						StringDefine.X,
						num,
						Game.Language.Get("分解后最多保留", ""),
						StringDefine.X,
						maxCount
					}));
				}
			}
		}
		return list;
	}

	// Token: 0x0600135D RID: 4957 RVA: 0x00075874 File Offset: 0x00073A74
	private void DecomposeSelectedCard(int decomposeNum, int remainingCount)
	{
		int id = this.selectedMakeCardData.id;
		int cardDecomposeDustReward = this.GetCardDecomposeDustReward(this.selectedMakeCardData);
		int num = Mathf.Min(this.GetWarehouseCardCount(id), decomposeNum);
		int num2 = decomposeNum - num;
		this.RemoveWarehouseCards(id, num);
		if (num2 > 0)
		{
			this.RemoveCardFromList(SaveLoadManager.gameSaveData.equipCards, id, num2);
			SaveLoadManager.SetCurrentEquipCardPresetIndex(0);
		}
		if (this.TrimEquipCardPresets(id, remainingCount))
		{
			SaveLoadManager.SetCurrentEquipCardPresetIndex(0);
		}
		this.AddCardDust((long)cardDecomposeDustReward * (long)decomposeNum);
		CardManager cardManager = EntityStatic.Get<CardManager>();
		if (cardManager != null)
		{
			cardManager.RefreshEquipCardsFromSave(false);
		}
		this.RefreshEquipCardView();
		this.UpdateCardView();
		this.RefreshCardPresetDropdowns(0, 0);
		this.RefreshGoldAndJiYi();
		this.SetMakeCardInputValue(0L);
		this.RefreshMakeCardView();
		SaveLoadManager.SaveGameData();
		Util.ShowTips("分解成功");
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x00075934 File Offset: 0x00073B34
	private void RemoveWarehouseCards(int cardId, int removeCount)
	{
		if (removeCount <= 0)
		{
			return;
		}
		if (SaveLoadManager.haveCardDataDic == null)
		{
			SaveLoadManager.haveCardDataDic = new Dictionary<int, CardManager.HaveCardData>();
		}
		CardManager.HaveCardData haveCardData;
		if (!SaveLoadManager.haveCardDataDic.TryGetValue(cardId, out haveCardData))
		{
			return;
		}
		haveCardData.haveNum = Mathf.Max(0, haveCardData.haveNum - removeCount);
		SaveLoadManager.haveCardDataDic[cardId] = haveCardData;
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x00075988 File Offset: 0x00073B88
	private bool TrimEquipCardPresets(int cardId, int maxCount)
	{
		bool result = false;
		SaveLoadManager.EnsureEquipCardPresets();
		if (SaveLoadManager.gameSaveData.equipCardPresets == null)
		{
			return false;
		}
		foreach (SaveLoadManager.EquipCardPresetData equipCardPresetData in SaveLoadManager.gameSaveData.equipCardPresets)
		{
			if (equipCardPresetData != null && equipCardPresetData.isSaved && equipCardPresetData.equipCards != null)
			{
				int num = this.CountCardInList(equipCardPresetData.equipCards, cardId) - maxCount;
				if (num > 0)
				{
					this.RemoveCardFromList(equipCardPresetData.equipCards, cardId, num);
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x06001360 RID: 4960 RVA: 0x00075A28 File Offset: 0x00073C28
	private int CountCardInList(List<int> cardIds, int cardId)
	{
		if (cardIds == null)
		{
			return 0;
		}
		int num = 0;
		using (List<int>.Enumerator enumerator = cardIds.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == cardId)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06001361 RID: 4961 RVA: 0x00075A80 File Offset: 0x00073C80
	private void RemoveCardFromList(List<int> cardIds, int cardId, int removeCount)
	{
		if (cardIds == null || removeCount <= 0)
		{
			return;
		}
		int num = cardIds.Count - 1;
		while (num >= 0 && removeCount > 0)
		{
			if (cardIds[num] == cardId)
			{
				cardIds.RemoveAt(num);
				removeCount--;
			}
			num--;
		}
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x00075AC4 File Offset: 0x00073CC4
	private void AddCardDust(long addValue)
	{
		if (addValue <= 0L)
		{
			return;
		}
		SaveLoadManager.gameSaveData.cardDust = Math.Max(0L, SaveLoadManager.gameSaveData.cardDust);
		if (SaveLoadManager.gameSaveData.cardDust > 9223372036854775807L - addValue)
		{
			SaveLoadManager.gameSaveData.cardDust = long.MaxValue;
			return;
		}
		SaveLoadManager.gameSaveData.cardDust += addValue;
	}

	// Token: 0x06001363 RID: 4963 RVA: 0x00075B30 File Offset: 0x00073D30
	private void ComposeSelectedCard()
	{
		int makeCardInputValue = this.GetMakeCardInputValue();
		if (makeCardInputValue <= 0)
		{
			Util.ShowTips("请输入数量");
			return;
		}
		int id = this.selectedMakeCardData.id;
		int cardComposeDustCost = this.GetCardComposeDustCost(this.selectedMakeCardData);
		if (cardComposeDustCost <= 0)
		{
			Util.ShowTips("合成消耗配置错误");
			return;
		}
		long num = (long)cardComposeDustCost * (long)makeCardInputValue;
		if (num > SaveLoadManager.gameSaveData.cardDust)
		{
			Util.ShowTips("粉尘不足");
			this.SetMakeCardInputValue((long)this.GetSelectedMakeCardMaxNum());
			return;
		}
		int num2 = Mathf.Max(0, 999 - this.GetWarehouseCardCount(id));
		if (makeCardInputValue > num2)
		{
			Util.ShowTips("卡牌数量上限");
			this.SetMakeCardInputValue((long)num2);
			return;
		}
		SaveLoadManager.gameSaveData.cardDust = Math.Max(0L, SaveLoadManager.gameSaveData.cardDust - num);
		this.AddWarehouseCards(id, makeCardInputValue);
		this.UpdateCardView();
		this.RefreshGoldAndJiYi();
		this.SetMakeCardInputValue(0L);
		this.RefreshMakeCardView();
		SaveLoadManager.SaveGameData();
		Util.ShowTips("合成成功");
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x00075C24 File Offset: 0x00073E24
	private void AddWarehouseCards(int cardId, int addCount)
	{
		if (addCount <= 0)
		{
			return;
		}
		if (SaveLoadManager.haveCardDataDic == null)
		{
			SaveLoadManager.haveCardDataDic = new Dictionary<int, CardManager.HaveCardData>();
		}
		CardManager.HaveCardData haveCardData;
		if (SaveLoadManager.haveCardDataDic.TryGetValue(cardId, out haveCardData))
		{
			haveCardData.haveNum = Mathf.Min(999, haveCardData.haveNum + addCount);
			SaveLoadManager.haveCardDataDic[cardId] = haveCardData;
			return;
		}
		SaveLoadManager.haveCardDataDic.Add(cardId, new CardManager.HaveCardData
		{
			cardId = cardId,
			haveNum = Mathf.Min(999, addCount)
		});
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x0006D931 File Offset: 0x0006BB31
	private int ClampLongToInt(long value)
	{
		if (value <= 0L)
		{
			return 0;
		}
		if (value >= 2147483647L)
		{
			return int.MaxValue;
		}
		return (int)value;
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x00075CAA File Offset: 0x00073EAA
	public void ShowCurPower(int power)
	{
		this.selfView.ltext_jiyi.text = power.ToString() + "/" + SaveLoadManager.gameSaveData.maxCapacity.ToString();
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x0006D9FB File Offset: 0x0006BBFB
	private void ApplyData()
	{
		base.CloseSelfPanel();
	}

	// Token: 0x06001368 RID: 4968 RVA: 0x00075CDC File Offset: 0x00073EDC
	public static string GetCardInfo(CardData cardData)
	{
		string str = "";
		if (cardData.isTeam)
		{
			str = str + Game.Language.Get("所有人生效", "") + StringDefine.Wrap;
		}
		CardEntries entries = cardData.entries;
		if (!Mathf.Approximately(entries.critical, 0f))
		{
			if (entries.critical > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("baojiLv", ""), entries.critical * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("baojiLv", ""), entries.critical * 100f);
			}
		}
		if (!Mathf.Approximately(entries.criticalDamage, 0f))
		{
			if (entries.criticalDamage > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("baojiDamage", ""), entries.criticalDamage * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("baojiDamage", ""), entries.criticalDamage * 100f);
			}
		}
		if (entries.attack != 0)
		{
			if (entries.attack > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("attack", ""), entries.attack);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("attack", ""), entries.attack);
			}
		}
		if (!Mathf.Approximately(entries.attackSpeed, 0f))
		{
			if (entries.attackSpeed > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("attackSpeed", ""), entries.attackSpeed * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("attackSpeed", ""), -entries.attackSpeed * 100f);
			}
		}
		if (entries.attackAddHp != 0)
		{
			if (entries.attackAddHp > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("xixue", ""), entries.attackAddHp);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("xixue", ""), entries.attackAddHp);
			}
		}
		if (!Mathf.Approximately(entries.moveSpeed, 0f))
		{
			if (entries.moveSpeed > 0f)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("moveSpeed", ""), entries.moveSpeed);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("moveSpeed", ""), entries.moveSpeed);
			}
		}
		if (entries.sta != 0)
		{
			if (entries.sta > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("sta", ""), entries.sta);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("sta", ""), entries.sta);
			}
		}
		if (entries.agi != 0)
		{
			if (entries.agi > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("dex", ""), entries.agi);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("dex", ""), entries.agi);
			}
		}
		if (entries.str != 0)
		{
			if (entries.str > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("str", ""), entries.str);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("str", ""), entries.str);
			}
		}
		if (entries.armor != 0)
		{
			if (entries.armor > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("armor", ""), entries.armor);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("armor", ""), entries.armor);
			}
		}
		if (entries.hpAdd != 0)
		{
			if (entries.hpAdd > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("hpAddSec", ""), entries.hpAdd);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("hpAddSec", ""), entries.hpAdd);
			}
		}
		if (entries.mpAdd != 0)
		{
			if (entries.mpAdd > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("mpAddSec", ""), entries.mpAdd);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("mpAddSec", ""), entries.mpAdd);
			}
		}
		if (entries.startMoney != 0)
		{
			if (entries.startMoney > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("初始金币", ""), entries.startMoney);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("初始金币", ""), entries.startMoney);
			}
		}
		if (entries.startGem != 0)
		{
			if (entries.startGem > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("初始骷髅币", ""), entries.startGem);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("初始骷髅币", ""), entries.startGem);
			}
		}
		if (entries.lucky != 0)
		{
			if (entries.lucky > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("幸运值", ""), entries.lucky);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("幸运值", ""), entries.lucky);
			}
		}
		if (!Mathf.Approximately(entries.skillDamage, 0f))
		{
			if (entries.skillDamage > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("法术伤害加成", ""), entries.skillDamage * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("法术伤害加成", ""), entries.skillDamage * 100f);
			}
		}
		if (!Mathf.Approximately(entries.skillRange, 0f))
		{
			if (entries.skillRange > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("技能范围", ""), entries.skillRange * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("技能范围", ""), entries.skillRange * 100f);
			}
		}
		if (!Mathf.Approximately(entries.skillTime, 0f))
		{
			if (entries.skillTime > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("技能持续时间", ""), entries.skillTime * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("技能持续时间", ""), entries.skillTime * 100f);
			}
		}
		if (!Mathf.Approximately(entries.skillExpend, 0f))
		{
			if (entries.skillExpend > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("法力值消耗", ""), entries.skillExpend * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("法力值消耗", ""), entries.skillExpend * 100f);
			}
		}
		if (entries.skillCd != 0)
		{
			if (entries.skillCd > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("技能急速", ""), entries.skillCd);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("技能急速", ""), entries.skillCd);
			}
		}
		if (!Mathf.Approximately(entries.expAdd, 0f))
		{
			if (entries.expAdd > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("经验获取", ""), entries.expAdd * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("经验获取", ""), entries.expAdd * 100f);
			}
		}
		if (!Mathf.Approximately(entries.normalDamage, 0f))
		{
			if (entries.normalDamage > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("物理伤害加成", ""), entries.normalDamage * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("物理伤害加成", ""), entries.normalDamage * 100f);
			}
		}
		if (entries.maxHp != 0)
		{
			if (entries.maxHp > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("生命值", ""), entries.maxHp);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("生命值", ""), entries.maxHp);
			}
		}
		if (entries.maxMp != 0)
		{
			if (entries.maxMp > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("法力值", ""), entries.maxMp);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("法力值", ""), entries.maxMp);
			}
		}
		if (!Mathf.Approximately(entries.normalBreak, 0f))
		{
			if (entries.normalBreak > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("物理破盾加成", ""), entries.normalBreak * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("物理破盾加成", ""), entries.normalBreak * 100f);
			}
		}
		if (!Mathf.Approximately(entries.skillBreak, 0f))
		{
			if (entries.skillBreak > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("法术破盾伤害", ""), entries.skillBreak * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("法术破盾伤害", ""), entries.skillBreak * 100f);
			}
		}
		if (!Mathf.Approximately(entries.allDamage, 0f))
		{
			if (entries.allDamage > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("总伤害加成", ""), entries.allDamage * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("总伤害加成", ""), entries.allDamage * 100f);
			}
		}
		if (!Mathf.Approximately(entries.addMoney, 0f))
		{
			if (entries.addMoney > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("金币获取", ""), entries.addMoney * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("金币获取", ""), entries.addMoney * 100f);
			}
		}
		if (entries.addEnemyLimit != 0)
		{
			if (entries.addEnemyLimit > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("敌人上限", ""), entries.addEnemyLimit);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("敌人上限", ""), entries.addEnemyLimit);
			}
		}
		if (entries.refreshNum != 0)
		{
			if (entries.refreshNum > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("刷新次数", ""), entries.refreshNum);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("刷新次数", ""), entries.refreshNum);
			}
		}
		if (!Mathf.Approximately(entries.lifeStealing, 0f))
		{
			if (entries.lifeStealing > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("攻击生命偷取", ""), entries.lifeStealing * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("攻击生命偷取", ""), entries.lifeStealing * 100f);
			}
		}
		if (entries.reduceInjury != 0)
		{
			if (entries.reduceInjury > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("gdj", ""), entries.reduceInjury);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("gdj", ""), entries.reduceInjury);
			}
		}
		if (entries.extraDamage != 0)
		{
			if (entries.extraDamage > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("exs", ""), entries.extraDamage);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("exs", ""), entries.extraDamage);
			}
		}
		if (entries.dodge != 0)
		{
			if (entries.dodge > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("闪避值", ""), entries.dodge);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("闪避值", ""), entries.dodge);
			}
		}
		if (!Mathf.Approximately(entries.hpPercent, 0f))
		{
			if (entries.hpPercent > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("最大生命值提升", ""), entries.hpPercent * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("最大生命值提升", ""), entries.hpPercent * 100f);
			}
		}
		if (!Mathf.Approximately(entries.hpSecRate, 0f))
		{
			if (entries.hpSecRate > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("hpAddSec", ""), entries.hpSecRate * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("hpAddSec", ""), entries.hpSecRate * 100f);
			}
		}
		if (entries.skillReduction != 0)
		{
			if (entries.skillReduction > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("技能抵抗", ""), entries.skillReduction);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("技能抵抗", ""), entries.skillReduction);
			}
		}
		if (!Mathf.Approximately(entries.strPercent, 0f))
		{
			if (entries.strPercent > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("str", ""), entries.strPercent * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("str", ""), entries.strPercent * 100f);
			}
		}
		if (!Mathf.Approximately(entries.agiPercent, 0f))
		{
			if (entries.agiPercent > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("dex", ""), entries.agiPercent * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("dex", ""), entries.agiPercent * 100f);
			}
		}
		if (!Mathf.Approximately(entries.staPercent, 0f))
		{
			if (entries.staPercent > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("sta", ""), entries.staPercent * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("sta", ""), entries.staPercent * 100f);
			}
		}
		if (!Mathf.Approximately(entries.attackDistance, 0f))
		{
			if (entries.attackDistance > 0f)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("攻击距离", ""), entries.attackDistance);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("攻击距离", ""), entries.attackDistance);
			}
		}
		if (!Mathf.Approximately(entries.fireDamage, 0f))
		{
			if (entries.fireDamage > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("火焰伤害", ""), entries.fireDamage * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("火焰伤害", ""), entries.fireDamage * 100f);
			}
		}
		if (!Mathf.Approximately(entries.iceDamage, 0f))
		{
			if (entries.iceDamage > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("冰冻伤害", ""), entries.iceDamage * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("冰冻伤害", ""), entries.iceDamage * 100f);
			}
		}
		if (!Mathf.Approximately(entries.lightDamage, 0f))
		{
			if (entries.lightDamage > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("雷电伤害", ""), entries.lightDamage * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("雷电伤害", ""), entries.lightDamage * 100f);
			}
		}
		if (!Mathf.Approximately(entries.relicAdd, 0f))
		{
			if (entries.relicAdd > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("遗物稀有度", ""), entries.relicAdd * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("遗物稀有度", ""), entries.relicAdd * 100f);
			}
		}
		if (!Mathf.Approximately(entries.bookAdd, 0f))
		{
			if (entries.bookAdd > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("技能书稀有度", ""), entries.bookAdd * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("技能书稀有度", ""), entries.bookAdd * 100f);
			}
		}
		if (!Mathf.Approximately(entries.forgingAdd, 0f))
		{
			if (entries.forgingAdd > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("锻造器稀有度", ""), entries.forgingAdd * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("锻造器稀有度", ""), entries.forgingAdd * 100f);
			}
		}
		if (!Mathf.Approximately(entries.effectDamage, 0f))
		{
			if (entries.effectDamage > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("攻击特效加成", ""), entries.effectDamage * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("攻击特效加成", ""), entries.effectDamage * 100f);
			}
		}
		if (!Mathf.Approximately(entries.buffDamage, 0f))
		{
			if (entries.buffDamage > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("BUFF伤害加成", ""), entries.buffDamage * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("BUFF伤害加成", ""), entries.buffDamage * 100f);
			}
		}
		if (entries.relifeTime != 0)
		{
			if (entries.relifeTime > 0)
			{
				str += string.Format("{0}+{1}\n", Game.Language.Get("复活时间", ""), entries.relifeTime);
			}
			else
			{
				str += string.Format("{0}{1}\n", Game.Language.Get("复活时间", ""), entries.relifeTime);
			}
		}
		if (!Mathf.Approximately(entries.addCall, 0f))
		{
			if (entries.addCall > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("召唤物强度", ""), entries.addCall * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("召唤物强度", ""), entries.addCall * 100f);
			}
		}
		if (!Mathf.Approximately(entries.addHenshin, 0f))
		{
			if (entries.addHenshin > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("变身强度", ""), entries.addHenshin * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("变身强度", ""), entries.addHenshin * 100f);
			}
		}
		if (!Mathf.Approximately(entries.addNormalEnemy, 0f))
		{
			if (entries.addNormalEnemy > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("对普通敌人伤害", ""), entries.addNormalEnemy * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("对普通敌人伤害", ""), entries.addNormalEnemy * 100f);
			}
		}
		if (!Mathf.Approximately(entries.addBossEnemy, 0f))
		{
			if (entries.addBossEnemy > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("对BOSS伤害", ""), entries.addBossEnemy * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("对BOSS伤害", ""), entries.addBossEnemy * 100f);
			}
		}
		if (!Mathf.Approximately(entries.attackPercent, 0f))
		{
			if (entries.attackPercent > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("attack", ""), entries.attackPercent * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("attack", ""), entries.attackPercent * 100f);
			}
		}
		if (!Mathf.Approximately(entries.forgingAddValue, 0f))
		{
			if (entries.forgingAddValue > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("属性锻造器增幅", ""), entries.forgingAddValue * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("属性锻造器增幅", ""), entries.forgingAddValue * 100f);
			}
		}
		if (!Mathf.Approximately(entries.equipAddValue, 0f))
		{
			if (entries.equipAddValue > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("装备属性加成", ""), entries.equipAddValue * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("装备属性加成", ""), entries.equipAddValue * 100f);
			}
		}
		if (!Mathf.Approximately(entries.hpAddUpgrade, 0f))
		{
			if (entries.hpAddUpgrade > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("生命回复加成", ""), entries.hpAddUpgrade * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("生命回复加成", ""), entries.hpAddUpgrade * 100f);
			}
		}
		if (!Mathf.Approximately(entries.armedAdd, 0f))
		{
			if (entries.armedAdd > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("武装伤害", ""), entries.armedAdd * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("武装伤害", ""), entries.armedAdd * 100f);
			}
		}
		if (!Mathf.Approximately(entries.castSpeed, 0f))
		{
			if (entries.castSpeed > 0f)
			{
				str += string.Format("{0}+{1}%\n", Game.Language.Get("施法速度提升", ""), entries.castSpeed * 100f);
			}
			else
			{
				str += string.Format("{0}{1}%\n", Game.Language.Get("施法速度提升", ""), entries.castSpeed * 100f);
			}
		}
		return str + Game.Language.Get(PathDefine.Concat("card_", cardData.id, "_m"), "");
	}

	// Token: 0x06001369 RID: 4969 RVA: 0x00077C20 File Offset: 0x00075E20
	private void AddRoom()
	{
		if (SaveLoadManager.gameSaveData.maxCapacity >= 40)
		{
			Util.ShowTips("卡牌容量上限");
			return;
		}
		long num = SaveLoadManager.gameSaveData.memory;
		int num2 = this.UpLevelNeed[SaveLoadManager.gameSaveData.maxCapacity];
		if (num < (long)num2)
		{
			Util.ShowTips("记忆不足");
			return;
		}
		num -= (long)num2;
		SaveLoadManager.gameSaveData.maxCapacity++;
		if (SaveLoadManager.gameSaveData.maxCapacity >= 40)
		{
			SaveLoadManager.gameSaveData.maxCapacity = 40;
		}
		SaveLoadManager.SetMaxPower(SaveLoadManager.gameSaveData.maxCapacity);
		SaveLoadManager.SetJiyi(num);
		this.selfView.ltext_gold.text = num.ToString();
		Util.ShowTips("tip_levelUpSuccess");
		UI_MyCard ui = Game.UI.GetUI<UI_MyCard>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshGoldAndJiYi();
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x00077CF0 File Offset: 0x00075EF0
	public void ShowCardInfo(Vector3 cardPosition, CardData cardData, bool showOnLeft = false)
	{
		this.tipInfoRect.gameObject.SetActive(true);
		this.tipInfoRect.position = cardPosition;
		if (showOnLeft)
		{
			this.tipInfoRect.anchoredPosition += new Vector2(-374f, -100f);
		}
		else
		{
			this.tipInfoRect.anchoredPosition += new Vector2(115f, 0f);
		}
		this.selfView.ltext_tipTitle.text = Game.Language.Get("获取途径", "");
		this.selfView.ltext_tipInfo.text = this.GetCardTipInfo(cardData);
	}

	// Token: 0x0600136B RID: 4971 RVA: 0x00077DA4 File Offset: 0x00075FA4
	private string GetCardTipInfo(CardData cardData)
	{
		string text = "";
		if (cardData.unlockType == UnlockType.Drop)
		{
			if (((Dictionary<string, object>)ExcelManager.allExcelData["enemy"].DIC(cardData.unlockData)).DIC("enemyType").Equals("boss"))
			{
				text = text + string.Format(Game.Language.Get("掉落说明", ""), PathDefine.Concat("BOSS", StringDefine.Point, Game.Language.Get(cardData.unlockData, ""))) + "\n\n" + string.Format(Game.Language.Get("掉落率", ""), cardData.unlockValue * 100f);
			}
			else
			{
				text = text + string.Format(Game.Language.Get("掉落说明", ""), PathDefine.Concat(Game.Language.Get("精英", ""), StringDefine.Point, Game.Language.Get(cardData.unlockData, ""))) + "\n\n" + string.Format(Game.Language.Get("掉落率", ""), cardData.unlockValue * 100f);
			}
		}
		else if (cardData.unlockType == UnlockType.Total)
		{
			int num = 0;
			CardManager.HaveCardData haveCardData;
			if (SaveLoadManager.haveCardDataDic.TryGetValue(cardData.id, out haveCardData))
			{
				num = haveCardData.curProgress;
			}
			if (cardData.unlockData.Contains("HeroWin"))
			{
				string[] array = cardData.unlockData.Split("_", StringSplitOptions.None);
				string heroName = Util.GetHeroName((HeroType)int.Parse(array[array.Length - 1]));
				text = string.Concat(new string[]
				{
					text,
					string.Format(Game.Language.Get("CardTotal_HeroWin", ""), string.Format(ColorDefine.NormalColor, heroName), string.Format(ColorDefine.NormalColor, cardData.progress)),
					"\n\n",
					Game.Language.Get("当前进度", ""),
					string.Format(ColorDefine.RedForColor, num)
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					text,
					string.Format(Game.Language.Get("CardTotal_" + cardData.unlockData, ""), string.Format(ColorDefine.NormalColor, cardData.progress)),
					"\n\n",
					Game.Language.Get("当前进度", ""),
					string.Format(ColorDefine.RedForColor, num)
				});
			}
		}
		if (cardData.limit > 0)
		{
			string c = PathDefine.Concat(Game.Language.Get("携带上限", ""), StringDefine.ColonSpace, string.Format(ColorDefine.NormalColor, cardData.limit));
			text = PathDefine.Concat(text, "\n\n", c);
		}
		return text + "\n\n" + PathDefine.Concat(Game.Language.Get("品质", ""), StringDefine.ColonSpace, string.Format(ColorDefine.QuaRelicText[cardData.quality], Game.Language.Get("quality_" + cardData.quality.ToString(), "")));
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x00078101 File Offset: 0x00076301
	public void HideCardInfo()
	{
		this.tipInfoRect.gameObject.SetActive(false);
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x00078114 File Offset: 0x00076314
	public void ShowEquipCardInfo(Vector3 cardPosition, CardData cardData)
	{
		this.equipInfoView.gameObject.SetActive(true);
		this.equipInfoView.transform.position = new Vector3(this.equipInfoView.transform.position.x, cardPosition.y, this.equipInfoView.transform.position.z);
		this.equipInfoView.UpdateView(cardData, false, -1, false);
	}

	// Token: 0x0600136E RID: 4974 RVA: 0x00078186 File Offset: 0x00076386
	public void HideEquipCardInfo()
	{
		this.equipInfoView.gameObject.SetActive(false);
	}

	// Token: 0x0600136F RID: 4975 RVA: 0x0007819C File Offset: 0x0007639C
	public void UpdateCardNum(int cardId)
	{
		foreach (GameObject gameObject in this.selfView.pool_cangku.viewList)
		{
			CardView component = gameObject.GetComponent<CardView>();
			if (component != null && component.CardId == cardId && Game.GameData.CardDataDic.ContainsKey(cardId))
			{
				component.UpdateView(Game.GameData.CardDataDic[cardId], false, -1, false);
			}
		}
	}

	// Token: 0x06001370 RID: 4976 RVA: 0x00078234 File Offset: 0x00076434
	private void OnAllDropdownChanged(int index)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		this.allDropType = (UI_MyCard.AllDropType)index;
		this.UpdateCardView();
	}

	// Token: 0x06001371 RID: 4977 RVA: 0x00078264 File Offset: 0x00076464
	private void OnSortDropdownChanged(int index)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		this.cardSortType = (UI_MyCard.CardSortType)index;
		this.UpdateCardView();
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x00078294 File Offset: 0x00076494
	private void OnMakeCardDropdownChanged(int index)
	{
		if (index < 0)
		{
			return;
		}
		if (GameHelperClient.InDungeon)
		{
			Util.ShowTips("不能在地下城使用");
			this.SetCardMakeState(UI_MyCard.CardMakeState.Normal);
			return;
		}
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		if (index == 0)
		{
			Action action = this.onMakeCardDecomposeCallback;
			if (action != null)
			{
				action();
			}
		}
		else if (index == 1)
		{
			Action action2 = this.onMakeCardComposeCallback;
			if (action2 != null)
			{
				action2();
			}
		}
		else if (index == 2)
		{
			Action action3 = this.onMakeCardCancelCallback;
			if (action3 != null)
			{
				action3();
			}
		}
		this.RefreshMakeCardCaption();
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x0007832C File Offset: 0x0007652C
	private void OnSaveCardPresetDropdownChanged(int index)
	{
		if (this.isRefreshingCardPresetDropdown || GameHelperClient.InDungeon || index < 0 || index >= this.saveCardPresetOperations.Count)
		{
			return;
		}
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		UI_MyCard.CardPresetOperationType cardPresetOperationType = this.saveCardPresetOperations[index];
		if (cardPresetOperationType == UI_MyCard.CardPresetOperationType.SaveAsNew)
		{
			this.ShowCreateCardPresetConfirm();
		}
		else if (cardPresetOperationType == UI_MyCard.CardPresetOperationType.SaveCurrent)
		{
			if (this.currentSelectedCardPresetIndex > 0 && SaveLoadManager.SaveEquipCardPreset(this.currentSelectedCardPresetIndex, null))
			{
				this.RefreshCardPresetDropdowns(this.currentSelectedCardPresetIndex, this.currentSelectedCardPresetIndex);
			}
		}
		else if (cardPresetOperationType == UI_MyCard.CardPresetOperationType.RenameCurrent)
		{
			this.ShowRenameCardPresetConfirm();
		}
		else if (cardPresetOperationType == UI_MyCard.CardPresetOperationType.DeleteCurrent)
		{
			this.DeleteCurrentCardPreset();
		}
		this.RefreshSaveCardPresetCaption();
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x000783E0 File Offset: 0x000765E0
	private void ShowCreateCardPresetConfirm()
	{
		(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText("", null, delegate
		{
			this.RefreshCardPresetDropdowns(0, 0);
		}, new Action<string>(this.OnCreateCardPresetInput), Game.Language.Get("请输入预设名称", ""));
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x00078434 File Offset: 0x00076634
	private void OnCreateCardPresetInput(string presetName)
	{
		int num;
		if (SaveLoadManager.SaveEquipCardPresetToNew(presetName, out num))
		{
			this.currentSelectedCardPresetIndex = num;
			this.RefreshCardPresetDropdowns(num, num);
			return;
		}
		Util.ShowTips("预设已满");
		this.RefreshCardPresetDropdowns(0, 0);
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x00078470 File Offset: 0x00076670
	private void OnLoadCardPresetDropdownChanged(int index)
	{
		if (this.isRefreshingCardPresetDropdown || GameHelperClient.InDungeon || index < 0 || index >= this.loadCardPresetIndexes.Count)
		{
			return;
		}
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		int num = this.loadCardPresetIndexes[index];
		if (num == 0)
		{
			SaveLoadManager.ClearEquipCards(true);
			this.currentSelectedCardPresetIndex = 0;
			this.RefreshCardPresetDropdowns(0, 0);
			return;
		}
		if (SaveLoadManager.LoadEquipCardPreset(num, true))
		{
			this.currentSelectedCardPresetIndex = num;
			this.RefreshCardPresetDropdowns(num, num);
		}
	}

	// Token: 0x06001377 RID: 4983 RVA: 0x00078500 File Offset: 0x00076700
	private void ShowRenameCardPresetConfirm()
	{
		if (this.currentSelectedCardPresetIndex <= 0 || !SaveLoadManager.IsEquipCardPresetSaved(this.currentSelectedCardPresetIndex))
		{
			this.RefreshCardPresetDropdowns(0, 0);
			return;
		}
		(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText("", null, delegate
		{
			this.RefreshCardPresetDropdowns(0, 0);
		}, new Action<string>(this.OnRenameCardPresetInput), Game.Language.Get("请输入预设名称", ""));
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x00078573 File Offset: 0x00076773
	private void OnRenameCardPresetInput(string presetName)
	{
		if (this.currentSelectedCardPresetIndex > 0 && SaveLoadManager.RenameEquipCardPreset(this.currentSelectedCardPresetIndex, presetName))
		{
			this.RefreshCardPresetDropdowns(this.currentSelectedCardPresetIndex, this.currentSelectedCardPresetIndex);
		}
	}

	// Token: 0x06001379 RID: 4985 RVA: 0x0007859E File Offset: 0x0007679E
	private void DeleteCurrentCardPreset()
	{
		if (this.currentSelectedCardPresetIndex <= 0 || !SaveLoadManager.DeleteEquipCardPreset(this.currentSelectedCardPresetIndex))
		{
			this.RefreshCardPresetDropdowns(0, 0);
			return;
		}
		this.currentSelectedCardPresetIndex = 0;
		SaveLoadManager.ClearEquipCards(true);
		this.RefreshCardPresetDropdowns(0, 0);
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x000785D4 File Offset: 0x000767D4
	public void UpdateCardView()
	{
		this.selfView.pool_cangku.RemoveAllView();
		Dictionary<int, CardManager.HaveCardData> haveCardDataDic = SaveLoadManager.haveCardDataDic;
		Dictionary<int, CardData>.ValueCollection values = Game.GameData.CardDataDic.Values;
		this.cardListSort.Clear();
		if (this.cardSortType == UI_MyCard.CardSortType.QualityAdd)
		{
			this.cardListSort.Add("0", new List<CardData>());
			this.cardListSort.Add("1", new List<CardData>());
			this.cardListSort.Add("2", new List<CardData>());
			this.cardListSort.Add("3", new List<CardData>());
			this.cardListSort.Add("4", new List<CardData>());
		}
		else if (this.cardSortType == UI_MyCard.CardSortType.QualityRed)
		{
			this.cardListSort.Add("4", new List<CardData>());
			this.cardListSort.Add("3", new List<CardData>());
			this.cardListSort.Add("2", new List<CardData>());
			this.cardListSort.Add("1", new List<CardData>());
			this.cardListSort.Add("0", new List<CardData>());
		}
		else if (this.cardSortType == UI_MyCard.CardSortType.Quality_0)
		{
			this.cardListSort.Add("0", new List<CardData>());
		}
		else if (this.cardSortType == UI_MyCard.CardSortType.Quality_1)
		{
			this.cardListSort.Add("1", new List<CardData>());
		}
		else if (this.cardSortType == UI_MyCard.CardSortType.Quality_2)
		{
			this.cardListSort.Add("2", new List<CardData>());
		}
		else if (this.cardSortType == UI_MyCard.CardSortType.Quality_3)
		{
			this.cardListSort.Add("3", new List<CardData>());
		}
		else if (this.cardSortType == UI_MyCard.CardSortType.Quality_4)
		{
			this.cardListSort.Add("4", new List<CardData>());
		}
		foreach (CardData cardData in values)
		{
			CardManager.HaveCardData haveCardData2;
			if (this.allDropType == UI_MyCard.AllDropType.Have)
			{
				CardManager.HaveCardData haveCardData;
				if (!haveCardDataDic.TryGetValue(cardData.id, out haveCardData))
				{
					continue;
				}
				if (haveCardData.haveNum == 0 && SaveLoadManager.gameSaveData.equipCards.IndexOf(cardData.id) == -1)
				{
					continue;
				}
			}
			else if (this.allDropType == UI_MyCard.AllDropType.NotHave && haveCardDataDic.TryGetValue(cardData.id, out haveCardData2) && (haveCardData2.haveNum > 0 || SaveLoadManager.gameSaveData.equipCards.IndexOf(cardData.id) != -1))
			{
				continue;
			}
			if (this.cardSortType == UI_MyCard.CardSortType.DropType)
			{
				if (!this.cardListSort.ContainsKey(cardData.unlockData))
				{
					this.cardListSort.Add(cardData.unlockData, new List<CardData>());
				}
				this.cardListSort[cardData.unlockData].Add(cardData);
			}
			else if (this.cardSortType == UI_MyCard.CardSortType.QualityAdd || this.cardSortType == UI_MyCard.CardSortType.QualityRed || this.cardSortType == UI_MyCard.CardSortType.Quality_0 || this.cardSortType == UI_MyCard.CardSortType.Quality_1 || this.cardSortType == UI_MyCard.CardSortType.Quality_2 || this.cardSortType == UI_MyCard.CardSortType.Quality_3 || this.cardSortType == UI_MyCard.CardSortType.Quality_4)
			{
				string key = cardData.quality.ToString();
				if (this.cardListSort.ContainsKey(key))
				{
					this.cardListSort[key].Add(cardData);
				}
			}
			else if (this.cardSortType == UI_MyCard.CardSortType.Time)
			{
				string key2 = "Default";
				if (!this.cardListSort.ContainsKey(key2))
				{
					this.cardListSort.Add(key2, new List<CardData>());
				}
				this.cardListSort[key2].Add(cardData);
			}
		}
		foreach (List<CardData> list in this.cardListSort.Values)
		{
			using (List<CardData>.Enumerator enumerator3 = list.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					CardData one = enumerator3.Current;
					GameObject go = this.selfView.pool_cangku.AddView();
					this.SetCard(go, one, false);
					go.GetComponent<Button>().AddButtonEvent(delegate
					{
						if (GameHelperClient.InDungeon)
						{
							Util.ShowTips("不能在地下城使用");
							return;
						}
						if (this.cardMakeState != UI_MyCard.CardMakeState.Normal)
						{
							this.SelectMakeCard(one);
							return;
						}
						this.AddEquipCard(one, go);
					});
				}
			}
		}
	}

	// Token: 0x040011BB RID: 4539
	private const string SaveCardPresetCaption = "预设操作";

	// Token: 0x040011BC RID: 4540
	private const string MakeCardCaption = "制作";

	// Token: 0x040011BD RID: 4541
	private const float CardPresetDropdownItemHeight = 55f;

	// Token: 0x040011BE RID: 4542
	private const float CardPresetDropdownMaxHeight = 480f;

	// Token: 0x040011BF RID: 4543
	private const float MakeCardHoldStartDelay = 0.35f;

	// Token: 0x040011C0 RID: 4544
	private const float MakeCardHoldRepeatInterval = 0.08f;

	// Token: 0x040011C1 RID: 4545
	private static readonly int[] CardDecomposeDustRewards = new int[]
	{
		5,
		20,
		80,
		320,
		640
	};

	// Token: 0x040011C2 RID: 4546
	private static readonly int[] CardComposeDustCosts = new int[]
	{
		40,
		100,
		400,
		1600,
		3200
	};

	// Token: 0x040011C3 RID: 4547
	private static readonly FieldInfo DropdownValueField = typeof(Dropdown).GetField("m_Value", BindingFlags.Instance | BindingFlags.NonPublic);

	// Token: 0x040011C4 RID: 4548
	public UI_MyCard_View selfView;

	// Token: 0x040011C5 RID: 4549
	private Action onMakeCardDecomposeCallback;

	// Token: 0x040011C6 RID: 4550
	private Action onMakeCardComposeCallback;

	// Token: 0x040011C7 RID: 4551
	private Action onMakeCardCancelCallback;

	// Token: 0x040011C8 RID: 4552
	private CardView equipInfoView;

	// Token: 0x040011C9 RID: 4553
	private RectTransform tipInfoRect;

	// Token: 0x040011CA RID: 4554
	private Dropdown allDropdown;

	// Token: 0x040011CB RID: 4555
	private Dropdown sortDropdown;

	// Token: 0x040011CC RID: 4556
	private Dropdown saveCardPresetDropdown;

	// Token: 0x040011CD RID: 4557
	private Dropdown loadCardPresetDropdown;

	// Token: 0x040011CE RID: 4558
	private Dropdown makeCardDropdown;

	// Token: 0x040011CF RID: 4559
	private TMP_InputField makeCardNumInputField;

	// Token: 0x040011D0 RID: 4560
	private CardView makeCardInfoView;

	// Token: 0x040011D1 RID: 4561
	private Text makeCardButtonText;

	// Token: 0x040011D2 RID: 4562
	private UI_MyCard.CardMakeState cardMakeState;

	// Token: 0x040011D3 RID: 4563
	private CardData selectedMakeCardData;

	// Token: 0x040011D4 RID: 4564
	private bool hasSelectedMakeCardData;

	// Token: 0x040011D5 RID: 4565
	private int holdMakeCardNumStep;

	// Token: 0x040011D6 RID: 4566
	private float nextMakeCardNumHoldTime;

	// Token: 0x040011D7 RID: 4567
	private bool isHoldingMakeCardNum;

	// Token: 0x040011D8 RID: 4568
	private readonly List<int> loadCardPresetIndexes = new List<int>();

	// Token: 0x040011D9 RID: 4569
	private readonly List<UI_MyCard.CardPresetOperationType> saveCardPresetOperations = new List<UI_MyCard.CardPresetOperationType>();

	// Token: 0x040011DA RID: 4570
	private int currentSelectedCardPresetIndex;

	// Token: 0x040011DB RID: 4571
	private bool isRefreshingCardPresetDropdown;

	// Token: 0x040011DC RID: 4572
	private UI_MyCard.AllDropType allDropType;

	// Token: 0x040011DD RID: 4573
	private UI_MyCard.CardSortType cardSortType;

	// Token: 0x040011DE RID: 4574
	private Dictionary<string, List<CardData>> cardListSort = new Dictionary<string, List<CardData>>();

	// Token: 0x040011DF RID: 4575
	private readonly int[] UpLevelNeed = new int[]
	{
		100,
		500,
		800,
		1200,
		2000,
		3000,
		3500,
		4000,
		4500,
		5000,
		5000,
		5500,
		5500,
		6000,
		6000,
		7000,
		7500,
		8000,
		8500,
		9000,
		9500,
		10000,
		10500,
		11000,
		11500,
		12500,
		13500,
		14500,
		16000,
		18000,
		20000,
		22000,
		25000,
		28000,
		32000,
		36000,
		40000,
		45000,
		50000,
		60000
	};

	// Token: 0x0200034B RID: 843
	private enum AllDropType
	{
		// Token: 0x040011E1 RID: 4577
		All,
		// Token: 0x040011E2 RID: 4578
		Have,
		// Token: 0x040011E3 RID: 4579
		NotHave
	}

	// Token: 0x0200034C RID: 844
	private enum CardSortType
	{
		// Token: 0x040011E5 RID: 4581
		DropType,
		// Token: 0x040011E6 RID: 4582
		Time,
		// Token: 0x040011E7 RID: 4583
		QualityAdd,
		// Token: 0x040011E8 RID: 4584
		QualityRed,
		// Token: 0x040011E9 RID: 4585
		Quality_0,
		// Token: 0x040011EA RID: 4586
		Quality_1,
		// Token: 0x040011EB RID: 4587
		Quality_2,
		// Token: 0x040011EC RID: 4588
		Quality_3,
		// Token: 0x040011ED RID: 4589
		Quality_4
	}

	// Token: 0x0200034D RID: 845
	private enum CardPresetOperationType
	{
		// Token: 0x040011EF RID: 4591
		SaveAsNew,
		// Token: 0x040011F0 RID: 4592
		SaveCurrent,
		// Token: 0x040011F1 RID: 4593
		RenameCurrent,
		// Token: 0x040011F2 RID: 4594
		DeleteCurrent
	}

	// Token: 0x0200034E RID: 846
	private enum MakeCardOperationType
	{
		// Token: 0x040011F4 RID: 4596
		Decompose,
		// Token: 0x040011F5 RID: 4597
		Compose,
		// Token: 0x040011F6 RID: 4598
		Cancel
	}

	// Token: 0x0200034F RID: 847
	private enum CardMakeState
	{
		// Token: 0x040011F8 RID: 4600
		Normal,
		// Token: 0x040011F9 RID: 4601
		Decompose,
		// Token: 0x040011FA RID: 4602
		Compose
	}
}
