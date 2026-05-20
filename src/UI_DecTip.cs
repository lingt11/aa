using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x0200030F RID: 783
public class UI_DecTip : UGUICtrl
{
	// Token: 0x06001223 RID: 4643 RVA: 0x0006B8C7 File Offset: 0x00069AC7
	public UI_DecTip()
	{
		this.selfView = new UI_DecTip_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_decTip", base.GetType());
	}

	// Token: 0x06001224 RID: 4644 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x0006B8F4 File Offset: 0x00069AF4
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.selfView.trans_SkillDetail.gameObject.SetActive(false);
		this.selfView.trans_forging.gameObject.SetActive(false);
		this.trans_roleAttributeCanvas = this.selfView.trans_roleAttribute.GetComponent<CanvasGroup>();
		this.trans_roleAttributeCanvas.alpha = 0f;
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ClosePanel()
	{
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x0006B95C File Offset: 0x00069B5C
	public void RefreshBaoJi()
	{
		if (!this.isOpenRole)
		{
			return;
		}
		string str = Game.Language.Get("exs", "");
		string str2 = Game.Language.Get("gdj", "");
		string str3 = Game.Language.Get("baoji", "");
		string str4 = Game.Language.Get("baojiDamage", "");
		string str5 = Game.Language.Get("attackSpeed", "");
		this.selfView.ltext_hpPercent.text = string.Format("{0}:{1}%", Game.Language.Get("最大生命值提升", ""), Mathf.RoundToInt(GameHelperClient.localPlayer.maxHpAddPercent * 100f));
		float num = 1f - Util.GetArmorLevel(GameHelperClient.localPlayer.FinalSkillReduction);
		this.selfView.ltext_skillHit.text = Game.Language.Get("技能抵抗", "") + ":" + GameHelperClient.localPlayer.FinalSkillReduction.ToString() + string.Format("({0}:{1:F1}%)", Game.Language.Get("技能伤害减免", ""), num * 100f);
		this.selfView.ltext_forgingAdd.text = string.Format("{0}:{1}%", Game.Language.Get("属性锻造器增幅", ""), Mathf.RoundToInt(EntityStatic.Get<ShopManager>().forgingManager.forgingAdd * 100f));
		this.selfView.ltext_exDamage.text = str + ":" + GameHelperClient.localPlayer.extraDamage.ToString();
		this.selfView.ltext_reduce.text = str2 + ":" + GameHelperClient.localPlayer.reduceInjury.ToString();
		this.selfView.ltext_baoji.text = str3 + ":" + Mathf.RoundToInt(GameHelperClient.localPlayer.critical * 100f).ToString() + "%";
		this.selfView.ltext_baojiDamage.text = str4 + ":" + Mathf.RoundToInt(GameHelperClient.localPlayer.criticalDamage * 100f).ToString() + "%";
		this.selfView.ltext_attackSpeed.text = str5 + ":" + Math.Round((double)GameHelperClient.localPlayer.GetAttackSpeed(), 2).ToString();
		this.selfView.ltext_xixue.text = string.Format("{0}:{1}", Game.Language.Get("xixue", ""), GameHelperClient.localPlayer.xiXue);
		this.selfView.ltext_xixueRate.text = string.Format("{0}:{1}%", Game.Language.Get("攻击生命偷取", ""), Mathf.RoundToInt(GameHelperClient.localPlayer.XiXueLvAll * 100f));
		this.selfView.ltext_normalAddDamage.text = string.Format("{0}:{1}%", Game.Language.Get("物理伤害加成", ""), Mathf.RoundToInt(GameHelperClient.localPlayer.normalAttackAddDamage * 100f));
		this.selfView.ltext_normalShieldDamage.text = string.Format("{0}:{1}%", Game.Language.Get("物理破盾加成", ""), Mathf.RoundToInt(GameHelperClient.localPlayer.normalBreakShield * 100f));
		this.selfView.ltext_coolCd.text = string.Format("{0}:{1}", Game.Language.Get("技能急速", ""), GameHelperClient.localPlayer.AllSkillCd) + string.Format("({0}:{1:F1}%)", Game.Language.Get("冷却时间缩减", ""), (1f - Util.GetCdReduce(GameHelperClient.localPlayer.AllSkillCd)) * 100f);
		this.selfView.ltext_skillAddDamage.text = string.Format("{0}:{1}%", Game.Language.Get("法术伤害加成", ""), Mathf.RoundToInt(GameHelperClient.localPlayer.SkillExDamageAll * 100f));
		this.selfView.ltext_skillShiledDamage.text = string.Format("{0}:{1}%", Game.Language.Get("法术破盾伤害", ""), Mathf.RoundToInt(GameHelperClient.localPlayer.skillBreakShield * 100f));
		this.selfView.ltext_buffAddDamage.text = string.Format("{0}:{1}%", Game.Language.Get("BUFF伤害加成", ""), Mathf.RoundToInt(GameHelperClient.localPlayer.buffAddDamage * 100f));
		this.selfView.ltext_effectAddDamage.text = string.Format("{0}:{1}%", Game.Language.Get("攻击特效加成", ""), Mathf.RoundToInt(GameHelperClient.localPlayer.addAttackEffectDamage * 100f));
		this.selfView.ltext_allAddDamage.text = string.Format("{0}:{1}%", Game.Language.Get("总伤害加成", ""), Mathf.RoundToInt(GameHelperClient.localPlayer.addDamagePercent * 100f));
		float num2 = 1f - Util.GetArmorLevel(GameHelperClient.localPlayer.FinalDoge);
		this.selfView.ltext_doge.text = Game.Language.Get("闪避值", "") + ":" + GameHelperClient.localPlayer.FinalDoge.ToString() + string.Format("({0}:{1:F1}%)", Game.Language.Get("闪避率", ""), num2 * 100f);
		float luckAddValue = Util.GetLuckAddValue(GameHelperClient.localPlayer.lucky);
		this.selfView.ltext_lucky.text = string.Format("{0}:{1}", Game.Language.Get("幸运值", ""), GameHelperClient.localPlayer.lucky) + string.Format("({0}:{1:F1}%)", Game.Language.Get("幸运值提示", ""), luckAddValue * 100f);
		this.selfView.ltext_hpAdd.text = string.Format("{0}:{1} | {2:F1}%", Game.Language.Get("hpAddSec", ""), GameHelperClient.localPlayer.hpAddSec, GameHelperClient.localPlayer.hpAddSecRate * 100f);
		this.selfView.ltext_mpAdd.text = string.Format("{0}:{1}", Game.Language.Get("mpAddSec", ""), GameHelperClient.localPlayer.mpAddSecRate);
		this.selfView.ltext_moveSpeed.text = string.Format("{0}:{1:F1}", Game.Language.Get("moveSpeed", ""), GameHelperClient.localPlayer.GetMoveSpeed());
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x0006C0CC File Offset: 0x0006A2CC
	public void ShowTipInfo(bool show, UI_DecTip.TipInfo tipInfo)
	{
		if (!show)
		{
			this.selfView.trans_SkillDetail.gameObject.SetActive(false);
			this.selfView.trans_forging.gameObject.SetActive(false);
			return;
		}
		this.selfView.transform.SetAsLastSibling();
		this.selfView.trans_SkillDetail.gameObject.SetActive(true);
		this.selfView.trans_forging.gameObject.SetActive(false);
		this.selfView.trans_SkillDetail.position = tipInfo.showPos;
		this.selfView.trans_SkillDetail.GetComponent<SkillDetail>().ShowInfo(tipInfo);
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x0006C174 File Offset: 0x0006A374
	public void RefreshPlayerStateUI()
	{
		if (!this.isOpenRole)
		{
			return;
		}
		string str = GameHelperClient.localPlayer.FinalAttackPower.ToString();
		string str2 = GameHelperClient.localPlayer.armor.ToString();
		string str3 = Game.Language.Get("attack", "");
		string str4 = Game.Language.Get("armor", "");
		this.selfView.ltext_attack.text = str3 + ":" + str;
		float num = 1f - Util.GetArmorLevel(GameHelperClient.localPlayer.armor);
		this.selfView.ltext_armor.text = str4 + ":" + str2 + string.Format("({0}:{1:F1}%)", Game.Language.Get("减伤率", ""), num * 100f);
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x0006C258 File Offset: 0x0006A458
	public void StartOpen()
	{
		this.selfView.transform.SetAsLastSibling();
		this.isOpenRole = true;
		this.selfView.trans_roleAttribute.GetComponent<CanvasGroup>().DOFade(1f, 0.1f);
		this.RefreshPlayerStateUI();
		this.RefreshBaoJi();
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x0006C2A8 File Offset: 0x0006A4A8
	public void StartClose()
	{
		this.isOpenRole = false;
		this.selfView.trans_roleAttribute.GetComponent<CanvasGroup>().DOFade(0f, 0.1f);
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x0006C2D4 File Offset: 0x0006A4D4
	public void ShowForgingData(Vector3 position)
	{
		this.selfView.transform.SetAsLastSibling();
		this.selfView.trans_SkillDetail.gameObject.SetActive(false);
		this.selfView.trans_forging.gameObject.SetActive(true);
		this.selfView.trans_forging.position = position;
		this.selfView.trans_forging.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0f, 60f);
		string text = "";
		ForgingManager forgingManager = EntityStatic.Get<ShopManager>().forgingManager;
		text += PathDefine.Concat(Game.Language.Get("str", ""), StringDefine.ColonSpace, forgingManager.AllStr);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("sta", ""), StringDefine.ColonSpace, forgingManager.AllSta);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("dex", ""), StringDefine.ColonSpace, forgingManager.AllAgi);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("attack", ""), StringDefine.ColonSpace, forgingManager.AllAttack);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("生命值", ""), StringDefine.ColonSpace, forgingManager.AllHP);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("法力值", ""), StringDefine.ColonSpace, forgingManager.AllMP);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("armor", ""), StringDefine.ColonSpace, forgingManager.AllArmor);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("hpAddSec", ""), StringDefine.ColonSpace, forgingManager.AllHPSec);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("mpAddSec", ""), StringDefine.ColonSpace, forgingManager.AllMPSec);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("attackSpeed", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllAttackSpeed));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("物理破盾加成", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllNormalBreak));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("法术破盾伤害", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllSkillBreak));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("baoji", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllCritical));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("幸运值", ""), StringDefine.ColonSpace, forgingManager.AllLuck);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("baojiDamage", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllCriticalDamage));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("攻击生命偷取", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllXiXueRate));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("物理伤害加成", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllNormalAdd));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("法术伤害加成", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllSkillAdd));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("xixue", ""), StringDefine.ColonSpace, forgingManager.AllXiXue);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("技能急速", ""), StringDefine.ColonSpace, forgingManager.AllCoolDown);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("gdj", ""), StringDefine.ColonSpace, forgingManager.AllReduceInjury);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("exs", ""), StringDefine.ColonSpace, forgingManager.AllExtraDamage);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("总伤害加成", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllAddDamage));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("闪避值", ""), StringDefine.ColonSpace, forgingManager.AllDoge);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("moveSpeed", ""), StringDefine.ColonSpace, forgingManager.AllMoveSpeed.ToString("F1"));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("最大生命值提升", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllHpPercent));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("hpAddSec", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllHpSecRate));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("技能抵抗", ""), StringDefine.ColonSpace, forgingManager.AllSkillHit);
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("经验获取", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllExpAdd));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("召唤物强度", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllSummonAdd));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("变身强度", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllHenshinAdd));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("BUFF伤害加成", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllHaloAdd));
		text += PathDefine.Concat(StringDefine.Wrap, Game.Language.Get("武装伤害", ""), StringDefine.ColonSpace, Util.GetPercentData(forgingManager.AllArmedAdd));
		this.selfView.ltext_forging.text = text;
	}

	// Token: 0x04001051 RID: 4177
	public UI_DecTip_View selfView;

	// Token: 0x04001052 RID: 4178
	private CanvasGroup trans_roleAttributeCanvas;

	// Token: 0x04001053 RID: 4179
	private bool isOpenRole;

	// Token: 0x02000310 RID: 784
	public struct TipInfo
	{
		// Token: 0x04001054 RID: 4180
		public string nameStr;

		// Token: 0x04001055 RID: 4181
		public string info;

		// Token: 0x04001056 RID: 4182
		public string iconPath;

		// Token: 0x04001057 RID: 4183
		public Vector3 showPos;

		// Token: 0x04001058 RID: 4184
		public int quality;

		// Token: 0x04001059 RID: 4185
		public bool isRelic;

		// Token: 0x0400105A RID: 4186
		public string exInfo;

		// Token: 0x0400105B RID: 4187
		public float cd;
	}
}
