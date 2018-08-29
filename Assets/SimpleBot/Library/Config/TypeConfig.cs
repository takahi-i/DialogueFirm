using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeConfig {
	private Dictionary<string, List<string>> types;

	public TypeConfig() {
		types = new Dictionary<string,List<string>>();
	} 

	public void Add(string typeName, List<string> typeList)
	{
		this.types.Add(typeName, typeList);
	}

	public List<string> Get(string typeName, List<string> typeList)
	{
		if (this.types.ContainsKey(typeName)) {
	    	return this.types.Get(typeName);
		}
		return new List<string>();
	}

	public bool HasKey(string typeName) {
		if (this.types.ContainsKey(typeName)) {
			return true;
		}
		return false;
	}
}
