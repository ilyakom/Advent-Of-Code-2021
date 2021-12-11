using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_4
{
	public class Board
	{
		private int[,] _nums;
		private bool[,] _marked;
		private Dictionary<int, (int, int)> _dic;

		private int curRow = 0;
		private int curCol = 0;

		public Board()
		{
			_nums = new int[5, 5];
			_marked = new bool[5, 5];
			_dic = new Dictionary<int, (int, int)>();
		}

		public int GetUnmarkedSum()
		{
			var sum = 0;

			for(int i = 0; i < 5; i++)
			{
				for(int j = 0; j < 5; j++)
				{
					if (!_marked[i,j])
					{
						sum += _nums[i, j];
					}
				}
			}

			return sum;
		}

		public void Add(int num)
		{
			_nums[curRow, curCol] = num;
			_dic[num] = (curRow, curCol);

			curCol++;

			if (curCol == 5)
			{
				curCol = 0;
				curRow++;
			}
		}

		public int? WhenWins(IList<int> nums)
		{
			for(int i = 0; i < nums.Count; i++)
			{
				var el = nums[i];

				if (_dic.TryGetValue(el, out var pair))
				{
					_marked[pair.Item1, pair.Item2] = true;

					if (CheckIsSolved())
					{
						return i;
					}
				}
			}

			return null;
		}

		private bool CheckIsSolved()
		{
			for (int i = 0; i < 5; i++)
			{
				bool rm = true;
				bool cm = true;

				for (int j = 0; j < 5; j++)
				{
					if (!_marked[i, j])
					{
						rm = false;
					}

					if (!_marked[j, i])
					{
						cm = false;
					}
				}

				if (rm || cm)
				{
					return true;
				}
			}

			return false;
		}
	}
}
