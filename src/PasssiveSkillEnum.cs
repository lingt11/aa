using System;

// Token: 0x02000155 RID: 341
public enum PasssiveSkillEnum
{
	// Token: 0x04000969 RID: 2409
	None = -1,
	// Token: 0x0400096A RID: 2410
	D属性加强,
	// Token: 0x0400096B RID: 2411
	D小闪避,
	// Token: 0x0400096C RID: 2412
	D重击,
	// Token: 0x0400096D RID: 2413
	D长臂猿,
	// Token: 0x0400096E RID: 2414
	D小斩杀,
	// Token: 0x0400096F RID: 2415
	D大树祝福,
	// Token: 0x04000970 RID: 2416
	D小附魔武器,
	// Token: 0x04000971 RID: 2417
	D小凋零打击,
	// Token: 0x04000972 RID: 2418
	D小刀扇装甲,
	// Token: 0x04000973 RID: 2419
	D先发制人,
	// Token: 0x04000974 RID: 2420
	D杀怪加钱,
	// Token: 0x04000975 RID: 2421
	D荆棘皮肤,
	// Token: 0x04000976 RID: 2422
	D微小吸收,
	// Token: 0x04000977 RID: 2423
	D攻击加钱,
	// Token: 0x04000978 RID: 2424
	D没用,
	// Token: 0x04000979 RID: 2425
	D蓝条加成,
	// Token: 0x0400097A RID: 2426
	D攻击加成,
	// Token: 0x0400097B RID: 2427
	D力量加成,
	// Token: 0x0400097C RID: 2428
	D敏捷加成,
	// Token: 0x0400097D RID: 2429
	D耐力加成,
	// Token: 0x0400097E RID: 2430
	D暴击加成,
	// Token: 0x0400097F RID: 2431
	D暴击伤害,
	// Token: 0x04000980 RID: 2432
	D攻速加成,
	// Token: 0x04000981 RID: 2433
	D移速加成,
	// Token: 0x04000982 RID: 2434
	D召唤物加成,
	// Token: 0x04000983 RID: 2435
	D猎物标记斩,
	// Token: 0x04000984 RID: 2436
	D猎物标记破,
	// Token: 0x04000985 RID: 2437
	D火之呼吸,
	// Token: 0x04000986 RID: 2438
	D攻速光环,
	// Token: 0x04000987 RID: 2439
	D应急护盾,
	// Token: 0x04000988 RID: 2440
	D无极剑道,
	// Token: 0x04000989 RID: 2441
	D杀人书,
	// Token: 0x0400098A RID: 2442
	D苦难光环,
	// Token: 0x0400098B RID: 2443
	D涂毒,
	// Token: 0x0400098C RID: 2444
	D剑灵,
	// Token: 0x0400098D RID: 2445
	D枪灵,
	// Token: 0x0400098E RID: 2446
	DRPG,
	// Token: 0x0400098F RID: 2447
	D喷火器,
	// Token: 0x04000990 RID: 2448
	C小重生 = 100,
	// Token: 0x04000991 RID: 2449
	C属性加强,
	// Token: 0x04000992 RID: 2450
	C闪避,
	// Token: 0x04000993 RID: 2451
	C斩杀,
	// Token: 0x04000994 RID: 2452
	C小吸血鬼,
	// Token: 0x04000995 RID: 2453
	C小分裂攻击,
	// Token: 0x04000996 RID: 2454
	C杀怪加很多钱,
	// Token: 0x04000997 RID: 2455
	C小火锅,
	// Token: 0x04000998 RID: 2456
	C大树祝福,
	// Token: 0x04000999 RID: 2457
	C攻击加很多钱,
	// Token: 0x0400099A RID: 2458
	C附魔武器,
	// Token: 0x0400099B RID: 2459
	C凋零打击,
	// Token: 0x0400099C RID: 2460
	C刀扇装甲,
	// Token: 0x0400099D RID: 2461
	C大先发制人,
	// Token: 0x0400099E RID: 2462
	C大荆棘皮肤,
	// Token: 0x0400099F RID: 2463
	C重击,
	// Token: 0x040009A0 RID: 2464
	C超长臂猿,
	// Token: 0x040009A1 RID: 2465
	C群众效应,
	// Token: 0x040009A2 RID: 2466
	C蓝条加成,
	// Token: 0x040009A3 RID: 2467
	C攻击加成,
	// Token: 0x040009A4 RID: 2468
	C力量加成,
	// Token: 0x040009A5 RID: 2469
	C敏捷加成,
	// Token: 0x040009A6 RID: 2470
	C耐力加成,
	// Token: 0x040009A7 RID: 2471
	C暴击加成,
	// Token: 0x040009A8 RID: 2472
	C暴击伤害,
	// Token: 0x040009A9 RID: 2473
	C攻速加成,
	// Token: 0x040009AA RID: 2474
	C移速加成,
	// Token: 0x040009AB RID: 2475
	C召唤物加成,
	// Token: 0x040009AC RID: 2476
	C猎物标记斩,
	// Token: 0x040009AD RID: 2477
	C猎物标记破,
	// Token: 0x040009AE RID: 2478
	C火之呼吸,
	// Token: 0x040009AF RID: 2479
	C攻速光环,
	// Token: 0x040009B0 RID: 2480
	C应急护盾,
	// Token: 0x040009B1 RID: 2481
	C无极剑道,
	// Token: 0x040009B2 RID: 2482
	C杀人书,
	// Token: 0x040009B3 RID: 2483
	C苦难光环,
	// Token: 0x040009B4 RID: 2484
	C涂毒,
	// Token: 0x040009B5 RID: 2485
	C剑灵,
	// Token: 0x040009B6 RID: 2486
	C枪灵,
	// Token: 0x040009B7 RID: 2487
	CRPG,
	// Token: 0x040009B8 RID: 2488
	C喷火器,
	// Token: 0x040009B9 RID: 2489
	B重生 = 200,
	// Token: 0x040009BA RID: 2490
	B属性加强,
	// Token: 0x040009BB RID: 2491
	B闪避,
	// Token: 0x040009BC RID: 2492
	B真超长臂猿,
	// Token: 0x040009BD RID: 2493
	B大火锅,
	// Token: 0x040009BE RID: 2494
	B吸血鬼,
	// Token: 0x040009BF RID: 2495
	B杀怪加超多钱,
	// Token: 0x040009C0 RID: 2496
	B攻击加超多钱,
	// Token: 0x040009C1 RID: 2497
	B高级活性护甲,
	// Token: 0x040009C2 RID: 2498
	B分裂攻击,
	// Token: 0x040009C3 RID: 2499
	B多重攻击,
	// Token: 0x040009C4 RID: 2500
	B大斩杀,
	// Token: 0x040009C5 RID: 2501
	B大刀扇装甲,
	// Token: 0x040009C6 RID: 2502
	B超荆棘皮肤,
	// Token: 0x040009C7 RID: 2503
	B哥布林帅哥,
	// Token: 0x040009C8 RID: 2504
	B重击,
	// Token: 0x040009C9 RID: 2505
	B大树祝福,
	// Token: 0x040009CA RID: 2506
	B蓝条加成,
	// Token: 0x040009CB RID: 2507
	B攻击加成,
	// Token: 0x040009CC RID: 2508
	B力量加成,
	// Token: 0x040009CD RID: 2509
	B敏捷加成,
	// Token: 0x040009CE RID: 2510
	B耐力加成,
	// Token: 0x040009CF RID: 2511
	B暴击加成,
	// Token: 0x040009D0 RID: 2512
	B暴击伤害,
	// Token: 0x040009D1 RID: 2513
	B攻速加成,
	// Token: 0x040009D2 RID: 2514
	B移速加成,
	// Token: 0x040009D3 RID: 2515
	B召唤物加成,
	// Token: 0x040009D4 RID: 2516
	B猎物标记斩,
	// Token: 0x040009D5 RID: 2517
	B猎物标记破,
	// Token: 0x040009D6 RID: 2518
	B火之呼吸,
	// Token: 0x040009D7 RID: 2519
	B攻速光环,
	// Token: 0x040009D8 RID: 2520
	B应急护盾,
	// Token: 0x040009D9 RID: 2521
	B无极剑道,
	// Token: 0x040009DA RID: 2522
	B杀人书,
	// Token: 0x040009DB RID: 2523
	B苦难光环,
	// Token: 0x040009DC RID: 2524
	B涂毒,
	// Token: 0x040009DD RID: 2525
	B剑灵,
	// Token: 0x040009DE RID: 2526
	B枪灵,
	// Token: 0x040009DF RID: 2527
	BRPG,
	// Token: 0x040009E0 RID: 2528
	B喷火器,
	// Token: 0x040009E1 RID: 2529
	A装逼 = 300,
	// Token: 0x040009E2 RID: 2530
	A苦难光环,
	// Token: 0x040009E3 RID: 2531
	A几乎无限重生,
	// Token: 0x040009E4 RID: 2532
	A闪避,
	// Token: 0x040009E5 RID: 2533
	A杀怪加超级巨多钱,
	// Token: 0x040009E6 RID: 2534
	A究超长臂猿,
	// Token: 0x040009E7 RID: 2535
	A攻击加超级巨多钱,
	// Token: 0x040009E8 RID: 2536
	A多重攻击,
	// Token: 0x040009E9 RID: 2537
	A吸血鬼,
	// Token: 0x040009EA RID: 2538
	A大分裂攻击,
	// Token: 0x040009EB RID: 2539
	A超级火锅,
	// Token: 0x040009EC RID: 2540
	A龟壳,
	// Token: 0x040009ED RID: 2541
	A涂毒,
	// Token: 0x040009EE RID: 2542
	A剑灵,
	// Token: 0x040009EF RID: 2543
	A枪灵,
	// Token: 0x040009F0 RID: 2544
	ARPG,
	// Token: 0x040009F1 RID: 2545
	A喷火器,
	// Token: 0x040009F2 RID: 2546
	S闪避 = 400,
	// Token: 0x040009F3 RID: 2547
	S超_苦难光环,
	// Token: 0x040009F4 RID: 2548
	S属性加强,
	// Token: 0x040009F5 RID: 2549
	S多重攻击,
	// Token: 0x040009F6 RID: 2550
	S超分裂攻击,
	// Token: 0x040009F7 RID: 2551
	S财富自由群员,
	// Token: 0x040009F8 RID: 2552
	S究极护甲,
	// Token: 0x040009F9 RID: 2553
	S涂毒,
	// Token: 0x040009FA RID: 2554
	S剑灵,
	// Token: 0x040009FB RID: 2555
	S枪灵,
	// Token: 0x040009FC RID: 2556
	SRPG,
	// Token: 0x040009FD RID: 2557
	S喷火器,
	// Token: 0x040009FE RID: 2558
	H重金悬赏 = 500,
	// Token: 0x040009FF RID: 2559
	H起洞,
	// Token: 0x04000A00 RID: 2560
	H量子阅读,
	// Token: 0x04000A01 RID: 2561
	H复仇电锯,
	// Token: 0x04000A02 RID: 2562
	H内部折扣,
	// Token: 0x04000A03 RID: 2563
	H凝视黑暗,
	// Token: 0x04000A04 RID: 2564
	H对死者的供奉,
	// Token: 0x04000A05 RID: 2565
	HAK4鸡,
	// Token: 0x04000A06 RID: 2566
	H二刀流,
	// Token: 0x04000A07 RID: 2567
	H大魔法使,
	// Token: 0x04000A08 RID: 2568
	H赛亚人之血,
	// Token: 0x04000A09 RID: 2569
	H电锯恶魔,
	// Token: 0x04000A0A RID: 2570
	H忍者旋风,
	// Token: 0x04000A0B RID: 2571
	H禁忌魔法,
	// Token: 0x04000A0C RID: 2572
	H三级除草证,
	// Token: 0x04000A0D RID: 2573
	H橡胶果实,
	// Token: 0x04000A0E RID: 2574
	H死亡笔记,
	// Token: 0x04000A0F RID: 2575
	H战斗龙卷风,
	// Token: 0x04000A10 RID: 2576
	H土豆兄弟
}
