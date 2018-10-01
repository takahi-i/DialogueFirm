namespace SimpleBot
{
    public sealed class Pair
    {

        public string First;
        public object Second;


        public Pair()
        {
        }


        public Pair(string x, object y)
        {
            First = x;
            Second = y;
        }
    }
}