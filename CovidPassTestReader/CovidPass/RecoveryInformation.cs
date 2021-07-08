using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidPassReader.CovidPass
{
    public record RecoveryInformation
    {
        /// <summary>
        /// disease or agent targeted
        /// </summary>
        public Json.ValueSetValue AgentTargeted { get; set; }
        /// <summary>
        /// ISO 8601 complete date of first positive NAA test result
        /// </summary>
        public DateTime FirstPositiveDate { get; set; }
        /// <summary>
        /// Country of Test
        /// </summary>
        public Json.ValueSetValue CountryOfTest { get; set; }
        /// <summary>
        /// ISO 8601 complete date: Certificate Valid From
        /// </summary>
        public DateTime ValidFrom { get; set; }
        /// <summary>
        /// ISO 8601 complete date: Certificate Valid Until
        /// </summary>
        public DateTime ValidUntil { get; set; }
        /// <summary>
        /// Certificate Issuer
        /// </summary>
        public string CertificationIssuer { get; set; }
        /// <summary>
        /// Unique Certificate Identifier, UVCI
        /// </summary>
        public string UVCI { get; set; }
        /// <summary>
        /// Is certificate currently valid? DateTime check only.
        /// </summary>
        public bool IsValid => DateTime.Now < ValidUntil && DateTime.Now > ValidFrom;
    }
}
