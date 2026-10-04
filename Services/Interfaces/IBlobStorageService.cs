namespace ContractMaster.Services.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadPdfAsync(byte[] pdf, string contractId, int version);
}