﻿using DrawingsApp.Data.Common;

namespace DrawingsApp.Groups.Models.InputModels.Group
{
    public class SearchGroupInputModel
    {
        public string? Name { get; set; }
        public List<int>? Tags { get; set; }
        public string? UserId { get; set; }
        public SortType Order { get; set; }
        public GroupType? GroupType { get; set; }
        public int Page { get; set; } 
    }
}
