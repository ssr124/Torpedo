using System;
using System.Collections.Generic;
using System.IO;


namespace Com.JohnsonControls {
  /// <summary>
  /// A quick implementation of Urban Muller's Brain**** language. 
  /// </summary>
  class TorpedoProcessor {
    const int DATASEGSIZE = short.MaxValue;

    String programSrc;
    Dictionary<char, Action> instructionSet;
    char[] dataSegment, codeSegment;
    int instructionPointer = 0, basePointer = 0;
    Stack<int> stack = new Stack<int>();
    TextReader _in;
    TextWriter _out;

    public TorpedoProcessor() {
      _in = Console.In;
      _out = Console.Out;
      LoadInstructionSet();
    }

#if DEBUG
    public TorpedoProcessor(TextReader _in, TextWriter _out) {
      this._in = _in;
      this._out = _out;
      LoadInstructionSet();
    }
#endif

    /// <summary>
    /// Load a known set of instructions
    /// </summary>
    void LoadInstructionSet() {
      instructionSet = new Dictionary<char, Action> { 
        {'>', () => basePointer++ },
        {'<', () => basePointer-- },
        {'+', () => dataSegment[basePointer]++ },
        {'-', () => dataSegment[basePointer]-- },
        {'.', () => _out.Write(dataSegment[basePointer]) },
        {',', () => dataSegment[basePointer] = (char) _in.Read() },
        {'[', () => {
          if (dataSegment[basePointer] == 0) {
            instructionPointer = programSrc.IndexOf(']', instructionPointer);
          } else {
            stack.Push(instructionPointer);
          }
        }},
        {']', () => {
          if (dataSegment[basePointer] != 0) {
            instructionPointer = stack.Peek();
          } else {
            stack.Pop();
          }
        }},
      };
    }

    /// <summary>
    /// Run the given program code
    /// </summary>
    /// <param name="expr"></param>
    public void Run(string expr) {
      programSrc = expr;
      dataSegment = new char[DATASEGSIZE];
      codeSegment = expr.ToCharArray();
      while (instructionPointer < codeSegment.Length) {
        if (instructionSet.ContainsKey(codeSegment[instructionPointer]))
          instructionSet[codeSegment[instructionPointer]]();
        instructionPointer++;
      }
      stack.Clear();
    }
  }
}

