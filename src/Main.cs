using System;

// Token: 0x02000082 RID: 130
public class Main
{
	// Token: 0x060002F4 RID: 756 RVA: 0x00014BAC File Offset: 0x00012DAC
	public static void Init()
	{
		new MySystemEvent();
		Game.Init();
		CodeLoader instance = CodeLoader.Instance;
		instance.Update = (Action)Delegate.Combine(instance.Update, new Action(delegate()
		{
			Game.Update();
		}));
		CodeLoader instance2 = CodeLoader.Instance;
		instance2.FixedUpdate = (Action)Delegate.Combine(instance2.FixedUpdate, new Action(delegate()
		{
			Game.FixedUpdate();
		}));
		CodeLoader instance3 = CodeLoader.Instance;
		instance3.LateUpdate = (Action)Delegate.Combine(instance3.LateUpdate, new Action(delegate()
		{
			Game.LateUpdate();
		}));
		CodeLoader instance4 = CodeLoader.Instance;
		instance4.OnApplicationQuit = (Action)Delegate.Combine(instance4.OnApplicationQuit, new Action(delegate()
		{
			Game.OnApplicationQuit();
		}));
		EntityStatic.Get<MainLogic>().Init();
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00014CB4 File Offset: 0x00012EB4
	public static void Clear()
	{
		CodeLoader.Instance.Update = delegate()
		{
		};
		CodeLoader.Instance.FixedUpdate = delegate()
		{
		};
		CodeLoader.Instance.LateUpdate = delegate()
		{
		};
		CodeLoader.Instance.OnApplicationQuit = delegate()
		{
		};
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x00014D65 File Offset: 0x00012F65
	public static void Quit()
	{
		Game.OnApplicationQuit();
	}
}
