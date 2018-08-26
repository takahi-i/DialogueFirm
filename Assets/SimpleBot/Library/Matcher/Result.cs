using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleBot {
    namespace Matcher {
		public class Result {
			public string input;
			public bool success;
			public Dictionary<string, string> features;

			public Result(string input,  bool success ,Dictionary<string, string> features) {
				this.success = success;
				this.input = input;
				this.features = features;
			}
		}	
	}
}
