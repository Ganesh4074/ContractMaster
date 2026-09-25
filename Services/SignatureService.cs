using ContractMaster.Constants;
using ContractMaster.DTOs;
using ContractMaster.Exceptions;
using ContractMaster.Models;
using ContractMaster.Models.Enums;
using ContractMaster.Repositories.Interfaces;

namespace ContractMaster.Services;

public class SignatureService
{
    private readonly ISignatureRepository _repository;
    private readonly ContractService _contractService;

    public SignatureService(ISignatureRepository repository, ContractService contractService)
    {
        _repository = repository;
        _contractService = contractService;
    }

    public async Task<List<GetSignatureDTO>> GetSignatures()
    {
        var signatures = await _repository.GetAllAsync();

        return signatures.Select(signature => new GetSignatureDTO(
                signature.Id,
                signature.ContractId,
                signature.Version,
                signature.SignatoryId,
                signature.SignatureType,
                signature.SignatureStatus
            )
        ).ToList();
    }

    public async Task<GetSignatureDTO?> UpdateSignature(int id, UpdateSignatureDTO update, int userId, string userRole)
    {
        var signature = await _repository.GetByIdAsync(id);
        //check if signature exists
        if (signature is null)
        {
            return null;
        }
        //Check if Signature is already signed
        if (signature.SignatureStatus == SignatureStatus.Signed)
        {
            throw new BusinessRuleException("Signature is already signed.");
        }
        //Check if it is the correct Signatory for the signature
        var isAuthorized = signature.SignatureType switch
        {
            SignatureType.Internal =>
                userRole == RoleNames.InternalSignatory,

            SignatureType.External =>
                userRole == RoleNames.ExternalSignatory,

            _ => false
        };

        if (!isAuthorized)
        {
            throw new BusinessRuleException(
                "You are not authorized to sign this signature.");
        }


        signature.SignatureStatus = SignatureStatus.Signed;
        signature.SignatoryId = userId;

        await _repository.UpdateAsync(signature);

        var signatures = await _repository.GetByContractAndVersionAsync(
                signature.ContractId,
                signature.Version);

        var allSigned = signatures.Count > 0 && signatures.All(item => item.SignatureStatus == SignatureStatus.Signed);

        if (allSigned)
        {
            await _contractService.UpdateStatus(
                signature.ContractId,
                signature.Version,
                ContractStatus.Active);
        }

        return new GetSignatureDTO(
            signature.Id,
            signature.ContractId,
            signature.Version,
            signature.SignatoryId,
            signature.SignatureType,
            signature.SignatureStatus
        );
    }
    public async Task<GetSignatureDTO> CreateSignature(NewSignatureDTO newSignature)
    {
        var signature = new Signature
        {
            ContractId = newSignature.ContractId,
            Version = newSignature.Version,
            SignatoryId = 0,
            SignatureType = newSignature.SignatureType,
            SignatureStatus = SignatureStatus.NotSigned
        };

        await _repository.AddAsync(signature);

        return new GetSignatureDTO(
            signature.Id,
            signature.ContractId,
            signature.Version,
            signature.SignatoryId,
            signature.SignatureType,
            signature.SignatureStatus
        );
    }
}