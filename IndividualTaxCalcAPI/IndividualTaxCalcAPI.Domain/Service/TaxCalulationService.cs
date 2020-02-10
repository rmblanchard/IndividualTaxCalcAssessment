using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using IndividualTaxCalcAPI.Entity;
using IndividualTaxCalcAPI.Entity.UnitofWork;

namespace IndividualTaxCalcAPI.Domain.Service
{
    public class TaxCalculationService<Tv, Te> : GenericService<Tv, Te>
                                        where Tv : TaxCalculationViewModel
                                        where Te : TaxCalculation
    {
        //DI must be implemented in specific service as well beside GenericService constructor
        public TaxCalculationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            if (_mapper == null)
                _mapper = mapper;
        }

        //add here any custom service method or override generic service method
        public bool DoNothing()
        {
            return true;
        }
    }
}