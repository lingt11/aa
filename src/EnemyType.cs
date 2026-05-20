using System;

// Token: 0x02000115 RID: 277
public enum EnemyType
{
	// Token: 0x04000679 RID: 1657
	Goblin_1,
	// Token: 0x0400067A RID: 1658
	Goblin_2,
	// Token: 0x0400067B RID: 1659
	Goblin_3,
	// Token: 0x0400067C RID: 1660
	Goblin_4,
	// Token: 0x0400067D RID: 1661
	Dummy = 6,
	// Token: 0x0400067E RID: 1662
	SkeletonCrossbow,
	// Token: 0x0400067F RID: 1663
	SaiYa,
	// Token: 0x04000680 RID: 1664
	SaiYaDark,
	// Token: 0x04000681 RID: 1665
	Teleporter,
	// Token: 0x04000682 RID: 1666
	Colossus,
	// Token: 0x04000683 RID: 1667
	Necromancer,
	// Token: 0x04000684 RID: 1668
	NecromancerStone,
	// Token: 0x04000685 RID: 1669
	Wraith,
	// Token: 0x04000686 RID: 1670
	SkeletonSoldier,
	// Token: 0x04000687 RID: 1671
	ForestGuardian,
	// Token: 0x04000688 RID: 1672
	Chest,
	// Token: 0x04000689 RID: 1673
	Goblin_Boss_0 = 100,
	// Token: 0x0400068A RID: 1674
	Goblin_Boss_1,
	// Token: 0x0400068B RID: 1675
	Goblin_Boss_2,
	// Token: 0x0400068C RID: 1676
	Goblin_Boss_3,
	// Token: 0x0400068D RID: 1677
	Goblin_Boss_4,
	// Token: 0x0400068E RID: 1678
	Goblin_Boss_5,
	// Token: 0x0400068F RID: 1679
	Goblin_Blacksmith_0 = 200,
	// Token: 0x04000690 RID: 1680
	Goblin_Blacksmith_1,
	// Token: 0x04000691 RID: 1681
	Goblin_Blacksmith_2,
	// Token: 0x04000692 RID: 1682
	Goblin_Blacksmith_3,
	// Token: 0x04000693 RID: 1683
	Goblin_HellFlame_0 = 300,
	// Token: 0x04000694 RID: 1684
	Goblin_HellFlame_1,
	// Token: 0x04000695 RID: 1685
	Goblin_HellFlame_2,
	// Token: 0x04000696 RID: 1686
	Goblin_HellFlame_3,
	// Token: 0x04000697 RID: 1687
	Goblin_HellFlame_4,
	// Token: 0x04000698 RID: 1688
	Goblin_HellFlame_5,
	// Token: 0x04000699 RID: 1689
	Goblin_HellFlameSummon,
	// Token: 0x0400069A RID: 1690
	Goblin_LocalTyrant_0 = 400,
	// Token: 0x0400069B RID: 1691
	Goblin_LocalTyrant_1,
	// Token: 0x0400069C RID: 1692
	Goblin_LocalTyrant_2,
	// Token: 0x0400069D RID: 1693
	Goblin_LocalTyrant_3,
	// Token: 0x0400069E RID: 1694
	Goblin_LocalTyrant_4,
	// Token: 0x0400069F RID: 1695
	Goblin_LocalTyrant_5,
	// Token: 0x040006A0 RID: 1696
	Goblin_Mine_0 = 500,
	// Token: 0x040006A1 RID: 1697
	Goblin_Mine_1,
	// Token: 0x040006A2 RID: 1698
	Goblin_Mine_2,
	// Token: 0x040006A3 RID: 1699
	Goblin_Mine_3,
	// Token: 0x040006A4 RID: 1700
	Goblin_Mine_4,
	// Token: 0x040006A5 RID: 1701
	Goblin_Mine_5,
	// Token: 0x040006A6 RID: 1702
	Goblin_HeartMonster_0 = 600,
	// Token: 0x040006A7 RID: 1703
	Goblin_HeartMonster_1,
	// Token: 0x040006A8 RID: 1704
	Goblin_HeartMonster_2,
	// Token: 0x040006A9 RID: 1705
	Goblin_HeartMonster_3,
	// Token: 0x040006AA RID: 1706
	Goblin_HeartMonster_4,
	// Token: 0x040006AB RID: 1707
	Goblin_HeartMonster_5,
	// Token: 0x040006AC RID: 1708
	Goblin_Warrior_0 = 700,
	// Token: 0x040006AD RID: 1709
	Goblin_Warrior_1,
	// Token: 0x040006AE RID: 1710
	Goblin_Warrior_2,
	// Token: 0x040006AF RID: 1711
	Goblin_Warrior_3,
	// Token: 0x040006B0 RID: 1712
	Goblin_Warrior_4,
	// Token: 0x040006B1 RID: 1713
	Goblin_Warrior_5,
	// Token: 0x040006B2 RID: 1714
	Goblin_Elder_0 = 800,
	// Token: 0x040006B3 RID: 1715
	Goblin_Elder_1,
	// Token: 0x040006B4 RID: 1716
	Goblin_Elder_2,
	// Token: 0x040006B5 RID: 1717
	Goblin_Elder_3,
	// Token: 0x040006B6 RID: 1718
	Goblin_Elder_4,
	// Token: 0x040006B7 RID: 1719
	Goblin_Elder_5,
	// Token: 0x040006B8 RID: 1720
	Goblin_General_0 = 900,
	// Token: 0x040006B9 RID: 1721
	Goblin_General_1,
	// Token: 0x040006BA RID: 1722
	Goblin_General_2,
	// Token: 0x040006BB RID: 1723
	Goblin_General_3,
	// Token: 0x040006BC RID: 1724
	Goblin_General_4,
	// Token: 0x040006BD RID: 1725
	Goblin_General_5,
	// Token: 0x040006BE RID: 1726
	Summon_Knight_D = 1000,
	// Token: 0x040006BF RID: 1727
	Summon_Knight_C,
	// Token: 0x040006C0 RID: 1728
	Summon_Knight_B,
	// Token: 0x040006C1 RID: 1729
	Summon_Knight_A,
	// Token: 0x040006C2 RID: 1730
	Summon_Knight_S,
	// Token: 0x040006C3 RID: 1731
	Summon_Peashooter,
	// Token: 0x040006C4 RID: 1732
	Summon_Sunflower,
	// Token: 0x040006C5 RID: 1733
	Summon_Nezuko,
	// Token: 0x040006C6 RID: 1734
	Summon_Naruto,
	// Token: 0x040006C7 RID: 1735
	Summon_Tree_D,
	// Token: 0x040006C8 RID: 1736
	Summon_Tree_C,
	// Token: 0x040006C9 RID: 1737
	Summon_Tree_B,
	// Token: 0x040006CA RID: 1738
	NPC_King,
	// Token: 0x040006CB RID: 1739
	NPC_Ghost
}
