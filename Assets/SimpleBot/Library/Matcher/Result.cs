using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleBot {
    namespace Matcher {
		public class Result {
			public string input;
			public string Input {
				get { return this.input; }
				set { this.input = value; }
			}

			public bool success;
			public bool Success {
				get { return this.success; }
				set { this.success = value; }
			}
			public Dictionary<string, string> features;
			public Dictionary<string, string> Features {
				get { return this.features; }
				set { this.features = value; }
			}

			public Result(string input,  bool success ,Dictionary<string, string> features) {
				this.Input = input;
				this.Success = success;
				this.features = features;
			}
		}
	}
}
