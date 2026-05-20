using System;
using System.Collections.Generic;

// Token: 0x020000F1 RID: 241
public class EquipSkillOldSword : EquipEventBase
{
	// Token: 0x060004F9 RID: 1273 RVA: 0x0001DA1A File Offset: 0x0001BC1A
	public override void Init(EquipBase equipBaseValue)
	{
		base.Init(equipBaseValue);
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x0001DA23 File Offset: 0x0001BC23
	public override void Clear()
	{
		base.Clear();
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x0001DA2C File Offset: 0x0001BC2C
	public override void OnLevelUpSuccess()
	{
		base.OnLevelUpSuccess();
		string nextEquipIndex;
		if (this.strengLevel < this.equipBase.maxLevel || !EquipSkillOldSword.EvolutionMap.TryGetValue(this.equipBase.equipIndex, out nextEquipIndex))
		{
			return;
		}
		UI_EquipStreng ui = Game.UI.GetUI<UI_EquipStreng>();
		if (ui != null && ui.isOpen)
		{
			Game.UI.CloseUI<UI_EquipStreng>();
		}
		List<EquipEvolutionEntryData> evolutionEntries = EquipEvolutionEntryData.GetRandomOptions(this.equipBase, 3, nextEquipIndex);
		if (evolutionEntries.Count == 0)
		{
			return;
		}
		if (!Util.CheckCanRoguelike())
		{
			this.EvolveEquip(nextEquipIndex, evolutionEntries[0]);
			return;
		}
		RoguelikeUIData[] array = new RoguelikeUIData[evolutionEntries.Count];
		string evolutionIcon = this.GetEvolutionIcon(nextEquipIndex);
		for (int i = 0; i < evolutionEntries.Count; i++)
		{
			array[i] = evolutionEntries[i].CreateRoguelikeUIData(evolutionIcon, i.ToString());
		}
		UI_Roguelike ui_Roguelike = Game.UI.OpenUI<UI_Roguelike>(null) as UI_Roguelike;
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/装备进化", 1f, 3f);
		ui_Roguelike.ShowRoguelike(array, delegate(RoguelikeUIData roguelikeData)
		{
			int index = int.Parse(roguelikeData.data);
			this.EvolveEquip(nextEquipIndex, evolutionEntries[index]);
		}, Game.Language.Get("装备进化", ""), null, null, 0f, null, "equip_evolution");
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x0001DB9C File Offset: 0x0001BD9C
	private string GetEvolutionIcon(string equipIndex)
	{
		string str = ExcelManager.allExcelData["equipment"].DIC(equipIndex).DIC("equipmentIcon");
		return "Bundles/UI/Icon/Shop/" + str;
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x0001DBD4 File Offset: 0x0001BDD4
	private void EvolveEquip(string nextEquipIndex, EquipEvolutionEntryData evolutionEntry)
	{
		List<EquipEvolutionEntryData> list = new List<EquipEvolutionEntryData>();
		if (this.equipBase.evolutionEntryList != null)
		{
			list.AddRange(EquipEvolutionEntryData.GetUpdatedEntries(this.equipBase.evolutionEntryList, nextEquipIndex));
		}
		if (evolutionEntry != null)
		{
			list.Add(evolutionEntry);
		}
		this.playerBase.playerAttribute.SellEquip(this.equipBase, true);
		ShopManager.OnBuyEquipSuccess("equip_" + nextEquipIndex, 0, list);
	}

	// Token: 0x04000475 RID: 1141
	private static readonly Dictionary<string, string> EvolutionMap = new Dictionary<string, string>
	{
		{
			"123",
			"1003"
		},
		{
			"1003",
			"1004"
		},
		{
			"1004",
			"1005"
		},
		{
			"1005",
			"1006"
		},
		{
			"124",
			"1007"
		},
		{
			"1007",
			"1008"
		},
		{
			"1008",
			"1009"
		},
		{
			"1009",
			"1010"
		},
		{
			"125",
			"1011"
		},
		{
			"1011",
			"1012"
		},
		{
			"1012",
			"1013"
		},
		{
			"1013",
			"1014"
		}
	};
}
