using System;
using System.IO;


namespace Com.JohnsonControls {
  /// <summary>
  /// Driver program for the torpedo implementation
  /// </summary>
  class Driver {
    static void Main(string[] args) {

#if DEBUG
      RunTests();
#endif
      string programSrc = null;

      if (args.Length == 0) {//read the program from stdin
        programSrc = Console.In.ReadToEnd();
      } else if (args.Length == 1) {//program arg
        if (args[0] == "--help") {
          Console.WriteLine("Torpedo -- a quick implementaion of Urban Muller's Brain**** language\nusage: torpedo [file]");
        } else {//could be a file, let's try to open it
          try {
            programSrc = File.ReadAllText(args[0]);
          } catch (IOException ex) {
            Console.Error.WriteLine("Error finding/reading the specified file. More info below:\n{0}", ex.Message);
          }
        }
      }

      if (!string.IsNullOrWhiteSpace(programSrc)) Run(programSrc);
    }

    private static void Run(string programSrc) {
      try {
        new TorpedoProcessor().Run(programSrc);
      } catch (InvalidOperationException ex) {//catch unbalanced brackets
        Console.Error.WriteLine("Syntax error in your torpedo source. More info below:\n{0}", ex.Message);
      }
    }

    private static void RunTests() {
      new Tests().Run();
      
    }

    /*a quick and dirty implementation 
    static void Main() {
      var xpr = "++++++++[>+++++++++<-]>.<+++++[>++++++<-]>-.+++++++..+++.<++++++++[>>++++<<-]>>.<<++++[>------<-]>.<++++[>++++++<-]>.+++.------.--------.>+.";
      var cs = xpr.ToCharArray();
      var ds = new char[int.MaxValue];
      var ip = 0;
      var bp = 0;
      var sp = new Stack<int>();
      while (ip < cs.Length) {
        switch (cs[ip]) {
          case '>':
            bp++;
            break;
          case '<':
            bp--;
            break;
          case '+':
            ds[bp]++;
            break;
          case '-':
            ds[bp]--;
            break;
          case '.':
            Console.Write(ds[bp]);
            break;
          case ',':
            ds[bp] = (char)Console.Read();
            break;
          case '[':
            if (ds[bp] == 0) {
              ip = cs.ToString().IndexOf(']', ip);
            } else {
              sp.Push(ip);
            }
            break;
          case ']':
            if (ds[bp] != 0) {
              ip = sp.Peek();
            } else {
              sp.Pop();
            }
            break;
        }
        ip++;
      }
    }
    */
  }
}
