using System.Text.RegularExpressions;

namespace HtmxAppServer.Components.Blocks;

[GenerateParameterMethods]
public partial record FormValidationParameters : HtmxComponentParameters
{
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string ConfirmPassword { get; init; } = string.Empty;

    public Dictionary<string, string> ValidationErrors => ValidateFields();

    private Dictionary<string, string> ValidateFields()
    {
        Dictionary<string, string> errors = [];

        // Username validation
        if (!string.IsNullOrEmpty(Username))
        {
            if (Username.Length < 3)
            {
                errors["username"] = "Username must be at least 3 characters";
            }
            else if (!Regex.IsMatch(Username, @"^[a-zA-Z0-9]+$"))
            {
                errors["username"] = "Username can only contain letters and numbers";
            }
        }

        // Email validation
        if (!string.IsNullOrEmpty(Email))
        {
            if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errors["email"] = "Please enter a valid email address";
            }
        }

        // Password validation
        if (!string.IsNullOrEmpty(Password))
        {
            if (Password.Length < 8)
            {
                errors["password"] = "Password must be at least 8 characters";
            }
            else if (!Regex.IsMatch(Password, @"[A-Z]"))
            {
                errors["password"] = "Password must contain at least one uppercase letter";
            }
            else if (!Regex.IsMatch(Password, @"\d"))
            {
                errors["password"] = "Password must contain at least one number";
            }
        }

        // Confirm password validation
        if (!string.IsNullOrEmpty(ConfirmPassword))
        {
            if (ConfirmPassword != Password)
            {
                errors["confirmPassword"] = "Passwords do not match";
            }
        }

        return errors;
    }
}
