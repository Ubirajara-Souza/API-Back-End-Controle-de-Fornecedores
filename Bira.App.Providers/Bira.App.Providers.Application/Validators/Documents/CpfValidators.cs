namespace Bira.App.Providers.Application.Validators.Documents
{
    public class CpfValidators
    {
        public const int SizeCpf = 11;

        public static bool Validate(string cpf)
        {
            var cpfNumbers = Utils.OnlyNumbers(cpf);

            if (!SizeValid(cpfNumbers)) return false;
            return !HasRepeatedDigits(cpfNumbers) && HaveValidDigits(cpfNumbers);
        }

        private static bool SizeValid(string value)
        {
            return value.Length == SizeCpf;
        }

        private static bool HasRepeatedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HaveValidDigits(string value)
        {
            var number = value.Substring(0, SizeCpf - 2);
            var verifyingDigit = new VerifyingDigit(number)
                .WithAteMultipliers(2, 11)
                .Replacing("0", 10, 11);
            var firstDigit = verifyingDigit.CalculateDigit();
            verifyingDigit.AddDigit(firstDigit);
            var secondDigit = verifyingDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(SizeCpf - 2, 2);
        }
    }
}
