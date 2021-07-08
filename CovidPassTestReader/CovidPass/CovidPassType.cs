//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
namespace CovidPassReader.CovidPass
{
    /// <summary>
    /// Covid pass type
    /// </summary>
    public enum CovidPassType
    {
        /// <summary>
        /// No idea
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Vaccination report
        /// </summary>
        Vaccine,

        /// <summary>
        /// Test report
        /// </summary>
        TestProof,

        /// <summary>
        /// Covid recovery report
        /// </summary>
        RecoveryProof
    }
}