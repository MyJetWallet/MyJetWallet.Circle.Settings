using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyJetWallet.Circle.Settings.Domain
{
    [DataContract]
    public class BankAccountCountry
    {
        [DataMember(Order = 1)]
        public string CountryName { get; set; }


        [DataMember(Order = 2)]
        public string Alpha2Code { get; set; }


        [DataMember(Order = 3)]
        public string Alpha3Code { get; set; }


        [DataMember(Order = 4)]
        public int Numeric { get; set; }


        [DataMember(Order = 5)]
        public BankAccountType BankAccountType { get; set; }
    }
}
