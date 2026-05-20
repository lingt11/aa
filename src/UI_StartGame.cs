using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Mirror;
using Mirror.Discovery;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003B6 RID: 950
public class UI_StartGame : UGUICtrl
{
	// Token: 0x060015AC RID: 5548 RVA: 0x00086C8B File Offset: 0x00084E8B
	public UI_StartGame()
	{
		this.selfView = new UI_StartGame_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_startGame", base.GetType());
	}

	// Token: 0x060015AD RID: 5549 RVA: 0x00086CB8 File Offset: 0x00084EB8
	protected override void ButtonAddClick()
	{
		this.selfView.btn_bendi.AddButtonEvent(delegate
		{
			this.BenDiGroup();
		});
		this.selfView.btn_selectHero.AddButtonEvent(delegate
		{
			this.selfView.btn_selectHero.gameObject.SetActive(false);
			this.selfView.trans_zhangjie.gameObject.SetActive(true);
			GameHelperClient.LockLobbyOnGameStart();
		});
		this.selfView.btn_set.AddButtonEvent(delegate
		{
			Game.UI.OpenUI<UI_SelectLanguage>(null);
		});
		this.selfView.btn_guide.AddButtonEvent(delegate
		{
			Game.UI.OpenUI<UI_IllustratedGuide>(null);
		});
		this.selfView.btn_quit.AddButtonEvent(delegate
		{
			Game.QuitApplication();
		});
		this.selfView.btn_z1.AddButtonEvent(delegate
		{
			Game.MainLogic.GameStart(true, 1);
		});
		this.selfView.btn_z2.AddButtonEvent(delegate
		{
			if (!this.GetCanInLevel())
			{
				Util.ShowTipsNoLanguage(Game.Language.Get("需要通关", "") + Game.Language.Get("第1层", "") + "!");
				return;
			}
			Game.MainLogic.GameStart(true, 2);
		});
		this.selfView.btn_z3.AddButtonEvent(delegate
		{
			Util.ShowTips(Game.Language.Get("暂未开放", ""));
		});
		this.selfView.btn_refresh.AddButtonEvent(delegate
		{
			EntityStatic.Get<SteamLobby>().SearchLobbies();
		});
		this.selfView.btn_rank.AddButtonEvent(delegate
		{
			Game.UI.OpenUI<UI_MyRank>(null);
		});
		this.selfView.btn_backMenu.AddButtonEvent(new UnityAction(this.OnQuitBtnClick));
		this.selfView.btn_back1.AddButtonEvent(delegate
		{
			this.MainGroup();
		});
		this.selfView.btn_host.AddButtonEvent(delegate
		{
			NetworkManager.singleton.serverTickRate = 60;
			((MyServerNetworkManager)NetworkManager.singleton).SetTransport(1);
			NetworkManager.singleton.StartHost();
			((MyServerNetworkManager)NetworkManager.singleton).DiscoveredServers.Clear();
			((MyServerNetworkManager)NetworkManager.singleton).NetworkDiscovery.AdvertiseServer();
		});
		this.selfView.btn_join.AddButtonEvent(delegate
		{
			((MyServerNetworkManager)NetworkManager.singleton).SetTransport(1);
			((MyServerNetworkManager)NetworkManager.singleton).DiscoveredServers.Clear();
			((MyServerNetworkManager)NetworkManager.singleton).NetworkDiscovery.StartDiscovery();
			this.selfView.trans_serverList.gameObject.SetActive(true);
			this.CreateRoomSuccess();
			this.selfView.trans_wait.gameObject.SetActive(false);
			this.selfView.btn_refresh.gameObject.SetActive(false);
		});
		this.selfView.btn_steam.AddButtonEvent(delegate
		{
			this.SteamGroup();
			if (EntityStatic.Get<SteamLobby>() == null)
			{
				EntityStatic.AddComp<SteamLobby>();
			}
			((MyServerNetworkManager)NetworkManager.singleton).SetTransport(2);
		});
		this.selfView.btn_hostSteam.AddButtonEvent(delegate
		{
			this.selfView.trans_createRoom.gameObject.SetActive(true);
			this.selfView.trans_inputMessage.GetComponent<InputField>().text = "";
		});
		this.selfView.btn_joinSteam.AddButtonEvent(delegate
		{
			EntityStatic.Get<SteamLobby>().SearchLobbies();
			this.selfView.trans_serverList.gameObject.SetActive(true);
			this.CreateRoomSuccess();
			this.selfView.trans_wait.gameObject.SetActive(false);
			this.selfView.btn_refresh.gameObject.SetActive(true);
		});
		this.selfView.btn_back2.AddButtonEvent(delegate
		{
			this.MainGroup();
		});
		this.selfView.btn_steam2.AddButtonEvent(delegate
		{
			Application.OpenURL("https://store.steampowered.com/app/4246640/_/");
		});
		this.selfView.btn_addQQ.AddButtonEvent(delegate
		{
			Application.OpenURL("https://qun.qq.com/universal-share/share?ac=1&authKey=%2BLHOI3oYLoQkF25cRQd4esGYEktXt9T4hXArgpN739dX0Meiz5dGx1E0Ty4983%2BK&busi_data=eyJncm91cENvZGUiOiIxMDk1OTY2Mjg0IiwidG9rZW4iOiJJMks1d1hGVlNZUnhEOUZzQUs4UXJTK29oWTd5dnNlaUFiTEVkSlF1bXBQWWpCRnBHS2pkNFdpZmVRNWZIcDlaIiwidWluIjoiMjc1NTY3NTU3NSJ9&data=gjeVFVYOdJkpSo3hMbgGsTUk9xslBhtVzQ2ktVnaaF40AhG5Of7cKF74NAzNs71iBGQu6hkr8ihS6Kqe9lUSHw&svctype=4&tempid=h5_group_info");
		});
		this.selfView.btn_unLock.AddButtonEvent(new UnityAction(this.UnlockHeroBtnClick));
		this.selfView.trans_Toggle_0.gameObject.GetComponent<Toggle>().onValueChanged.AddListener(new UnityAction<bool>(this.OnServerToggleRate0));
		this.selfView.trans_Toggle_1.gameObject.GetComponent<Toggle>().onValueChanged.AddListener(new UnityAction<bool>(this.OnServerToggleRate1));
		this.selfView.trans_Toggle_2.gameObject.GetComponent<Toggle>().onValueChanged.AddListener(new UnityAction<bool>(this.OnServerToggleRate2));
		this.selfView.btn_confirm.AddButtonEvent(delegate
		{
			this.selfView.trans_createRoom.gameObject.SetActive(false);
			EntityStatic.Get<SteamLobby>().HostLobby(this.selfView.trans_inputMessage.GetComponent<InputField>().text);
		});
	}

	// Token: 0x060015AE RID: 5550 RVA: 0x0008706C File Offset: 0x0008526C
	private void OnServerToggleRate0(bool on)
	{
		if (on)
		{
			NetworkManager.singleton.serverTickRate = 30;
			if (Game.AudioManager != null)
			{
				Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
			}
		}
	}

	// Token: 0x060015AF RID: 5551 RVA: 0x0008709E File Offset: 0x0008529E
	private void OnServerToggleRate1(bool on)
	{
		if (on)
		{
			NetworkManager.singleton.serverTickRate = 45;
			if (Game.AudioManager != null)
			{
				Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
			}
		}
	}

	// Token: 0x060015B0 RID: 5552 RVA: 0x000870D0 File Offset: 0x000852D0
	private void OnServerToggleRate2(bool on)
	{
		if (on)
		{
			NetworkManager.singleton.serverTickRate = 60;
			if (Game.AudioManager != null)
			{
				Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
			}
		}
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x00087102 File Offset: 0x00085302
	private bool GetCanInLevel()
	{
		return (SaveLoadManager.gameSaveData.levelMaskComplete & 1) != 0;
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x00087113 File Offset: 0x00085313
	private void OnQuitBtnClick()
	{
		(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText(Game.Language.Get("是否返回主菜单", ""), new Action(this.OnQuitCallBack), null, null, "");
	}

	// Token: 0x060015B3 RID: 5555 RVA: 0x00080227 File Offset: 0x0007E427
	private void OnQuitCallBack()
	{
		GameHelperClient.OnGameReset();
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x00087151 File Offset: 0x00085351
	public void HideSelectHero()
	{
		this.selfView.btn_selectHero.gameObject.SetActive(false);
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x0008716C File Offset: 0x0008536C
	public void CreateRoomSuccess()
	{
		this.selfView.trans_bg.gameObject.SetActive(false);
		this.selfView.btn_selectHero.gameObject.SetActive(true);
		this.selfView.trans_wait.gameObject.SetActive(true);
		this.selfView.btn_backMenu.gameObject.SetActive(true);
		Game.UI.OpenUI<UI_LobbyMsg>(null);
		base.ClearNavigation();
		base.AddNavigation(this.selfView.btn_selectHero);
		base.ActiveFirstNavigation();
		if (!GameHelperClient.isHost)
		{
			this.selfView.btn_selectHero.gameObject.SetActive(false);
		}
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x00087218 File Offset: 0x00085418
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.selfView.trans_bg.gameObject.SetActive(true);
		this.selfView.trans_wait.gameObject.SetActive(false);
		this.selfView.btn_backMenu.gameObject.SetActive(false);
		this.selfView.btn_selectHero.gameObject.SetActive(false);
		this.selfView.trans_serverList.gameObject.SetActive(false);
		this.selfView.trans_createRoom.gameObject.SetActive(false);
		this.MainGroup();
		EntityStatic.Get<SaveLoadManager>().Load<string>("serverIP");
		this.selfView.trans_zhangjie.gameObject.SetActive(false);
		this.selfView.trans_CNShow.gameObject.SetActive(Game.Language.LanguageCur == LanguageType.Chinese);
		this.CheckLockSetting();
		this.selfView.btn_z2.transform.GetChild(0).gameObject.SetActive(!this.GetCanInLevel());
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x00087330 File Offset: 0x00085530
	private void CheckLockSetting()
	{
		string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DungeonBrawl/LockSetting.json");
		if (File.Exists(path))
		{
			UI_StartGame.LockSettingData lockSettingData = JsonUtility.FromJson<UI_StartGame.LockSettingData>(File.ReadAllText(path));
			Game.Save.Save("isSaveHero", lockSettingData.isSaveHero);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x060015B8 RID: 5560 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ClosePanel()
	{
	}

	// Token: 0x060015B9 RID: 5561 RVA: 0x00087384 File Offset: 0x00085584
	private void MainGroup()
	{
		UI_StartGame.<MainGroup>d__15 <MainGroup>d__;
		<MainGroup>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<MainGroup>d__.<>4__this = this;
		<MainGroup>d__.<>1__state = -1;
		<MainGroup>d__.<>t__builder.Start<UI_StartGame.<MainGroup>d__15>(ref <MainGroup>d__);
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x000873BC File Offset: 0x000855BC
	private void BenDiGroup()
	{
		this.selfView.trans_main.gameObject.SetActive(false);
		this.selfView.trans_bendi.gameObject.SetActive(true);
		this.selfView.ltext_lianjiTip.text = Game.Language.Get("局域网提示", "");
		base.ClearNavigation();
		base.AddNavigation(this.selfView.btn_host);
		base.AddNavigation(this.selfView.btn_join);
		base.AddNavigation(this.selfView.btn_back1);
		base.ActiveFirstNavigation();
	}

	// Token: 0x060015BB RID: 5563 RVA: 0x00087458 File Offset: 0x00085658
	private void SteamGroup()
	{
		this.selfView.trans_main.gameObject.SetActive(false);
		this.selfView.trans_steam.gameObject.SetActive(true);
		this.selfView.ltext_lianjiTip.text = Game.Language.Get("创建在线房间后，通过 steam 好友栏位加入游戏", "");
		base.ClearNavigation();
		base.AddNavigation(this.selfView.btn_hostSteam);
		base.AddNavigation(this.selfView.btn_joinSteam);
		base.AddNavigation(this.selfView.btn_back2);
		base.ActiveFirstNavigation();
	}

	// Token: 0x060015BC RID: 5564 RVA: 0x000874F4 File Offset: 0x000856F4
	public void UnlockHeroBtnClick()
	{
		(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText("是否解锁全部英雄？", new Action(this.OnConfirmCallBack), null, null, "");
	}

	// Token: 0x060015BD RID: 5565 RVA: 0x00087523 File Offset: 0x00085723
	private void OnConfirmCallBack()
	{
		GameHelperClient.isSaveHero = false;
		Game.Save.Save("isSaveHero", GameHelperClient.isSaveHero);
	}

	// Token: 0x060015BE RID: 5566 RVA: 0x00087544 File Offset: 0x00085744
	public void UpdateServerList()
	{
		this.selfView.pool_serverList.RemoveAllView();
		foreach (ServerResponse serverResponse in ((MyServerNetworkManager)NetworkManager.singleton).DiscoveredServers.Values)
		{
			this.selfView.pool_serverList.AddView().GetComponent<JoinServerItem>().SetServerResponse(serverResponse);
		}
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x000875CC File Offset: 0x000857CC
	public void UpdateSteamServerList(List<SteamLobby.RoomInfo> roomList)
	{
		this.selfView.pool_serverList.RemoveAllView();
		foreach (SteamLobby.RoomInfo steamServerResponse in roomList)
		{
			this.selfView.pool_serverList.AddView().GetComponent<JoinServerItem>().SetSteamServerResponse(steamServerResponse);
		}
	}

	// Token: 0x060015C0 RID: 5568 RVA: 0x00087640 File Offset: 0x00085840
	public void OnConnectBtnClick()
	{
		this.selfView.trans_serverList.gameObject.SetActive(false);
	}

	// Token: 0x04001464 RID: 5220
	public UI_StartGame_View selfView;

	// Token: 0x020003B7 RID: 951
	[Serializable]
	public class LockSettingData
	{
		// Token: 0x04001465 RID: 5221
		public bool isSaveHero;
	}
}
