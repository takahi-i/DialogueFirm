namespace SimpleBot {
  namespace Matcher {
    public abstract class IntentMatcher {
      public abstract string Name();
      public abstract Result Match(string input);
    }
  }
}
