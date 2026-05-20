using System;
using System.Collections.Generic;

namespace RVO
{
	// Token: 0x02000489 RID: 1161
	internal class KdTree
	{
		// Token: 0x060019DC RID: 6620 RVA: 0x0009FF44 File Offset: 0x0009E144
		internal void buildAgentTree()
		{
			if (this.agents_ == null || this.agents_.Length != Simulator.Instance.agents_.Count)
			{
				this.agents_ = new Agent[Simulator.Instance.agents_.Count];
				for (int i = 0; i < this.agents_.Length; i++)
				{
					this.agents_[i] = Simulator.Instance.agents_[i];
				}
				this.agentTree_ = new KdTree.AgentTreeNode[2 * this.agents_.Length];
				for (int j = 0; j < this.agentTree_.Length; j++)
				{
					this.agentTree_[j] = default(KdTree.AgentTreeNode);
				}
			}
			if (this.agents_.Length != 0)
			{
				this.buildAgentTreeRecursive(0, this.agents_.Length, 0);
			}
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x000A000C File Offset: 0x0009E20C
		internal void buildObstacleTree()
		{
			this.obstacleTree_ = new KdTree.ObstacleTreeNode();
			IList<Obstacle> list = new List<Obstacle>(Simulator.Instance.obstacles_.Count);
			for (int i = 0; i < Simulator.Instance.obstacles_.Count; i++)
			{
				list.Add(Simulator.Instance.obstacles_[i]);
			}
			this.obstacleTree_ = this.buildObstacleTreeRecursive(list);
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x000A0076 File Offset: 0x0009E276
		internal void computeAgentNeighbors(Agent agent, ref float rangeSq)
		{
			this.queryAgentTreeRecursive(agent, ref rangeSq, 0);
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000A0081 File Offset: 0x0009E281
		internal void computeObstacleNeighbors(Agent agent, float rangeSq)
		{
			this.queryObstacleTreeRecursive(agent, rangeSq, this.obstacleTree_);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x000A0091 File Offset: 0x0009E291
		internal bool queryVisibility(Vector2 q1, Vector2 q2, float radius)
		{
			return this.queryVisibilityRecursive(q1, q2, radius, this.obstacleTree_);
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x000A00A4 File Offset: 0x0009E2A4
		internal int queryNearAgent(Vector2 point, float radius)
		{
			float maxValue = float.MaxValue;
			int result = -1;
			this.queryAgentTreeRecursive(point, ref maxValue, ref result, 0);
			if (maxValue < radius * radius)
			{
				return result;
			}
			return -1;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000A00D0 File Offset: 0x0009E2D0
		private void buildAgentTreeRecursive(int begin, int end, int node)
		{
			this.agentTree_[node].begin_ = begin;
			this.agentTree_[node].end_ = end;
			this.agentTree_[node].minX_ = (this.agentTree_[node].maxX_ = this.agents_[begin].position_.x_);
			this.agentTree_[node].minY_ = (this.agentTree_[node].maxY_ = this.agents_[begin].position_.y_);
			for (int i = begin + 1; i < end; i++)
			{
				this.agentTree_[node].maxX_ = Math.Max(this.agentTree_[node].maxX_, this.agents_[i].position_.x_);
				this.agentTree_[node].minX_ = Math.Min(this.agentTree_[node].minX_, this.agents_[i].position_.x_);
				this.agentTree_[node].maxY_ = Math.Max(this.agentTree_[node].maxY_, this.agents_[i].position_.y_);
				this.agentTree_[node].minY_ = Math.Min(this.agentTree_[node].minY_, this.agents_[i].position_.y_);
			}
			if (end - begin > 10)
			{
				bool flag = this.agentTree_[node].maxX_ - this.agentTree_[node].minX_ > this.agentTree_[node].maxY_ - this.agentTree_[node].minY_;
				float num = 0.5f * (flag ? (this.agentTree_[node].maxX_ + this.agentTree_[node].minX_) : (this.agentTree_[node].maxY_ + this.agentTree_[node].minY_));
				int j = begin;
				int num2 = end;
				while (j < num2)
				{
					while (j < num2)
					{
						if ((flag ? this.agents_[j].position_.x_ : this.agents_[j].position_.y_) >= num)
						{
							break;
						}
						j++;
					}
					while (num2 > j && (flag ? this.agents_[num2 - 1].position_.x_ : this.agents_[num2 - 1].position_.y_) >= num)
					{
						num2--;
					}
					if (j < num2)
					{
						Agent agent = this.agents_[j];
						this.agents_[j] = this.agents_[num2 - 1];
						this.agents_[num2 - 1] = agent;
						j++;
						num2--;
					}
				}
				int num3 = j - begin;
				if (num3 == 0)
				{
					num3++;
					j++;
					num2++;
				}
				this.agentTree_[node].left_ = node + 1;
				this.agentTree_[node].right_ = node + 2 * num3;
				this.buildAgentTreeRecursive(begin, j, this.agentTree_[node].left_);
				this.buildAgentTreeRecursive(j, end, this.agentTree_[node].right_);
			}
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000A0454 File Offset: 0x0009E654
		private KdTree.ObstacleTreeNode buildObstacleTreeRecursive(IList<Obstacle> obstacles)
		{
			if (obstacles.Count == 0)
			{
				return null;
			}
			KdTree.ObstacleTreeNode obstacleTreeNode = new KdTree.ObstacleTreeNode();
			int num = 0;
			int num2 = obstacles.Count;
			int num3 = obstacles.Count;
			for (int i = 0; i < obstacles.Count; i++)
			{
				int num4 = 0;
				int num5 = 0;
				Obstacle obstacle = obstacles[i];
				Obstacle next_ = obstacle.next_;
				for (int j = 0; j < obstacles.Count; j++)
				{
					if (i != j)
					{
						Obstacle obstacle2 = obstacles[j];
						Obstacle next_2 = obstacle2.next_;
						float num6 = RVOMath.leftOf(obstacle.point_, next_.point_, obstacle2.point_);
						float num7 = RVOMath.leftOf(obstacle.point_, next_.point_, next_2.point_);
						if (num6 >= -1E-05f && num7 >= -1E-05f)
						{
							num4++;
						}
						else if (num6 <= 1E-05f && num7 <= 1E-05f)
						{
							num5++;
						}
						else
						{
							num4++;
							num5++;
						}
						if (new KdTree.FloatPair((float)Math.Max(num4, num5), (float)Math.Min(num4, num5)) >= new KdTree.FloatPair((float)Math.Max(num2, num3), (float)Math.Min(num2, num3)))
						{
							break;
						}
					}
				}
				if (new KdTree.FloatPair((float)Math.Max(num4, num5), (float)Math.Min(num4, num5)) < new KdTree.FloatPair((float)Math.Max(num2, num3), (float)Math.Min(num2, num3)))
				{
					num2 = num4;
					num3 = num5;
					num = i;
				}
			}
			IList<Obstacle> list = new List<Obstacle>(num2);
			for (int k = 0; k < num2; k++)
			{
				list.Add(null);
			}
			IList<Obstacle> list2 = new List<Obstacle>(num3);
			for (int l = 0; l < num3; l++)
			{
				list2.Add(null);
			}
			int num8 = 0;
			int num9 = 0;
			int num10 = num;
			Obstacle obstacle3 = obstacles[num10];
			Obstacle next_3 = obstacle3.next_;
			for (int m = 0; m < obstacles.Count; m++)
			{
				if (num10 != m)
				{
					Obstacle obstacle4 = obstacles[m];
					Obstacle next_4 = obstacle4.next_;
					float num11 = RVOMath.leftOf(obstacle3.point_, next_3.point_, obstacle4.point_);
					float num12 = RVOMath.leftOf(obstacle3.point_, next_3.point_, next_4.point_);
					if (num11 >= -1E-05f && num12 >= -1E-05f)
					{
						list[num8++] = obstacles[m];
					}
					else if (num11 <= 1E-05f && num12 <= 1E-05f)
					{
						list2[num9++] = obstacles[m];
					}
					else
					{
						float scalar = RVOMath.det(next_3.point_ - obstacle3.point_, obstacle4.point_ - obstacle3.point_) / RVOMath.det(next_3.point_ - obstacle3.point_, obstacle4.point_ - next_4.point_);
						Vector2 point_ = obstacle4.point_ + scalar * (next_4.point_ - obstacle4.point_);
						Obstacle obstacle5 = new Obstacle();
						obstacle5.point_ = point_;
						obstacle5.previous_ = obstacle4;
						obstacle5.next_ = next_4;
						obstacle5.convex_ = true;
						obstacle5.direction_ = obstacle4.direction_;
						obstacle5.id_ = Simulator.Instance.obstacles_.Count;
						Simulator.Instance.obstacles_.Add(obstacle5);
						obstacle4.next_ = obstacle5;
						next_4.previous_ = obstacle5;
						if (num11 > 0f)
						{
							list[num8++] = obstacle4;
							list2[num9++] = obstacle5;
						}
						else
						{
							list2[num9++] = obstacle4;
							list[num8++] = obstacle5;
						}
					}
				}
			}
			obstacleTreeNode.obstacle_ = obstacle3;
			obstacleTreeNode.left_ = this.buildObstacleTreeRecursive(list);
			obstacleTreeNode.right_ = this.buildObstacleTreeRecursive(list2);
			return obstacleTreeNode;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x000A0860 File Offset: 0x0009EA60
		private void queryAgentTreeRecursive(Vector2 position, ref float rangeSq, ref int agentNo, int node)
		{
			if (this.agentTree_[node].end_ - this.agentTree_[node].begin_ <= 10)
			{
				for (int i = this.agentTree_[node].begin_; i < this.agentTree_[node].end_; i++)
				{
					float num = RVOMath.absSq(position - this.agents_[i].position_);
					if (num < rangeSq)
					{
						rangeSq = num;
						agentNo = this.agents_[i].id_;
					}
				}
				return;
			}
			float num2 = RVOMath.sqr(Math.Max(0f, this.agentTree_[this.agentTree_[node].left_].minX_ - position.x_)) + RVOMath.sqr(Math.Max(0f, position.x_ - this.agentTree_[this.agentTree_[node].left_].maxX_)) + RVOMath.sqr(Math.Max(0f, this.agentTree_[this.agentTree_[node].left_].minY_ - position.y_)) + RVOMath.sqr(Math.Max(0f, position.y_ - this.agentTree_[this.agentTree_[node].left_].maxY_));
			float num3 = RVOMath.sqr(Math.Max(0f, this.agentTree_[this.agentTree_[node].right_].minX_ - position.x_)) + RVOMath.sqr(Math.Max(0f, position.x_ - this.agentTree_[this.agentTree_[node].right_].maxX_)) + RVOMath.sqr(Math.Max(0f, this.agentTree_[this.agentTree_[node].right_].minY_ - position.y_)) + RVOMath.sqr(Math.Max(0f, position.y_ - this.agentTree_[this.agentTree_[node].right_].maxY_));
			if (num2 < num3)
			{
				if (num2 < rangeSq)
				{
					this.queryAgentTreeRecursive(position, ref rangeSq, ref agentNo, this.agentTree_[node].left_);
					if (num3 < rangeSq)
					{
						this.queryAgentTreeRecursive(position, ref rangeSq, ref agentNo, this.agentTree_[node].right_);
						return;
					}
				}
			}
			else if (num3 < rangeSq)
			{
				this.queryAgentTreeRecursive(position, ref rangeSq, ref agentNo, this.agentTree_[node].right_);
				if (num2 < rangeSq)
				{
					this.queryAgentTreeRecursive(position, ref rangeSq, ref agentNo, this.agentTree_[node].left_);
				}
			}
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x000A0B44 File Offset: 0x0009ED44
		private void queryAgentTreeRecursive(Agent agent, ref float rangeSq, int node)
		{
			if (this.agentTree_[node].end_ - this.agentTree_[node].begin_ <= 10)
			{
				for (int i = this.agentTree_[node].begin_; i < this.agentTree_[node].end_; i++)
				{
					agent.insertAgentNeighbor(this.agents_[i], ref rangeSq);
				}
				return;
			}
			float num = RVOMath.sqr(Math.Max(0f, this.agentTree_[this.agentTree_[node].left_].minX_ - agent.position_.x_)) + RVOMath.sqr(Math.Max(0f, agent.position_.x_ - this.agentTree_[this.agentTree_[node].left_].maxX_)) + RVOMath.sqr(Math.Max(0f, this.agentTree_[this.agentTree_[node].left_].minY_ - agent.position_.y_)) + RVOMath.sqr(Math.Max(0f, agent.position_.y_ - this.agentTree_[this.agentTree_[node].left_].maxY_));
			float num2 = RVOMath.sqr(Math.Max(0f, this.agentTree_[this.agentTree_[node].right_].minX_ - agent.position_.x_)) + RVOMath.sqr(Math.Max(0f, agent.position_.x_ - this.agentTree_[this.agentTree_[node].right_].maxX_)) + RVOMath.sqr(Math.Max(0f, this.agentTree_[this.agentTree_[node].right_].minY_ - agent.position_.y_)) + RVOMath.sqr(Math.Max(0f, agent.position_.y_ - this.agentTree_[this.agentTree_[node].right_].maxY_));
			if (num < num2)
			{
				if (num < rangeSq)
				{
					this.queryAgentTreeRecursive(agent, ref rangeSq, this.agentTree_[node].left_);
					if (num2 < rangeSq)
					{
						this.queryAgentTreeRecursive(agent, ref rangeSq, this.agentTree_[node].right_);
						return;
					}
				}
			}
			else if (num2 < rangeSq)
			{
				this.queryAgentTreeRecursive(agent, ref rangeSq, this.agentTree_[node].right_);
				if (num < rangeSq)
				{
					this.queryAgentTreeRecursive(agent, ref rangeSq, this.agentTree_[node].left_);
				}
			}
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000A0E1C File Offset: 0x0009F01C
		private void queryObstacleTreeRecursive(Agent agent, float rangeSq, KdTree.ObstacleTreeNode node)
		{
			if (node != null)
			{
				Obstacle obstacle_ = node.obstacle_;
				Obstacle next_ = obstacle_.next_;
				float num = RVOMath.leftOf(obstacle_.point_, next_.point_, agent.position_);
				this.queryObstacleTreeRecursive(agent, rangeSq, (num >= 0f) ? node.left_ : node.right_);
				if (RVOMath.sqr(num) / RVOMath.absSq(next_.point_ - obstacle_.point_) < rangeSq)
				{
					if (num < 0f)
					{
						agent.insertObstacleNeighbor(node.obstacle_, rangeSq);
					}
					this.queryObstacleTreeRecursive(agent, rangeSq, (num >= 0f) ? node.right_ : node.left_);
				}
			}
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x000A0EC8 File Offset: 0x0009F0C8
		private bool queryVisibilityRecursive(Vector2 q1, Vector2 q2, float radius, KdTree.ObstacleTreeNode node)
		{
			if (node == null)
			{
				return true;
			}
			Obstacle obstacle_ = node.obstacle_;
			Obstacle next_ = obstacle_.next_;
			float num = RVOMath.leftOf(obstacle_.point_, next_.point_, q1);
			float num2 = RVOMath.leftOf(obstacle_.point_, next_.point_, q2);
			float num3 = 1f / RVOMath.absSq(next_.point_ - obstacle_.point_);
			if (num >= 0f && num2 >= 0f)
			{
				return this.queryVisibilityRecursive(q1, q2, radius, node.left_) && ((RVOMath.sqr(num) * num3 >= RVOMath.sqr(radius) && RVOMath.sqr(num2) * num3 >= RVOMath.sqr(radius)) || this.queryVisibilityRecursive(q1, q2, radius, node.right_));
			}
			if (num <= 0f && num2 <= 0f)
			{
				return this.queryVisibilityRecursive(q1, q2, radius, node.right_) && ((RVOMath.sqr(num) * num3 >= RVOMath.sqr(radius) && RVOMath.sqr(num2) * num3 >= RVOMath.sqr(radius)) || this.queryVisibilityRecursive(q1, q2, radius, node.left_));
			}
			if (num >= 0f && num2 <= 0f)
			{
				return this.queryVisibilityRecursive(q1, q2, radius, node.left_) && this.queryVisibilityRecursive(q1, q2, radius, node.right_);
			}
			float num4 = RVOMath.leftOf(q1, q2, obstacle_.point_);
			float num5 = RVOMath.leftOf(q1, q2, next_.point_);
			float num6 = 1f / RVOMath.absSq(q2 - q1);
			return num4 * num5 >= 0f && RVOMath.sqr(num4) * num6 > RVOMath.sqr(radius) && RVOMath.sqr(num5) * num6 > RVOMath.sqr(radius) && this.queryVisibilityRecursive(q1, q2, radius, node.left_) && this.queryVisibilityRecursive(q1, q2, radius, node.right_);
		}

		// Token: 0x04001940 RID: 6464
		private const int MAX_LEAF_SIZE = 10;

		// Token: 0x04001941 RID: 6465
		private Agent[] agents_;

		// Token: 0x04001942 RID: 6466
		private KdTree.AgentTreeNode[] agentTree_;

		// Token: 0x04001943 RID: 6467
		private KdTree.ObstacleTreeNode obstacleTree_;

		// Token: 0x0200048A RID: 1162
		private struct AgentTreeNode
		{
			// Token: 0x04001944 RID: 6468
			internal int begin_;

			// Token: 0x04001945 RID: 6469
			internal int end_;

			// Token: 0x04001946 RID: 6470
			internal int left_;

			// Token: 0x04001947 RID: 6471
			internal int right_;

			// Token: 0x04001948 RID: 6472
			internal float maxX_;

			// Token: 0x04001949 RID: 6473
			internal float maxY_;

			// Token: 0x0400194A RID: 6474
			internal float minX_;

			// Token: 0x0400194B RID: 6475
			internal float minY_;
		}

		// Token: 0x0200048B RID: 1163
		private struct FloatPair
		{
			// Token: 0x060019E9 RID: 6633 RVA: 0x000A109B File Offset: 0x0009F29B
			internal FloatPair(float a, float b)
			{
				this.a_ = a;
				this.b_ = b;
			}

			// Token: 0x060019EA RID: 6634 RVA: 0x000A10AB File Offset: 0x0009F2AB
			public static bool operator <(KdTree.FloatPair pair1, KdTree.FloatPair pair2)
			{
				return pair1.a_ < pair2.a_ || (pair2.a_ >= pair1.a_ && pair1.b_ < pair2.b_);
			}

			// Token: 0x060019EB RID: 6635 RVA: 0x000A10DB File Offset: 0x0009F2DB
			public static bool operator <=(KdTree.FloatPair pair1, KdTree.FloatPair pair2)
			{
				return (pair1.a_ == pair2.a_ && pair1.b_ == pair2.b_) || pair1 < pair2;
			}

			// Token: 0x060019EC RID: 6636 RVA: 0x000A1102 File Offset: 0x0009F302
			public static bool operator >(KdTree.FloatPair pair1, KdTree.FloatPair pair2)
			{
				return !(pair1 <= pair2);
			}

			// Token: 0x060019ED RID: 6637 RVA: 0x000A110E File Offset: 0x0009F30E
			public static bool operator >=(KdTree.FloatPair pair1, KdTree.FloatPair pair2)
			{
				return !(pair1 < pair2);
			}

			// Token: 0x0400194C RID: 6476
			private float a_;

			// Token: 0x0400194D RID: 6477
			private float b_;
		}

		// Token: 0x0200048C RID: 1164
		private class ObstacleTreeNode
		{
			// Token: 0x0400194E RID: 6478
			internal Obstacle obstacle_;

			// Token: 0x0400194F RID: 6479
			internal KdTree.ObstacleTreeNode left_;

			// Token: 0x04001950 RID: 6480
			internal KdTree.ObstacleTreeNode right_;
		}
	}
}
