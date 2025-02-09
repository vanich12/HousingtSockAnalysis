namespace HousingAnalysis.ApiServer.Extension
{
    public class JwtOptions
    {
        public string Key { get; set; } = String.Empty;
        public int ExpiresHours { get; set; }
    }
}
