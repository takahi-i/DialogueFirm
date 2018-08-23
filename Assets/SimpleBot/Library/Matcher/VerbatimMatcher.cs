namespace SimpleBot {
  namespace Matcher {
    abstract class VerbatimMatcher : IntentMatcher {
	override public bool Match(string input) {
		  return true;
	  }
    }
  }
}