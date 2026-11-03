namespace TestMVCSharpC.Exceptions;

using System;

public class ResourceNotFoundException : Exception 
{
  public ResourceNotFoundException(string message) : base(message) {}
  public ResourceNotFoundException(string message, Exception cause) : base(message, cause) {}
}
