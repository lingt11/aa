using System;

namespace RVO
{
	// Token: 0x0200048E RID: 1166
	internal class Obstacle
	{
		// Token: 0x04001953 RID: 6483
		internal Obstacle next_;

		// Token: 0x04001954 RID: 6484
		internal Obstacle previous_;

		// Token: 0x04001955 RID: 6485
		internal Vector2 direction_;

		// Token: 0x04001956 RID: 6486
		internal Vector2 point_;

		// Token: 0x04001957 RID: 6487
		internal int id_;

		// Token: 0x04001958 RID: 6488
		internal bool convex_;
	}
}
