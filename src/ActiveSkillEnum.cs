using System;

// Token: 0x02000156 RID: 342
public enum ActiveSkillEnum
{
	// Token: 0x04000A12 RID: 2578
	None = -1,
	// Token: 0x04000A13 RID: 2579
	D_SpellThunder,
	// Token: 0x04000A14 RID: 2580
	D_SwordMove,
	// Token: 0x04000A15 RID: 2581
	D_Revive,
	// Token: 0x04000A16 RID: 2582
	D_SummonKight,
	// Token: 0x04000A17 RID: 2583
	D_BlackHole,
	// Token: 0x04000A18 RID: 2584
	D_ContinuousLight,
	// Token: 0x04000A19 RID: 2585
	D_Flameshrower,
	// Token: 0x04000A1A RID: 2586
	D_SurroundingFire,
	// Token: 0x04000A1B RID: 2587
	D_KingsTreasure,
	// Token: 0x04000A1C RID: 2588
	D_Whirlwind,
	// Token: 0x04000A1D RID: 2589
	D_FireTornado,
	// Token: 0x04000A1E RID: 2590
	D_WindBreakSlash,
	// Token: 0x04000A1F RID: 2591
	D_Sacrifice = 13,
	// Token: 0x04000A20 RID: 2592
	D_SuperSaiyan,
	// Token: 0x04000A21 RID: 2593
	D_KamehamehaWave,
	// Token: 0x04000A22 RID: 2594
	D_Henshin,
	// Token: 0x04000A23 RID: 2595
	D_Rachel,
	// Token: 0x04000A24 RID: 2596
	D_Rasengan,
	// Token: 0x04000A25 RID: 2597
	D_IceWall,
	// Token: 0x04000A26 RID: 2598
	D_BlackCoffin,
	// Token: 0x04000A27 RID: 2599
	D_FireStep,
	// Token: 0x04000A28 RID: 2600
	D_SwordOfLight,
	// Token: 0x04000A29 RID: 2601
	D_LightningChain,
	// Token: 0x04000A2A RID: 2602
	D_LandMine,
	// Token: 0x04000A2B RID: 2603
	D_DarkMonster,
	// Token: 0x04000A2C RID: 2604
	D_HenshinLight,
	// Token: 0x04000A2D RID: 2605
	D_Peashooter,
	// Token: 0x04000A2E RID: 2606
	D_BloodSacrifice,
	// Token: 0x04000A2F RID: 2607
	D_Alchemy,
	// Token: 0x04000A30 RID: 2608
	D_Execution,
	// Token: 0x04000A31 RID: 2609
	D_Sunflower,
	// Token: 0x04000A32 RID: 2610
	D_SummomTree,
	// Token: 0x04000A33 RID: 2611
	D_AvatarDodge,
	// Token: 0x04000A34 RID: 2612
	D_DeathCyclone,
	// Token: 0x04000A35 RID: 2613
	D_PoisionAoe,
	// Token: 0x04000A36 RID: 2614
	C_SpellThunder = 100,
	// Token: 0x04000A37 RID: 2615
	C_SwordMove,
	// Token: 0x04000A38 RID: 2616
	C_SummonKight = 103,
	// Token: 0x04000A39 RID: 2617
	C_BlackHole,
	// Token: 0x04000A3A RID: 2618
	C_ContinuousLight,
	// Token: 0x04000A3B RID: 2619
	C_Flameshrower,
	// Token: 0x04000A3C RID: 2620
	C_SurroundingFire,
	// Token: 0x04000A3D RID: 2621
	C_KingsTreasure,
	// Token: 0x04000A3E RID: 2622
	C_Whirlwind,
	// Token: 0x04000A3F RID: 2623
	C_FireTornado,
	// Token: 0x04000A40 RID: 2624
	C_WindBreakSlash,
	// Token: 0x04000A41 RID: 2625
	C_Sacrifice = 113,
	// Token: 0x04000A42 RID: 2626
	C_SuperSaiyan,
	// Token: 0x04000A43 RID: 2627
	C_KamehamehaWave,
	// Token: 0x04000A44 RID: 2628
	C_Henshin,
	// Token: 0x04000A45 RID: 2629
	C_Rachel,
	// Token: 0x04000A46 RID: 2630
	C_Rasengan,
	// Token: 0x04000A47 RID: 2631
	C_IceWall,
	// Token: 0x04000A48 RID: 2632
	C_BlackCoffin,
	// Token: 0x04000A49 RID: 2633
	C_FireStep,
	// Token: 0x04000A4A RID: 2634
	C_SwordOfLight,
	// Token: 0x04000A4B RID: 2635
	C_LightningChain,
	// Token: 0x04000A4C RID: 2636
	C_LandMine,
	// Token: 0x04000A4D RID: 2637
	C_DarkMonster,
	// Token: 0x04000A4E RID: 2638
	C_HenshinLight,
	// Token: 0x04000A4F RID: 2639
	C_Peashooter,
	// Token: 0x04000A50 RID: 2640
	C_BloodSacrifice,
	// Token: 0x04000A51 RID: 2641
	C_Alchemy,
	// Token: 0x04000A52 RID: 2642
	C_Execution,
	// Token: 0x04000A53 RID: 2643
	C_Sunflower,
	// Token: 0x04000A54 RID: 2644
	C_SummomTree,
	// Token: 0x04000A55 RID: 2645
	C_AvatarDodge,
	// Token: 0x04000A56 RID: 2646
	C_DeathCyclone,
	// Token: 0x04000A57 RID: 2647
	C_PoisionAoe,
	// Token: 0x04000A58 RID: 2648
	B_SpellThunder = 200,
	// Token: 0x04000A59 RID: 2649
	B_SwordMove,
	// Token: 0x04000A5A RID: 2650
	B_SummonKight = 203,
	// Token: 0x04000A5B RID: 2651
	B_BlackHole,
	// Token: 0x04000A5C RID: 2652
	B_ContinuousLight,
	// Token: 0x04000A5D RID: 2653
	B_Flameshrower,
	// Token: 0x04000A5E RID: 2654
	B_SurroundingFire,
	// Token: 0x04000A5F RID: 2655
	B_KingsTreasure,
	// Token: 0x04000A60 RID: 2656
	B_Whirlwind,
	// Token: 0x04000A61 RID: 2657
	B_FireTornado,
	// Token: 0x04000A62 RID: 2658
	B_WindBreakSlash,
	// Token: 0x04000A63 RID: 2659
	B_Sacrifice = 213,
	// Token: 0x04000A64 RID: 2660
	B_SuperSaiyan,
	// Token: 0x04000A65 RID: 2661
	B_KamehamehaWave,
	// Token: 0x04000A66 RID: 2662
	B_Henshin,
	// Token: 0x04000A67 RID: 2663
	B_Rachel,
	// Token: 0x04000A68 RID: 2664
	B_Rasengan,
	// Token: 0x04000A69 RID: 2665
	B_IceWall,
	// Token: 0x04000A6A RID: 2666
	B_BlackCoffin,
	// Token: 0x04000A6B RID: 2667
	B_FireStep,
	// Token: 0x04000A6C RID: 2668
	B_SwordOfLight,
	// Token: 0x04000A6D RID: 2669
	B_LightningChain,
	// Token: 0x04000A6E RID: 2670
	B_LandMine,
	// Token: 0x04000A6F RID: 2671
	B_DarkMonster,
	// Token: 0x04000A70 RID: 2672
	B_HenshinLight,
	// Token: 0x04000A71 RID: 2673
	B_Peashooter,
	// Token: 0x04000A72 RID: 2674
	B_BloodSacrifice,
	// Token: 0x04000A73 RID: 2675
	B_Alchemy,
	// Token: 0x04000A74 RID: 2676
	B_Execution,
	// Token: 0x04000A75 RID: 2677
	B_Sunflower,
	// Token: 0x04000A76 RID: 2678
	B_SummomTree,
	// Token: 0x04000A77 RID: 2679
	B_AvatarDodge,
	// Token: 0x04000A78 RID: 2680
	B_DeathCyclone,
	// Token: 0x04000A79 RID: 2681
	B_PoisionAoe,
	// Token: 0x04000A7A RID: 2682
	A_SpellThunder = 300,
	// Token: 0x04000A7B RID: 2683
	A_SwordMove,
	// Token: 0x04000A7C RID: 2684
	A_SummonKight = 303,
	// Token: 0x04000A7D RID: 2685
	A_BlackHole,
	// Token: 0x04000A7E RID: 2686
	A_ContinuousLight,
	// Token: 0x04000A7F RID: 2687
	A_Flameshrower,
	// Token: 0x04000A80 RID: 2688
	A_SurroundingFire,
	// Token: 0x04000A81 RID: 2689
	A_KingsTreasure,
	// Token: 0x04000A82 RID: 2690
	A_Whirlwind,
	// Token: 0x04000A83 RID: 2691
	A_FireTornado,
	// Token: 0x04000A84 RID: 2692
	A_WindBreakSlash,
	// Token: 0x04000A85 RID: 2693
	A_Sacrifice = 313,
	// Token: 0x04000A86 RID: 2694
	A_SuperSaiyan,
	// Token: 0x04000A87 RID: 2695
	A_KamehamehaWave,
	// Token: 0x04000A88 RID: 2696
	A_Henshin,
	// Token: 0x04000A89 RID: 2697
	A_Rachel,
	// Token: 0x04000A8A RID: 2698
	A_Rasengan,
	// Token: 0x04000A8B RID: 2699
	A_IceWall,
	// Token: 0x04000A8C RID: 2700
	A_BlackCoffin,
	// Token: 0x04000A8D RID: 2701
	A_FireStep,
	// Token: 0x04000A8E RID: 2702
	A_SwordOfLight,
	// Token: 0x04000A8F RID: 2703
	A_LightningChain,
	// Token: 0x04000A90 RID: 2704
	A_LandMine,
	// Token: 0x04000A91 RID: 2705
	A_DarkMonster,
	// Token: 0x04000A92 RID: 2706
	A_HenshinLight,
	// Token: 0x04000A93 RID: 2707
	A_Peashooter,
	// Token: 0x04000A94 RID: 2708
	A_BloodSacrifice,
	// Token: 0x04000A95 RID: 2709
	A_Alchemy,
	// Token: 0x04000A96 RID: 2710
	A_Execution,
	// Token: 0x04000A97 RID: 2711
	A_Sunflower,
	// Token: 0x04000A98 RID: 2712
	A_SummomTree,
	// Token: 0x04000A99 RID: 2713
	A_AvatarDodge,
	// Token: 0x04000A9A RID: 2714
	A_DeathCyclone,
	// Token: 0x04000A9B RID: 2715
	A_PoisionAoe,
	// Token: 0x04000A9C RID: 2716
	S_SpellThunder = 400,
	// Token: 0x04000A9D RID: 2717
	S_SwordMove,
	// Token: 0x04000A9E RID: 2718
	S_SummonKight = 403,
	// Token: 0x04000A9F RID: 2719
	S_BlackHole,
	// Token: 0x04000AA0 RID: 2720
	S_ContinuousLight,
	// Token: 0x04000AA1 RID: 2721
	S_Flameshrower,
	// Token: 0x04000AA2 RID: 2722
	S_SurroundingFire,
	// Token: 0x04000AA3 RID: 2723
	S_KingsTreasure,
	// Token: 0x04000AA4 RID: 2724
	S_Whirlwind,
	// Token: 0x04000AA5 RID: 2725
	S_FireTornado,
	// Token: 0x04000AA6 RID: 2726
	S_WindBreakSlash,
	// Token: 0x04000AA7 RID: 2727
	S_StarBurstStream,
	// Token: 0x04000AA8 RID: 2728
	S_Sacrifice,
	// Token: 0x04000AA9 RID: 2729
	S_SuperSaiyan,
	// Token: 0x04000AAA RID: 2730
	S_KamehamehaWave,
	// Token: 0x04000AAB RID: 2731
	S_Henshin,
	// Token: 0x04000AAC RID: 2732
	S_Rachel,
	// Token: 0x04000AAD RID: 2733
	S_Rasengan,
	// Token: 0x04000AAE RID: 2734
	S_IceWall,
	// Token: 0x04000AAF RID: 2735
	S_BlackCoffin,
	// Token: 0x04000AB0 RID: 2736
	S_FireStep,
	// Token: 0x04000AB1 RID: 2737
	S_SwordOfLight,
	// Token: 0x04000AB2 RID: 2738
	S_LightningChain,
	// Token: 0x04000AB3 RID: 2739
	S_LandMine,
	// Token: 0x04000AB4 RID: 2740
	S_DarkMonster,
	// Token: 0x04000AB5 RID: 2741
	S_HenshinLight,
	// Token: 0x04000AB6 RID: 2742
	S_Peashooter,
	// Token: 0x04000AB7 RID: 2743
	S_BloodSacrifice,
	// Token: 0x04000AB8 RID: 2744
	S_Alchemy,
	// Token: 0x04000AB9 RID: 2745
	S_Execution,
	// Token: 0x04000ABA RID: 2746
	S_Sunflower,
	// Token: 0x04000ABB RID: 2747
	S_SummomTree,
	// Token: 0x04000ABC RID: 2748
	S_AvatarDodge,
	// Token: 0x04000ABD RID: 2749
	S_DeathCyclone,
	// Token: 0x04000ABE RID: 2750
	S_PoisionAoe,
	// Token: 0x04000ABF RID: 2751
	Hero_Blink = 500,
	// Token: 0x04000AC0 RID: 2752
	Hero_DrawKnife,
	// Token: 0x04000AC1 RID: 2753
	Hero_PlayMusic,
	// Token: 0x04000AC2 RID: 2754
	Hero_FieldExpansion,
	// Token: 0x04000AC3 RID: 2755
	Hero_Roll,
	// Token: 0x04000AC4 RID: 2756
	Hero_Titan,
	// Token: 0x04000AC5 RID: 2757
	XieHuangBao,
	// Token: 0x04000AC6 RID: 2758
	SummonNezuko,
	// Token: 0x04000AC7 RID: 2759
	CopyNinja,
	// Token: 0x04000AC8 RID: 2760
	ChickenDance,
	// Token: 0x04000AC9 RID: 2761
	HollofiedIchigo,
	// Token: 0x04000ACA RID: 2762
	ShadowCloneTechnique,
	// Token: 0x04000ACB RID: 2763
	SoulDevourer,
	// Token: 0x04000ACC RID: 2764
	PlantBomb,
	// Token: 0x04000ACD RID: 2765
	Boss_SwordMove = 600,
	// Token: 0x04000ACE RID: 2766
	HellFire_Call,
	// Token: 0x04000ACF RID: 2767
	D_Kight_Sword,
	// Token: 0x04000AD0 RID: 2768
	C_Kight_Sword,
	// Token: 0x04000AD1 RID: 2769
	B_Kight_Sword,
	// Token: 0x04000AD2 RID: 2770
	A_Kight_Sword,
	// Token: 0x04000AD3 RID: 2771
	S_Kight_Sword,
	// Token: 0x04000AD4 RID: 2772
	Elder_Wave,
	// Token: 0x04000AD5 RID: 2773
	MoonlightGreatsword,
	// Token: 0x04000AD6 RID: 2774
	DrangonFireBoom,
	// Token: 0x04000AD7 RID: 2775
	FireDaggers,
	// Token: 0x04000AD8 RID: 2776
	ChargeBoom,
	// Token: 0x04000AD9 RID: 2777
	SaiyaCall,
	// Token: 0x04000ADA RID: 2778
	PlayerDrangonFireBoom,
	// Token: 0x04000ADB RID: 2779
	PlayerDrangonFireBoomEnd,
	// Token: 0x04000ADC RID: 2780
	RockFall,
	// Token: 0x04000ADD RID: 2781
	NecromancerCall,
	// Token: 0x04000ADE RID: 2782
	IceGround,
	// Token: 0x04000ADF RID: 2783
	GuardianBullet,
	// Token: 0x04000AE0 RID: 2784
	TrapSpears = 700
}
