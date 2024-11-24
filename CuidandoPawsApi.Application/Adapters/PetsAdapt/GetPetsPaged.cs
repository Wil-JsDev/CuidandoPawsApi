﻿using AutoMapper;
using CuidandoPawsApi.Application.DTOs.Pets;
using CuidandoPawsApi.Domain.Pagination;
using CuidandoPawsApi.Domain.Ports.Repository;
using CuidandoPawsApi.Domain.Ports.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Application.Adapters.PetsAdapt
{
    public class GetPetsPaged : IGetPagedPets<PetsDTos>
    {

        private readonly IPetsRepository _petsRepository;
        private readonly IMapper _mapper;

        public GetPetsPaged(IPetsRepository petsRepository, IMapper mapper)
        {
            _petsRepository = petsRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PetsDTos>> ListWithPaginationAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var petsPagedWithNumbe = await _petsRepository.GetPagedPetsAsync(pageNumber, pageSize, cancellationToken);

            if (petsPagedWithNumbe != null)
            {
                var petsPagedDto = _mapper.Map<PagedResult<PetsDTos>>(petsPagedWithNumbe);
                return petsPagedDto;
            }

            return null;
        }
    }
}