using System.Collections.Generic;

namespace DependencyInjection.Services
{
	/*
	* This class stores multiple lists of strings. Each list of strings can be looked up by a string key.
	*/
	public class MemoryDatabase
    {
		private Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();

		public int Size { get { return data.Count; } }

		public void AddString(string key, string newData)
		{
			if (this.data.TryGetValue(key, out List<string> keyData))
			{
				keyData.Add(newData);
				return;
			}
			keyData = new List<string>() { newData };
			this.data.Add(key, keyData);
		}

		public IEnumerable<string> GetData(string key)
		{
			if (this.data.TryGetValue(key, out List<string> keyData))
			{
				return keyData;
			}
			return new List<string>();
		}

		public void DeleteAll()
		{
			this.data.Clear();
		}
	}
}
