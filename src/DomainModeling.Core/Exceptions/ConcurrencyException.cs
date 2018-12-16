using System;

namespace DomainModeling.Core.Exceptions {
  public class ConcurrencyException : Exception {
    public ConcurrencyException(string message) :base(message) { }
  }
}
