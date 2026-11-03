namespace TestMVCSharpC.Exceptions;

using System;

public class InternalServerErrorException : Exception 
{
  public InternalServerErrorException(string message) : base(message) {}
  public InternalServerErrorException(string message, Exception cause) : base(message, cause) {}
}
