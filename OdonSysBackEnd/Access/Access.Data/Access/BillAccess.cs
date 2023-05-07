﻿using Access.Contract.Bills;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Access.Data.Access
{
    internal class BillAccess : IBillAccess
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public BillAccess(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<BillAccessModel> CreateBillAsync(HeaderBillAccessRequest accessRequest)
        {
            var entity = _mapper.Map<HeaderBill>(accessRequest);
            _context.HeaderBills.Add(entity);
            await _context.SaveChangesAsync();
            var clientProcedureIds = entity.BillDetails.Select(x => x.ClientProcedureId);
            var clientProcedureEntities = _context.ClientProcedures
                                                    .Include(x => x.Procedure)
                                                    .Where(x => clientProcedureIds.Contains(x.Id));
            return new BillAccessModel(
                entity.Id,
                entity.BillNumber,
                entity.Iva10,
                entity.TotalIva,
                entity.SubTotal,
                entity.Total,
                entity.Timbrado,
                entity.Status,
                entity.ClientId,
                entity.BillDetails.Select(x => new BillDetailAccessModel(
                    x.Id,
                    x.HeaderBillId,
                    clientProcedureEntities.First(y => y.Id == x.ClientProcedureId).Procedure.Name,
                    x.ProcedurePrice,
                    x.FinalPrice
                ))
            );
        }
    }
}
