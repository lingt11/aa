using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020000B7 RID: 183
public class BuffUITouch : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06000361 RID: 865 RVA: 0x00016672 File Offset: 0x00014872
	private void OnDisable()
	{
		this.OnPointerExit(null);
	}

	// Token: 0x06000362 RID: 866 RVA: 0x0001667C File Offset: 0x0001487C
	public void OnPointerEnter(PointerEventData eventData)
	{
		string text = "";
		string nameStr = "";
		string iconPath = "";
		int quality = 0;
		bool isRelic = false;
		string exInfo = "";
		if (this.relicBase != null)
		{
			string text2 = this.relicBase.relicData.DIC("id");
			if (text2.Contains("monster"))
			{
				text = Game.Language.Get(text2 + "_m", "");
				nameStr = Game.Language.Get(text2, "");
				quality = -1;
			}
			else
			{
				isRelic = true;
				text = this.relicBase.GetFormatDec(Game.Language.Get("pickitem_" + text2 + "_m", ""));
				nameStr = Util.GetLevelStarName(Game.Language.Get("pickitem_" + text2, ""), this.relicBase.level);
				quality = this.relicBase.quality;
				exInfo = string.Format(ColorDefine.QuaRelicText[this.relicBase.quality], Game.Language.Get(PathDefine.Concat("quality_", this.relicBase.quality), ""));
			}
			if (this.relicBase.totals != null)
			{
				int num = this.relicBase.totals.Length;
				string[] array = new string[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = string.Format(ColorDefine.NormalColor, this.relicBase.isTotalPercent ? PathDefine.Concat(this.relicBase.totals[i], StringDefine.Percent) : this.relicBase.totals[i]);
				}
				object a = text;
				object wrapDouble = StringDefine.WrapDouble;
				string format = Game.Language.Get("pickitem_" + text2 + StringDefine.Total, "");
				object[] args = array;
				text = PathDefine.Concat(a, wrapDouble, string.Format(format, args));
			}
			else if (!string.IsNullOrEmpty(this.relicBase.exDec))
			{
				text = PathDefine.Concat(text, StringDefine.WrapDouble, string.Format(Game.Language.Get("pickitem_" + text2 + StringDefine.Total, ""), this.relicBase.exDec));
			}
			iconPath = PathDefine.Concat("Bundles/UI/Icon/", this.relicBase.icon);
		}
		if (this.roleBuff != null)
		{
			if (this.roleBuff.isShow)
			{
				text = this.roleBuff.info;
				nameStr = this.roleBuff.buffName;
				if (this.roleBuff.buffName.Equals(StringDefine.ShowForgingData))
				{
					UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
					if (ui == null)
					{
						return;
					}
					ui.ShowForgingData(base.transform.position);
					return;
				}
			}
			else
			{
				text = Game.Language.Get(this.roleBuff.buffName + "_m", "");
				nameStr = Game.Language.Get(this.roleBuff.buffName, "");
			}
			quality = -1;
			iconPath = PathDefine.Concat("Bundles/UI/Icon/", this.roleBuff.icon);
		}
		UI_DecTip.TipInfo tipInfo = new UI_DecTip.TipInfo
		{
			nameStr = nameStr,
			info = text,
			iconPath = iconPath,
			showPos = base.transform.position - new Vector3(0f, 20f, 0f),
			quality = quality,
			isRelic = isRelic,
			exInfo = exInfo
		};
		UI_DecTip ui2 = Game.UI.GetUI<UI_DecTip>();
		if (ui2 == null)
		{
			return;
		}
		ui2.ShowTipInfo(true, tipInfo);
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00016A18 File Offset: 0x00014C18
	public void OnPointerExit(PointerEventData eventData)
	{
		UGUIManager ui = Game.UI;
		if (ui == null)
		{
			return;
		}
		UI_DecTip ui2 = ui.GetUI<UI_DecTip>();
		if (ui2 == null)
		{
			return;
		}
		ui2.ShowTipInfo(false, default(UI_DecTip.TipInfo));
	}

	// Token: 0x04000365 RID: 869
	public RelicBase relicBase;

	// Token: 0x04000366 RID: 870
	public RoleBuff roleBuff;
}
