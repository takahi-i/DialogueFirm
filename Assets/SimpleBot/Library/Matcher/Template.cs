﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using SimpleBot.Matcher;

namespace SimpleBot {
	namespace Matcher {
		public class Template  {
			private List<string> slotNames;
		    private Regex pattern;

			public Template(string patternStr, List<string> slots)  {
				this.pattern = new Regex(patternStr);
				this.slotNames = slots;
			}

			public Result Match(string input) {
				var match = this.pattern.Match(input);
				if (match.Success) {
					return new Result(input, true, new Dictionary<string, string>());
				}
				return new Result(input, false, new Dictionary<string, string>());
			}
		}
	}
}