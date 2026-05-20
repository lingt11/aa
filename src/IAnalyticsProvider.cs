using System;

// Token: 0x02000048 RID: 72
public interface IAnalyticsProvider
{
	// Token: 0x06000127 RID: 295
	void Initialize();

	// Token: 0x06000128 RID: 296
	void TrackDesignEvent(string eventId, float value);

	// Token: 0x06000129 RID: 297
	void Flush();
}
