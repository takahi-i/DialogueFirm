using System;
using System.Collections.Generic;
using SimpleBot;

public class SimpleResponder {
    private List<string> responds;
    private Random cRandom;
    private string targetIntent;

    public SimpleResponder(string targetIntent, List<string> responds)
    {
        this.targetIntent = targetIntent;
        this.responds = responds;
        this.cRandom = new System.Random();
    }

    public string Respond(Intent intent) {
        if (this.responds.Count == 0) {
            throw new InvalidOperationException("No responds are deployed in the responder for intent " + targetIntent);
        }
        return this.responds[this.cRandom.Next(this.responds.Count-1)];
    }
}
