using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200039C RID: 924
public class UI_SelectLanguage_View : UGUIView
{
	// Token: 0x0600152B RID: 5419 RVA: 0x00082BD0 File Offset: 0x00080DD0
	public override void Init(Transform trans)
	{
		this.btn_language = trans.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
		this.btn_view = trans.GetChild(1).GetChild(0).GetChild(1).GetComponent<Button>();
		this.btn_voice = trans.GetChild(1).GetChild(0).GetChild(2).GetComponent<Button>();
		this.btn_inputPC = trans.GetChild(1).GetChild(0).GetChild(3).GetComponent<Button>();
		this.btn_battle = trans.GetChild(1).GetChild(0).GetChild(4).GetComponent<Button>();
		this.btn_deleteSave = trans.GetChild(1).GetChild(0).GetChild(5).GetComponent<Button>();
		this.btn_quit = trans.GetChild(1).GetChild(0).GetChild(6).GetComponent<Button>();
		this.trans_lang = trans.GetChild(1).GetChild(1).GetComponent<Transform>();
		this.trans_langTip = trans.GetChild(1).GetChild(2).GetComponent<Transform>();
		this.trans_view = trans.GetChild(1).GetChild(3).GetComponent<Transform>();
		this.trans_displayModeDropdown = trans.GetChild(1).GetChild(3).GetChild(0).GetComponent<Transform>();
		this.trans_resolutionDropdown = trans.GetChild(1).GetChild(3).GetChild(1).GetComponent<Transform>();
		this.trans_vsyncToggle = trans.GetChild(1).GetChild(3).GetChild(2).GetComponent<Transform>();
		this.ltext_sync = trans.GetChild(1).GetChild(3).GetChild(2).GetChild(1).GetComponent<Text>();
		this.trans_rate = trans.GetChild(1).GetChild(3).GetChild(3).GetComponent<Transform>();
		this.trans_rateLabel = trans.GetChild(1).GetChild(3).GetChild(3).GetChild(3).GetComponent<Transform>();
		this.trans_viewLight = trans.GetChild(1).GetChild(3).GetChild(4).GetComponent<Transform>();
		this.trans_viewLightLabel = trans.GetChild(1).GetChild(3).GetChild(4).GetChild(3).GetComponent<Transform>();
		this.trans_effectLight = trans.GetChild(1).GetChild(3).GetChild(5).GetComponent<Transform>();
		this.trans_effectLightLabel = trans.GetChild(1).GetChild(3).GetChild(5).GetChild(3).GetComponent<Transform>();
		this.trans_voice = trans.GetChild(1).GetChild(4).GetComponent<Transform>();
		this.trans_bgmVolumeSlider = trans.GetChild(1).GetChild(4).GetChild(0).GetComponent<Transform>();
		this.trans_bgmVolumeLabel = trans.GetChild(1).GetChild(4).GetChild(0).GetChild(3).GetComponent<Transform>();
		this.trans_sfxVolumeSlider = trans.GetChild(1).GetChild(4).GetChild(1).GetComponent<Transform>();
		this.trans_sfxVolumeLabel = trans.GetChild(1).GetChild(4).GetChild(1).GetChild(3).GetComponent<Transform>();
		this.trans_inputPC = trans.GetChild(1).GetChild(5).GetComponent<Transform>();
		this.btn_Refresh = trans.GetChild(1).GetChild(5).GetChild(2).GetComponent<Button>();
		this.trans_battle = trans.GetChild(1).GetChild(6).GetComponent<Transform>();
		this.trans_autoBattleToggle = trans.GetChild(1).GetChild(6).GetChild(0).GetComponent<Transform>();
		this.trans_damageToggle = trans.GetChild(1).GetChild(6).GetChild(1).GetComponent<Transform>();
		this.trans_keyPick = trans.GetChild(1).GetChild(6).GetChild(2).GetComponent<Transform>();
		this.trans_autoCard = trans.GetChild(1).GetChild(6).GetChild(3).GetComponent<Transform>();
		this.trans_casting = trans.GetChild(1).GetChild(6).GetChild(4).GetComponent<Transform>();
		this.trans_pickShare = trans.GetChild(1).GetChild(6).GetChild(5).GetComponent<Transform>();
		this.trans_autoSellBook = trans.GetChild(1).GetChild(6).GetChild(6).GetComponent<Transform>();
		this.trans_autoSellBook_D = trans.GetChild(1).GetChild(6).GetChild(6).GetChild(0).GetComponent<Transform>();
		this.trans_autoSellBook_C = trans.GetChild(1).GetChild(6).GetChild(6).GetChild(1).GetComponent<Transform>();
		this.trans_autoSellBook_B = trans.GetChild(1).GetChild(6).GetChild(6).GetChild(2).GetComponent<Transform>();
		this.trans_autoSellBook_A = trans.GetChild(1).GetChild(6).GetChild(6).GetChild(3).GetComponent<Transform>();
		this.trans_autoSellBook_S = trans.GetChild(1).GetChild(6).GetChild(6).GetChild(4).GetComponent<Transform>();
		this.trans_showName = trans.GetChild(1).GetChild(6).GetChild(7).GetComponent<Transform>();
		this.trans_showHeroName = trans.GetChild(1).GetChild(6).GetChild(7).GetChild(0).GetComponent<Transform>();
		this.trans_showSteamName = trans.GetChild(1).GetChild(6).GetChild(7).GetChild(1).GetComponent<Transform>();
		this.trans_hideName = trans.GetChild(1).GetChild(6).GetChild(7).GetChild(2).GetComponent<Transform>();
	}

	// Token: 0x040013AD RID: 5037
	public Button btn_language;

	// Token: 0x040013AE RID: 5038
	public Button btn_view;

	// Token: 0x040013AF RID: 5039
	public Button btn_voice;

	// Token: 0x040013B0 RID: 5040
	public Button btn_inputPC;

	// Token: 0x040013B1 RID: 5041
	public Button btn_battle;

	// Token: 0x040013B2 RID: 5042
	public Button btn_deleteSave;

	// Token: 0x040013B3 RID: 5043
	public Button btn_quit;

	// Token: 0x040013B4 RID: 5044
	public Transform trans_lang;

	// Token: 0x040013B5 RID: 5045
	public Transform trans_langTip;

	// Token: 0x040013B6 RID: 5046
	public Transform trans_view;

	// Token: 0x040013B7 RID: 5047
	public Transform trans_displayModeDropdown;

	// Token: 0x040013B8 RID: 5048
	public Transform trans_resolutionDropdown;

	// Token: 0x040013B9 RID: 5049
	public Transform trans_vsyncToggle;

	// Token: 0x040013BA RID: 5050
	public Text ltext_sync;

	// Token: 0x040013BB RID: 5051
	public Transform trans_rate;

	// Token: 0x040013BC RID: 5052
	public Transform trans_rateLabel;

	// Token: 0x040013BD RID: 5053
	public Transform trans_viewLight;

	// Token: 0x040013BE RID: 5054
	public Transform trans_viewLightLabel;

	// Token: 0x040013BF RID: 5055
	public Transform trans_effectLight;

	// Token: 0x040013C0 RID: 5056
	public Transform trans_effectLightLabel;

	// Token: 0x040013C1 RID: 5057
	public Transform trans_voice;

	// Token: 0x040013C2 RID: 5058
	public Transform trans_bgmVolumeSlider;

	// Token: 0x040013C3 RID: 5059
	public Transform trans_bgmVolumeLabel;

	// Token: 0x040013C4 RID: 5060
	public Transform trans_sfxVolumeSlider;

	// Token: 0x040013C5 RID: 5061
	public Transform trans_sfxVolumeLabel;

	// Token: 0x040013C6 RID: 5062
	public Transform trans_inputPC;

	// Token: 0x040013C7 RID: 5063
	public Button btn_Refresh;

	// Token: 0x040013C8 RID: 5064
	public Transform trans_battle;

	// Token: 0x040013C9 RID: 5065
	public Transform trans_autoBattleToggle;

	// Token: 0x040013CA RID: 5066
	public Transform trans_damageToggle;

	// Token: 0x040013CB RID: 5067
	public Transform trans_keyPick;

	// Token: 0x040013CC RID: 5068
	public Transform trans_autoCard;

	// Token: 0x040013CD RID: 5069
	public Transform trans_casting;

	// Token: 0x040013CE RID: 5070
	public Transform trans_pickShare;

	// Token: 0x040013CF RID: 5071
	public Transform trans_autoSellBook;

	// Token: 0x040013D0 RID: 5072
	public Transform trans_autoSellBook_D;

	// Token: 0x040013D1 RID: 5073
	public Transform trans_autoSellBook_C;

	// Token: 0x040013D2 RID: 5074
	public Transform trans_autoSellBook_B;

	// Token: 0x040013D3 RID: 5075
	public Transform trans_autoSellBook_A;

	// Token: 0x040013D4 RID: 5076
	public Transform trans_autoSellBook_S;

	// Token: 0x040013D5 RID: 5077
	public Transform trans_showName;

	// Token: 0x040013D6 RID: 5078
	public Transform trans_showHeroName;

	// Token: 0x040013D7 RID: 5079
	public Transform trans_showSteamName;

	// Token: 0x040013D8 RID: 5080
	public Transform trans_hideName;
}
