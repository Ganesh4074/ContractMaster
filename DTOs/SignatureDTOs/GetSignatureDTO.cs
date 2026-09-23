using ContractMaster.Models.Enums;

namespace ContractMaster.DTOs;

public record GetSignatureDTO(
    int Id,
    int ContractId,
    int Version,
    int SignatoryId,
    SignatureType SignatureType,
    SignatureStatus SignatureStatus
);