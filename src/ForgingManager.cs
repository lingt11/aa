using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
public class ForgingManager
{
	// Token: 0x060005D5 RID: 1493 RVA: 0x00022AFC File Offset: 0x00020CFC
	public void UpdateStr(int value)
	{
		this.Str += value;
		this.OnUpdateStr();
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x00022B14 File Offset: 0x00020D14
	private void OnUpdateStr()
	{
		int allStr = this.AllStr;
		this.AllStr = Mathf.RoundToInt((float)this.Str * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.AddSTR(this.AllStr - allStr);
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00022B59 File Offset: 0x00020D59
	public void UpdateSta(int value)
	{
		this.Sta += value;
		this.OnUpdateSta();
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x00022B70 File Offset: 0x00020D70
	private void OnUpdateSta()
	{
		int allSta = this.AllSta;
		this.AllSta = Mathf.RoundToInt((float)this.Sta * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.AddSTA(this.AllSta - allSta);
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x00022BB5 File Offset: 0x00020DB5
	public void UpdateAgi(int value)
	{
		this.Agi += value;
		this.OnUpdateAgi();
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x00022BCC File Offset: 0x00020DCC
	private void OnUpdateAgi()
	{
		int allAgi = this.AllAgi;
		this.AllAgi = Mathf.RoundToInt((float)this.Agi * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.AddAGI(this.AllAgi - allAgi);
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x00022C11 File Offset: 0x00020E11
	public void UpdateAttack(int value)
	{
		this.Attack += value;
		this.OnUpdateAttack();
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x00022C28 File Offset: 0x00020E28
	private void OnUpdateAttack()
	{
		int allAttack = this.AllAttack;
		this.AllAttack = Mathf.RoundToInt((float)this.Attack * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.AddAttackPower(this.AllAttack - allAttack);
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x00022C6D File Offset: 0x00020E6D
	public void UpdateHP(int value)
	{
		this.HP += value;
		this.OnUpdateHP();
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x00022C84 File Offset: 0x00020E84
	private void OnUpdateHP()
	{
		int allHP = this.AllHP;
		this.AllHP = Mathf.RoundToInt((float)this.HP * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.CmdUpdateMaxHp((long)(this.AllHP - allHP), GameHelperClient.localPlayer.netId);
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x00022CD4 File Offset: 0x00020ED4
	public void UpdateMP(int value)
	{
		this.MP += value;
		this.OnUpdateMP();
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x00022CEC File Offset: 0x00020EEC
	private void OnUpdateMP()
	{
		int allMP = this.AllMP;
		this.AllMP = Mathf.RoundToInt((float)this.MP * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.AddMaxMp(this.AllMP - allMP);
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x00022D31 File Offset: 0x00020F31
	public void UpdateArmor(int value)
	{
		this.Armor += value;
		this.OnUpdateArmor();
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x00022D48 File Offset: 0x00020F48
	private void OnUpdateArmor()
	{
		int allArmor = this.AllArmor;
		this.AllArmor = Mathf.RoundToInt((float)this.Armor * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.AddArmor(this.AllArmor - allArmor);
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x00022D8D File Offset: 0x00020F8D
	public void UpdateHPSec(int value)
	{
		this.HPSec += value;
		this.OnUpdateHPSec();
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x00022DA4 File Offset: 0x00020FA4
	private void OnUpdateHPSec()
	{
		int allHPSec = this.AllHPSec;
		this.AllHPSec = Mathf.RoundToInt((float)this.HPSec * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.AddHpAddSec(this.AllHPSec - allHPSec);
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x00022DE9 File Offset: 0x00020FE9
	public void UpdateMPSec(int value)
	{
		this.MPSec += value;
		this.OnUpdateMPSec();
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x00022E00 File Offset: 0x00021000
	private void OnUpdateMPSec()
	{
		int allMPSec = this.AllMPSec;
		this.AllMPSec = Mathf.RoundToInt((float)this.MPSec * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.AddMpAddSec(this.AllMPSec - allMPSec);
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x00022E45 File Offset: 0x00021045
	public void UpdateAttackSpeed(float value)
	{
		this.AttackSpeed += value;
		this.OnUpdateAttackSpeed();
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x00022E5C File Offset: 0x0002105C
	private void OnUpdateAttackSpeed()
	{
		float allAttackSpeed = this.AllAttackSpeed;
		this.AllAttackSpeed = this.AttackSpeed * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.AddAttackSpeed(this.AllAttackSpeed - allAttackSpeed);
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x00022E9B File Offset: 0x0002109B
	public void UpdateNormalBreak(float value)
	{
		this.NormalBreak += value;
		this.OnUpdateNormalBreak();
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x00022EB4 File Offset: 0x000210B4
	private void OnUpdateNormalBreak()
	{
		float allNormalBreak = this.AllNormalBreak;
		this.AllNormalBreak = this.NormalBreak * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.normalBreakShieldBase += this.AllNormalBreak - allNormalBreak;
		GameHelperClient.localPlayer.UpdateBreakShield();
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x00022F04 File Offset: 0x00021104
	public void UpdateSkillBreak(float value)
	{
		this.SkillBreak += value;
		this.OnUpdateSkillBreak();
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x00022F1C File Offset: 0x0002111C
	private void OnUpdateSkillBreak()
	{
		float allSkillBreak = this.AllSkillBreak;
		this.AllSkillBreak = this.SkillBreak * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.skillBreakShieldBase += this.AllSkillBreak - allSkillBreak;
		GameHelperClient.localPlayer.UpdateBreakShield();
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x00022F6C File Offset: 0x0002116C
	public void UpdateCritical(float value)
	{
		this.Critical += value;
		this.OnUpdateCritical();
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x00022F84 File Offset: 0x00021184
	private void OnUpdateCritical()
	{
		float allCritical = this.AllCritical;
		this.AllCritical = this.Critical * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.AddCritical(this.AllCritical - allCritical);
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x00022FC3 File Offset: 0x000211C3
	public void UpdateLuck(int value)
	{
		this.Luck += value;
		this.OnUpdateLuck();
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00022FDC File Offset: 0x000211DC
	private void OnUpdateLuck()
	{
		int allLuck = this.AllLuck;
		this.AllLuck = Mathf.RoundToInt((float)this.Luck * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.CmdUpdateLucky(this.AllLuck - allLuck);
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x00023021 File Offset: 0x00021221
	public void UpdateCriticalDamage(float value)
	{
		this.CriticalDamage += value;
		this.OnUpdateCriticalDamage();
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x00023038 File Offset: 0x00021238
	private void OnUpdateCriticalDamage()
	{
		float allCriticalDamage = this.AllCriticalDamage;
		this.AllCriticalDamage = this.CriticalDamage * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.AddCriticalDamage(this.AllCriticalDamage - allCriticalDamage);
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x00023077 File Offset: 0x00021277
	public void UpdateXiXueRate(float value)
	{
		this.XiXueRate += value;
		this.OnUpdateXiXueRate();
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00023090 File Offset: 0x00021290
	private void OnUpdateXiXueRate()
	{
		float allXiXueRate = this.AllXiXueRate;
		this.AllXiXueRate = this.XiXueRate * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.xiXueLv += this.AllXiXueRate - allXiXueRate;
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x000230D6 File Offset: 0x000212D6
	public void UpdateNormalAdd(float value)
	{
		this.NormalAdd += value;
		this.OnUpdateNormalAdd();
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x000230EC File Offset: 0x000212EC
	private void OnUpdateNormalAdd()
	{
		float allNormalAdd = this.AllNormalAdd;
		this.AllNormalAdd = this.NormalAdd * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.normalAttackAddDamage += this.AllNormalAdd - allNormalAdd;
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x00023132 File Offset: 0x00021332
	public void UpdateSkillAdd(float value)
	{
		this.SkillAdd += value;
		this.OnUpdateSkillAdd();
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x00023148 File Offset: 0x00021348
	private void OnUpdateSkillAdd()
	{
		float allSkillAdd = this.AllSkillAdd;
		this.AllSkillAdd = this.SkillAdd * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.skillExDamage += this.AllSkillAdd - allSkillAdd;
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x0002318E File Offset: 0x0002138E
	public void UpdateXiXue(int value)
	{
		this.XiXue += value;
		this.OnUpdateXiXue();
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x000231A4 File Offset: 0x000213A4
	private void OnUpdateXiXue()
	{
		int allXiXue = this.AllXiXue;
		this.AllXiXue = Mathf.RoundToInt((float)this.XiXue * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.AddXiXue((float)(this.AllXiXue - allXiXue));
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x000231EA File Offset: 0x000213EA
	public void UpdateCoolDown(int value)
	{
		this.CoolDown += value;
		this.OnUpdateCoolDown();
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x00023200 File Offset: 0x00021400
	private void OnUpdateCoolDown()
	{
		int allCoolDown = this.AllCoolDown;
		this.AllCoolDown = Mathf.RoundToInt((float)this.CoolDown * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.skillCdReduce += this.AllCoolDown - allCoolDown;
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0002324C File Offset: 0x0002144C
	public void UpdateReduceInjury(int value)
	{
		this.ReduceInjury += value;
		this.OnUpdateReduceInjury();
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x00023264 File Offset: 0x00021464
	private void OnUpdateReduceInjury()
	{
		int allReduceInjury = this.AllReduceInjury;
		this.AllReduceInjury = Mathf.RoundToInt((float)this.ReduceInjury * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.UpdateReduce(this.AllReduceInjury - allReduceInjury);
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x000232A9 File Offset: 0x000214A9
	public void UpdateExtraDamage(int value)
	{
		this.ExtraDamage += value;
		this.OnUpdateExtraDamage();
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x000232C0 File Offset: 0x000214C0
	private void OnUpdateExtraDamage()
	{
		int allExtraDamage = this.AllExtraDamage;
		this.AllExtraDamage = Mathf.RoundToInt((float)this.ExtraDamage * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.extraDamage += this.AllExtraDamage - allExtraDamage;
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0002330C File Offset: 0x0002150C
	public void UpdateAddDamage(float value)
	{
		this.AddDamage += value;
		this.OnUpdateAddDamage();
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x00023324 File Offset: 0x00021524
	private void OnUpdateAddDamage()
	{
		float allAddDamage = this.AllAddDamage;
		this.AllAddDamage = this.AddDamage * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.addDamagePercent += this.AllAddDamage - allAddDamage;
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0002336A File Offset: 0x0002156A
	public void UpdateDoge(int value)
	{
		this.Doge += value;
		this.OnUpdateDoge();
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x00023380 File Offset: 0x00021580
	private void OnUpdateDoge()
	{
		int allDoge = this.AllDoge;
		this.AllDoge = Mathf.RoundToInt((float)this.Doge * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.doge += this.AllDoge - allDoge;
		GameHelperClient.localPlayer.CmdDoge(GameHelperClient.localPlayer.doge);
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x000233E0 File Offset: 0x000215E0
	public void UpdateMoveSpeed(float value)
	{
		this.MoveSpeed += value;
		this.OnUpdateMoveSpeed();
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x000233F8 File Offset: 0x000215F8
	private void OnUpdateMoveSpeed()
	{
		float allMoveSpeed = this.AllMoveSpeed;
		this.AllMoveSpeed = this.MoveSpeed * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.AddMoveSpeed(this.AllMoveSpeed - allMoveSpeed);
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x00023437 File Offset: 0x00021637
	public void UpdateHpPercent(float value)
	{
		this.HpPercent += value;
		this.OnUpdateHpPercent();
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x00023450 File Offset: 0x00021650
	private void OnUpdateHpPercent()
	{
		float allHpPercent = this.AllHpPercent;
		this.AllHpPercent = this.HpPercent * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.CmdUpdateMaxHpAddPercent(this.AllHpPercent - allHpPercent);
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0002348F File Offset: 0x0002168F
	public void UpdateHpSecRate(float value)
	{
		this.HpSecRate += value;
		this.OnUpdateHpSecRate();
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x000234A8 File Offset: 0x000216A8
	private void OnUpdateHpSecRate()
	{
		float allHpSecRate = this.AllHpSecRate;
		this.AllHpSecRate = this.HpSecRate * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.hpAddSecRate += this.AllHpSecRate - allHpSecRate;
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x000234EE File Offset: 0x000216EE
	public void UpdateSkillHit(int value)
	{
		this.SkillHit += value;
		this.OnUpdateSkillHit();
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x00023504 File Offset: 0x00021704
	private void OnUpdateSkillHit()
	{
		int allSkillHit = this.AllSkillHit;
		this.AllSkillHit = Mathf.RoundToInt((float)this.SkillHit * (1f + this.forgingAdd));
		GameHelperClient.localPlayer.UpdateSkillHitDamage(this.AllSkillHit - allSkillHit);
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x00023549 File Offset: 0x00021749
	public void UpdateExpAdd(float value)
	{
		this.ExpAdd += value;
		this.OnUpdateExpAdd();
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x00023560 File Offset: 0x00021760
	private void OnUpdateExpAdd()
	{
		float allExpAdd = this.AllExpAdd;
		this.AllExpAdd = this.ExpAdd * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.addExp += this.AllExpAdd - allExpAdd;
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x000235A6 File Offset: 0x000217A6
	public void UpdateSummonAdd(float value)
	{
		this.SummonAdd += value;
		this.OnUpdateSummonAdd();
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x000235BC File Offset: 0x000217BC
	private void OnUpdateSummonAdd()
	{
		float allSummonAdd = this.AllSummonAdd;
		this.AllSummonAdd = this.SummonAdd * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.addCallMonsterAttack += this.AllSummonAdd - allSummonAdd;
		GameHelperClient.localPlayer.addCallMonsterHp += this.AllSummonAdd - allSummonAdd;
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x0002361B File Offset: 0x0002181B
	public void UpdateHenshinAdd(float value)
	{
		this.HenshinAdd += value;
		this.OnUpdateHenshinAdd();
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x00023634 File Offset: 0x00021834
	private void OnUpdateHenshinAdd()
	{
		float allHenshinAdd = this.AllHenshinAdd;
		this.AllHenshinAdd = this.HenshinAdd * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.addHenshin += this.AllHenshinAdd - allHenshinAdd;
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0002367A File Offset: 0x0002187A
	public void UpdateHaloAdd(float value)
	{
		this.HaloAdd += value;
		this.OnUpdateHaloAdd();
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x00023690 File Offset: 0x00021890
	private void OnUpdateHaloAdd()
	{
		float allHaloAdd = this.AllHaloAdd;
		this.AllHaloAdd = this.HaloAdd * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.buffAddDamage += this.AllHaloAdd - allHaloAdd;
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x000236D6 File Offset: 0x000218D6
	public void UpdateArmedAdd(float value)
	{
		this.ArmedAdd += value;
		this.OnUpdateArmedAdd();
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x000236EC File Offset: 0x000218EC
	private void OnUpdateArmedAdd()
	{
		float allArmedAdd = this.AllArmedAdd;
		this.AllArmedAdd = this.ArmedAdd * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.armedAdd += this.AllArmedAdd - allArmedAdd;
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x00023732 File Offset: 0x00021932
	public void UpdateGoldAdd(float value)
	{
		this.GoldAdd += value;
		this.OnUpdateGoldAdd();
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x00023748 File Offset: 0x00021948
	private void OnUpdateGoldAdd()
	{
		float allGoldAdd = this.AllGoldAdd;
		this.AllGoldAdd = this.GoldAdd * (1f + this.forgingAdd);
		GameHelperClient.localPlayer.addGoldPercent += this.AllGoldAdd - allGoldAdd;
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x00023790 File Offset: 0x00021990
	public void UpdateForgingAdd(float updateValue)
	{
		this.forgingAdd += updateValue;
		this.OnUpdateStr();
		this.OnUpdateSta();
		this.OnUpdateAgi();
		this.OnUpdateAttack();
		this.OnUpdateHP();
		this.OnUpdateMP();
		this.OnUpdateArmor();
		this.OnUpdateHPSec();
		this.OnUpdateMPSec();
		this.OnUpdateAttackSpeed();
		this.OnUpdateNormalBreak();
		this.OnUpdateSkillBreak();
		this.OnUpdateCritical();
		this.OnUpdateLuck();
		this.OnUpdateCriticalDamage();
		this.OnUpdateXiXueRate();
		this.OnUpdateNormalAdd();
		this.OnUpdateSkillAdd();
		this.OnUpdateXiXue();
		this.OnUpdateCoolDown();
		this.OnUpdateReduceInjury();
		this.OnUpdateExtraDamage();
		this.OnUpdateAddDamage();
		this.OnUpdateDoge();
		this.OnUpdateMoveSpeed();
		this.OnUpdateHpPercent();
		this.OnUpdateHpSecRate();
		this.OnUpdateSkillHit();
		this.OnUpdateExpAdd();
		this.OnUpdateSummonAdd();
		this.OnUpdateHenshinAdd();
		this.OnUpdateHaloAdd();
		this.OnUpdateArmedAdd();
		this.OnUpdateGoldAdd();
	}

	// Token: 0x0400083D RID: 2109
	public bool isInit;

	// Token: 0x0400083E RID: 2110
	public float forgingAdd;

	// Token: 0x0400083F RID: 2111
	private int Str;

	// Token: 0x04000840 RID: 2112
	private int Sta;

	// Token: 0x04000841 RID: 2113
	private int Agi;

	// Token: 0x04000842 RID: 2114
	private int Attack;

	// Token: 0x04000843 RID: 2115
	private int HP;

	// Token: 0x04000844 RID: 2116
	private int MP;

	// Token: 0x04000845 RID: 2117
	private int Armor;

	// Token: 0x04000846 RID: 2118
	private int HPSec;

	// Token: 0x04000847 RID: 2119
	private int MPSec;

	// Token: 0x04000848 RID: 2120
	private float AttackSpeed;

	// Token: 0x04000849 RID: 2121
	private float NormalBreak;

	// Token: 0x0400084A RID: 2122
	private float SkillBreak;

	// Token: 0x0400084B RID: 2123
	private float Critical;

	// Token: 0x0400084C RID: 2124
	private int Luck;

	// Token: 0x0400084D RID: 2125
	private float CriticalDamage;

	// Token: 0x0400084E RID: 2126
	private float XiXueRate;

	// Token: 0x0400084F RID: 2127
	private float NormalAdd;

	// Token: 0x04000850 RID: 2128
	private float SkillAdd;

	// Token: 0x04000851 RID: 2129
	private int XiXue;

	// Token: 0x04000852 RID: 2130
	private int CoolDown;

	// Token: 0x04000853 RID: 2131
	private int ReduceInjury;

	// Token: 0x04000854 RID: 2132
	private int ExtraDamage;

	// Token: 0x04000855 RID: 2133
	private float AddDamage;

	// Token: 0x04000856 RID: 2134
	private int Doge;

	// Token: 0x04000857 RID: 2135
	private float MoveSpeed;

	// Token: 0x04000858 RID: 2136
	private float HpPercent;

	// Token: 0x04000859 RID: 2137
	private float HpSecRate;

	// Token: 0x0400085A RID: 2138
	private int SkillHit;

	// Token: 0x0400085B RID: 2139
	private float ExpAdd;

	// Token: 0x0400085C RID: 2140
	public float SummonAdd;

	// Token: 0x0400085D RID: 2141
	public float HenshinAdd;

	// Token: 0x0400085E RID: 2142
	public float HaloAdd;

	// Token: 0x0400085F RID: 2143
	public float ArmedAdd;

	// Token: 0x04000860 RID: 2144
	public float GoldAdd;

	// Token: 0x04000861 RID: 2145
	public int AllStr;

	// Token: 0x04000862 RID: 2146
	public int AllSta;

	// Token: 0x04000863 RID: 2147
	public int AllAgi;

	// Token: 0x04000864 RID: 2148
	public int AllAttack;

	// Token: 0x04000865 RID: 2149
	public int AllHP;

	// Token: 0x04000866 RID: 2150
	public int AllMP;

	// Token: 0x04000867 RID: 2151
	public int AllArmor;

	// Token: 0x04000868 RID: 2152
	public int AllHPSec;

	// Token: 0x04000869 RID: 2153
	public int AllMPSec;

	// Token: 0x0400086A RID: 2154
	public float AllAttackSpeed;

	// Token: 0x0400086B RID: 2155
	public float AllNormalBreak;

	// Token: 0x0400086C RID: 2156
	public float AllSkillBreak;

	// Token: 0x0400086D RID: 2157
	public float AllCritical;

	// Token: 0x0400086E RID: 2158
	public int AllLuck;

	// Token: 0x0400086F RID: 2159
	public float AllCriticalDamage;

	// Token: 0x04000870 RID: 2160
	public float AllXiXueRate;

	// Token: 0x04000871 RID: 2161
	public float AllNormalAdd;

	// Token: 0x04000872 RID: 2162
	public float AllSkillAdd;

	// Token: 0x04000873 RID: 2163
	public int AllXiXue;

	// Token: 0x04000874 RID: 2164
	public int AllCoolDown;

	// Token: 0x04000875 RID: 2165
	public int AllReduceInjury;

	// Token: 0x04000876 RID: 2166
	public int AllExtraDamage;

	// Token: 0x04000877 RID: 2167
	public float AllAddDamage;

	// Token: 0x04000878 RID: 2168
	public int AllDoge;

	// Token: 0x04000879 RID: 2169
	public float AllMoveSpeed;

	// Token: 0x0400087A RID: 2170
	public float AllHpPercent;

	// Token: 0x0400087B RID: 2171
	public float AllHpSecRate;

	// Token: 0x0400087C RID: 2172
	public int AllSkillHit;

	// Token: 0x0400087D RID: 2173
	public float AllExpAdd;

	// Token: 0x0400087E RID: 2174
	public float AllSummonAdd;

	// Token: 0x0400087F RID: 2175
	public float AllHenshinAdd;

	// Token: 0x04000880 RID: 2176
	public float AllHaloAdd;

	// Token: 0x04000881 RID: 2177
	public float AllArmedAdd;

	// Token: 0x04000882 RID: 2178
	public float AllGoldAdd;
}
