using ContractMaster.DTOs;
using ContractMaster.Models;
using ContractMaster.Models.Enums;
using ContractMaster.Repositories.Interfaces;

namespace ContractMaster.Services;

public class ContractService
{
    private readonly IContractRepository _repository;
    private readonly IApprovalRepository _approvalRepository;
    public ContractService(IContractRepository repository, IApprovalRepository approvalRepository)
    {
        _repository = repository;
        _approvalRepository=approvalRepository;
    }

    public async Task CreateContract(NewContractDTO newContract)
{
    var exists = await _repository.ExistsAsync(newContract.ContractId, newContract.Version);

    if (exists)
    {
        throw new InvalidOperationException("Contract already exists for this version.");
    }

    Contract contract = new()
    {
        ContractId = newContract.ContractId,
        Version = newContract.Version,
        ContractType = newContract.ContractType,
        Status = newContract.Status,
        StartDate = newContract.StartDate,
        EndDate = newContract.EndDate,
        CounterPartyName = newContract.CounterPartyName,
        CounterPartyEmail = newContract.CounterPartyEmail,
        CreatedById = newContract.CreatedById,
        CreatedAt = newContract.CreatedAt
    };

    await _repository.AddAsync(contract);

    var approvals = new List<Approvals>
    {
        new()
        {
            Version = contract.Version,
            Sequence = 1,
            ContractId = contract.Id,
            ApproverId = 1,
            ApproverType = ApproverType.Legal,
            ApprovalStatus = ApprovalStatus.Pending
        },
        new()
        {
            Version = contract.Version,
            Sequence = 2,
            ContractId = contract.Id,
            ApproverId = 1,
            ApproverType = ApproverType.Internal,
            ApprovalStatus = ApprovalStatus.Pending
        },
        new()
        {
            Version = contract.Version,
            Sequence = 3,
            ContractId = contract.Id,
            ApproverId = 1,
            ApproverType = ApproverType.External,
            ApprovalStatus = ApprovalStatus.Pending
        }
    };

    await _approvalRepository.AddRangeAsync(approvals);
}

    public async Task<List<ContractDTO>> GetContracts()
    {
        var contracts = await _repository.GetAllAsync();

        return contracts.Select(contract => new ContractDTO(
            contract.Id,
            contract.ContractId,
            contract.Version,
            contract.ContractType,
            contract.Status,
            contract.StartDate,
            contract.EndDate,
            contract.CounterPartyName,
            contract.CounterPartyEmail,
            contract.CreatedAt
        )).ToList();
    }

    public async Task DeleteById(int id)
    {
        var contract = await _repository.GetByIdAsync(id);

        if(contract == null)
        {
            throw new FileNotFoundException();
        }

        await _repository.DeleteAsync(contract);
    }
}