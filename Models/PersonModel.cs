using System.Text.RegularExpressions;

namespace GrpcPersonService.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string BirthDate { get; set; }

        public static bool IsValidNationalCode(string nationalCode)
        {
            if (!string.IsNullOrWhiteSpace(nationalCode))
            {
                if (!Regex.IsMatch(nationalCode, @"^\d{10}$"))
                    return false;
            }
            

            var check = int.Parse(nationalCode[9].ToString());
            var sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(nationalCode[i].ToString()) * (10 - i);

            var remainder = sum % 11;
            return remainder < 2 ? check == remainder : check == (11 - remainder);
        }

        public static bool IsValidBirthDate(string birthDate)
        {
            return DateTime.TryParseExact(birthDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
