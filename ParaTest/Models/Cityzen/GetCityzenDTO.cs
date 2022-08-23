using System.ComponentModel.DataAnnotations;

namespace ParaTest.Models.Cityzen
{
    public class GetCityzenDTO : IValidatableObject
    {
        public string? FullName { get; set; }
        public string? Snils { get; set; }
        public string? Inn { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? IsDead { get; set; }
        public DateTime? DeathDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((IsDead.HasValue && !IsDead.Value || !IsDead.HasValue) && DeathDate != null)
                yield return new ValidationResult("You shouldn't have DeathDate set while having IsDead null or false",
                    new string[] { nameof(DeathDate), nameof(IsDead) });

            int NonNulls = 0;
            List<string> Nulls = new List<string>();
            foreach (var Prop in validationContext.GetType().GetProperties())
            {
                if (Prop.GetValue(validationContext.ObjectInstance) == null)
                {
                    NonNulls++;
                    Nulls.Add(Prop.Name);
                }
            }

            if (NonNulls < 2)
                yield return new ValidationResult("At least two filter properties must have value. Use dedicated single-property 'get' method instead.",
                    Nulls);
        }
    }
}
