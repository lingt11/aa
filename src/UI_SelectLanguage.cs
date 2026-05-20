using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000396 RID: 918
public class UI_SelectLanguage : UGUICtrl
{
	// Token: 0x060014E4 RID: 5348 RVA: 0x00080D80 File Offset: 0x0007EF80
	public UI_SelectLanguage()
	{
		this.selfView = new UI_SelectLanguage_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_selectLanguage", base.GetType());
		this.uiObjects.Add(this.selfView.trans_lang.gameObject);
		this.uiObjects.Add(this.selfView.trans_view.gameObject);
		this.uiObjects.Add(this.selfView.trans_voice.gameObject);
		this.uiObjects.Add(this.selfView.trans_langTip.gameObject);
		this.uiObjects.Add(this.selfView.trans_inputPC.gameObject);
		this.uiObjects.Add(this.selfView.trans_battle.gameObject);
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x00080F74 File Offset: 0x0007F174
	protected override void ButtonAddClick()
	{
		this.selfView.btn_language.AddButtonEvent(new UnityAction(this.OnBtnLanguageClick));
		this.selfView.btn_view.AddButtonEvent(new UnityAction(this.OnBtnViewClick));
		this.selfView.btn_voice.AddButtonEvent(new UnityAction(this.OnBtnVoiceClick));
		this.selfView.btn_quit.AddButtonEvent(new UnityAction(this.OnQuitBtnClick));
		this.selfView.btn_inputPC.AddButtonEvent(new UnityAction(this.OnInputPCBtnClick));
		this.selfView.btn_battle.AddButtonEvent(new UnityAction(this.OnBattleClick));
		this.selfView.btn_deleteSave.AddButtonEvent(new UnityAction(this.OnDeleteSaveClick));
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x00081045 File Offset: 0x0007F245
	private void OnDeleteSaveClick()
	{
		(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText(Game.Language.Get("删除存档确认", ""), new Action(SaveLoadManager.OnDeleteSave), null, null, "");
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x00081083 File Offset: 0x0007F283
	private void OnQuitBtnClick()
	{
		Game.UI.CloseUI<UI_SelectLanguage>();
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x00081090 File Offset: 0x0007F290
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		for (int i = 0; i < this.all.Length; i++)
		{
			int index = i;
			this.selfView.trans_lang.GetChild(i).GetChild(0).GetComponent<Text>().text = this.all[index];
			this.selfView.trans_lang.GetChild(i).GetComponent<Button>().AddButtonEvent(delegate
			{
				LanguageType index;
				EntityStatic.Get<LanguageManager>().LanguageCur = (LanguageType)index;
				Game.UI.CloseUI<UI_SelectLanguage>();
				index = (LanguageType)index;
				Debug.Log(index.ToString());
			});
		}
		if (!this.isInit)
		{
			this.isInit = true;
			this.resolutionDropdown = this.selfView.trans_resolutionDropdown.gameObject.GetComponent<Dropdown>();
			this.sfxVolumeSlider = this.selfView.trans_sfxVolumeSlider.gameObject.GetComponent<Slider>();
			this.bgmVolumeSlider = this.selfView.trans_bgmVolumeSlider.gameObject.GetComponent<Slider>();
			this.frameRateSlider = this.selfView.trans_rate.gameObject.GetComponent<Slider>();
			this.vsyncToggle = this.selfView.trans_vsyncToggle.gameObject.GetComponent<Toggle>();
			this.autoBattleToggle = this.selfView.trans_autoBattleToggle.gameObject.GetComponent<Toggle>();
			this.damageToggle = this.selfView.trans_damageToggle.gameObject.GetComponent<Toggle>();
			this.keyPickToggle = this.selfView.trans_keyPick.gameObject.GetComponent<Toggle>();
			this.smartCastingToggle = this.selfView.trans_casting.gameObject.GetComponent<Toggle>();
			this.pickShareToggle = this.selfView.trans_pickShare.gameObject.GetComponent<Toggle>();
			this.sfxVolumeLabel = this.selfView.trans_sfxVolumeLabel.gameObject.GetComponent<TextMeshProUGUI>();
			this.bgmVolumeLabel = this.selfView.trans_bgmVolumeLabel.gameObject.GetComponent<TextMeshProUGUI>();
			this.frameRateLabel = this.selfView.trans_rateLabel.gameObject.GetComponent<TextMeshProUGUI>();
			this.displayModeDropdown = this.selfView.trans_displayModeDropdown.gameObject.GetComponent<Dropdown>();
			this.autoUseCardToggle = this.selfView.trans_autoCard.gameObject.GetComponent<Toggle>();
			this.autoSellBookToggle_D = this.selfView.trans_autoSellBook_D.gameObject.GetComponent<Toggle>();
			this.autoSellBookToggle_C = this.selfView.trans_autoSellBook_C.gameObject.GetComponent<Toggle>();
			this.autoSellBookToggle_B = this.selfView.trans_autoSellBook_B.gameObject.GetComponent<Toggle>();
			this.autoSellBookToggle_A = this.selfView.trans_autoSellBook_A.gameObject.GetComponent<Toggle>();
			this.autoSellBookToggle_S = this.selfView.trans_autoSellBook_S.gameObject.GetComponent<Toggle>();
			this.showNameToggleGroup = this.selfView.trans_showName.gameObject.GetComponent<ToggleGroup>();
			this.showHeroNameToggle = this.selfView.trans_showHeroName.gameObject.GetComponent<Toggle>();
			this.showSteamNameToggle = this.selfView.trans_showSteamName.gameObject.GetComponent<Toggle>();
			this.hideNameToggle = this.selfView.trans_hideName.gameObject.GetComponent<Toggle>();
			this.viewLightSlider = this.selfView.trans_viewLight.gameObject.GetComponent<Slider>();
			this.effectLightSlider = this.selfView.trans_effectLight.gameObject.GetComponent<Slider>();
			this.viewLightLabel = this.selfView.trans_viewLightLabel.gameObject.GetComponent<TextMeshProUGUI>();
			this.effectLightLabel = this.selfView.trans_effectLightLabel.gameObject.GetComponent<TextMeshProUGUI>();
			this.BuildResolutionList();
			this.BuildDisplayModeList();
			this.BuildFrameRateList();
			this.LoadAndApplySettings();
			this.HookUIEvents();
		}
		else
		{
			this.displayModeDropdown.ClearOptions();
			this.displayModeDropdown.AddOptions(new List<string>
			{
				Game.Language.Get("全屏", ""),
				Game.Language.Get("无边框全屏", ""),
				Game.Language.Get("窗口", "")
			});
		}
		this.selfView.ltext_sync.text = Game.Language.Get("垂直同步", "");
		if (GameHelperClient.localPlayer != null)
		{
			if (this.selfView.trans_lang.gameObject.activeSelf)
			{
				this.selfView.trans_lang.gameObject.SetActive(false);
				this.selfView.trans_langTip.gameObject.SetActive(true);
			}
		}
		else if (this.selfView.trans_langTip.gameObject.activeSelf)
		{
			this.selfView.trans_langTip.gameObject.SetActive(false);
			this.selfView.trans_lang.gameObject.SetActive(true);
		}
		MySystemEvent.Instance.DispatchMessage(35);
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x00081570 File Offset: 0x0007F770
	private void OnBtnLanguageClick()
	{
		GameObject y = (GameHelperClient.localPlayer != null) ? this.selfView.trans_langTip.gameObject : this.selfView.trans_lang.gameObject;
		for (int i = 0; i < this.uiObjects.Count; i++)
		{
			GameObject gameObject = this.uiObjects[i].gameObject;
			if (gameObject == y)
			{
				gameObject.SetActive(true);
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x000815F0 File Offset: 0x0007F7F0
	private void OnBtnViewClick()
	{
		GameObject gameObject = this.selfView.trans_view.gameObject;
		for (int i = 0; i < this.uiObjects.Count; i++)
		{
			GameObject gameObject2 = this.uiObjects[i].gameObject;
			if (gameObject2 == gameObject)
			{
				gameObject2.SetActive(true);
			}
			else
			{
				gameObject2.SetActive(false);
			}
		}
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x00081650 File Offset: 0x0007F850
	private void OnBtnVoiceClick()
	{
		GameObject gameObject = this.selfView.trans_voice.gameObject;
		for (int i = 0; i < this.uiObjects.Count; i++)
		{
			GameObject gameObject2 = this.uiObjects[i].gameObject;
			if (gameObject2 == gameObject)
			{
				gameObject2.SetActive(true);
			}
			else
			{
				gameObject2.SetActive(false);
			}
		}
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x000816B0 File Offset: 0x0007F8B0
	private void OnInputPCBtnClick()
	{
		GameObject gameObject = this.selfView.trans_inputPC.gameObject;
		for (int i = 0; i < this.uiObjects.Count; i++)
		{
			GameObject gameObject2 = this.uiObjects[i].gameObject;
			if (gameObject2 == gameObject)
			{
				gameObject2.SetActive(true);
			}
			else
			{
				gameObject2.SetActive(false);
			}
		}
		MySystemEvent.Instance.DispatchMessage(35);
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x0008171C File Offset: 0x0007F91C
	public void OnBattleClick()
	{
		GameObject gameObject = this.selfView.trans_battle.gameObject;
		for (int i = 0; i < this.uiObjects.Count; i++)
		{
			GameObject gameObject2 = this.uiObjects[i].gameObject;
			if (gameObject2 == gameObject)
			{
				gameObject2.SetActive(true);
			}
			else
			{
				gameObject2.SetActive(false);
			}
		}
		MySystemEvent.Instance.DispatchMessage(35);
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x00081788 File Offset: 0x0007F988
	private void BuildResolutionList()
	{
		Resolution[] resolutions = Screen.resolutions;
		this._uniqueResolutions = (from r in (from r in resolutions
		group r by new Vector2Int(r.width, r.height)).Select(delegate(IGrouping<Vector2Int, Resolution> g)
		{
			Resolution resolution = (from r in g
			orderby r.refreshRate descending
			select r).First<Resolution>();
			return new Resolution
			{
				width = resolution.width,
				height = resolution.height,
				refreshRate = resolution.refreshRate
			};
		})
		orderby r.width, r.height
		select r).ToList<Resolution>();
		this.resolutionDropdown.ClearOptions();
		List<string> options = (from r in this._uniqueResolutions
		select string.Format("{0} x {1} @{2}Hz", r.width, r.height, r.refreshRate)).ToList<string>();
		this.resolutionDropdown.AddOptions(options);
		Resolution cur = Screen.currentResolution;
		this._currentResIndex = this._uniqueResolutions.FindIndex((Resolution r) => r.width == Screen.width && r.height == Screen.height);
		if (this._currentResIndex < 0)
		{
			this._currentResIndex = this._uniqueResolutions.FindIndex((Resolution r) => r.width == cur.width && r.height == cur.height);
			if (this._currentResIndex < 0)
			{
				this._currentResIndex = this._uniqueResolutions.Count - 1;
			}
		}
		this.resolutionDropdown.value = this._currentResIndex;
		this.resolutionDropdown.RefreshShownValue();
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x00081920 File Offset: 0x0007FB20
	private void BuildDisplayModeList()
	{
		this.displayModeDropdown.ClearOptions();
		this.displayModeDropdown.AddOptions(new List<string>
		{
			Game.Language.Get("全屏", ""),
			Game.Language.Get("无边框全屏", ""),
			Game.Language.Get("窗口", "")
		});
		int value = (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen) ? 0 : ((Screen.fullScreenMode == FullScreenMode.FullScreenWindow) ? 1 : 2);
		this.displayModeDropdown.value = value;
		this.displayModeDropdown.RefreshShownValue();
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x000819C4 File Offset: 0x0007FBC4
	private void BuildFrameRateList()
	{
		List<int> list = new List<int>();
		foreach (int frameRate in UI_SelectLanguage.DefaultFrameRateOptions)
		{
			this.AddFrameRateOption(list, frameRate);
		}
		this.AddFrameRateOption(list, UI_SelectLanguage.GetDefaultFrameRate());
		int @int = PlayerPrefs.GetInt("settings.gfx.framerate", UI_SelectLanguage.GetDefaultFrameRate());
		if (@int != -1)
		{
			this.AddFrameRateOption(list, @int);
		}
		list.Sort();
		list.Add(-1);
		this._frameRateOptions = list;
		if (this.frameRateSlider)
		{
			this.frameRateSlider.minValue = 0f;
			this.frameRateSlider.maxValue = (float)Mathf.Max(0, this._frameRateOptions.Count - 1);
			this.frameRateSlider.wholeNumbers = true;
		}
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x00081A7D File Offset: 0x0007FC7D
	private void AddFrameRateOption(List<int> options, int frameRate)
	{
		if (frameRate > 0 && !options.Contains(frameRate))
		{
			options.Add(frameRate);
		}
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x00081A94 File Offset: 0x0007FC94
	private void LoadAndApplySettings()
	{
		bool flag = PlayerPrefs.GetInt("settings.gfx.vsync", 1) == 1;
		QualitySettings.vSyncCount = (flag ? 1 : 0);
		if (this.vsyncToggle)
		{
			this.vsyncToggle.isOn = flag;
		}
		int @int = PlayerPrefs.GetInt("settings.gfx.framerate", UI_SelectLanguage.GetDefaultFrameRate());
		UI_SelectLanguage.ApplyFrameRate(@int);
		if (this.frameRateSlider)
		{
			this.frameRateSlider.value = (float)this.GetFrameRateOptionIndex(@int);
		}
		this.UpdateFrameRateLabel(this.frameRateLabel, @int);
		this.autoBattleToggle.isOn = GameHelperClient.IsAutoBattle;
		this.damageToggle.isOn = GameHelperClient.IsShowDamage;
		this.keyPickToggle.isOn = GameHelperClient.IsKeyPickTalisman;
		this.smartCastingToggle.isOn = GameHelperClient.IsSmartCasting;
		this.pickShareToggle.isOn = GameHelperClient.IsPickShare;
		this.autoUseCardToggle.isOn = GameHelperClient.IsAutoUseCard;
		this.autoSellBookToggle_D.isOn = ((GameHelperClient.AutoSellBookMask & 1) != 0);
		this.autoSellBookToggle_C.isOn = ((GameHelperClient.AutoSellBookMask & 2) != 0);
		this.autoSellBookToggle_B.isOn = ((GameHelperClient.AutoSellBookMask & 4) != 0);
		this.autoSellBookToggle_A.isOn = ((GameHelperClient.AutoSellBookMask & 8) != 0);
		this.autoSellBookToggle_S.isOn = ((GameHelperClient.AutoSellBookMask & 16) != 0);
		this.ApplyPlayerNameDisplayToggle(GameHelperClient.playerNameDisplayMode);
		float @float = PlayerPrefs.GetFloat("settings.audio.sfx", 0.5f);
		this.ApplyVolume("SFXVolume", @float, false, "");
		if (this.sfxVolumeSlider)
		{
			this.sfxVolumeSlider.value = @float;
		}
		this.UpdateVolumeLabel(this.sfxVolumeLabel, @float);
		float float2 = PlayerPrefs.GetFloat("settings.audio.bgm", 0.5f);
		this.ApplyVolume("BGMVolume", float2, false, "");
		if (this.bgmVolumeSlider)
		{
			this.bgmVolumeSlider.value = float2;
		}
		this.UpdateVolumeLabel(this.bgmVolumeLabel, float2);
		int num = PlayerPrefs.GetInt("settings.gfx.displaymode", 0);
		num = Mathf.Clamp(num, 0, 2);
		if (this.displayModeDropdown)
		{
			this.displayModeDropdown.value = num;
			this.displayModeDropdown.RefreshShownValue();
		}
		this._windowedResIndex = PlayerPrefs.GetInt("settings.res.index.windowed", this._currentResIndex);
		this._windowedResIndex = Mathf.Clamp(this._windowedResIndex, 0, this._uniqueResolutions.Count - 1);
		int num2 = PlayerPrefs.GetInt("settings.res.index", this._currentResIndex);
		num2 = Mathf.Clamp(num2, 0, this._uniqueResolutions.Count - 1);
		if (this.resolutionDropdown)
		{
			this.resolutionDropdown.value = num2;
			this.resolutionDropdown.RefreshShownValue();
		}
		this.ApplyDisplayModeImmediate((UI_SelectLanguage.DisplayModeOption)num);
		this.ApplyResolutionImmediateForMode((UI_SelectLanguage.DisplayModeOption)num, num2);
		this.ReapplyNextFrame((UI_SelectLanguage.DisplayModeOption)num);
		float float3 = PlayerPrefs.GetFloat("settings.viewlight", 1f);
		this.ApplyViewLight(float3);
		this.viewLightSlider.value = float3;
		this.UpdateVolumeLabel(this.viewLightLabel, float3);
		float float4 = PlayerPrefs.GetFloat("settings.effectlight", 1f);
		this.ApplyEffectLight(float4);
		this.effectLightSlider.value = float4;
		this.UpdateVolumeLabel(this.effectLightLabel, float4);
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x00081DC0 File Offset: 0x0007FFC0
	private void HookUIEvents()
	{
		this.vsyncToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnVSyncToggle));
		this.autoBattleToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnAutoBattleToggle));
		this.damageToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnDamageToggle));
		this.keyPickToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnKeyPickToggle));
		this.smartCastingToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnSmartCastingToggle));
		this.pickShareToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnPickShareToggle));
		this.autoUseCardToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnAutoUseCardToggle));
		this.autoSellBookToggle_D.onValueChanged.AddListener(new UnityAction<bool>(this.OnAutoSellBookToggle_D));
		this.autoSellBookToggle_C.onValueChanged.AddListener(new UnityAction<bool>(this.OnAutoSellBookToggle_C));
		this.autoSellBookToggle_B.onValueChanged.AddListener(new UnityAction<bool>(this.OnAutoSellBookToggle_B));
		this.autoSellBookToggle_A.onValueChanged.AddListener(new UnityAction<bool>(this.OnAutoSellBookToggle_A));
		this.autoSellBookToggle_S.onValueChanged.AddListener(new UnityAction<bool>(this.OnAutoSellBookToggle_S));
		this.showHeroNameToggle.onValueChanged.AddListener(delegate(bool on)
		{
			this.OnPlayerNameDisplayToggle(GameHelperClient.PlayerNameDisplayMode.HeroName, on);
		});
		this.showSteamNameToggle.onValueChanged.AddListener(delegate(bool on)
		{
			this.OnPlayerNameDisplayToggle(GameHelperClient.PlayerNameDisplayMode.SteamName, on);
		});
		this.hideNameToggle.onValueChanged.AddListener(delegate(bool on)
		{
			this.OnPlayerNameDisplayToggle(GameHelperClient.PlayerNameDisplayMode.HideName, on);
		});
		this.sfxVolumeSlider.onValueChanged.AddListener(delegate(float val)
		{
			this.OnVolumeChanged("SFXVolume", "settings.audio.sfx", this.sfxVolumeLabel, val);
		});
		this.bgmVolumeSlider.onValueChanged.AddListener(delegate(float val)
		{
			this.OnVolumeChanged("BGMVolume", "settings.audio.bgm", this.bgmVolumeLabel, val);
		});
		this.frameRateSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnFrameRateChanged));
		this.resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnResolutionChanged));
		this.displayModeDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnDisplayModeChanged));
		this.viewLightSlider.onValueChanged.AddListener(delegate(float val)
		{
			this.OnViewLightChanged("settings.viewlight", this.viewLightLabel, val);
		});
		this.effectLightSlider.onValueChanged.AddListener(delegate(float val)
		{
			this.OnEffectLightChanged("settings.effectlight", this.effectLightLabel, val);
		});
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x00082038 File Offset: 0x00080238
	private void OnVSyncToggle(bool on)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		QualitySettings.vSyncCount = (on ? 1 : 0);
		UI_SelectLanguage.ApplySavedFrameRateSetting();
		PlayerPrefs.SetInt("settings.gfx.vsync", on ? 1 : 0);
		PlayerPrefs.Save();
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x00082090 File Offset: 0x00080290
	private void OnAutoBattleToggle(bool on)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		GameHelperClient.IsAutoBattle = on;
		Game.Save.Save("IsAutoBattle", GameHelperClient.IsAutoBattle);
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x000820E0 File Offset: 0x000802E0
	private void OnDamageToggle(bool on)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		GameHelperClient.IsShowDamage = on;
		Game.Save.Save("IsShowDamage", GameHelperClient.IsShowDamage);
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x00082130 File Offset: 0x00080330
	private void OnKeyPickToggle(bool on)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		GameHelperClient.IsKeyPickTalisman = on;
		Game.Save.Save("IsKeyPickTalisman", GameHelperClient.IsKeyPickTalisman);
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x00082180 File Offset: 0x00080380
	private void OnSmartCastingToggle(bool on)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		GameHelperClient.IsSmartCasting = on;
		Game.Save.Save("IsSmartCasting", GameHelperClient.IsSmartCasting);
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x000821D0 File Offset: 0x000803D0
	private void OnPickShareToggle(bool on)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		GameHelperClient.IsPickShare = on;
		Game.Save.Save("IsPickShare", GameHelperClient.IsPickShare);
		if (GameHelperClient.localPlayer != null)
		{
			GameHelperClient.localPlayer.CmdUpdatePickShare(on);
		}
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x00082238 File Offset: 0x00080438
	private void OnAutoUseCardToggle(bool on)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		GameHelperClient.IsAutoUseCard = on;
		Game.Save.Save("IsAutoUseCard", GameHelperClient.IsAutoUseCard);
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x00082288 File Offset: 0x00080488
	private void ApplyPlayerNameDisplayToggle(GameHelperClient.PlayerNameDisplayMode mode)
	{
		if (this.showNameToggleGroup != null)
		{
			this.showNameToggleGroup.allowSwitchOff = false;
			this.showHeroNameToggle.group = this.showNameToggleGroup;
			this.showSteamNameToggle.group = this.showNameToggleGroup;
			this.hideNameToggle.group = this.showNameToggleGroup;
		}
		this.showHeroNameToggle.isOn = (mode == GameHelperClient.PlayerNameDisplayMode.HeroName);
		this.showSteamNameToggle.isOn = (mode == GameHelperClient.PlayerNameDisplayMode.SteamName);
		this.hideNameToggle.isOn = (mode == GameHelperClient.PlayerNameDisplayMode.HideName);
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x00082310 File Offset: 0x00080510
	private void OnPlayerNameDisplayToggle(GameHelperClient.PlayerNameDisplayMode mode, bool on)
	{
		if (!on)
		{
			return;
		}
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		GameHelperClient.playerNameDisplayMode = mode;
		Game.Save.Save("PlayerNameDisplayMode", (int)mode);
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x00082360 File Offset: 0x00080560
	private void OnAutoSellBookToggle_D(bool on)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		if (on)
		{
			if ((GameHelperClient.AutoSellBookMask & 1) == 0)
			{
				GameHelperClient.AutoSellBookMask++;
				return;
			}
		}
		else if ((GameHelperClient.AutoSellBookMask & 1) != 0)
		{
			GameHelperClient.AutoSellBookMask--;
		}
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x000823BC File Offset: 0x000805BC
	private void OnAutoSellBookToggle_C(bool on)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		if (on)
		{
			if ((GameHelperClient.AutoSellBookMask & 2) == 0)
			{
				GameHelperClient.AutoSellBookMask += 2;
				return;
			}
		}
		else if ((GameHelperClient.AutoSellBookMask & 2) != 0)
		{
			GameHelperClient.AutoSellBookMask -= 2;
		}
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x00082418 File Offset: 0x00080618
	private void OnAutoSellBookToggle_B(bool on)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		if (on)
		{
			if ((GameHelperClient.AutoSellBookMask & 4) == 0)
			{
				GameHelperClient.AutoSellBookMask += 4;
				return;
			}
		}
		else if ((GameHelperClient.AutoSellBookMask & 4) != 0)
		{
			GameHelperClient.AutoSellBookMask -= 4;
		}
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x00082474 File Offset: 0x00080674
	private void OnAutoSellBookToggle_A(bool on)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		if (on)
		{
			if ((GameHelperClient.AutoSellBookMask & 8) == 0)
			{
				GameHelperClient.AutoSellBookMask += 8;
				return;
			}
		}
		else if ((GameHelperClient.AutoSellBookMask & 8) != 0)
		{
			GameHelperClient.AutoSellBookMask -= 8;
		}
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x000824D0 File Offset: 0x000806D0
	private void OnAutoSellBookToggle_S(bool on)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		if (on)
		{
			if ((GameHelperClient.AutoSellBookMask & 16) == 0)
			{
				GameHelperClient.AutoSellBookMask += 16;
				return;
			}
		}
		else if ((GameHelperClient.AutoSellBookMask & 16) != 0)
		{
			GameHelperClient.AutoSellBookMask -= 16;
		}
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x00082530 File Offset: 0x00080730
	private void OnResolutionChanged(int index)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		UI_SelectLanguage.DisplayModeOption displayModeOption = (UI_SelectLanguage.DisplayModeOption)(this.displayModeDropdown ? this.displayModeDropdown.value : 1);
		if (displayModeOption == UI_SelectLanguage.DisplayModeOption.Windowed)
		{
			this._windowedResIndex = index;
			PlayerPrefs.SetInt("settings.res.index.windowed", this._windowedResIndex);
		}
		this.ApplyResolutionImmediateForMode(displayModeOption, index);
		PlayerPrefs.SetInt("settings.res.index", index);
		PlayerPrefs.Save();
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x000825B0 File Offset: 0x000807B0
	private void OnDisplayModeChanged(int index)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		UI_SelectLanguage.DisplayModeOption displayModeOption = (UI_SelectLanguage.DisplayModeOption)Mathf.Clamp(index, 0, 2);
		this.ApplyDisplayModeImmediate(displayModeOption);
		int dropdownIndex = this.resolutionDropdown ? this.resolutionDropdown.value : this._currentResIndex;
		this.ApplyResolutionImmediateForMode(displayModeOption, dropdownIndex);
		PlayerPrefs.SetInt("settings.gfx.displaymode", (int)displayModeOption);
		PlayerPrefs.Save();
		this.ReapplyNextFrame(displayModeOption);
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x0008262E File Offset: 0x0008082E
	private void ReapplyNextFrame(UI_SelectLanguage.DisplayModeOption mode)
	{
		this.ApplyResolutionImmediateForMode(mode, this.resolutionDropdown ? this.resolutionDropdown.value : this._currentResIndex);
	}

	// Token: 0x06001505 RID: 5381 RVA: 0x00082657 File Offset: 0x00080857
	private void ApplyDisplayModeImmediate(UI_SelectLanguage.DisplayModeOption option)
	{
		Screen.fullScreenMode = ((option == UI_SelectLanguage.DisplayModeOption.ExclusiveFullscreen) ? FullScreenMode.ExclusiveFullScreen : ((option == UI_SelectLanguage.DisplayModeOption.BorderlessFullscreen) ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed));
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x0008266C File Offset: 0x0008086C
	private void ApplyResolutionImmediateForMode(UI_SelectLanguage.DisplayModeOption mode, int dropdownIndex)
	{
		dropdownIndex = Mathf.Clamp(dropdownIndex, 0, this._uniqueResolutions.Count - 1);
		if (mode == UI_SelectLanguage.DisplayModeOption.BorderlessFullscreen)
		{
			int systemWidth = Display.main.systemWidth;
			int systemHeight = Display.main.systemHeight;
			Screen.SetResolution(systemWidth, systemHeight, FullScreenMode.FullScreenWindow);
			return;
		}
		if (mode == UI_SelectLanguage.DisplayModeOption.ExclusiveFullscreen)
		{
			Resolution resolution = this._uniqueResolutions[dropdownIndex];
			Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.ExclusiveFullScreen);
			return;
		}
		int num = (this._windowedResIndex >= 0) ? this._windowedResIndex : dropdownIndex;
		num = Mathf.Clamp(num, 0, this._uniqueResolutions.Count - 1);
		Resolution resolution2 = this._uniqueResolutions[num];
		Screen.SetResolution(resolution2.width, resolution2.height, FullScreenMode.Windowed);
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x0008271E File Offset: 0x0008091E
	private void OnVolumeChanged(string mixerParam, string prefsKey, TextMeshProUGUI label, float value)
	{
		this.ApplyVolume(mixerParam, value, true, prefsKey);
		this.UpdateVolumeLabel(label, value);
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x00082734 File Offset: 0x00080934
	private void OnFrameRateChanged(float value)
	{
		int index = Mathf.Clamp(Mathf.RoundToInt(value), 0, this._frameRateOptions.Count - 1);
		int num = this._frameRateOptions[index];
		UI_SelectLanguage.ApplyFrameRate(num);
		PlayerPrefs.SetInt("settings.gfx.framerate", num);
		PlayerPrefs.Save();
		this.UpdateFrameRateLabel(this.frameRateLabel, num);
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x0008278B File Offset: 0x0008098B
	public static void ApplySavedFrameRateSetting()
	{
		UI_SelectLanguage.ApplyFrameRate(PlayerPrefs.GetInt("settings.gfx.framerate", UI_SelectLanguage.GetDefaultFrameRate()));
	}

	// Token: 0x0600150A RID: 5386 RVA: 0x000827A1 File Offset: 0x000809A1
	private static void ApplyFrameRate(int frameRate)
	{
		Application.targetFrameRate = ((frameRate == -1) ? -1 : Mathf.Max(1, frameRate));
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x000827B8 File Offset: 0x000809B8
	private static int GetDefaultFrameRate()
	{
		return Mathf.Max(1, Mathf.RoundToInt((float)Screen.currentResolution.refreshRateRatio.value));
	}

	// Token: 0x0600150C RID: 5388 RVA: 0x000827E8 File Offset: 0x000809E8
	private int GetFrameRateOptionIndex(int frameRate)
	{
		int num = this._frameRateOptions.IndexOf(frameRate);
		if (num < 0)
		{
			return this._frameRateOptions.IndexOf(-1);
		}
		return num;
	}

	// Token: 0x0600150D RID: 5389 RVA: 0x00082814 File Offset: 0x00080A14
	private void UpdateFrameRateLabel(TextMeshProUGUI label, int frameRate)
	{
		if (!label)
		{
			return;
		}
		label.text = ((frameRate == -1) ? "∞" : string.Format("{0} FPS", frameRate));
	}

	// Token: 0x0600150E RID: 5390 RVA: 0x00082840 File Offset: 0x00080A40
	private void OnViewLightChanged(string prefsKey, TextMeshProUGUI label, float value)
	{
		float value2 = Mathf.Clamp(value, 0f, 1f);
		PlayerPrefs.SetFloat(prefsKey, value);
		PlayerPrefs.Save();
		this.ApplyViewLight(value2);
		this.UpdateVolumeLabel(label, value2);
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x00082879 File Offset: 0x00080A79
	private void ApplyViewLight(float value)
	{
		GlobalVolumeController.Instance.SetBrightness(value);
	}

	// Token: 0x06001510 RID: 5392 RVA: 0x00082888 File Offset: 0x00080A88
	private void OnEffectLightChanged(string prefsKey, TextMeshProUGUI label, float value)
	{
		float value2 = Mathf.Clamp(value, 0f, 1f);
		PlayerPrefs.SetFloat(prefsKey, value);
		PlayerPrefs.Save();
		this.ApplyEffectLight(value2);
		this.UpdateVolumeLabel(label, value2);
	}

	// Token: 0x06001511 RID: 5393 RVA: 0x000828C1 File Offset: 0x00080AC1
	private void ApplyEffectLight(float value)
	{
		GlobalVolumeController.Instance.SetBloom(value);
	}

	// Token: 0x06001512 RID: 5394 RVA: 0x000828D0 File Offset: 0x00080AD0
	private void ApplyResolution(int index, bool save)
	{
		index = Mathf.Clamp(index, 0, this._uniqueResolutions.Count - 1);
		Resolution resolution = this._uniqueResolutions[index];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
		if (save)
		{
			PlayerPrefs.SetInt("settings.res.index", index);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x06001513 RID: 5395 RVA: 0x0008292B File Offset: 0x00080B2B
	private void ApplyDisplayMode(UI_SelectLanguage.DisplayModeOption option, bool save)
	{
		Screen.fullScreenMode = ((option == UI_SelectLanguage.DisplayModeOption.ExclusiveFullscreen) ? FullScreenMode.ExclusiveFullScreen : ((option == UI_SelectLanguage.DisplayModeOption.BorderlessFullscreen) ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed));
		if (save)
		{
			PlayerPrefs.SetInt("settings.gfx.displaymode", (int)option);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x00082954 File Offset: 0x00080B54
	private void ApplyVolume(string mixerParam, float value, bool save, string prefsKey = "")
	{
		float num = Mathf.Clamp(value, 0f, 1f);
		if (mixerParam.Equals("SFXVolume"))
		{
			Game.AudioManager.MusicVol = num;
		}
		else
		{
			Game.AudioManager.bgmVol = num;
		}
		if (save && !string.IsNullOrEmpty(prefsKey))
		{
			PlayerPrefs.SetFloat(prefsKey, value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x000829B0 File Offset: 0x00080BB0
	private void UpdateVolumeLabel(TextMeshProUGUI label, float value)
	{
		if (label)
		{
			label.text = string.Format("{0}%", Mathf.RoundToInt(value * 100f));
		}
	}

	// Token: 0x0400136A RID: 4970
	public UI_SelectLanguage_View selfView;

	// Token: 0x0400136B RID: 4971
	private List<GameObject> uiObjects = new List<GameObject>();

	// Token: 0x0400136C RID: 4972
	public string[] all = new string[]
	{
		"简体中文",
		"繁体中文",
		"English",
		"日本語",
		"Русский язык",
		"Français",
		"Deutsch",
		"España",
		"Italiano",
		"Dansk",
		"Magyar",
		"Türkçe",
		"Norge",
		"ไทย",
		"Română",
		"Nederlands",
		"Português",
		"Tiếng Việt",
		"한국어",
		"Українська",
		"Български",
		"Indonesian",
		"Ελληνικά",
		"Čeština",
		"Polski",
		"Svenska",
		"suomi",
		"بالعربية"
	};

	// Token: 0x0400136D RID: 4973
	private bool isInit;

	// Token: 0x0400136E RID: 4974
	private Dropdown resolutionDropdown;

	// Token: 0x0400136F RID: 4975
	private Dropdown displayModeDropdown;

	// Token: 0x04001370 RID: 4976
	private Slider sfxVolumeSlider;

	// Token: 0x04001371 RID: 4977
	private Slider bgmVolumeSlider;

	// Token: 0x04001372 RID: 4978
	private Slider frameRateSlider;

	// Token: 0x04001373 RID: 4979
	private Toggle vsyncToggle;

	// Token: 0x04001374 RID: 4980
	private Toggle autoBattleToggle;

	// Token: 0x04001375 RID: 4981
	private Toggle damageToggle;

	// Token: 0x04001376 RID: 4982
	private Toggle keyPickToggle;

	// Token: 0x04001377 RID: 4983
	private Toggle smartCastingToggle;

	// Token: 0x04001378 RID: 4984
	private Toggle pickShareToggle;

	// Token: 0x04001379 RID: 4985
	private Toggle autoUseCardToggle;

	// Token: 0x0400137A RID: 4986
	private Toggle autoSellBookToggle_D;

	// Token: 0x0400137B RID: 4987
	private Toggle autoSellBookToggle_C;

	// Token: 0x0400137C RID: 4988
	private Toggle autoSellBookToggle_B;

	// Token: 0x0400137D RID: 4989
	private Toggle autoSellBookToggle_A;

	// Token: 0x0400137E RID: 4990
	private Toggle autoSellBookToggle_S;

	// Token: 0x0400137F RID: 4991
	private ToggleGroup showNameToggleGroup;

	// Token: 0x04001380 RID: 4992
	private Toggle showHeroNameToggle;

	// Token: 0x04001381 RID: 4993
	private Toggle showSteamNameToggle;

	// Token: 0x04001382 RID: 4994
	private Toggle hideNameToggle;

	// Token: 0x04001383 RID: 4995
	private TextMeshProUGUI sfxVolumeLabel;

	// Token: 0x04001384 RID: 4996
	private TextMeshProUGUI bgmVolumeLabel;

	// Token: 0x04001385 RID: 4997
	private TextMeshProUGUI frameRateLabel;

	// Token: 0x04001386 RID: 4998
	private const string KEY_RESOLUTION_INDEX = "settings.res.index";

	// Token: 0x04001387 RID: 4999
	private const string KEY_WINDOWED_RES_INDEX = "settings.res.index.windowed";

	// Token: 0x04001388 RID: 5000
	private const string KEY_VSYNC = "settings.gfx.vsync";

	// Token: 0x04001389 RID: 5001
	private const string KEY_DISPLAY_MODE = "settings.gfx.displaymode";

	// Token: 0x0400138A RID: 5002
	public const string KEY_FRAME_RATE = "settings.gfx.framerate";

	// Token: 0x0400138B RID: 5003
	public const string KEY_SFX_VOLUME = "settings.audio.sfx";

	// Token: 0x0400138C RID: 5004
	public const string KEY_BGM_VOLUME = "settings.audio.bgm";

	// Token: 0x0400138D RID: 5005
	public const string KEY_VIEWLIGHT_VOLUME = "settings.viewlight";

	// Token: 0x0400138E RID: 5006
	public const string KEY_EFFECTLIGHT_VOLUME = "settings.effectlight";

	// Token: 0x0400138F RID: 5007
	private List<Resolution> _uniqueResolutions;

	// Token: 0x04001390 RID: 5008
	private List<int> _frameRateOptions = new List<int>();

	// Token: 0x04001391 RID: 5009
	private int _currentResIndex;

	// Token: 0x04001392 RID: 5010
	private const int FRAME_RATE_UNLIMITED = -1;

	// Token: 0x04001393 RID: 5011
	private static readonly int[] DefaultFrameRateOptions = new int[]
	{
		30,
		60,
		90,
		120,
		144,
		200,
		240,
		360
	};

	// Token: 0x04001394 RID: 5012
	private int _windowedResIndex = -1;

	// Token: 0x04001395 RID: 5013
	private Slider viewLightSlider;

	// Token: 0x04001396 RID: 5014
	private Slider effectLightSlider;

	// Token: 0x04001397 RID: 5015
	private TextMeshProUGUI viewLightLabel;

	// Token: 0x04001398 RID: 5016
	private TextMeshProUGUI effectLightLabel;

	// Token: 0x02000397 RID: 919
	public enum BookMask
	{
		// Token: 0x0400139A RID: 5018
		D = 1,
		// Token: 0x0400139B RID: 5019
		C,
		// Token: 0x0400139C RID: 5020
		B = 4,
		// Token: 0x0400139D RID: 5021
		A = 8,
		// Token: 0x0400139E RID: 5022
		S = 16
	}

	// Token: 0x02000398 RID: 920
	private enum DisplayModeOption
	{
		// Token: 0x040013A0 RID: 5024
		ExclusiveFullscreen,
		// Token: 0x040013A1 RID: 5025
		BorderlessFullscreen,
		// Token: 0x040013A2 RID: 5026
		Windowed
	}
}
