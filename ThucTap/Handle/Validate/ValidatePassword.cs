namespace ThucTap.Handle.Validate
{
    public class ValidatePassword
    {
        public static bool isValidPassword(string password)
        {
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = IsSpecialCharacter(password);
            return hasDigit && hasSpecialChar;
        }
        private static bool IsSpecialCharacter(string password)
        {
            char[] arr = password.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (!char.IsLetterOrDigit(arr[i]))
                    return true;
            }
            return false;
        }
    }
}
