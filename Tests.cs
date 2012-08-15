using System;
using System.Diagnostics;
using System.IO;


namespace Com.JohnsonControls {
  /// <summary>
  /// A bunch of quick unit tests
  /// </summary>
  class Tests {
    public void Run() {
      TestHelloWorld();
      TestHelloWorldWithComments();
    }
    void TestHelloWorld() {
      StringWriter sw = new StringWriter();
      var tp = new TorpedoProcessor(Console.In, sw);
      var xpr = "++++++++[>+++++++++<-]>.<+++++[>++++++<-]>-.+++++++..+++.<++++++++[>>++++<<-]>>.<<++++[>------<-]>.<++++[>++++++<-]>.+++.------.--------.>+."; 
      tp.Run(xpr);
      Debug.Assert(sw.ToString() == "Hello World!");
    }
    void TestHelloWorldWithComments() {
      StringWriter sw = new StringWriter();
      var tp = new TorpedoProcessor(Console.In, sw);
      var xpr = "This is a comment++++++++[>and so is this!+++++++++<-]>.<+++++[>++++++<-]>-.+++++++..+++.<++++++++[>>++++<<-]>>.<<++++[>------<-]>.<++++[>++++++<-]>.+++.------.--------.>+.";
      tp.Run(xpr);
      Debug.Assert(sw.ToString() == "Hello World!");
    }
  }
}
