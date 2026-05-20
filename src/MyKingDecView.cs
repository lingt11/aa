using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200033A RID: 826
public class MyKingDecView : MonoBehaviour
{
	// Token: 0x060012E5 RID: 4837 RVA: 0x00070A74 File Offset: 0x0006EC74
	private void Awake()
	{
		this.startSkillRectTransformPos = this.skillRectTransform.anchoredPosition;
		this.startEquipRectTransformPos = this.equipRectTransform.anchoredPosition;
		this.startEquipGridSpacing = this.equipGridLayoutGroup.spacing;
		this.uiKingDecRelicList.Add(this.uiKingDecRelicTemp);
		this.uiKingDecCardList.Add(this.uiKingDecCardTemp);
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x00070AD8 File Offset: 0x0006ECD8
	public void SetPlayKingData(SaveLoadManager.PlayerKingData playerKingData)
	{
		this.uiRankHeroItem.SetHeroHead(playerKingData);
		this.titleText.text = PathDefine.Concat(Game.Language.Get("玩家", ""), StringDefine.ColonSpace, playerKingData.kingName);
		this.UpdateSkill(playerKingData);
		this.UpdateEquip(playerKingData);
		this.UpdateRelic(playerKingData);
		this.UpdateCard(playerKingData);
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x00070B3C File Offset: 0x0006ED3C
	private void UpdateSkill(SaveLoadManager.PlayerKingData playerKingData)
	{
		SaveLoadManager.PlayerKingSkillData[] skill = playerKingData.skill;
		int num = 0;
		if (skill != null && skill.Length == 5)
		{
			this.exSkillGo.gameObject.SetActive(true);
			this.skillRectTransform.localScale = Vector3.one * 0.85f;
			this.skillRectTransform.anchoredPosition = new Vector2(this.exSkillRectTransformPosX, this.startSkillRectTransformPos.y);
		}
		else
		{
			this.exSkillGo.gameObject.SetActive(false);
			this.skillRectTransform.localScale = Vector3.one;
			this.skillRectTransform.anchoredPosition = this.startSkillRectTransformPos;
		}
		if (skill != null)
		{
			num = skill.Length;
		}
		for (int i = 0; i < this.skillImage.Length; i++)
		{
			if (i < num)
			{
				this.SetSkillSprite(skill[i].skillName, this.skillImage[i], this.skillName[i]);
				this.skillImage[i].gameObject.SetActive(true);
				this.skillName[i].gameObject.SetActive(true);
			}
			else
			{
				this.skillImage[i].gameObject.SetActive(false);
				this.skillName[i].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x00070C6C File Offset: 0x0006EE6C
	private void SetSkillSprite(string skillKey, Image setSkillImg, Text setSkillText)
	{
		string[] array = skillKey.Split("_", StringSplitOptions.None);
		if (array[0].Equals("a"))
		{
			ActiveSkillEnum key = (ActiveSkillEnum)int.Parse(array[1]);
			ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[key];
			string text = activeSkillData.icon;
			if (GameHelperClient.isSaveHero && activeSkillData.isSaveMode)
			{
				text = PathDefine.Concat(text, StringDefine.SaveMode);
			}
			setSkillImg.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Skill/" + text);
			setSkillText.text = Game.Language.Get("a_" + array[1], "");
			return;
		}
		string text2 = array[1];
		Dictionary<string, object> dictionary = (Dictionary<string, object>)ExcelManager.allExcelData["passsiveSkill"].DIC(text2);
		if (dictionary == null)
		{
			setSkillText.text = text2;
			return;
		}
		string text3 = dictionary.DIC("icon");
		bool flag = dictionary.DIC("saveMode");
		if (GameHelperClient.isSaveHero && flag)
		{
			text3 = PathDefine.Concat(text3, StringDefine.SaveMode);
		}
		setSkillImg.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Skill/" + text3);
		setSkillText.text = Game.Language.Get("p_" + array[1], "");
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x00070DA4 File Offset: 0x0006EFA4
	private void UpdateEquip(SaveLoadManager.PlayerKingData playerKingData)
	{
		SaveLoadManager.PlayerKingEquipData[] equip = playerKingData.equip;
		int num = 0;
		if (equip != null && equip.Length == 7)
		{
			this.exEquipGo.gameObject.SetActive(true);
			this.equipRectTransform.anchoredPosition = new Vector2(this.exEquipRectTransformPosX, this.startEquipRectTransformPos.y);
			this.equipGridLayoutGroup.spacing = new Vector2(this.exEquipGridSpacingX, this.startEquipGridSpacing.y);
		}
		else
		{
			this.exEquipGo.gameObject.SetActive(false);
			this.equipRectTransform.anchoredPosition = this.startEquipRectTransformPos;
			this.equipGridLayoutGroup.spacing = this.startEquipGridSpacing;
		}
		if (equip != null)
		{
			num = equip.Length;
		}
		for (int i = 0; i < this.equipImage.Length; i++)
		{
			if (i < num)
			{
				this.SetEquipSprite(equip[i].equip, this.equipImage[i]);
				this.equipImage[i].gameObject.SetActive(true);
			}
			else
			{
				this.equipImage[i].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x00070EAC File Offset: 0x0006F0AC
	private void SetEquipSprite(string equipKey, Image setEquipImg)
	{
		string str = ExcelManager.allExcelData["equipment"].DIC(equipKey).DIC("equipmentIcon");
		setEquipImg.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Shop/" + str);
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x00070EF0 File Offset: 0x0006F0F0
	private void UpdateRelic(SaveLoadManager.PlayerKingData playerKingData)
	{
		SaveLoadManager.PlayerKingRelicData[] relic = playerKingData.relic;
		int num = 0;
		if (relic != null)
		{
			num = relic.Length;
		}
		int num2 = 0;
		int count = this.uiKingDecRelicList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i += 2)
			{
				UIKingDecRelic uikingDecRelic;
				if (num2 < count)
				{
					uikingDecRelic = this.uiKingDecRelicList[num2];
				}
				else
				{
					uikingDecRelic = Object.Instantiate<UIKingDecRelic>(this.uiKingDecRelicTemp, this.uiKingDecRelicTemp.transform.parent);
					uikingDecRelic.transform.SetSiblingIndex(this.uiKingDecRelicTemp.transform.GetSiblingIndex() + num2);
					this.uiKingDecRelicList.Add(uikingDecRelic);
				}
				if (!uikingDecRelic.gameObject.activeSelf)
				{
					uikingDecRelic.gameObject.SetActive(true);
				}
				if (i + 1 < num)
				{
					uikingDecRelic.SetRelic(relic[i], relic[i + 1]);
				}
				else
				{
					uikingDecRelic.SetRelic(relic[i], default(SaveLoadManager.PlayerKingRelicData));
				}
				num2++;
			}
		}
		if (count > num2)
		{
			for (int j = num2; j < count; j++)
			{
				UIKingDecRelic uikingDecRelic2 = this.uiKingDecRelicList[j];
				if (uikingDecRelic2.gameObject.activeSelf)
				{
					uikingDecRelic2.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x0007102C File Offset: 0x0006F22C
	private void UpdateCard(SaveLoadManager.PlayerKingData playerKingData)
	{
		int[] card = playerKingData.card;
		int num = 0;
		if (card != null)
		{
			num = card.Length;
		}
		int num2 = 0;
		int count = this.uiKingDecCardList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i += 2)
			{
				UIKingDecCard uikingDecCard;
				if (num2 < count)
				{
					uikingDecCard = this.uiKingDecCardList[num2];
				}
				else
				{
					uikingDecCard = Object.Instantiate<UIKingDecCard>(this.uiKingDecCardTemp, this.uiKingDecCardTemp.transform.parent);
					uikingDecCard.transform.SetSiblingIndex(this.uiKingDecCardTemp.transform.GetSiblingIndex() + num2);
					this.uiKingDecCardList.Add(uikingDecCard);
				}
				if (!uikingDecCard.gameObject.activeSelf)
				{
					uikingDecCard.gameObject.SetActive(true);
				}
				if (i + 1 < num)
				{
					uikingDecCard.SetCard(card[i], card[i + 1]);
				}
				else
				{
					uikingDecCard.SetCard(card[i], -1);
				}
				num2++;
			}
		}
		if (count > num2)
		{
			for (int j = num2; j < count; j++)
			{
				UIKingDecCard uikingDecCard2 = this.uiKingDecCardList[j];
				if (uikingDecCard2.gameObject.activeSelf)
				{
					uikingDecCard2.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x04001132 RID: 4402
	public Text titleText;

	// Token: 0x04001133 RID: 4403
	public UIRankHeroItem uiRankHeroItem;

	// Token: 0x04001134 RID: 4404
	[Header("技能")]
	public RectTransform skillRectTransform;

	// Token: 0x04001135 RID: 4405
	public Image[] skillImage;

	// Token: 0x04001136 RID: 4406
	public Text[] skillName;

	// Token: 0x04001137 RID: 4407
	public GameObject exSkillGo;

	// Token: 0x04001138 RID: 4408
	private Vector2 startSkillRectTransformPos;

	// Token: 0x04001139 RID: 4409
	private readonly float exSkillRectTransformPosX = -32.5f;

	// Token: 0x0400113A RID: 4410
	[Header("装备")]
	public RectTransform equipRectTransform;

	// Token: 0x0400113B RID: 4411
	public GridLayoutGroup equipGridLayoutGroup;

	// Token: 0x0400113C RID: 4412
	public Image[] equipImage;

	// Token: 0x0400113D RID: 4413
	public GameObject exEquipGo;

	// Token: 0x0400113E RID: 4414
	private Vector2 startEquipRectTransformPos;

	// Token: 0x0400113F RID: 4415
	private readonly float exEquipRectTransformPosX = 4f;

	// Token: 0x04001140 RID: 4416
	private Vector2 startEquipGridSpacing;

	// Token: 0x04001141 RID: 4417
	private readonly float exEquipGridSpacingX = 5f;

	// Token: 0x04001142 RID: 4418
	[Header("遗物")]
	public UIKingDecRelic uiKingDecRelicTemp;

	// Token: 0x04001143 RID: 4419
	private readonly List<UIKingDecRelic> uiKingDecRelicList = new List<UIKingDecRelic>();

	// Token: 0x04001144 RID: 4420
	[Header("卡牌")]
	public UIKingDecCard uiKingDecCardTemp;

	// Token: 0x04001145 RID: 4421
	private readonly List<UIKingDecCard> uiKingDecCardList = new List<UIKingDecCard>();
}
