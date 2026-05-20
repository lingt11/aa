using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003A4 RID: 932
public class EquipStrengUI : MonoBehaviour
{
	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x06001546 RID: 5446 RVA: 0x00083894 File Offset: 0x00081A94
	public ItemType StrengItemType
	{
		get
		{
			return this.strengItemType;
		}
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x0008389C File Offset: 0x00081A9C
	private void Awake()
	{
		this.strengBtn.AddButtonEvent(new UnityAction(this.OnStrengBtnClick));
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x000838B8 File Offset: 0x00081AB8
	private void OnStrengBtnClick()
	{
		if (this.curEquipBase == null)
		{
			return;
		}
		if (!ShopManager.CanEquipStreng(this.curEquipBase, this.strengItemType))
		{
			Util.ShowTipsNoLanguage(string.Format(ColorDefine.RedForColor, Game.Language.Get("达到最高等级", "")));
			this.UpdateEquipStrengUI();
			return;
		}
		if (this.curEquipBase.level >= this.curEquipBase.maxLevel)
		{
			Util.ShowTipsNoLanguage(string.Format(ColorDefine.RedForColor, Game.Language.Get("达到最高等级", "")));
			return;
		}
		bool flag = true;
		int num = 1;
		if (this.strengItemType == ItemType.None)
		{
			EquipStrengUI.EquipStrengData levelUpData = this.curEquipBase.GetLevelUpData();
			if (levelUpData.gold > GameHelperClient.localPlayer.gold)
			{
				Util.ShowTips("noJin");
				return;
			}
			if (levelUpData.gem > GameHelperClient.localPlayer.gem)
			{
				Util.ShowTips("noTou");
				return;
			}
			if (levelUpData.gold != 0)
			{
				GameHelperClient.localPlayer.AddGold(GameHelperClient.localPlayer.GetHeadUIPos(), -levelUpData.gold, true);
			}
			if (levelUpData.gem != 0)
			{
				GameHelperClient.localPlayer.AddGem(GameHelperClient.localPlayer.GetHeadUIPos(), -levelUpData.gem, false);
			}
			flag = (Random.value < levelUpData.chance + (float)this.curEquipBase.upgradeFailed * 0.05f);
		}
		else
		{
			num = this.itemStrengAddLevel[this.strengItemType - ItemType.EquipAdd_1];
			if (this.curEquipBase.level + num > this.curEquipBase.maxLevel)
			{
				num = this.curEquipBase.maxLevel - this.curEquipBase.level;
			}
		}
		if (!flag)
		{
			EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/强化失败", 1f, 3f);
			this.curEquipBase.upgradeFailed++;
			Util.ShowTipsNoLanguage(PathDefine.Concat(string.Format(ColorDefine.FailColor, this.curEquipBase.name), Game.Language.Get("强化失败", "")));
			return;
		}
		Util.ShowTipsNoLanguage(PathDefine.Concat(string.Format(ColorDefine.NormalColor, this.curEquipBase.name), Game.Language.Get("强化成功", "")));
		this.curEquipBase.OnLevelUpSuccess(false, num);
		if (this.strengItemType == ItemType.None)
		{
			this.UpdateEquipStrengUI();
			return;
		}
		Game.UI.CloseUI<UI_EquipStreng>();
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x00083B10 File Offset: 0x00081D10
	private void OnEnable()
	{
		MySystemEvent.Instance.RegisterMessage<EquipBase>(43, new Action<Body, EquipBase>(this.OnEquipStrengItemSelect));
		if (GameHelperClient.localPlayer != null)
		{
			PlayerBase localPlayer = GameHelperClient.localPlayer;
			localPlayer.onEquipChange = (RoleBase.OnEquipChange)Delegate.Combine(localPlayer.onEquipChange, new RoleBase.OnEquipChange(this.OnEquipChange));
		}
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x00083B68 File Offset: 0x00081D68
	private void OnDisable()
	{
		MySystemEvent.Instance.UnregisterMessage<EquipBase>(43, new Action<Body, EquipBase>(this.OnEquipStrengItemSelect));
		if (GameHelperClient.localPlayer != null)
		{
			PlayerBase localPlayer = GameHelperClient.localPlayer;
			localPlayer.onEquipChange = (RoleBase.OnEquipChange)Delegate.Remove(localPlayer.onEquipChange, new RoleBase.OnEquipChange(this.OnEquipChange));
		}
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x00083BC0 File Offset: 0x00081DC0
	private void OnEquipChange()
	{
		this.UpdateEquipStrengUI();
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x00083BC8 File Offset: 0x00081DC8
	private void OnEquipStrengItemSelect(Body body, EquipBase equipBase)
	{
		int num = this.equipStrengItems.Length;
		int count = this.showEquipList.Count;
		for (int i = 0; i < num; i++)
		{
			if (i < count && this.showEquipList[i] == equipBase)
			{
				this.SetCurEquipBase(equipBase, i);
				return;
			}
		}
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x00083C14 File Offset: 0x00081E14
	private void UpdateShowEquipList()
	{
		this.showEquipList.Clear();
		List<EquipBase> equipList = GameHelperClient.localPlayer.playerAttribute.equipList;
		for (int i = 0; i < equipList.Count; i++)
		{
			EquipBase equipBase = equipList[i];
			if (ShopManager.CanEquipStreng(equipBase, this.strengItemType))
			{
				this.showEquipList.Add(equipBase);
			}
		}
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x00083C70 File Offset: 0x00081E70
	private void UpdateEquipStrengUI()
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		bool flag = this.curEquipBase != null;
		bool flag2 = false;
		int index = 0;
		if (this.curMaxEquip != GameHelperClient.MaxEquipNum)
		{
			this.curMaxEquip = GameHelperClient.MaxEquipNum;
			if (this.curMaxEquip > 6)
			{
				this.gridLayoutGroup.anchoredPosition = new Vector2(0f, this.gridLayoutGroup.anchoredPosition.y);
				this.gridLayoutGroup.sizeDelta = new Vector2(450f, this.gridLayoutGroup.sizeDelta.y);
				for (int i = 6; i < this.curMaxEquip; i++)
				{
					GameObject gameObject = this.equipStrengItems[i].gameObject;
					if (!gameObject.activeSelf)
					{
						gameObject.SetActive(true);
					}
				}
			}
		}
		this.UpdateShowEquipList();
		int num = this.equipStrengItems.Length;
		int count = this.showEquipList.Count;
		for (int j = 0; j < num; j++)
		{
			if (j < count)
			{
				EquipBase equipBase = this.showEquipList[j];
				if (flag && equipBase == this.curEquipBase)
				{
					flag2 = true;
					index = j;
				}
				this.equipStrengItems[j].UpdateUI(equipBase);
			}
			else if (j < GameHelperClient.MaxEquipNum)
			{
				this.equipStrengItems[j].HideSprite();
			}
		}
		if (!flag2)
		{
			this.SetCurEquipBase(null, -1);
			return;
		}
		this.SetCurEquipBase(this.curEquipBase, index);
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x00083DE0 File Offset: 0x00081FE0
	private void SetCurEquipBase(EquipBase equipBase, int index)
	{
		this.curEquipBase = equipBase;
		if (this.curEquipBase == null)
		{
			if (this.selectObj.activeSelf)
			{
				this.selectObj.SetActive(false);
			}
			return;
		}
		if (!this.selectObj.activeSelf)
		{
			this.selectObj.SetActive(true);
		}
		EquipStrengUI.EquipStrengData equipStrengData = default(EquipStrengUI.EquipStrengData);
		this.selectName.text = equipBase.name;
		this.selectImg.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Shop/" + equipBase.iconName);
		int addLevel = 1;
		if (this.curEquipBase.level >= this.curEquipBase.maxLevel)
		{
			this.chanceText.text = string.Format(ColorDefine.RedForColor, Game.Language.Get("达到最高等级", ""));
		}
		else if (this.strengItemType == ItemType.None)
		{
			equipStrengData = this.curEquipBase.GetLevelUpData();
			this.chanceText.text = PathDefine.Concat(Game.Language.Get("成功率：", ""), string.Format(ColorDefine.NormalColor, string.Format("{0:F1}%", equipStrengData.chance * 100f)));
		}
		else
		{
			this.chanceText.text = PathDefine.Concat(Game.Language.Get("成功率：", ""), string.Format(ColorDefine.NormalColor, "100%"));
			addLevel = this.itemStrengAddLevel[this.strengItemType - ItemType.EquipAdd_1];
		}
		this.selectDec.text = equipBase.GetEquipInfo(addLevel);
		this.selectEffect.transform.position = this.equipStrengItems[index].transform.position;
		int gold = equipStrengData.gold;
		int gem = equipStrengData.gem;
		if (gold > 0 || gem == 0)
		{
			this.goldText.text = gold.ToString();
			if (!this.goldObj.activeSelf)
			{
				this.goldObj.SetActive(true);
			}
		}
		else if (this.goldObj.activeSelf)
		{
			this.goldObj.SetActive(false);
		}
		if (gem > 0)
		{
			this.gemText.text = gem.ToString();
			if (!this.gemObj.gameObject.activeSelf)
			{
				this.gemObj.gameObject.SetActive(true);
				return;
			}
		}
		else if (this.gemObj.gameObject.activeSelf)
		{
			this.gemObj.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x0006898B File Offset: 0x00066B8B
	public void HideEquipStrengUI()
	{
		if (base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x00084043 File Offset: 0x00082243
	public void SetStrengItemType(ItemType itemType)
	{
		this.strengItemType = itemType;
		this.UpdateEquipStrengUI();
	}

	// Token: 0x040013F0 RID: 5104
	[SerializeField]
	private EquipStrengItem[] equipStrengItems;

	// Token: 0x040013F1 RID: 5105
	[SerializeField]
	private RectTransform selectEffect;

	// Token: 0x040013F2 RID: 5106
	[SerializeField]
	private GameObject selectObj;

	// Token: 0x040013F3 RID: 5107
	[SerializeField]
	private Image selectImg;

	// Token: 0x040013F4 RID: 5108
	[SerializeField]
	private Text selectDec;

	// Token: 0x040013F5 RID: 5109
	[SerializeField]
	private Text selectName;

	// Token: 0x040013F6 RID: 5110
	[SerializeField]
	private Text chanceText;

	// Token: 0x040013F7 RID: 5111
	[SerializeField]
	private RectTransform gridLayoutGroup;

	// Token: 0x040013F8 RID: 5112
	[SerializeField]
	private Button strengBtn;

	// Token: 0x040013F9 RID: 5113
	[SerializeField]
	private GameObject goldObj;

	// Token: 0x040013FA RID: 5114
	[SerializeField]
	private TextMeshProUGUI goldText;

	// Token: 0x040013FB RID: 5115
	[SerializeField]
	private GameObject gemObj;

	// Token: 0x040013FC RID: 5116
	[SerializeField]
	private TextMeshProUGUI gemText;

	// Token: 0x040013FD RID: 5117
	private int curMaxEquip;

	// Token: 0x040013FE RID: 5118
	private EquipBase curEquipBase;

	// Token: 0x040013FF RID: 5119
	private readonly List<EquipBase> showEquipList = new List<EquipBase>();

	// Token: 0x04001400 RID: 5120
	private ItemType strengItemType = ItemType.None;

	// Token: 0x04001401 RID: 5121
	private int[] itemStrengAddLevel = new int[]
	{
		1,
		2,
		3,
		4,
		5,
		10
	};

	// Token: 0x020003A5 RID: 933
	public struct EquipStrengData
	{
		// Token: 0x04001402 RID: 5122
		public float chance;

		// Token: 0x04001403 RID: 5123
		public int gold;

		// Token: 0x04001404 RID: 5124
		public int gem;
	}
}
