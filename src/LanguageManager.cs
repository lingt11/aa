using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class LanguageManager
{
	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060000F9 RID: 249 RVA: 0x000068CC File Offset: 0x00004ACC
	// (set) Token: 0x060000FA RID: 250 RVA: 0x000068D4 File Offset: 0x00004AD4
	public LanguageType LanguageCur
	{
		get
		{
			return this.language;
		}
		set
		{
			this.language = value;
			Game.Save.Save("language", (int)this.language);
			MySystemEvent.Instance.DispatchMessage(21);
		}
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00006904 File Offset: 0x00004B04
	public LanguageManager()
	{
		this.languageDic.Clear();
		foreach (KeyValuePair<string, object> keyValuePair in ExcelManager.allExcelData)
		{
			if (keyValuePair.Key.Contains("language_"))
			{
				foreach (KeyValuePair<string, object> keyValuePair2 in ((Dictionary<string, object>)keyValuePair.Value))
				{
					this.languageDic.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
		}
		if (Game.Save.Check("language"))
		{
			this.language = (LanguageType)Game.Save.Load<int>("language", "2");
			return;
		}
		switch (Application.systemLanguage)
		{
		case SystemLanguage.Arabic:
			this.language = LanguageType.alabo;
			break;
		case SystemLanguage.Basque:
		case SystemLanguage.Belarusian:
		case SystemLanguage.Catalan:
		case SystemLanguage.Chinese:
		case SystemLanguage.English:
		case SystemLanguage.Estonian:
		case SystemLanguage.Faroese:
		case SystemLanguage.Hebrew:
		case SystemLanguage.Icelandic:
		case SystemLanguage.Latvian:
		case SystemLanguage.Lithuanian:
		case SystemLanguage.SerboCroatian:
		case SystemLanguage.Slovak:
		case SystemLanguage.Slovenian:
			break;
		case SystemLanguage.Bulgarian:
			this.language = LanguageType.baojialiya;
			return;
		case SystemLanguage.Czech:
			this.language = LanguageType.jieke;
			return;
		case SystemLanguage.Danish:
			this.language = LanguageType.danmai;
			return;
		case SystemLanguage.Dutch:
			this.language = LanguageType.helan;
			return;
		case SystemLanguage.Finnish:
			this.language = LanguageType.fenlan;
			return;
		case SystemLanguage.French:
			this.language = LanguageType.fayu;
			return;
		case SystemLanguage.German:
			this.language = LanguageType.deyu;
			return;
		case SystemLanguage.Greek:
			this.language = LanguageType.xila;
			return;
		case SystemLanguage.Hungarian:
			this.language = LanguageType.xiongyali;
			return;
		case SystemLanguage.Indonesian:
			this.language = LanguageType.yindunixiya;
			return;
		case SystemLanguage.Italian:
			this.language = LanguageType.yidali;
			return;
		case SystemLanguage.Japanese:
			this.language = LanguageType.Japanese;
			return;
		case SystemLanguage.Korean:
			this.language = LanguageType.hanyu;
			return;
		case SystemLanguage.Norwegian:
			this.language = LanguageType.nuowei;
			return;
		case SystemLanguage.Polish:
			this.language = LanguageType.bolan;
			return;
		case SystemLanguage.Portuguese:
			this.language = LanguageType.putaoya;
			return;
		case SystemLanguage.Romanian:
			this.language = LanguageType.luomaniya;
			return;
		case SystemLanguage.Russian:
			this.language = LanguageType.ru;
			return;
		case SystemLanguage.Spanish:
			this.language = LanguageType.xibanya;
			return;
		case SystemLanguage.Swedish:
			this.language = LanguageType.ruidian;
			return;
		case SystemLanguage.Thai:
			this.language = LanguageType.taiyu;
			return;
		case SystemLanguage.Turkish:
			this.language = LanguageType.tuerqi;
			return;
		case SystemLanguage.Ukrainian:
			this.language = LanguageType.wukelan;
			return;
		case SystemLanguage.Vietnamese:
			this.language = LanguageType.yuenan;
			return;
		case SystemLanguage.ChineseSimplified:
			this.language = LanguageType.Chinese;
			return;
		case SystemLanguage.ChineseTraditional:
			this.language = LanguageType.Fanti;
			return;
		default:
			return;
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00006BAC File Offset: 0x00004DAC
	public string Get(string key, string readyStr = "")
	{
		LanguageType languageCur = Game.Language.LanguageCur;
		if (!this.languageDic.ContainsKey(key))
		{
			return key;
		}
		Dictionary<string, object> dic = (Dictionary<string, object>)this.languageDic[key];
		string text = dic.DIC(languageCur.ToString());
		if (!string.IsNullOrEmpty(text))
		{
			return text;
		}
		string text2 = dic.DIC(LanguageType.Chinese.ToString());
		if (!string.IsNullOrEmpty(text2))
		{
			return text2;
		}
		if (!string.IsNullOrEmpty(readyStr))
		{
			return readyStr;
		}
		return key;
	}

	// Token: 0x04000127 RID: 295
	private LanguageType language = LanguageType.English;

	// Token: 0x04000128 RID: 296
	public Dictionary<string, object> languageDic = new Dictionary<string, object>();
}
