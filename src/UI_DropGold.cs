using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000319 RID: 793
public class UI_DropGold : UGUICtrl
{
	// Token: 0x06001249 RID: 4681 RVA: 0x0006D540 File Offset: 0x0006B740
	public UI_DropGold()
	{
		this.selfView = new UI_DropGold_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_dropGold", base.GetType());
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x0006D5A0 File Offset: 0x0006B7A0
	protected override void OnRegisterEvent()
	{
		this.goldInputField = this.selfView.trans_inputGold.GetComponent<TMP_InputField>();
		this.gemInputField = this.selfView.trans_inputGem.GetComponent<TMP_InputField>();
		this.InitInputField(this.goldInputField);
		this.InitInputField(this.gemInputField);
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x0006D5F4 File Offset: 0x0006B7F4
	protected override void ButtonAddClick()
	{
		this.selfView.btn_close.AddButtonEvent(new UnityAction(this.OnCloseBtnClick));
		this.selfView.btn_drop.AddButtonEvent(new UnityAction(this.OnDropBtnClick));
		this.AddHoldButtonEvent(this.selfView.btn_goldAdd, this.goldInputField, this.goldStep);
		this.AddHoldButtonEvent(this.selfView.btn_goldRed, this.goldInputField, -this.goldStep);
		this.AddHoldButtonEvent(this.selfView.btn_gemAdd, this.gemInputField, this.gemStep);
		this.AddHoldButtonEvent(this.selfView.btn_gemRed, this.gemInputField, -this.gemStep);
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x0006D6B0 File Offset: 0x0006B8B0
	public override void Update()
	{
		base.Update();
		if (!this.isOpen || !this.isHolding || this.holdInputField == null || Time.unscaledTime < this.nextHoldTime)
		{
			return;
		}
		this.ChangeInputValue(this.holdInputField, this.holdStep);
		this.nextHoldTime = Time.unscaledTime + this.holdRepeatInterval;
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x0006D714 File Offset: 0x0006B914
	private void InitInputField(TMP_InputField inputField)
	{
		if (inputField == null)
		{
			return;
		}
		inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
		inputField.text = "0";
		inputField.onEndEdit.AddListener(delegate(string _)
		{
			this.ClampInputValue(inputField);
		});
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x0006D77C File Offset: 0x0006B97C
	private void AddHoldButtonEvent(Button button, TMP_InputField inputField, int step)
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
		this.AddTriggerEvent(eventTrigger, EventTriggerType.PointerDown, delegate(BaseEventData _)
		{
			this.StartHold(inputField, step);
		});
		this.AddTriggerEvent(eventTrigger, EventTriggerType.PointerUp, delegate(BaseEventData _)
		{
			this.StopHold();
		});
		this.AddTriggerEvent(eventTrigger, EventTriggerType.PointerExit, delegate(BaseEventData _)
		{
			this.StopHold();
		});
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x0006D808 File Offset: 0x0006BA08
	private void AddTriggerEvent(EventTrigger trigger, EventTriggerType eventType, UnityAction<BaseEventData> action)
	{
		EventTrigger.Entry entry = new EventTrigger.Entry
		{
			eventID = eventType
		};
		entry.callback.AddListener(action);
		trigger.triggers.Add(entry);
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x0006D83C File Offset: 0x0006BA3C
	private void StartHold(TMP_InputField inputField, int step)
	{
		Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		this.ChangeInputValue(inputField, step);
		this.holdInputField = inputField;
		this.holdStep = step;
		this.nextHoldTime = Time.unscaledTime + this.holdStartDelay;
		this.isHolding = true;
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x0006D892 File Offset: 0x0006BA92
	private void StopHold()
	{
		this.isHolding = false;
		this.holdInputField = null;
		this.holdStep = 0;
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x0006D8AC File Offset: 0x0006BAAC
	private void ChangeInputValue(TMP_InputField inputField, int step)
	{
		if (inputField == null)
		{
			return;
		}
		long value = (long)this.GetInputValue(inputField) + (long)step;
		this.SetInputValue(inputField, this.ClampValue(value));
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x0006D8DD File Offset: 0x0006BADD
	private void ClampInputValue(TMP_InputField inputField)
	{
		this.SetInputValue(inputField, this.ClampValue((long)this.GetInputValue(inputField)));
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x0006D8F4 File Offset: 0x0006BAF4
	private int GetInputValue(TMP_InputField inputField)
	{
		if (inputField == null || string.IsNullOrEmpty(inputField.text))
		{
			return 0;
		}
		long value;
		if (!long.TryParse(inputField.text, out value))
		{
			return 0;
		}
		return this.ClampValue(value);
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x0006D931 File Offset: 0x0006BB31
	private int ClampValue(long value)
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

	// Token: 0x06001256 RID: 4694 RVA: 0x0006D94C File Offset: 0x0006BB4C
	private void SetInputValue(TMP_InputField inputField, int value)
	{
		if (inputField == null)
		{
			return;
		}
		inputField.SetTextWithoutNotify(Mathf.Max(0, value).ToString());
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x0006D978 File Offset: 0x0006BB78
	private void OnDropBtnClick()
	{
		PlayerBase localPlayer = GameHelperClient.localPlayer;
		if (localPlayer == null)
		{
			return;
		}
		int num = Mathf.Min(this.GetInputValue(this.goldInputField), localPlayer.gold);
		int num2 = Mathf.Min(this.GetInputValue(this.gemInputField), localPlayer.gem);
		if (num > 0)
		{
			localPlayer.DropGold(num);
		}
		if (num2 > 0)
		{
			localPlayer.DropGem(num2);
		}
		this.SetInputValue(this.goldInputField, 0);
		this.SetInputValue(this.gemInputField, 0);
		this.StopHold();
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x0006D9FB File Offset: 0x0006BBFB
	private void OnCloseBtnClick()
	{
		base.CloseSelfPanel();
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x0006DA04 File Offset: 0x0006BC04
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/丢弃物品", 1f, 3f);
		this.SetInputValue(this.goldInputField, 0);
		this.SetInputValue(this.gemInputField, 0);
		this.StopHold();
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x0006DA52 File Offset: 0x0006BC52
	protected override void ClosePanel()
	{
		base.ClosePanel();
		this.StopHold();
	}

	// Token: 0x0400108D RID: 4237
	public UI_DropGold_View selfView;

	// Token: 0x0400108E RID: 4238
	public int goldStep = 500;

	// Token: 0x0400108F RID: 4239
	public int gemStep = 1;

	// Token: 0x04001090 RID: 4240
	public float holdStartDelay = 0.35f;

	// Token: 0x04001091 RID: 4241
	public float holdRepeatInterval = 0.08f;

	// Token: 0x04001092 RID: 4242
	private TMP_InputField goldInputField;

	// Token: 0x04001093 RID: 4243
	private TMP_InputField gemInputField;

	// Token: 0x04001094 RID: 4244
	private TMP_InputField holdInputField;

	// Token: 0x04001095 RID: 4245
	private int holdStep;

	// Token: 0x04001096 RID: 4246
	private float nextHoldTime;

	// Token: 0x04001097 RID: 4247
	private bool isHolding;
}
