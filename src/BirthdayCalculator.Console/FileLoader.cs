# nullable disable
using BirthdayCalculator.ViewModels;
using System.Text.Json;

namespace BirthdayCalculator.Console
{
    public static class PeopleFileLoader
    {

        public static BirthdayRequest Load(StreamReader reader)
        {           
            var people = new List<PersonDTO>();
          
            string json = reader.ReadToEnd();
            JsonElement root = JsonSerializer.Deserialize<JsonElement>(json);

            foreach (JsonElement element in root.EnumerateArray())
            {
                var lastName = string.Empty;
                var firstName = string.Empty;
                var birthdateText = string.Empty;

                if (element.ValueKind == JsonValueKind.Array)
                {
                    lastName = element[0].GetString();
                    firstName = element[1].GetString();
                    birthdateText = element[2].GetString();
                }
                else if (element.ValueKind == JsonValueKind.Object)
                {
                    firstName = element.GetProperty("firstName").GetString();
                    lastName = element.GetProperty("lastName").GetString();
                    birthdateText = element.GetProperty("birthDate").GetString();
                }
                else
                {
                    throw new FileLoadException("Format of the content of the file is incorrect, please check file");
                }

                var isValidBirthdate = DateTime.TryParse(birthdateText, out DateTime birthDate);

                people.Add(new PersonDTO { FirstName = firstName, LastName = lastName, BirthDate = isValidBirthdate ? birthDate : null });
            }

            return new BirthdayRequest { People = people };
        }
    }
}
