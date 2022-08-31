using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParaTest.Models.Cityzen
{
    [Table("cityzen", Schema = "registry")]
    [Index(nameof(SNILS), IsUnique = true, Name = "cz_snils_uq")]
    [Index(nameof(INN), IsUnique = true, Name = "cz_inn_uq")]

    [Index(nameof(FullName), Name = "cz_full_name")]
    [Index(nameof(FullName), Name = "cz_birth_date")]
    [Index(nameof(FullName), Name = "cz_death_date")]
    public class Cityzen
    {
        public Cityzen(long id, string fullName, string sNILS, string iNN, DateTime birthDate, DateTime? deathDate)
        {
            Id = id;
            FullName = fullName;
            SNILS = sNILS;
            INN = iNN;
            BirthDate = birthDate;
            DeathDate = deathDate;
        }

        [Key]
        public long Id { get; private set; }

        [Required]
        [StringLength(747, MinimumLength = 5)]
        [Column("full_name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14)]
        [RegularExpression(@"\d{3}\-\d{3}\-\d{3} \d{2}")]
        public string SNILS { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 10)]
        [RegularExpression(@"\d{10,12}")]
        public string INN { get; set; }

        [Required]
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("death_date")]
        public DateTime? DeathDate { get; set; }

        public void Update(Cityzen NewCityzen)
        {
            if (NewCityzen != null)
            {
                if (NewCityzen.FullName != FullName)
                    FullName = NewCityzen.FullName;
                if (NewCityzen.SNILS != SNILS)
                    SNILS = NewCityzen.SNILS;
                if (NewCityzen.INN != INN)
                    INN = NewCityzen.INN;
                if (NewCityzen.BirthDate != BirthDate)
                    BirthDate = NewCityzen.BirthDate;
                if (NewCityzen.DeathDate != DeathDate)
                    DeathDate = NewCityzen.DeathDate;
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder SB = new();
            SB.Append($"{Id};");
            SB.Append($"\"{FullName}\";");
            SB.Append($"\"{SNILS}\";");
            SB.Append($"\"{INN}\";");
            SB.Append($"\"{BirthDate.ToString("yyyy-MM-dd HH:mm:ss")}\";");
            SB.Append($"\"{(DeathDate.HasValue ? DeathDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "NULL")}\"");
            return SB.ToString();
        }
    }
}
