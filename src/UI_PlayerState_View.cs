using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000375 RID: 885
public class UI_PlayerState_View : UGUIView
{
	// Token: 0x0600144F RID: 5199 RVA: 0x0007E3A4 File Offset: 0x0007C5A4
	public override void Init(Transform trans)
	{
		this.trans_hpParent = trans.GetChild(0).GetComponent<Transform>();
		this.trans_talk = trans.GetChild(1).GetComponent<Transform>();
		this.trans_lock = trans.GetChild(2).GetComponent<Transform>();
		this.slider_hp = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>();
		this.text_hp = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(3).GetComponent<TMP_Text>();
		this.slider_mp = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Slider>();
		this.text_mp = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
		this.img_HeadIcon = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetComponent<Image>();
		this.ltext_UserName = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(4).GetComponent<Text>();
		this.slider_exp = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<Slider>();
		this.text_XP = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(0).GetChild(3).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
		this.text_ATK = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
		this.trans_touchATK = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetComponent<Transform>();
		this.text_armor = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<TMP_Text>();
		this.trans_touchArm = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<Transform>();
		this.text_str = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<TMP_Text>();
		this.trans_touchSTR = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(3).GetChild(1).GetComponent<Transform>();
		this.text_AGI = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(4).GetChild(0).GetComponent<TMP_Text>();
		this.trans_touchAGI = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(4).GetChild(1).GetComponent<Transform>();
		this.text_STA = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(5).GetChild(0).GetComponent<TMP_Text>();
		this.trans_touchSTA = trans.GetChild(3).GetChild(0).GetChild(1).GetChild(5).GetChild(1).GetComponent<Transform>();
		this.text_gold = trans.GetChild(3).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetComponent<TMP_Text>();
		this.text_gem = trans.GetChild(3).GetChild(0).GetChild(2).GetChild(0).GetChild(3).GetComponent<TMP_Text>();
		this.trans_PlayerLevel = trans.GetChild(3).GetChild(0).GetChild(3).GetComponent<Transform>();
		this.text_level = trans.GetChild(3).GetChild(0).GetChild(3).GetChild(0).GetChild(4).GetComponent<TMP_Text>();
		this.trans_exEquip = trans.GetChild(3).GetChild(0).GetChild(5).GetChild(0).GetComponent<Transform>();
		this.pool_equip = trans.GetChild(3).GetChild(0).GetChild(6).GetComponent<PoolView>();
		this.pool_buff = trans.GetChild(3).GetChild(0).GetChild(7).GetComponent<PoolView>();
		this.trans_teleportTip = trans.GetChild(3).GetChild(0).GetChild(8).GetComponent<Transform>();
		this.pool_skillPanel = trans.GetChild(3).GetChild(0).GetChild(9).GetComponent<PoolView>();
		this.trans_map = trans.GetChild(3).GetChild(0).GetChild(11).GetComponent<Transform>();
		this.trans_heroMap = trans.GetChild(3).GetChild(0).GetChild(12).GetComponent<Transform>();
		this.trans_damage = trans.GetChild(3).GetChild(0).GetChild(13).GetComponent<Transform>();
		this.trans_switchSkill = trans.GetChild(3).GetChild(0).GetChild(15).GetComponent<Transform>();
		this.btn_giveup = trans.GetChild(3).GetChild(0).GetChild(15).GetChild(1).GetComponent<Button>();
		this.trans_touch = trans.GetChild(3).GetChild(1).GetComponent<Transform>();
		this.pool_bag = trans.GetChild(3).GetChild(2).GetChild(0).GetComponent<PoolView>();
		this.trans_bagDetail = trans.GetChild(3).GetChild(4).GetComponent<Transform>();
	}

	// Token: 0x040012D6 RID: 4822
	public Transform trans_hpParent;

	// Token: 0x040012D7 RID: 4823
	public Transform trans_talk;

	// Token: 0x040012D8 RID: 4824
	public Transform trans_lock;

	// Token: 0x040012D9 RID: 4825
	public Slider slider_hp;

	// Token: 0x040012DA RID: 4826
	public TMP_Text text_hp;

	// Token: 0x040012DB RID: 4827
	public Slider slider_mp;

	// Token: 0x040012DC RID: 4828
	public TMP_Text text_mp;

	// Token: 0x040012DD RID: 4829
	public Image img_HeadIcon;

	// Token: 0x040012DE RID: 4830
	public Text ltext_UserName;

	// Token: 0x040012DF RID: 4831
	public Slider slider_exp;

	// Token: 0x040012E0 RID: 4832
	public TMP_Text text_XP;

	// Token: 0x040012E1 RID: 4833
	public TMP_Text text_ATK;

	// Token: 0x040012E2 RID: 4834
	public Transform trans_touchATK;

	// Token: 0x040012E3 RID: 4835
	public TMP_Text text_armor;

	// Token: 0x040012E4 RID: 4836
	public Transform trans_touchArm;

	// Token: 0x040012E5 RID: 4837
	public TMP_Text text_str;

	// Token: 0x040012E6 RID: 4838
	public Transform trans_touchSTR;

	// Token: 0x040012E7 RID: 4839
	public TMP_Text text_AGI;

	// Token: 0x040012E8 RID: 4840
	public Transform trans_touchAGI;

	// Token: 0x040012E9 RID: 4841
	public TMP_Text text_STA;

	// Token: 0x040012EA RID: 4842
	public Transform trans_touchSTA;

	// Token: 0x040012EB RID: 4843
	public TMP_Text text_gold;

	// Token: 0x040012EC RID: 4844
	public TMP_Text text_gem;

	// Token: 0x040012ED RID: 4845
	public Transform trans_PlayerLevel;

	// Token: 0x040012EE RID: 4846
	public TMP_Text text_level;

	// Token: 0x040012EF RID: 4847
	public Transform trans_exEquip;

	// Token: 0x040012F0 RID: 4848
	public PoolView pool_equip;

	// Token: 0x040012F1 RID: 4849
	public PoolView pool_buff;

	// Token: 0x040012F2 RID: 4850
	public Transform trans_teleportTip;

	// Token: 0x040012F3 RID: 4851
	public PoolView pool_skillPanel;

	// Token: 0x040012F4 RID: 4852
	public Transform trans_map;

	// Token: 0x040012F5 RID: 4853
	public Transform trans_heroMap;

	// Token: 0x040012F6 RID: 4854
	public Transform trans_damage;

	// Token: 0x040012F7 RID: 4855
	public Transform trans_switchSkill;

	// Token: 0x040012F8 RID: 4856
	public Button btn_giveup;

	// Token: 0x040012F9 RID: 4857
	public Transform trans_touch;

	// Token: 0x040012FA RID: 4858
	public PoolView pool_bag;

	// Token: 0x040012FB RID: 4859
	public Transform trans_bagDetail;
}
