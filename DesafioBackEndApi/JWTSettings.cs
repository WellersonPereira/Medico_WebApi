using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioBackEndApi
{
    public class JWTSettings
    {
        public string Secret { get; set; }
        public int ExpiracaoHoras { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
