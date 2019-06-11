using System;
using System.Configuration;

namespace DotnetConsole
{
  public class UsingRegexStringValidator
  {
    /// Summary:
    ///     Determines whether the value of an object is valid.
    ///
    /// Parameters:
    ///   value:
    ///     The value of an object.
    ///
    /// Exceptions:
    ///   T:System.ArgumentException:
    ///     value does not conform to the parameters of the System.Text.RegularExpressions.Regex
    ///     class.
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
