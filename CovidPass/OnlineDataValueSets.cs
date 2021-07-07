using System.Net;
namespace CovidPassTestReader.CovidPass
{
    /// <summary>
    /// Database of all known things from EU
    /// </summary>
    public class OnlineDataValueSets
    {
        /// <summary>
        /// URIs provided by EU
        /// </summary>
        public const string manufacturersUri = @"https://raw.githubusercontent.com/ehn-dcc-development/ehn-dcc-valuesets/main/vaccine-mah-manf.json"; //EU OFFICIAL
        /// <summary>
        /// URIs provided by EU
        /// </summary>
        public const string countryCodesUri = @"https://raw.githubusercontent.com/ehn-dcc-development/ehn-dcc-valuesets/main/country-2-codes.json"; //EU OFFICIAL
        /// <summary>
        /// URIs provided by EU
        /// </summary>
        public const string medicalProductsUri = @"https://raw.githubusercontent.com/ehn-dcc-development/ehn-dcc-valuesets/main/vaccine-medicinal-product.json"; //EU OFFICIAL

        /// <summary>
        /// List of all manufacturers registered in EU
        /// </summary>
        public Json.Manufacturers Manufacturers { get; private set; }
        /// <summary>
        /// List of all medical products registered in EU
        /// </summary>
        public Json.MedicalProducts MedicalProducts { get; private set; }
        /// <summary>
        /// List of all known countries to EU
        /// </summary>
        public Json.CountryCodes CountryCodes { get; private set; }

        /// <summary>
        /// This constructor is for loading cached data, please check <see cref="Json.Manufacturers.ValueSetDate"/> or <see cref="Json.MedicalProducts.ValueSetDate"/> or <see cref="Json.CountryCodes.ValueSetDate"/> to check if you are up to date.
        /// </summary>
        /// <param name="manufacturers">Manufacturers to load</param>
        /// <param name="medicalProducts">Medical products to load</param>
        /// <param name="countryCodes">Country codes to load</param>
        public OnlineDataValueSets(Json.Manufacturers manufacturers, Json.MedicalProducts medicalProducts, Json.CountryCodes countryCodes)
        {
            //Load data from given values (cache)
            Manufacturers = manufacturers;
            MedicalProducts = medicalProducts;
            CountryCodes = countryCodes;
        }
        /// <summary>
        /// Automatically downloads all data from URIs above, no caching and constructs database
        /// </summary>
        public OnlineDataValueSets()
        {
            using WebClient webClient = new WebClient(); //Load all the data online
            Manufacturers = Json.Manufacturers.FromJson(webClient.DownloadString(manufacturersUri));
            MedicalProducts = Json.MedicalProducts.FromJson(webClient.DownloadString(medicalProductsUri));
            CountryCodes = Json.CountryCodes.FromJson(webClient.DownloadString(countryCodesUri));
        }
    }
}
