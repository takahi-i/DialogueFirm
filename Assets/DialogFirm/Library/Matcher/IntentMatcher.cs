namespace DialogFirm
{
  namespace Matcher {
    public abstract class IntentMatcher {
      public abstract string Name();
      public abstract Intent Match(string input);
    }
  }
}
