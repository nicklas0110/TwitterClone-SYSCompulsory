using AuthorisationService.Core.Entities;

namespace AuthorisationService.Core.Repositories;

public interface IAuthorisationRepository
{
    public Task Register(Authorisation authorisation);

    public Task<Authorisation> GetAuthorisationById(int authorisationId);
    public Task<Authorisation> GetAuthorisationByEmail(string email);

    public Task DeleteAuthorisation(int authorisationId);

    public Task<bool> DoesAuthorisationExists(string email);
    
}