using Grpc.Core;
using GrpcPersonService.Models;

namespace GrpcPersonService.Services
{
    public class PersonGrpcService : PersonService.PersonServiceBase
    {
        private readonly IRepository<Person> _repository;
        private readonly ILogger<PersonGrpcService> _logger;

        public PersonGrpcService(IRepository<Person> repository, ILogger<PersonGrpcService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public override async Task<PersonResponse> CreatePerson(Person request, ServerCallContext context)
        {
            if (request == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Request cannot be null"));

            if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
                return new PersonResponse { Success = false, Message = "FirstName and LastName are required." };

            if (!PersonModel.IsValidNationalCode(request.NationalCode))
                return new PersonResponse { Success = false, Message = "Invalid National Code. It must be a valid 10-digit number." };

            if (!PersonModel.IsValidBirthDate(request.BirthDate))
                return new PersonResponse { Success = false, Message = "Invalid Birth Date. Use format yyyy-MM-dd." };

            try
            {
                await _repository.AddAsync(request);
                return new PersonResponse { Success = true, Message = "Person created successfully." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating person.");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing your request."));
            }
        }

        public override async Task<Person> GetPerson(PersonRequest request, ServerCallContext context)
        {
            if (request == null || request.Id <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ID"));

            try
            {
                var person = await _repository.GetByIdAsync(request.Id);
                if (person == null)
                    throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));

                return person;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving person.");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing your request."));
            }
        }

        public override async Task<PersonResponse> UpdatePerson(Person request, ServerCallContext context)
        {
            if (request == null || request.Id <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

            if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
                return new PersonResponse { Success = false, Message = "FirstName and LastName are required." };

            if (!PersonModel.IsValidNationalCode(request.NationalCode))
                return new PersonResponse { Success = false, Message = "Invalid National Code. It must be a valid 10-digit number." };

            if (!PersonModel.IsValidBirthDate(request.BirthDate))
                return new PersonResponse { Success = false, Message = "Invalid Birth Date. Use format yyyy-MM-dd." };

            try
            {
                var existingPerson = await _repository.GetByIdAsync(request.Id);
                if (existingPerson == null)
                    throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));

                await _repository.UpdateAsync(request);
                return new PersonResponse { Success = true, Message = "Person updated successfully." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating person.");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing your request."));
            }
        }

        public override async Task<PersonResponse> DeletePerson(PersonRequest request, ServerCallContext context)
        {
            if (request == null || request.Id <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ID"));

            try
            {
                var existingPerson = await _repository.GetByIdAsync(request.Id);
                if (existingPerson == null)
                    throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));

                await _repository.DeleteAsync(request.Id);
                return new PersonResponse { Success = true, Message = "Person deleted successfully." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting person.");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing your request."));
            }
        }

        public override async Task<PersonsList> GetAllPersons(Empty request, ServerCallContext context)
        {
            try
            {
                var persons = await _repository.GetAllAsync();
                var response = new PersonsList();
                response.Persons.AddRange(persons);
                return response;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error occured while retriving persons .");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing your request."));
            }
          

        }
    }
}
