using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Contracts.Models
{
    public class OmsTypeResponseModel
    {
        public bool IsHeadersRequired { get; set; }
        public string Delimiter { get; set; }
        public string FileType { get; set; }
        public string OmsType { get; set; }
        public List<OmsResponseModel> OmsData { get; set; }

        public OmsTypeResponseModel()
        {
            OmsData = new List<OmsResponseModel>();
        }
    }
}
