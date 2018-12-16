using System;
using DomainModeling.Core.Utilities.Interfaces;

namespace DomainModeling.Core.Utilities
{
  public class DateProvider : IDate {
    public DateTime CurrentDateUtc() {
      return DateTime.UtcNow;
    }
  }
}
