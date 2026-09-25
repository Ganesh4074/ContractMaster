using ContractMaster.DTOs;
using ContractMaster.Models.Enums;

namespace ContractMaster.Services;

public interface IContractService
{
    Task CreateContract(NewContractDTO newContract, int userId);

    Task<List<ContractDTO>> GetContracts();

    Task DeleteById(int id);

    Task UpdateStatus(
        int contractId,
        int version,
        ContractStatus status);
}