using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000257 RID: 599
public class SkillUITouch : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06000ABE RID: 2750 RVA: 0x00037124 File Offset: 0x00035324
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.skillBase == null)
		{
			return;
		}
		string exInfo;
		float cd;
		string text = this.skillBase.GetSkillInfo(out exInfo, out cd);
		if (!string.IsNullOrEmpty(this.skillBase.totalName))
		{
			int num = this.skillBase.totals.Length;
			string[] array = new string[num];
			for (int i = 0; i < num; i++)
			{
				bool flag = false;
				if (this.skillBase.isTotalsPercent != null)
				{
					flag = this.skillBase.isTotalsPercent[i];
				}
				array[i] = string.Format(ColorDefine.NormalColor, flag ? PathDefine.Concat(this.skillBase.totals[i], StringDefine.Percent) : this.skillBase.totals[i]);
			}
			object a = text;
			object wrapDouble = StringDefine.WrapDouble;
			string totalName = this.skillBase.totalName;
			object[] args = array;
			text = PathDefine.Concat(a, wrapDouble, string.Format(totalName, args));
		}
		if (!string.IsNullOrEmpty(this.skillBase.exDec))
		{
			text = PathDefine.Concat(text, StringDefine.WrapDouble, this.skillBase.exDec);
		}
		string text2 = this.skillBase.languageName;
		if (this.skillBase.level > 0)
		{
			text2 = string.Format("{0}(+{1})", text2, this.skillBase.level);
		}
		UI_DecTip.TipInfo tipInfo = new UI_DecTip.TipInfo
		{
			nameStr = text2,
			info = text,
			iconPath = PathDefine.Concat("Bundles/UI/Icon/Skill/", this.skillBase.iconName),
			showPos = this.skillBase.skillUI.playerStateSkill.transform.position,
			quality = this.skillBase.quality,
			isRelic = false,
			exInfo = exInfo,
			cd = cd
		};
		UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
		if (ui == null)
		{
			return;
		}
		ui.ShowTipInfo(true, tipInfo);
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x00037308 File Offset: 0x00035508
	public void OnPointerExit(PointerEventData eventData)
	{
		UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
		if (ui == null)
		{
			return;
		}
		ui.ShowTipInfo(false, default(UI_DecTip.TipInfo));
	}

	// Token: 0x04000BEE RID: 3054
	public SkillBase skillBase;
}
