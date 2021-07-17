using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("countDownTime")]
	public class ES3UserType_LevelManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_LevelManager() : base(typeof(LevelManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (LevelManager)obj;
			
			writer.WriteProperty("countDownTime", instance.countDownTime, ES3Type_int.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (LevelManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "countDownTime":
						instance.countDownTime = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_LevelManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_LevelManagerArray() : base(typeof(LevelManager[]), ES3UserType_LevelManager.Instance)
		{
			Instance = this;
		}
	}
}