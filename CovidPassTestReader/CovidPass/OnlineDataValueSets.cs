using CovidPassReader.CovidPass.Json;

using System.Net;
namespace CovidPassReader.CovidPass
{
    /// <summary>
    /// Database of all known things from EU
    /// </summary>
    public class OnlineDataValueSets
    {
        /// <summary>
        /// URIs provided by EU
        /// </summary>
        public const string manufacturersVaccinesUri = @"https://raw.githubusercontent.com/ehn-dcc-development/ehn-dcc-valuesets/main/vaccine-mah-manf.json"; //EU OFFICIAL
        /// <summary>
        /// URIs provided by EU
        /// </summary>
        public const string countryCodesUri = @"https://raw.githubusercontent.com/ehn-dcc-development/ehn-dcc-valuesets/main/country-2-codes.json"; //EU OFFICIAL
        /// <summary>
        /// URIs provided by EU
        /// </summary>
        public const string medicalProductsUri = @"https://raw.githubusercontent.com/ehn-dcc-development/ehn-dcc-valuesets/main/vaccine-medicinal-product.json"; //EU OFFICIAL
        /// <summary>
        /// URIs provided by EU
        /// </summary>
        public const string agentsTargetedUri = @"https://raw.githubusercontent.com/ehn-dcc-development/ehn-dcc-valuesets/main/disease-agent-targeted.json"; //EU OFFICIAL
        /// <summary>
        /// URIs provided by EU
        /// </summary>
        public const string vaccineProphylaxisUri = @"https://raw.githubusercontent.com/ehn-dcc-development/ehn-dcc-valuesets/main/vaccine-prophylaxis.json"; //EU OFFICIAL
        /// <summary>
        /// URIs provided by EU
        /// </summary>
        public const string manufacturersTestsUri = @"https://raw.githubusercontent.com/ehn-dcc-development/ehn-dcc-valuesets/main/test-manf.json"; //EU OFFICIAL
        /// <summary>
        /// URIs provided by EU
        /// </summary>
        public const string testResultUri = @"https://raw.githubusercontent.com/ehn-dcc-development/ehn-dcc-valuesets/main/test-result.json"; //EU OFFICIAL
        /// <summary>
        /// URIs provided by EU
        /// </summary>
        public const string testTypesUri = @"https://raw.githubusercontent.com/ehn-dcc-development/ehn-dcc-valuesets/main/test-type.json"; //EU OFFICIAL

        /// <summary>
        /// List of all vaccine manufacturers registered in EU
        /// </summary>
        /// <remarks>EU eHealthNetwork: Value Sets for Digital Covid Certificates. version 1.0, 2021-04-16, section 2.4</remarks>
        public Json.EUData ManufacturersVaccines { get; private set; }
        /// <summary>
        /// List of all medical products registered in EU
        /// </summary>
        /// <remarks>EU eHealthNetwork: Value Sets for Digital Covid Certificates. version 1.0, 2021-04-16, section 2.3</remarks>
        public Json.EUData VaccineMedicalProducts { get; private set; }
        /// <summary>
        /// List of all known countries to EU
        /// </summary>
        /// <remarks>Country of Vaccination / Test, ISO 3166 alpha-2 where possible</remarks>
        public Json.EUData CountryCodes { get; private set; }
        /// <summary>
        /// List of all known agents targeted to EU
        /// </summary>
        /// <remarks>EU eHealthNetwork: Value Sets for Digital Covid Certificates. version 1.0, 2021-04-16, section 2.1</remarks>
        public Json.EUData AgentsTargeted { get; private set; }
        /// <summary>
        /// List of all known vaccine prophylaxis to EU
        /// </summary>
        /// <remarks>EU eHealthNetwork: Value Sets for Digital Covid Certificates. version 1.0, 2021-04-16, section 2.2</remarks>
        public Json.EUData VaccineProphylaxis { get; private set; }
        /// <summary>
        /// List of all tests manufacturers registered in EU
        /// </summary>
        /// <remarks>EU eHealthNetwork: Value Sets for Digital Covid Certificates. version 1.0, 2021-04-16, section 2.8</remarks>
        public Json.EUData ManufacturersTests { get; private set; }
        /// <summary>
        /// List of all known test results to EU
        /// </summary>
        /// <remarks>EU eHealthNetwork: Value Sets for Digital Covid Certificates. version 1.0, 2021-04-16, section 2.9</remarks>
        public Json.EUData TestResults { get; private set; }
        /// <summary>
        /// List of all known test types to EU
        /// </summary>
        /// <remarks>EU eHealthNetwork: Value Sets for Digital Covid Certificates. version 1.0, 2021-04-16, section 2.7</remarks>
        public Json.EUData TestTypes { get; private set; }

        /// <summary>
        /// Automatically downloads all data from URIs above, no caching and constructs database
        /// </summary>
        public OnlineDataValueSets()
        {
            using WebClient webClient = new WebClient(); //Load all the data online
            ManufacturersVaccines = Json.EUData.FromJson(webClient.DownloadString(manufacturersVaccinesUri));
            VaccineMedicalProducts = Json.EUData.FromJson(webClient.DownloadString(medicalProductsUri));
            CountryCodes = Json.EUData.FromJson(webClient.DownloadString(countryCodesUri));
            AgentsTargeted = Json.EUData.FromJson(webClient.DownloadString(agentsTargetedUri));
            VaccineProphylaxis = Json.EUData.FromJson(webClient.DownloadString(vaccineProphylaxisUri));
            ManufacturersTests = Json.EUData.FromJson(webClient.DownloadString(manufacturersTestsUri));
            TestResults = Json.EUData.FromJson(webClient.DownloadString(testResultUri));
            TestTypes = Json.EUData.FromJson(webClient.DownloadString(testTypesUri));
        }
        /// <summary>
        /// This constructor is for loading cached data, please check <see cref="Json.EUData.ValueSetDate"/> to check if you are up to date.
        /// </summary>
        public OnlineDataValueSets(EUData manufacturersVaccines, EUData vaccineMedicalProducts, EUData countryCodes, EUData agentsTargeted, EUData vaccineProphylaxis, EUData manufacturersTests, EUData testResults, EUData testTypes)
        {
            ManufacturersVaccines = manufacturersVaccines;
            VaccineMedicalProducts = vaccineMedicalProducts;
            CountryCodes = countryCodes;
            AgentsTargeted = agentsTargeted;
            VaccineProphylaxis = vaccineProphylaxis;
            ManufacturersTests = manufacturersTests;
            TestResults = testResults;
            TestTypes = testTypes;
        }
    }
}
