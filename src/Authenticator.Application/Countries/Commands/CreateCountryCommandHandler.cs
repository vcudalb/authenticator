using Authenticator.Domain.Common;
using Authenticator.Domain.Entities;
using MediatR;

namespace Authenticator.Application.Countries.Commands;

public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCountryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Guid> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        Address addressEntity = new();
        await _unitOfWork.AddressRepository.InsertAsync(addressEntity, cancellationToken);
        await _unitOfWork.AddressRepository.SaveChangesAsync(cancellationToken);

        return addressEntity.Id;
    }
}