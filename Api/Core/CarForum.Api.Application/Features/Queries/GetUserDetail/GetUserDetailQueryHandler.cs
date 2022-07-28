using AutoMapper;
using CarForum.Api.Application.Interfaces.Repositories;
using CarForum.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public  async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            Domain.Models.User dbUser = null;
            if(request.UserId!=Guid.Empty)
                dbUser=await _userRepo.GetByIdAsync(request.UserId);
            else if (!string.IsNullOrEmpty(request.UserName))
                dbUser=await _userRepo.GetSingleAsync(i=>i.Username==request.UserName); 
            
            return _mapper.Map<UserDetailViewModel>(dbUser);
           
        }
    }
}
