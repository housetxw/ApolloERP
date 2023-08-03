using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class BasicInfoDTO
    {
        public string Key { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;
    }


    public class GroupSelectDTO
    {
        public string Label { get; set; } = string.Empty;

        public List<BasicInfoDTO> Options { get; set; } = new List<BasicInfoDTO>();


    }
}
