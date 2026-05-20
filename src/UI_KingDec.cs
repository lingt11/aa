using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200033D RID: 829
public class UI_KingDec : UGUICtrl
{
	// Token: 0x060012F2 RID: 4850 RVA: 0x000714DB File Offset: 0x0006F6DB
	public UI_KingDec()
	{
		this.selfView = new UI_KingDec_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_kingDec", base.GetType());
		this.myKingDecView = this.selfView.gameObject.GetComponent<MyKingDecView>();
	}

	// Token: 0x060012F3 RID: 4851 RVA: 0x0007151B File Offset: 0x0006F71B
	public override void Update()
	{
		base.Update();
		if (Input.GetKeyDown(KeyCode.Escape) && this.isOpen)
		{
			base.CloseSelfPanel();
		}
	}

	// Token: 0x060012F4 RID: 4852 RVA: 0x0007153A File Offset: 0x0006F73A
	protected override void ButtonAddClick()
	{
		this.selfView.btn_back.AddButtonEvent(new UnityAction(base.CloseSelfPanel));
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x0006DDD3 File Offset: 0x0006BFD3
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x00071558 File Offset: 0x0006F758
	public void SetPlayKingData(SaveLoadManager.PlayerKingData playerKingData)
	{
		this.curPlayerKingData = playerKingData;
		this.myKingDecView.SetPlayKingData(playerKingData);
		this.selfView.ltext_level.text = PathDefine.Concat(Game.Language.Get("等级", ""), StringDefine.ColonSpace, playerKingData.level);
		this.selfView.ltext_allDamage.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("总伤害加成", ""), StringDefine.ColonSpace, playerKingData.allDamage * 100f);
		this.selfView.ltext_allMoney.text = PathDefine.Concat(Game.Language.Get("金钱", ""), StringDefine.ColonSpace, playerKingData.allMoney);
		this.selfView.ltext_allGem.text = PathDefine.Concat(Game.Language.Get("骷髅币", ""), StringDefine.ColonSpace, playerKingData.allGem);
		this.selfView.ltext_maxHp.text = PathDefine.Concat(Game.Language.Get("生命值", ""), StringDefine.ColonSpace, playerKingData.maxHp);
		this.selfView.ltext_maxMp.text = PathDefine.Concat(Game.Language.Get("法力值", ""), StringDefine.ColonSpace, playerKingData.maxMp);
		this.selfView.ltext_str.text = PathDefine.Concat(Game.Language.Get("str", ""), StringDefine.ColonSpace, playerKingData.str);
		this.selfView.ltext_agi.text = PathDefine.Concat(Game.Language.Get("dex", ""), StringDefine.ColonSpace, playerKingData.agi);
		this.selfView.ltext_sta.text = PathDefine.Concat(Game.Language.Get("sta", ""), StringDefine.ColonSpace, playerKingData.sta);
		float num = 1f - Util.GetArmorLevel(playerKingData.armor);
		this.selfView.ltext_armor.text = PathDefine.Concat(Game.Language.Get("armor", ""), StringDefine.ColonSpace, playerKingData.armor) + string.Format("({0}:{1:F1}%)", Game.Language.Get("减伤率", ""), num * 100f);
		float num2 = 1f - Util.GetArmorLevel(playerKingData.dodge);
		this.selfView.ltext_dodge.text = PathDefine.Concat(Game.Language.Get("闪避值", ""), StringDefine.ColonSpace, playerKingData.dodge) + string.Format("({0}:{1:F1}%)", Game.Language.Get("闪避率", ""), num2 * 100f);
		float num3 = 1f - Util.GetArmorLevel(playerKingData.skillReduction);
		this.selfView.ltext_skillReduction.text = PathDefine.Concat(Game.Language.Get("技能抵抗", ""), StringDefine.ColonSpace, playerKingData.skillReduction) + string.Format("({0}:{1:F1}%)", Game.Language.Get("技能伤害减免", ""), num3 * 100f);
		this.selfView.ltext_moveSpeed.text = string.Format("{0}{1}{2:F1}", Game.Language.Get("moveSpeed", ""), StringDefine.ColonSpace, playerKingData.moveSpeed);
		this.selfView.ltext_lucky.text = PathDefine.Concat(Game.Language.Get("幸运值", ""), StringDefine.ColonSpace, playerKingData.lucky);
		this.selfView.ltext_hpAdd.text = PathDefine.Concat(Game.Language.Get("hpAddSec", ""), StringDefine.ColonSpace, playerKingData.hpAdd);
		this.selfView.ltext_mpAdd.text = PathDefine.Concat(Game.Language.Get("mpAddSec", ""), StringDefine.ColonSpace, playerKingData.mpAdd);
		this.selfView.ltext_hpSecRate.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("hpAddSec", ""), StringDefine.ColonSpace, playerKingData.hpSecRate * 100f);
		this.selfView.ltext_attackAddHp.text = PathDefine.Concat(Game.Language.Get("xixue", ""), StringDefine.ColonSpace, playerKingData.attackAddHp);
		this.selfView.ltext_lifeStealing.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("攻击生命偷取", ""), StringDefine.ColonSpace, playerKingData.lifeStealing * 100f);
		this.selfView.ltext_magicXiXue.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("法术吸血", ""), StringDefine.ColonSpace, playerKingData.magicXiXue * 100f);
		this.selfView.ltext_attack.text = PathDefine.Concat(Game.Language.Get("attack", ""), StringDefine.ColonSpace, playerKingData.attack);
		this.selfView.ltext_attackSpeed.text = string.Format("{0}{1}{2:F1}", Game.Language.Get("attackSpeed", ""), StringDefine.ColonSpace, playerKingData.attackSpeed);
		this.selfView.ltext_critical.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("baojiLv", ""), StringDefine.ColonSpace, playerKingData.critical * 100f);
		this.selfView.ltext_criticalDamage.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("baojiDamage", ""), StringDefine.ColonSpace, playerKingData.criticalDamage * 100f);
		this.selfView.ltext_normalDamage.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("物理伤害加成", ""), StringDefine.ColonSpace, playerKingData.normalDamage * 100f);
		this.selfView.ltext_normalBreak.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("物理破盾加成", ""), StringDefine.ColonSpace, playerKingData.normalBreak * 100f);
		this.selfView.ltext_skillDamage.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("法术伤害加成", ""), StringDefine.ColonSpace, playerKingData.skillDamage * 100f);
		this.selfView.ltext_skillBreak.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("法术破盾伤害", ""), StringDefine.ColonSpace, playerKingData.skillBreak * 100f);
		this.selfView.ltext_skillCd.text = PathDefine.Concat(Game.Language.Get("技能急速", ""), StringDefine.ColonSpace, playerKingData.skillCd) + string.Format("({0}:{1:F1}%)", Game.Language.Get("冷却时间缩减", ""), (1f - Util.GetCdReduce(playerKingData.skillCd)) * 100f);
		this.selfView.ltext_skillRange.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("技能范围", ""), StringDefine.ColonSpace, playerKingData.skillRange * 100f);
		this.selfView.ltext_skillTime.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("技能持续时间", ""), StringDefine.ColonSpace, playerKingData.skillTime * 100f);
		this.selfView.ltext_skillExpend.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("法力值消耗", ""), StringDefine.ColonSpace, playerKingData.skillExpend * 100f);
		this.selfView.ltext_reduceInjury.text = PathDefine.Concat(Game.Language.Get("gdj", ""), StringDefine.ColonSpace, playerKingData.reduceInjury);
		this.selfView.ltext_extraDamage.text = PathDefine.Concat(Game.Language.Get("exs", ""), StringDefine.ColonSpace, playerKingData.extraDamage);
		this.selfView.ltext_attackDistance.text = string.Format("{0}{1}{2}", Game.Language.Get("攻击距离", ""), StringDefine.ColonSpace, Mathf.Round(playerKingData.attackDistance * 10f) / 10f);
		this.selfView.ltext_castSpeed.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("施法速度提升", ""), StringDefine.ColonSpace, playerKingData.castSpeed * 100f);
		this.selfView.ltext_skillNoneDamage.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("无属性技能伤害", ""), StringDefine.ColonSpace, playerKingData.skillNoneDamage * 100f);
		this.selfView.ltext_fireDamage.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("火焰伤害", ""), StringDefine.ColonSpace, playerKingData.fireDamage * 100f);
		this.selfView.ltext_iceDamage.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("冰冻伤害", ""), StringDefine.ColonSpace, playerKingData.iceDamage * 100f);
		this.selfView.ltext_lightDamage.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("雷电伤害", ""), StringDefine.ColonSpace, playerKingData.lightDamage * 100f);
		this.selfView.ltext_effectDamage.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("攻击特效加成", ""), StringDefine.ColonSpace, playerKingData.effectDamage * 100f);
		this.selfView.ltext_hpAddUpgrade.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("生命回复加成", ""), StringDefine.ColonSpace, playerKingData.hpAddUpgrade * 100f);
		this.selfView.ltext_buffDamage.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("BUFF伤害加成", ""), StringDefine.ColonSpace, playerKingData.buffDamage * 100f);
		this.selfView.ltext_haloRangeAdd.text = string.Format("{0}{1}{2}", Game.Language.Get("光环范围提升", ""), StringDefine.ColonSpace, Mathf.Round(playerKingData.haloRangeAdd * 10f) / 10f);
		this.selfView.ltext_addCallMonsterAttack.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("召唤物强度", ""), StringDefine.ColonSpace, playerKingData.addCallMonsterAttack * 100f);
		this.selfView.ltext_addCallMonsterTime.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("召唤物持续时间", ""), StringDefine.ColonSpace, playerKingData.addCallMonsterTime * 100f);
		this.selfView.ltext_addHenshin.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("变身强度", ""), StringDefine.ColonSpace, playerKingData.addHenshin * 100f);
		this.selfView.ltext_addHenshinTime.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("变身持续时间", ""), StringDefine.ColonSpace, playerKingData.addHenshinTime * 100f);
		this.selfView.ltext_armedAdd.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("武装伤害", ""), StringDefine.ColonSpace, playerKingData.armedAdd * 100f);
		this.selfView.ltext_equipAdd.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("装备属性加成", ""), StringDefine.ColonSpace, playerKingData.equipAdd * 100f);
		this.selfView.ltext_forgeAdd.text = string.Format("{0}{1}{2:F1}%", Game.Language.Get("属性锻造器增幅", ""), StringDefine.ColonSpace, playerKingData.forgeAdd * 100f);
	}

	// Token: 0x04001154 RID: 4436
	public UI_KingDec_View selfView;

	// Token: 0x04001155 RID: 4437
	private MyKingDecView myKingDecView;

	// Token: 0x04001156 RID: 4438
	private SaveLoadManager.PlayerKingData curPlayerKingData;
}
