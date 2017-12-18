using UnityEngine;

namespace Statistics
{
	[System.Serializable]
	public class Statistic
	{
		[SerializeField]
		private int _baseValue;

		public int GetValue()
		{
			return _baseValue;
		}
	}
}
