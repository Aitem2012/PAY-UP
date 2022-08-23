using AutoMapper;
using PAY_UP.Application.Abstracts.Repositories;
using PAY_UP.Application.Abstracts.Services;
using PAY_UP.Application.Dtos.Users;
using PAY_UP.Common.Extensions;
using PAY_UP.Common.Helpers;

namespace PAY_UP.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ResponseObject<GetUserDto>> CreateAsync(CreateUserDto entity, string role = "user")
        {
            var user = await _userRepository.CreateAsync(entity, new CancellationToken(), role);
            if (user.IsNull())
            {
                return new ResponseObject<GetUserDto>().CreateResponse("User email already exist", false, null);
            }
            return new ResponseObject<GetUserDto>().CreateResponse("User successfully created, Please check your " +
                "email for email confirmation", true, _mapper.Map<GetUserDto>(user));
        }

        public async Task<ResponseObject<bool>> DeleteUserAsync(string id)
        {
            var userIsDeleted = await _userRepository.DeleteUserAsync(id, new CancellationToken());
            if (!userIsDeleted)
            {
                return new ResponseObject<bool>().CreateResponse("User could not be deleted", false, userIsDeleted);
            }
            return new ResponseObject<bool>().CreateResponse("User Deleted Successfully", true, userIsDeleted);
        }


        public async Task<ResponseObject<IEnumerable<GetUserDto>>> GetAllAsync(bool isActive = false)
        {
            var users = await _userRepository.GetAllAsync(isActive);
            return new ResponseObject<IEnumerable<GetUserDto>>().CreateResponse($"Successfully retrieved {users.Count()}", true,
                _mapper.Map<IEnumerable<GetUserDto>>(users));
        }

        public async Task<ResponseObject<GetUserDto>> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return new ResponseObject<GetUserDto>().CreateResponse("", true, _mapper.Map<GetUserDto>(user));
        }

        public async Task<ResponseObject<GetUserDto>> GetByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return new ResponseObject<GetUserDto>().CreateResponse("", true, _mapper.Map<GetUserDto>(user));
        }

        public async Task<ResponseObject<GetUserDto>> UpdateAsync(UpdateUserDto entity)
        {
            var user = await _userRepository.UpdateAsync(entity, new CancellationToken());
            if (user.IsNull())
            {
                return new ResponseObject<GetUserDto>().CreateResponse("User does not exist", false, null);
            }
            return new ResponseObject<GetUserDto>().CreateResponse("User updated successfully", true, _mapper.Map<GetUserDto>(user));
        }
    }
}
