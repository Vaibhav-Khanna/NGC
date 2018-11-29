﻿using System;
using Newtonsoft.Json;

namespace NGC.DataStore.Abstraction
{
	public class BaseDataObject : IBaseDataObject
    {
		[JsonProperty("_id")]
		public string Id { get; set; }
    }

	public interface IBaseDataObject
	{
		string Id { get; set; }
	}
}
