using System;
using System.Collections.Generic;
using Mirror;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000357 RID: 855
public class UI_MyRank : UGUICtrl
{
	// Token: 0x06001397 RID: 5015 RVA: 0x0007911A File Offset: 0x0007731A
	public UI_MyRank()
	{
		this.selfView = new UI_MyRank_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_myRank", base.GetType());
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x0007914C File Offset: 0x0007734C
	protected override void ButtonAddClick()
	{
		this.selfView.btn_back.AddButtonEvent(new UnityAction(base.CloseSelfPanel));
		this.selfView.btn_one.AddButtonEvent(new UnityAction(this.OnOneBtnClick));
		this.selfView.btn_two.AddButtonEvent(new UnityAction(this.OnTwoBtnClick));
		this.selfView.btn_thr.AddButtonEvent(new UnityAction(this.OnThreeBtnClick));
		this.selfView.btn_four.AddButtonEvent(new UnityAction(this.OnFourBtnClick));
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x000791E8 File Offset: 0x000773E8
	private void OnOneBtnClick()
	{
		if (this.rankType == UI_MyRank.RankType.SOLO)
		{
			return;
		}
		this.rankType = UI_MyRank.RankType.SOLO;
		this.StartUpdateRankView();
		this.selfView.btn_one.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn_select");
		this.selfView.btn_two.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn");
		this.selfView.btn_thr.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn");
		this.selfView.btn_four.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn");
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x00079288 File Offset: 0x00077488
	private void OnTwoBtnClick()
	{
		if (this.rankType == UI_MyRank.RankType.DUO)
		{
			return;
		}
		this.rankType = UI_MyRank.RankType.DUO;
		this.StartUpdateRankView();
		this.selfView.btn_one.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn");
		this.selfView.btn_two.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn_select");
		this.selfView.btn_thr.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn");
		this.selfView.btn_four.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn");
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x00079328 File Offset: 0x00077528
	private void OnThreeBtnClick()
	{
		if (this.rankType == UI_MyRank.RankType.TRIO)
		{
			return;
		}
		this.rankType = UI_MyRank.RankType.TRIO;
		this.StartUpdateRankView();
		this.selfView.btn_one.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn");
		this.selfView.btn_two.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn");
		this.selfView.btn_thr.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn_select");
		this.selfView.btn_four.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn");
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x000793C8 File Offset: 0x000775C8
	private void OnFourBtnClick()
	{
		if (this.rankType == UI_MyRank.RankType.SQUAD)
		{
			return;
		}
		this.rankType = UI_MyRank.RankType.SQUAD;
		this.StartUpdateRankView();
		this.selfView.btn_one.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn");
		this.selfView.btn_two.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn");
		this.selfView.btn_thr.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn");
		this.selfView.btn_four.GetComponent<Image>().sprite = Resources.Load<Sprite>("Bundles/UI/RankUI/rank_btn_select");
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x0007151B File Offset: 0x0006F71B
	public override void Update()
	{
		base.Update();
		if (Input.GetKeyDown(KeyCode.Escape) && this.isOpen)
		{
			base.CloseSelfPanel();
		}
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x00079468 File Offset: 0x00077668
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		MyRankTestRangeData myRankTestRangeData = data as MyRankTestRangeData;
		if (myRankTestRangeData != null)
		{
			this.StartUpdateTestRankRange(myRankTestRangeData);
		}
		else
		{
			this.StartUpdateRankView();
		}
		MySystemEvent.Instance.RegisterMessage<Vector3>(39, new Action<Body, Vector3>(this.OnShowRankHeroTip));
		MySystemEvent.Instance.RegisterMessage(40, new Action<Body>(this.OnHideRankHeroTip));
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x000794C5 File Offset: 0x000776C5
	protected override void ClosePanel()
	{
		base.ClosePanel();
		MySystemEvent.Instance.UnregisterMessage<Vector3>(39, new Action<Body, Vector3>(this.OnShowRankHeroTip));
		MySystemEvent.Instance.UnregisterMessage(40, new Action<Body>(this.OnHideRankHeroTip));
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x00079500 File Offset: 0x00077700
	private void OnShowRankHeroTip(Body body, Vector3 pos)
	{
		this.selfView.trans_heroTip.gameObject.SetActive(true);
		this.selfView.trans_heroTip.position = pos;
		this.selfView.trans_heroTip.GetComponent<RectTransform>().anchoredPosition += new Vector2(120f, 0f);
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x00079563 File Offset: 0x00077763
	private void OnHideRankHeroTip(Body body)
	{
		this.selfView.trans_heroTip.gameObject.SetActive(false);
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x0007957C File Offset: 0x0007777C
	public void StartUpdateRankView()
	{
		this.selfView.trans_heroTip.gameObject.SetActive(false);
		this.selfView.pool_rank.RemoveAllView();
		List<SaveLoadManager.TeamBuildData> curListData = this.GetCurListData();
		if (curListData == null || curListData.Count == 0)
		{
			int requestedRankType = (int)this.rankType;
			(NetworkManager.singleton as MyServerNetworkManager).MySteamLeaderboardBatchLoader.GetTopPlayersData(SteamLeaderboardManager.GetLEADERBOARDName(requestedRankType), 100, delegate(List<SaveLoadManager.TeamBuildData> datas)
			{
				this.OnLoadRankOver(requestedRankType, datas);
			});
			this.selfView.trans_load.gameObject.SetActive(true);
			return;
		}
		this.UpdateView(curListData, 1);
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x00079628 File Offset: 0x00077828
	public void StartUpdateTestRankRange(MyRankTestRangeData testRangeData)
	{
		this.selfView.trans_heroTip.gameObject.SetActive(false);
		this.selfView.pool_rank.RemoveAllView();
		this.selfView.trans_load.gameObject.SetActive(true);
		this.rankType = (UI_MyRank.RankType)Mathf.Clamp(testRangeData.PlayerCount, 1, 4);
		int playerNum = (int)this.rankType;
		int startRank = Mathf.Max(1, testRangeData.StartRank);
		int endRank = Mathf.Max(startRank, testRangeData.EndRank);
		(NetworkManager.singleton as MyServerNetworkManager).MySteamLeaderboardBatchLoader.GetPlayersDataByRankRange(SteamLeaderboardManager.GetLEADERBOARDName(playerNum), startRank, endRank, delegate(List<SaveLoadManager.TeamBuildData> datas)
		{
			this.OnLoadTestRankRangeOver(datas, startRank);
		});
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x000796E9 File Offset: 0x000778E9
	private void OnLoadTestRankRangeOver(List<SaveLoadManager.TeamBuildData> datas, int startRank)
	{
		this.selfView.trans_load.gameObject.SetActive(false);
		if (datas == null || datas.Count == 0)
		{
			return;
		}
		this.UpdateView(datas, startRank);
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x00079718 File Offset: 0x00077918
	private void UpdateView(List<SaveLoadManager.TeamBuildData> teamBuildDataList, int startRank = 1)
	{
		this.selfView.trans_load.gameObject.SetActive(false);
		int count = teamBuildDataList.Count;
		for (int i = 0; i < count; i++)
		{
			SaveLoadManager.TeamBuildData data = teamBuildDataList[i];
			if (SteamLeaderboardRankOrder.HasCompleteBuildData(data))
			{
				this.selfView.pool_rank.AddView().GetComponent<UIRankItem>().SetData(data, startRank + i);
			}
		}
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x0007977C File Offset: 0x0007797C
	private void OnLoadRankOver(int requestedRankType, List<SaveLoadManager.TeamBuildData> datas)
	{
		if (datas == null || datas.Count == 0)
		{
			if (this.rankType == (UI_MyRank.RankType)requestedRankType)
			{
				this.selfView.trans_load.gameObject.SetActive(false);
			}
			return;
		}
		int count = datas.Count;
		int num = -1;
		ulong steamID = SteamUser.GetSteamID().m_SteamID;
		for (int i = 0; i < count; i++)
		{
			SaveLoadManager.TeamBuildData teamBuildData = datas[i];
			if (SteamLeaderboardRankOrder.HasCompleteBuildData(teamBuildData) && teamBuildData.members[0].steamID == steamID)
			{
				num = i + 1;
			}
		}
		if (requestedRankType == 1)
		{
			this.teamBuildDataList_1 = datas;
			if (this.rankType == UI_MyRank.RankType.SOLO)
			{
				this.UpdateView(this.teamBuildDataList_1, 1);
			}
			if (num != -1)
			{
				if (num > 3)
				{
					this.selfView.text_rankOne.enabled = true;
					this.selfView.img_rankOne.gameObject.SetActive(false);
					this.selfView.text_rankOne.text = num.ToString();
					return;
				}
				this.selfView.text_rankOne.enabled = false;
				this.selfView.img_rankOne.gameObject.SetActive(true);
				this.selfView.img_rankOne.sprite = Resources.Load<Sprite>(PathDefine.Concat("Bundles/UI/RankUI/rank_list", num));
				return;
			}
		}
		else if (requestedRankType == 2)
		{
			this.teamBuildDataList_2 = datas;
			if (this.rankType == UI_MyRank.RankType.DUO)
			{
				this.UpdateView(this.teamBuildDataList_2, 1);
			}
			if (num != -1)
			{
				if (num > 3)
				{
					this.selfView.text_rankTwo.enabled = true;
					this.selfView.img_rankTwo.gameObject.SetActive(false);
					this.selfView.text_rankTwo.text = num.ToString();
					return;
				}
				this.selfView.text_rankTwo.enabled = false;
				this.selfView.img_rankTwo.gameObject.SetActive(true);
				this.selfView.img_rankTwo.sprite = Resources.Load<Sprite>(PathDefine.Concat("Bundles/UI/RankUI/rank_list", num));
				return;
			}
		}
		else if (requestedRankType == 3)
		{
			this.teamBuildDataList_3 = datas;
			if (this.rankType == UI_MyRank.RankType.TRIO)
			{
				this.UpdateView(this.teamBuildDataList_3, 1);
			}
			if (num != -1)
			{
				if (num > 3)
				{
					this.selfView.text_rankThree.enabled = true;
					this.selfView.img_rankThree.gameObject.SetActive(false);
					this.selfView.text_rankThree.text = num.ToString();
					return;
				}
				this.selfView.text_rankThree.enabled = false;
				this.selfView.img_rankThree.gameObject.SetActive(true);
				this.selfView.img_rankThree.sprite = Resources.Load<Sprite>(PathDefine.Concat("Bundles/UI/RankUI/rank_list", num));
				return;
			}
		}
		else if (requestedRankType == 4)
		{
			this.teamBuildDataList_4 = datas;
			if (this.rankType == UI_MyRank.RankType.SQUAD)
			{
				this.UpdateView(this.teamBuildDataList_4, 1);
			}
			if (num != -1)
			{
				if (num > 3)
				{
					this.selfView.text_rankFour.enabled = true;
					this.selfView.img_rankFour.gameObject.SetActive(false);
					this.selfView.text_rankFour.text = num.ToString();
					return;
				}
				this.selfView.text_rankFour.enabled = false;
				this.selfView.img_rankFour.gameObject.SetActive(true);
				this.selfView.img_rankFour.sprite = Resources.Load<Sprite>(PathDefine.Concat("Bundles/UI/RankUI/rank_list", num));
			}
		}
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x00079AF4 File Offset: 0x00077CF4
	private List<SaveLoadManager.TeamBuildData> GetCurListData()
	{
		if (this.rankType == UI_MyRank.RankType.SOLO)
		{
			return this.teamBuildDataList_1;
		}
		if (this.rankType == UI_MyRank.RankType.DUO)
		{
			return this.teamBuildDataList_2;
		}
		if (this.rankType == UI_MyRank.RankType.TRIO)
		{
			return this.teamBuildDataList_3;
		}
		if (this.rankType == UI_MyRank.RankType.SQUAD)
		{
			return this.teamBuildDataList_4;
		}
		return null;
	}

	// Token: 0x0400122B RID: 4651
	public UI_MyRank_View selfView;

	// Token: 0x0400122C RID: 4652
	private UI_MyRank.RankType rankType = UI_MyRank.RankType.SOLO;

	// Token: 0x0400122D RID: 4653
	private List<SaveLoadManager.TeamBuildData> teamBuildDataList_1;

	// Token: 0x0400122E RID: 4654
	private List<SaveLoadManager.TeamBuildData> teamBuildDataList_2;

	// Token: 0x0400122F RID: 4655
	private List<SaveLoadManager.TeamBuildData> teamBuildDataList_3;

	// Token: 0x04001230 RID: 4656
	private List<SaveLoadManager.TeamBuildData> teamBuildDataList_4;

	// Token: 0x02000358 RID: 856
	private enum RankType
	{
		// Token: 0x04001232 RID: 4658
		SOLO = 1,
		// Token: 0x04001233 RID: 4659
		DUO,
		// Token: 0x04001234 RID: 4660
		TRIO,
		// Token: 0x04001235 RID: 4661
		SQUAD
	}
}
