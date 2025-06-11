using NailSalon.BL.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class ZodiacService : IZodiacService
    {
        public string Calculate(DateTime birthDate)
        {
            int day = birthDate.Day;
            int month = birthDate.Month;

            return (month, day) switch
            {
                (3, >= 21) or (4, <= 19) => "Qoç",
                (4, >= 20) or (5, <= 20) => "Buğa",
                (5, >= 21) or (6, <= 20) => "Əkizlər",
                (6, >= 21) or (7, <= 22) => "Xərçəng",
                (7, >= 23) or (8, <= 22) => "Şir",
                (8, >= 23) or (9, <= 22) => "Qız",
                (9, >= 23) or (10, <= 22) => "Tərəzi",
                (10, >= 23) or (11, <= 21) => "Əqrəb",
                (11, >= 22) or (12, <= 21) => "Oxatan",
                (12, >= 22) or (1, <= 19) => "Oğlaq",
                (1, >= 20) or (2, <= 18) => "Dolça",
                (2, >= 19) or (3, <= 20) => "Balıq",
                _ => "Naməlum"
            };
        }
    }
}
