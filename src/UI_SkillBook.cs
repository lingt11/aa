using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003B0 RID: 944
public class UI_SkillBook : UGUICtrl
{
	// Token: 0x06001598 RID: 5528 RVA: 0x000865FE File Offset: 0x000847FE
	public UI_SkillBook()
	{
		this.selfView = new UI_SkillBook_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_skillBook", base.GetType());
		this.InitData();
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x0008663C File Offset: 0x0008483C
	private void InitData()
	{
		foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)ExcelManager.allExcelData["activeSkill"]))
		{
			string key = keyValuePair.Value.DIC("quality");
			if (!this.allSkillData.ContainsKey(key))
			{
				this.allSkillData[key] = new List<object>();
			}
			this.allSkillData[key].Add(keyValuePair.Value);
		}
		foreach (KeyValuePair<string, object> keyValuePair2 in ((Dictionary<string, object>)ExcelManager.allExcelData["passsiveSkill"]))
		{
			string key2 = keyValuePair2.Value.DIC("quality");
			if (!this.allSkillData.ContainsKey(key2))
			{
				this.allSkillData[key2] = new List<object>();
			}
			this.allSkillData[key2].Add(keyValuePair2.Value);
		}
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x00086778 File Offset: 0x00084978
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.selfView.pool_btnSkill.RemoveAllView();
		this.selfView.pool_content.RemoveAllView();
		using (Dictionary<string, List<object>>.Enumerator enumerator = this.allSkillData.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, List<object>> one = enumerator.Current;
				GameObject gameObject = this.selfView.pool_btnSkill.AddView();
				gameObject.transform.GetChild(0).GetComponent<Text>().text = one.Key;
				gameObject.GetComponent<Button>().AddButtonEvent(delegate
				{
					this.ShowSkillList(one.Value);
				});
			}
		}
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x00086844 File Offset: 0x00084A44
	private void ShowSkillList(List<object> list)
	{
		this.selfView.pool_content.RemoveAllView();
		for (int i = 0; i < list.Count; i++)
		{
			string text = list[i].DIC("name");
			if (((Dictionary<string, object>)list[i]).ContainsKey("damageType"))
			{
				GameObject gameObject = this.selfView.pool_content.AddView();
				text = "主:" + text;
				int id = list[i].DIC("id");
				gameObject.transform.GetChild(0).GetComponent<Text>().text = text;
				gameObject.GetComponent<Button>().AddButtonEvent(delegate
				{
					ActiveSkillEnum id = (ActiveSkillEnum)id;
					GameHelperClient.localPlayer.AddActiveSkillBook(id, null);
				});
			}
			else
			{
				if (!list[i].DIC("lock"))
				{
					GameObject gameObject2 = this.selfView.pool_content.AddView();
					text = "被:" + text;
					string id = list[i].DIC("id");
					gameObject2.transform.GetChild(0).GetComponent<Text>().text = text;
					gameObject2.GetComponent<Button>().AddButtonEvent(delegate
					{
						GameHelperClient.localPlayer.AddPasssiveSkillBook((PasssiveSkillEnum)int.Parse(id), null);
					});
				}
			}
		}
	}

	// Token: 0x04001456 RID: 5206
	public UI_SkillBook_View selfView;

	// Token: 0x04001457 RID: 5207
	private Dictionary<string, List<object>> allSkillData = new Dictionary<string, List<object>>();
}
