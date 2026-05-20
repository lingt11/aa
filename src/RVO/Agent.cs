using System;
using System.Collections.Generic;

namespace RVO
{
	// Token: 0x02000488 RID: 1160
	internal class Agent
	{
		// Token: 0x060019D3 RID: 6611 RVA: 0x0009EC2C File Offset: 0x0009CE2C
		internal void computeNeighbors()
		{
			this.obstacleNeighbors_.Clear();
			float rangeSq = RVOMath.sqr(this.timeHorizonObst_ * this.maxSpeed_ + this.radius_);
			Simulator.Instance.kdTree_.computeObstacleNeighbors(this, rangeSq);
			this.agentNeighbors_.Clear();
			if (this.maxNeighbors_ > 0)
			{
				rangeSq = RVOMath.sqr(this.neighborDist_);
				Simulator.Instance.kdTree_.computeAgentNeighbors(this, ref rangeSq);
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0009ECA4 File Offset: 0x0009CEA4
		internal void computeNewVelocity()
		{
			this.orcaLines_.Clear();
			float num = 1f / this.timeHorizonObst_;
			for (int i = 0; i < this.obstacleNeighbors_.Count; i++)
			{
				Obstacle obstacle = this.obstacleNeighbors_[i].Value;
				Obstacle obstacle2 = obstacle.next_;
				Vector2 vector = obstacle.point_ - this.position_;
				Vector2 vector2 = obstacle2.point_ - this.position_;
				bool flag = false;
				for (int j = 0; j < this.orcaLines_.Count; j++)
				{
					if (RVOMath.det(num * vector - this.orcaLines_[j].point, this.orcaLines_[j].direction) - num * this.radius_ >= -1E-05f && RVOMath.det(num * vector2 - this.orcaLines_[j].point, this.orcaLines_[j].direction) - num * this.radius_ >= -1E-05f)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					float num2 = RVOMath.absSq(vector);
					float num3 = RVOMath.absSq(vector2);
					float num4 = RVOMath.sqr(this.radius_);
					Vector2 vector3 = obstacle2.point_ - obstacle.point_;
					float num5 = -vector * vector3 / RVOMath.absSq(vector3);
					float num6 = RVOMath.absSq(-vector - num5 * vector3);
					if (num5 < 0f && num2 <= num4)
					{
						if (obstacle.convex_)
						{
							Line item;
							item.point = new Vector2(0f, 0f);
							item.direction = RVOMath.normalize(new Vector2(-vector.y(), vector.x()));
							this.orcaLines_.Add(item);
						}
					}
					else if (num5 > 1f && num3 <= num4)
					{
						if (obstacle2.convex_ && RVOMath.det(vector2, obstacle2.direction_) >= 0f)
						{
							Line item;
							item.point = new Vector2(0f, 0f);
							item.direction = RVOMath.normalize(new Vector2(-vector2.y(), vector2.x()));
							this.orcaLines_.Add(item);
						}
					}
					else if (num5 >= 0f && num5 < 1f && num6 <= num4)
					{
						Line item;
						item.point = new Vector2(0f, 0f);
						item.direction = -obstacle.direction_;
						this.orcaLines_.Add(item);
					}
					else
					{
						Vector2 vector4;
						Vector2 vector5;
						if (num5 < 0f && num6 <= num4)
						{
							if (!obstacle.convex_)
							{
								goto IL_8B1;
							}
							obstacle2 = obstacle;
							float num7 = RVOMath.sqrt(num2 - num4);
							vector4 = new Vector2(vector.x() * num7 - vector.y() * this.radius_, vector.x() * this.radius_ + vector.y() * num7) / num2;
							vector5 = new Vector2(vector.x() * num7 + vector.y() * this.radius_, -vector.x() * this.radius_ + vector.y() * num7) / num2;
						}
						else if (num5 > 1f && num6 <= num4)
						{
							if (!obstacle2.convex_)
							{
								goto IL_8B1;
							}
							obstacle = obstacle2;
							float num8 = RVOMath.sqrt(num3 - num4);
							vector4 = new Vector2(vector2.x() * num8 - vector2.y() * this.radius_, vector2.x() * this.radius_ + vector2.y() * num8) / num3;
							vector5 = new Vector2(vector2.x() * num8 + vector2.y() * this.radius_, -vector2.x() * this.radius_ + vector2.y() * num8) / num3;
						}
						else
						{
							if (obstacle.convex_)
							{
								float num9 = RVOMath.sqrt(num2 - num4);
								vector4 = new Vector2(vector.x() * num9 - vector.y() * this.radius_, vector.x() * this.radius_ + vector.y() * num9) / num2;
							}
							else
							{
								vector4 = -obstacle.direction_;
							}
							if (obstacle2.convex_)
							{
								float num10 = RVOMath.sqrt(num3 - num4);
								vector5 = new Vector2(vector2.x() * num10 + vector2.y() * this.radius_, -vector2.x() * this.radius_ + vector2.y() * num10) / num3;
							}
							else
							{
								vector5 = obstacle.direction_;
							}
						}
						Obstacle previous_ = obstacle.previous_;
						bool flag2 = false;
						bool flag3 = false;
						if (obstacle.convex_ && RVOMath.det(vector4, -previous_.direction_) >= 0f)
						{
							vector4 = -previous_.direction_;
							flag2 = true;
						}
						if (obstacle2.convex_ && RVOMath.det(vector5, obstacle2.direction_) <= 0f)
						{
							vector5 = obstacle2.direction_;
							flag3 = true;
						}
						Vector2 vector6 = num * (obstacle.point_ - this.position_);
						Vector2 vector7 = num * (obstacle2.point_ - this.position_);
						Vector2 vector8 = vector7 - vector6;
						float num11 = (obstacle == obstacle2) ? 0.5f : ((this.velocity_ - vector6) * vector8 / RVOMath.absSq(vector8));
						float num12 = (this.velocity_ - vector6) * vector4;
						float num13 = (this.velocity_ - vector7) * vector5;
						if ((num11 < 0f && num12 < 0f) || (obstacle == obstacle2 && num12 < 0f && num13 < 0f))
						{
							Vector2 vector9 = RVOMath.normalize(this.velocity_ - vector6);
							Line item;
							item.direction = new Vector2(vector9.y(), -vector9.x());
							item.point = vector6 + this.radius_ * num * vector9;
							this.orcaLines_.Add(item);
						}
						else if (num11 > 1f && num13 < 0f)
						{
							Vector2 vector10 = RVOMath.normalize(this.velocity_ - vector7);
							Line item;
							item.direction = new Vector2(vector10.y(), -vector10.x());
							item.point = vector7 + this.radius_ * num * vector10;
							this.orcaLines_.Add(item);
						}
						else
						{
							float num14 = (num11 < 0f || num11 > 1f || obstacle == obstacle2) ? float.PositiveInfinity : RVOMath.absSq(this.velocity_ - (vector6 + num11 * vector8));
							float num15 = (num12 < 0f) ? float.PositiveInfinity : RVOMath.absSq(this.velocity_ - (vector6 + num12 * vector4));
							float num16 = (num13 < 0f) ? float.PositiveInfinity : RVOMath.absSq(this.velocity_ - (vector7 + num13 * vector5));
							if (num14 <= num15 && num14 <= num16)
							{
								Line item;
								item.direction = -obstacle.direction_;
								item.point = vector6 + this.radius_ * num * new Vector2(-item.direction.y(), item.direction.x());
								this.orcaLines_.Add(item);
							}
							else if (num15 <= num16)
							{
								if (!flag2)
								{
									Line item;
									item.direction = vector4;
									item.point = vector6 + this.radius_ * num * new Vector2(-item.direction.y(), item.direction.x());
									this.orcaLines_.Add(item);
								}
							}
							else if (!flag3)
							{
								Line item;
								item.direction = -vector5;
								item.point = vector7 + this.radius_ * num * new Vector2(-item.direction.y(), item.direction.x());
								this.orcaLines_.Add(item);
							}
						}
					}
				}
				IL_8B1:;
			}
			int count = this.orcaLines_.Count;
			float num17 = 1f / this.timeHorizon_;
			for (int k = 0; k < this.agentNeighbors_.Count; k++)
			{
				Agent value = this.agentNeighbors_[k].Value;
				Vector2 vector11 = value.position_ - this.position_;
				Vector2 vector12 = this.velocity_ - value.velocity_;
				float num18 = RVOMath.absSq(vector11);
				float num19 = this.radius_ + value.radius_;
				float num20 = RVOMath.sqr(num19);
				Line line;
				Vector2 vector15;
				if (num18 > num20)
				{
					Vector2 vector13 = vector12 - num17 * vector11;
					float num21 = RVOMath.absSq(vector13);
					float num22 = vector13 * vector11;
					if (num22 < 0f && RVOMath.sqr(num22) > num20 * num21)
					{
						float num23 = RVOMath.sqrt(num21);
						Vector2 vector14 = vector13 / num23;
						line.direction = new Vector2(vector14.y(), -vector14.x());
						vector15 = (num19 * num17 - num23) * vector14;
					}
					else
					{
						float num24 = RVOMath.sqrt(num18 - num20);
						if (RVOMath.det(vector11, vector13) > 0f)
						{
							line.direction = new Vector2(vector11.x() * num24 - vector11.y() * num19, vector11.x() * num19 + vector11.y() * num24) / num18;
						}
						else
						{
							line.direction = -new Vector2(vector11.x() * num24 + vector11.y() * num19, -vector11.x() * num19 + vector11.y() * num24) / num18;
						}
						vector15 = vector12 * line.direction * line.direction - vector12;
					}
				}
				else
				{
					float num25 = 1f / Simulator.Instance.timeStep_;
					Vector2 vector16 = vector12 - num25 * vector11;
					float num26 = RVOMath.abs(vector16);
					Vector2 vector17 = vector16 / num26;
					line.direction = new Vector2(vector17.y(), -vector17.x());
					vector15 = (num19 * num25 - num26) * vector17;
				}
				line.point = this.velocity_ + 0.5f * vector15;
				this.orcaLines_.Add(line);
			}
			int num27 = this.linearProgram2(this.orcaLines_, this.maxSpeed_, this.prefVelocity_, false, ref this.newVelocity_);
			if (num27 < this.orcaLines_.Count)
			{
				this.linearProgram3(this.orcaLines_, count, num27, this.maxSpeed_, ref this.newVelocity_);
			}
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0009F834 File Offset: 0x0009DA34
		internal void insertAgentNeighbor(Agent agent, ref float rangeSq)
		{
			if (this != agent)
			{
				float num = RVOMath.absSq(this.position_ - agent.position_);
				if (num < rangeSq)
				{
					if (this.agentNeighbors_.Count < this.maxNeighbors_)
					{
						this.agentNeighbors_.Add(new KeyValuePair<float, Agent>(num, agent));
					}
					int num2 = this.agentNeighbors_.Count - 1;
					while (num2 != 0 && num < this.agentNeighbors_[num2 - 1].Key)
					{
						this.agentNeighbors_[num2] = this.agentNeighbors_[num2 - 1];
						num2--;
					}
					this.agentNeighbors_[num2] = new KeyValuePair<float, Agent>(num, agent);
					if (this.agentNeighbors_.Count == this.maxNeighbors_)
					{
						rangeSq = this.agentNeighbors_[this.agentNeighbors_.Count - 1].Key;
					}
				}
			}
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0009F920 File Offset: 0x0009DB20
		internal void insertObstacleNeighbor(Obstacle obstacle, float rangeSq)
		{
			Obstacle next_ = obstacle.next_;
			float num = RVOMath.distSqPointLineSegment(obstacle.point_, next_.point_, this.position_);
			if (num < rangeSq)
			{
				this.obstacleNeighbors_.Add(new KeyValuePair<float, Obstacle>(num, obstacle));
				int num2 = this.obstacleNeighbors_.Count - 1;
				while (num2 != 0 && num < this.obstacleNeighbors_[num2 - 1].Key)
				{
					this.obstacleNeighbors_[num2] = this.obstacleNeighbors_[num2 - 1];
					num2--;
				}
				this.obstacleNeighbors_[num2] = new KeyValuePair<float, Obstacle>(num, obstacle);
			}
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0009F9BF File Offset: 0x0009DBBF
		internal void update()
		{
			this.velocity_ = this.newVelocity_;
			this.position_ += this.velocity_ * Simulator.Instance.timeStep_;
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0009F9F4 File Offset: 0x0009DBF4
		private bool linearProgram1(IList<Line> lines, int lineNo, float radius, Vector2 optVelocity, bool directionOpt, ref Vector2 result)
		{
			float num = lines[lineNo].point * lines[lineNo].direction;
			float num2 = RVOMath.sqr(num) + RVOMath.sqr(radius) - RVOMath.absSq(lines[lineNo].point);
			if (num2 < 0f)
			{
				return false;
			}
			float num3 = RVOMath.sqrt(num2);
			float num4 = -num - num3;
			float num5 = -num + num3;
			for (int i = 0; i < lineNo; i++)
			{
				float num6 = RVOMath.det(lines[lineNo].direction, lines[i].direction);
				float num7 = RVOMath.det(lines[i].direction, lines[lineNo].point - lines[i].point);
				if (RVOMath.fabs(num6) <= 1E-05f)
				{
					if (num7 < 0f)
					{
						return false;
					}
				}
				else
				{
					float val = num7 / num6;
					if (num6 >= 0f)
					{
						num5 = Math.Min(num5, val);
					}
					else
					{
						num4 = Math.Max(num4, val);
					}
					if (num4 > num5)
					{
						return false;
					}
				}
			}
			if (directionOpt)
			{
				if (optVelocity * lines[lineNo].direction > 0f)
				{
					result = lines[lineNo].point + num5 * lines[lineNo].direction;
				}
				else
				{
					result = lines[lineNo].point + num4 * lines[lineNo].direction;
				}
			}
			else
			{
				float num8 = lines[lineNo].direction * (optVelocity - lines[lineNo].point);
				if (num8 < num4)
				{
					result = lines[lineNo].point + num4 * lines[lineNo].direction;
				}
				else if (num8 > num5)
				{
					result = lines[lineNo].point + num5 * lines[lineNo].direction;
				}
				else
				{
					result = lines[lineNo].point + num8 * lines[lineNo].direction;
				}
			}
			return true;
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0009FC3C File Offset: 0x0009DE3C
		private int linearProgram2(IList<Line> lines, float radius, Vector2 optVelocity, bool directionOpt, ref Vector2 result)
		{
			if (directionOpt)
			{
				result = optVelocity * radius;
			}
			else if (RVOMath.absSq(optVelocity) > RVOMath.sqr(radius))
			{
				result = RVOMath.normalize(optVelocity) * radius;
			}
			else
			{
				result = optVelocity;
			}
			for (int i = 0; i < lines.Count; i++)
			{
				if (RVOMath.det(lines[i].direction, lines[i].point - result) > 0f)
				{
					Vector2 vector = result;
					if (!this.linearProgram1(lines, i, radius, optVelocity, directionOpt, ref result))
					{
						result = vector;
						return i;
					}
				}
			}
			return lines.Count;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0009FCF4 File Offset: 0x0009DEF4
		private void linearProgram3(IList<Line> lines, int numObstLines, int beginLine, float radius, ref Vector2 result)
		{
			float num = 0f;
			for (int i = beginLine; i < lines.Count; i++)
			{
				if (RVOMath.det(lines[i].direction, lines[i].point - result) > num)
				{
					IList<Line> list = new List<Line>();
					for (int j = 0; j < numObstLines; j++)
					{
						list.Add(lines[j]);
					}
					int k = numObstLines;
					while (k < i)
					{
						float num2 = RVOMath.det(lines[i].direction, lines[k].direction);
						Line item;
						if (RVOMath.fabs(num2) > 1E-05f)
						{
							item.point = lines[i].point + RVOMath.det(lines[k].direction, lines[i].point - lines[k].point) / num2 * lines[i].direction;
							goto IL_14B;
						}
						if (lines[i].direction * lines[k].direction <= 0f)
						{
							item.point = 0.5f * (lines[i].point + lines[k].point);
							goto IL_14B;
						}
						IL_17D:
						k++;
						continue;
						IL_14B:
						item.direction = RVOMath.normalize(lines[k].direction - lines[i].direction);
						list.Add(item);
						goto IL_17D;
					}
					Vector2 vector = result;
					IList<Line> lines2 = list;
					Line line = lines[i];
					float x = -line.direction.y();
					line = lines[i];
					if (this.linearProgram2(lines2, radius, new Vector2(x, line.direction.x()), true, ref result) < list.Count)
					{
						result = vector;
					}
					num = RVOMath.det(lines[i].direction, lines[i].point - result);
				}
			}
		}

		// Token: 0x04001931 RID: 6449
		internal IList<KeyValuePair<float, Agent>> agentNeighbors_ = new List<KeyValuePair<float, Agent>>();

		// Token: 0x04001932 RID: 6450
		internal IList<KeyValuePair<float, Obstacle>> obstacleNeighbors_ = new List<KeyValuePair<float, Obstacle>>();

		// Token: 0x04001933 RID: 6451
		internal IList<Line> orcaLines_ = new List<Line>();

		// Token: 0x04001934 RID: 6452
		internal Vector2 position_;

		// Token: 0x04001935 RID: 6453
		internal Vector2 prefVelocity_;

		// Token: 0x04001936 RID: 6454
		internal Vector2 velocity_;

		// Token: 0x04001937 RID: 6455
		internal int id_;

		// Token: 0x04001938 RID: 6456
		internal int maxNeighbors_;

		// Token: 0x04001939 RID: 6457
		internal float maxSpeed_;

		// Token: 0x0400193A RID: 6458
		internal float neighborDist_;

		// Token: 0x0400193B RID: 6459
		internal float radius_;

		// Token: 0x0400193C RID: 6460
		internal float timeHorizon_;

		// Token: 0x0400193D RID: 6461
		internal float timeHorizonObst_;

		// Token: 0x0400193E RID: 6462
		internal bool needDelete_;

		// Token: 0x0400193F RID: 6463
		private Vector2 newVelocity_;
	}
}
