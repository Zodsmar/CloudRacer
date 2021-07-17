using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("isDestroyable")]
	public class ES3UserType_Destroyable : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Destroyable() : base(typeof(Destroyable)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Destroyable)obj;
			
			writer.WritePrivateField("isDestroyable", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Destroyable)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "isDestroyable":
					reader.SetPrivateField("isDestroyable", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_DestroyableArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_DestroyableArray() : base(typeof(Destroyable[]), ES3UserType_Destroyable.Instance)
		{
			Instance = this;
		}
	}
}