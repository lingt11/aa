using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200037A RID: 890
public class UI_RelicTool : UGUICtrl
{
	// Token: 0x0600145F RID: 5215 RVA: 0x0007EDA3 File Offset: 0x0007CFA3
	public UI_RelicTool()
	{
		this.selfView = new UI_RelicTool_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_relicTool", base.GetType());
		this.InitData();
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x0007EDE0 File Offset: 0x0007CFE0
	private void InitData()
	{
		foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)ExcelManager.allExcelData["remains"]))
		{
			if (!this.relicData.ContainsKey("remains"))
			{
				this.relicData["remains"] = new List<object>();
			}
			this.relicData["remains"].Add(keyValuePair.Value);
		}
		object dic = ExcelManager.allExcelData["equipment"];
		for (int i = 0; i < 999; i++)
		{
			string key = (100 + i + 1).ToString();
			if (!this.relicData.ContainsKey("equipment"))
			{
				this.relicData["equipment"] = new List<object>();
			}
			if (dic.DIC(key) == null)
			{
				break;
			}
			this.relicData["equipment"].Add(dic.DIC(key));
		}
	}

	// Token: 0x06001461 RID: 5217 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x0007EEFC File Offset: 0x0007D0FC
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.selfView.pool_btnSkill.RemoveAllView();
		this.selfView.pool_content.RemoveAllView();
		string text = "遗物";
		using (Dictionary<string, List<object>>.Enumerator enumerator = this.relicData.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, List<object>> one = enumerator.Current;
				GameObject gameObject = this.selfView.pool_btnSkill.AddView();
				gameObject.transform.GetChild(0).GetComponent<Text>().text = text;
				string type = text;
				gameObject.GetComponent<Button>().AddButtonEvent(delegate
				{
					this.ShowSkillList(type, one.Value);
				});
				text = "神器";
			}
		}
		this.ShowSkillList("遗物", this.relicData["remains"]);
	}

	// Token: 0x06001463 RID: 5219 RVA: 0x0007EFEC File Offset: 0x0007D1EC
	private void ShowSkillList(string type, List<object> list)
	{
		this.selfView.pool_content.RemoveAllView();
		if (type.Equals("遗物"))
		{
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i].DIC("name");
				GameObject gameObject = this.selfView.pool_content.AddView();
				int id = list[i].DIC("id");
				gameObject.transform.GetChild(0).GetComponent<Text>().text = text;
				gameObject.GetComponent<Button>().AddButtonEvent(delegate
				{
					Util.OnLocalPlayerPickItem((ItemType)id, 0);
				});
			}
			return;
		}
		if (type.Equals("神器"))
		{
			for (int j = 0; j < list.Count; j++)
			{
				string text2 = list[j].DIC("name");
				GameObject gameObject2 = this.selfView.pool_content.AddView();
				string id = list[j].DIC("equipmentIcon");
				gameObject2.transform.GetChild(0).GetComponent<Text>().text = text2;
				gameObject2.GetComponent<Button>().AddButtonEvent(delegate
				{
					ShopManager.OnBuyEquipSuccess(id, 0, null);
				});
			}
		}
	}

	// Token: 0x04001310 RID: 4880
	public UI_RelicTool_View selfView;

	// Token: 0x04001311 RID: 4881
	private Dictionary<string, List<object>> relicData = new Dictionary<string, List<object>>();
}
