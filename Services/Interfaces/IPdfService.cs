using ContractMaster.Models;

namespace ContractMaster.Services.Interfaces;

public interface IPdfService
{
    byte[] GenerateContractPdf(Contract contract);
}