﻿namespace DrawingsApp.Groups.Services.Contracts
{
    public interface ITagService
    {
        Task<int> Create(string name,string tagInfo);
        Task<string> GetTagInfo(int id);
    }
}
