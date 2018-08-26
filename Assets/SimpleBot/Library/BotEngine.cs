﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleBot;

namespace SimpleBot {
	public class BotEngine {
		private IntentIdentifier identifier;

		public BotEngine(Configuration config) {
			this.identifier = new IntentIdentifier(config);
		}

		public string IdenfityIntent(string input) {
			return this.identifier.Identify(input);
		}

		//TODO
		// - Reply sentence
		// - Return properties from Identifier
	}
}