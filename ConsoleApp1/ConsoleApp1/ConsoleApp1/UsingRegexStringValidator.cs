using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
  public class UsingRegexStringValidator
  {
    public static void RegexStringValidatorMethod()
    {
      Console.WriteLine("ASP.NET Validators");
      Console.WriteLine();

      // Create RegexString and Validator.
      string testString = "someone@example.com";
      string regexString =
       @"^[a-zA-Z\.\-_]+@([a-zA-Z\.\-_]+\.)+[a-zA-Z]{2,4}$";
      RegexStringValidator myRegexValidator =
       new RegexStringValidator(regexString);

      // Determine if the object to validate can be validated.
      Console.WriteLine("CanValidate: {0}",
          myRegexValidator.CanValidate(testString.GetType()));

      try
      {
        // Attempt validation.
        myRegexValidator.Validate(testString);
        Console.WriteLine("Validated.");
      }
      catch (ArgumentException e)
      {
        // Validation failed.
        Console.WriteLine("Error: {0}", e.Message.ToString());
      }

      // Display and wait
      Console.ReadLine();
    }
  }
}
