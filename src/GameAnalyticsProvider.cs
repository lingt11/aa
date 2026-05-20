using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class GameAnalyticsProvider : IAnalyticsProvider
{
	// Token: 0x0600012A RID: 298 RVA: 0x00007BF8 File Offset: 0x00005DF8
	public void Initialize()
	{
		this.EnsureMethods();
		if (this.analyticsType == null || this.initializeMethod == null)
		{
			this.WarnMissingSdk();
			return;
		}
		this.DisableAutomaticUnityErrorSubmission();
		if (this.IsInitialized())
		{
			this.hasInitialized = true;
			this.RegisterFilteredErrorListener();
			return;
		}
		if (!this.HasAnyConfiguredPlatformKey())
		{
			if (!this.hasInitWarned)
			{
				this.hasInitWarned = true;
				Debug.Log("GameAnalytics 已检测到 SDK，但 Settings 里还没有有效的 Game Key / Secret Key，暂时跳过初始化。");
			}
			return;
		}
		this.EnsureAnalyticsObject();
		this.initializeMethod.Invoke(null, null);
		this.hasInitialized = true;
		this.RegisterFilteredErrorListener();
		Debug.Log("GameAnalytics SDK 初始化完成。");
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00007C98 File Offset: 0x00005E98
	public void TrackDesignEvent(string eventId, float value)
	{
		this.EnsureMethods();
		if (this.newDesignEventMethod == null && this.newDesignEventWithValueMethod == null)
		{
			this.WarnMissingSdk();
			return;
		}
		if (!this.hasInitialized && !this.IsInitialized())
		{
			this.Initialize();
			if (!this.hasInitialized && !this.IsInitialized())
			{
				return;
			}
		}
		if (this.newDesignEventWithValueMethod != null)
		{
			this.newDesignEventWithValueMethod.Invoke(null, new object[]
			{
				eventId,
				value
			});
			return;
		}
		MethodInfo methodInfo = this.newDesignEventMethod;
		if (methodInfo == null)
		{
			return;
		}
		methodInfo.Invoke(null, new object[]
		{
			eventId
		});
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00007D40 File Offset: 0x00005F40
	public void Flush()
	{
		Application.logMessageReceived -= this.HandleUnityLog;
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00007D53 File Offset: 0x00005F53
	private bool IsInitialized()
	{
		return this.initializedProperty != null && (bool)this.initializedProperty.GetValue(null, null);
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00007D77 File Offset: 0x00005F77
	private void EnsureAnalyticsObject()
	{
		if (Object.FindObjectOfType(this.analyticsType) != null)
		{
			return;
		}
		GameObject gameObject = new GameObject("GameAnalytics");
		Object.DontDestroyOnLoad(gameObject);
		gameObject.AddComponent(this.analyticsType);
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00007DAC File Offset: 0x00005FAC
	private void DisableAutomaticUnityErrorSubmission()
	{
		if (this.settingsProperty == null)
		{
			return;
		}
		object value = this.settingsProperty.GetValue(null, null);
		if (value == null)
		{
			return;
		}
		FieldInfo field = value.GetType().GetField("SubmitErrors", BindingFlags.Instance | BindingFlags.Public);
		if (field == null)
		{
			return;
		}
		field.SetValue(value, false);
	}

	// Token: 0x06000130 RID: 304 RVA: 0x00007DFD File Offset: 0x00005FFD
	private void RegisterFilteredErrorListener()
	{
		Application.logMessageReceived -= this.HandleUnityLog;
		Application.logMessageReceived += this.HandleUnityLog;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00007E24 File Offset: 0x00006024
	private void HandleUnityLog(string logString, string stackTrace, LogType type)
	{
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00007E34 File Offset: 0x00006034
	private string BuildErrorMessage(string logString, string stackTrace)
	{
		string text = string.IsNullOrEmpty(logString) ? string.Empty : logString.Replace('"', '\'').Replace('\n', ' ').Replace('\r', ' ');
		string text2 = string.IsNullOrEmpty(stackTrace) ? string.Empty : stackTrace.Replace('"', '\'').Replace('\n', ' ').Replace('\r', ' ');
		string text3 = string.IsNullOrEmpty(text2) ? text : (text + " " + text2);
		if (text3.Length > 8192)
		{
			text3 = text3.Substring(0, 8191);
		}
		return text3;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00007ECC File Offset: 0x000060CC
	private bool HasAnyConfiguredPlatformKey()
	{
		if (this.settingsProperty == null)
		{
			return false;
		}
		object value = this.settingsProperty.GetValue(null, null);
		if (value == null)
		{
			return false;
		}
		Type type = value.GetType();
		FieldInfo field = type.GetField("Platforms", BindingFlags.Instance | BindingFlags.Public);
		MethodInfo method = type.GetMethod("GetGameKey", BindingFlags.Instance | BindingFlags.Public);
		MethodInfo method2 = type.GetMethod("GetSecretKey", BindingFlags.Instance | BindingFlags.Public);
		if (field == null || method == null || method2 == null)
		{
			return false;
		}
		IList list = field.GetValue(value) as IList;
		if (list == null)
		{
			return false;
		}
		for (int i = 0; i < list.Count; i++)
		{
			string value2 = method.Invoke(value, new object[]
			{
				i
			}) as string;
			string value3 = method2.Invoke(value, new object[]
			{
				i
			}) as string;
			if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(value3))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00007FC4 File Offset: 0x000061C4
	private void EnsureMethods()
	{
		if (this.hasSearched)
		{
			return;
		}
		this.hasSearched = true;
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		int i = 0;
		while (i < assemblies.Length)
		{
			this.analyticsType = assemblies[i].GetType("GameAnalyticsSDK.GameAnalytics");
			if (!(this.analyticsType == null))
			{
				this.errorSeverityType = assemblies[i].GetType("GameAnalyticsSDK.GAErrorSeverity");
				this.initializedProperty = this.analyticsType.GetProperty("Initialized", BindingFlags.Static | BindingFlags.Public);
				this.settingsProperty = this.analyticsType.GetProperty("SettingsGA", BindingFlags.Static | BindingFlags.Public);
				this.initializeMethod = this.analyticsType.GetMethod("Initialize", BindingFlags.Static | BindingFlags.Public, null, Type.EmptyTypes, null);
				this.newDesignEventMethod = this.analyticsType.GetMethod("NewDesignEvent", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(string)
				}, null);
				this.newDesignEventWithValueMethod = this.analyticsType.GetMethod("NewDesignEvent", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(string),
					typeof(float)
				}, null);
				if (this.errorSeverityType != null)
				{
					this.newErrorEventMethod = this.analyticsType.GetMethod("NewErrorEvent", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						this.errorSeverityType,
						typeof(string)
					}, null);
					return;
				}
				break;
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x06000135 RID: 309 RVA: 0x00008132 File Offset: 0x00006332
	private void WarnMissingSdk()
	{
		if (this.hasWarned)
		{
			return;
		}
		this.hasWarned = true;
		Debug.LogWarning("GameAnalytics SDK 未导入或未就绪，当前埋点会跳过发送。");
	}

	// Token: 0x04000149 RID: 329
	private const bool EnableErrorEventUpload = false;

	// Token: 0x0400014A RID: 330
	private const int MaxCustomErrorCount = 20;

	// Token: 0x0400014B RID: 331
	private bool hasSearched;

	// Token: 0x0400014C RID: 332
	private bool hasWarned;

	// Token: 0x0400014D RID: 333
	private bool hasInitialized;

	// Token: 0x0400014E RID: 334
	private bool hasInitWarned;

	// Token: 0x0400014F RID: 335
	private int submittedErrorCount;

	// Token: 0x04000150 RID: 336
	private Type analyticsType;

	// Token: 0x04000151 RID: 337
	private Type errorSeverityType;

	// Token: 0x04000152 RID: 338
	private PropertyInfo initializedProperty;

	// Token: 0x04000153 RID: 339
	private PropertyInfo settingsProperty;

	// Token: 0x04000154 RID: 340
	private MethodInfo initializeMethod;

	// Token: 0x04000155 RID: 341
	private MethodInfo newDesignEventMethod;

	// Token: 0x04000156 RID: 342
	private MethodInfo newDesignEventWithValueMethod;

	// Token: 0x04000157 RID: 343
	private MethodInfo newErrorEventMethod;
}
