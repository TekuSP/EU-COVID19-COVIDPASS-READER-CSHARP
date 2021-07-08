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
