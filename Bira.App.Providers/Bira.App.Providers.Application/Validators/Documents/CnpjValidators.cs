namespace Bira.App.Providers.Application.Validators.Documents
{
    public class CnpjValidators
    {
        public const int SizeCnpj = 14;

        public static bool Validate(string cpnj)
        {
            var cnpjNumbers = Utils.OnlyNumbers(cpnj);

            if (!SizeValid(cnpjNumbers)) return false;
            return !HasRepeatedDigits(cnpjNumbers) && HaveValidDigits(cnpjNumbers);
        }

        private static bool SizeValid(string value)
        {
            return value.Length == SizeCnpj;
        }

        private static bool HasRepeatedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HaveValidDigits(string value)
        {
            var number = value.Substring(0, SizeCnpj - 2);

            var verifyingDigit = new VerifyingDigit(number)
                .WithAteMultipliers(2, 9)
                .Replacing("0", 10, 11);
            var firstDigit = verifyingDigit.CalculateDigit();
            verifyingDigit.AddDigit(firstDigit);
            var secondDigit = verifyingDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(SizeCnpj - 2, 2);
        }
    }
}
