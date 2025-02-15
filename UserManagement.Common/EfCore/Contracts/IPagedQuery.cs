﻿using MediatR;

namespace UserManagement.Common.EfCore.Contracts
{
    public interface IPagedQuery : IRequest
    {
        int Page { get; }
        int Results { get; }
        string OrderBy { get; }
        string SortOrder { get; }
    }
}