using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000383 RID: 899
public class UI_Roguelike : UGUICtrl
{
	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x06001483 RID: 5251 RVA: 0x0007F661 File Offset: 0x0007D861
	// (set) Token: 0x06001484 RID: 5252 RVA: 0x0007F669 File Offset: 0x0007D869
	public Action CloseAction
	{
		get
		{
			return this.closeAction;
		}
		set
		{
			this.closeAction = value;
		}
	}

	// Token: 0x06001485 RID: 5253 RVA: 0x0007F674 File Offset: 0x0007D874
	public UI_Roguelike()
	{
		this.selfView = new UI_Roguelike_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_roguelike", base.GetType());
		for (int i = 0; i < 3; i++)
		{
			Roguelike_Item component = this.selfView.pool_roguelike.GetComponent<PoolView>().AddView().GetComponent<Roguelike_Item>();
			this.uiRoguelikeList.Add(component);
		}
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x0007F6F3 File Offset: 0x0007D8F3
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		MySystemEvent.Instance.RegisterMessage<Vector3>(39, new Action<Body, Vector3>(this.OnShowRankHeroTip));
		MySystemEvent.Instance.RegisterMessage(40, new Action<Body>(this.OnHideRankHeroTip));
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x0007F72C File Offset: 0x0007D92C
	private void OnShowRankHeroTip(Body body, Vector3 pos)
	{
		this.selfView.trans_heroTip.gameObject.SetActive(true);
		this.selfView.trans_heroTip.position = pos;
		this.selfView.trans_heroTip.GetComponent<RectTransform>().anchoredPosition += new Vector2(120f, 0f);
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x0007F78F File Offset: 0x0007D98F
	private void OnHideRankHeroTip(Body body)
	{
		this.selfView.trans_heroTip.gameObject.SetActive(false);
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x0007F7A8 File Offset: 0x0007D9A8
	public void ShowRoguelike(RoguelikeUIData[] roguelikeData, Action<RoguelikeUIData> callback, string titleStr, UI_Roguelike.RefreshActionEvent onRefresh, Action argCloseAction = null, float delayShow = 0f, List<SaveLoadManager.TeamBuildData> playerKingDataList = null, string analyticsSource = null)
	{
		this.ClearSlotRefresh();
		this.currentAnalyticsSource = analyticsSource;
		this.ShowRoguelikeInternal(roguelikeData, callback, titleStr, onRefresh != null, delayShow, playerKingDataList, delegate(int i)
		{
			if (onRefresh == null)
			{
				return 0;
			}
			return GameHelperClient.RefreshNum;
		});
		this.closeAction = argCloseAction;
		this.onRefreshActionEvent = onRefresh;
		this.onIndexRefreshActionEvent = null;
	}

	// Token: 0x0600148B RID: 5259 RVA: 0x0007F810 File Offset: 0x0007DA10
	public void ShowRoguelikeWithSlotRefresh(RoguelikeUIData[] roguelikeData, Action<RoguelikeUIData> callback, string titleStr, UI_Roguelike.IndexRefreshActionEvent onRefresh, int[] refreshNums, Action argCloseAction = null, float delayShow = 0f, List<SaveLoadManager.TeamBuildData> playerKingDataList = null, string analyticsSource = null)
	{
		this.ClearSlotRefresh();
		this.currentAnalyticsSource = analyticsSource;
		bool flag = onRefresh != null && this.HasAnySlotRefresh(refreshNums, roguelikeData.Length);
		this.ShowRoguelikeInternal(roguelikeData, callback, titleStr, flag, delayShow, playerKingDataList, delegate(int i)
		{
			this.slotRefreshNums[i] = ((refreshNums != null && i < refreshNums.Length) ? Mathf.Max(0, refreshNums[i]) : 0);
			return this.slotRefreshNums[i];
		});
		this.closeAction = argCloseAction;
		this.onRefreshActionEvent = null;
		this.onIndexRefreshActionEvent = (flag ? onRefresh : null);
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x0007F890 File Offset: 0x0007DA90
	private void ShowRoguelikeInternal(RoguelikeUIData[] roguelikeData, Action<RoguelikeUIData> callback, string titleStr, bool canRefresh, float delayShow, List<SaveLoadManager.TeamBuildData> playerKingDataList, Func<int, int> getRefreshNum)
	{
		this.selfView.trans_heroTip.gameObject.SetActive(false);
		if (this.onRoguelikeClick != null)
		{
			Action<RoguelikeUIData> action = this.onRoguelikeClick;
			if (action != null)
			{
				action(this.uiRoguelikeList[Random.Range(0, this.currentRoguelikeCount)].RoguelikeData);
			}
		}
		MySystemEvent.Instance.DispatchMessage<bool>(31, canRefresh);
		EventSystem.current.SetSelectedGameObject(null);
		Game.UI.GetUI<UI_Shop>().CloseAnim(false, false);
		this.onRoguelikeClick = callback;
		this.currentRoguelikeCount = roguelikeData.Length;
		this.selfView.ltext_Title.text = titleStr;
		for (int i = 0; i < this.uiRoguelikeList.Count; i++)
		{
			Roguelike_Item roguelike_Item = this.uiRoguelikeList[i];
			bool flag = i < roguelikeData.Length;
			if (roguelike_Item.gameObject.activeSelf != flag)
			{
				roguelike_Item.gameObject.SetActive(flag);
			}
			if (flag)
			{
				if (playerKingDataList == null)
				{
					roguelike_Item.SetKingBattle(false);
					roguelike_Item.roguelikeKingHead.Hide();
				}
				else
				{
					roguelike_Item.SetKingBattle(true);
					roguelike_Item.SetKingData(playerKingDataList[i]);
					roguelike_Item.roguelikeKingHead.SetTeamBuildData(playerKingDataList[i]);
				}
				int refreshNum = (getRefreshNum != null) ? getRefreshNum(i) : 0;
				roguelike_Item.UpdateView(roguelikeData[i], i, canRefresh, refreshNum);
			}
		}
		AnalyticsManager analytics = Game.Analytics;
		if (analytics != null)
		{
			analytics.RecordRoguelikeShown(this.currentAnalyticsSource, roguelikeData);
		}
		this.canvasGroup.alpha = 0f;
		this.CancelDelayShowTimer();
		this.canvasGroup.DOKill(false);
		this.forceSelectableTime = Time.time + Mathf.Max(delayShow + 1f + 0.2f, 2.5f);
		if (Mathf.Approximately(delayShow, 0f))
		{
			this.StartFadeIn();
		}
		else
		{
			this.timer = Game.TimerManager.AddTimer(delayShow, new Action(this.StartFadeIn));
		}
		Game.UI.GetUI<UI_Battle>().OnOpenRoguelike();
	}

	// Token: 0x0600148D RID: 5261 RVA: 0x0007FA82 File Offset: 0x0007DC82
	public void OnClickRoguelike(RoguelikeUIData roguelikeData)
	{
		if (this.isSelecting)
		{
			return;
		}
		if (this.canvasGroup.alpha < 0.9f && Time.time < this.forceSelectableTime)
		{
			return;
		}
		this.OnSelectRoguelike(roguelikeData);
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x0007FAB4 File Offset: 0x0007DCB4
	public override void Update()
	{
		base.Update();
		if (!this.isOpen || this.isSelecting || this.canvasGroup.alpha >= 0.9f || Time.time < this.forceSelectableTime)
		{
			return;
		}
		this.CancelDelayShowTimer();
		this.canvasGroup.DOKill(false);
		this.canvasGroup.alpha = 1f;
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x0007FB1C File Offset: 0x0007DD1C
	private void OnSelectRoguelike(RoguelikeUIData roguelikeData)
	{
		if (this.isSelecting)
		{
			return;
		}
		this.isSelecting = true;
		Action<RoguelikeUIData> action = this.onRoguelikeClick;
		Action action2 = this.closeAction;
		this.onRoguelikeClick = null;
		this.closeAction = null;
		this.CancelDelayShowTimer();
		AnalyticsManager analytics = Game.Analytics;
		if (analytics != null)
		{
			analytics.RecordRoguelikeSelected(this.currentAnalyticsSource, roguelikeData);
		}
		try
		{
			if (action != null)
			{
				action(roguelikeData);
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		Game.UI.GetUI<UI_Battle>().OnCloseRoguelike();
		base.CloseSelfPanel();
		this.isSelecting = false;
		try
		{
			if (action2 != null)
			{
				action2();
			}
		}
		catch (Exception exception2)
		{
			Debug.LogException(exception2);
		}
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x0007FBD0 File Offset: 0x0007DDD0
	private void StartFadeIn()
	{
		this.timer = null;
		this.canvasGroup.DOKill(false);
		this.canvasGroup.DOFade(1f, 1f);
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x0007FBFC File Offset: 0x0007DDFC
	private void CancelDelayShowTimer()
	{
		if (this.timer == null)
		{
			return;
		}
		Game.TimerManager.CancelTimer(this.timer);
		this.timer = null;
	}

	// Token: 0x06001492 RID: 5266 RVA: 0x0007FC20 File Offset: 0x0007DE20
	public void OnRefreshBtn(int indexValue)
	{
		if (this.onIndexRefreshActionEvent != null)
		{
			if (indexValue < 0 || indexValue >= this.slotRefreshNums.Length || this.slotRefreshNums[indexValue] == 0)
			{
				return;
			}
			this.slotRefreshNums[indexValue]--;
			MySystemEvent.Instance.DispatchMessage(30);
			AnalyticsManager analytics = Game.Analytics;
			if (analytics != null)
			{
				analytics.RecordRoguelikeRefreshAway(this.currentAnalyticsSource, this.uiRoguelikeList[indexValue].RoguelikeData);
			}
			RoguelikeUIData roguelikeUIData = this.onIndexRefreshActionEvent(indexValue);
			this.uiRoguelikeList[indexValue].UpdateView(roguelikeUIData, indexValue, true, this.slotRefreshNums[indexValue]);
			AnalyticsManager analytics2 = Game.Analytics;
			if (analytics2 == null)
			{
				return;
			}
			analytics2.RecordRoguelikeShown(this.currentAnalyticsSource, new RoguelikeUIData[]
			{
				roguelikeUIData
			});
			return;
		}
		else
		{
			if (this.onRefreshActionEvent == null || GameHelperClient.RefreshNum == 0)
			{
				return;
			}
			GameHelperClient.AddRefreshNum(-1);
			MySystemEvent.Instance.DispatchMessage(30);
			AnalyticsManager analytics3 = Game.Analytics;
			if (analytics3 != null)
			{
				analytics3.RecordRoguelikeRefreshAway(this.currentAnalyticsSource, this.uiRoguelikeList[indexValue].RoguelikeData);
			}
			RoguelikeUIData roguelikeUIData2 = this.onRefreshActionEvent();
			for (int i = 0; i < this.uiRoguelikeList.Count; i++)
			{
				if (this.uiRoguelikeList[i].gameObject.activeSelf)
				{
					if (i == indexValue)
					{
						this.uiRoguelikeList[i].UpdateView(roguelikeUIData2, i, true, GameHelperClient.RefreshNum);
					}
					else
					{
						this.uiRoguelikeList[i].UpateRefreshNum(GameHelperClient.RefreshNum);
					}
				}
			}
			AnalyticsManager analytics4 = Game.Analytics;
			if (analytics4 == null)
			{
				return;
			}
			analytics4.RecordRoguelikeShown(this.currentAnalyticsSource, new RoguelikeUIData[]
			{
				roguelikeUIData2
			});
			return;
		}
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x0007FDC0 File Offset: 0x0007DFC0
	private void ClearSlotRefresh()
	{
		for (int i = 0; i < this.slotRefreshNums.Length; i++)
		{
			this.slotRefreshNums[i] = 0;
		}
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x0007FDEC File Offset: 0x0007DFEC
	private bool HasAnySlotRefresh(int[] refreshNums, int count)
	{
		if (refreshNums == null)
		{
			return false;
		}
		int num = Mathf.Min(count, refreshNums.Length);
		for (int i = 0; i < num; i++)
		{
			if (refreshNums[i] > 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x0007FE20 File Offset: 0x0007E020
	protected override void ClosePanel()
	{
		base.ClosePanel();
		this.canvasGroup.DOKill(false);
		this.CancelDelayShowTimer();
		this.isSelecting = false;
		this.ClearSlotRefresh();
		this.onRefreshActionEvent = null;
		this.onIndexRefreshActionEvent = null;
		this.currentAnalyticsSource = null;
		MySystemEvent.Instance.UnregisterMessage<Vector3>(39, new Action<Body, Vector3>(this.OnShowRankHeroTip));
		MySystemEvent.Instance.UnregisterMessage(40, new Action<Body>(this.OnHideRankHeroTip));
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x0007FE98 File Offset: 0x0007E098
	public void DevCloseRoguelike()
	{
		this.onRoguelikeClick = null;
		base.CloseSelfPanel();
	}

	// Token: 0x0400132A RID: 4906
	public UI_Roguelike_View selfView;

	// Token: 0x0400132B RID: 4907
	private readonly List<Roguelike_Item> uiRoguelikeList = new List<Roguelike_Item>();

	// Token: 0x0400132C RID: 4908
	private readonly int[] slotRefreshNums = new int[3];

	// Token: 0x0400132D RID: 4909
	private Action<RoguelikeUIData> onRoguelikeClick;

	// Token: 0x0400132E RID: 4910
	private Action closeAction;

	// Token: 0x0400132F RID: 4911
	private int currentRoguelikeCount;

	// Token: 0x04001330 RID: 4912
	private string currentAnalyticsSource;

	// Token: 0x04001331 RID: 4913
	private Timer timer;

	// Token: 0x04001332 RID: 4914
	private const float FadeInTime = 1f;

	// Token: 0x04001333 RID: 4915
	private const float SelectFallbackTime = 2.5f;

	// Token: 0x04001334 RID: 4916
	private float forceSelectableTime;

	// Token: 0x04001335 RID: 4917
	private bool isSelecting;

	// Token: 0x04001336 RID: 4918
	public UI_Roguelike.RefreshActionEvent onRefreshActionEvent;

	// Token: 0x04001337 RID: 4919
	public UI_Roguelike.IndexRefreshActionEvent onIndexRefreshActionEvent;

	// Token: 0x02000384 RID: 900
	// (Invoke) Token: 0x06001498 RID: 5272
	public delegate RoguelikeUIData RefreshActionEvent();

	// Token: 0x02000385 RID: 901
	// (Invoke) Token: 0x0600149C RID: 5276
	public delegate RoguelikeUIData IndexRefreshActionEvent(int indexValue);
}
