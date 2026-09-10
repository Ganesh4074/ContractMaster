namespace ContractMaster.Configurations;

public class JWTSettings
{
    public string Key{get;set;}=String.Empty;
    public string Issuer{get;set;}=String.Empty;
    public string Audience{get;set;}=String.Empty;
    public int ExpiryMinutes{get;set;}=30;
}