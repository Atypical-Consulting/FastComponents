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

        ValidateUsername(errors);
        ValidateEmail(errors);
        ValidatePassword(errors);
        ValidatePasswordConfirmation(errors);

        return errors;
    }

    private void ValidateUsername(Dictionary<string, string> errors)
    {
        if (string.IsNullOrEmpty(Username))
        {
            return;
        }

        if (Username.Length < 3)
        {
            errors["username"] = "Username must be at least 3 characters";
        }
        else if (!Regex.IsMatch(Username, @"^[a-zA-Z0-9]+$"))
        {
            errors["username"] = "Username can only contain letters and numbers";
        }
    }

    private void ValidateEmail(Dictionary<string, string> errors)
    {
        if (string.IsNullOrEmpty(Email))
        {
            return;
        }

        if (Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            return;
        }

        errors["email"] = "Please enter a valid email address";
    }

    private void ValidatePassword(Dictionary<string, string> errors)
    {
        if (string.IsNullOrEmpty(Password))
        {
            return;
        }

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

    private void ValidatePasswordConfirmation(Dictionary<string, string> errors)
    {
        if (string.IsNullOrEmpty(ConfirmPassword))
        {
            return;
        }

        if (ConfirmPassword == Password)
        {
            return;
        }

        errors["confirmPassword"] = "Passwords do not match";
    }
}
