using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class LevelDataVo
    {
        /// <summary>
        /// 
        /// </summary>
        public List<LevelDataResult> Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LevelDataAdditional Additional { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LevelDataResult
    {
        /// <summary>
        /// 力洋Id
        /// </summary>
        public string LevelId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LevelDataAdditional
    {
        /// <summary>
        /// 
        /// </summary>
        public string Vin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VinYear { get; set; }
    }
}
